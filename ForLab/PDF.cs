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
    public enum LocationToInsert
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
        //Hold each coordinate
        public List<RectAndText> myPoints = new List<RectAndText>();


        //The string that we're searching for
        public String TextToSearchFor { get; set; }

        //How to compare strings
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
        public static void InsetStringToPDF(string stringToInsert, string targetString, int numEntryTargetString, PdfReader reader, PdfWriter writer, int pageNum, LocationToInsert locationToInsert, Point offsetInsertion)
        {
            string ttf = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "TAHOMA.TTF");
            var baseFont = BaseFont.CreateFont(ttf, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            var importedPage = writer.GetImportedPage(reader, pageNum);
            var contentByte = writer.DirectContent;
            contentByte.BeginText();
            contentByte.SetFontAndSize(baseFont, 6);
            var extractionStrategy = new PDFTextExtractionStrategy();
            var extractor = PdfTextExtractor.GetTextFromPage(reader, pageNum, extractionStrategy);
            var LisFoundStringInfo = extractionStrategy.GetTargetStringsInfo(targetString);
            if (LisFoundStringInfo.Count > numEntryTargetString - 1)
            {
                iTextSharp.text.Rectangle rect = LisFoundStringInfo[numEntryTargetString - 1].Rect;
                Point PointToInsert = GetPointToInsert(locationToInsert, offsetInsertion, rect);
                contentByte.ShowTextAligned(
                    PdfContentByte.ALIGN_LEFT,
                    stringToInsert,
                    PointToInsert.X,
                    PointToInsert.Y, 
                    rotation: 0);
                
            }
            else
            {
                MessageBox.Show($"Не найдены ключевые слова \"{targetString}\": для размещения: \"{stringToInsert}\"");
            }
            contentByte.EndText();
            contentByte.AddTemplate(importedPage, 0, 0);
        }
        public static void InsertImageToPDF(iTextSharp.text.Image img, string targetString, int numEntryTargetString,
            PdfReader reader, PdfWriter writer, int pageNum, LocationToInsert locationToInsert, Point offsetInsertion)
        {
            var extractionStrategy = new PDFTextExtractionStrategy();
            var extractor = PdfTextExtractor.GetTextFromPage(reader, pageNum, extractionStrategy);
            var importedPage = writer.GetImportedPage(reader, pageNum);
            var LisFoundStringInfo = extractionStrategy.GetTargetStringsInfo(targetString);
            var contentByte = writer.DirectContent;
            contentByte.BeginText();
            if (LisFoundStringInfo.Count > numEntryTargetString - 1)
            {
                iTextSharp.text.Rectangle rect = LisFoundStringInfo[numEntryTargetString - 1].Rect;
                Point PointToInsert = GetPointToInsert(locationToInsert, offsetInsertion, rect);
                img.SetAbsolutePosition(PointToInsert.X, PointToInsert.Y);
                contentByte.AddImage(img);
            }
            else
            {
                MessageBox.Show($"Не найдены ключевые слова \"{targetString}\": для размещения картинки");
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