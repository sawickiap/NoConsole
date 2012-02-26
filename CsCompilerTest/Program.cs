using System;
using System.Collections.Generic;
using System.Text;
using System.CodeDom.Compiler;
using System.Reflection;

namespace ConsoleApplication1
{
    class Program
    {
        const string Code = @"
using System;
using System.Windows.Forms;

public class MainClass {
    public void Main()
    {
        Console.WriteLine(""Test from script"");
        //MessageBox.Show(""Test from script"");
    }
} // class MainClass
";

        static void Main(string[] args)
        {
            CodeDomProvider codeDomProvider = new Microsoft.CSharp.CSharpCodeProvider();

            string[] assemblyNames = new string[]{
                "mscorlib.dll",
                "System.dll",
                "System.Data.dll",
                "System.Drawing.dll",
                "System.Xml.dll",
                "System.Core.dll",
                "System.Windows.Forms.dll"
            };
            
            CompilerParameters compilerParameters = new CompilerParameters(assemblyNames){
                OutputAssembly = "OutputAssembly.dll",
                GenerateExecutable = false,
                GenerateInMemory = true,
                WarningLevel = 3,
                CompilerOptions = "/optimize",
                IncludeDebugInformation = false,
                //TempFiles = new TempFileCollection(".", true)
            };
           
            CompilerResults compilerResults = codeDomProvider.CompileAssemblyFromSource(
                compilerParameters,
                new string[] { Code });

            /* This prints low-level messages like this, as well as error messages:
                * 
                * G:\tmp\CsCompilerTest\WorkingDir> "C:\Windows\Microsoft.NET\Framework\v4.0.30319
                * \csc.exe" /t:library /utf8output /out:"C:\Users\Adam Sawicki\AppData\Local\Temp\
                * 0pdzupen.dll" /debug- /optimize+ /w:3 /optimize  "C:\Users\Adam Sawicki\AppData\
                * Local\Temp\0pdzupen.0.cs"

                * Microsoft (R) Visual C# 2010 Compiler version 4.0.30319.1
                * Copyright (C) Microsoft Corporation. All rights reserved.
                */
            foreach (String message in compilerResults.Output)
                Console.WriteLine(message);

            /* This prints error messages in form of:
                * 
                * c:\Users\Adam Sawicki\AppData\Local\Temp\4kbqoyz2.0.cs(7,9) : error CS0103: The
                * name 'Console' does not exist in the current context
                */
            //foreach (CompilerError error in compilerResults.Errors)
            //    Console.WriteLine(error.ToString());

            Assembly assembly = compilerResults.CompiledAssembly;
            if (assembly == null) return;

            object obj = assembly.CreateInstance("MainClass");
            if (obj == null) return;

            Type type = obj.GetType();
            type.InvokeMember("Main", BindingFlags.Instance | BindingFlags.InvokeMethod | BindingFlags.Public, null, obj, new object[] { });
        }
    }
}
