using System.Resources;
using ExchangeApp.App.Resources.Texts;
using iText.Kernel.Events;
using iText.Kernel.Font;
using iText.Kernel.Pdf.Canvas;
using iText.Layout;
using iText.Layout.Element;
using TextAlignment = iText.Layout.Properties.TextAlignment;

namespace ExchangeApp.App.Services.HeaderAndFooters;

public class TotalBalanceFooterEventHandler : IEventHandler
{
    private const int SideMargins = 20;
    private readonly PdfFont _font;
    private readonly int _fontSize;

    public TotalBalanceFooterEventHandler(PdfFont font, int fontSize)
    {
        _font = font;
        _fontSize = fontSize;
    }

    public void HandleEvent(Event @event)
    {
        var docEvent = (PdfDocumentEvent)@event;
        var pdfDoc = docEvent.GetDocument();
        var page = docEvent.GetPage();

        var pageNumber = pdfDoc.GetPageNumber(page);

        var pdfCanvas = new PdfCanvas(page);
        var pageSize = page.GetPageSize();

        var x = pageSize.GetRight() - SideMargins;
        var y = pageSize.GetBottom() + 5;

        var rm = new ResourceManager(typeof(PrinterResources));

        new Canvas(pdfCanvas, pageSize, true)
            .SetFont(_font)
            .SetFontSize(_fontSize)
            .ShowTextAligned(new Paragraph($"{rm.GetString("PageLabel")} {pageNumber}"), x, y, TextAlignment.RIGHT);

        var lineY = pageSize.GetBottom() + 20;
        pdfCanvas.MoveTo(pageSize.GetLeft() + SideMargins, lineY);
        pdfCanvas.LineTo(pageSize.GetRight() - SideMargins, lineY);
        pdfCanvas.Stroke();

        pdfCanvas.Release();
    }
}