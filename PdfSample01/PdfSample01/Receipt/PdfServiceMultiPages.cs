using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Kernel.Colors;
using iText.Kernel.Pdf.Canvas;
using iText.Layout.Properties;
using iText.Layout.Renderer;
using iText.Layout.Borders;
using iText.IO.Font;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.IO.Image;

namespace PdfSample01.Receipt
{
    public class PdfServiceMultiPages
    {
        public static readonly String FONT = "../../Font/malgun.ttf";

        public void CreateReceiptPdf(ReceiptModel receiptModel)
        {
            FileStream fs = new FileStream(string.Format(@"receiptPdf\ReceiptPDF_{0:yyyyMMddhhmmss}_500p.pdf", DateTime.Now), FileMode.Create);

            PdfFont baseFont = PdfFontFactory.CreateFont(FONT, PdfEncodings.IDENTITY_H);

            //Initialize PDF writer
            PdfWriter writer = new PdfWriter(fs);
            //Initialize PDF document
            PdfDocument pdf = new PdfDocument(writer);
            // Initialize document
            Document doc = new Document(pdf);
            doc.GetPageEffectiveArea(PageSize.A4);
            doc.SetTopMargin(20f);
            doc.SetRightMargin(40f);
            doc.SetBottomMargin(30f);
            doc.SetLeftMargin(40f);

            doc.SetFont(baseFont);
            doc.SetFontSize(8.6f);

            for (int i = 0; i < 500; i++)
            {
                Paragraph p;

                // 여기부터 영수증 pdf
                p = GetBoldMainTitle("기부금 영수증", 16f);
                p.SetRelativePosition(200, 24, 0, 0);
                doc.Add(p);

                Table receiptCodeTable = new Table(new float[] { 80f, 80f });
                receiptCodeTable.SetWidth(160f); // width 100%
                receiptCodeTable.AddCell(GetCellForDetailTable("일련번호", TextAlignment.CENTER, RowLocation.MIDDLE_ROW, CellLocation.MIDDLE_CELL));
                receiptCodeTable.AddCell(GetCellForDetailTable(receiptModel.ReceiptCode, TextAlignment.LEFT, RowLocation.MIDDLE_ROW, CellLocation.MIDDLE_CELL));
                receiptCodeTable.SetMarginBottom(12f); // 아래 테이블과의 높이 조절
                doc.Add(receiptCodeTable);

                // 1. 기부자
                p = GetBoldTitle("1. 기부자", 9.5f);
                p.SetBorderTop(new SolidBorder(ColorConstants.BLACK, 0.5f));
                doc.Add(p);

                Table personTable = new Table(new float[] { 65, 140, 80, 140 });
                personTable.SetWidth(UnitValue.CreatePercentValue(100)); // width 100%
                // 1 Line
                personTable.AddCell(GetCellForDetailTable("성명(법인명)", TextAlignment.CENTER, RowLocation.HEADER, CellLocation.FIRST_CELL));
                personTable.AddCell(GetCellForDetailTable(receiptModel.Name, TextAlignment.CENTER, RowLocation.HEADER, CellLocation.MIDDLE_CELL));
                personTable.AddCell(GetCellForDetailTable("주민(사업자)등록번호", TextAlignment.CENTER, RowLocation.HEADER, CellLocation.MIDDLE_CELL));
                personTable.AddCell(GetCellForDetailTable(receiptModel.PersonalNumber, TextAlignment.CENTER, RowLocation.HEADER, CellLocation.LAST_CELL));

                // 2 Line
                personTable.AddCell(GetCellForDetailTable("주소(소재지)", TextAlignment.CENTER, RowLocation.LAST_ROW, CellLocation.FIRST_CELL));
                personTable.AddCell(GetCellForDetailTable(receiptModel.Address, TextAlignment.LEFT, RowLocation.LAST_ROW, CellLocation.LAST_CELL, 1, 3));
                personTable.SetMarginBottom(2f); // 아래 테이블과의 높이 조절
                doc.Add(personTable);

                // 2. 기부금 단체
                Table OrganTable = new Table(new float[] { 65, 140, 80, 140 });
                OrganTable.SetWidth(UnitValue.CreatePercentValue(100)); // width 100%
                // title
                OrganTable.AddCell(GetCellForDetailTable("2. 기부금 단체", TextAlignment.LEFT, RowLocation.TITLE, CellLocation.FIRST_CELL, 1, 4));
                // 1 Line
                OrganTable.AddCell(GetCellForDetailTable("단체명", TextAlignment.CENTER, RowLocation.HEADER, CellLocation.FIRST_CELL));
                OrganTable.AddCell(GetCellForDetailTable(receiptModel.OrganName, TextAlignment.CENTER, RowLocation.HEADER, CellLocation.MIDDLE_CELL));
                OrganTable.AddCell(GetCellForDetailTable("사업자(고유)등록번호", TextAlignment.CENTER, RowLocation.HEADER, CellLocation.MIDDLE_CELL));
                OrganTable.AddCell(GetCellForDetailTable(receiptModel.OrganBusinessNo, TextAlignment.CENTER, RowLocation.HEADER, CellLocation.LAST_CELL));
                // 2 Line
                OrganTable.AddCell(GetCellForDetailTable("소재지", TextAlignment.CENTER, RowLocation.MIDDLE_ROW, CellLocation.FIRST_CELL));
                OrganTable.AddCell(GetCellForDetailTable(receiptModel.OrganAddress, TextAlignment.LEFT, RowLocation.MIDDLE_ROW, CellLocation.LAST_CELL, 1, 3));
                // 3 Line
                OrganTable.AddCell(GetCellForDetailTable("근거법령", TextAlignment.CENTER, RowLocation.LAST_ROW, CellLocation.FIRST_CELL));
                OrganTable.AddCell(GetCellForDetailTable(receiptModel.OrganLaw, TextAlignment.LEFT, RowLocation.LAST_ROW, CellLocation.LAST_CELL, 1, 3));
                OrganTable.SetMarginBottom(2f); // 아래 테이블과의 높이 조절
                doc.Add(OrganTable);

                // 3. 기부금 모집처 (언론기관 등)
                Table distributorTable = new Table(new float[] { 65, 140, 80, 140 });
                distributorTable.SetWidth(UnitValue.CreatePercentValue(100)); // width 100%
                // title
                distributorTable.AddCell(GetCellForDetailTable("3. 기부금 모집처 (언론기관 등)", TextAlignment.LEFT, RowLocation.TITLE, CellLocation.FIRST_CELL, 1, 4));

                // 1 Line
                distributorTable.AddCell(GetCellForDetailTable("단체명", TextAlignment.CENTER, RowLocation.HEADER, CellLocation.FIRST_CELL));
                distributorTable.AddCell(GetCellForDetailTable(receiptModel.DistributorName, TextAlignment.CENTER, RowLocation.HEADER, CellLocation.MIDDLE_CELL));
                distributorTable.AddCell(GetCellForDetailTable("사업자등록번호", TextAlignment.CENTER, RowLocation.HEADER, CellLocation.MIDDLE_CELL));
                distributorTable.AddCell(GetCellForDetailTable(receiptModel.DistributorBusinessno, TextAlignment.CENTER, RowLocation.HEADER, CellLocation.LAST_CELL));

                // 2 Line
                distributorTable.AddCell(GetCellForDetailTable("소재지", TextAlignment.CENTER, RowLocation.LAST_ROW, CellLocation.FIRST_CELL));
                distributorTable.AddCell(GetCellForDetailTable(receiptModel.DistributorAddress, TextAlignment.LEFT, RowLocation.LAST_ROW, CellLocation.LAST_CELL, 1, 3));
                distributorTable.SetMarginBottom(2f); // 아래 테이블과의 높이 조절
                doc.Add(distributorTable);

                // 4. 기부내용
                Table donateDetailTable = new Table(new float[] { 65, 30, 30, 65, 80, 30, 60, 70 });
                donateDetailTable.SetWidth(UnitValue.CreatePercentValue(100)); // width 100%
                // title
                donateDetailTable.AddCell(GetCellForDetailTable("4. 기부내용", TextAlignment.LEFT, RowLocation.TITLE, CellLocation.FIRST_CELL, 1, 8));

                // Header 1Line
                donateDetailTable.AddCell(GetCellForDetailTable("유형", TextAlignment.CENTER, RowLocation.HEADER, CellLocation.FIRST_CELL, 2));
                donateDetailTable.AddCell(GetCellForDetailTable("코드", TextAlignment.CENTER, RowLocation.HEADER, CellLocation.MIDDLE_CELL, 2));
                donateDetailTable.AddCell(GetCellForDetailTable("구분", TextAlignment.CENTER, RowLocation.HEADER, CellLocation.MIDDLE_CELL, 2));
                donateDetailTable.AddCell(GetCellForDetailTable("연월일", TextAlignment.CENTER, RowLocation.HEADER, CellLocation.MIDDLE_CELL, 2));
                donateDetailTable.AddCell(GetCellForDetailTable("내용", TextAlignment.CENTER, RowLocation.HEADER, CellLocation.MIDDLE_CELL, 1, 3));
                donateDetailTable.AddCell(GetCellForDetailTable("금액", TextAlignment.CENTER, RowLocation.HEADER, CellLocation.LAST_CELL, 2));
                // Header 2Line
                donateDetailTable.AddCell(GetCellForDetailTable("품명", TextAlignment.CENTER, RowLocation.HEADER, CellLocation.MIDDLE_CELL));
                donateDetailTable.AddCell(GetCellForDetailTable("수량", TextAlignment.CENTER, RowLocation.HEADER, CellLocation.MIDDLE_CELL));
                donateDetailTable.AddCell(GetCellForDetailTable("단가", TextAlignment.CENTER, RowLocation.HEADER, CellLocation.MIDDLE_CELL));

                decimal totalPrice = receiptModel.DonateDetailList.Sum(x => x.PayPrice);

                foreach (DonateDetail donateDetail in receiptModel.DonateDetailList)
                {
                    donateDetailTable.AddCell(GetCellForDetailTable(donateDetail.DonateType, TextAlignment.CENTER, RowLocation.MIDDLE_ROW, CellLocation.FIRST_CELL));
                    donateDetailTable.AddCell(GetCellForDetailTable(donateDetail.DonateCode, TextAlignment.CENTER, RowLocation.MIDDLE_ROW, CellLocation.MIDDLE_CELL));
                    donateDetailTable.AddCell(GetCellForDetailTable(donateDetail.DonateDivision, TextAlignment.CENTER, RowLocation.MIDDLE_ROW, CellLocation.MIDDLE_CELL));
                    donateDetailTable.AddCell(GetCellForDetailTable(string.Format("{0:yyyy-MM-dd}", donateDetail.DonateDate), TextAlignment.CENTER, RowLocation.MIDDLE_ROW, CellLocation.MIDDLE_CELL));
                    donateDetailTable.AddCell(GetCellForDetailTable(donateDetail.ProductName, TextAlignment.CENTER, RowLocation.MIDDLE_ROW, CellLocation.MIDDLE_CELL));
                    donateDetailTable.AddCell(GetCellForDetailTable(string.Format("{0}", donateDetail.Quantity), TextAlignment.CENTER, RowLocation.MIDDLE_ROW, CellLocation.MIDDLE_CELL));
                    donateDetailTable.AddCell(GetCellForDetailTable(string.Format("{0:#,##0}", donateDetail.UnitPrice), TextAlignment.RIGHT, RowLocation.MIDDLE_ROW, CellLocation.MIDDLE_CELL));
                    donateDetailTable.AddCell(GetCellForDetailTable(string.Format("{0:#,##0}", donateDetail.PayPrice), TextAlignment.RIGHT, RowLocation.MIDDLE_ROW, CellLocation.LAST_CELL));
                }
                // 합계 Line
                donateDetailTable.AddCell(GetCellForDetailTable("합계", TextAlignment.CENTER, RowLocation.LAST_ROW, CellLocation.FIRST_CELL, 1, 7));
                donateDetailTable.AddCell(GetCellForDetailTable(string.Format("{0:#,##0}", totalPrice), TextAlignment.RIGHT, RowLocation.LAST_ROW, CellLocation.LAST_CELL));
                doc.Add(donateDetailTable);


                //Add paragraph to the document
                doc.Add(new Paragraph("「소득세법」 제34조, 「조세특례제한법」 제76조ㆍ제88조의4 및 「법인세법」 제24조에 따른 기부금을 위와 같이 기부하였음을 증명하여 주시기 바랍니다."));

                Table table = new Table(1);
                table.SetWidth(UnitValue.CreatePercentValue(100)); // width 100%
                table.AddCell(GetCellForNoBorder("2019년 12월 30일", TextAlignment.RIGHT));
                doc.Add(table);

                table = new Table(new float[] { 33, 35, 32 });
                table.SetWidth(UnitValue.CreatePercentValue(100)); // width 100%
                table.AddCell(GetCellForUnderline("신청인", TextAlignment.RIGHT, true));
                table.AddCell(GetCellForUnderline("신청인 이름", TextAlignment.RIGHT, true));
                table.AddCell(GetCellForUnderline("(서명 또는 인)", TextAlignment.RIGHT, true));
                doc.Add(table);

                p = new Paragraph("위와 같이 기부금을 기부받았음을 증명합니다.");
                p.SetPaddingTop(6f);
                doc.Add(p);

                table = new Table(1);
                table.SetWidth(UnitValue.CreatePercentValue(100)); // width 100%
                table.AddCell(GetCellForNoBorder("2019년 12월 13일", TextAlignment.RIGHT));
                doc.Add(table);
            
                // 높이 조절
                doc.Add(new Paragraph(string.Empty).SetPaddingTop(20f));

                // 인감 이미지
                Image img = new Image(ImageDataFactory.Create(@"pdf\image\image.png"));
                img.SetWidth(75f); // 이미지 사이즈 고정
                img.SetAutoScale(false);
                img.SetFixedPosition(485f, 225f);
                doc.Add(img);

                // 인감 위에 (서명 또는 인) 문자가 있어야 해서 인감 이미지 처리 뒤에 pdf에 추가한다.
                table = new Table(new float[] { 33, 35, 32 });
                table.SetWidth(UnitValue.CreatePercentValue(100)); // width 100%
                table.AddCell(GetCellForUnderline("기부금 수령인", TextAlignment.RIGHT, false));
                table.AddCell(GetCellForUnderline("도너스", TextAlignment.RIGHT, false));
                table.AddCell(GetCellForUnderline("(서명 또는 인)", TextAlignment.RIGHT, false));
                doc.Add(table);

                if(i < 499)
                    doc.Add(new AreaBreak(AreaBreakType.NEXT_PAGE));

            }
            //Close document
            doc.Close();
        }

        public Paragraph GetBoldTitle(string text, float fontSize)
        {
            Paragraph p = new Paragraph();
            PdfFont baseFont = PdfFontFactory.CreateFont(FONT, PdfEncodings.IDENTITY_H);
            Text bold = new Text(text).SetFont(baseFont).SetTextRenderingMode(PdfCanvasConstants.TextRenderingMode.FILL_STROKE).SetStrokeWidth(0.3f).SetStrokeColor(DeviceGray.BLACK);
            p = new Paragraph(bold);
            p.SetFontSize(fontSize);

            return p;
        }

        public Paragraph GetBoldMainTitle(string text, float fontSize)
        {
            Paragraph p = new Paragraph();
            PdfFont baseFont = PdfFontFactory.CreateFont(FONT, PdfEncodings.IDENTITY_H);
            Text bold = new Text(text).SetFont(baseFont).SetTextRenderingMode(PdfCanvasConstants.TextRenderingMode.FILL_STROKE).SetStrokeWidth(0.5f).SetStrokeColor(DeviceGray.BLACK);
            p = new Paragraph(bold);
            p.SetFontSize(fontSize);
            p.SetPadding(0);
            p.SetMargin(0);

            return p;
        }

        public Cell GetCellForNoBorder(string text, TextAlignment alignment)
        {
            Cell cell = new Cell().Add(new Paragraph(text));
            cell.SetPadding(0);
            cell.SetTextAlignment(alignment);
            cell.SetBorder(Border.NO_BORDER);
            cell.SetPaddingTop(4f);
            cell.SetPaddingBottom(4f);
            return cell;
        }

        public Cell GetCellForUnderline(string text, TextAlignment alignment, bool isUnderline)
        {
            Cell cell = new Cell().Add(new Paragraph(text));
            cell.SetPadding(0);
            cell.SetTextAlignment(alignment);
            cell.SetBorderLeft(Border.NO_BORDER);
            cell.SetBorderRight(Border.NO_BORDER);
            cell.SetBorderTop(Border.NO_BORDER);

            if (isUnderline)
            {
                cell.SetBorderBottom(new SolidBorder(ColorConstants.BLACK, 0.5f));
            }
            else
            {
                cell.SetBorderBottom(Border.NO_BORDER);
            }

            cell.SetPaddingTop(4f);
            cell.SetPaddingBottom(4f);
            return cell;
        }

        public Cell GetCellForDetailTable(string text, TextAlignment alignment, RowLocation rowLocation = RowLocation.MIDDLE_ROW, CellLocation cellLocation = CellLocation.MIDDLE_CELL, int rowspan = 1, int colspan = 1)
        {
            PdfFont baseFont = PdfFontFactory.CreateFont(FONT, PdfEncodings.IDENTITY_H);

            Cell cell = new Cell(rowspan, colspan);
            cell.SetFontSize(8f);
            cell.SetPadding(0);
            cell.SetTextAlignment(alignment);
            cell.SetVerticalAlignment(VerticalAlignment.MIDDLE);
            cell.SetBorder(new SolidBorder(ColorConstants.LIGHT_GRAY, 0.5f));

            if (rowLocation == RowLocation.TITLE)
            {
                Paragraph p = new Paragraph();
                Text bold = new Text(text).SetFont(baseFont).SetTextRenderingMode(PdfCanvasConstants.TextRenderingMode.FILL_STROKE).SetStrokeWidth(0.5f).SetStrokeColor(DeviceGray.BLACK);
                p = new Paragraph(bold);
                p.SetFontSize(9.5f);
                cell.Add(p);
                cell.SetBorderTop(new SolidBorder(ColorConstants.BLACK, 0.5f));
                cell.SetBorderLeft(Border.NO_BORDER);
                cell.SetBorderRight(Border.NO_BORDER);
            }
            else
            {
                cell.Add(new Paragraph(text));

                // 테이블의 시작 셀
                if (cellLocation == CellLocation.FIRST_CELL)
                {
                    cell.SetBorderLeft(Border.NO_BORDER);
                }
                else if (cellLocation == CellLocation.LAST_CELL)
                {
                    // 테이블의 마지막 셀
                    cell.SetBorderRight(Border.NO_BORDER);
                }

                if (rowLocation != RowLocation.HEADER)
                {
                    cell.SetPaddingTop(2f);
                    cell.SetPaddingBottom(2f);
                }

                if (rowLocation == RowLocation.LAST_ROW)
                {
                    cell.SetBorderBottom(new SolidBorder(ColorConstants.BLACK, 0.5f));
                }

                if (alignment == TextAlignment.RIGHT)
                {
                    cell.SetPaddingRight(4f);
                }

                if (alignment == TextAlignment.LEFT)
                {
                    cell.SetPaddingLeft(4f);
                }
            }

            return cell;
        }
    }
}
