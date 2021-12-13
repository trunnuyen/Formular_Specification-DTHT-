using System;
using System.Collections.Generic;
using System.Text;

namespace Formular_Specifications.Functionalities
{
    class InputVarClass
    {
        public InputVarClass() { }
        public string InputName;
        public string[] VarofInput;
        public string VarInput(string[] varofinput, int i)
        {

            VarofInput = varofinput;
            if (VarofInput.Length <= 2)
            {
                InputName = VarofInput[0];
            }
            else
            {
                if (i < VarofInput.Length - 2)
                {
                    InputName = VarofInput[i] + " , ";

                }
                else
                {
                    InputName = VarofInput[i];
                }
            }
            return InputName;
        }
    }
}

