using System;
using System.Collections.Generic;
using System.Text;
using Formular_Specifications.Functionalities;

namespace Formular_Specifications.Functionalities
{
    class FunctionOutputClass
    {
        public FunctionOutputClass() { }
        CheckVarTypeClass checkvartype = new CheckVarTypeClass();
        string nameofthread;
        public void SplitInput(List<string> ProgramContent, string NameofThread)
        {
            nameofthread = NameofThread;
            string[] SplitThread = nameofthread.Split(new[] { "(", ")" }, StringSplitOptions.None);
            string[] VarofOutput = SplitThread[2].Split(new[] { ":", "," }, StringSplitOptions.None);

            string OutputName = "\t\tpublic void Xuat" + SplitThread[0] + "(";
            OutputName += checkvartype.CheckVarType(VarofOutput[1]);
            OutputName += VarofOutput[0];
            OutputName += ")";
            ProgramContent.Add(OutputName);

            ProgramContent.Add("\t\t{");
            string OutputLine = "\t\t\tConsole.WriteLine(" + "\"Ket qua la:\" + " + VarofOutput[0] + ");";
            ProgramContent.Add(OutputLine);
            ProgramContent.Add("\t\t}");
        }
    }
}

