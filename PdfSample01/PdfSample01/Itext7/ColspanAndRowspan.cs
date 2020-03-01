using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.IO.Image;
using iText.Layout.Borders;
using iText.IO.Font.Constants;
using iText.Kernel.Font;
using iText.Kernel.Geom;

// https://itextpdf.com/en/resources/examples/itext-7/colspan-and-rowspan
namespace PdfSample01.Itext7
{
    public class ColspanAndRowspan
    {
        private string ClassName = "2_ColspanAndRowspan";

        public void ColspanRowspan()
        {
            FileStream fs = new FileStream(string.Format(@"pdf\{0}__ColspanRowspan_{1:yyyyMMddhhmmss}.pdf", ClassName, DateTime.Now), FileMode.Create);

            PdfDocument pdfDoc = new PdfDocument(new PdfWriter(fs));
            Document doc = new Document(pdfDoc);

            Table table = new Table(UnitValue.CreatePercentArray(4)).UseAllAvailableWidth();
            Cell cell = new Cell().Add(new Paragraph(" 1,1 "));
            table.AddCell(cell);

            cell = new Cell().Add(new Paragraph(" 1,2 "));
            table.AddCell(cell);

            Cell cell23 = new Cell(2, 2).Add(new Paragraph("multi 1,3 and 1,4"));
            table.AddCell(cell23);

            cell = new Cell().Add(new Paragraph(" 2,1 "));
            table.AddCell(cell);

            cell = new Cell().Add(new Paragraph(" 2,2 "));
            table.AddCell(cell);

            doc.Add(table);

            doc.Close();
        }

        public void RowspanAbsolutePosition()
        {
            FileStream fs = new FileStream(string.Format(@"pdf\{0}__RowspanAbsolutePosition_{1:yyyyMMddhhmmss}.pdf", ClassName, DateTime.Now), FileMode.Create);

            PdfDocument pdfDoc = new PdfDocument(new PdfWriter(fs));
            Document doc = new Document(pdfDoc);

            Table table1 = new Table(new float[] { 150, 200, 200 });

            Cell cell = new Cell(1, 2).Add(new Paragraph("{Month}"));
            cell.SetHorizontalAlignment(HorizontalAlignment.LEFT);

            Image img = new Image(ImageDataFactory.Create(@"pdf\image\image.png"));
            img.SetWidth(UnitValue.CreatePercentValue(100));
            img.SetAutoScale(true);

            Cell cell2 = new Cell(2, 1).Add(img);
            Cell cell3 = new Cell(1, 2).Add(new Paragraph("Mr Fname Lname"));
            cell3.SetHorizontalAlignment(HorizontalAlignment.LEFT);

            table1.AddCell(cell);
            table1.AddCell(cell2);
            table1.AddCell(cell3);

            doc.Add(table1);

            doc.Close();
        }

        public void SimpleRowColspan()
        {
            FileStream fs = new FileStream(string.Format(@"pdf\{0}__SimpleRowColspan_{1:yyyyMMddhhmmss}.pdf", ClassName, DateTime.Now), FileMode.Create);

            PdfDocument pdfDoc = new PdfDocument(new PdfWriter(fs));
            Document doc = new Document(pdfDoc);

            Table table = new Table(UnitValue.CreatePercentArray(new float[] { 1, 2, 2, 2, 1 }));

            Cell cell = new Cell(2, 1).Add(new Paragraph("S/N"));
            table.AddCell(cell);

            cell = new Cell(1, 3).Add(new Paragraph("Name"));
            table.AddCell(cell);

            cell = new Cell(2, 1).Add(new Paragraph("Age"));
            table.AddCell(cell);

            table.AddCell("SURNAME");
            table.AddCell("FIRST NAME");
            table.AddCell("MIDDLE NAME");
            table.AddCell("1");
            table.AddCell("James");
            table.AddCell("Fish");
            table.AddCell("Stone");
            table.AddCell("17");

            doc.Add(table);

            doc.Close();
        }

        public void SimpleTable11()
        {
            String[][] data =
                            {
                                new[]
                                {
                                    "ABC123", "The descriptive text may be more than one line and the text should wrap automatically",
                                    "$5.00", "10", "$50.00"
                                },
                                new[] {"QRS557", "Another description", "$100.00", "15", "$1,500.00"},
                                new[] {"XYZ999", "Some stuff", "$1.00", "2", "$2.00"}
                            };

            FileStream fs = new FileStream(string.Format(@"pdf\{0}__SimpleTable11_{1:yyyyMMddhhmmss}.pdf", ClassName, DateTime.Now), FileMode.Create);

            PdfDocument pdfDoc = new PdfDocument(new PdfWriter(fs));
            Document doc = new Document(pdfDoc);

            Table table = new Table(UnitValue.CreatePercentArray(new float[] { 1, 2, 1, 1, 1 }));

            table.AddCell(CreateCellForSimpleTable11("SKU", 2, 1, TextAlignment.LEFT));
            table.AddCell(CreateCellForSimpleTable11("Description", 2, 1, TextAlignment.LEFT));
            table.AddCell(CreateCellForSimpleTable11("Unit Price", 2, 1, TextAlignment.LEFT));
            table.AddCell(CreateCellForSimpleTable11("Quantity", 2, 1, TextAlignment.LEFT));
            table.AddCell(CreateCellForSimpleTable11("Extension", 2, 1, TextAlignment.LEFT));

            foreach (String[] row in data)
            {
                table.AddCell(CreateCellForSimpleTable11(row[0], 1, 1, TextAlignment.LEFT));
                table.AddCell(CreateCellForSimpleTable11(row[1], 1, 1, TextAlignment.LEFT));
                table.AddCell(CreateCellForSimpleTable11(row[2], 1, 1, TextAlignment.RIGHT));
                table.AddCell(CreateCellForSimpleTable11(row[3], 1, 1, TextAlignment.RIGHT));
                table.AddCell(CreateCellForSimpleTable11(row[4], 1, 1, TextAlignment.RIGHT));
            }

            table.AddCell(CreateCellForSimpleTable11("Totals", 2, 4, TextAlignment.LEFT));
            table.AddCell(CreateCellForSimpleTable11("$1,552.00", 2, 1, TextAlignment.RIGHT));

            doc.Add(table);

            doc.Close();
        }

        private static Cell CreateCellForSimpleTable11(String content, float borderWidth, int colspan, TextAlignment? alignment)
        {
            Cell cell = new Cell(1, colspan).Add(new Paragraph(content));
            cell.SetTextAlignment(alignment);
            cell.SetBorder(new SolidBorder(borderWidth));
            return cell;
        }

        public void SimpleTable12()
        {
            FileStream fs = new FileStream(string.Format(@"pdf\{0}__SimpleTable12_{1:yyyyMMddhhmmss}.pdf", ClassName, DateTime.Now), FileMode.Create);

            PdfDocument pdfDoc = new PdfDocument(new PdfWriter(fs));
            Document doc = new Document(pdfDoc, PageSize.A4.Rotate());

            PdfFont font = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);

            Table table = new Table(UnitValue.CreatePercentArray(8)).UseAllAvailableWidth();

            table.AddCell(CreateCellForSimpleTable12("Examination", 1, 2, 15));
            table.AddCell(CreateCellForSimpleTable12("Board", 1, 2, 15));
            table.AddCell(CreateCellForSimpleTable12("Month and Year of Passing", 1, 2, 15));
            table.AddCell(CreateCellForSimpleTable12("", 1, 1, 1));
            table.AddCell(CreateCellForSimpleTable12("Marks", 2, 1, 1));
            table.AddCell(CreateCellForSimpleTable12("Percentage", 1, 2, 15));
            table.AddCell(CreateCellForSimpleTable12("Class / Grade", 1, 2, 15));
            table.AddCell(CreateCellForSimpleTable12("", 1, 1, 15));
            table.AddCell(CreateCellForSimpleTable12("Obtained", 1, 1, 15));
            table.AddCell(CreateCellForSimpleTable12("Out of", 1, 1, 15));
            table.AddCell(CreateCellForSimpleTable12("12th / I.B. Diploma", 1, 1, 15));
            table.AddCell(CreateCellForSimpleTable12("", 1, 1, 15));
            table.AddCell(CreateCellForSimpleTable12("", 1, 1, 15));
            table.AddCell(CreateCellForSimpleTable12("Aggregate (all subjects)", 1, 1, 15));
            table.AddCell(CreateCellForSimpleTable12("", 1, 1, 15));
            table.AddCell(CreateCellForSimpleTable12("", 1, 1, 15));
            table.AddCell(CreateCellForSimpleTable12("", 1, 1, 15));
            table.AddCell(CreateCellForSimpleTable12("", 1, 1, 15));

            doc.Add(table);

            doc.Close();

        }

        private static Cell CreateCellForSimpleTable12(String content, int colspan, int rowspan, int border)
        {
            PdfFont font = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);

            Cell cell = new Cell(rowspan, colspan).Add(new Paragraph(content).SetFont(font).SetFontSize(10));
            cell
                .SetBorder(Border.NO_BORDER)
                .SetTextAlignment(TextAlignment.CENTER);

            if (8 == (border & 8))
            {
                cell.SetBorderRight(new SolidBorder(1));
                cell.SetBorderBottom(new SolidBorder(1));
            }

            if (4 == (border & 4))
            {
                cell.SetBorderLeft(new SolidBorder(1));
            }

            if (2 == (border & 2))
            {
                cell.SetBorderBottom(new SolidBorder(1));
            }

            if (1 == (border & 1))
            {
                cell.SetBorderTop(new SolidBorder(1));
            }

            return cell;
        }

        public void SimpleTable2()
        {
            FileStream fs = new FileStream(string.Format(@"pdf\{0}__SimpleTable2_{1:yyyyMMddhhmmss}.pdf", ClassName, DateTime.Now), FileMode.Create);

            PdfDocument pdfDoc = new PdfDocument(new PdfWriter(fs));
            Document doc = new Document(pdfDoc);

            Table table = new Table(UnitValue.CreatePercentArray(8)).UseAllAvailableWidth();

            Cell cell = new Cell(2, 1).Add(new Paragraph("hi"));
            table.AddCell(cell);

            for (int i = 0; i < 14; i++)
            {
                table.AddCell("hi");
            }

            doc.Add(table);

            doc.Close();
        }

        public void SimpleTable9()
        {
            FileStream fs = new FileStream(string.Format(@"pdf\{0}__SimpleTable9_{1:yyyyMMddhhmmss}.pdf", ClassName, DateTime.Now), FileMode.Create);

            PdfDocument pdfDoc = new PdfDocument(new PdfWriter(fs));
            Document doc = new Document(pdfDoc);

            doc.Add(new Paragraph("With 3 columns:"));

            Table table = new Table(UnitValue.CreatePercentArray(new float[] { 10, 10, 80 }));
            table.SetMarginTop(5);
            table.AddCell("Col a");
            table.AddCell("Col b");
            table.AddCell("Col c");
            table.AddCell("Value a");
            table.AddCell("Value b");
            table.AddCell("This is a long description for column c. "
                          + "It needs much more space hence we made sure that the third column is wider.");
            doc.Add(table);

            doc.Add(new Paragraph("With 2 columns:"));

            table = new Table(UnitValue.CreatePercentArray(2)).UseAllAvailableWidth();
            table.SetMarginTop(5);
            table.AddCell("Col a");
            table.AddCell("Col b");
            table.AddCell("Value a");
            table.AddCell("Value b");
            table.AddCell(new Cell(1, 2).Add(new Paragraph("Value b")));
            table.AddCell(new Cell(1, 2).Add(new Paragraph("This is a long description for column c. "
                                                           + "It needs much more space hence we made sure that the third column is wider.")));
            table.AddCell("Col a");
            table.AddCell("Col b");
            table.AddCell("Value a");
            table.AddCell("Value b");
            table.AddCell(new Cell(1, 2).Add(new Paragraph("Value b")));
            table.AddCell(new Cell(1, 2).Add(new Paragraph("This is a long description for column c. "
                                                           + "It needs much more space hence we made sure that the third column is wider.")));

            doc.Add(table);

            doc.Close();
        }

        public void TableMeasurements()
        {
            FileStream fs = new FileStream(string.Format(@"pdf\{0}__TableMeasurements_{1:yyyyMMddhhmmss}.pdf", ClassName, DateTime.Now), FileMode.Create);

            PdfDocument pdfDoc = new PdfDocument(new PdfWriter(fs));
            Document doc = new Document(pdfDoc);

            Table table = new Table(UnitValue.CreatePercentArray(10));
            table.SetWidth(MillimetersToPointsForTableMeasurements(100));
            table.AddCell(GetCellForTableMeasurements(10));
            table.AddCell(GetCellForTableMeasurements(5));
            table.AddCell(GetCellForTableMeasurements(3));
            table.AddCell(GetCellForTableMeasurements(2));
            table.AddCell(GetCellForTableMeasurements(3));
            table.AddCell(GetCellForTableMeasurements(5));
            table.AddCell(GetCellForTableMeasurements(1));

            doc.Add(table);

            doc.Close();
        }

        private static float MillimetersToPointsForTableMeasurements(float value)
        {
            return (value / 25.4f) * 72f;
        }

        private static Cell GetCellForTableMeasurements(int cm)
        {
            Cell cell = new Cell(1, cm);
            Paragraph p = new Paragraph(String.Format("{0}mm", 10 * cm)).SetFontSize(8);
            p.SetTextAlignment(TextAlignment.CENTER);
            p.SetMultipliedLeading(0.5f);
            p.SetMarginTop(0);
            cell.Add(p);
            return cell;
        }
    }
}
