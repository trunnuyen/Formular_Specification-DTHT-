using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formular_Specifications.Functionalities
{
    class PostProcessingClass2
    {
        public PostProcessingClass2() { }
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
			string[] postcontent = postcondition.Split(new[] { "}." }, StringSplitOptions.None);
			
			if (postcontent.Length <= 2)
			{
				string[] temp1 = postcontent[0].Split(new[] { "(", "{" }, StringSplitOptions.None);
				string[] temp2 = temp1[2].Split(new[] { ".." }, StringSplitOptions.None);
				string temp4 = postcontent[1].Replace("(", "[").Replace(")", "]").Replace("]]", "]");
				
				if (temp1[1].Contains("VM"))
				{
					string temp3 = temp1[1].Replace("VM", string.Empty).Replace("TH", string.Empty);
					ProgramContent.Add("\t\t\tfor(int " + temp3 + " = " + temp2[0] + "; " + temp3 + " < " + temp2[1] + "; " + temp3 + "++)");
					ProgramContent.Add("\t\t\t{");
					ProgramContent.Add("\t\t\t\tif(" + temp4 + ")");
					ProgramContent.Add("\t\t\t\t\t" + VarofResult[0] + " = true;");
					ProgramContent.Add("\t\t\t\telse");
					ProgramContent.Add("\t\t\t\t\t" + VarofResult[0] + " = false;");
					ProgramContent.Add("\t\t\t}");
				}
				else if (temp1[1].Contains("TT"))
				{
					string temp3 = temp1[1].Replace("TT", string.Empty).Replace("TH", string.Empty);
					ProgramContent.Add("\t\t\tint count = 0;");
					ProgramContent.Add("\t\t\tfor(int " + temp3 + " = " + temp2[0] + "; " + temp3 + " < " + temp2[1] + "; " + temp3 + "++)");
					ProgramContent.Add("\t\t\t{");
					ProgramContent.Add("\t\t\t\tif(" + temp4 + ")");
					ProgramContent.Add("\t\t\t\t\tcount ++;" );
					ProgramContent.Add("\t\t\t}");
					ProgramContent.Add("\t\t\tif(count >= 1)");
					ProgramContent.Add("\t\t\t\t\t" + VarofResult[0] + " = true;");
					ProgramContent.Add("\t\t\telse");
					ProgramContent.Add("\t\t\t\t\t" + VarofResult[0] + " = false;");
				}
			}
			else if (postcontent.Length > 2)
			{
				string[] temp1 = postcontent[0].Split(new[] { "(", "{" }, StringSplitOptions.None);
				string[] temp2 = postcontent[1].Split(new[] {"{" }, StringSplitOptions.None);
				string[] temp11 = temp1[2].Split(new[] { ".." }, StringSplitOptions.None);
				string[] temp22 = temp2[1].Split(new[] { ".." }, StringSplitOptions.None);
				string temp4 = postcontent[2].Replace("(", "[").Replace(")", "]").Replace("]]", "]");

				if (temp1[1].Contains("VM") && temp2[0].Contains("VM"))
				{
					string temp3 = temp1[1].Replace("VM", string.Empty).Replace("TH", string.Empty);
					string temp33 = temp2[0].Replace("VM", string.Empty).Replace("TH", string.Empty);
					ProgramContent.Add("\t\t\tint count = 0;");
					ProgramContent.Add("\t\t\tfor(int " + temp3 + " = " + temp11[0] + "; " + temp3 + " < " + temp11[1] + "; " + temp3 + "++)");
					ProgramContent.Add("\t\t\t{");
					ProgramContent.Add("\t\t\t\tfor(int " + temp33 + " = " + temp22[0] + "; " + temp33 + " < " + temp22[1] + "; " + temp33 + "++)");
					ProgramContent.Add("\t\t\t\t{");
					ProgramContent.Add("\t\t\t\t\tif(" + temp4 + ")");
					ProgramContent.Add("\t\t\t\t\t\tcount ++;");
					ProgramContent.Add("\t\t\t\t}");
					ProgramContent.Add("\t\t\t}");
					ProgramContent.Add("\t\t\tif(count >= n - 1)");
					ProgramContent.Add("\t\t\t\t\t" + VarofResult[0] + " = true;");
					ProgramContent.Add("\t\t\telse");
					ProgramContent.Add("\t\t\t\t\t" + VarofResult[0] + " = false;");
				}
				else if (temp1[1].Contains("VM") && temp2[0].Contains("TT"))
				{
					string temp3 = temp1[1].Replace("VM", string.Empty).Replace("TH", string.Empty);
					string temp33 = temp2[0].Replace("TT", string.Empty).Replace("TH", string.Empty);
					ProgramContent.Add("\t\t\tint count = 0;");
					ProgramContent.Add("\t\t\tfor(int " + temp3 + " = " + temp11[0] + "; " + temp3 + " < " + temp11[1] + "; " + temp3 + "++)");
					ProgramContent.Add("\t\t\t{");
					ProgramContent.Add("\t\t\t\tfor(int " + temp33 + " = " + temp22[0] + "; " + temp33 + " < " + temp22[1] + "; " + temp33 + "++)");
					ProgramContent.Add("\t\t\t\t{");
					ProgramContent.Add("\t\t\t\t\tif(" + temp4 + ")");
					ProgramContent.Add("\t\t\t\t\t\tcount ++;");
					ProgramContent.Add("\t\t\t\t}");
					ProgramContent.Add("\t\t\t}");
					ProgramContent.Add("\t\t\tif(count >= n - 1)");
					ProgramContent.Add("\t\t\t\t\t" + VarofResult[0] + " = true;");
					ProgramContent.Add("\t\t\telse");
					ProgramContent.Add("\t\t\t\t\t" + VarofResult[0] + " = false;");
				}
				else if (temp1[1].Contains("TT") && temp2[0].Contains("VM"))
				{
					string temp3 = temp1[1].Replace("TT", string.Empty).Replace("TH", string.Empty);
					string temp33 = temp2[0].Replace("VM", string.Empty).Replace("TH", string.Empty);
					ProgramContent.Add("\t\t\tint count = 0;");
					ProgramContent.Add("\t\t\tfor(int " + temp3 + " = " + temp11[0] + "; " + temp3 + " < " + temp11[1] + "; " + temp3 + "++)");
					ProgramContent.Add("\t\t\t{");
					ProgramContent.Add("\t\t\t\tfor(int " + temp33 + " = " + temp22[0] + "; " + temp33 + " < " + temp22[1] + "; " + temp33 + "++)");
					ProgramContent.Add("\t\t\t\t{");
					ProgramContent.Add("\t\t\t\t\tif(" + temp4 + ")");
					ProgramContent.Add("\t\t\t\t\t\tcount ++;");
					ProgramContent.Add("\t\t\t\t}");
					ProgramContent.Add("\t\t\t}");
					ProgramContent.Add("\t\t\tif(count >= n - 1)");
					ProgramContent.Add("\t\t\t\t\t" + VarofResult[0] + " = true;");
					ProgramContent.Add("\t\t\telse");
					ProgramContent.Add("\t\t\t\t\t" + VarofResult[0] + " = false;");
				}
				else if (temp1[1].Contains("TT") && temp2[0].Contains("TT"))
				{
					string temp3 = temp1[1].Replace("TT", string.Empty).Replace("TH", string.Empty);
					string temp33 = temp2[0].Replace("TT", string.Empty).Replace("TH", string.Empty);
					ProgramContent.Add("\t\t\tint count = 0;");
					ProgramContent.Add("\t\t\tfor(int " + temp3 + " = " + temp11[0] + "; " + temp3 + " < " + temp11[1] + "; " + temp3 + "++)");
					ProgramContent.Add("\t\t\t{");
					ProgramContent.Add("\t\t\t\tfor(int " + temp33 + " = " + temp22[0] + "; " + temp33 + " < " + temp22[1] + "; " + temp33 + "++)");
					ProgramContent.Add("\t\t\t\t{");
					ProgramContent.Add("\t\t\t\t\tif(" + temp4 + ")");
					ProgramContent.Add("\t\t\t\t\t\tcount ++;");
					ProgramContent.Add("\t\t\t\t}");
					ProgramContent.Add("\t\t\t}");
					ProgramContent.Add("\t\t\tif(count >= 1)");
					ProgramContent.Add("\t\t\t\t\t" + VarofResult[0] + " = true;");
					ProgramContent.Add("\t\t\telse");
					ProgramContent.Add("\t\t\t\t\t" + VarofResult[0] + " = false;");
				}
			}
			/*for (int i = 0; i < postcontent.Length; i += 1)
			{
				string[] splitpostcondition = postcontent[i].Split(new[] { "&&" }, StringSplitOptions.None);
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

			}*/
			ProgramContent.Add("\t\t\treturn " + VarofResult[0] + ";");
			ProgramContent.Add("\t\t}");
		}
    }
}
