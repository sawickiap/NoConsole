using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace NoConsoleLib
{
    public class ConfigNode
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public ConfigNode Parent { get { return m_Parent; } }
        public ConfigNode PrevSibling { get { return m_PrevSibling; } }
        public ConfigNode NextSibling { get { return m_NextSibling; } }
        public ConfigNode FirstChild { get { return m_FirstChild; } }
        public ConfigNode LastChild { get { return m_LastChild; } }
        public bool Root { get { return Parent == null; } }

        public ConfigNode(string name = null, string value = null)
        {
            Name = name;
            Value = value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Returns true if this node is valid comparing to its surrounding.</returns>
        public bool NodeValid()
        {
            // This is root node.
            if (Parent == null)
            {
                // Name and Value must be null.
                /*
                if (Name != null || Value != null)
                    return false;
                 */
                // Must not have siblings.
                if (PrevSibling != null || NextSibling != null)
                    return false;
            }
            // This is not root node.
            else
            {
                // Name must not be empty.
                /*
                if (string.IsNullOrEmpty(Name))
                    return false;
                 */
                // Parent must have some children.
                if (Parent.FirstChild == null || Parent.LastChild == null)
                    return false;
            }

            if (PrevSibling != null)
            {
                // My prev sibling must point to me.
                if (PrevSibling.NextSibling != this)
                    return false;
                // My prev sibling must point to same parent.
                if (PrevSibling.Parent != Parent)
                    return false;
                // I can't be first child of my parent.
                if (Parent != null && Parent.FirstChild == this)
                        return false;
            }
            else
            {
                // I must be first child of my parent.
                if (Parent != null && Parent.FirstChild != this)
                        return false;
            }

            if (NextSibling != null)
            {
                // My next sibling must point to me.
                if (NextSibling.PrevSibling != this)
                    return false;
                // My next sibling must point to same parent.
                if (NextSibling.Parent != Parent)
                    return false;
                // I can't be last child of my parent.
                if (Parent != null && Parent.LastChild == this)
                    return false;
            }
            else
            {
                // I must be last child of my parent.
                if (Parent != null && Parent.LastChild != this)
                    return false;
            }

            // I have some children.
            if (FirstChild != null)
            {
                // I must have both FirstChild and LastChild set.
                if (LastChild == null)
                    return false;

                // My first child must point to me.
                if (FirstChild.Parent != this)
                    return false;
                // My first child must not have prev sibling.
                if (FirstChild.PrevSibling != null)
                    return false;

                // My last child must point to me.
                if (LastChild.Parent != this)
                    return false;
                // My last child must not have next sibling.
                if (LastChild.NextSibling != null)
                    return false;
            }
            // I have no children.
            else
            {
                // I must have neither FirstChild nor LastChild set.
                if (LastChild != null)
                    return false;
            }

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Returns true if this node and all child nodes recursively are valid.</returns>
        public bool TreeValid()
        {
            if (!NodeValid())
                return false;

            for (ConfigNode childNode = FirstChild; childNode != null; childNode = childNode.NextSibling)
                if (!childNode.TreeValid())
                    return false;

            return true;
        }

        /// <summary>
        /// Remove this and all its children from the tree.
        /// It will become root of a separate tree.
        /// </summary>
        public void Remove()
        {
            // I already am root of separate tree.
            if (Parent == null)
                return;

            if (m_PrevSibling != null)
                m_PrevSibling.m_NextSibling = m_NextSibling;
            else
                m_Parent.m_FirstChild = m_NextSibling;

            if (m_NextSibling != null)
                m_NextSibling.m_PrevSibling = m_PrevSibling;
            else
                m_Parent.m_LastChild = m_PrevSibling;

            m_PrevSibling = null;
            m_NextSibling = null;
            m_Parent = null;
        }

        /// <summary>
        /// Clear children list.
        /// Child nodes will be in invalid state, but noone cares.
        /// </summary>
        public void RemoveAllChildren()
        {
            m_FirstChild = m_LastChild = null;
        }

        /// <summary>
        /// Insert given node as first child of this node.
        /// </summary>
        /// <param name="newNode">Must be valid root of separate tree. Can have children.</param>
        public void InsertFirstChild(ConfigNode newNode)
        {
            newNode.m_Parent = this;
            newNode.m_NextSibling = m_FirstChild;

            if (m_FirstChild != null)
                m_FirstChild.m_PrevSibling = newNode;
            else
                m_LastChild = newNode;

            m_FirstChild = newNode;
        }

        /// <summary>
        /// Insert given node as last child of this node.
        /// </summary>
        /// <param name="newNode">Must be valid root of separate tree. Can have children.</param>
        public void InsertLastChild(ConfigNode newNode)
        {
            newNode.m_Parent = this;
            newNode.m_PrevSibling = m_LastChild;

            if (m_LastChild != null)
                m_LastChild.m_NextSibling = newNode;
            else
                m_FirstChild = newNode;

            m_LastChild = newNode;
        }

        /// <summary>
        /// Insert given node as previous sibling of this node.
        /// </summary>
        /// <param name="newNode">Must be valid root of separate tree. Can have children.</param>
        public void InsertPrevSibling(ConfigNode newNode)
        {
            newNode.m_Parent = m_Parent;
            newNode.m_PrevSibling = m_PrevSibling;
            newNode.m_NextSibling = this;

            if (m_PrevSibling != null)
                m_PrevSibling.m_NextSibling = newNode;
            else
                m_Parent.m_FirstChild = newNode;

            m_PrevSibling = newNode;
        }

        /// <summary>
        /// Insert given node as next sibling of this node.
        /// </summary>
        /// <param name="newNode">Must be valid root of separate tree. Can have children.</param>
        public void InsertNextSibling(ConfigNode newNode)
        {
            newNode.m_Parent = m_Parent;
            newNode.m_NextSibling = m_NextSibling;
            newNode.m_PrevSibling = this;

            if (m_NextSibling != null)
                m_NextSibling.m_PrevSibling = newNode;
            else
                m_Parent.m_LastChild = newNode;

            m_NextSibling = newNode;
        }

        /// <summary>
        /// Find first direct child node with given name. O(n).
        /// </summary>
        /// <param name="name">Case-sensitive.</param>
        /// <returns>Found child node or null if not found.</returns>
        public ConfigNode FindFirstChild(string name)
        {
            for (ConfigNode child = FirstChild; child != null; child = child.NextSibling)
                if (child.Name == name)
                    return child;
            return null;
        }

        /// <summary>
        /// Find last direct child node with given name. O(n).
        /// </summary>
        /// <param name="name">Case-sensitive.</param>
        /// <returns>Found child node or null if not found.</returns>
        public ConfigNode FindLastChild(string name)
        {
            for (ConfigNode child = LastChild; child != null; child = child.PrevSibling)
                if (child.Name == name)
                    return child;
            return null;
        }

        /// <summary>
        /// Search next siblings to find first with given name. O(n).
        /// </summary>
        /// <param name="name">Case-sensitive.</param>
        /// <returns>Found sibling or null if not found.</returns>
        public ConfigNode FindNextSibling(string name)
        {
            for (ConfigNode sibling = NextSibling; sibling != null; sibling = sibling.NextSibling)
                if (sibling.Name == name)
                    return sibling;
            return null;
        }

        /// <summary>
        /// Search previous siblings to find first with given name. O(n).
        /// </summary>
        /// <param name="name">Case-sensitive.</param>
        /// <returns>Found sibling or null if not found.</returns>
        public ConfigNode FindPrevSibling(string name)
        {
            for (ConfigNode sibling = PrevSibling; sibling != null; sibling = sibling.PrevSibling)
                if (sibling.Name == name)
                    return sibling;
            return null;
        }

        /// <summary>
        /// Find direct or indirect child node based on given name path.
        /// </summary>
        /// <param name="path">In form of "Name1/Name2/Name3"</param>
        /// <returns>Found child node or null if not found.</returns>
        public ConfigNode FindPath(string path)
        {
            int slashIndex = path.IndexOf('/');
            if (slashIndex == -1)
                return FindFirstChild(path);
            else
            {
                string name = path.Substring(0, slashIndex);
                ConfigNode child = FindFirstChild(name);
                if (child == null)
                    return null;
                path = path.Substring(slashIndex + 1);
                return child.FindPath(path);
            }
        }

        /// <summary>
        /// Return child node with given name.
        /// If no such node exists, create it.
        /// </summary>
        public ConfigNode EnsureChild(string name)
        {
            ConfigNode childNode = FindFirstChild(name);
            
            if (childNode == null)
            {
                childNode = new ConfigNode(name);
                InsertLastChild(childNode);
                return childNode;
            }

            return childNode;
        }

        /// <summary>
        /// Set value of a subnode with given name to given value.
        /// If subnode with such name doesn't exist, create it.
        /// </summary>
        public void SetValue(string name, string value)
        {
            ConfigNode childNode = FindFirstChild(name);

            if (childNode != null)
                childNode.Value = value;
            else
            {
                ConfigNode newNode = new ConfigNode(name, value);
                InsertLastChild(newNode);
            }
        }

        /// <summary>
        /// Return value of a subnode with given name.
        /// If subnode with such name doesn't exist, leave value unmodified and return false.
        /// </summary>
        public bool GetValue(string name, ref string value)
        {
            ConfigNode childNode = FindFirstChild(name);

            if (childNode != null)
            {
                value = childNode.Value;
                return true;
            }
            else
                return false;
        }

        public void SetBoolValue(string name, bool value)
        {
            SetValue(name, value ? "1" : "0");
        }

        public bool GetBoolValue(string name, ref bool value)
        {
            string s = null;
            if (!GetValue(name, ref s))
                return false;

            if (s == "1")
            {
                value = true;
                return true;
            }
            else if (s == "0")
            {
                value = false;
                return true;
            }
            else
                return false;
        }

        private ConfigNode m_Parent;
        private ConfigNode m_PrevSibling, m_NextSibling;
        private ConfigNode m_FirstChild, m_LastChild;
    }

    public class ConfigWriter
    {
        public ConfigWriter(TextWriter textWriter)
        {
            m_TextWriter = textWriter;
        }

        public void Process(ConfigNode node)
        {
            ProcessChildren(0, node);
        }

        private void ProcessNode(int level, ConfigNode node)
        {
            for (int i = 0; i < level; ++i)
                m_TextWriter.Write('\t');
            
            WriteString(node.Name, true);

            if (!string.IsNullOrEmpty(node.Value))
            {
                m_TextWriter.Write('=');
                WriteString(node.Value, false);
            }

            m_TextWriter.Write("\r\n");

            ++level;
            ProcessChildren(level, node);
        }

        private void ProcessChildren(int level, ConfigNode node)
        {
            for (ConfigNode child = node.FirstChild; child != null; child = child.NextSibling)
                ProcessNode(level, child);
        }

        private void WriteString(string s, bool isName)
        {
            /* Characters that must be escaped:
             * Space           -> \s    Only at the beginning of name
             * Tab             -> \t    Only at the beginning of name
             * Equal           -> \=    Only inside name
             * Line feed       -> \n    Always
             * Carriage return -> \r    Always
             * Backslash       -> \\    Always
             */

            int index = 0, sLen = s.Length;
            
            if (isName)
            {
                for (; index < sLen; ++index)
                {
                    char ch = s[index];
                    if (ch == ' ')
                        m_TextWriter.Write(@"\s");
                    else if (ch == '\t')
                        m_TextWriter.Write(@"\t");
                    else
                        break;
                }
            }

            char[] charsToFind = isName ?
                new char[] { '\r', '\n', '\\', '=' } :
                new char[] { '\r', '\n', '\\' };

            while (index < sLen)
            {
                int specialCharIndex = s.IndexOfAny(charsToFind, index);
                
                // No special chars left: Write to the end.
                if (specialCharIndex == -1)
                {
                    m_TextWriter.Write(s.Substring(index));
                    index = sLen;
                }
                else
                {
                    // Write until the special char.
                    if (specialCharIndex > index)
                        m_TextWriter.Write(s.Substring(index, specialCharIndex - index));
                    // Encode special char.
                    switch (s[specialCharIndex])
                    {
                        case '\r': m_TextWriter.Write(@"\r"); break;
                        case '\n': m_TextWriter.Write(@"\n"); break;
                        case '\\': m_TextWriter.Write(@"\\"); break;
                        case '=':  m_TextWriter.Write(@"\="); break;
                        default: Debug.Assert(false);break;
                    }
                    index = specialCharIndex + 1;
                }
            }
        }

        private TextWriter m_TextWriter;
    }

    public class ConfigReader
    {
        public ConfigReader(TextReader textReader)
        {
            m_TextReader = textReader;
        }

        public void Process(ConfigNode node)
        {
            node.RemoveAllChildren();

            Stack<ConfigNode> nodeStack = new Stack<ConfigNode>();
            nodeStack.Push(node);

            string line, name, value;
            int level, lineIndex = 1;
            while ((line = m_TextReader.ReadLine()) != null)
            {
                ParseLine(out level, out name, out value, line, lineIndex);

                if (level > nodeStack.Count - 1)
                    throw new Exception(string.Format("Invalid indentation in line {0}.", lineIndex));
                else while (level < nodeStack.Count - 1)
                    nodeStack.Pop();

                ConfigNode newNode = new ConfigNode(name, value);
                nodeStack.Peek().InsertLastChild(newNode);
                nodeStack.Push(newNode);

                ++lineIndex;
            }
        }

        private void ParseLine(out int level, out string name, out string value, string line, int lineIndex)
        {
            int index = 0, lineLen = line.Length;

            level = 0;
            while (line[index] == '\t')
            {
                ++level;
                ++index;
            }

            ParseString(out name, line, lineIndex, ref index, true);

            if (index == lineLen)
                value = null;
            else
            {
                // Skip '='.
                ++index;

                if (index == lineLen)
                    value = string.Empty;
                else
                    ParseString(out value, line, lineIndex, ref index, false);
            }
        }

        private void ParseString(out string dst, string src, int srcLineIndex, ref int srcIndex, bool isName)
        {
            int srcLen = src.Length;
            StringBuilder sb = new StringBuilder(src.Length);

            while (srcIndex < srcLen)
            {
                char ch = src[srcIndex];
                
                if (ch == '=' && isName)
                    break;
                else if (ch == '\\')
                {
                    ++srcIndex;

                    ch = src[srcIndex];
                    ++srcIndex;

                    switch (ch)
                    {
                        case 's': sb.Append(' '); break;
                        case 't': sb.Append('\t'); break;
                        case 'r': sb.Append('\r'); break;
                        case 'n': sb.Append('\n'); break;
                        case '=': sb.Append('='); break;
                        case '\\': sb.Append('\\'); break;
                        default:
                            throw new Exception(
                                string.Format("Invalid escape sequence in line {0}.", srcLineIndex));
                    }
                }
                else
                {
                    ++srcIndex;
                    sb.Append(ch);
                }
            }

            dst = sb.ToString();
        }

        private TextReader m_TextReader;
    }
}
