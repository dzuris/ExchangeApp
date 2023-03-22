using System.Resources;
using ExchangeApp.App.Resources.Texts;
using iText.Kernel.Events;
using iText.Kernel.Font;
using iText.Kernel.Pdf.Canvas;
using iText.Layout;
using iText.Layout.Element;
using TextAlignment = iText.Layout.Properties.TextAlignment;

namespace ExchangeApp.App.Services;

public class TotalBalanceHeaderEventHandler : IEventHandler
{
    private const int SideMargins = 20;
    private readonly PdfFont _font;
    private readonly string _companyName;
    private readonly string _ico;
    private readonly DateTime _created;
    private readonly int _totalBalanceId;

    public TotalBalanceHeaderEventHandler(PdfFont font, string companyName, string ico, DateTime created, int totalBalanceId)
    {
        _font = font;
        _companyName = companyName;
        _ico = ico;
        _created = created;
        _totalBalanceId = totalBalanceId;
    }

    public void HandleEvent(Event @event)
    {
        var docEvent = (PdfDocumentEvent) @event;
        var page = docEvent.GetPage();

        var pdfCanvas = new PdfCanvas(page);
        var pageSize = page.GetPageSize();

        var x = pageSize.GetLeft();
        var y = pageSize.GetTop();

        var rm = new ResourceManager(typeof(PrinterTotalBalanceResources));

        new Canvas(pdfCanvas, pageSize, true)
            .SetFont(_font)
            .SetFontSize(8)
            .ShowTextAligned(new Paragraph($"{_companyName}, {rm.GetString("ICO_headerText")} {_ico}"),
                x + pageSize.GetWidth() / 2, y - 20, TextAlignment.CENTER)
            .ShowTextAligned(new Paragraph($"{_created}"), pageSize.GetRight() - SideMargins, y - 20,
                TextAlignment.RIGHT)
            .ShowTextAligned(new Paragraph($"{rm.GetString("DailyTotalBalance")} - {_totalBalanceId}"),
                x + pageSize.GetWidth() / 2, y - 40,
                TextAlignment.CENTER);

        var firstLineY = pageSize.GetTop() - 25;
        pdfCanvas.MoveTo(SideMargins, firstLineY);
        pdfCanvas.LineTo(pageSize.GetRight() - SideMargins, firstLineY);

        var secondLineY = pageSize.GetTop() - 45;
        pdfCanvas.MoveTo(SideMargins, secondLineY);
        pdfCanvas.LineTo(pageSize.GetRight() - SideMargins, secondLineY);
        pdfCanvas.Stroke();

        pdfCanvas.Release();
    }
}