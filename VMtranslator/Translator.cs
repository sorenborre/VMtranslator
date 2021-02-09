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
            string result;
            foreach (var line in commands)
            {
                (string vmOperation, string segment, string value) = SplitOperation(line);

                if (vmOperation == "push")
                    result = AssemblyCommands.Push(segments[key: segment], value);

                else if (vmOperation == "pop")
                    result = AssemblyCommands.Pop(segments[key: segment], value);

                else
                    result = AssemblyCommands.GetLogicOrArithmeticCommand(vmOperation);

                results.Add(result);
            }
            return results;
        }

        private (string, string, string) SplitOperation(string line)
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

        private readonly IDictionary<string, string> segments = new Dictionary<string, string>() {
        {"argument", "ARG"},
        {"local", "LCL"},
        {"this", "THIS"},
        {"that", "THAT"},
        {"pointer0", "THIS"},
        {"pointer1", "THAT"},
        {"static", "static"},
        {"temp", "temp"},
        {"constant", "constant"}

        //"argument",//ARG(2)
        //"local",//LCL(1)
        //"this",//THIS(3)
        //"that",//THAT(4)
        //"temp",//0-7(5-12)
        //""
        };


    }
}
