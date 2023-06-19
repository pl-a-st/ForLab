using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System.Windows.Forms;
using System.Drawing;

namespace ForLab
{
    /// <summary>
    /// расоположение относительно вершин прямоугольника
    /// </summary>
    public enum TypeLocationToInsert
    {
        /// <summary>
        /// лево низ
        /// </summary>
        LB,
        /// <summary>
        /// лево верх
        /// </summary>
        LT,
        /// <summary>
        /// право верх
        /// </summary>
        RT,
        /// <summary>
        /// право низ
        /// </summary>
        RB
    }
    public enum OnFail
    {
        Message,
        Log
    }
    public class RectAndText
    {
        public iTextSharp.text.Rectangle Rect;
        public String Text;
        public RectAndText(iTextSharp.text.Rectangle rect, String text)
        {
            this.Rect = rect;
            this.Text = text;
        }
    }
    public class PDFTextExtractionStrategy : LocationTextExtractionStrategy
    {
        public List<RectAndText> myPoints = new List<RectAndText>();
        public String TextToSearchFor { get; set; }
        public System.Globalization.CompareOptions CompareOptions { get; set; }

        public PDFTextExtractionStrategy(System.Globalization.CompareOptions compareOptions = System.Globalization.CompareOptions.None)
        {
            
            this.CompareOptions = compareOptions;
        }

        public override void RenderText(TextRenderInfo renderInfo)
        {
            base.RenderText(renderInfo);

            //Get the bounding box for the chunk of text
            var bottomLeft = renderInfo.GetDescentLine().GetStartPoint();
            var topRight = renderInfo.GetAscentLine().GetEndPoint();

            //Create a rectangle from it
            var rect = new iTextSharp.text.Rectangle(
                                                    bottomLeft[Vector.I1],
                                                    bottomLeft[Vector.I2],
                                                    topRight[Vector.I1],
                                                    topRight[Vector.I2]
                                                    );

            //Add this to our main collection
            this.myPoints.Add(new RectAndText(rect, renderInfo.GetText()));
        }
        public List<RectAndText> GetTargetStringsInfo(string targetString)
        {
            List<RectAndText> targetRectAndText = new List<RectAndText>();
            string[] splitStr = targetString.Split(' ');
            if (myPoints.Count < 0)
            {
                return targetRectAndText;
            }
            for (int i = 0; i < myPoints.Count; i++)
            {
                if (myPoints[i].Text.Trim().ToLower() == splitStr[0].ToLower())
                {
                    for (int j = 1; j < splitStr.Length; j++)
                    {
                        string targetText = myPoints[i].Text;


                        if (myPoints[i + j].Text.Trim().ToLower() != splitStr[j].ToLower())
                        {
                            break;
                        }
                        targetText += myPoints[i + j].Text;

                        iTextSharp.text.Rectangle targetRect = new iTextSharp.text.Rectangle(
                            myPoints[i].Rect.Left,
                            myPoints[i].Rect.Bottom,
                            myPoints[i + j].Rect.Right,
                            myPoints[i + j].Rect.Top);
                        if (j == splitStr.Length - 1)
                        {
                            RectAndText rectAndText = new RectAndText(targetRect, targetText);
                            targetRectAndText.Add(rectAndText);
                        }
                    }
                }
            }
            return targetRectAndText;
        }
        public static void InsetStringToPDF(string fileName, string stringToInsert, string targetString, int numEntryTargetString, PdfReader reader, PdfWriter writer, int pageNum, TypeLocationToInsert TypelocationToInsert, Point offsetInsertion, OnFail onFail, bool isFirstEntry)
        {
            string ttf = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "TAHOMA.TTF");
            var baseFont = BaseFont.CreateFont(ttf, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            var importedPage = writer.GetImportedPage(reader, pageNum);
            var contentByte = writer.DirectContent;
            contentByte.BeginText();
            contentByte.SetFontAndSize(baseFont, 7);
            var extractionStrategy = new PDFTextExtractionStrategy();
            var extractor = PdfTextExtractor.GetTextFromPage(reader, pageNum, extractionStrategy);
            var LisFoundStringInfo = extractionStrategy.GetTargetStringsInfo(targetString);
            if (LisFoundStringInfo.Count < numEntryTargetString)
            {
                if (onFail == OnFail.Message)
                {
                    MessageBox.Show($"Не найдены ключевые слова \"{targetString}\": для размещения: \"{stringToInsert}\"");
                    
                }
                if (onFail == OnFail.Log)
                {
                    TryWtiteLog(fileName, stringToInsert, targetString);
                }
                contentByte.EndText();
                contentByte.AddTemplate(importedPage, 0, 0);
                return;
            }

            iTextSharp.text.Rectangle rectFoundString = LisFoundStringInfo[numEntryTargetString - 1].Rect;
            Point PointToInsert = GetPointToInsert(TypelocationToInsert, offsetInsertion, rectFoundString);
            contentByte.ShowTextAligned(
                PdfContentByte.ALIGN_LEFT,
                stringToInsert,
                PointToInsert.X,
                PointToInsert.Y,
                rotation: 0);
            contentByte.EndText();
            if (isFirstEntry)
            {
                contentByte.AddTemplate(importedPage, 0, 0);
            }
        }
        public static void InsetStringToPDF(string fileName, string stringToInsert, float fontSize, string targetString, int numEntryTargetString, PdfReader reader, PdfWriter writer, int pageNum, TypeLocationToInsert TypelocationToInsert, Point offsetInsertion, OnFail onFail, bool isFirstEntry)
        {
            string ttf = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "TAHOMABD.TTF");
            var baseFont = BaseFont.CreateFont(ttf, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            var importedPage = writer.GetImportedPage(reader, pageNum);
            var contentByte = writer.DirectContent;
            contentByte.BeginText();
            contentByte.SetFontAndSize(baseFont, fontSize);
            var extractionStrategy = new PDFTextExtractionStrategy();
            var extractor = PdfTextExtractor.GetTextFromPage(reader, pageNum, extractionStrategy);
            var LisFoundStringInfo = extractionStrategy.GetTargetStringsInfo(targetString);
            if (LisFoundStringInfo.Count < numEntryTargetString)
            {
                if (onFail == OnFail.Message)
                {
                    MessageBox.Show($"Не найдены ключевые слова \"{targetString}\": для размещения: \"{stringToInsert}\"");

                }
                if (onFail == OnFail.Log)
                {
                    TryWtiteLog(fileName, stringToInsert, targetString);
                }
                contentByte.EndText();
                if (isFirstEntry)
                {
                    contentByte.AddTemplate(importedPage, 0, 0);
                }
                return;
            }

            iTextSharp.text.Rectangle rectFoundString = LisFoundStringInfo[numEntryTargetString - 1].Rect;
            Point PointToInsert = GetPointToInsert(TypelocationToInsert, offsetInsertion, rectFoundString);
            contentByte.ShowTextAligned(
                PdfContentByte.ALIGN_LEFT,
                stringToInsert,
                PointToInsert.X,
                PointToInsert.Y,
                rotation: 0);
            contentByte.EndText();
            contentByte.AddTemplate(importedPage, 0, 0);
        }
        /// <summary>
        /// Пытается записать лог в туже папку куда пишет файл.
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="stringToInsert"></param>
        /// <param name="targetString"></param>
        private static void TryWtiteLog(string fileName, string stringToInsert, string targetString)
        {
            FileInfo fileInfo = new FileInfo(fileName);
            string pathToLog = fileInfo.DirectoryName;
            string LogFile = fileInfo.DirectoryName + @"\log " + DateTime.Now.Date.ToShortDateString() + ".txt";
            StreamWriter stream = null;
            try
            {
                stream = new StreamWriter(LogFile, true);
                stream.WriteLine($"Для файла{fileName}: Не найдены ключевые слова \"{targetString}\": для размещения: \"{stringToInsert}\"");
            }
            catch { }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }
            }
        }

        public static void InsertImageToPDF(string fileName, iTextSharp.text.Image img, string targetString, int numEntryTargetString,
            PdfReader reader, PdfWriter writer, int pageNum, TypeLocationToInsert locationToInsert, Point offsetInsertion, OnFail onFail, bool isFirstEntry)
        {
            var extractionStrategy = new PDFTextExtractionStrategy();
            var extractor = PdfTextExtractor.GetTextFromPage(reader, pageNum, extractionStrategy);
            
            var importedPage = writer.GetImportedPage(reader, pageNum);
            var LisFoundStringInfo = extractionStrategy.GetTargetStringsInfo(targetString);
            var contentByte = writer.DirectContent;
            contentByte.BeginText();
            if (LisFoundStringInfo.Count < numEntryTargetString)
            {
                if (onFail == OnFail.Message)
                {
                    MessageBox.Show($"Не найдены ключевые слова \"{targetString}\": для размещения картинки");

                }
                if (onFail == OnFail.Log)
                {
                    TryWtiteLog(fileName, "картинки", targetString);
                }
                contentByte.EndText();
                contentByte.AddTemplate(importedPage, 0, 0);
                return;
            }
            iTextSharp.text.Rectangle rect = LisFoundStringInfo[numEntryTargetString - 1].Rect;
            Point PointToInsert = GetPointToInsert(locationToInsert, offsetInsertion, rect);
            img.SetAbsolutePosition(PointToInsert.X, PointToInsert.Y);

            if (isFirstEntry)
            {
                contentByte.AddTemplate(importedPage, 0, 0);
            }
            contentByte.AddImage(img, true);
            contentByte.EndText();
        }
        public static void InsertImageToPDF(string fileName, iTextSharp.text.Image img,  PdfReader reader, PdfWriter writer, int pageNum, Point offsetInsertion, bool isFirstEntry)
        {
            var extractionStrategy = new PDFTextExtractionStrategy();
            var extractor = PdfTextExtractor.GetTextFromPage(reader, pageNum, extractionStrategy);
            var importedPage = writer.GetImportedPage(reader, pageNum);
            var contentByte = writer.DirectContent;
            contentByte.BeginText();
            img.SetAbsolutePosition(offsetInsertion.X, offsetInsertion.Y);
            if (isFirstEntry)
            {
                contentByte.AddTemplate(importedPage, 0, 0);
            }
            contentByte.AddImage(img,true);
            contentByte.EndText();
        }
        private static Point GetPointToInsert(TypeLocationToInsert locationToInsert, Point offsetInsertion, iTextSharp.text.Rectangle rect)
        {
            if (locationToInsert == TypeLocationToInsert.LT)
            {
                return new Point((int)(rect.Left) + offsetInsertion.X, (int)(rect.Top) + offsetInsertion.Y);
            }
            if (locationToInsert == TypeLocationToInsert.LB)
            {
                return new Point((int)(rect.Left) + offsetInsertion.X, (int)(rect.Bottom) + offsetInsertion.Y);
            }
            if (locationToInsert == TypeLocationToInsert.RB)
            {
                return new Point((int)(rect.Right) + offsetInsertion.X, (int)(rect.Bottom) + offsetInsertion.Y);
            }
            if (locationToInsert == TypeLocationToInsert.RT)
            {
                return new Point((int)(rect.Right) + offsetInsertion.X, (int)(rect.Top) + offsetInsertion.Y);
            }
            return new Point();
        }
    }
}