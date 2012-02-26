using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace NoConsoleLib
{
    public class FileProperty : StringProperty
    {
        private OpenFileDialog m_OpenFileDialog;
        private SaveFileDialog m_SaveFileDialog;

        public OpenFileDialog MyOpenFileDialog { get { return m_OpenFileDialog; } }
        public SaveFileDialog MySaveFileDialog { get { return m_SaveFileDialog; } }

        public FileProperty()
        {
            MyComboBox.AutoCompleteMode = AutoCompleteMode.Suggest;
            MyComboBox.AutoCompleteSource = AutoCompleteSource.FileSystem;

            MyBrowseButton.Visible = true;
            MyBrowseButton.Click += new System.EventHandler(this.OnBrowseButtonClick);

            m_OpenFileDialog = new OpenFileDialog();
            m_OpenFileDialog.AddExtension = false;
            m_OpenFileDialog.Filter = "All files|*";
            m_OpenFileDialog.Title = "Select File";
           
            m_SaveFileDialog = new SaveFileDialog();
            m_SaveFileDialog.AddExtension = false;
            m_SaveFileDialog.Filter = "All files|*";
            m_SaveFileDialog.Title = "Save File";

            IsSave = false;
        }

        public bool IsSave { get; set; }

        private void OnBrowseButtonClick(object sender, EventArgs e)
        {
            if (IsSave)
            {
                if (m_SaveFileDialog.ShowDialog(Lib.MainForm) == DialogResult.OK)
                    Value = m_SaveFileDialog.FileName;
            }
            else
            {
                if (m_OpenFileDialog.ShowDialog(Lib.MainForm) == DialogResult.OK)
                    Value = m_OpenFileDialog.FileName;
            }
        }
    }
}
