using System.Diagnostics;
using System.Globalization;
using System.Resources;
using ExchangeApp.App.Converters;
using ExchangeApp.App.Resources.Texts;
using ExchangeApp.BL.Models.Company;
using ExchangeApp.BL.Models.Currency;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using Border = iText.Layout.Borders.Border;
using Cell = iText.Layout.Element.Cell;
using TextAlignment = iText.Layout.Properties.TextAlignment;

namespace ExchangeApp.App.Services;

public partial class PrinterService
{
    private async Task CreatePdf(List<CurrencyProfitModel> currencies, string fileName, DateTime from, DateTime until)
    {
        // Create a new PDF document with default properties
        var pdf = new PdfDocument(new PdfWriter(fileName, new WriterProperties().SetPdfVersion(PdfVersion.PDF_2_0)));
        var document = new Document(pdf, OperationDocumentPageSize);

        // Gets company and branch information
        var company = await _settingsFacade.GetCompanyDataAsync();
        var branch = await _settingsFacade.GetBranchDataAsync();

        // Gets PDF document
        document = GetProfitDocument(document, currencies, company, branch, from, until);

        document.Close();
        pdf.Close();
    }

    public async Task Print(List<CurrencyProfitModel> currencies, DateTime from, DateTime until)
    {
        var fileName = GetProfitFileName();

        await CreatePdf(currencies, fileName, from, until);

        try
        {
            var info = new ProcessStartInfo(fileName)
            {
                UseShellExecute = true
            };

            Process.Start(info);
        }
        catch (System.ComponentModel.Win32Exception ex)
        {
            Debug.WriteLine(ex.Message);
            await Application.Current?.MainPage?.DisplayAlert(
                "Error", "An error occurred while printing the file.",
                "OK")!;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            await Application.Current?.MainPage?.DisplayAlert(
                "Error", "An error 2 occurred while printing the file.",
                "OK")!;
        }
    }

    private static string GetProfitFileName()
    {
        var fileName = Path.Combine(GetTemporaryFolder(), "vypocet_zisku.pdf");
        return fileName;
    }

    private static Document GetProfitDocument(Document document, List<CurrencyProfitModel> currencies,
        CompanyDetailModel? company, BranchDetailModel? branch, DateTime from, DateTime until)
    {
        var rm = new ResourceManager(typeof(PrinterProfitResources));
        var rmPrinter = new ResourceManager(typeof(PrinterResources));

        // Setting fonts
        var boldFont = PdfFontFactory.CreateFont(Path.Combine(AppContext.BaseDirectory, BoldFontFile));
        var commonFont = PdfFontFactory.CreateFont(Path.Combine(AppContext.BaseDirectory, CommonFontFile));

        #region Header section

        document.Add(new Paragraph(rm.GetString("HeaderText"))
            .SetTextAlignment(TextAlignment.CENTER)
            .SetFont(boldFont)
            .SetFontSize(HeaderFontSize));

        var headerTable = new Table(2)
            .UseAllAvailableWidth()
            .SetFont(commonFont)
            .SetFontSize(CommonFontSize)
            .SetMarginTop(5);

        if (company is not null && branch is not null)
        {
            var leftCellHeader = new Cell().SetBorder(Border.NO_BORDER);

            var list = new List().SetListSymbol("").SetFont(commonFont).SetFontSize(SmallFontSize);

            list.Add(new ListItem($"{company.TradeNameOfTheOwner}"));
            list.Add(new ListItem(AddressToString(company.Address)));
            list.Add(new ListItem($"{rmPrinter.GetString("IdentificationNumberCompanyLabel")}  {company.Ico},  {rmPrinter.GetString("TinNumberCompanyLabel")}  {company.Tin}"));
            list.Add(new ListItem($"{branch.Name}"));
            list.Add(new ListItem(AddressToString(branch.Address)));
            list.Add(new ListItem($"{rmPrinter.GetString("PhoneAbbreviationLabel")} {branch.PhoneNumber}"));

            leftCellHeader.Add(list);
            headerTable.AddCell(leftCellHeader);
        }

        var rightCellHeader = new Cell().SetBorder(Border.NO_BORDER);
        rightCellHeader.Add(new Paragraph($"{from.ToShortDateString()} - {until.ToShortDateString()}").SetFont(commonFont).SetFontSize(SmallFontSize).SetTextAlignment(TextAlignment.RIGHT));
        headerTable.AddCell(rightCellHeader);

        document.Add(headerTable);

        #endregion

        var contentTable = new Table(3)
            .UseAllAvailableWidth()
            .SetFont(commonFont)
            .SetFontSize(CommonFontSize)
            .SetMarginTop(20);

        contentTable.AddHeaderCell(rm.GetString("ListHeaderItemCurrencyCode"));
        contentTable.AddHeaderCell(rm.GetString("ListHeaderItemState"));
        contentTable.AddHeaderCell(rm.GetString("ListHeaderItemProfit"));

        foreach (var currency in currencies)
        {
            // Currency code cell
            contentTable.AddCell(currency.Code);

            // State cell
            var currencyCodeConverter = new CurrencyCodeToStateConverter();
            var state = (string)currencyCodeConverter.Convert(currency.Code, typeof(string), null,
                CultureInfo.CurrentCulture);
            contentTable.AddCell(state);

            // Profit cell
            var profitCell = new Cell();
            profitCell.Add(
                new Paragraph(currency.Profit.ToString(DecimalFormatTwoDecimals)).SetTextAlignment(
                    TextAlignment.RIGHT).SetFont(boldFont));
            contentTable.AddCell(profitCell);
        }

        #region Footer section

        // Total profit label
        var totalProfitLabelCell = new Cell(1, 2);
        totalProfitLabelCell.Add(new Paragraph(rm.GetString("ProfitTotalSumLabel")));
        contentTable.AddFooterCell(totalProfitLabelCell);

        // Total profit
        var totalProfitCell = new Cell();
        totalProfitCell.Add(new Paragraph(currencies.Sum(c => c.Profit).ToString(DecimalFormatTwoDecimals))
            .SetTextAlignment(TextAlignment.RIGHT));
        contentTable.AddFooterCell(totalProfitCell);

        #endregion

        #region Alignments

        foreach (var element in contentTable.GetHeader().GetChildren())
        {
            var cell = (Cell)element;
            cell.SetBorder(null);
            cell.SetBorderBottom(new SolidBorder(1));
        }

        for (var i = 2; i < contentTable.GetHeader().GetChildren().Count; i++)
        {
            var cell = (Cell)contentTable.GetHeader().GetChildren().ElementAt(i);
            cell.SetTextAlignment(TextAlignment.RIGHT);
        }

        foreach (var element in contentTable.GetChildren())
        {
            var cell = (Cell)element;
            cell.SetBorder(null);
        }

        foreach (var element in contentTable.GetFooter().GetChildren())
        {
            var cell = (Cell)element;
            cell.SetBorder(null);
            cell.SetBorderTop(new SolidBorder(1));
        }

        #endregion

        document.Add(contentTable);

        return document;
    }
}