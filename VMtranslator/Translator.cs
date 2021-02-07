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
            foreach (var line in commands)
            {
                string result;
                string[] splittedLine = line.Split(' ');
                string vmOperation = splittedLine[0];
                string segment = splittedLine.Length > 1 ? splittedLine[1] : "";
                string value = SetValue(splittedLine);







                if (splittedLine.Length > 0 && splittedLine[1] == "constant")
                    result = AssemblyCommands.Push(value);
                else
                    result = AssemblyCommands.Pop(value);

                if (splittedLine.Length == 1)
                    result = GetVmOperationAsAssemblyCommands(vmOperation, value);

                results.Add(result);

                Console.WriteLine(result);
            }
            return results;
        }

        private static string SetValue(string[] commandLine) =>
            commandLine.Length > 1 ? commandLine[2] : "";

        public string GetVmOperationAsAssemblyCommands(string method, string value)
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

        public string SelectSegment() { 
        
        
        
        }


        public void This(string action) {

            if (action == "pointer")
            {
//            @SP
//            AM = M - 1
//            D = M
//            @THIS
//            M = D

            }
            else
            {

            }



        }
        public void That()
        {



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
