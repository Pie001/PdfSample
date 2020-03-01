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

// https://itextpdf.com/en/resources/examples/itext-7/large-tables
namespace PdfSample01.Itext7
{
    public class LargeTables
    {
        private string ClassName = "3_LargeTables";

        public void IncompleteTable()
        {
            FileStream fs = new FileStream(string.Format(@"pdf\{0}__SplittingAndRowspan_{1:yyyyMMddhhmmss}.pdf", ClassName, DateTime.Now), FileMode.Create);

            PdfDocument pdfDoc = new PdfDocument(new PdfWriter(fs));
            Document doc = new Document(pdfDoc);

            // The second argument determines 'large table' functionality is used
            // It defines whether parts of the table will be written before all data is added.
            Table table = new Table(UnitValue.CreatePercentArray(5), true);

            for (int i = 0; i < 5; i++)
            {
                table.AddHeaderCell(new Cell().SetKeepTogether(true).Add(new Paragraph("Header " + i)));
            }

            // For the "large tables" they shall be added to the document before its child elements are populated
            doc.Add(table);

            for (int i = 0; i < 500; i++)
            {
                if (i % 5 == 0)
                {
                    // Flushes the current content, e.g. places it on the document.
                    // Please bear in mind that the method (alongside complete()) make sense only for 'large tables'
                    table.Flush();
                }

                table.AddCell(new Cell().SetKeepTogether(true).Add(new Paragraph("Test " + i)
                    .SetMargins(0, 0, 0, 0)));
            }

            // Flushes the rest of the content and indicates that no more content will be added to the table
            table.Complete();

            doc.Close();

        }

    }
}
