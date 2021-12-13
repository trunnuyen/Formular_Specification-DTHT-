using System;
using System.Collections.Generic;
using System.Text;
using Formular_Specifications.Functionalities;

namespace Formular_Specifications.Functionalities
{
    class FunctionInputClass
    {
        public FunctionInputClass() { }
        CheckVarTypeClass checkvartype = new CheckVarTypeClass();
        InputVarClass inputvar = new InputVarClass();
        string nameofthread;
        public void SplitInput(List<string> ProgramContent, string NameofThread)
        {
            nameofthread = NameofThread;
            string[] SplitThread = nameofthread.Split(new[] { "(", ")" }, StringSplitOptions.None);
            string[] VarofInput = SplitThread[1].Split(new[] { ":", "," }, StringSplitOptions.None);



            string InputName = "\t\tpublic void Nhap" + SplitThread[0] + "(";
            for (int i = 0; i < VarofInput.Length; i += 2)
            {
                InputName += "ref ";
                InputName += checkvartype.CheckVarType(VarofInput[i + 1]);
                InputName += inputvar.VarInput(VarofInput, i);
                /*if (VarofInput.Length <= 2)
                {
                    InputName += VarofInput[0];
                }
                else
                {
                    if (i < VarofInput.Length - 2)
                    {
                        InputName += VarofInput[i];
                        InputName += " , ";
                    }
                    else
                    {
                        InputName += VarofInput[i];
                    }
                }*/

            }
            InputName += ")";
            ProgramContent.Add(InputName);
            ProgramContent.Add("\t\t{");


            for (int i = 0; i < VarofInput.Length; i += 2)
            {
                string InputLine = "\t\t\tConsole.WriteLine(" + "\"Nhap " + VarofInput[i] + ":\");";
                ProgramContent.Add(InputLine);
                string InputParse = "\t\t\t" + VarofInput[i] + " = ";

                if (VarofInput[i + 1] == "R")
                {
                    InputParse += "float.Parse(Console.ReadLine());";
                }
                else if (VarofInput[i + 1] == "Z")
                {
                    InputParse += "int.Parse(Console.ReadLine());";
                }
                else if (VarofInput[i + 1] == "B")
                {
                    InputParse += "bool.Parse(Console.ReadLine());";
                }
                else if (VarofInput[i + 1] == "char*")
                {
                    InputParse += "Console.ReadLine();";
                }
                ProgramContent.Add(InputParse);
            }
            ProgramContent.Add("\t\t}");




        }
    }
}

