using System;
using System.Collections.Generic;
using System.Text;

namespace NoConsoleLib
{
    public interface ISerializable
    {
        string GetId();
        void SaveToConfig(ConfigNode node);
        void LoadFromConfig(ConfigNode node);
    }
}
