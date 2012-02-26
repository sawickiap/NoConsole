namespace NoConsoleLib
{
    partial class TextDialog
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
            this.m_OkButton = new System.Windows.Forms.Button();
            this.m_OutputControl = new NoConsoleLib.OutputControl();
            this.m_PictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.m_PictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // m_OkButton
            // 
            this.m_OkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_OkButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.m_OkButton.Location = new System.Drawing.Point(537, 477);
            this.m_OkButton.Name = "m_OkButton";
            this.m_OkButton.Size = new System.Drawing.Size(75, 23);
            this.m_OkButton.TabIndex = 0;
            this.m_OkButton.Text = "OK";
            this.m_OkButton.UseVisualStyleBackColor = true;
            // 
            // m_OutputControl
            // 
            this.m_OutputControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_OutputControl.Id = null;
            this.m_OutputControl.Location = new System.Drawing.Point(51, 9);
            this.m_OutputControl.Margin = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.m_OutputControl.Name = "m_OutputControl";
            this.m_OutputControl.Size = new System.Drawing.Size(564, 465);
            this.m_OutputControl.TabIndex = 1;
            // 
            // m_PictureBox
            // 
            this.m_PictureBox.Location = new System.Drawing.Point(12, 9);
            this.m_PictureBox.Name = "m_PictureBox";
            this.m_PictureBox.Size = new System.Drawing.Size(32, 32);
            this.m_PictureBox.TabIndex = 2;
            this.m_PictureBox.TabStop = false;
            // 
            // TextDialog
            // 
            this.AcceptButton = this.m_OkButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 512);
            this.Controls.Add(this.m_PictureBox);
            this.Controls.Add(this.m_OkButton);
            this.Controls.Add(this.m_OutputControl);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TextDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.m_PictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private OutputControl m_OutputControl;
        private System.Windows.Forms.Button m_OkButton;
        private System.Windows.Forms.PictureBox m_PictureBox;
    }
}