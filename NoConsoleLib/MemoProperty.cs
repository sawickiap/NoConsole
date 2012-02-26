using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace NoConsoleLib
{
    public class MemoProperty : Property
    {
        private TextBox m_TextBox;

        public TextBox MyTextBox { get { return m_TextBox; } }

        public string Value { get { return m_TextBox.Text; } set { m_TextBox.Text = value; } }
        public bool WordWrap { get { return m_TextBox.WordWrap; } set { m_TextBox.WordWrap = value; } }

        public MemoProperty()
        {
            InitializeComponent();

            UseCheckBox = false;
        }
    
        private void InitializeComponent()
        {
            this.m_TextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // m_TextBox
            // 
            this.m_TextBox.AcceptsReturn = true;
            this.m_TextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_TextBox.Location = new System.Drawing.Point(210, 0);
            this.m_TextBox.Multiline = true;
            this.m_TextBox.Name = "m_TextBox";
            this.m_TextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.m_TextBox.Size = new System.Drawing.Size(233, 130);
            this.m_TextBox.TabIndex = 2;
            this.m_TextBox.WordWrap = false;
            // 
            // MemoProperty
            // 
            this.Controls.Add(this.m_TextBox);
            this.Name = "MemoProperty";
            this.Size = new System.Drawing.Size(443, 130);
            this.Controls.SetChildIndex(this.m_TextBox, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        public override void SaveToConfig(ConfigNode node)
        {
            node.SetValue(CONFIG_VALUE, Value);

            base.SaveToConfig(node);
        }

        public override void LoadFromConfig(ConfigNode node)
        {
            base.LoadFromConfig(node);

            string s = null;
            if (node.GetValue(CONFIG_VALUE, ref s))
                Value = s;
        }
    }
}
