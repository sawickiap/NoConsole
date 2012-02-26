namespace NoConsole
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.m_ScriptPanel = new System.Windows.Forms.Panel();
            this.m_RefreshScriptListButton = new System.Windows.Forms.Button();
            this.m_ImageList16 = new System.Windows.Forms.ImageList(this.components);
            this.m_ScriptsListView = new System.Windows.Forms.ListView();
            this.m_TitleLabel = new System.Windows.Forms.Label();
            this.m_TestPanel = new System.Windows.Forms.Panel();
            this.m_BottomButtonNext = new System.Windows.Forms.Button();
            this.m_BottomButtonPrev = new System.Windows.Forms.Button();
            this.m_InitTimer = new System.Windows.Forms.Timer(this.components);
            this.m_LoadProfileButton = new System.Windows.Forms.Button();
            this.m_SaveProfileButton = new System.Windows.Forms.Button();
            this.m_ToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.m_SettingsButton = new System.Windows.Forms.Button();
            this.m_ScriptPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_ScriptPanel
            // 
            this.m_ScriptPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_ScriptPanel.Controls.Add(this.m_RefreshScriptListButton);
            this.m_ScriptPanel.Controls.Add(this.m_ScriptsListView);
            this.m_ScriptPanel.Location = new System.Drawing.Point(0, 31);
            this.m_ScriptPanel.Name = "m_ScriptPanel";
            this.m_ScriptPanel.Size = new System.Drawing.Size(784, 387);
            this.m_ScriptPanel.TabIndex = 12;
            // 
            // m_RefreshScriptListButton
            // 
            this.m_RefreshScriptListButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_RefreshScriptListButton.ImageIndex = 5;
            this.m_RefreshScriptListButton.ImageList = this.m_ImageList16;
            this.m_RefreshScriptListButton.Location = new System.Drawing.Point(746, 0);
            this.m_RefreshScriptListButton.Name = "m_RefreshScriptListButton";
            this.m_RefreshScriptListButton.Size = new System.Drawing.Size(26, 26);
            this.m_RefreshScriptListButton.TabIndex = 17;
            this.m_RefreshScriptListButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.m_ToolTip.SetToolTip(this.m_RefreshScriptListButton, "Refresh List");
            this.m_RefreshScriptListButton.UseVisualStyleBackColor = true;
            this.m_RefreshScriptListButton.Click += new System.EventHandler(this.m_RefreshScriptListButton_Click);
            // 
            // m_ImageList16
            // 
            this.m_ImageList16.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("m_ImageList16.ImageStream")));
            this.m_ImageList16.TransparentColor = System.Drawing.Color.Transparent;
            this.m_ImageList16.Images.SetKeyName(0, "script.ico");
            this.m_ImageList16.Images.SetKeyName(1, "resultset_previous.png");
            this.m_ImageList16.Images.SetKeyName(2, "resultset_next.png");
            this.m_ImageList16.Images.SetKeyName(3, "1327165823_folderopen1.png");
            this.m_ImageList16.Images.SetKeyName(4, "1327088842_save_16x16.gif");
            this.m_ImageList16.Images.SetKeyName(5, "arrow-circle-315-left.png");
            this.m_ImageList16.Images.SetKeyName(6, "1327227086_settings1_16x16.gif");
            // 
            // m_ScriptsListView
            // 
            this.m_ScriptsListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_ScriptsListView.HideSelection = false;
            this.m_ScriptsListView.Location = new System.Drawing.Point(12, 0);
            this.m_ScriptsListView.MultiSelect = false;
            this.m_ScriptsListView.Name = "m_ScriptsListView";
            this.m_ScriptsListView.Size = new System.Drawing.Size(728, 387);
            this.m_ScriptsListView.SmallImageList = this.m_ImageList16;
            this.m_ScriptsListView.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.m_ScriptsListView.TabIndex = 0;
            this.m_ScriptsListView.UseCompatibleStateImageBehavior = false;
            this.m_ScriptsListView.View = System.Windows.Forms.View.List;
            this.m_ScriptsListView.DoubleClick += new System.EventHandler(this.m_ScriptsListView_DoubleClick);
            this.m_ScriptsListView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_ScriptsListView_KeyDown);
            // 
            // m_TitleLabel
            // 
            this.m_TitleLabel.AutoSize = true;
            this.m_TitleLabel.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.m_TitleLabel.Location = new System.Drawing.Point(8, 9);
            this.m_TitleLabel.Name = "m_TitleLabel";
            this.m_TitleLabel.Size = new System.Drawing.Size(20, 19);
            this.m_TitleLabel.TabIndex = 14;
            this.m_TitleLabel.Text = "X";
            // 
            // m_TestPanel
            // 
            this.m_TestPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_TestPanel.Location = new System.Drawing.Point(0, 31);
            this.m_TestPanel.Name = "m_TestPanel";
            this.m_TestPanel.Size = new System.Drawing.Size(784, 387);
            this.m_TestPanel.TabIndex = 13;
            this.m_TestPanel.Visible = false;
            // 
            // m_BottomButtonNext
            // 
            this.m_BottomButtonNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_BottomButtonNext.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.m_BottomButtonNext.ImageIndex = 2;
            this.m_BottomButtonNext.ImageList = this.m_ImageList16;
            this.m_BottomButtonNext.Location = new System.Drawing.Point(692, 424);
            this.m_BottomButtonNext.Name = "m_BottomButtonNext";
            this.m_BottomButtonNext.Size = new System.Drawing.Size(80, 26);
            this.m_BottomButtonNext.TabIndex = 13;
            this.m_BottomButtonNext.Text = "Next";
            this.m_BottomButtonNext.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.m_BottomButtonNext.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.m_BottomButtonNext.UseVisualStyleBackColor = true;
            this.m_BottomButtonNext.Click += new System.EventHandler(this.m_BottomButtonNext_Click);
            // 
            // m_BottomButtonPrev
            // 
            this.m_BottomButtonPrev.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_BottomButtonPrev.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.m_BottomButtonPrev.ImageIndex = 1;
            this.m_BottomButtonPrev.ImageList = this.m_ImageList16;
            this.m_BottomButtonPrev.Location = new System.Drawing.Point(606, 424);
            this.m_BottomButtonPrev.Name = "m_BottomButtonPrev";
            this.m_BottomButtonPrev.Size = new System.Drawing.Size(80, 26);
            this.m_BottomButtonPrev.TabIndex = 15;
            this.m_BottomButtonPrev.Text = "Back";
            this.m_BottomButtonPrev.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.m_BottomButtonPrev.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.m_BottomButtonPrev.UseVisualStyleBackColor = true;
            this.m_BottomButtonPrev.Visible = false;
            this.m_BottomButtonPrev.Click += new System.EventHandler(this.m_BottomButtonPrev_Click);
            // 
            // m_InitTimer
            // 
            this.m_InitTimer.Enabled = true;
            this.m_InitTimer.Interval = 10;
            this.m_InitTimer.Tick += new System.EventHandler(this.m_InitTimer_Tick);
            // 
            // m_LoadProfileButton
            // 
            this.m_LoadProfileButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_LoadProfileButton.ImageIndex = 3;
            this.m_LoadProfileButton.ImageList = this.m_ImageList16;
            this.m_LoadProfileButton.Location = new System.Drawing.Point(12, 424);
            this.m_LoadProfileButton.Name = "m_LoadProfileButton";
            this.m_LoadProfileButton.Size = new System.Drawing.Size(26, 26);
            this.m_LoadProfileButton.TabIndex = 16;
            this.m_LoadProfileButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.m_ToolTip.SetToolTip(this.m_LoadProfileButton, "Load Profile...");
            this.m_LoadProfileButton.UseVisualStyleBackColor = true;
            this.m_LoadProfileButton.Visible = false;
            this.m_LoadProfileButton.Click += new System.EventHandler(this.m_LoadProfileButton_Click);
            // 
            // m_SaveProfileButton
            // 
            this.m_SaveProfileButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_SaveProfileButton.ImageIndex = 4;
            this.m_SaveProfileButton.ImageList = this.m_ImageList16;
            this.m_SaveProfileButton.Location = new System.Drawing.Point(44, 424);
            this.m_SaveProfileButton.Name = "m_SaveProfileButton";
            this.m_SaveProfileButton.Size = new System.Drawing.Size(26, 26);
            this.m_SaveProfileButton.TabIndex = 17;
            this.m_SaveProfileButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.m_ToolTip.SetToolTip(this.m_SaveProfileButton, "Save Profile...");
            this.m_SaveProfileButton.UseVisualStyleBackColor = true;
            this.m_SaveProfileButton.Visible = false;
            this.m_SaveProfileButton.Click += new System.EventHandler(this.m_SaveProfileButton_Click);
            // 
            // m_SettingsButton
            // 
            this.m_SettingsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_SettingsButton.ImageIndex = 6;
            this.m_SettingsButton.ImageList = this.m_ImageList16;
            this.m_SettingsButton.Location = new System.Drawing.Point(746, 2);
            this.m_SettingsButton.Name = "m_SettingsButton";
            this.m_SettingsButton.Size = new System.Drawing.Size(26, 26);
            this.m_SettingsButton.TabIndex = 18;
            this.m_SettingsButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.m_ToolTip.SetToolTip(this.m_SettingsButton, "Settings");
            this.m_SettingsButton.UseVisualStyleBackColor = true;
            this.m_SettingsButton.Visible = false;
            this.m_SettingsButton.Click += new System.EventHandler(this.m_SettingsButton_Click);
            // 
            // Form1
            // 
            this.AcceptButton = this.m_BottomButtonNext;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 462);
            this.Controls.Add(this.m_SettingsButton);
            this.Controls.Add(this.m_SaveProfileButton);
            this.Controls.Add(this.m_LoadProfileButton);
            this.Controls.Add(this.m_BottomButtonPrev);
            this.Controls.Add(this.m_TitleLabel);
            this.Controls.Add(this.m_BottomButtonNext);
            this.Controls.Add(this.m_ScriptPanel);
            this.Controls.Add(this.m_TestPanel);
            this.Name = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.m_ScriptPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel m_ScriptPanel;
        private System.Windows.Forms.ListView m_ScriptsListView;
        private System.Windows.Forms.ImageList m_ImageList16;
        private System.Windows.Forms.Button m_BottomButtonNext;
        private System.Windows.Forms.Label m_TitleLabel;
        private System.Windows.Forms.Panel m_TestPanel;
        private System.Windows.Forms.Button m_BottomButtonPrev;
        private System.Windows.Forms.Timer m_InitTimer;
        private System.Windows.Forms.Button m_LoadProfileButton;
        private System.Windows.Forms.Button m_SaveProfileButton;
        private System.Windows.Forms.ToolTip m_ToolTip;
        private System.Windows.Forms.Button m_RefreshScriptListButton;
        private System.Windows.Forms.Button m_SettingsButton;
    }
}

