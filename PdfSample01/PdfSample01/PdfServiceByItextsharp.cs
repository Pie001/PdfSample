//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using iTextSharp;
//using iTextSharp.text;
//using iTextSharp.text.pdf;

//namespace PdfSample01
//{
//    public class PdfServiceByItextsharp
//    {
//        public void CreatePdfFile()
//        {
//            System.IO.FileStream fs = new FileStream("First PDF document.pdf", FileMode.Create);

//            // Create an instance of the document class which represents the PDF document itself.  
//            Document document = new Document(PageSize.A4, 25, 25, 30, 30);
//            // Create an instance to the PDF file by creating an instance of the PDF   
//            // Writer class using the document and the filestrem in the constructor.  

//            PdfWriter writer = PdfWriter.GetInstance(document, fs);

//            // Open the document to enable you to write to the document  
//            document.Open();
//            // Add a simple and wellknown phrase to the document in a flow layout manner  
//            document.Add(new Paragraph("Hello World!"));
//            // Close the document  
//            document.Close();
//            // Close the writer instance  
//            writer.Close();
//            // Always close open filehandles explicity  
//            fs.Close();

//        }
//    }
//}
