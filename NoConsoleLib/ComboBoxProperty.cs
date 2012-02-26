using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace NoConsoleLib
{
    /// <summary>
    /// Do not use this class directly. Please use inherited classes.
    /// </summary>
    public class ComboBoxProperty : Property
    {
        private Button m_BrowseButton;
        private ComboBox m_ComboBox;

        public ComboBox MyComboBox { get { return m_ComboBox; } }
        public Button MyBrowseButton { get { return m_BrowseButton; } }

        public string Value { get { return MyComboBox.Text; } set { MyComboBox.Text = value; } }
        public int MaxLength { get { return MyComboBox.MaxLength; } set { MyComboBox.MaxLength = value; } }

        public ComboBoxProperty()
        {
            InitializeComponent();

            UseCheckBox = false;
        }

        public void SetFixedWidth(int width)
        {
            this.m_ComboBox.Anchor = (System.Windows.Forms.AnchorStyles)(
                System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left);
            this.m_ComboBox.Width = width;
        }

        protected override void CheckboxCheckedChanged()
        {
            base.CheckboxCheckedChanged();

            bool enabled = MyCheckBox.Checked;
            
            MyComboBox.Enabled = enabled;
            MyBrowseButton.Enabled = enabled;
        }

        private void InitializeComponent()
        {
            this.m_ComboBox = new System.Windows.Forms.ComboBox();
            this.m_BrowseButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // m_ComboBox
            // 
            this.m_ComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_ComboBox.FormattingEnabled = true;
            this.m_ComboBox.Location = new System.Drawing.Point(210, 2);
            this.m_ComboBox.Name = "m_ComboBox";
            this.m_ComboBox.Size = new System.Drawing.Size(189, 21);
            this.m_ComboBox.TabIndex = 2;
            // 
            // m_BrowseButton
            // 
            this.m_BrowseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_BrowseButton.Location = new System.Drawing.Point(405, 0);
            this.m_BrowseButton.Name = "m_BrowseButton";
            this.m_BrowseButton.Size = new System.Drawing.Size(36, 23);
            this.m_BrowseButton.TabIndex = 3;
            this.m_BrowseButton.Text = "...";
            this.m_BrowseButton.UseVisualStyleBackColor = true;
            this.m_BrowseButton.Visible = false;
            // 
            // ComboBoxProperty
            // 
            this.Controls.Add(this.m_BrowseButton);
            this.Controls.Add(this.m_ComboBox);
            this.Name = "ComboBoxProperty";
            this.Size = new System.Drawing.Size(441, 23);
            this.Controls.SetChildIndex(this.m_ComboBox, 0);
            this.Controls.SetChildIndex(this.m_BrowseButton, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
