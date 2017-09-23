﻿using System.Reflection;

namespace CmdCommands.Helpers
{
    public static class AssemblyHelpers
    {
        static public string AssemblyFilePath
        {
            get
            {
                return Assembly.GetExecutingAssembly().Location;
            }
        }
        static public string AssemblyFileName
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Name + ".exe";
            }
        }
    }
}