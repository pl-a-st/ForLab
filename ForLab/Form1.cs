using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;

namespace ForLab
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void butModifyDoc_Click(object sender, EventArgs e)
        {
            ProgressBar progressBar = new ProgressBar();
            Label label = new Label();
            InsertControls(progressBar, label);
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "PDF files(*.pdf)|*.pdf";
            if (fileDialog.ShowDialog() != DialogResult.OK)
            {
                progressBar.Dispose();
                label.Dispose();
                return;
            }
            string inputFile = fileDialog.FileName;
            string outputFile = fileDialog.FileName.Replace(".pdf", " ВАСТ.pdf");
            if (IsFileLocked(new FileInfo(outputFile)))
            {
                MessageBox.Show("Данное свидетельство уже модифицировалось и итоговый файл открыт, для перезаписи необходимо закрыть файл и выбрать его снова");
                progressBar.Dispose();
                label.Dispose();
                return;
            }
            using (var reader = new PdfReader(inputFile))
            {
                using (var fileStream = new FileStream(outputFile, FileMode.Create, FileAccess.Write))
                {
                    var document = new Document(reader.GetPageSizeWithRotation(1));
                    var writer = PdfWriter.GetInstance(document, fileStream);
                    document.Open();
                    for (var i = 1; i <= reader.NumberOfPages; i++)
                    {
                        document.NewPage();
                        InsertRegNum(reader, writer, i);
                        InsertPosition(reader, writer, i);
                        InsertNameHeadMetrology(reader, writer, i);
                        InsertWorkSymbol(reader, writer);
                    }
                    document.Close();
                    writer.Close();
                }
            }
            await Task.Factory.StartNew(()=>System.Diagnostics.Process.Start(outputFile));

            await Task.Delay(2000);
            label.Dispose();
            progressBar.Dispose();
        }

        private void InsertControls(ProgressBar progressBar, Label label)
        {
            progressBar.Width = butModifyDoc.Width * 2;
            progressBar.Height = butModifyDoc.Height * 2;

            progressBar.Location = new Point(
                x: butModifyDoc.Location.X + butModifyDoc.Width / 2 - progressBar.Width / 2,
                y: butModifyDoc.Location.Y + butModifyDoc.Height / 2 - progressBar.Height / 2);
            progressBar.Style = ProgressBarStyle.Marquee;
            progressBar.MarqueeAnimationSpeed = 30;
            label.AutoSize = true;
            label.Text = "Идет модификация свидетельства!";

            label.Location = new Point(
                x: butModifyDoc.Location.X + butModifyDoc.Width / 2 - label.PreferredWidth / 2,
                y: butModifyDoc.Location.Y + butModifyDoc.Height / 2 - label.Height / 2);
            this.Controls.Add(progressBar);
            this.Controls.Add(label);
            progressBar.BringToFront();
            label.BringToFront();
        }

        private void InsertWorkSymbol(PdfReader reader, PdfWriter writer)
        {
            if (!chkIsElectronic.Checked)
            {
                return;
            }
            iTextSharp.text.Image imgfoot = iTextSharp.text.Image.GetInstance(Properties.Resources.Первичная_поверка, System.Drawing.Imaging.ImageFormat.Png);
            if (cboTypeWork.SelectedItem.ToString().ToLower().Contains("первичная"))
            {
                imgfoot.ScaleAbsolute(60, 35);
            }
            if (cboTypeWork.SelectedItem.ToString().ToLower().Contains("периодическая"))
            {
                imgfoot.ScaleAbsolute(32, 35);
            }
            PDFTextExtractionStrategy.InsertImageToPDF(
                imgfoot,
                "Знак поверки:",
                numEntryTargetString: 1,
                reader,
                writer,
                pageNum: 1,
                LocationToInsert.RB,
                offsetInsertion: new Point(2, -5)
                );
        }

        private static void InsertNameHeadMetrology(PdfReader reader, PdfWriter writer, int i)
        {
            PDFTextExtractionStrategy.InsetStringToPDF(
                stringToInsert: Constatnts.NameHeadMetrologist,
                targetString: Constatnts.TargetStrForNameHead,
                numEntryTargetString: 2,
                reader,
                writer,
                pageNum: i,
                LocationToInsert.LT,
                offsetInsertion: new Point(x: 0, y: 10));
        }

        private static void InsertPosition(PdfReader reader, PdfWriter writer, int i)
        {
            PDFTextExtractionStrategy.InsetStringToPDF(
                stringToInsert: Constatnts.PositionHeadMetrologist,
                targetString: Constatnts.TargetStrForPosHead,
                numEntryTargetString: 1,
                reader,
                writer,
                pageNum: i,
                LocationToInsert.LT,
                offsetInsertion: new Point(x: 0, y: 10));
        }

        private static void InsertRegNum(PdfReader reader, PdfWriter writer, int i)
        {
            PDFTextExtractionStrategy.InsetStringToPDF(
                stringToInsert: Constatnts.RegNumber,
                targetString: Constatnts.TargetStrForRegNumber,
                numEntryTargetString: 1,
                reader,
                writer,
                pageNum: i,
                LocationToInsert.RB,
                offsetInsertion: new Point(x: 0, y: 2));
        }

        private void chkIsElectronic_CheckedChanged(object sender, EventArgs e)
        {
            if (chkIsElectronic.Checked)
            {
                cboTypeWork.Enabled = true;
                cboTypeWork.Text = cboTypeWork.Items[0].ToString();
            }
            if (!chkIsElectronic.Checked)
            {
                cboTypeWork.Enabled = false;
                cboTypeWork.Text = "Вид поверки";
            }
        }
        protected virtual bool IsFileLocked(FileInfo file)
        {
            FileStream stream = null;
            try
            {
                stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None);
            }
            catch (IOException)
            {
                return true;
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }

            }
            return false;
        }
    }



}

