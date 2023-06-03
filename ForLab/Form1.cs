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
using Microsoft.WindowsAPICodePack.Dialogs;

namespace ForLab
{
    public enum Result
    {
        Sucsess,
        Fail,
        Error
    }

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
            if (chkIsMulty.Checked)
            {
                CommonOpenFileDialog FolderDialog = new CommonOpenFileDialog();
                FolderDialog.IsFolderPicker = true;
                if (FolderDialog.ShowDialog() != CommonFileDialogResult.Ok)
                {
                    return;
                }
                FileSystemInfo[] files = new DirectoryInfo(FolderDialog.FileName).GetFileSystemInfos();
                foreach (FileSystemInfo info in files)
                {
                    if (info.Extension.ToLower() != ".pdf")
                    {
                        continue;
                    }
                    string inputFile = info.FullName;
                    string outputFile = info.FullName.Replace(".pdf", " ВАСТ.pdf");
                    if (IsFileLocked(new FileInfo(outputFile)))
                    {
                        continue;
                    }
                    await InsertDataInFile(inputFile, outputFile, OnFail.Log);
                }
            }
            if (!chkIsMulty.Checked)
            {
                await GetFilePathAndInsertData(OnFail.Message);
            }
            label.Dispose();
            progressBar.Dispose();
        }
        private async Task GetFilePathAndInsertData(OnFail onFail)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "PDF files(*.pdf)|*.pdf";
            if (fileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            string inputFile = fileDialog.FileName;
            string outputFile = fileDialog.FileName.Replace(".pdf", " ВАСТ.pdf");
            if (IsFileLocked(new FileInfo(outputFile)))
            {
                MessageBox.Show("Данное свидетельство уже модифицировалось и итоговый файл открыт, для перезаписи необходимо закрыть файл и выбрать его снова");
                return;
            }
            await InsertDataInFile(inputFile, outputFile, onFail);
            await Task.Factory.StartNew(() => System.Diagnostics.Process.Start(outputFile));
            await Task.Delay(2500);
        }
        private async Task InsertDataInFile(string inputFile, string outputFile, OnFail onFail)
        {
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
                        InsertRegNum(inputFile, reader, writer, i, onFail);
                        InsertPosition(inputFile, reader, writer, i, onFail);
                        InsertNameHeadMetrology(inputFile, reader, writer, i, onFail);
                        InsertWorkSymbol(inputFile, reader, writer, onFail);
                    }
                    document.Close();
                    writer.Close();
                }
            }
           
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
            label.BackColor = Color.Transparent;
            label.Parent = progressBar;
            label.Location = new Point(
                x: butModifyDoc.Location.X + butModifyDoc.Width / 2 - label.PreferredWidth / 2,
                y: butModifyDoc.Location.Y + butModifyDoc.Height / 2 - label.Height / 2);
            this.Controls.Add(progressBar);
            this.Controls.Add(label);
            progressBar.BringToFront();
            label.BringToFront();
        }

        private void InsertWorkSymbol(string fileName, PdfReader reader, PdfWriter writer, OnFail onFail)
        {
            if (!chkIsMulty.Checked && !chkIsElectronic.Checked)
            {
                return;
            }
            iTextSharp.text.Image imgfoot = iTextSharp.text.Image.GetInstance(Properties.Resources.Первичная_поверка, System.Drawing.Imaging.ImageFormat.Png);
            imgfoot.ScaleAbsolute(60, 35);
            if (!chkIsMulty.Checked && chkIsElectronic.Checked)
            {
                if (cboTypeWork.SelectedItem.ToString().ToLower().Contains("первичная"))
                {
                    imgfoot.ScaleAbsolute(60, 35);
                }
                if (cboTypeWork.SelectedItem.ToString().ToLower().Contains("периодическая"))
                {
                    imgfoot.ScaleAbsolute(32, 35);
                }
            }
            PDFTextExtractionStrategy.InsertImageToPDF(
                fileName,
                imgfoot,
                "Знак поверки:",
                numEntryTargetString: 1,
                reader,
                writer,
                pageNum: 1,
                TypeLocationToInsert.RB,
                offsetInsertion: new Point(2, -(int)imgfoot.ScaledHeight / 2 + 5),
                onFail);
        }

        private static void InsertNameHeadMetrology(string fileName, PdfReader reader, PdfWriter writer, int pageNum, OnFail onFail)
        {
            PDFTextExtractionStrategy.InsetStringToPDF(
                fileName,
                stringToInsert: Constatnts.NameHeadMetrologist,
                targetString: Constatnts.TargetStrForNameHead,
                numEntryTargetString: 2,
                reader,
                writer,
                pageNum: pageNum,
                TypeLocationToInsert.LT,
                offsetInsertion: new Point(x: 0, y: 10),
                onFail);
        }

        private static void InsertPosition(string fileName, PdfReader reader, PdfWriter writer, int pageNum, OnFail onFail)
        {
            PDFTextExtractionStrategy.InsetStringToPDF(
                fileName,
                stringToInsert: Constatnts.PositionHeadMetrologist,
                targetString: Constatnts.TargetStrForPosHead,
                numEntryTargetString: 1,
                reader,
                writer,
                pageNum: pageNum,
                TypeLocationToInsert.LT,
                offsetInsertion: new Point(x: 0, y: 10),
                onFail);
        }

        private static void InsertRegNum(string fileName, PdfReader reader, PdfWriter writer, int pageNum, OnFail onFail)
        {
            PDFTextExtractionStrategy.InsetStringToPDF(
                fileName,
                stringToInsert: Constatnts.RegNumber,
                targetString: Constatnts.TargetStrForRegNumber,
                numEntryTargetString: 1,
                reader,
                writer,
                pageNum: pageNum,
                TypeLocationToInsert.RB,
                offsetInsertion: new Point(x: 0, y: 2),
                onFail);
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
                stream = file.Open(FileMode.OpenOrCreate, FileAccess.Read, FileShare.None);
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

