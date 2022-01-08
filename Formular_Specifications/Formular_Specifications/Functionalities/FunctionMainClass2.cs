using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formular_Specifications.Functionalities
{
    class FunctionMainClass2
    {
        public FunctionMainClass2() { }
        CheckVarTypeClass checkvartype = new CheckVarTypeClass();
        InputVarClass inputvar = new InputVarClass();
        string nameofthread;
        public void SplitInput(List<string> ProgramContent, string NameofThread)
        {
            nameofthread = NameofThread;
            string[] SplitThread = nameofthread.Split(new[] { "(", ")" }, StringSplitOptions.None);
            string[] VarofInput = SplitThread[1].Split(new[] { ":", "," }, StringSplitOptions.None);
            string[] VarofResult = SplitThread[2].Split(new[] { ":", "," }, StringSplitOptions.None);
            ProgramContent.Add("\t\tpublic static void Main(string[] args)");
            ProgramContent.Add("\t\t{");
            ProgramContent.Add("\t\t\tConsole.WriteLine(\"Nhap " + VarofInput[2] + ":\" );");
            string InputParse = "\t\t\t" + checkvartype.CheckVarType(VarofInput[3]) + VarofInput[2] + " = ";

            if (VarofInput[3] == "R")
            {
                InputParse += "float.Parse(Console.ReadLine());";
            }
            else if (VarofInput[3] == "Z" || VarofInput[3] == "N")
            {
                InputParse += "int.Parse(Console.ReadLine());";
            }
            else if (VarofInput[3] == "B")
            {
                InputParse += "bool.Parse(Console.ReadLine());";
            }
            else if (VarofInput[3] == "char*")
            {
                InputParse += "Console.ReadLine();";
            }
            ProgramContent.Add(InputParse);
            ProgramContent.Add("\t\t\t"+checkvartype.CheckVarType(VarofInput[1]) + VarofInput[0] + " = new " + checkvartype.CheckVarType(VarofInput[1]).Replace("[]",string.Empty) + "[" + VarofInput[2] + "];");
            
            
            
            
                     
            ProgramContent.Add("\t\t\t" + checkvartype.CreateVarType(VarofResult[1], VarofResult[0]));                      
            ProgramContent.Add("\t\t\tProgram p = new Program();");
            string MainInput = "\t\t\tp.Nhap" + SplitThread[0] + "(";
            string MainCheck = "\t\t\tif( p.Kiemtra" + SplitThread[0] + "(";
            string MainPost = "\t\t\t\t" + VarofResult[0] + "= p." + SplitThread[0] + "(";
            string MainOutput = "\t\t\t\tp.Xuat" + SplitThread[0] + "(" + VarofResult[0] + ");";
            for (int i = 0; i < VarofInput.Length; i += 2)
            {
                

                MainInput += inputvar.VarInput(VarofInput, i);
                MainCheck += inputvar.VarInput(VarofInput, i);
                MainPost += inputvar.VarInput(VarofInput, i);
            }
            MainInput += ");";
            MainCheck += ") == 1)";
            MainPost += ");";
            ProgramContent.Add(MainInput);
            ProgramContent.Add(MainCheck);
            ProgramContent.Add("\t\t\t{");
            ProgramContent.Add(MainPost);
            ProgramContent.Add(MainOutput);
            ProgramContent.Add("\t\t\t}");
            ProgramContent.Add("\t\t\telse");
            ProgramContent.Add("\t\t\t\tConsole.WriteLine(\"Thong tin nhap khong hop le!\");");
            ProgramContent.Add("\t\t\tConsole.ReadLine();");
            ProgramContent.Add("\t\t}");
        }
    }
}
