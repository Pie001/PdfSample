using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PdfSample01.Itext7;
using PdfSample01.Receipt;

namespace PdfSample01
{
    class Program
    {
        // https://itextpdf.com/en/resources/api-documentation/itext-7-net

        static void Main(string[] args)
        {
            //PdfServiceByItextsharp pdfService = new PdfServiceByItextsharp();
            //pdfService.CreatePdfFile();

            //PdfServiceByItext7 pdfServiceByItext7 = new PdfServiceByItext7();
            //pdfServiceByItext7.CreatePdf();

            //CellAndTableWidths cellAndTableWidths = new CellAndTableWidths();
            //cellAndTableWidths.ColumnWidthExample();
            //cellAndTableWidths.FullPageTable();
            //cellAndTableWidths.RightCornerTable();
            //cellAndTableWidths.SimpleTable3();

            //ColspanAndRowspan colspanAndRowspan = new ColspanAndRowspan();
            //colspanAndRowspan.ColspanRowspan();
            //colspanAndRowspan.RowspanAbsolutePosition();
            //colspanAndRowspan.SimpleRowColspan();
            //colspanAndRowspan.SimpleTable11();
            //colspanAndRowspan.SimpleTable12();
            //colspanAndRowspan.SimpleTable2();
            //colspanAndRowspan.SimpleTable9();
            //colspanAndRowspan.TableMeasurements();

            //LargeTables largeTables = new LargeTables();
            //largeTables.IncompleteTable();

            //RowspanAndSplitting rowspanAndSplitting = new RowspanAndSplitting();
            //rowspanAndSplitting.SplittingAndRowspan();
            //rowspanAndSplitting.SplittingNestedTable1();
            //rowspanAndSplitting.SplittingNestedTable2();

            PdfService pdfService = new PdfService();
            PdfServiceMultiPages pdfServiceMultiPages = new PdfServiceMultiPages();
            ReceiptModel receiptModel = new ReceiptModel();


            System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
            long elapse;
            stopwatch.Start();

            //pdfService.CreateReceiptPdf(receiptModel);
            pdfServiceMultiPages.CreateReceiptPdf(receiptModel);

            elapse = stopwatch.ElapsedMilliseconds;
            CSiteUtility.WriteLog("500개 발행 : stop", elapse.ToString());

        }
    }
}
