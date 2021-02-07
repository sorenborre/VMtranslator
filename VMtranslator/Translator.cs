using System;
using System.Collections.Generic;

namespace VMtranslator
{
    class Translator
    {
        private readonly AssemblyCommands AssemblyCommands;

        public Translator(AssemblyCommands assemblyCommands)
        {
            AssemblyCommands = assemblyCommands;
        }

        public List<string> Run(List<string> commands)
        {
            List<string> results = new List<string>();
            foreach (var item in commands)
            {
                string[] commandLine = item.Split(' ');

                string vmCommand = commandLine[0];
                string value = commandLine.Length > 1 ? commandLine[2] : "0";

                if (commandLine[0] =="push")
                {

                }
                else if (commandLine[0] == "pop")
                {

                }

                string result = GetVmCommandAsAssemblyCommands(vmCommand, value);
                results.Add(result);

                Console.WriteLine(result);
            }
            return results;
        }

        public string GetVmCommandAsAssemblyCommands(string method, string value)
        {
            return method switch
            {
                "push" => AssemblyCommands.Push(value),
                "pop" => AssemblyCommands.Pop(value),//tag stilling til parameter
                "add" => AssemblyCommands.Add(),
                "sub" => AssemblyCommands.Sub(),
                "neg" => AssemblyCommands.Neg(),
                "eq" => AssemblyCommands.Equals(),
                "gt" => AssemblyCommands.GreaterThan(),
                "lt" => AssemblyCommands.LesserThan(),
                "not" => AssemblyCommands.Not(),
                "or" => AssemblyCommands.Or(),
                "and" => AssemblyCommands.And(),
                _ => "",
            };
        }

        List<string> stringlist = new List<string>() {
        "constant",
        "argument",//ARG(2)
        "local",//LCL(1)
        "this",//THIS(3)
        "that",//THAT(4)
        "temp",//0-7(5-12)
        ""
        };
    }
}
