﻿using iText.Kernel.Events;
using iText.Kernel.Font;
using iText.Kernel.Pdf.Canvas;
using iText.Layout;
using iText.Layout.Element;
using TextAlignment = iText.Layout.Properties.TextAlignment;

namespace ExchangeApp.App.Services;

public class TotalBalanceFooterEventHandler : IEventHandler
{
    private const int SideMargins = 20;
    private readonly PdfFont _font;

    public TotalBalanceFooterEventHandler(PdfFont font)
    {
        _font = font;
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

        new Canvas(pdfCanvas, pageSize, true)
            .SetFont(_font)
            .SetFontSize(8)
            .ShowTextAligned(new Paragraph($"{pageNumber}"), x, y, TextAlignment.RIGHT);

        var lineY = pageSize.GetBottom() + 20;
        pdfCanvas.MoveTo(pageSize.GetLeft() + SideMargins, lineY);
    }
}