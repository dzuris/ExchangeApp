using System.Globalization;
using System.Resources;
using ExchangeApp.App.Converters;
using ExchangeApp.App.Resources.Texts;
using ExchangeApp.App.Services.Interfaces;
using ExchangeApp.BL.Facades.Interfaces;
using ExchangeApp.BL.Models.Company;
using ExchangeApp.BL.Models.Donation;
using ExchangeApp.BL.Models.TotalBalance;
using ExchangeApp.BL.Models.Transaction;
using ExchangeApp.Common.Enums;
using iText.Kernel.Events;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using Border = iText.Layout.Borders.Border;
using Cell = iText.Layout.Element.Cell;
using Path = System.IO.Path;
using TextAlignment = iText.Layout.Properties.TextAlignment;

namespace ExchangeApp.App.Services;

public class PrinterService : IPrinterService
{
    private readonly ISettingsFacade _settingsFacade;

    private const string DomesticCurrencyCode = "EUR";
    private static readonly PageSize OperationDocumentPageSize = PageSize.A5;
    private const string BoldFontFile = "Sono-SemiBold.ttf";
    private const string CommonFontFile = "Sono-Regular.ttf";
    private const int HeaderFontSize = 12;
    private const int CommonFontSize = 8;
    private const int SmallFontSize = 6;

    public PrinterService(ISettingsFacade settingsFacade)
    {
        _settingsFacade = settingsFacade;
    }

    public async Task SavePdf(TransactionDetailModel model)
    {
        var directory = await GetTransactionDirectory(model.Time);

        if (directory is null)
        {
            throw new ArgumentNullException();
        }

        var fileName = Path.Combine(directory, model.IsCanceled ? $"{model.Id}_storno.pdf" : $"{model.Id}.pdf");

        // Create a new PDF document with default properties
        var pdf = new PdfDocument(new PdfWriter(fileName, new WriterProperties().SetPdfVersion(PdfVersion.PDF_2_0)));
        var document = new Document(pdf, OperationDocumentPageSize);

        // Setting fonts
        var boldFont = PdfFontFactory.CreateFont(Path.Combine(AppContext.BaseDirectory, BoldFontFile));
        var commonFont = PdfFontFactory.CreateFont(Path.Combine(AppContext.BaseDirectory, CommonFontFile));

        // Gets company and branch information
        var company = await _settingsFacade.GetCompanyDataAsync();
        var branch = await _settingsFacade.GetBranchDataAsync();

        var rm = new ResourceManager(typeof(PrinterResources));

        // ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        #region Header section

        var headerText = model.TransactionType == TransactionType.Buy
            ? rm.GetString("BuyTransactionHeader")
            : rm.GetString("SellTransactionHeader");

        if (model.IsCanceled)
        {
            headerText = $"{rm.GetString("CanceledHeaderTitle")} - {headerText}";
        }

        var headerTitle = new Paragraph(headerText)
            .SetTextAlignment(TextAlignment.CENTER)
            .SetFont(boldFont)
            .SetFontSize(HeaderFontSize);
        document.Add(headerTitle);

        var headerTable = new Table(new float[] { 1, 1 })
            .UseAllAvailableWidth()
            .SetBorder(Border.NO_BORDER)
            .SetFont(commonFont)
            .SetFontSize(CommonFontSize)
            .SetMarginTop(5);

        if (company is not null && branch is not null)
        {
            var leftCellHeader = new Cell().SetBorder(Border.NO_BORDER);

            var list = new List().SetListSymbol("").SetFont(commonFont).SetFontSize(SmallFontSize);

            list.Add(new ListItem($"{company.TradeNameOfTheOwner}"));
            list.Add(new ListItem(AddressToString(company.Address)));
            list.Add(new ListItem($"{rm.GetString("IdentificationNumberCompanyLabel")}  {company.Ico},  {rm.GetString("TinNumberCompanyLabel")}  {company.Tin}"));
            list.Add(new ListItem($"{branch.Name}"));
            list.Add(new ListItem(AddressToString(branch.Address)));
            list.Add(new ListItem($"{rm.GetString("PhoneAbbreviationLabel")} {branch.PhoneNumber}"));

            leftCellHeader.Add(list);
            headerTable.AddCell(leftCellHeader);
        }

        var rightCellHeader = new Cell().SetBorder(Border.NO_BORDER);
        rightCellHeader.Add(new Paragraph($"{model.Time}").SetFont(commonFont).SetFontSize(SmallFontSize).SetTextAlignment(TextAlignment.RIGHT));
        rightCellHeader.Add(new Paragraph($"{rm.GetString("IdentificationNumberLabelBeforeIdNumber")}").SetFont(commonFont).SetFontSize(SmallFontSize).SetTextAlignment(TextAlignment.RIGHT));
        rightCellHeader.Add(new Paragraph($"{model.Id}").SetFont(boldFont).SetFontSize(HeaderFontSize).SetTextAlignment(TextAlignment.RIGHT));
        headerTable.AddCell(rightCellHeader);

        document.Add(headerTable);

        #endregion

        // ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        #region Content section
        var contentTable = new Table(4).UseAllAvailableWidth()
            .SetFont(commonFont)
            .SetFontSize(CommonFontSize)
            .SetMarginTop(20);

        contentTable.AddHeaderCell(rm.GetString("CurrencyCodeTableHeader"));
        contentTable.AddHeaderCell(rm.GetString("CourseRateTableHeader"));
        contentTable.AddHeaderCell(rm.GetString("QuantityTableHeader"));
        contentTable.AddHeaderCell(string.Format(rm.GetString("AmountTableHeader") ?? string.Empty, DomesticCurrencyCode));

        contentTable.AddCell(model.CurrencyCode);
        contentTable.AddCell($"{model.CourseRate}");
        contentTable.AddCell($"{model.Quantity}");
        contentTable.AddCell($"{model.AmountDomesticCurrency}");

        document.Add(contentTable);

        var roundingConverter = new DecimalToDecimalWithSignStringConverter();
        var roundedValue =
            (string)roundingConverter.Convert(model.Rounding, typeof(decimal), null, CultureInfo.CurrentCulture);
        var roundingParagraph = new Paragraph($"{rm.GetString("RoundingLabel")} {roundedValue}")
            .SetTextAlignment(TextAlignment.RIGHT)
            .SetFont(commonFont)
            .SetFontSize(CommonFontSize)
            .SetMarginRight(5);
        document.Add(roundingParagraph);

        var totalAmountText = model.TransactionType == TransactionType.Buy
            ? rm.GetString("ReceivedAmountLabel")
            : rm.GetString("PaidAmountLabel");
        var totalAmountParagraph =
            new Paragraph($"{totalAmountText} {model.TotalAmountDomesticCurrency} {DomesticCurrencyCode}")
                .SetTextAlignment(TextAlignment.RIGHT)
                .SetFont(boldFont)
                .SetFontSize(HeaderFontSize)
                .SetMarginRight(5);
        document.Add(totalAmountParagraph);

        #endregion

        // ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        #region Footer section

        var footerTable = new Table(new float[] { 1, 1 })
            .UseAllAvailableWidth()
            .SetBorder(Border.NO_BORDER)
            .SetFont(commonFont)
            .SetFontSize(CommonFontSize)
            .SetMarginTop(30);

        var leftCell = new Cell().SetBorder(Border.NO_BORDER);
        leftCell.Add(new Paragraph($"{rm.GetString("FooterNiceSentence")}").SetFont(commonFont)
            .SetFontSize(SmallFontSize));

        var rightCell = new Cell().SetBorder(Border.NO_BORDER);
        rightCell.Add(new Paragraph($"{rm.GetString("SignatureLabel")} .................")
            .SetTextAlignment(TextAlignment.RIGHT));

        footerTable.AddCell(leftCell);
        footerTable.AddCell(rightCell);
        document.Add(footerTable);

        #endregion

        document.Close();
        pdf.Close();
    }

    public async Task SavePdf(DonationDetailModel model)
    {
        var directory = await GetDonationDirectory(model.Time);

        if (directory is null)
        {
            throw new ArgumentNullException();
        }

        var fileName = Path.Combine(directory, model.IsCanceled ? $"{model.Id}_storno.pdf" : $"{model.Id}.pdf");

        // Create a new PDF document with default properties
        var pdf = new PdfDocument(new PdfWriter(fileName, new WriterProperties().SetPdfVersion(PdfVersion.PDF_2_0)));
        var document = new Document(pdf, OperationDocumentPageSize);

        // Setting fonts
        var boldFont = PdfFontFactory.CreateFont(Path.Combine(AppContext.BaseDirectory, BoldFontFile));
        var commonFont = PdfFontFactory.CreateFont(Path.Combine(AppContext.BaseDirectory, CommonFontFile));

        // Gets company and branch information
        var company = await _settingsFacade.GetCompanyDataAsync();
        var branch = await _settingsFacade.GetBranchDataAsync();

        var rm = new ResourceManager(typeof(PrinterResources));

        // ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        #region Header section
        var donationTypeConverter = new DonationTypeToStringConverter();
        var headerText = (string)donationTypeConverter.Convert(model.Type, typeof(DonationType), null, CultureInfo.CurrentCulture);

        if (model.IsCanceled)
        {
            headerText = $"{rm.GetString("CanceledHeaderTitle")} - {headerText}";
        }

        var headerTitle = new Paragraph(headerText)
            .SetTextAlignment(TextAlignment.CENTER)
            .SetFont(boldFont)
            .SetFontSize(HeaderFontSize);
        document.Add(headerTitle);

        var headerTable = new Table(new float[] { 1, 1 })
            .UseAllAvailableWidth()
            .SetBorder(Border.NO_BORDER)
            .SetFont(commonFont)
            .SetFontSize(CommonFontSize)
            .SetMarginTop(5);

        if (company is not null && branch is not null)
        {
            var leftCellHeader = new Cell().SetBorder(Border.NO_BORDER);

            var list = new List().SetListSymbol("").SetFont(commonFont).SetFontSize(SmallFontSize);

            list.Add(new ListItem($"{company.TradeNameOfTheOwner}"));
            list.Add(new ListItem(AddressToString(company.Address)));
            list.Add(new ListItem($"{rm.GetString("IdentificationNumberCompanyLabel")}  {company.Ico},  {rm.GetString("TinNumberCompanyLabel")}  {company.Tin}"));
            list.Add(new ListItem($"{branch.Name}"));
            list.Add(new ListItem(AddressToString(branch.Address)));
            list.Add(new ListItem($"{rm.GetString("PhoneAbbreviationLabel")} {branch.PhoneNumber}"));

            leftCellHeader.Add(list);
            headerTable.AddCell(leftCellHeader);
        }

        var rightCellHeader = new Cell().SetBorder(Border.NO_BORDER);
        rightCellHeader.Add(new Paragraph($"{model.Time}").SetFont(commonFont).SetFontSize(SmallFontSize).SetTextAlignment(TextAlignment.RIGHT));
        rightCellHeader.Add(new Paragraph($"{rm.GetString("IdentificationNumberLabelBeforeIdNumber")}").SetFont(commonFont).SetFontSize(SmallFontSize).SetTextAlignment(TextAlignment.RIGHT));
        rightCellHeader.Add(new Paragraph($"{model.Id}").SetFont(boldFont).SetFontSize(HeaderFontSize).SetTextAlignment(TextAlignment.RIGHT));
        headerTable.AddCell(rightCellHeader);

        document.Add(headerTable);

        #endregion

        // ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        #region Content section
        var contentTable = new Table(4).UseAllAvailableWidth()
            .SetFont(commonFont)
            .SetFontSize(CommonFontSize)
            .SetMarginTop(20);

        contentTable.AddHeaderCell(rm.GetString("CurrencyCodeTableHeader"));
        contentTable.AddHeaderCell(rm.GetString("CourseRateTableHeader"));
        contentTable.AddHeaderCell(rm.GetString("AverageCourseRateTableHeader"));
        contentTable.AddHeaderCell(rm.GetString("QuantityTableHeader"));

        contentTable.AddCell(model.CurrencyCode);
        contentTable.AddCell($"{model.CourseRate}");
        contentTable.AddCell($"{model.AverageCourseRate}");
        contentTable.AddCell($"{model.Quantity}");

        document.Add(contentTable);

        if (!string.IsNullOrWhiteSpace(model.Note))
        {
            document.Add(new Paragraph($"{rm.GetString("NoteLabel")} {model.Note}").SetFont(commonFont).SetFontSize(CommonFontSize));
        }

        #endregion

        // ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        #region Footer section

        document.Add(
            new Paragraph($"{rm.GetString("SignatureLabel")} .................")
            .SetFont(commonFont)
            .SetFontSize(CommonFontSize)
            .SetTextAlignment(TextAlignment.RIGHT)
            .SetMarginTop(30));

        #endregion

        document.Close();
        pdf.Close();
    }

    public async Task SavePdf(TotalBalanceModel model)
    {
        var directory = await GetTotalBalanceDirectory(model.Created, model.Type);

        if (directory is null)
        {
            throw new ArgumentNullException();
        }

        var rmTotalBalance = new ResourceManager(typeof(PrinterTotalBalanceResources));

        // File name setting
        var fileName = model.Type switch
        {
            TotalBalanceType.Daily => Path.Combine(directory, $"{model.Id}_{rmTotalBalance.GetString("FolderNameDaily")}.pdf"),
            TotalBalanceType.Monthly => Path.Combine(directory, $"{model.Id}_{rmTotalBalance.GetString("FolderNameMonthly")}.pdf"),
            _ => throw new ArgumentOutOfRangeException()
        };

        // Create a new PDF document with default properties
        var pdf = new PdfDocument(new PdfWriter(fileName, new WriterProperties().SetPdfVersion(PdfVersion.PDF_2_0)));
        var document = new Document(pdf, PageSize.A4);

        // Setting fonts
        var boldFont = PdfFontFactory.CreateFont(Path.Combine(AppContext.BaseDirectory, BoldFontFile));
        var commonFont = PdfFontFactory.CreateFont(Path.Combine(AppContext.BaseDirectory, CommonFontFile));

        // Gets company info
        var company = await _settingsFacade.GetCompanyDataAsync();

        if (company is null)
        {
            return;
        }

        // Adding header
        var headerHandler = new TotalBalanceHeaderEventHandler(commonFont, company.TradeNameOfTheOwner, company.Ico, model.Created, model.Id);
        pdf.AddEventHandler(PdfDocumentEvent.END_PAGE, headerHandler);

        var rm = new ResourceManager(typeof(PrinterResources));

        document.Close();
        pdf.Close();
    }

    public Task Print()
    {
        throw new NotImplementedException();
    }

    private async Task<string?> GetTransactionDirectory(DateTime created)
    {
        var saveFolderPath = await _settingsFacade.GetSaveFolderPathAsync();

        if (saveFolderPath is null) return null;

        var year = created.Year.ToString();
        var month = created.Month.ToString();

        var directory = Path.Combine(saveFolderPath, year, month, "Transakcie");

        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        return directory;
    }

    private async Task<string?> GetDonationDirectory(DateTime created)
    {
        var saveFolderPath = await _settingsFacade.GetSaveFolderPathAsync();

        if (saveFolderPath is null) return null;

        var year = created.Year.ToString();
        var month = created.Month.ToString();

        var directory = Path.Combine(saveFolderPath, year, month, "Dotácie");

        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        return directory;
    }

    private async Task<string?> GetTotalBalanceDirectory(DateTime created, TotalBalanceType type)
    {
        var saveFolderPath = await _settingsFacade.GetSaveFolderPathAsync();

        if (saveFolderPath is null) return null;

        var year = created.Year.ToString();
        var month = created.Month.ToString();

        var directory = type == TotalBalanceType.Monthly 
            ? Path.Combine(saveFolderPath, year, month, "Uzávierky") 
            : Path.Combine(saveFolderPath, year, month, "Uzávierky", "Denné");

        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        return directory;
    }

    private static string AddressToString(AddressModel address) 
        => $"{address.Street} {address.StreetNumber},  {address.PostalCode} {address.City}";
}