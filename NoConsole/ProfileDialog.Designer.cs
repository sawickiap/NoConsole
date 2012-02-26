namespace NoConsole
{
    partial class ProfileDialog
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
            this.label1 = new System.Windows.Forms.Label();
            this.m_NameTextBox = new System.Windows.Forms.TextBox();
            this.m_DeleteButton = new System.Windows.Forms.Button();
            this.m_ListBox = new System.Windows.Forms.ListBox();
            this.m_OkButton = new System.Windows.Forms.Button();
            this.m_CancelButton = new System.Windows.Forms.Button();
            this.m_ToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.m_InitTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select &Profile";
            // 
            // m_NameTextBox
            // 
            this.m_NameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_NameTextBox.Location = new System.Drawing.Point(12, 25);
            this.m_NameTextBox.Name = "m_NameTextBox";
            this.m_NameTextBox.Size = new System.Drawing.Size(238, 20);
            this.m_NameTextBox.TabIndex = 1;
            // 
            // m_DeleteButton
            // 
            this.m_DeleteButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_DeleteButton.Image = global::NoConsole.Properties.Resources.delete;
            this.m_DeleteButton.Location = new System.Drawing.Point(256, 21);
            this.m_DeleteButton.Name = "m_DeleteButton";
            this.m_DeleteButton.Size = new System.Drawing.Size(26, 26);
            this.m_DeleteButton.TabIndex = 2;
            this.m_ToolTip.SetToolTip(this.m_DeleteButton, "Delete Profile...");
            this.m_DeleteButton.UseVisualStyleBackColor = true;
            this.m_DeleteButton.Click += new System.EventHandler(this.m_DeleteButton_Click);
            // 
            // m_ListBox
            // 
            this.m_ListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_ListBox.FormattingEnabled = true;
            this.m_ListBox.Location = new System.Drawing.Point(12, 53);
            this.m_ListBox.Name = "m_ListBox";
            this.m_ListBox.Size = new System.Drawing.Size(270, 199);
            this.m_ListBox.Sorted = true;
            this.m_ListBox.TabIndex = 3;
            this.m_ListBox.SelectedValueChanged += new System.EventHandler(this.m_ListBox_SelectedValueChanged);
            this.m_ListBox.DoubleClick += new System.EventHandler(this.m_ListBox_DoubleClick);
            this.m_ListBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_ListBox_KeyDown);
            // 
            // m_OkButton
            // 
            this.m_OkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_OkButton.Location = new System.Drawing.Point(126, 262);
            this.m_OkButton.Name = "m_OkButton";
            this.m_OkButton.Size = new System.Drawing.Size(75, 23);
            this.m_OkButton.TabIndex = 4;
            this.m_OkButton.Text = "OK";
            this.m_OkButton.UseVisualStyleBackColor = true;
            this.m_OkButton.Click += new System.EventHandler(this.m_OkButton_Click);
            // 
            // m_CancelButton
            // 
            this.m_CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_CancelButton.Location = new System.Drawing.Point(207, 262);
            this.m_CancelButton.Name = "m_CancelButton";
            this.m_CancelButton.Size = new System.Drawing.Size(75, 23);
            this.m_CancelButton.TabIndex = 5;
            this.m_CancelButton.Text = "Cancel";
            this.m_CancelButton.UseVisualStyleBackColor = true;
            // 
            // m_InitTimer
            // 
            this.m_InitTimer.Enabled = true;
            this.m_InitTimer.Interval = 10;
            this.m_InitTimer.Tick += new System.EventHandler(this.m_InitTimer_Tick);
            // 
            // ProfileDialog
            // 
            this.AcceptButton = this.m_OkButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.m_CancelButton;
            this.ClientSize = new System.Drawing.Size(294, 297);
            this.Controls.Add(this.m_CancelButton);
            this.Controls.Add(this.m_OkButton);
            this.Controls.Add(this.m_ListBox);
            this.Controls.Add(this.m_DeleteButton);
            this.Controls.Add(this.m_NameTextBox);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProfileDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox m_NameTextBox;
        private System.Windows.Forms.Button m_DeleteButton;
        private System.Windows.Forms.ListBox m_ListBox;
        private System.Windows.Forms.Button m_OkButton;
        private System.Windows.Forms.Button m_CancelButton;
        private System.Windows.Forms.ToolTip m_ToolTip;
        private System.Windows.Forms.Timer m_InitTimer;
    }
}