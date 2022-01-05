﻿using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.JSInterop;


namespace Daily_Blazor_App.PDF
{
    using Daily_Blazor_App.Data;
    using Helpers;

    public class Report
    {
        private int _pagenumber;
        private Person _person;

        public void Generate(IJSRuntime js, Person person, int pagenumber, string filename = "report.pdf")
        {
            _pagenumber = pagenumber;
            _person = person;

            js.InvokeVoidAsync("jsSaveAsFile",
                                filename,
                                Convert.ToBase64String(ReportPDF())
                                );
        }

        public void OpenToIframe(IJSRuntime js, int pagenumber, string idiFrame)
        {
            _pagenumber = pagenumber;

            js.InvokeVoidAsync("jsOpenToIframe",
                                idiFrame,
                                Convert.ToBase64String(ReportPDF())
                                );
        }
        public void OpenNewTab(IJSRuntime js, int pagenumber, string filename = "report.pdf")
        {
            _pagenumber = pagenumber;

            js.InvokeVoidAsync("jsOpenIntoNewTab",
                                filename,
                                Convert.ToBase64String(ReportPDF())
                                );
        }


        private byte[] ReportPDF()
        {
            var memoryStream = new MemoryStream();

            // Marge in centimeter, then I convert with .ToDpi()
            float margeLeft = 1.5f;
            float margeRight = 1.5f;
            float margeTop = 1.0f;
            float margeBottom = 1.0f;

            Document pdf = new Document(
                                    PageSize.A4,
                                    margeLeft.ToDpi(),
                                    margeRight.ToDpi(),
                                    margeTop.ToDpi(),
                                    margeBottom.ToDpi()
                                   );

            pdf.AddTitle("Blazor-PDF");
            pdf.AddAuthor("Christophe Peugnet");
            pdf.AddCreationDate();
            pdf.AddKeywords("blazor");
            pdf.AddSubject("Create a pdf file with iText");

            PdfWriter writer = PdfWriter.GetInstance(pdf, memoryStream);

            //HEADER and FOOTER
            var fontStyle = FontFactory.GetFont("Arial", 16, BaseColor.White);
            var labelHeader = new Chunk($"Header Blazor PDF for {_person.LastName}", fontStyle);
            HeaderFooter header = new HeaderFooter(new Phrase(labelHeader), false)
            {
                BackgroundColor = new BaseColor(133, 76, 199),
                Alignment = Element.ALIGN_CENTER,
                Border = Rectangle.NO_BORDER
            };
            //header.Border = Rectangle.NO_BORDER;
            pdf.Header = header;


            var labelFooter = new Chunk("Page", fontStyle);
            HeaderFooter footer = new HeaderFooter(new Phrase(labelFooter), true)
            {
                Border = Rectangle.NO_BORDER,
                Alignment = Element.ALIGN_RIGHT
            };
            pdf.Footer = footer;

            pdf.Open();


            if (_pagenumber == 1)
                Page1.PageText(pdf);
            //else if (_pagenumber == 2)
            //    Page2.PageBookmark(pdf);
            //else if (_pagenumber == 3)
            //    Page3.PageImage(pdf, writer);
            //else if (_pagenumber == 4)
            //    Page4.PageTable(pdf, writer);
            //else if (_pagenumber == 5)
            //    Page5.PageFonts(pdf, writer);
            //else if (_pagenumber == 6)
            //    Page6.PageList(pdf);
            //else if (_pagenumber == 7)
            //    page7.PageShapes(pdf, writer);

            pdf.Close();

            return memoryStream.ToArray();
        }
    }
}
