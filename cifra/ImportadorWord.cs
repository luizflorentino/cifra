using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Cifra
{
    class ImportadorWord
    {
        public CampoHarmonico CampoHarmonico { get; set; }

        private object Missing = System.Reflection.Missing.Value;
        public string ArquivoMusica { get; set; }

        private Document Doc;

        public ImportadorWord(string fileName)
        {
            ArquivoMusica = fileName;
            Console.WriteLine("Avaliando documento...");
            AbrirFecharDoc(
                () => {
                    var range = Doc.Range();
                    range.Find.ClearFormatting();
                    range.Find.Replacement.ClearFormatting();

                    CampoHarmonico = CampoHarmonico.DescobreCampoHarmonico(DescobreLinhasDeCifra(Doc));
                    Console.WriteLine("Musica na tonalidade: {0}", CampoHarmonico.Grau_1());
                }
            );
            Console.WriteLine("Fim da avaliação.");
        }

        private Dictionary<string, bool> DescobreCifras()
        {
            Dictionary<string, bool> dic = new Dictionary<string, bool>();

            using (StreamWriter file = new StreamWriter("myfile.txt"))
            {
                for (int i = 1; i <= Doc.Paragraphs.Count; i++)
                {
                    string line = Doc.Paragraphs[i].Range.Text.TrimEnd();
                    bool chordLine = Acorde.IsChordLine(line);
                    if (chordLine)
                    {
                        string aux = Regex.Replace(line, " {2,}", " ").Trim();
                        string[] tokens = aux.Split(' ');
                        foreach (var token in tokens)
                        {
                            try
                            {
                                dic.Add(token, Acorde.IsChord(token));
                                file.WriteLine("{0};{1}", token, dic[token]);
                            }
                            catch (ArgumentException) { }
                        }
                    }
                }
            }
            return dic;
        }

        public void SalvarPDF(string fileName)
        {
            Console.WriteLine("Salvando PDF...");
            AbrirFecharDoc(() => {
                Doc.ExportAsFixedFormat(fileName, WdExportFormat.wdExportFormatPDF, false, WdExportOptimizeFor.wdExportOptimizeForOnScreen,
                                        WdExportRange.wdExportAllDocument, 1, 1, WdExportItem.wdExportDocumentContent, true, true,
                                        WdExportCreateBookmarks.wdExportCreateHeadingBookmarks, true, true, false, Missing);
            });
            Console.WriteLine("Docx salvo!");
        }

        public void SubirTom(int semitons, string fileName)
        {
            Console.WriteLine("Subindo {0} semitons...", semitons);
            AbrirFecharDoc(() => {
                foreach (var rangeAlterar in DescobreLinhasDeCifra(Doc))
                {
                    rangeAlterar.Text = Acorde.SubirLinha(rangeAlterar.Text, semitons);
                }
                CampoHarmonico = CampoHarmonico.DescobreCampoHarmonico(DescobreLinhasDeCifra(Doc));
                Console.WriteLine("Musica em: {0}", CampoHarmonico.Grau_1());
                Console.WriteLine("Salvando DOCX...");
                Doc.SaveAs(fileName, Missing, ref Missing, ref Missing, ref Missing, ref Missing, ref Missing, ref Missing, ref Missing, ref Missing, ref Missing, ref Missing, ref Missing, ref Missing, ref Missing, ref Missing);
                Console.WriteLine("Docx salvo!");
            });

        }

        private void AbrirFecharDoc(Action block)
        {
            string templateEditable = @"D:\OneDrive\projetos\cifra\cifra\modelo-editavel.docx";
            File.Copy(ArquivoMusica, templateEditable, true);

            Application app = new Application
            {
                ShowAnimation = false,
                Visible = false
            };

            Doc = app.Documents.Open(templateEditable, Missing, true);

            try
            {
                block();
            }
            finally
            {
                Doc.Close(false, Missing, Missing);
                app.Quit(false, false, false);
                Marshal.ReleaseComObject(app);
            }
        }

        private List<Range> DescobreLinhasDeCifra(Document doc)
        {
            List<Range> linhasDeCifra = new List<Range>();

            for (int i = 1; i <= doc.Paragraphs.Count; i++)
            {
                string temp = doc.Paragraphs[i].Range.Text.TrimEnd();
                if (!string.IsNullOrEmpty(temp) && Acorde.IsChordLine(temp))
                {
                    doc.Paragraphs[i].Range.Text = temp + "\n";
                    linhasDeCifra.Add(doc.Paragraphs[i].Range);
                }
            }

            return linhasDeCifra;
        }

    }

}
