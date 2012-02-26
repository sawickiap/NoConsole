using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.CodeDom.Compiler;
using System.Reflection;
using System.Diagnostics;
using NoConsoleLib;

namespace NoConsole
{
    public partial class Form1 : Form
    {
        private const string SCRIPT_PAGE_TITLE = "Select Script";
        // TODO Set this to false in release version.
        private const bool COMPILED_SCRIPT_INCLUDE_DEBUG_INFORMATION = false;
        
        public Form1(string[] args)
        {
            InitializeComponent();

            Text = Lib.APP_TITLE;
            m_TitleLabel.Text = SCRIPT_PAGE_TITLE;

            Font boldFont = new Font(this.Font, FontStyle.Bold);
            Font monospacedFont = CreateMonospacedFont();

            Lib.Init(
                this,                            // mainForm
                m_TitleLabel,
                m_BottomButtonPrev,
                m_BottomButtonNext,
                m_SettingsButton,
                m_ToolTip,
                this.Font,                       // normalFont
                boldFont,
                monospacedFont,
                Directory.GetCurrentDirectory(), // startupDir
                Application.StartupPath);        // appDir

            // Must be called after Lib is initialzed with ScriptDataDir
            ParseCommandLine(args);
        }

        enum CmdLineArg
        {
            None,
            SetScriptsDir,
            AddScriptsDir,
        };

        // Filled by ParseCommandLine, called in constructor.
        private List<string> m_ScriptDirs = new List<string>();

        private void ParseCommandLine(string[] args)
        {
            m_ScriptDirs.Add(Path.Combine(Lib.StartupDir, "Scripts"));
            m_ScriptDirs.Add(Lib.ScriptDataDir);

            CmdLineArg lastArg = CmdLineArg.None;

            foreach (string arg in args)
            {
                if (arg == "/SetScriptsDir")
                    lastArg = CmdLineArg.SetScriptsDir;
                else if (arg == "/AddScriptsDir")
                    lastArg = CmdLineArg.AddScriptsDir;
                else
                {
                    switch (lastArg)
                    {
                        case CmdLineArg.AddScriptsDir:
                            m_ScriptDirs.Add(arg);
                            break;

                        case CmdLineArg.SetScriptsDir:
                            m_ScriptDirs.Clear();
                            m_ScriptDirs.Add(arg);
                            break;

                        default:
                            throw new Exception("Invalid command line argument: " + arg);
                    }
                }
            }
        }

        private void DeferredInit()
        {
            try
            {
                Lib.WaitCursor = true;

                LoadScriptList();

                LoadGlobalConfig();

                Lib.WaitCursor = false;
            }
            catch (Exception ex)
            {
                Lib.ShowError("Cannot initialize program.", ex);
                Close();
            }
        }

        private Font CreateMonospacedFont()
        {
            string familyName = "Courier New";

            using (System.Drawing.Text.InstalledFontCollection fontCollection =
                new System.Drawing.Text.InstalledFontCollection())
            {
                foreach (FontFamily fontFamily in fontCollection.Families)
                {
                    if (fontFamily.Name == "Consolas")
                    {
                        familyName = "Consolas";
                        break;
                    }
                }
            }

            return new Font(familyName, 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        }

        /// <summary>
        /// On error: Show message box.
        /// </summary>
        private void LoadScriptList()
        {
            try
            {
                m_ScriptsListView.BeginUpdate();

                m_ScriptsListView.Items.Clear();

                const string SCRIPT_FILE_MASK = "*.cs";

                foreach (string dir in m_ScriptDirs)
                {
                    string[] scriptFiles = Directory.GetFiles(dir, SCRIPT_FILE_MASK);
                    foreach (string scriptFile in scriptFiles)
                        LoadScriptFile(scriptFile);
                }
            }
            catch (Exception ex)
            {
                Lib.ShowError("Cannot load script list.", ex);
            }
            finally
            {
                m_ScriptsListView.EndUpdate();
            }
        }

        private void LoadScriptFile(string scriptFilePath)
        {
            string scriptName = Path.GetFileNameWithoutExtension(scriptFilePath);
            ListViewItem item = new ListViewItem(scriptName, Lib.IMG_SCRIPT);
            // Tag for m_ScriptListView items will be string with full path to CS file.
            item.Tag = scriptFilePath;
            m_ScriptsListView.Items.Add(item);
        }

        private void m_BottomButtonNext_Click(object sender, EventArgs e)
        {
            // We are inside script execution.
            if (m_ScriptObj != null && m_NextClickMethod != null)
            {
                m_NextClickMethod.Invoke(m_ScriptObj, null);
            }
            // We are on script selection page.
            else
            {
                Debug.Assert(Lib.ActivePage == null);

                if (m_ScriptsListView.SelectedItems.Count == 1)
                {
                    ListViewItem scriptItem = m_ScriptsListView.SelectedItems[0];
                    string scriptPath = scriptItem.Tag as string;
                    StartScript(scriptItem.Text, scriptPath);
                }
                else
                {
                    MessageBox.Show(
                        this,
                        "Please select a script.",
                        Lib.APP_TITLE,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }

        private void StartScript(string scriptName, string scriptPath)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                string assemblyPath = Path.Combine(Lib.ScriptDataDir, scriptName + ".dll");

                bool alreadyCompiled = false;

                FileInfo assemblyFileInfo = new FileInfo(assemblyPath);
                if (assemblyFileInfo.Exists)
                {
                    FileInfo scriptFileinfo = new FileInfo(scriptPath);
                    if (assemblyFileInfo.LastWriteTime > scriptFileinfo.LastWriteTime)
                    {
                        alreadyCompiled = true;
                    }
                }

                if (!alreadyCompiled)
                    CompileScript(scriptPath, assemblyPath);

                Assembly assembly = Assembly.LoadFrom(assemblyPath);

                Type type = assembly.GetType("Script", true);

                // We now know that compilation succeeded (almost).

                Lib.SetScriptParams(scriptName, scriptPath);
                m_LoadProfileButton.Visible = true;
                m_SaveProfileButton.Visible = true;
                m_ScriptPanel.Visible = false;

                m_ScriptObj = assembly.CreateInstance("Script", false, BindingFlags.CreateInstance, null, new object[] { }, null, null);
                if (m_ScriptObj == null)
                    throw new Exception("Class \"Script\" not found.");

                m_NextClickMethod = type.GetMethod(
                    "OnNextClick",
                    BindingFlags.Instance | BindingFlags.Public,
                    null,
                    new Type[] { },
                    null);
                m_PrevClickMethod = type.GetMethod(
                    "OnPrevClick",
                    BindingFlags.Instance | BindingFlags.Public,
                    null,
                    new Type[] { },
                    null);
                m_SettingsClickMethod = type.GetMethod(
                    "OnSettingsClick",
                    BindingFlags.Instance | BindingFlags.Public,
                    null,
                    new Type[] { },
                    null);
                m_ExitMethod = type.GetMethod(
                    "OnExit",
                    BindingFlags.Instance | BindingFlags.Public,
                    null,
                    new Type[] { },
                    null);

                /*
                Type type = obj.GetType();
                type.InvokeMember(
                    "Main",
                    BindingFlags.Instance | BindingFlags.InvokeMethod | BindingFlags.Public,
                    null,
                    obj,
                    new object[] { });
                */
            }
            catch (Exception ex)
            {
                TextDialog.ShowText(
                    this,
                    SystemIcons.Error.ToBitmap(),
                    "Run Script",
                    ex.Message,
                    true);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// On error: Throw exception.
        /// </summary>
        private static void CompileScript(string scriptPath, string assemblyPath)
        {
            using (CodeDomProvider codeDomProvider = new Microsoft.CSharp.CSharpCodeProvider())
            {
                string[] assemblyNames = new string[]{
                        "mscorlib.dll",
                        "System.dll",
                        "System.Data.dll",
                        "System.Drawing.dll",
                        "System.Xml.dll",
                        "System.Core.dll",
                        "System.Windows.Forms.dll",
                        Path.Combine(Lib.AppDir, "NoConsoleLib.dll")
                    };

                CompilerParameters compilerParameters = new CompilerParameters(assemblyNames)
                {
                    OutputAssembly = assemblyPath,
                    GenerateExecutable = false,
                    GenerateInMemory = false,
                    WarningLevel = 3,
                    CompilerOptions = "/optimize",
                    IncludeDebugInformation = COMPILED_SCRIPT_INCLUDE_DEBUG_INFORMATION,
                    //TempFiles = new TempFileCollection(".", true)
                };

                CompilerResults compilerResults = codeDomProvider.CompileAssemblyFromFile(
                    compilerParameters,
                    new string[] { scriptPath });

                /* This prints low-level messages like this, as well as error messages:
                * 
                * G:\tmp\CsCompilerTest\WorkingDir> "C:\Windows\Microsoft.NET\Framework\v4.0.30319
                * \csc.exe" /t:library /utf8output /out:"C:\Users\Adam Sawicki\AppData\Local\Temp\
                * 0pdzupen.dll" /debug- /optimize+ /w:3 /optimize  "C:\Users\Adam Sawicki\AppData\
                * Local\Temp\0pdzupen.0.cs"

                * Microsoft (R) Visual C# 2010 Compiler version 4.0.30319.1
                * Copyright (C) Microsoft Corporation. All rights reserved.
                */
                //foreach (String message in compilerResults.Output)
                //    Console.WriteLine(message);

                /* This prints error messages in form of:
                * 
                * c:\Users\Adam Sawicki\AppData\Local\Temp\4kbqoyz2.0.cs(7,9) : error CS0103: The
                * name 'Console' does not exist in the current context
                */
                //foreach (CompilerError error in compilerResults.Errors)
                //    Console.WriteLine(error.ToString());

                if (compilerResults.NativeCompilerReturnValue != 0)
                {
                    StringBuilder sb = new StringBuilder("Compilation failed.\n");

                    foreach (CompilerError error in compilerResults.Errors)
                        sb.AppendLine(error.ToString());

                    throw new Exception(sb.ToString());
                }
            }
        }

        private void m_ScriptsListView_DoubleClick(object sender, EventArgs e)
        {
            m_BottomButtonNext_Click(sender, null);
        }

        private void m_ScriptsListView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                m_BottomButtonNext_Click(sender, null);
        }

        object m_ScriptObj;
        MethodInfo m_NextClickMethod, m_PrevClickMethod, m_SettingsClickMethod, m_ExitMethod;

        private void m_BottomButtonPrev_Click(object sender, EventArgs e)
        {
            if (m_ScriptObj != null && m_PrevClickMethod != null)
                m_PrevClickMethod.Invoke(m_ScriptObj, null);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_ScriptObj != null && m_ExitMethod != null)
            {
                bool closeOk = (bool)m_ExitMethod.Invoke(m_ScriptObj, null);
                if (!closeOk)
                    e.Cancel = true;
            }

            SaveGlobalConfig();
        }

        private string CalcGlobalConfigFilePath()
        {
            return Path.Combine(Lib.AppDataDir, "Global.config");
        }

        /// <summary>
        /// On error: Show message box.
        /// </summary>
        private void SaveGlobalConfig()
        {
            string configFilePath = CalcGlobalConfigFilePath();

            try
            {
                if (m_ScriptsListView.SelectedItems.Count == 1)
                    Lib.GlobalConfig.SetValue(CONFIG_SELECTED_SCRIPT, m_ScriptsListView.SelectedItems[0].Text);

                using (StreamWriter streamWriter = new StreamWriter(configFilePath, false, Encoding.UTF8))
                {
                    ConfigWriter configWriter = new ConfigWriter(streamWriter);
                    configWriter.Process(Lib.GlobalConfig);
                }
            }
            catch (Exception ex)
            {
                Lib.ShowError(
                    string.Format("Cannot save global configuration to \"{0}\".", configFilePath),
                    ex);
            }
        }

        private void LoadGlobalConfig()
        {
            string configFilePath = CalcGlobalConfigFilePath();

            try
            {
                if (File.Exists(configFilePath))
                {
                    using (StreamReader streamReader = new StreamReader(configFilePath, Encoding.UTF8, true))
                    {
                        ConfigReader configReader = new ConfigReader(streamReader);
                        configReader.Process(Lib.GlobalConfig);
                    }

                    PreselectScript();
                }
            }
            catch (Exception ex)
            {
                Lib.ShowError(
                    string.Format("Cannot load global configuration from \"{0}\".", configFilePath),
                    ex);
            }
        }

        private void PreselectScript()
        {
            Debug.Assert(Lib.GlobalConfig != null);

            string s = null;
            if (Lib.GlobalConfig.GetValue(CONFIG_SELECTED_SCRIPT, ref s))
                SelectScript(s);
        }

        /// <summary>
        /// Select script with given name in m_ScriptsListView.
        /// If not found, do nothing.
        /// </summary>
        private void SelectScript(string name)
        {
            ListViewItem item = m_ScriptsListView.FindItemWithText(name, false, 0, false);
            if (item != null)
                item.Selected = true;
        }

        private void m_InitTimer_Tick(object sender, EventArgs e)
        {
            m_InitTimer.Enabled = false;
            DeferredInit();
        }

        private const string CONFIG_SELECTED_SCRIPT = "SelectedScript";

        private void m_LoadProfileButton_Click(object sender, EventArgs e)
        {
            ProfileDialog dialog = new ProfileDialog();
            dialog.Init(false);
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                Lib.WaitCursor = true;
                Lib.LoadScriptConfig(dialog.SelectedName);
                Lib.WaitCursor = false;
            }
        }

        private void m_SaveProfileButton_Click(object sender, EventArgs e)
        {
            ProfileDialog dialog = new ProfileDialog();
            dialog.Init(true);
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                Lib.WaitCursor = true;
                Lib.SaveScriptConfig(dialog.SelectedName);
                Lib.WaitCursor = false;
            }
        }

        private void m_RefreshScriptListButton_Click(object sender, EventArgs e)
        {
            Lib.WaitCursor = true;
            LoadScriptList();
            PreselectScript();
            Lib.WaitCursor = false;
        }

        private void m_SettingsButton_Click(object sender, EventArgs e)
        {
            if (m_ScriptObj != null && m_SettingsClickMethod != null)
            {
                m_SettingsClickMethod.Invoke(m_ScriptObj, null);
            }
        }

        /*
        private void ProgramSearchButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                string[] interestingDirNames = {
                    "microsoft",
                    "dx",
                    "directx",
                    "sdk",
                    "utilities",
                    "bin",
                    "x86" };

                List<string> startDirs = new List<string>();

                startDirs.Add(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
                
                foreach (System.IO.DriveInfo Drive in System.IO.DriveInfo.GetDrives())
                    if (Drive.DriveType == System.IO.DriveType.Fixed)
                        startDirs.Add(Drive.RootDirectory.Name);

                string result = Globals.IntelligentSearch(
                    "fxc.exe",
                    false,
                    interestingDirNames,
                    startDirs.ToArray(),
                    TimeSpan.FromMilliseconds(5000.0));

                if (!string.IsNullOrEmpty(result))
                    ProgramTextbox.Text = result;
            }
            catch (Exception ex)
            {
                Lib.ShowError(ex);
            }
            finally
            {
                this.Cursor = Cursors.Arrow;
            }
        }
        */
    }
}
