using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Xml.XPath;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Shapes;
using MigraDoc.DocumentObjectModel.Tables;
using PdfSharp.Drawing;
using PdfSharp.Pdf;

namespace Report_Generator
{
    public class PDFCreationClass
    {
        public static void CreatePDF(DataSet ds)
        {
            try
            {
                int i = 0;
                int j = 0;
                int yPoint = 0;
                string pubname = null;
                string city = null;
                string state = null;
                string state1 = null;
                string state2 = null;
                string state3 = null;


                PdfDocument pdf = new PdfDocument();
                pdf.Info.Title = "Database to PDF";
                PdfPage pdfPage = pdf.AddPage();

                XGraphics graph = XGraphics.FromPdfPage(pdfPage);

                XFont font = new XFont("Verdana", 5, XFontStyle.Regular);

                yPoint = yPoint + 100;

                for (i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                {

                    pubname = ds.Tables[0].Rows[i].ItemArray[0].ToString();
                    city = ds.Tables[0].Rows[i].ItemArray[1].ToString();
                    state = ds.Tables[0].Rows[i].ItemArray[2].ToString();
                    state1 = ds.Tables[0].Rows[i].ItemArray[3].ToString();
                    state2 = ds.Tables[0].Rows[i].ItemArray[4].ToString();
                    state3 = ds.Tables[0].Rows[i].ItemArray[5].ToString();

                    graph.DrawString(pubname, font, XBrushes.Black, new XRect(10, yPoint, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                    graph.DrawString(city, font, XBrushes.Black, new XRect(110, yPoint, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                    graph.DrawString(state, font, XBrushes.Black, new XRect(210, yPoint, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                    graph.DrawString(state1, font, XBrushes.Black, new XRect(310, yPoint, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                    graph.DrawString(state2, font, XBrushes.Black, new XRect(410, yPoint, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                    graph.DrawString(state3, font, XBrushes.Black, new XRect(510, yPoint, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                    yPoint = yPoint + 40;
                }


                string pdfFilename = "dbtopdf.pdf";
                pdf.Save(pdfFilename);
                Process.Start(pdfFilename);
            }
            catch (Exception ex)
            {
                Debug.Write(ex.ToString());
            }
        }
    }
}
