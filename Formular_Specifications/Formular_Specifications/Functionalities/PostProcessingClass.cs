using System;
using System.Collections.Generic;
using System.Text;

namespace Formular_Specifications.Functionalities
{
    class PostProcessingClass
    {
        public PostProcessingClass() { }
        CheckVarTypeClass checkvartype = new CheckVarTypeClass();
        InputVarClass inputvar = new InputVarClass();
        string nameofthread;
        string postcondition;
        public void SplitInput(List<string> ProgramContent, string NameofThread, string PostCondition)
        {
            nameofthread = NameofThread;
            postcondition = PostCondition;
            string[] SplitThread = nameofthread.Split(new[] { "(", ")" }, StringSplitOptions.None);
            string[] VarofInput = SplitThread[1].Split(new[] { ":", "," }, StringSplitOptions.None);
            string[] VarofResult = SplitThread[2].Split(new[] { ":", "," }, StringSplitOptions.None);

            string InputName = "\t\tpublic " + checkvartype.CheckVarType(VarofResult[1]) + " " + SplitThread[0] + "(";
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
            ProgramContent.Add("\t\t\t" + checkvartype.CreateVarType(VarofResult[1], VarofResult[0]));
            string postcontent = postcondition.Replace("(", string.Empty).Replace(")", string.Empty);
            string[] splitpostcontent = postcontent.Split(new[] { "||" }, StringSplitOptions.None);
            for (int i = 0; i < splitpostcontent.Length; i += 1)
            {
                string[] splitpostcondition = splitpostcontent[i].Split(new[] { "&&" }, StringSplitOptions.None);
                if (splitpostcondition.Length <= 1)
                {
                    string splcdtn = splitpostcondition[0].Replace("TRUE", "true").Replace("FALSE", "false");
                    ProgramContent.Add("\t\t\t" + splcdtn + ";");
                }
                else
                {

                    string IfContent = "\t\t\tif(";
                    for (int j = 1; j < splitpostcondition.Length; j += 1)
                    {
                        if (splitpostcondition.Length <= 2)
                        {
                            IfContent += splitpostcondition[1];
                        }
                        else
                        {

                            if (j < splitpostcondition.Length - 1)
                            {
                                IfContent += splitpostcondition[j];
                                IfContent += "&&";
                            }
                            else
                            {
                                IfContent += splitpostcondition[j];
                            }
                        }
                    }

                    IfContent += ")";

                    string ifcontent = IfContent.Replace("=", "==").Replace("TRUE", "true").Replace("FALSE", "false").Replace(">==", ">=").Replace("<==", "<=").Replace("!==", "!=");
                    ProgramContent.Add(ifcontent);

                    ProgramContent.Add("\t\t\t{");
                    string splcdtn = splitpostcondition[0].Replace("TRUE", "true").Replace("FALSE", "false");
                    ProgramContent.Add("\t\t\t\t" + splcdtn + ";");
                    ProgramContent.Add("\t\t\t}");
                }

            }
            ProgramContent.Add("\t\t\treturn " + VarofResult[0] + ";");
            ProgramContent.Add("\t\t}");
        }
    }
}
