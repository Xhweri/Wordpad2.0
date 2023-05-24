using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Labb3
{
    public partial class Form1 : Form
    {
        bool osparad = false;   
        string filepath = string.Empty;
        bool abortMisson=false;
        bool sparaClicked = false;
        public Form1()
        {

            InitializeComponent();
        }

        private void menyToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void radera_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
        }

        private void sparaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (!File.Exists(filepath))
                {
                    SaveFileDialog saveFileDialog1 = new SaveFileDialog(); //Skapar Spara rutan i windows.
                    saveFileDialog1.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";//Egenskap av Savefile som filtrerar de slags filer användaren kan se i rutan.         
                    saveFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);//Den standard Pathen för windows operativsystem till dokument,Rutan börjar där.

                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)//Kollar om användaren tryckte på okej eller avbryt i rutan. ok tryckt händer följande..
                    {
                        using (StreamWriter sw = new StreamWriter(saveFileDialog1.FileName))
                        {
                            filepath = saveFileDialog1.FileName;
                            sw.Write(textBox1.Text);
                            ActiveForm.Text = Path.GetFileName(filepath);
                            sparaClicked = true;
                        }
                    }
                }
                ActiveForm.Text = Path.GetFileName(filepath);


            }
            catch
            {
                MessageBox.Show("Filen kunde ej sparas!");
                
            }
        }

        private void sparaSomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog(); //Skapar Spara rutan i windows.
            saveFileDialog1.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";//Egenskap av Savefile som filtrerar de slags filer användaren kan se i rutan.         
            saveFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);//Den standard Pathen för windows operativsystem till dokument,Rutan börjar där.

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)//Kollar om användaren tryckte på okej eller avbryt i rutan. ok tryckt händer följande..
            {
                using (StreamWriter sw = new StreamWriter(saveFileDialog1.FileName))
                {
                    filepath = saveFileDialog1.FileName;
                    sw.Write(textBox1.Text);
                    ActiveForm.Text = Path.GetFileName(filepath);
                    sparaClicked = true;
                }
            }
        }


        private void öppnaNyToolStripMenuItem_Click(object sender, EventArgs e)
        {            try
            {                abortMisson = false;
                if (ActiveForm.Text.Contains("*"))
                {
var unSaved = MessageBox.Show("Du har inte sparat din fil vill du fortsätta?",
                        "Spara",
                        MessageBoxButtons.YesNoCancel,
                        MessageBoxIcon.Warning
                        );
            switch(unSaved)
                    {
                        case DialogResult.Yes:
                            abortMisson = false;
                            break;
                        case DialogResult.No:
                            abortMisson = true;

                            SaveFileDialog saveFileDialog1 = new SaveFileDialog(); //Skapar Spara rutan i windows.
                            saveFileDialog1.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";//Egenskap av Savefile som filtrerar de slags filer användaren kan se i rutan.         
                            saveFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);//Den standard Pathen för windows operativsystem till dokument,Rutan börjar där.

                            if (saveFileDialog1.ShowDialog() == DialogResult.OK)//Kollar om användaren tryckte på okej eller avbryt i rutan. ok tryckt händer följande..
                            {
                                using (StreamWriter sw = new StreamWriter(saveFileDialog1.FileName))
                                {
                                    sw.Write(textBox1.Text);
                                    osparad = false;
                                }
                            }

                            break;
                            case DialogResult.Cancel:
                            return;
                            break;


                    }
                }
                
                if (abortMisson == false)
                {
                    OpenFileDialog opna = new OpenFileDialog();
                    if (opna.ShowDialog() == DialogResult.OK && abortMisson == false)
                    {
                        filepath = opna.FileName;
                        textBox1.Text = File.ReadAllText(filepath);
                        ActiveForm.Text = Path.GetFileName(filepath);
                        osparad= false;
                    }
                }




            }
            catch
            {
                MessageBox.Show("Programmet fick ett avbrott!");
            }
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (sparaClicked)
            {
                ActiveForm.Text = Path.GetFileName(filepath);
                sparaClicked= false;
            }
            
            if (File.Exists(filepath))
            {
                ActiveForm.Text = "* " + Path.GetFileName(filepath);
                osparad = true;
            }
            else
            {
                string nyForm = "* ";
                ActiveForm.Text = nyForm + "namnlös.txt";
            }

        }

        
    }
}
