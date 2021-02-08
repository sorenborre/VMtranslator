using System;
using System.Collections.Generic;
using System.Text;

namespace VMtranslator
{

    class AssemblyCommands
    {
        private int labelCounter = 0;

        public string Add()
        {
            return
                "//add\n" +
                "@SP\n" +
                "A=M-1\n" +
                "D=M\n" +
                "A=A-1\n" +
                "M=M+D\n" +
                "@SP\n" +
                "AM=M-1\n";
        }
        public string Sub()
        {
            return
                "//sub\n" +
                "@SP\n" +
                "A=M-1\n" +
                "D=M\n" +
                "A=A-1\n" +
                "M=M-D\n" +
                "@SP\n" +
                "AM=M-1\n";
        }

        public string Neg()
        {
            return
                "//neg\n" +
                "@SP\n" +
                "M=M-1\n" +
                "A=M\n" +
                "M=-M\n" +
                "@SP\n" +
                "AM=M+1\n";
        }
        public string Equals()
        {
            string ifTrue = GenerateLabel();
            string end = GenerateLabel();

            return
                "//equals\n" +
                "@SP\n" +
                "A=M-1\n" +
                "D=M\n" +
                "A=A-1\n" +
                "D=D-M\n" +
                $"@{ifTrue}\n" +
                "D;JEQ\n" +
                "@SP\n" +
                "AM=M-1\n" +
                "A=A-1\n" +
                "M=0\n" +
                //"@SP\n" +
                //"M=M+1\n" +
                $"@{end}\n" +
                "0;JMP\n" +
                $"({ifTrue})\n" +
                "@SP\n" +
                "AM=M-1\n" +
                "A=A-1\n" +
                "M=D-1\n" +
                $"({end})\n";
        }
        public string GreaterThan()
        {
            string ifTrue = GenerateLabel();
            string end = GenerateLabel();
            return
                "//GT\n" +
                "@SP\n" +
                "A=M-1\n" +
                "D=M\n" +
                "A=A-1\n" +
                "D=M-D\n" +
                $"@{ifTrue}\n" +
                "D;JGT\n" +
                "@SP\n" +
                "AM=M-1\n" +
                "A=A-1\n" +
                "M=0\n" +
                $"@{end}\n" +
                "0;JMP\n" +
                $"({ifTrue})\n" +
                "@SP\n" +
                "AM=M-1\n" +
                "A=A-1\n" +
                "M=0\n" +
                "M=-1\n" +
                $"({end})\n";
        }
        public string LesserThan()
        {
            string ifTrue = GenerateLabel();
            string end = GenerateLabel();
            return
                "//LT\n" +
                "@SP\n" +
                "A=M-1\n" +
                "D=M\n" +
                "A=A-1\n" +
                "D=M-D\n" +
                $"@{ifTrue}\n" +
                "D;JLT\n" +
                "@SP\n" +
                "AM=M-1\n" +
                "A=A-1\n" +
                "M=0\n" +
                $"@{end}\n" +
                "0;JMP\n" +
                $"({ifTrue})\n" +
                "@SP\n" +
                "AM=M-1\n" +
                "A=A-1\n" +
                "M=0\n" +
                "M=-1\n" +
                $"({end})\n";
        }
        public string And()
        {
            return
                "//and\n" +
                "@SP\n" +
                "M=M-1\n" +
                "A=M\n" +
                "D=M\n" +
                "@SP\n" +
                "M=M-1\n" +
                "A=M\n" +
                "M=D&M\n" +
                "D=M\n" +
                "@SP\n" +
                "M=M+1\n";

        }

        public string Or()
        {
            return
                "//or\n" +
                "@SP\n" +
                "M=M-1\n" +
                "A=M\n" +
                "D=M\n" +
                "@SP\n" +
                "M=M-1\n" +
                "A=M\n" +
                "M=D|M\n" +
                "D=M\n" +
                "@SP\n" +
                "M=M+1\n";
        }

        public string Not()
        {
            return
                "//not\n" +
                "@SP\n" +
                "M=M-1\n" +
                "A=M\n" +
                "D=M\n" +
                "M=!D\n" +
                "@SP\n" +
                "M=M+1\n";
        }
        public string GenerateLabel() =>
           "label." + labelCounter++;
        public string GenerateVariable() =>
           "var." + labelCounter++;

        public string PushConstant(string value)
        {
            return
                "//push constant\n" +
                $"@{value}\n" +
                "D = A\n" +
                "@SP\n" +
                "A = M\n" +
                "M = D\n" +
                "@SP\n" +
                "M = M + 1\n";
        }

        public string PushPointer(string segment)
        {
            return
                "//push constant\n" +
                $"@{segment}\n" +
                "D = M\n" +
                "@SP\n" +
                "A = M\n" +
                "M = D\n" +
                "@SP\n" +
                "M = M + 1\n";
        }

        public string Push(string segment, string value)
        {
            string commands = 
                "//push\n" +
                $"@{value}\n" +
                "D=A\n" +
                $"@{segment}\n" +
                "A=M+D\n" +
                "D=M\n" +
                "@SP\n" +
                "A=M\n" +
                "M=D\n" +
                "@SP\n" +
                "AM=M+1\n";

            return segment == "static" ? PushStatic(value) : commands;
        }

        public string PushStatic(string value)
        {
            return
                "//push static\n" +
                $"@var.{value}\n" +
                "D=M\n" +
                "@SP\n" +
                "A=M\n" +
                "M=D\n" +
                "@SP\n" +
                "AM=M+1\n";
        }


        public string Pop(string segment, string value) {

            string commands = 
               "//pop\n" +
               $"@{value}\n" +
               "D=A\n" +
               $"@{segment}\n" +
               "D=M+D\n" +
               "@R13\n" +
               "M=D\n" +
               "@SP\n" +
               "AM=M-1\n" +
               "D=M\n" +
               "@R13\n" +
               "A=M\n" +
               "M=D\n";

            return segment == "static" ? PopStatic(value) : commands;
        }

        public string PopStatic(string value)
        {

            return
                "//Pop static\n"+
                   "@SP\n" +
                   "AM= M-1\n" +
                   "D=M\n" +
                   $"@var.{value}\n" +
                   "M=D\n" +
                   "@SP\n";
        }

    }
}
