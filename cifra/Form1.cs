using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cifra
{
    public partial class Form1 : Form
    {
        private ImportadorWord Importador;
        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog.ShowDialog();

            if (result.Equals(DialogResult.OK))
            {
                try
                {
                    Cursor.Current = Cursors.WaitCursor;
                    tbNomeArquivo.Text = openFileDialog.FileName;
                    Importador = new ImportadorWord(openFileDialog.FileName);
                }
                finally
                {
                    Cursor.Current = Cursors.Default;
                }
            }
            else
            {
                tbNomeArquivo.Text = "";
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (Importador != null)
            {
                try
                {
                    Cursor.Current = Cursors.WaitCursor;
                    int semiton = Convert.ToInt32(tbNumeroSemiTons.Text);
                    Importador.SubirTom(semiton, @"D:\OneDrive\projetos\cifra\cifra\teste.docx");
                }
                finally
                {
                    Cursor.Current = Cursors.Default;
                }
            }
        }

        private void RemoveDuplicidade()
        {

            Dictionary<string, string> listA = new Dictionary<string, string>();
            using (var reader = new StreamReader(@"D:\OneDrive\projetos\cifra\cifra\dic-train.csv"))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    try
                    {
                        listA.Add(values[0], values[1]);
                    }
                    catch (ArgumentException)
                    {
                        Console.WriteLine("{0} -> {1} * corrigindo...", values[0], listA[values[0]]);
                        listA[values[0]] = values[1];
                        Console.WriteLine("{0} -> {1} * corrigindo!", values[0], listA[values[0]]);
                    }
                }
            }

            using (var writer = new StreamWriter(@"D:\OneDrive\projetos\cifra\cifra\dic-final-train.csv"))
            {
                foreach (var key in listA.Keys)
                {
                    writer.WriteLine("{0},{1}", key, listA[key]);
                }
            }
        }

    }
}
