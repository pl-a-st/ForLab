using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;

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
    public class MyLocationTextExtractionStrategy : LocationTextExtractionStrategy
    {
        //Hold each coordinate
        public List<RectAndText> myPoints = new List<RectAndText>();


        //The string that we're searching for
        public String TextToSearchFor { get; set; }

        //How to compare strings
        public System.Globalization.CompareOptions CompareOptions { get; set; }

        public MyLocationTextExtractionStrategy(System.Globalization.CompareOptions compareOptions = System.Globalization.CompareOptions.None)
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
    }
}