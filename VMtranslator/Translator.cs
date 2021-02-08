using System;
using System.Collections.Generic;

namespace VMtranslator
{
    class Translator
    {
        private readonly AssemblyCommands AssemblyCommands;
        private const string ThisPointer = "3";
        private const string ThatPointer = "4";

        public Translator(AssemblyCommands assemblyCommands)
        {
            AssemblyCommands = assemblyCommands;
        }

        public List<string> Run(List<string> commands)
        {
            List<string> results = new List<string>();
            foreach (var line in commands)
            {
                (string vmOperation, string segment, string value) = SplitOperation(line);

                string result;
                if (vmOperation == "push")
                {
                    if (segment == "constant")
                        result = AssemblyCommands.PushConstant(value);
                    else if (segment.Contains("pointer"))
                        result = AssemblyCommands.PushPointer(segments[segment]);
                    else
                        result = AssemblyCommands.Push(segments[key: segment], value);

                }
                else if (vmOperation == "pop")
                {
                    result = AssemblyCommands.Pop(segments[key: segment], value);
                }
                else
                    result = GetLogicOrArithmeticAssemblyCommand(vmOperation);

                results.Add(result);
            }
            return results;
        }

        public (string, string, string) SplitOperation(string line)
        {
            string[] splittedLine = line.Split(' ');
            string vmOperation = splittedLine[0];

            string segment = splittedLine.Length > 1 ? splittedLine[1] : "";
            string value = splittedLine.Length > 1 ? splittedLine[2] : "";
            
            if (segment == "pointer")
            {
                segment += value;
                value = value == "0" ? ThisPointer : ThatPointer;
            }

            segment = segment == "pointer" ? segment + value : segment;

            return (vmOperation, segment, value);
        }

        public string GetLogicOrArithmeticAssemblyCommand(string command)
        {
            return command switch
            {
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

        //public string SelectSegment()
        //{



        //}


        public void This(string action)
        {
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

        IDictionary<string, string> segments = new Dictionary<string, string>() {
        {"argument", "ARG"},
        {"local", "LCL"},
        {"this", "THIS"},
        {"that", "THAT"},
        {"pointer0", "THIS"},
        {"pointer1", "THAT"},
        {"static", "static"}

        //{"temp", "TEMP"},
        //"argument",//ARG(2)
        //"local",//LCL(1)
        //"this",//THIS(3)
        //"that",//THAT(4)
        //"temp",//0-7(5-12)
        //""
        };
    }
}
