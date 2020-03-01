using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using iText.IO.Font.Constants;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Layout.Borders;

// https://itextpdf.com/en/resources/examples/itext-7/cell-and-table-widths
namespace PdfSample01.Itext7
{
    public class CellAndTableWidths
    {
        private string ClassName = "1_CellAndTableWidths";

        public void ColumnWidthExample()
        {
            FileStream fs = new FileStream(string.Format(@"pdf\{0}__ColumnWidthExample_{1:yyyyMMddhhmmss}.pdf", ClassName, DateTime.Now), FileMode.Create);

            PdfDocument pdfDoc = new PdfDocument(new PdfWriter(fs));
            Document doc = new Document(pdfDoc, PageSize.A4.Rotate());

            float[] columnWidths = { 1, 5, 5 };
            Table table = new Table(UnitValue.CreatePercentArray(columnWidths));

            PdfFont f = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
            Cell cell = new Cell(1, 3)
                .Add(new Paragraph("This is a header"))
                .SetFont(f)
                .SetFontSize(13)
                .SetFontColor(DeviceGray.WHITE)
                .SetBackgroundColor(DeviceGray.BLACK)
                .SetTextAlignment(TextAlignment.CENTER);

            table.AddHeaderCell(cell);

            for (int i = 0; i < 2; i++)
            {
                Cell[] headerFooter =
                {
                    new Cell().SetBackgroundColor(new DeviceGray(0.75f)).Add(new Paragraph("#")),
                    new Cell().SetBackgroundColor(new DeviceGray(0.75f)).Add(new Paragraph("Key")),
                    new Cell().SetBackgroundColor(new DeviceGray(0.75f)).Add(new Paragraph("Value"))
                };

                foreach (Cell hfCell in headerFooter)
                {
                    if (i == 0)
                    {
                        table.AddHeaderCell(hfCell);
                    }
                    else
                    {
                        table.AddFooterCell(hfCell);
                    }
                }
            }

            for (int counter = 0; counter < 100; counter++)
            {
                table.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph((counter + 1).ToString())));
                table.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph("key " + (counter + 1))));
                table.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph("value " + (counter + 1))));
            }

            doc.Add(table);

            doc.Close();
        }

        public void FullPageTable()
        {
            FileStream fs = new FileStream(string.Format(@"pdf\{0}__FullPageTable_{1:yyyyMMddhhmmss}.pdf", ClassName, DateTime.Now), FileMode.Create);

            PdfDocument pdfDoc = new PdfDocument(new PdfWriter(fs));
            Document doc = new Document(pdfDoc, new PageSize(595, 842));
            doc.SetMargins(0, 0, 0, 0);

            Table table = new Table(new float[10]).UseAllAvailableWidth();
            table.SetMarginTop(0);
            table.SetMarginBottom(0);

            // first row
            Cell cell = new Cell(1, 10).Add(new Paragraph("DateRange"));
            cell.SetTextAlignment(TextAlignment.CENTER);
            cell.SetPadding(5);
            cell.SetBackgroundColor(new DeviceRgb(140, 221, 8));
            table.AddCell(cell);

            table.AddCell("Calldate");
            table.AddCell("Calltime");
            table.AddCell("Source");
            table.AddCell("DialedNo");
            table.AddCell("Extension");
            table.AddCell("Trunk");
            table.AddCell("Duration");
            table.AddCell("Calltype");
            table.AddCell("Callcost");
            table.AddCell("Site");

            for (int i = 0; i < 100; i++)
            {
                table.AddCell("date" + i);
                table.AddCell("time" + i);
                table.AddCell("source" + i);
                table.AddCell("destination" + i);
                table.AddCell("extension" + i);
                table.AddCell("trunk" + i);
                table.AddCell("dur" + i);
                table.AddCell("toc" + i);
                table.AddCell("callcost" + i);
                table.AddCell("Site" + i);
            }

            doc.Add(table);

            doc.Close();
        }

        public void RightCornerTable()
        {
            FileStream fs = new FileStream(string.Format(@"pdf\{0}__RightCornerTable_{1:yyyyMMddhhmmss}.pdf", ClassName, DateTime.Now), FileMode.Create);

            PdfDocument pdfDoc = new PdfDocument(new PdfWriter(fs));
            Document doc = new Document(pdfDoc, new PageSize(300, 300));
            doc.SetMargins(0, 0, 0, 0);

            Table table = new Table(UnitValue.CreatePercentArray(1)).UseAllAvailableWidth();
            table.SetHorizontalAlignment(HorizontalAlignment.RIGHT);
            table.SetWidth(90);

            Cell cell = new Cell().Add(new Paragraph(" Date").SetFontColor(ColorConstants.WHITE));
            cell.SetBackgroundColor(ColorConstants.BLACK);
            cell.SetBorder(new SolidBorder(ColorConstants.GRAY, 2));
            table.AddCell(cell);

            Cell cellTwo = new Cell().Add(new Paragraph("10/01/2015"));
            cellTwo.SetBorder(new SolidBorder(2));
            table.AddCell(cellTwo);

            doc.Add(table);

            doc.Close();
        }

        public void SimpleTable3()
        {
            FileStream fs = new FileStream(string.Format(@"pdf\{0}__SimpleTable3_{1:yyyyMMddhhmmss}.pdf", ClassName, DateTime.Now), FileMode.Create);

            PdfDocument pdfDoc = new PdfDocument(new PdfWriter(fs));
            Document doc = new Document(pdfDoc, PageSize.A3.Rotate());

            Table table = new Table(UnitValue.CreatePercentArray(35)).UseAllAvailableWidth().SetFixedLayout();
            table.SetWidth(pdfDoc.GetDefaultPageSize().GetWidth() - 80);

            Cell contractor = new Cell(1, 5).Add(new Paragraph("XXXXXXXXXXXXX"));
            table.AddCell(contractor);

            Cell workType = new Cell(1, 5).Add(new Paragraph("Refractory Works"));
            table.AddCell(workType);

            Cell supervisor = new Cell(1, 4).Add(new Paragraph("XXXXXXXXXXXXXX"));
            table.AddCell(supervisor);

            Cell paySlipHead = new Cell(1, 10).Add(new Paragraph("XXXXXXXXXXXXXXXX"));
            table.AddCell(paySlipHead);

            Cell paySlipMonth = new Cell(1, 2).Add(new Paragraph("XXXXXXX"));
            table.AddCell(paySlipMonth);

            Cell blank = new Cell(1, 9).Add(new Paragraph(""));
            table.AddCell(blank);

            doc.Add(table);

            doc.Close();
        }
    }
}
