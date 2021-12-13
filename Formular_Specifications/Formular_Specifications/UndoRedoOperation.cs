using Formular_Specifications.Functionalities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formular_Specifications
{
    public class UndoRedoOperation
    {
        private UndoRedoClass data;
        private bool richTextBox1TextChangeRequired = true;
        public UndoRedoOperation()
        {
            data = new UndoRedoClass();
        }

        public bool RichTextBox1TextChangeRequired
        {
            get
            {
                return richTextBox1TextChangeRequired;
            }

            set
            {
                richTextBox1TextChangeRequired = value;
            }
        }

        public string DateTime_Now()
        {
            return DateTime.Now.ToString();
        }

        public string UndoClicked()
        {
            RichTextBox1TextChangeRequired = false;
            return data.Undo();
        }

        public string RedoClicked()
        {
            RichTextBox1TextChangeRequired = false;
            return data.Redo();
        }

        public void Add_UndoRedo(string item)
        {
            data.AddItem(item);
        }

        public bool CanUndo()
        {
            return data.CanUndo();
        }

        public bool CanRedo()
        {
            return data.CanRedo();
        }
    }
}
