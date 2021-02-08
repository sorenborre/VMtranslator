using System;
using System.Collections.Generic;
using System.IO;

namespace VMtranslator
{
    class Program
    {
        static void Main(string[] args)
        {


            Translator translator = new Translator(new AssemblyCommands());
            List<string> tmp = new List<string>(VmStreamReader.ReadStream());
            //List<string> tmp = new List<string>() { "pop pointer 0" };

            List<string> assemblyCommands = translator.Run(tmp);

            File.WriteAllLines("C:/Users/Soere/Desktop/generatedAssembly.txt", assemblyCommands);
            File.WriteAllLines(@"../../../generatedAssembly.txt", assemblyCommands);
        }
    }
}
