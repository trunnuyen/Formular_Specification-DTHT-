using System;
using System.Collections.Generic;
using System.Text;

namespace Formular_Specifications.Functionalities
{
    class CheckVarTypeClass
    {
        public string input;
        public string var;
        public CheckVarTypeClass() { }
        public string CheckVarType(string Var)
        {

            if (Var == "R")
            {
                var = "float ";
            }
            else if (Var == "Z" || Var == "N")
            {
                var = "int ";
            }
            else if (Var == "B")
            {
                var = "bool ";
            }
            else if (Var == "char*")
            {
                var = "string ";
            }
            else if (Var == "R*")
            {
                var = "float[] ";
            }
            return var;
        }
        public string CreateVarType(string vartype, string varname)
        {
            if (vartype == "R")
            {
                input = "float " + varname + "= 0;";
            }
            else if (vartype == "Z" || vartype == "N")
            {
                input = "int " + varname + "= 0;";
            }
            else if (vartype == "B")
            {
                input = "bool " + varname + "= true;";
            }
            else if (vartype == "char*")
            {
                input = "string " + varname + "= \"\";";
            }
            return input;
        }
    }
}

