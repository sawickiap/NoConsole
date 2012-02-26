using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace NoConsoleLib
{
    public class StringProperty : ComboBoxProperty
    {
        public void PostValueToMRU()
        {
            string val = Value;
            
            if (string.IsNullOrEmpty(val))
                return;

            int i, count = MyComboBox.Items.Count;
                
            for (i = 0; i < count; ++i)
            {
                if (MyComboBox.Items[i] as string == val)
                {
                    // Value fount in MRU list: Move to to the top.
                    if (i > 0)
                    {
                        MyComboBox.Items.RemoveAt(i);
                        MyComboBox.Items.Insert(0, val);
                        // WARNING! This operation cleared MyComboBox.Text. We have to restore it!
                        MyComboBox.SelectedIndex = 0;
                    }
                    break;
                }
            }

            // Value not found in MRU list: Add it.
            if (i == count)
            {
                while (count >= MRU_COUNT)
                    MyComboBox.Items.RemoveAt(--count);
                MyComboBox.Items.Insert(0, val);
            }
        }

        public override void SaveToConfig(ConfigNode node)
        {
            node.SetValue(CONFIG_VALUE, Value);

            for (int i = 0, count = MyComboBox.Items.Count; i < count; ++i)
            {
                ConfigNode mruNode = new ConfigNode(CONFIG_MRU, MyComboBox.Items[i] as string);
                node.InsertLastChild(mruNode);
            }

            base.SaveToConfig(node);
        }

        public override void LoadFromConfig(ConfigNode node)
        {
            base.LoadFromConfig(node);

            string s = null;
            if (node.GetValue(CONFIG_VALUE, ref s))
                Value = s;

            MyComboBox.Items.Clear();
            for (
                ConfigNode mruNode = node.FindFirstChild(CONFIG_MRU);
                mruNode != null;
                mruNode = mruNode.FindNextSibling(CONFIG_MRU))
            {
                if (!string.IsNullOrEmpty(mruNode.Value))
                    MyComboBox.Items.Add(mruNode.Value);
            }
        }

        public const int MRU_COUNT = 8;

        private const string CONFIG_MRU = "MRU";
    }
}
