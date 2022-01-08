using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formular_Specifications.Functionalities
{
    class FunctionInputClass2
    {
        public FunctionInputClass2() { }
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
            ProgramContent.Add("\t\t\tfor(int i = 0; i < " + VarofInput[2] + "; i++)");
            ProgramContent.Add("\t\t\t{");
            ProgramContent.Add("\t\t\t\tConsole.Write(\"" + VarofInput[0] + "[ \" + i + \"] = \");");
            string temp = checkvartype.CheckVarType(VarofInput[1]).Replace("[]", string.Empty);
            ProgramContent.Add("\t\t\t\t" + VarofInput[0] + "[i] = " + temp + ".Parse(Console.ReadLine());" );
            ProgramContent.Add("\t\t\t}");
            ProgramContent.Add("\t\t}");




        }
    }
}
