using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace NoConsoleLib
{
    // Data passed between background threads and main thread during process execution.
    public class ProcessOutput
    {
        public bool Exited { get { return m_Exited; } }
        public bool OutputEnd { get { return m_OutputEnd; } }
        public bool ErrorEnd { get { return m_ErrorEnd; } }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>true if some new text have been added to richTextBox.</returns>
        public bool AppendOutputToRichTextBox(System.Windows.Forms.RichTextBox richTextBox)
        {
            bool newLines = false;

            lock (m_SyncObj)
            {
                if (m_OutputLines.Count > 0)
                {
                    newLines = true;

                    // I can see it's not required, RichTextBox already does not flicker or slow down
                    // when adding hundreds of lines.
                    //richTextBox.BeginUpdate(); TODO ?

                    richTextBox.SelectionStart = richTextBox.TextLength;
                    richTextBox.SelectionLength = 0;

                    while (m_OutputLines.Count > 0)
                    {
                        string line = m_OutputLines.Dequeue();
                        bool lineIsError = m_OutputLineIsError.Dequeue();

                        richTextBox.SelectionColor = lineIsError ?
                            System.Drawing.Color.Maroon :
                            System.Drawing.Color.Black;

                        richTextBox.AppendText(line);
                        richTextBox.AppendText("\r\n");
                    }

                    //richTextBox.EndUpdate(); TODO ?
                }
            }

            return newLines;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>true if some new text have been written.</returns>
        public bool AppendOutputToWriter(System.IO.StreamWriter writer)
        {
            bool newLines = false;

            lock (m_SyncObj)
            {
                if (m_OutputLines.Count > 0)
                {
                    newLines = true;

                    while (m_OutputLines.Count > 0)
                    {
                        string line = m_OutputLines.Dequeue();
                        m_OutputLineIsError.Dequeue();

                        writer.WriteLine(line);
                    }
                }
            }

            return newLines;
        }

        /// <summary>
        /// Use it as event handler to Process:
        /// myProcess.OutputDataReceived += new DataReceivedEventHandler(myProcessOutput.OnOutputDataReceived);
        /// Can be called on separate thread.
        /// </summary>
        public void OnOutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            // e.Data == null means end of stream.
            if (e.Data == null)
                m_OutputEnd = true;
            else
            {
                lock (m_SyncObj)
                {
                    m_OutputLines.Enqueue(e.Data);
                    m_OutputLineIsError.Enqueue(false);
                }
            }
        }

        /// <summary>
        /// Use it as event handler to Process:
        /// myProcess.ErrorDataReceived += new DataReceivedEventHandler(myProcessOutput.OnErrorDataReceived);
        /// Can be called on separate thread.
        /// </summary>
        public void OnErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            // e.Data == null means end of stream/
            if (e.Data == null)
                m_ErrorEnd = true;
            else
            {
                lock (m_SyncObj)
                {
                    m_OutputLines.Enqueue(e.Data);
                    m_OutputLineIsError.Enqueue(true);
                }
            }
        }

        /// <summary>
        /// Use it as event handler to Process:
        /// myProcess.Exited += new EventHandler(myProcessOutput.OnProcessExited);
        /// Can be called on separate thread.
        /// </summary>
        public void OnProcessExited(object sender, EventArgs e)
        {
            m_Exited = true;
        }

        /// <summary>
        /// Used as mutex to accessm_OutputLines, m_OutputLineIsError.
        /// </summary>
        private Object m_SyncObj = new Object();

        private Queue<string> m_OutputLines = new Queue<string>();
        private Queue<bool> m_OutputLineIsError = new Queue<bool>();

        private volatile bool m_Exited = false;
        private volatile bool m_OutputEnd = false;
        private volatile bool m_ErrorEnd = false;
    }
}
