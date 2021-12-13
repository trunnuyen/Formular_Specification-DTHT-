using System;
using System.Collections.Generic;
using System.Text;
using Formular_Specifications.Functionalities;

namespace Formular_Specifications.Functionalities
{
    class CheckConditionClass
    {
        public CheckConditionClass() { }
        CheckVarTypeClass checkvartype = new CheckVarTypeClass();
        InputVarClass inputvar = new InputVarClass();
        string nameofthread;
        string precondition;
        public void SplitInput(List<string> ProgramContent, string NameofThread, string PreCondition)
        {
            nameofthread = NameofThread;
            precondition = PreCondition;
            string[] SplitThread = nameofthread.Split(new[] { "(", ")" }, StringSplitOptions.None);
            string[] VarofInput = SplitThread[1].Split(new[] { ":", "," }, StringSplitOptions.None);

            string InputName = "\t\tpublic int Kiemtra" + SplitThread[0] + "(";
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
            string precontent = precondition.Replace("=", "==").Replace("TRUE", "true").Replace("FALSE", "false").Replace(">==", ">=").Replace("<==", "<=").Replace("!==", "!=");
            if (precontent == "")
            {
                ProgramContent.Add("\t\t\treturn 1;");
            }
            else
            {
                ProgramContent.Add("\t\t\tif(" + precontent + ")");
                ProgramContent.Add("\t\t\t{");
                ProgramContent.Add("\t\t\t\treturn 1;");
                ProgramContent.Add("\t\t\t}");
                ProgramContent.Add("\t\t\telse");
                ProgramContent.Add("\t\t\t{");
                ProgramContent.Add("\t\t\t\treturn 0;");
                ProgramContent.Add("\t\t\t}");
            }

            ProgramContent.Add("\t\t}");
        }
    }
}

