using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using NoConsoleLib;

class Script
{
	public Script()
	{
		CreateParamsPage();
		CreateOutputPage();
		
        Lib.LoadDefaultScriptConfig();
		
        ShowParamsPage();
	}

    /// <summary>
    /// 
    /// </summary>
    /// <returns>true to continue closing, false to cancel closing.</returns>
    public bool OnExit()
	{
        if (m_Process != null)
        {
            MessageBox.Show(
                Lib.MainForm,
                "Program is still working. Wait for it to finish or use Kill button before closing application.",
                Lib.APP_TITLE,
                MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation);
            return false;
        }
        else
        {
            Lib.SaveDefaultScriptConfig();
            return true;
        }
    }
	
	public void OnNextClick()
	{
		if( Lib.ActivePage == m_ParamsPage )
			Run();
	}
	
	public void OnPrevClick()
	{
        if (Lib.ActivePage == m_OutputPage)
        {
            Debug.Assert(m_Process == null);
            ShowParamsPage();
        }
	}
	
	private void CreateParamsPage()
	{
		m_ParamsPage = new Page("Parameters");
		m_ParamsPage.SuspendLayout();
		
		m_ExeFileProp = new FileProperty {
			Id = "ExecutableFile",
			Title = "Executable file" };
		m_ParamsPage.AddControl( m_ExeFileProp );
        m_ExeFileProp.MyOpenFileDialog.Filter = "Executable files|*.exe|All files|*";

		m_WorkingDirProp = new DirectoryProperty {
			Id = "WorkingDirectory",
			Title = "Working directory" };
		m_ParamsPage.AddControl( m_WorkingDirProp );

		m_ArgsMemoProp = new MemoProperty {
			Id = "Arguments",
			Title = "Arguments" };
		m_ParamsPage.AddControl( m_ArgsMemoProp );
		m_ArgsMemoProp.MyTextBox.Font = Lib.MonospacedFont;
		
		m_ParamsPage.ResumeLayout();
	}
	
	private void CreateOutputPage()
	{
		m_OutputPage = new Page("Output");
		m_OutputPage.SuspendLayout();
				
		m_OutputControl = new OutputControl() {
			Id = "Output" };
		m_OutputPage.AddControl( m_OutputControl, true );
		
		
		m_PictureBox = new PictureBox();
		((System.ComponentModel.ISupportInitialize)(m_PictureBox)).BeginInit();
		m_PictureBox.Location = new System.Drawing.Point(0, 0);
		m_PictureBox.Margin = new System.Windows.Forms.Padding(0, 6, 0, 0);
		m_PictureBox.Size = new System.Drawing.Size(32, 32);
		m_PictureBox.TabStop = false;
		//m_PictureBox.Image = NoConsoleLib.Properties.Resources.circle_green;
		((System.ComponentModel.ISupportInitialize)(m_PictureBox)).EndInit();
		
		m_InfoTextBox = new InfoTextbox();
		m_InfoTextBox.SetHeightRows( 2 );

        m_KillButton = new Button();
        m_KillButton.Image = NoConsoleLib.Properties.Resources.Skull;
        m_KillButton.Location = new System.Drawing.Point(0, 0);
        m_KillButton.Size = new System.Drawing.Size(26, 26);
        Lib.ToolTip.SetToolTip(m_KillButton, "Kill");
        m_KillButton.UseVisualStyleBackColor = true;
        m_KillButton.Click += new System.EventHandler(this.OnKillClick);
        
        TableLayoutPanel tableLayoutPanel1 = new TableLayoutPanel();
		tableLayoutPanel1.SuspendLayout();
		tableLayoutPanel1.ColumnCount = 3;
		tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 32F + 12f));
		tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
        tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 32F));
        tableLayoutPanel1.RowCount = 1;
		tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
		tableLayoutPanel1.Controls.Add(m_PictureBox,  0, 0);
		tableLayoutPanel1.Controls.Add(m_InfoTextBox, 1, 0);
        tableLayoutPanel1.Controls.Add(m_KillButton, 2, 0);
        tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
		tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
		tableLayoutPanel1.Size = new System.Drawing.Size(300, 32 + 6);
		m_OutputPage.AddControl(tableLayoutPanel1);
		tableLayoutPanel1.ResumeLayout(false);
		
		m_OutputPage.ResumeLayout();
	}
	
	private void ShowParamsPage()
	{
		m_ParamsPage.Activate();

		m_InfoTextBox.Value = string.Empty;
		m_PictureBox.Image = null;
		m_OutputControl.MyRichTextBox.Clear();

		Lib.NextButton.Text = "Run";
		Lib.NextButton.Visible = true;
		Lib.PrevButton.Visible = false;
	}
	
	private void ShowOutputPage()
	{
		m_OutputPage.Activate();
		m_OutputControl.MyRichTextBox.Focus();

		Lib.NextButton.Visible = false;
		Lib.PrevButton.Visible = true;
	}
	
	private void Run()
	{
		try
		{
			string exeFile = m_ExeFileProp.Value;
            if (string.IsNullOrEmpty(exeFile))
            {
                Lib.ShowError("Please select executable file.");
                m_ExeFileProp.MyComboBox.Focus();
                return;
            }
			
			string workingDir = m_WorkingDirProp.Value;
			if( string.IsNullOrEmpty( workingDir ) )
				workingDir = Path.GetDirectoryName( exeFile );
			
			string args = m_ArgsMemoProp.Value;
			if( !string.IsNullOrEmpty( args ) )
			{
				args = args.Replace( '\r', ' ' );
				args = args.Replace( '\n', ' ' );
			}
			
			m_ExeFileProp.PostValueToMRU();
			m_WorkingDirProp.PostValueToMRU();
			
			Lib.WaitCursor = true;
			
            m_ProcessOutput = new ProcessOutput();
            m_LoadingImageIndex = 0;
            m_PictureBox.Image = Lib.GetLoadingImage(m_LoadingImageIndex);
            
            m_Process = new Process();

			m_Process.StartInfo.FileName = exeFile;
			if (args != null)
				m_Process.StartInfo.Arguments = args;
			if (workingDir != null)
			    m_Process.StartInfo.WorkingDirectory = workingDir;
			m_Process.StartInfo.RedirectStandardInput = true;
			m_Process.StartInfo.RedirectStandardOutput = true;
			m_Process.StartInfo.RedirectStandardError = true;
			m_Process.StartInfo.UseShellExecute = false;
			m_Process.StartInfo.CreateNoWindow = true;
			m_Process.EnableRaisingEvents = true;
            m_Process.OutputDataReceived += new DataReceivedEventHandler(m_ProcessOutput.OnOutputDataReceived);
            m_Process.ErrorDataReceived += new DataReceivedEventHandler(m_ProcessOutput.OnErrorDataReceived);
            m_Process.Exited += new EventHandler(m_ProcessOutput.OnProcessExited);
            m_Process.Start();
			m_Process.BeginOutputReadLine();
			m_Process.BeginErrorReadLine();

            m_UpdateTimer = new Timer();
            m_UpdateTimer.Tick += new EventHandler(OnUpdateTimerTick);
            m_UpdateTimer.Interval = 200;
            m_UpdateTimer.Start();

            ShowOutputPage();
            m_KillButton.Visible = true;
            Lib.PrevButton.Visible = false;
        }
		catch( Exception ex )
		{
            m_Process.Close();
            m_Process = null;
            m_ProcessOutput = null;

            Lib.ShowError("Cannot start process.", ex);
        }
		finally
		{
			Lib.WaitCursor = false;
		}
	}

    private void OnUpdateTimerTick(object sender, EventArgs e)
    {
        if (m_Process == null) return;

        if (m_ProcessOutput.AppendOutputToRichTextBox(m_OutputControl.MyRichTextBox))
        {
            m_LoadingImageIndex = (m_LoadingImageIndex + 1) % 8;
            m_PictureBox.Image = Lib.GetLoadingImage(m_LoadingImageIndex);
        }

        if (m_ProcessOutput.Exited)
        {
            int exitCode = m_Process.ExitCode;

            m_InfoTextBox.Value = string.Format(
                "Result: {0} (0x{1:X8})\r\n" +
                "Duration: {2}",
                exitCode,
                (uint)exitCode,
                m_Process.ExitTime - m_Process.StartTime);

            m_PictureBox.Image = exitCode == 0 ?
                NoConsoleLib.Properties.Resources.circle_green :
                NoConsoleLib.Properties.Resources.circle_red;

            m_KillButton.Visible = false;
            Lib.PrevButton.Visible = true;

            m_Process.Close();
            m_Process = null;
            m_ProcessOutput = null;
            m_UpdateTimer.Stop();
            m_UpdateTimer = null;
        }
        // Process is still working.
        else
        {
            m_InfoTextBox.Value = string.Format(
                "Duration: {0}",
                DateTime.Now - m_Process.StartTime);
        }
    }

    private void OnKillClick(object sender, EventArgs e)
    {
        if (m_Process == null || m_Process.HasExited)
            return;

        try
        {
            m_Process.Kill();
        }
        catch (Exception ex)
        {
            Lib.ShowError("Cannot kill process.", ex);
        }
    }
	
	private Page m_ParamsPage;
	private FileProperty m_ExeFileProp;
	private DirectoryProperty m_WorkingDirProp;
	private MemoProperty m_ArgsMemoProp;
	
	private Page m_OutputPage;
	private InfoTextbox m_InfoTextBox;
	private PictureBox m_PictureBox;
	private OutputControl m_OutputControl;
    private Button m_KillButton;

    private Process m_Process;
    private Timer m_UpdateTimer;
    private int m_LoadingImageIndex = 0;
    private ProcessOutput m_ProcessOutput;
};
