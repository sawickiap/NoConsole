using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace NoConsoleLib
{
    public class EnumProperty : ComboBoxProperty
    {
        public EnumProperty()
        {
            AllowCustomValue = false;
        }

        public int SelectedIndex
        {
            get { return MyComboBox.SelectedIndex; }
            set { MyComboBox.SelectedIndex = value; }
        }

        public bool AllowCustomValue
        {
            get { return MyComboBox.DropDownStyle == ComboBoxStyle.DropDown; }
            set { MyComboBox.DropDownStyle = value ? ComboBoxStyle.DropDown : ComboBoxStyle.DropDownList; }
        }

        public void AddItem(object item)
        {
            MyComboBox.Items.Add(item);
        }

        public override void SaveToConfig(ConfigNode node)
        {
            base.SaveToConfig(node);

            node.SetValue(CONFIG_VALUE, Value);
        }

        public override void LoadFromConfig(ConfigNode node)
        {
            base.LoadFromConfig(node);

            string s = null;
            if (node.GetValue(CONFIG_VALUE, ref s))
            {
                if (AllowCustomValue)
                    MyComboBox.Text = s;
                else
                {
                    foreach (object item in MyComboBox.Items)
                    {
                        if (item as string == s)
                        {
                            MyComboBox.SelectedItem = item;
                            break;
                        }
                    }
                }
            }
        }
    }
}
