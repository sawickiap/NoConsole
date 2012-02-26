using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NoConsoleLib;
using System.IO;

namespace NoConsole
{
    public partial class ProfileDialog : Form
    {
        public ProfileDialog()
        {
            InitializeComponent();
        }

        public void Init(bool save)
        {
            m_Save = save;
            Text = save ? "Save Profile" : "Load Profile";
        }

        public string SelectedPath { get { return m_SelectedPath; } }
        public string SelectedName { get { return m_SelectedName; } }

        private void m_InitTimer_Tick(object sender, EventArgs e)
        {
            m_InitTimer.Enabled = false;
            
            DeferredInit();
        }

        private void DeferredInit()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                LoadProfileList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    this,
                    ex.Message,
                    NoConsoleLib.Lib.APP_TITLE,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private string MakeProfileFileName(string profileName)
        {
            return string.Format(
                "{0}.{1}.config",
                Lib.ScriptName,
                profileName);
        }

        private void LoadProfileList()
        {
            string[] configFilePaths = Directory.GetFiles(
                Lib.ScriptDataDir,
                MakeProfileFileName("?*"));

            m_ListBox.BeginUpdate();

            foreach (string path in configFilePaths)
            {
                string name = Path.GetFileNameWithoutExtension(path);
                // name is now: scriptName '.' profileName
                name = name.Substring(Lib.ScriptName.Length + 1);

                Item item = new Item(path, name);
                m_ListBox.Items.Add(item);
            }

            m_ListBox.EndUpdate();
        }

        private void m_OkButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(m_NameTextBox.Text))
            {
                m_NameTextBox.Focus();
                return;
            }

            m_SelectedName = m_NameTextBox.Text;
            m_SelectedPath = Path.Combine(
                Lib.ScriptDataDir,
                MakeProfileFileName(m_SelectedName));

            if (!m_Save)
            {
                if (!CheckProfileExists(m_SelectedName, m_SelectedPath))
                    return;
            }

            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private class Item
        {
            public string m_Path;
            public string m_Name;

            public Item(string path, string name) { m_Path = path; m_Name = name; }
            public override string ToString() { return m_Name; }
        };

        private bool m_Save;
        private string m_SelectedPath, m_SelectedName;

        private void m_ListBox_SelectedValueChanged(object sender, EventArgs e)
        {
            if (m_ListBox.SelectedItem != null)
                m_NameTextBox.Text = m_ListBox.SelectedItem.ToString();
        }

        /// <summary>
        /// If profile file doesn't exist, show message box and return false.
        /// </summary>
        private bool CheckProfileExists(string profileName, string filePath)
        {
            if (File.Exists(filePath))
                return true;
            else
            {
                MessageBox.Show(
                    this,
                    string.Format("Profile \"{0}\" doesn't exist.", profileName),
                    DELETE_PROFILE,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }
        }

        private void m_DeleteButton_Click(object sender, EventArgs e)
        {
            string name = m_NameTextBox.Text;

            try
            {
                if (string.IsNullOrEmpty(name))
                    return;

                string filePath = Path.Combine(
                    Lib.ScriptDataDir,
                    MakeProfileFileName(name));

                if (!CheckProfileExists(name, filePath))
                    return;

                if (MessageBox.Show(
                    this,
                    string.Format("Do you really want to delete profile \"{0}\" ?", name),
                    DELETE_PROFILE,
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Exclamation) != DialogResult.Yes)
                {
                    return;
                }

                File.Delete(filePath);

                for (int i = 0, count = m_ListBox.Items.Count; i < count; ++i)
                {
                    if ((m_ListBox.Items[i] as Item).m_Name == name)
                    {
                        m_ListBox.Items.RemoveAt(i);
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    this,
                    string.Format("Cannot delete profile \"{0}\".\r\n{1}", name, ex.Message),
                    DELETE_PROFILE,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private const string DELETE_PROFILE = "Delete Profile";

        private void m_ListBox_DoubleClick(object sender, EventArgs e)
        {
            m_OkButton_Click(sender, e);
        }

        private void m_ListBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                m_OkButton_Click(sender, e);
        }
    }
}
