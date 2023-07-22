using System.Diagnostics;
using System.Globalization;
using System.Resources;
using ExchangeApp.App.Converters;
using ExchangeApp.App.Resources.Texts;
using ExchangeApp.BL.Models.Company;
using ExchangeApp.BL.Models.Customer;
using ExchangeApp.BL.Models.Transaction;
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
    public async Task SavePdf(TransactionDetailModel model, string? fileName = null)
    {
        fileName ??= await GetTransactionFileNameWithPath(model);

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

        // Gets customer
        CustomerDetailModel? customer = null;
        if (model.CustomerId is not null)
        {
            customer = await _customerFacade.GetByIdAsync(model.CustomerId ?? Guid.Empty);
        }

        // Gets pdf document
        document = GetTransactionDocument(document, model, customer, company, branch);

        document.Close();
        pdf.Close();
    }

    public async Task Print(TransactionDetailModel model)
    {
        var fileName = await GetTransactionFileNameWithPath(model) 
                       ?? Path.Combine(GetTemporaryFolder(), GetTransactionFileName(model));

        if (!File.Exists(fileName))
        {
            await SavePdf(model, fileName);
        }

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

    private async Task<string?> GetTransactionFileNameWithPath(TransactionDetailModel model)
    {
        var saveFolderPath = await _settingsFacade.GetSaveFolderPathAsync();

        if (saveFolderPath is null)
        {
            return null;
        }

        var year = model.Created.Year.ToString();
        var month = model.Created.Month.ToString();

        var directory = Path.Combine(saveFolderPath, year, month, "Transakcie");

        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        var fileName = GetTransactionFileName(model);

        fileName = Path.Combine(directory, fileName);

        return fileName;
    }

    private static string GetTransactionFileName(TransactionDetailModel model)
    {
        var transactionTypeString = model.TransactionType == TransactionType.Buy
            ? "nákup"
            : "predaj";
        var fileName = model.IsCanceled
            ? $"{transactionTypeString}_{model.Id}_storno.pdf"
            : $"{transactionTypeString}_{model.Id}.pdf";
        return fileName;
    }

    private static Document GetTransactionDocument(
        Document document, 
        TransactionDetailModel model, 
        CustomerDetailModel? customer,
        CompanyDetailModel? company, 
        BranchDetailModel? branch)
    {
        var rm = new ResourceManager(typeof(PrinterResources));

        PdfFont boldFont;
        PdfFont commonFont;
        // Setting fonts
        boldFont = PdfFontFactory.CreateFont(Path.Combine(AppContext.BaseDirectory, BoldFontFile));
        commonFont = PdfFontFactory.CreateFont(Path.Combine(AppContext.BaseDirectory, CommonFontFile));

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
        rightCellHeader.Add(new Paragraph($"{model.Created}").SetFont(commonFont).SetFontSize(SmallFontSize).SetTextAlignment(TextAlignment.RIGHT));
        rightCellHeader.Add(new Paragraph($"{rm.GetString("IdentificationNumberLabelBeforeIdNumber")}").SetFont(commonFont).SetFontSize(SmallFontSize).SetTextAlignment(TextAlignment.RIGHT));
        rightCellHeader.Add(new Paragraph($"{model.Id}").SetFont(boldFont).SetFontSize(HeaderFontSize).SetTextAlignment(TextAlignment.RIGHT));
        headerTable.AddCell(rightCellHeader);

        document.Add(headerTable);

        #endregion

        // ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        #region Customer data section

        if (customer is not null)
        {
            var customerRm = new ResourceManager(typeof(CustomerResources));

            var customerTable = new Table(2)
                .UseAllAvailableWidth()
                .SetFont(commonFont)
                .SetFontSize(SmallFontSize)
                .SetMarginTop(10);

            customerTable.AddCell($"{customerRm.GetString("FirstNameLabel")} {customer.FirstName}");
            customerTable.AddCell($"{customerRm.GetString("LastNameLabel")} {customer.FirstName}");

            if (!string.IsNullOrEmpty(customer.IdentificationNumber))
            {
                customerTable.AddCell($"{customerRm.GetString("IdentificationNumberLabel")} {customer.IdentificationNumber}");
            }

            if (customer.BirthDate is not null)
            {
                customerTable.AddCell($"{customerRm.GetString("BirthDateLabel")} {customer.BirthDate}");
            }

            customerTable.AddCell($"{customerRm.GetString("AddressLabel")} {customer.Address}");

            var evidenceTypeConverter = new EvidenceTypeToStringConverter();
            var evidenceType =
                (string)evidenceTypeConverter.Convert(customer.EvidenceType, typeof(EvidenceType), null, CultureInfo.CurrentCulture);
            customerTable.AddCell($"{customerRm.GetString("EvidenceTypeLabel")} {evidenceType}");
            customerTable.AddCell($"{customerRm.GetString("EvidenceNumberLabel")} {customer.EvidenceNumber}");

            switch (customer)
            {
                case IndividualCustomerDetailModel individualCustomer:
                    customerTable.AddCell($"{customerRm.GetString("NationalityLabel")} {individualCustomer.Nationality}");
                    break;
                case BusinessCustomerDetailModel businessCustomer:
                    customerTable.AddCell($"{customerRm.GetString("BusinessCompanyNameLabel")} {businessCustomer.TradeNameOfTheOwner}");
                    customerTable.AddCell($"{customerRm.GetString("BusinessAddressCompanyLabel")} {businessCustomer.TradeAddress}");
                    customerTable.AddCell($"{customerRm.GetString("BusinessIdentificationNumberLabel")} {businessCustomer.ICO}");
                    customerTable.AddCell($"{customerRm.GetString("NationalityLabel")} {businessCustomer.Nationality}");
                    break;
            }

            foreach (var element in customerTable.GetChildren())
            {
                var cell = (Cell)element;
                cell.SetBorder(null);
            }

            document.Add(customerTable);
        }

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

        var rmApp = new ResourceManager(typeof(AppResources));
        var leftCell = new Cell().SetBorder(Border.NO_BORDER);
        leftCell.Add(new Paragraph($"{rmApp.GetString("AppName")}").SetFont(commonFont)
            .SetFontSize(SmallFontSize));
        leftCell.Add(new Paragraph($"{rmApp.GetString("AppWebpage")}").SetFont(commonFont)
            .SetFontSize(SmallFontSize));

        var rightCell = new Cell().SetBorder(Border.NO_BORDER);
        rightCell.Add(new Paragraph($"{rm.GetString("SignatureLabel")} .................")
            .SetTextAlignment(TextAlignment.RIGHT));
        rightCell.Add(new Paragraph($"{rm.GetString("FooterNiceSentence")}").SetFont(commonFont)
            .SetFontSize(SmallFontSize));

        footerTable.AddCell(leftCell);
        footerTable.AddCell(rightCell);
        document.Add(footerTable);

        #endregion

        return document;
    }
}