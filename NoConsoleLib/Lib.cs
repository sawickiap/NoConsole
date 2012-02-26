using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using System.Diagnostics;

namespace NoConsoleLib
{
    public static class Lib
    {
        /////////////////////////////////////////////////////////////////////////////////
        // Interface for host application and other classes from NoConsoleLib

        public static void Init(
            Form mainForm,
            Label titleLabel,
            Button prevButton,
            Button nextButton,
            Button settingsButton,
            ToolTip toolTip,
            Font normalFont,
            Font boldFont,
            Font monospacedFont,
            string startupDir,
            string appDir)
        {
            m_Initialized = true;

            m_MainForm = mainForm;
            m_TitleLabel = titleLabel;
            m_PrevButton = prevButton;
            m_NextButton = nextButton;
            m_SettingsButton = settingsButton;
            m_ToolTip = toolTip;

            m_NormalFont = normalFont;
            m_BoldFont = boldFont;
            m_MonospacedFont = monospacedFont;

            m_StartupDir = startupDir;
            m_AppDir = appDir;

            m_AppDataDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "NoConsole");
            Directory.CreateDirectory(m_AppDataDir);

            m_ScriptDataDir = Path.Combine(m_AppDataDir, "Scripts");
            Directory.CreateDirectory(m_ScriptDataDir);
        }

        public static bool Initialized { get { return m_Initialized; } }

        public static void RegisterSerializable(ISerializable serializable)
        {
            bool ok = m_Serializables.Add(serializable);
            Debug.Assert(ok);
        }

        public static void UnregisterSerializable(ISerializable serializable)
        {
            bool ok = m_Serializables.Remove(serializable);
            Debug.Assert(ok);
        }

        public static void SetScriptParams(string scriptName, string scriptPath)
        {
            m_ScriptName = scriptName;
            m_ScriptPath = scriptPath;
        }

        private static string CalcScriptConfigFilePath(string configName)
        {
            string configFileName = string.Format(
                "{0}.{1}.config",
                ScriptName,
                configName);
            return Path.Combine(ScriptDataDir, configFileName);
        }

        /// <summary>
        /// On error: Show message box.
        /// </summary>
        public static void SaveScriptConfig(string configName)
        {
            string configFilePath = CalcScriptConfigFilePath(configName);

            try
            {
                ConfigNode rootNode = new ConfigNode();

                foreach (ISerializable s in m_Serializables)
                {
                    string sId = s.GetId();
                    if (!string.IsNullOrEmpty(sId))
                    {
                        ConfigNode sNode = rootNode.EnsureChild(sId);
                        s.SaveToConfig(sNode);
                    }
                }

                using (StreamWriter streamWriter = new StreamWriter(configFilePath, false, Encoding.UTF8))
                {
                    ConfigWriter configWriter = new ConfigWriter(streamWriter);
                    configWriter.Process(rootNode);
                }
            }
            catch (Exception ex)
            {
                ShowError(
                    string.Format("Cannot save script configuration to \"{0}\".", configFilePath),
                    ex);
            }
        }

        /// <summary>
        /// On error: Show message box.
        /// </summary>
        public static void LoadScriptConfig(string configName)
        {
            string configFilePath = CalcScriptConfigFilePath(configName);

            if (!File.Exists(configFilePath))
                return;

            try
            {
                ConfigNode rootNode = new ConfigNode();

                using (StreamReader streamReader = new StreamReader(configFilePath, Encoding.UTF8, true))
                {
                    ConfigReader configReader = new ConfigReader(streamReader);
                    configReader.Process(rootNode);
                }

                foreach (ISerializable s in m_Serializables)
                {
                    string sId = s.GetId();
                    if (!string.IsNullOrEmpty(sId))
                    {
                        ConfigNode sNode = rootNode.FindFirstChild(sId);
                        if (sNode != null)
                            s.LoadFromConfig(sNode);
                    }

                    
                }
            }
            catch (Exception ex)
            {
                ShowError(
                    string.Format("Cannot load script configuration from \"{0}\".", configFilePath),
                    ex);
            }
        }

        /////////////////////////////////////////////////////////////////////////////////
        // Interface for everyone, including scripts

        public const string APP_TITLE = "NoConsole 0.2";
        public const int GUI_MARGIN = 6;

        public const int IMG_SCRIPT = 0;
        public const int IMG_PREVIOUS = 1;
        public const int IMG_NEXT = 2;

        public static Form MainForm { get { return m_MainForm; } }
        public static Page ActivePage { get { return m_ActivePage; } }
        public static Button PrevButton { get { return m_PrevButton; } }
        public static Button NextButton { get { return m_NextButton; } }
        public static ToolTip ToolTip { get { return m_ToolTip; } }

        public static Font NormalFont { get { return m_NormalFont; } }
        public static Font BoldFont { get { return m_BoldFont; } }
        public static Font MonospacedFont { get { return m_MonospacedFont; } }
        public static ConfigNode GlobalConfig { get { return m_GlobalConfig; } }
        
        /// <summary>
        /// Working directory in the moment of application startup.
        /// </summary>
        public static string StartupDir { get { return m_StartupDir; } }
        /// <summary>
        /// Directory where program EXE file is located.
        /// </summary>
        public static string AppDir { get { return m_AppDir; } }
        /// <summary>
        /// Directory where program can store configuration files and other data.
        /// </summary>
        public static string AppDataDir { get { return m_AppDataDir; } }
        public static string ScriptDataDir { get { return m_ScriptDataDir; } }

        public static string ScriptName { get { return m_ScriptName; } }
        public static string ScriptPath { get { return m_ScriptPath; } }

        public static bool WaitCursor
        {
            get { return MainForm.Cursor == Cursors.WaitCursor; }
            set { MainForm.Cursor = value ? Cursors.WaitCursor : Cursors.Default; }
        }

        // Convert size in bytes to string.
        public static string SizeToStr(ulong Size)
        {
            if (Size < 1024)
                return Size.ToString() + " B";
            double s = Size / 1024.0;
            if (s < 1024.0)
                return s.ToString("F2") + " KB";
            s /= 1024.0;
            if (s < 1024.0)
                return s.ToString("F2") + " MB";
            s /= 1024.0;
            return s.ToString("F2") + " GB";
        }

        public static void ShowError(Exception Error)
        {
            MessageBox.Show(
                m_MainForm,
                Error.Message,
                APP_TITLE,
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }

        public static void ShowError(string ErrMsg)
        {
            MessageBox.Show(
                m_MainForm,
                ErrMsg,
                APP_TITLE,
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }

        public static void ShowError(string msgBegin, Exception Error)
        {
            MessageBox.Show(
                m_MainForm,
                msgBegin + "\r\n" + Error.Message,
                APP_TITLE,
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }

        /*
        // Copy directory with its content recursively
        public static void CopyDirectory(string SrcPath, string DestPath)
        {
            Directory.CreateDirectory(DestPath);

            string[] Dirs = Directory.GetDirectories(SrcPath);
            foreach (string Dir in Dirs)
            {
                string DirName = Path.GetFileName(Dir);
                Directory.CreateDirectory(DestPath + '\\' + DirName);
                CopyDirectory(SrcPath + '\\' + DirName, DestPath + '\\' + DirName);
            }

            string[] Files = Directory.GetFiles(SrcPath);
            foreach (string File in Files)
            {
                string FileName = Path.GetFileName(File);
                System.IO.File.Copy(SrcPath + '\\' + FileName, DestPath + '\\' + FileName); // Not overwriting.
            }
        }
        */
        
        // Remove trailing '\' or '/' from given path if there is one
        public static string ExcludeTrailingPathDelimiter(string Path)
        {
            if (string.IsNullOrEmpty(Path))
                return Path;

            int pathLen = Path.Length;
            char lastChar = Path[pathLen - 1];

            if (lastChar == '\\' || lastChar == '/')
                return Path.Substring(0, pathLen - 1);
            else
                return Path;
        }

        /*
        [System.Runtime.InteropServices.DllImport("User32.Dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        
        public const int EM_LINEINDEX = 0xBB;
        public const int EM_LINEFROMCHAR = 0xC9;
        */
        /*
        public static void shell_execute(IWin32Window error_parent, string path)
        {
            try
            {
                System.Diagnostics.Process p = new System.Diagnostics.Process();
                p.StartInfo.UseShellExecute = true;
                p.StartInfo.FileName = path;
                p.Start();
            }
            catch (Exception ex)
            {
                Lib.ShowError(ex);
            }
        }
        */

        /* Intelligent search for a file or directory by name, on the hard disk.
         * Can throw exceptions on some errors, while silently ignore other.
         * 
         * targetName - name of the target like "fxc.exe" or path suffix like @"bin\x86\fxc.exe".
         * interestingDirNames - case-insensitive strings with parts of
         *     directory names that make these directories particularly
         *     interesting, while not very common.
         *     Example: { "microsoft", "dx", "directx", "sdk" }
         * startDirs - paths to directories to start search from. For example,
         *     path to Program Files or hard disk root directories.
         *     
         * Returns path to found target or null if not found in given time.
         */
        public static string IntelligentSearch(string targetName, bool targetIsDirectory, string[] interestingDirNames, string[] startDirs, TimeSpan timeout)
        {
            DateTime endTime = DateTime.Now + timeout;

            // Paths to process
            LinkedList<string> PathQueue = new LinkedList<string>();
            // Path already processed in form Key=Path, Value="1"
            System.Collections.Specialized.StringDictionary Processed = new System.Collections.Specialized.StringDictionary();

            foreach (string startDir in startDirs)
                PathQueue.AddLast(startDir);

            // Processing loop - berform breadth first search (BFS) algorithm
            while (PathQueue.Count > 0)
            {
                // Get directory to process
                string Dir = PathQueue.First.Value;
                PathQueue.RemoveFirst();
                // Already processed
                if (Processed.ContainsKey(Dir))
                    continue;
                // Add to processed
                Processed.Add(Dir, "1");

                try
                {
                    string targetPath = Path.Combine(Dir, targetName);
                    if (targetIsDirectory)
                    {
                        if (Directory.Exists(targetPath))
                            return targetPath;
                    }
                    else
                    {
                        if (File.Exists(targetPath))
                            return targetPath;
                    }

                    foreach (string SubDir in Directory.GetDirectories(Dir))
                    {
                        bool dirIsInteresting = false;
                        if (interestingDirNames != null && interestingDirNames.Length > 0)
                        {
                            string subDirName = Path.GetFileName(SubDir);
                            foreach (string interestingName in interestingDirNames)
                            {
                                if (subDirName.IndexOf(interestingName, StringComparison.InvariantCultureIgnoreCase) != -1)
                                {
                                    dirIsInteresting = true;
                                    break;
                                }
                            }
                        }

                        if (dirIsInteresting)
                            PathQueue.AddFirst(SubDir);
                        else
                            PathQueue.AddLast(SubDir);
                    }
                }
                catch (Exception) { }

                if (DateTime.Now > endTime)
                    return null;
            }

            return null;
        }

        public static void ActivatePage(Page page)
        {
            if (page != m_ActivePage)
            {
                if (m_ActivePage != null)
                    m_ActivePage.Visible = false;
                
                m_ActivePage = page;

                if (m_ActivePage != null)
                {
                    m_ActivePage.Visible = true;
                    m_TitleLabel.Text = m_ActivePage.Title;
                }
                else
                    m_TitleLabel.Text = string.Empty;
            }
        }

        public static void SaveDefaultScriptConfig()
        {
            SaveScriptConfig(DEFAULT_CONFIG);
        }

        public static void LoadDefaultScriptConfig()
        {
            LoadScriptConfig(DEFAULT_CONFIG);
        }

        public static void DebugWriteLine(string s)
        {
            System.Diagnostics.Debug.WriteLine(s);
        }

        public static void ShowSettingsButton(bool visible)
        {
            m_SettingsButton.Visible = visible;
        }

        public static Image GetLoadingImage(int index)
        {
            switch (index)
            {
                case 0: return Properties.Resources.ajax_loader_1;
                case 1: return Properties.Resources.ajax_loader_2;
                case 2: return Properties.Resources.ajax_loader_3;
                case 3: return Properties.Resources.ajax_loader_4;
                case 4: return Properties.Resources.ajax_loader_5;
                case 5: return Properties.Resources.ajax_loader_6;
                case 6: return Properties.Resources.ajax_loader_7;
                case 7: return Properties.Resources.ajax_loader_8;
                default: Debug.Assert(false); return null;
            }
        }

        /////////////////////////////////////////////////////////////////////////////////
        // Private part

        private const string DEFAULT_CONFIG = "DEFAULT";

        private static bool m_Initialized = false;
        private static Form m_MainForm;
        private static Label m_TitleLabel;
        private static Button m_PrevButton, m_NextButton, m_SettingsButton;
        private static ToolTip m_ToolTip;
        private static Font m_NormalFont, m_BoldFont, m_MonospacedFont;
        private static string m_StartupDir;
        private static string m_AppDir;
        private static string m_AppDataDir;
        private static string m_ScriptDataDir;
        private static string m_ScriptName, m_ScriptPath;

        private static List<Page> m_Pages = new List<Page>();
        private static Page m_ActivePage;
        private static HashSet<ISerializable> m_Serializables = new HashSet<ISerializable>();
        private static ConfigNode m_GlobalConfig = new ConfigNode();
    }
}
