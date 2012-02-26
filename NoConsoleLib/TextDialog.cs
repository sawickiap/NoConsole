using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NoConsoleLib
{
    public partial class TextDialog : Form
    {
        public TextDialog()
        {
            InitializeComponent();
            m_OutputControl.MyRichTextBox.Font = Lib.MonospacedFont;
        }

        public static void ShowText(IWin32Window parent, Image image, string title, string text, bool wordWrap = false)
        {
            TextDialog dialog = new TextDialog();
            dialog.Text = title;
            dialog.m_PictureBox.Image = image;
            dialog.m_OutputControl.MyRichTextBox.Text = text;
            dialog.m_OutputControl.MyRichTextBox.WordWrap = wordWrap;
            dialog.ShowDialog(parent);
        }
    }
}
