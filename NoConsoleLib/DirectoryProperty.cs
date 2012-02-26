using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace NoConsoleLib
{
    public class DirectoryProperty : StringProperty
    {
        private FolderBrowserDialog m_FolderDialog;

        public FolderBrowserDialog MyFolderBrowserDialog { get { return m_FolderDialog; } }

        public DirectoryProperty()
        {
            MyComboBox.AutoCompleteMode = AutoCompleteMode.Suggest;
            MyComboBox.AutoCompleteSource = AutoCompleteSource.FileSystemDirectories;

            MyBrowseButton.Visible = true;
            MyBrowseButton.Click += new System.EventHandler(this.OnBrowseButtonClick);

            m_FolderDialog = new FolderBrowserDialog();
        }

        private void OnBrowseButtonClick(object sender, EventArgs e)
        {
            if (m_FolderDialog.ShowDialog(Lib.MainForm) == DialogResult.OK)
                Value = m_FolderDialog.SelectedPath;
        }
    }
}
