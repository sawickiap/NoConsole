using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;

namespace NoConsoleLib
{
    public class Page : TableLayoutPanel
    {
        ////////////////////////////////////////////////////////////////////////////////
        // For host application.

        ////////////////////////////////////////////////////////////////////////////////
        // For all users, including script

        public Page(string title)
        {
            Debug.Assert(Lib.Initialized);

            m_Title = title;

            Form mainForm = Lib.MainForm;

            this.Visible = false;
            this.AutoScroll = true;
            this.AutoScrollMinSize = new System.Drawing.Size(400, 300); // TODO
            this.ColumnCount = 1;
            this.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            this.Location = new System.Drawing.Point(0, MARGIN_TOP);
            this.Size = new System.Drawing.Size(
                mainForm.ClientSize.Width,
                mainForm.ClientSize.Height - MARGIN_TOP - MARGIN_BOTTOM);
            this.Anchor = (System.Windows.Forms.AnchorStyles)(
                System.Windows.Forms.AnchorStyles.Top |
                System.Windows.Forms.AnchorStyles.Bottom |
                System.Windows.Forms.AnchorStyles.Left |
                System.Windows.Forms.AnchorStyles.Right);
            this.Padding = new System.Windows.Forms.Padding(MARGIN_LEFT_RIGHT, 0, MARGIN_LEFT_RIGHT, 0);
            this.RowCount = 0;

            mainForm.Controls.Add(this);
        }

        public void Activate()
        {
            Lib.ActivatePage(this);
        }

        public string Title { get { return m_Title; } }

        public void AddControl(Control control, bool fill = false)
        {
            int row = this.RowCount;

            ++this.RowCount;
            if (fill)
            {
                this.RowStyles.Add(new System.Windows.Forms.RowStyle(
                    System.Windows.Forms.SizeType.Percent,
                    100F));
            }
            else
            {
                this.RowStyles.Add(new System.Windows.Forms.RowStyle(
                    System.Windows.Forms.SizeType.Absolute,
                    control.Size.Height + control.Margin.Top + control.Margin.Bottom));
            }

            this.Controls.Add(control, 0, row);
            control.Width = this.ClientSize.Width;

            if (fill)
                control.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            else
                control.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        }

        ////////////////////////////////////////////////////////////////////////////////
        // Private

        private const int MARGIN_LEFT_RIGHT = 9;
        private const int MARGIN_TOP = 31;
        private const int MARGIN_BOTTOM = 44;
        private const int PROPERTY_MARGIN_Y = 6;

        private string m_Title;
    }
}
