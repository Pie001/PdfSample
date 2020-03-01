using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;

// https://itextpdf.com/en/resources/examples/itext-7/rowspan-and-splitting
namespace PdfSample01.Itext7
{
    public class RowspanAndSplitting
    {
        private string ClassName = "4_RowspanAndSplitting";

        public void SplittingAndRowspan()
        {
            FileStream fs = new FileStream(string.Format(@"pdf\{0}__SplittingAndRowspan_{1:yyyyMMddhhmmss}.pdf", ClassName, DateTime.Now), FileMode.Create);

            PdfDocument pdfDoc = new PdfDocument(new PdfWriter(fs));
            Document doc = new Document(pdfDoc, new PageSize(300, 160));

            doc.Add(new Paragraph("Table with setKeepTogether(true):"));

            Table table = new Table(UnitValue.CreatePercentArray(2)).UseAllAvailableWidth();
            table.SetKeepTogether(true);
            table.SetMarginTop(10);

            Cell cell = new Cell(3, 1);
            cell.Add(new Paragraph("G"));
            cell.Add(new Paragraph("R"));
            cell.Add(new Paragraph("P"));

            table.AddCell(cell);
            table.AddCell("row 1");
            table.AddCell("row 2");
            table.AddCell("row 3");

            doc.Add(table);

            doc.Add(new AreaBreak());

            doc.Add(new Paragraph("Table with setKeepTogether(false):"));
            table.SetKeepTogether(false);

            doc.Add(table);

            doc.Close();
        }

        public void SplittingNestedTable1()
        {
            FileStream fs = new FileStream(string.Format(@"pdf\{0}__SplittingNestedTable1_{1:yyyyMMddhhmmss}.pdf", ClassName, DateTime.Now), FileMode.Create);

            PdfDocument pdfDoc = new PdfDocument(new PdfWriter(fs));
            Document doc = new Document(pdfDoc, new PageSize(300, 210));

            doc.Add(new Paragraph("Table with setKeepTogether(true):"));
            Table table = CreateTableForSplittingNestedTable1(true);
            doc.Add(table);

            doc.Add(new AreaBreak());

            doc.Add(new Paragraph("Table with setKeepTogether(false):"));
            table = CreateTableForSplittingNestedTable1(false);
            doc.Add(table);

            doc.Close();

        }

        private static Table CreateTableForSplittingNestedTable1(bool keepTableTogether)
        {
            Table table = new Table(UnitValue.CreatePercentArray(2)).UseAllAvailableWidth();
            table.SetMarginTop(10);

            // If true, iText will do its best trying not to split the table and process it on a single area
            table.SetKeepTogether(keepTableTogether);

            Cell cell = new Cell();
            cell.Add(new Paragraph("G"));
            cell.Add(new Paragraph("R"));
            cell.Add(new Paragraph("O"));
            cell.Add(new Paragraph("U"));
            cell.Add(new Paragraph("P"));

            table.AddCell(cell);

            Table inner = new Table(UnitValue.CreatePercentArray(1)).UseAllAvailableWidth();
            inner.AddCell("row 1");
            inner.AddCell("row 2");
            inner.AddCell("row 3");
            inner.AddCell("row 4");
            inner.AddCell("row 5");

            cell = new Cell().Add(inner);
            cell.SetPadding(0);
            table.AddCell(cell);

            return table;
        }

        public void SplittingNestedTable2()
        {
            FileStream fs = new FileStream(string.Format(@"pdf\{0}__SplittingNestedTable2_{1:yyyyMMddhhmmss}.pdf", ClassName, DateTime.Now), FileMode.Create);

            PdfDocument pdfDoc = new PdfDocument(new PdfWriter(fs));
            Document doc = new Document(pdfDoc, new PageSize(300, 170));

            doc.Add(new Paragraph("Cell with setKeepTogether(true):"));
            Table table = CreateTableForSplittingNestedTable2(true);
            doc.Add(table);

            doc.Add(new AreaBreak());

            doc.Add(new Paragraph("Cell with setKeepTogether(false):"));
            table = CreateTableForSplittingNestedTable2(false);
            doc.Add(table);

            doc.Close();
        }

        private static Table CreateTableForSplittingNestedTable2(bool keepFirstCellTogether)
        {
            Table table = new Table(UnitValue.CreatePercentArray(2)).UseAllAvailableWidth();
            table.SetMarginTop(10);

            Cell cell = new Cell();
            cell.Add(new Paragraph("GROUPS"));
            cell.SetRotationAngle(Math.PI / 2);

            // If true, iText will do its best trying not to split the table and process it on a single area
            cell.SetKeepTogether(keepFirstCellTogether);


            table.AddCell(cell);

            Table inner = new Table(UnitValue.CreatePercentArray(1)).UseAllAvailableWidth();
            inner.AddCell("row 1");
            inner.AddCell("row 2");
            inner.AddCell("row 3");
            inner.AddCell("row 4");
            inner.AddCell("row 5");

            cell = new Cell().Add(inner);
            cell.SetPadding(0);
            table.AddCell(cell);

            return table;
        }

    }
}
