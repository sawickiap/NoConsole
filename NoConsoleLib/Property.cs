using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace NoConsoleLib
{
    public class Property : UserControl, ISerializable
    {
        private CheckBox m_CheckBox;
        private Label m_Label;
        private string m_Title;
    
        public CheckBox MyCheckBox { get { return m_CheckBox; } }
        public Label MyLabel { get { return m_Label; } }

        public string Id { get; set; }
        public bool Checked { get { return MyCheckBox.Checked; } set { MyCheckBox.Checked = value; } }
        
        public string Title
        {
            get
            {
                return m_Title;
            }
            set
            {
                m_Title = value;
                m_Label.Text = value;
                m_CheckBox.Text = value;
            }
        }

        public bool UseCheckBox
        {
            get
            {
                return m_UseCheckBox;
            }
            set
            {
                m_UseCheckBox = value;
                m_CheckBox.Visible = value;
                m_Label.Visible = !value;
            }
        }

        public Property()
        {
            InitializeComponent();

            Lib.RegisterSerializable(this);
        }
        
        private void InitializeComponent()
        {
            this.m_CheckBox = new System.Windows.Forms.CheckBox();
            this.m_Label = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // m_CheckBox
            // 
            this.m_CheckBox.AutoSize = true;
            this.m_CheckBox.Checked = true;
            this.m_CheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.m_CheckBox.Location = new System.Drawing.Point(0, 0);
            this.m_CheckBox.Name = "m_CheckBox";
            this.m_CheckBox.Size = new System.Drawing.Size(31, 17);
            this.m_CheckBox.TabIndex = 0;
            this.m_CheckBox.Text = "x";
            this.m_CheckBox.UseVisualStyleBackColor = true;
            this.m_CheckBox.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            // 
            // m_Label
            // 
            this.m_Label.AutoSize = true;
            this.m_Label.Location = new System.Drawing.Point(16, 1);
            this.m_Label.Name = "m_Label";
            this.m_Label.Size = new System.Drawing.Size(12, 13);
            this.m_Label.TabIndex = 1;
            this.m_Label.Text = "x";
            this.m_Label.Visible = false;
            // 
            // Property
            // 
            this.Controls.Add(this.m_Label);
            this.Controls.Add(this.m_CheckBox);
            this.Margin = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.Name = "Property";
            this.Size = new System.Drawing.Size(128, 15);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckboxCheckedChanged();
        }

        protected virtual void CheckboxCheckedChanged() { }

        public virtual void SaveToConfig(ConfigNode node)
        {
            if (m_UseCheckBox)
                node.SetBoolValue(CONFIG_CHECKED, Checked);
        }

        public virtual void LoadFromConfig(ConfigNode node)
        {
            if (m_UseCheckBox)
            {
                bool b = false;
                if (node.GetBoolValue(CONFIG_CHECKED, ref b))
                    Checked = b;
            }
        }

        public string GetId() { return Id; }

        protected const string CONFIG_VALUE = "Value";
        private const string CONFIG_CHECKED = "Checked";

        private bool m_UseCheckBox = true;
    }
}
