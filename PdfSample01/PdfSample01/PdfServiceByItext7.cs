using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Kernel.Colors;
using iText.Kernel.Pdf.Canvas;
using iText.Layout.Properties;
using iText.Layout.Renderer;

namespace PdfSample01
{
    public class PdfServiceByItext7
    {
        // https://itextpdf.com/en/resources/examples/itext-7/itext-7-jump-start-tutorial-chapter-1
        public virtual void CreatePdf()
        {
            FileStream fs = new FileStream(string.Format("First itext7 PDF document_{0:yyyyMMddhhmmss}.pdf", DateTime.Now), FileMode.Create);
        
            //Initialize PDF writer
            PdfWriter writer = new PdfWriter(fs);
            //Initialize PDF document
            PdfDocument pdf = new PdfDocument(writer);
            // Initialize document
            Document document = new Document(pdf);
            //Add paragraph to the document
            document.Add(new Paragraph("Hello World!"));

            // add table
            // https://itextpdf.com/en/resources/examples/itext-7/table-and-cell-events-draw-borders
            Table table = new Table(UnitValue.CreatePercentArray(1)).UseAllAvailableWidth();
            //Cell cell = GetCell("The title of this cell is title 1", "title 1");
            //table.AddCell(cell);
            //cell = GetCell("The title of this cell is title 2", "title 2");
            //table.AddCell(cell);
            //cell = GetCell("The title of this cell is title 3", "title 3");
            //table.AddCell(cell);
            Cell cell = GetCell("The title of this cell is title 1");
            table.AddCell(cell);
            cell = GetCell("The title of this cell is title 2");
            table.AddCell(cell);
            cell = GetCell("The title of this cell is title 3");
            table.AddCell(cell);
            document.Add(table);

            //Close document
            document.Close();
        }

        private class CellTitleRenderer : CellRenderer
        {
            protected string title;

            public CellTitleRenderer(Cell modelElement, String title)
                : base(modelElement)
            {
                this.title = title;
            }

            // If renderer overflows on the next area, iText uses getNextRender() method to create a renderer for the overflow part.
            // If getNextRenderer isn't overriden, the default method will be used and thus a default rather than custom
            // renderer will be created
            public override IRenderer GetNextRenderer()
            {
                return new CellTitleRenderer((Cell)modelElement, title);
            }

            public override void DrawBorder(DrawContext drawContext)
            {
                PdfPage currentPage = drawContext.GetDocument().GetPage(GetOccupiedArea().GetPageNumber());

                // Create an above canvas in order to draw above borders.
                // Notice: bear in mind that iText draws cell borders on its TableRenderer level.
                PdfCanvas aboveCanvas = new PdfCanvas(currentPage.NewContentStreamAfter(), currentPage.GetResources(),
                    drawContext.GetDocument());
                new Canvas(aboveCanvas, drawContext.GetDocument(), GetOccupiedAreaBBox())
                    .Add(new Paragraph(title)
                        .SetMultipliedLeading(1)
                        .SetMargin(0)
                        .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
                        .SetFixedPosition(GetOccupiedAreaBBox().GetLeft() + 5,
                            GetOccupiedAreaBBox().GetTop() - 8, 30));
            }
        }

        private static Cell GetCell(string content, string title)
        {
            Cell cell = new Cell().Add(new Paragraph(content));
            cell.SetNextRenderer(new CellTitleRenderer(cell, title));
            cell.SetPaddingTop(8).SetPaddingBottom(8);
            return cell;
        }

        private static Cell GetCell(string content)
        {
            Cell cell = new Cell().Add(new Paragraph(content));
            cell.SetNextRenderer(new CellRenderer(cell));
            cell.SetPaddingTop(8).SetPaddingBottom(8);
            return cell;
        }

    }
}
