using System.Globalization;
using System.Resources;
using ExchangeApp.App.Converters;
using ExchangeApp.App.Resources.Texts;
using ExchangeApp.BL.Models.Company;
using ExchangeApp.BL.Models.Donation;
using ExchangeApp.Common.Enums;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using Border = iText.Layout.Borders.Border;
using Cell = iText.Layout.Element.Cell;
using Path = System.IO.Path;
using TextAlignment = iText.Layout.Properties.TextAlignment;

namespace ExchangeApp.App.Services;

public partial class PrinterService
{
    public async Task SavePdf(DonationDetailModel model)
    {
        var fileName = await GetDonationFileNameWithPath(model);

        if (fileName is null)
        {
            throw new ArgumentNullException();
        }

        // Create a new PDF document with default properties
        var pdf = new PdfDocument(new PdfWriter(fileName, new WriterProperties().SetPdfVersion(PdfVersion.PDF_2_0)));
        var document = new Document(pdf, OperationDocumentPageSize);

        // Gets company and branch information
        var company = await _settingsFacade.GetCompanyDataAsync();
        var branch = await _settingsFacade.GetBranchDataAsync();

        // Gets pdf document
        document = GetDonationDocument(document, model, company, branch);

        document.Close();
        pdf.Close();
    }

    private static Document GetDonationDocument(
        Document document, 
        DonationDetailModel model, 
        CompanyDetailModel? company, 
        BranchDetailModel? branch)
    {
        var rm = new ResourceManager(typeof(PrinterResources));

        // Setting fonts
        var boldFont = PdfFontFactory.CreateFont(Path.Combine(AppContext.BaseDirectory, BoldFontFile));
        var commonFont = PdfFontFactory.CreateFont(Path.Combine(AppContext.BaseDirectory, CommonFontFile));

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

        return document;
    }
}