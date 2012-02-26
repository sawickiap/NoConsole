using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;

namespace NoConsoleLib
{
    public class OutputControl : UserControl, ISerializable
    {
        private CheckBox m_WordWrapCheckBox;
        private Button m_FindButton;
        private Button m_CopyButton;
        private Button m_SaveButton;
        private ToolTip m_ToolTip;
        private System.ComponentModel.IContainer components;
        private SaveFileDialog m_SaveFileDialog;
        private RichTextBox m_RichTextBox;

        public RichTextBox MyRichTextBox
        {
            get { return m_RichTextBox; }
        }

        public string Id { get; set; }

        public OutputControl()
        {
            InitializeComponent();

            m_RichTextBox.Font = Lib.MonospacedFont;
            Lib.RegisterSerializable(this);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.m_RichTextBox = new System.Windows.Forms.RichTextBox();
            this.m_FindButton = new System.Windows.Forms.Button();
            this.m_WordWrapCheckBox = new System.Windows.Forms.CheckBox();
            this.m_CopyButton = new System.Windows.Forms.Button();
            this.m_SaveButton = new System.Windows.Forms.Button();
            this.m_ToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.m_SaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.SuspendLayout();
            // 
            // m_RichTextBox
            // 
            this.m_RichTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_RichTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.m_RichTextBox.DetectUrls = false;
            this.m_RichTextBox.Font = new System.Drawing.Font("Consolas", 9F);
            this.m_RichTextBox.HideSelection = false;
            this.m_RichTextBox.Location = new System.Drawing.Point(0, 0);
            this.m_RichTextBox.Margin = new System.Windows.Forms.Padding(0);
            this.m_RichTextBox.Name = "m_RichTextBox";
            this.m_RichTextBox.ReadOnly = true;
            this.m_RichTextBox.Size = new System.Drawing.Size(288, 240);
            this.m_RichTextBox.TabIndex = 1;
            this.m_RichTextBox.Text = "";
            this.m_RichTextBox.WordWrap = false;
            // 
            // m_FindButton
            // 
            this.m_FindButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_FindButton.Image = global::NoConsoleLib.Properties.Resources._1327089037_find;
            this.m_FindButton.Location = new System.Drawing.Point(294, 146);
            this.m_FindButton.Name = "m_FindButton";
            this.m_FindButton.Size = new System.Drawing.Size(26, 26);
            this.m_FindButton.TabIndex = 3;
            this.m_ToolTip.SetToolTip(this.m_FindButton, "Find...");
            this.m_FindButton.UseVisualStyleBackColor = true;
            this.m_FindButton.Visible = false;
            this.m_FindButton.Click += new System.EventHandler(this.m_FindButton_Click);
            // 
            // m_WordWrapCheckBox
            // 
            this.m_WordWrapCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_WordWrapCheckBox.Appearance = System.Windows.Forms.Appearance.Button;
            this.m_WordWrapCheckBox.Image = global::NoConsoleLib.Properties.Resources.WordWrap;
            this.m_WordWrapCheckBox.Location = new System.Drawing.Point(294, 0);
            this.m_WordWrapCheckBox.Name = "m_WordWrapCheckBox";
            this.m_WordWrapCheckBox.Size = new System.Drawing.Size(26, 26);
            this.m_WordWrapCheckBox.TabIndex = 2;
            this.m_WordWrapCheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.m_ToolTip.SetToolTip(this.m_WordWrapCheckBox, "Word Wrap");
            this.m_WordWrapCheckBox.UseVisualStyleBackColor = true;
            this.m_WordWrapCheckBox.CheckedChanged += new System.EventHandler(this.m_WordWrapCheckBox_CheckedChanged);
            // 
            // m_CopyButton
            // 
            this.m_CopyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_CopyButton.Image = global::NoConsoleLib.Properties.Resources._1327088813_page_2_copy;
            this.m_CopyButton.Location = new System.Drawing.Point(294, 32);
            this.m_CopyButton.Name = "m_CopyButton";
            this.m_CopyButton.Size = new System.Drawing.Size(26, 26);
            this.m_CopyButton.TabIndex = 4;
            this.m_ToolTip.SetToolTip(this.m_CopyButton, "Copy All to Clipboard");
            this.m_CopyButton.UseVisualStyleBackColor = true;
            this.m_CopyButton.Click += new System.EventHandler(this.m_CopyButton_Click);
            // 
            // m_SaveButton
            // 
            this.m_SaveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_SaveButton.Image = global::NoConsoleLib.Properties.Resources._1327088842_save_16x16;
            this.m_SaveButton.Location = new System.Drawing.Point(294, 58);
            this.m_SaveButton.Name = "m_SaveButton";
            this.m_SaveButton.Size = new System.Drawing.Size(26, 26);
            this.m_SaveButton.TabIndex = 5;
            this.m_ToolTip.SetToolTip(this.m_SaveButton, "Save As...");
            this.m_SaveButton.UseVisualStyleBackColor = true;
            this.m_SaveButton.Click += new System.EventHandler(this.m_SaveButton_Click);
            // 
            // m_SaveFileDialog
            // 
            this.m_SaveFileDialog.DefaultExt = "txt";
            this.m_SaveFileDialog.Filter = "ASCII text file|*.txt|Unicode text file|*.txt|Rich text file|*.rtf|All files|*";
            this.m_SaveFileDialog.Title = "Save Output";
            // 
            // OutputControl
            // 
            this.Controls.Add(this.m_SaveButton);
            this.Controls.Add(this.m_CopyButton);
            this.Controls.Add(this.m_FindButton);
            this.Controls.Add(this.m_WordWrapCheckBox);
            this.Controls.Add(this.m_RichTextBox);
            this.Margin = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.Name = "OutputControl";
            this.Size = new System.Drawing.Size(320, 240);
            this.ResumeLayout(false);

        }

        /*
        /// <summary>
        /// Method to be bound to Process.OutputDataReceived event.
        /// Will be called on separate thread.
        /// </summary>
        public void OnOutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (e.Data == null)
                return;

            Delegate del = new Action(() =>
            {
                MyRichTextBox.SelectionColor = Color.Black;
                MyRichTextBox.AppendText(e.Data);
                MyRichTextBox.AppendText("\r\n");
            });
            MyRichTextBox.Invoke(del);
        }

        /// <summary>
        /// Method to be bound to Process.ErrorDataReceived event.
        /// Will be called on separate thread.
        /// </summary>
        public void OnErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (e.Data == null)
                return;

            Delegate del = new Action(() =>
            {
                MyRichTextBox.SelectionColor = Color.Maroon;
                MyRichTextBox.AppendText(e.Data);
                MyRichTextBox.AppendText("\r\n");
            });
            MyRichTextBox.Invoke(del);
        }
        */

        private static string CONFIG_WORD_WRAP = "WordWrap";

        string ISerializable.GetId() { return Id; }

        void ISerializable.SaveToConfig(ConfigNode node)
        {
            node.SetBoolValue(CONFIG_WORD_WRAP, m_WordWrapCheckBox.Checked);
        }

        void ISerializable.LoadFromConfig(ConfigNode node)
        {
            bool b = false;
            if (node.GetBoolValue(CONFIG_WORD_WRAP, ref b))
                m_WordWrapCheckBox.Checked = b;
        }

        private void m_WordWrapCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            m_RichTextBox.WordWrap = m_WordWrapCheckBox.Checked;
        }

        private void m_FindButton_Click(object sender, EventArgs e)
        {

        }

        private void m_CopyButton_Click(object sender, EventArgs e)
        {
            m_RichTextBox.SelectAll();
            m_RichTextBox.Copy();
        }

        private void m_SaveButton_Click(object sender, EventArgs e)
        {
            if (m_SaveFileDialog.ShowDialog(Lib.MainForm) == DialogResult.OK)
            {
                try
                {
                    Lib.WaitCursor = true;

                    RichTextBoxStreamType type;
                    switch (m_SaveFileDialog.FilterIndex)
                    {
                        case 1: type = RichTextBoxStreamType.PlainText; break;
                        case 2: type = RichTextBoxStreamType.UnicodePlainText; break;
                        case 3: type = RichTextBoxStreamType.RichText; break;
                        default: throw new Exception("You didn't select file format.");
                    }

                    m_RichTextBox.SaveFile(m_SaveFileDialog.FileName, type);
                }
                catch (Exception ex)
                {
                    Lib.ShowError(
                        string.Format("Cannot save output to \"{0}\".", m_SaveFileDialog.FileName),
                        ex);
                }
                finally
                {
                    Lib.WaitCursor = false;
                }
            }
        }
    }
}
