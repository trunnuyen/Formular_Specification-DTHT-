using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Formular_Specifications.Functionalities;
using System.Text.RegularExpressions;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Diagnostics;

namespace Formular_Specifications
{
    public partial class Form1 : Form
    {
        string inputtext;
        string inputtextrpl;
        string[] inputtextspl;
        UndoRedoOperation undoRedoOperation;
        Timer timer;
        FunctionInputClass inputfunction = new FunctionInputClass();
        FunctionOutputClass outputfunction = new FunctionOutputClass();
        CheckConditionClass checkcondition = new CheckConditionClass();
        PostProcessingClass postprocessing = new PostProcessingClass();
        FunctionMainClass mainfunction = new FunctionMainClass();
        public Form1()
        {
            InitializeComponent();
            undoRedoOperation = new UndoRedoOperation();
            timer = new Timer();
            timer.Tick += Mytimer_Tick;
            timer.Interval = 500;
        }
        public List<string> GenerateProgram()
        {
            inputtext = richTextBox1.Text;
            inputtextrpl = inputtext.Replace(" ", string.Empty).Replace("\n", string.Empty).Replace("\t", string.Empty).Replace("\r", string.Empty);
            inputtextspl = inputtextrpl.Split(new[] { "pre", "post" }, StringSplitOptions.None);
            List<string> programContent = new List<string>();
            programContent.Add("using System;");
            programContent.Add("namespace FormalSpecification");
            programContent.Add("{");
            programContent.Add(string.Format("\tpublic class Program"));
            programContent.Add("\t{");
            inputfunction.SplitInput(programContent, inputtextspl[0]);
            outputfunction.SplitInput(programContent, inputtextspl[0]);
            checkcondition.SplitInput(programContent, inputtextspl[0], inputtextspl[1]);
            postprocessing.SplitInput(programContent, inputtextspl[0], inputtextspl[2]);
            mainfunction.SplitInput(programContent, inputtextspl[0]);
            programContent.Add("\t}");

            programContent.Add("}");

            return programContent;
        }


        private void Mytimer_Tick(object sender, EventArgs e)
        {
            timer.Stop();
            undoRedoOperation.Add_UndoRedo(richTextBox1.Text);
            UpdateView();
        }
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

            if (undoRedoOperation.RichTextBox1TextChangeRequired)
            {
                timer.Start();
            }
            else
            {
                undoRedoOperation.RichTextBox1TextChangeRequired = false;
            }
            UpdateView();
            //Highlight syntax
            string tokens = "(R|Z|B|char)";
            Regex rex = new Regex(tokens);
            MatchCollection mc = rex.Matches(richTextBox1.Text);
            int StartCursorPosition = richTextBox1.SelectionStart;
            foreach (Match m in mc)
            {
                int startIndex = m.Index;
                int StopIndex = m.Length;
                richTextBox1.Select(startIndex, StopIndex);
                richTextBox1.SelectionColor = Color.Red;
                richTextBox1.SelectionStart = StartCursorPosition;
                richTextBox1.SelectionColor = Color.Black;
            }
            string tokens2 = "(pre|post)";
            Regex rex2 = new Regex(tokens2);
            MatchCollection mc2 = rex2.Matches(richTextBox1.Text);
            int StartCursorPosition2 = richTextBox1.SelectionStart;
            foreach (Match m in mc2)
            {
                int startIndex = m.Index;
                int StopIndex = m.Length;
                richTextBox1.Select(startIndex, StopIndex);
                richTextBox1.SelectionColor = Color.Blue;
                richTextBox1.SelectionStart = StartCursorPosition;
                richTextBox1.SelectionColor = Color.Black;
            }

        }
        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked_1(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked_2(object sender, ToolStripItemClickedEventArgs e)
        {

        }
        //Open file 
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                richTextBox1.Text = File.ReadAllText(openFileDialog.FileName);
            }
            string fileName = Path.GetFileName(openFileDialog.FileName);
            textBox2.Text = fileName;
            richTextBox2.Clear();
            richTextBox3.Clear();
        }

        private void RichTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
        //New
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text != "")
            {
                DialogResult result = MessageBox.Show("Luu van ban?", "Thong Bao!", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                switch (result)
                {
                    case DialogResult.Cancel:
                        break;
                    case DialogResult.No:
                        richTextBox1.Clear();
                        richTextBox2.Clear();
                        richTextBox3.Clear();
                        textBox1.Clear();
                        textBox2.Clear();
                        break;
                    case DialogResult.Yes:
                        toolStripButton3_Click(sender, e);
                        richTextBox1.Clear();
                        richTextBox2.Clear();
                        richTextBox3.Clear();
                        textBox1.Clear();
                        textBox2.Clear();
                        break;
                    default:
                        break;
                }
            }
        }
        //Save
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            SaveFileDialog savefiledialog1 = new SaveFileDialog();
            savefiledialog1.Filter = "Txt Files(*.txt)|*.txt";
            if (savefiledialog1.ShowDialog() == DialogResult.OK)
            {
                using (Stream s = File.Open(savefiledialog1.FileName, FileMode.CreateNew))
                using (StreamWriter sw = new StreamWriter(s))
                {
                    sw.Write(richTextBox1.Text);
                }
            }
        }
        //new on menu
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButton1_Click(sender, e);
        }
        //open on menu       
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButton2_Click(sender, e);
        }
        //save on menu
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButton3_Click(sender, e);
        }
        //Exit on menu
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        //cut
        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            RichTextBox richTextBox = this.ActiveControl as RichTextBox;
            if (richTextBox.SelectedText != String.Empty)
                Clipboard.SetData(DataFormats.Text, richTextBox.SelectedText);
            richTextBox.SelectedText = String.Empty;
        }
        //copy
        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            RichTextBox richTextBox = this.ActiveControl as RichTextBox;
            if (richTextBox.SelectedText != String.Empty)
                Clipboard.SetData(DataFormats.Text, richTextBox.SelectedText);
            else
            {

            }
        }
        //paste
        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            int position = ((RichTextBox)this.ActiveControl).SelectionStart;
            this.ActiveControl.Text = this.ActiveControl.Text.Insert(position, Clipboard.GetText());
        }
        //cut on menu
        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButton4_Click(sender, e);
        }
        //copy on menu
        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButton5_Click(sender, e);
        }
        //paste on menu
        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButton6_Click(sender, e);
        }
        private void UpdateView()
        {
            toolStripButton7.Enabled = undoRedoOperation.CanUndo() ? true : false;
            toolStripButton8.Enabled = undoRedoOperation.CanRedo() ? true : false;
            undoToolStripMenuItem.Enabled = undoRedoOperation.CanUndo() ? true : false;
            redoToolStripMenuItem.Enabled = undoRedoOperation.CanRedo() ? true : false;
        }
        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = undoRedoOperation.UndoClicked();
            UpdateView();
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = undoRedoOperation.RedoClicked();
            UpdateView();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = undoRedoOperation.UndoClicked();
            UpdateView();
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = undoRedoOperation.RedoClicked();
            UpdateView();
        }



        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text != "" && richTextBox1.Text.Contains("pre") && richTextBox1.Text.Contains("post"))
            {
                richTextBox2.Clear();
                List<string> DataOutput = GenerateProgram();
                for (int i = 0; i < DataOutput.Count; i++)
                {
                    richTextBox2.Text += DataOutput[i] + "\n";
                    Console.WriteLine(DataOutput[i]);
                }
                textBox1.Text = "Program";

            }
            else
            {
                DialogResult result = MessageBox.Show("Input khong hop le.", "Thong bao!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {
            //Highlight syntax
            string tokens = "(int|string|return|float|void|static|public|ref|using|namespace|class|bool)";
            Regex rex = new Regex(tokens);
            MatchCollection mc = rex.Matches(richTextBox2.Text);
            int StartCursorPosition = richTextBox2.SelectionStart;
            foreach (Match m in mc)
            {
                int startIndex = m.Index;
                int StopIndex = m.Length;
                richTextBox2.Select(startIndex, StopIndex);
                richTextBox2.SelectionColor = Color.Blue;
                richTextBox2.SelectionStart = StartCursorPosition;
                richTextBox2.SelectionColor = Color.Black;
            }

            Regex rex2 = new Regex("\".*?\"");
            MatchCollection mc2 = rex2.Matches(richTextBox2.Text);
            int StartCursorPosition2 = richTextBox2.SelectionStart;
            foreach (Match m in mc2)
            {
                int startIndex = m.Index;
                int StopIndex = m.Length;
                richTextBox2.Select(startIndex, StopIndex);
                richTextBox2.SelectionColor = Color.Orange;
                richTextBox2.SelectionStart = StartCursorPosition;
                richTextBox2.SelectionColor = Color.Black;
            }
            string tokens2 = "(Program|Console)";
            Regex rex3 = new Regex(tokens2);
            MatchCollection mc3 = rex3.Matches(richTextBox2.Text);
            int StartCursorPosition3 = richTextBox2.SelectionStart;
            foreach (Match m in mc3)
            {
                int startIndex = m.Index;
                int StopIndex = m.Length;
                richTextBox2.Select(startIndex, StopIndex);
                richTextBox2.SelectionColor = Color.Green;
                richTextBox2.SelectionStart = StartCursorPosition;
                richTextBox2.SelectionColor = Color.Black;
            }
        }

        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            if (richTextBox1.Text != "")
            {
                DialogResult result = MessageBox.Show("Luu input?", "Thong Bao!", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                switch (result)
                {
                    case DialogResult.Cancel:
                        break;
                    case DialogResult.No:
                        richTextBox1.Text = "";
                        break;
                    case DialogResult.Yes:
                        toolStripButton3_Click(sender, e);
                        richTextBox1.Text = "";
                        break;
                    default:
                        break;
                }
            }
        }

        [Obsolete]
        private void button1_Click(object sender, EventArgs e)
        {
            richTextBox3.Clear();
            CSharpCodeProvider codeProvider = new CSharpCodeProvider();
            ICodeCompiler icc = codeProvider.CreateCompiler();
            string exe = "Program.exe";           
            System.CodeDom.Compiler.CompilerParameters parameters = new CompilerParameters();
            parameters.GenerateExecutable = true;
            parameters.OutputAssembly = exe;
            CompilerResults results = icc.CompileAssemblyFromSource(parameters, richTextBox2.Text);
            if (results.Errors.HasErrors)
            {
                richTextBox3.Text = "-----Build failed-----\n";
                results.Errors.Cast<CompilerError>().ToList().ForEach(error => richTextBox3.Text += error.ErrorText + "\r\n");
            }
            else
            {
                richTextBox3.Text = "-----Build succeeded-----";
                Process.Start(exe);

            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Formular Specifications Project\nUIT Duong Trung Nguyen 19520782", "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void toolsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButton9_Click(sender, e);
        }

        private void guideToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox2.SelectAll();
            richTextBox2.Focus();
            toolStripButton5_Click(sender, e);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void helpToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Formular Specifications Project\nUIT Duong Trung Nguyen 19520782", "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
