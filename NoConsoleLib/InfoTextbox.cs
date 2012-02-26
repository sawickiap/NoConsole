using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace NoConsoleLib
{
    public class InfoTextbox : UserControl
    {
        private TextBox m_TextBox;

        public TextBox MyTextBox
        {
            get { return m_TextBox; }
        }

        public string Value
        {
            get { return MyTextBox.Text; }
            set { MyTextBox.Text = value; }
        }

        public InfoTextbox()
        {
            InitializeComponent();
        }

        public void SetHeightRows(int rows)
        {
            this.Height = rows * 13;
        }
    
        private void InitializeComponent()
        {
            this.m_TextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // m_TextBox
            // 
            this.m_TextBox.BackColor = System.Drawing.SystemColors.Control;
            this.m_TextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.m_TextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_TextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.m_TextBox.ForeColor = System.Drawing.SystemColors.ControlText;
            this.m_TextBox.Location = new System.Drawing.Point(0, 0);
            this.m_TextBox.Multiline = true;
            this.m_TextBox.Name = "m_TextBox";
            this.m_TextBox.ReadOnly = true;
            this.m_TextBox.Size = new System.Drawing.Size(300, 13);
            this.m_TextBox.TabIndex = 1;
            this.m_TextBox.Text = "Text text text\r\nRow 2";
            // 
            // InfoTextbox
            // 
            this.Controls.Add(this.m_TextBox);
            this.Margin = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.Name = "InfoTextbox";
            this.Size = new System.Drawing.Size(300, 13);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
