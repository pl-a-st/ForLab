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

    public partial class MainForm : Form
    {
        public MainForm()
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
            iTextSharp.text.Image imgfoot = iTextSharp.text.Image.GetInstance(Properties.Resources.прямоугольник_для_штампа, System.Drawing.Imaging.ImageFormat.Png);
            imgfoot.ScaleAbsolute(60, 35);

            PDFTextExtractionStrategy.InsertImageToPDF(
                fileName,
                imgfoot,
                targetString: PmData.TargetStrForSymble,
                numEntryTargetString: 1,
                reader,
                writer,
                pageNum: 1,
                TypeLocationToInsert.RB,
                offsetInsertion: new Point(2, -(int)imgfoot.ScaledHeight / 2 + 5),
                onFail);
            PDFTextExtractionStrategy.InsetStringToPDF(
                fileName,
                stringToInsert: cboDateSymbol.Text[cboDateSymbol.Text.Length - 2].ToString(),
                fontSize: 30,
                targetString: PmData.TargetStrForSymble,
                numEntryTargetString: 1,
                reader,
                writer,
                pageNum: 1,
                TypeLocationToInsert.RB,
                offsetInsertion: new Point(5, -5),
                onFail
                );
            PDFTextExtractionStrategy.InsetStringToPDF(
                fileName,
                stringToInsert: PmData.Code,
                fontSize: 15,
                targetString: PmData.TargetStrForSymble,
                numEntryTargetString: 1,
                reader,
                writer,
                pageNum: 1,
                TypeLocationToInsert.RB,
                offsetInsertion: new Point(20, 0),
                onFail
                );
            PDFTextExtractionStrategy.InsetStringToPDF(
                fileName,
                stringToInsert: cboDateSymbol.Text[cboDateSymbol.Text.Length - 1].ToString(),
                fontSize: 30,
                targetString: PmData.TargetStrForSymble,
                numEntryTargetString: 1,
                reader,
                writer,
                pageNum: 1,
                TypeLocationToInsert.RB,
                offsetInsertion: new Point(43, -5),
                onFail
                );
        }

        private void InsertNameHeadMetrology(string fileName, PdfReader reader, PdfWriter writer, int pageNum, OnFail onFail)
        {
            PDFTextExtractionStrategy.InsetStringToPDF(
                fileName,
                stringToInsert: cboNameHead.Text,
                targetString: PmData.TargetStrForNameHead,
                numEntryTargetString: 2,
                reader,
                writer,
                pageNum: pageNum,
                TypeLocationToInsert.LT,
                offsetInsertion: new Point(x: 0, y: 10),
                onFail);
        }

        private void InsertPosition(string fileName, PdfReader reader, PdfWriter writer, int pageNum, OnFail onFail)
        {
            PDFTextExtractionStrategy.InsetStringToPDF(
                fileName,
                stringToInsert: cboHeadPosition.Text,
                targetString: PmData.TargetStrForPosHead,
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
                cboDateSymbol.Enabled = true;
                cboDateSymbol.Text = cboDateSymbol.Items[0].ToString();
            }
            if (!chkIsElectronic.Checked)
            {
                cboDateSymbol.Enabled = false;
                cboDateSymbol.Text = "Год поверки";
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

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadAllComboboxes();
        }

        private void LoadAllComboboxes()
        {
            cboNameHead.Items.Clear();
            cboNameHead.Items.AddRange(PmData.ListHeadNames.ToArray());
            if (cboNameHead.Items.Count > 0)
            {
                cboNameHead.SelectedIndex = 0;
            }
            cboHeadPosition.Items.Clear();
            cboHeadPosition.Items.AddRange(PmData.ListPositionHead.ToArray());
            if (cboHeadPosition.Items.Count > 0)
            {
                cboHeadPosition.SelectedIndex = 0;
            }
            cboDateSymbol.Items.Clear();
            cboDateSymbol.Items.AddRange(PmData.ListDateSimbols.ToArray());
            if (cboDateSymbol.Items.Count > 0)
            {
                cboDateSymbol.SelectedIndex = 0;
            }
        }

        private void butAddHead_Click(object sender, EventArgs e)
        {
            InputForm inputForm = GetInput("Добавление фамилии сотрудника", "Фамилия И.О.");
            AddStrigToListAndFile(inputForm, PmData.ListHeadNames, PmData.PathListHeadNames);
            if (inputForm.DialogResult != DialogResult.OK)
            {
                return;
            }
            LoadAllComboboxes();
            if (cboNameHead.Items.Count > 0)
            {
                cboNameHead.SelectedItem = cboNameHead.Items[cboNameHead.Items.Count - 1];
            }
        }
        private void butDelHead_Click(object sender, EventArgs e)
        {
            PmData.ListHeadNames.RemoveAt(cboNameHead.SelectedIndex);
            DAO.binWriteObjectToFile(PmData.ListHeadNames, PmData.PathListHeadNames);
            LoadAllComboboxes();
        }

        private void chkIsMulty_CheckedChanged(object sender, EventArgs e)
        {
            if (chkIsMulty.Checked)
            {
                butModifyDoc.Text = "Выбрать папку";
            }
            if (!chkIsMulty.Checked)
            {
                butModifyDoc.Text = "Выбрать файл";
            }
        }

        private void butAddPosition_Click(object sender, EventArgs e)
        {
            InputForm inputForm = GetInput("Добавление должности сотрудника", "Должность");
            AddStrigToListAndFile(inputForm, PmData.ListPositionHead, PmData.PathListHeadPositions);
            if (inputForm.DialogResult != DialogResult.OK)
            {
                return;
            }
            LoadAllComboboxes();
            if (cboHeadPosition.Items.Count > 0)
            {
                cboHeadPosition.SelectedItem = cboHeadPosition.Items[cboHeadPosition.Items.Count - 1];
            }
        }
        private InputForm GetInput(string formText, string lableText)
        {
            InputForm inputForm = new InputForm();
            inputForm.SetLable(lableText);
            inputForm.SetText(formText);
            return inputForm;
        }
        private static void AddStrigToListAndFile(InputForm inputForm,List<string> listStr, string pathFile)
        {
            inputForm.ShowDialog();
            if (inputForm.DialogResult != DialogResult.OK)
            {
                return;
            }
            listStr.Add(inputForm.GetTextBoxText());

            if (DAO.binWriteObjectToFile(listStr, pathFile) != MethodResultStatus.Sucsess)
            {
                inputForm.DialogResult = DialogResult.None;
                AddStrigToListAndFile(inputForm, listStr, pathFile);
            }
        }

        private void butDelPosition_Click(object sender, EventArgs e)
        {
            if (cboHeadPosition.SelectedIndex == -1)
            {
                return;
            }
            PmData.ListPositionHead.RemoveAt(cboHeadPosition.SelectedIndex);
            DAO.binWriteObjectToFile(PmData.ListPositionHead, PmData.PathListHeadPositions);
            LoadAllComboboxes();
        }

        private void butAddDate_Click(object sender, EventArgs e)
        {
            InputForm inputForm = GetInput("Добавление даты", "Дата");
            AddStrigToListAndFile(inputForm, PmData.ListDateSimbols, PmData.PathListDateSymbol);
            if (inputForm.DialogResult != DialogResult.OK)
            {
                return;
            }
            LoadAllComboboxes();
            if (cboDateSymbol.Items.Count > 0)
            {
                cboDateSymbol.SelectedItem = cboDateSymbol.Items[cboDateSymbol.Items.Count - 1];
            }
        }

        private void butDelDate_Click(object sender, EventArgs e)
        {
            if (cboDateSymbol.SelectedIndex == -1)
            {
                return;
            }
            PmData.ListDateSimbols.RemoveAt(cboDateSymbol.SelectedIndex);
            DAO.binWriteObjectToFile(PmData.ListDateSimbols, PmData.PathListDateSymbol);
            LoadAllComboboxes();
        }
    }



}

