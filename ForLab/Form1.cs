using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        private void butModifyDoc_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "PDF files(*.pdf)|*.pdf";
            if (fileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            string inputFile = fileDialog.FileName;
            string outputFile = fileDialog.FileName.Replace(".pdf", " ВАСТ.pdf");
            //iTextSharp.text.Image imgfoot = iTextSharp.text.Image.GetInstance(@"C:\Users\ВеринСГ\Desktop\Новая папка\Безымянный.png");

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
                        //InsetRegNumber(reader, writer, i);
                        //InsetHeadPosition(reader, writer, i);
                        NewMethod(reader, writer, i);
                        InsetStringToPDF(
                            stringToInsert: Constatnts.PositionHeadMetrologist,
                            targetString: Constatnts.TargetStrForPosHead,
                            numEntryTargetString: 1,
                            reader,
                            writer,
                            pageNum: i,
                            LocationToInsert.LT,
                            offsetInsertion: new Point(x: 0, y: 10));
                        InsetStringToPDF(
                            stringToInsert: Constatnts.NameHeadMetrologist,
                            targetString: Constatnts.TargetStrForNameHead,
                            numEntryTargetString: 2,
                            reader,
                            writer,
                            pageNum: i,
                            LocationToInsert.LT,
                            offsetInsertion: new Point(x: 0, y: 10));
                    }
                    //var t = new MyLocationTextExtractionStrategy();
                    //var ex = PdfTextExtractor.GetTextFromPage(reader, 1, t);
                    //imgfoot.SetAbsolutePosition(t.myPoints[0].Rect.Left, t.myPoints[0].Rect.Bottom - imgfoot.Height);

                    //document.Add(imgfoot);
                    document.Close();
                    writer.Close();
                }
            }
            System.Diagnostics.Process.Start(outputFile);
        }

        private static void NewMethod(PdfReader reader, PdfWriter writer, int i)
        {
            InsetStringToPDF(
                stringToInsert: Constatnts.RegNumber,
                targetString: Constatnts.TargetStrForRegNumber,
                numEntryTargetString: 1,
                reader,
                writer,
                pageNum: i,
                LocationToInsert.RB,
                offsetInsertion: new Point(x: 0, y: 2));
        }

        private static void InsetRegNumber(PdfReader reader, PdfWriter writer, int pageNum)
        {
            var baseFont = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            var importedPage = writer.GetImportedPage(reader, pageNum);
            var contentByte = writer.DirectContent;
            contentByte.BeginText();
            contentByte.SetFontAndSize(baseFont, 9);
            var registryNum = new MyLocationTextExtractionStrategy();
            var Extractor = PdfTextExtractor.GetTextFromPage(reader, pageNum, registryNum);
            var targetLocation = registryNum.GetTargetStringsInfo(Constatnts.TargetStrForRegNumber);
            Point registryNumLocation = new Point(450, 750);
            if (targetLocation.Count > 0)
            {
                iTextSharp.text.Rectangle rect = targetLocation[0].Rect;
                registryNumLocation = new Point((int)(rect.Right), (int)(rect.Bottom) + 2);
            }
            else
            {
                MessageBox.Show("Не найдены ключевые слова для размещения уникального номера");
            }
            contentByte.ShowTextAligned(
                PdfContentByte.ALIGN_LEFT,
                Constatnts.RegNumber,
                registryNumLocation.X,
                registryNumLocation.Y, 0);
            contentByte.EndText();
            contentByte.AddTemplate(importedPage, 0, 0);
        }
        private static void InsetHeadPosition(PdfReader reader, PdfWriter writer, int pageNum)
        {
            string ttf = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "TIMES.TTF");

            var baseFont = BaseFont.CreateFont(ttf, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            var importedPage = writer.GetImportedPage(reader, pageNum);
            var contentByte = writer.DirectContent;
            contentByte.BeginText();
            contentByte.SetFontAndSize(baseFont, 7);
            var registryNum = new MyLocationTextExtractionStrategy();
            var Extractor = PdfTextExtractor.GetTextFromPage(reader, pageNum, registryNum);
            var targetLocation = registryNum.GetTargetStringsInfo(Constatnts.TargetStrForPosHead);
            Point registryNumLocation = new Point(450, 750);
            if (targetLocation.Count > 0)
            {
                iTextSharp.text.Rectangle rect = targetLocation[0].Rect;
                registryNumLocation = new Point((int)(rect.Left), (int)(rect.Top) + 10);
            }
            else
            {
                MessageBox.Show("Не найдены ключевые слова для размещения уникального номера");
            }
            contentByte.ShowTextAligned(
                PdfContentByte.ALIGN_LEFT,
               Constatnts.PositionHeadMetrologist,
                registryNumLocation.X,
                registryNumLocation.Y, 0);
            contentByte.EndText();
            contentByte.AddTemplate(importedPage, 0, 0);
        }
        private static void InsetStringToPDF(string stringToInsert, string targetString, int numEntryTargetString, PdfReader reader, PdfWriter writer, int pageNum, LocationToInsert locationToInsert, Point offsetInsertion)
        {
            string ttf = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "TAHOMA.TTF");
            var baseFont = BaseFont.CreateFont(ttf, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            var importedPage = writer.GetImportedPage(reader, pageNum);
            var contentByte = writer.DirectContent;
            contentByte.BeginText();
            contentByte.SetFontAndSize(baseFont, 6);
            var registryNum = new MyLocationTextExtractionStrategy();
            var Extractor = PdfTextExtractor.GetTextFromPage(reader, pageNum, registryNum);
            var targetLocation = registryNum.GetTargetStringsInfo(targetString);
            if (targetLocation.Count > numEntryTargetString - 1)
            {
                iTextSharp.text.Rectangle rect = targetLocation[numEntryTargetString - 1].Rect;
                Point registryNumLocation = GetPointToInsert(locationToInsert, offsetInsertion, rect);
                contentByte.ShowTextAligned(
                    PdfContentByte.ALIGN_LEFT,
                    stringToInsert,
                    registryNumLocation.X,
                    registryNumLocation.Y, 0);
            }
            else
            {
                MessageBox.Show($"Не найдены ключевые слова \"{targetString}\": для размещения: \"{stringToInsert}\"");
            }
            contentByte.EndText();
            contentByte.AddTemplate(importedPage, 0, 0);
        }

        private static Point GetPointToInsert(LocationToInsert locationToInsert, Point offsetInsertion, iTextSharp.text.Rectangle rect)
        {
            if (locationToInsert == LocationToInsert.LT)
            {
                return new Point((int)(rect.Left) + offsetInsertion.X, (int)(rect.Top) + offsetInsertion.Y);
            }
            if (locationToInsert == LocationToInsert.LB)
            {
                return new Point((int)(rect.Left) + offsetInsertion.X, (int)(rect.Bottom) + offsetInsertion.Y);
            }
            if (locationToInsert == LocationToInsert.RB)
            {
                return new Point((int)(rect.Right) + offsetInsertion.X, (int)(rect.Bottom) + offsetInsertion.Y);
            }
            if (locationToInsert == LocationToInsert.RT)
            {
                return new Point((int)(rect.Right) + offsetInsertion.X, (int)(rect.Top) + offsetInsertion.Y);
            }

            return new Point();
        }
    }



}

