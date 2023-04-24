using System.Diagnostics;
using System.Globalization;
using System.Resources;
using ExchangeApp.App.Converters;
using ExchangeApp.App.Resources.Texts;
using ExchangeApp.App.Services.HeaderAndFooters;
using ExchangeApp.BL.Models;
using ExchangeApp.BL.Models.Company;
using ExchangeApp.BL.Models.Currency;
using ExchangeApp.BL.Models.Donation;
using ExchangeApp.BL.Models.Operations;
using ExchangeApp.BL.Models.TotalBalance;
using ExchangeApp.BL.Models.Transaction;
using ExchangeApp.Common.Enums;
using iText.IO.Font;
using iText.Kernel.Events;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using Cell = iText.Layout.Element.Cell;
using Path = System.IO.Path;
using TextAlignment = iText.Layout.Properties.TextAlignment;

namespace ExchangeApp.App.Services;

public partial class PrinterService
{
    public async Task SavePdf(TotalBalanceModel model, string? fileName = null)
    {
        fileName ??= await GetTotalBalanceFileNameWithPath(model);

        if (fileName is null)
        {
            throw new ArgumentNullException();
        }

        // Create a new PDF document with default properties
        var pdf = new PdfDocument(new PdfWriter(fileName, new WriterProperties().SetPdfVersion(PdfVersion.PDF_2_0)));
        var document = new Document(pdf, PageSize.A4);

        // Setting fonts
        var boldFont = PdfFontFactory.CreateFont(Path.Combine(AppContext.BaseDirectory, BoldFontFile));
        var commonFont = PdfFontFactory.CreateFont(Path.Combine(AppContext.BaseDirectory, CommonFontFile));

        // Gets company info
        var company = await _settingsFacade.GetCompanyDataAsync() ?? CompanyDetailModel.Empty;

        // Adding header and footer
        var headerHandler = new TotalBalanceHeaderEventHandler(model.Type, commonFont, SmallFontSize, company.TradeNameOfTheOwner, company.Ico, model.Created, model.Id);
        var footerHandler = new TotalBalanceFooterEventHandler(commonFont, SmallFontSize);
        pdf.AddEventHandler(PdfDocumentEvent.START_PAGE, headerHandler);
        pdf.AddEventHandler(PdfDocumentEvent.END_PAGE, footerHandler);
        document.SetMargins(50, 25, 20, 25);

        #region Monthly report - overview

        if (model.Type == TotalBalanceType.Monthly)
        {
            document = await AddMonthlyReportOverview(document, model, boldFont, commonFont);
            document.Add(new AreaBreak());
        }

        #endregion

        #region Cash register section

        if (model.Type == TotalBalanceType.Daily)
        {
            var currencies = await _currencyFacade.GetCurrenciesHistory(model.Created);
            document = AddCashRegisterData(document, currencies, boldFont, commonFont);
            document.Add(new AreaBreak());
        }

        #endregion

        #region Donation operations section

        if (model.Type == TotalBalanceType.Daily)
        {
            document = await AddDonationOperationsData(document, model, boldFont, commonFont);
            document.Add(new AreaBreak());
        }

        #endregion

        var transactions = (await _transactionFacade.GetTransactions(model.LastTotalBalance, model.Created)).ToList();

        #region Buy - abbreviated

        document = AddBuyAbbreviatedData(document, transactions, boldFont, commonFont);

        #endregion

        document.Add(new AreaBreak());

        #region Sell - abbreviated

        document = AddSellAbbreviatedData(document, transactions, boldFont, commonFont);

        #endregion

        document.Add(new AreaBreak());

        #region Purchase receipt

        document = AddPurchaseReceiptData(document, transactions, boldFont, commonFont);

        #endregion

        document.Add(new AreaBreak());

        #region Sales receipt

        document = AddSalesReceiptData(document, transactions, boldFont, commonFont);

        #endregion

        document.Add(new AreaBreak());

        #region Profit abbreviated list

        var profitList = await _operationFacade.GetProfitListAsync(model.LastTotalBalance, model.Created);
        document = AddProfitAbbreviatedListData(document, profitList, boldFont, commonFont);

        #endregion

        document.Add(new AreaBreak());

        #region Profit complete list

        var operationsProfitList =
            await _operationFacade.GetOperationsProfitAsync(model.LastTotalBalance, model.Created);
        document = AddProfitCompleteListData(document,
            operationsProfitList,
            boldFont,
            commonFont);

        #endregion

        document.Add(new AreaBreak());

        #region All operations

        var operations = (await _operationFacade.GetOperationsAsync(model.LastTotalBalance, model.Created))
            .OrderBy(e => e.CurrencyCode == DomesticCurrencyCode)
            .ThenBy(e => e.CurrencyCode)
            .ThenBy(e => e.Created)
            .ToList();
        var totalBalances = (await _totalBalanceFacade.GetAllAsync(model.LastTotalBalance, model.Created))
            .OrderBy(e => e.Created)
            .ToList();

        document = await AddAllOperationsData(document, operations, totalBalances, boldFont, commonFont);

        #endregion

        document.Close();
        pdf.Close();
    }

    public async Task Print(TotalBalanceModel model)
    {
        var fileName = await GetTotalBalanceFileNameWithPath(model)
                       ?? Path.Combine(GetTemporaryFolder(), GetTotalBalanceFileName(model));

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

    private async Task<string?> GetTotalBalanceFileNameWithPath(TotalBalanceModel model)
    {
        var saveFolderPath = await _settingsFacade.GetSaveFolderPathAsync();

        if (saveFolderPath is null) return null;

        var year = model.Created.Year.ToString();
        var month = model.Created.Month.ToString();

        var directory = model.Type == TotalBalanceType.Monthly
            ? Path.Combine(saveFolderPath, year, month, "Uzávierky")
            : Path.Combine(saveFolderPath, year, month, "Uzávierky", "Denné");

        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        var fileNameWithPath = Path.Combine(directory, GetTotalBalanceFileName(model));

        return fileNameWithPath;
    }

    private static string GetTotalBalanceFileName(TotalBalanceModel model)
    {
        var fileName = model.Type switch
        {
            TotalBalanceType.Daily => $"{model.Id}_{model.Created.ToString("yyMMdd")}_denná.pdf",
            TotalBalanceType.Monthly => $"{model.Id}_mesačná.pdf",
            _ => throw new ArgumentOutOfRangeException()
        };

        return fileName;
    }

    private async Task<Document> AddMonthlyReportOverview(Document document, TotalBalanceModel model, PdfFont boldFont, PdfFont commonFont)
    {
        var rm = new ResourceManager(typeof(PrinterTotalBalanceResources));

        var header = new Paragraph(rm.GetString("SectionHeaderMonthlyTotalOperationsNumber"))
            .SetTextAlignment(TextAlignment.CENTER)
            .SetFont(boldFont)
            .SetFontSize(HeaderFontSize);
        document.Add(header);
        document.Add(new LineSeparator(new SolidLine()));

        var contentTable = new Table(8)
            .UseAllAvailableWidth()
            .SetFont(commonFont)
            .SetFontSize(CommonFontSize)
            .SetMarginTop(20);

        var pastCurrenciesData = await _currencyFacade.GetCurrenciesHistory(model.LastTotalBalance);
        var currentCurrenciesData = await _currencyFacade.GetCurrenciesHistory(model.Created);
        var operations = (await _operationFacade.GetOperationsAsync(model.LastTotalBalance, model.Created)).ToList();

        #region Header

        contentTable.AddHeaderCell(rm.GetString("ListHeaderItemCurrencyCode"));
        contentTable.AddHeaderCell(rm.GetString("ListHeaderItemInitialState"));
        contentTable.AddHeaderCell(rm.GetString("ListHeaderItemBuy"));
        contentTable.AddHeaderCell(rm.GetString("ListHeaderItemSell"));
        contentTable.AddHeaderCell(rm.GetString("ListHeaderItemDeposit"));
        contentTable.AddHeaderCell(rm.GetString("ListHeaderItemWithdraw"));
        contentTable.AddHeaderCell(rm.GetString("ListHeaderItemLevy"));
        contentTable.AddHeaderCell(rm.GetString("ListHeaderItemFinalState"));

        #endregion

        foreach (var item in currentCurrenciesData)
        {
            var buySum = 0m;
            var sellSum = 0m;
            var depositSum = 0m;
            var withdrawSum = 0m;
            var levySum = 0m;

            // Setting sums
            if (item.Code == DomesticCurrencyCode)
            {
                foreach (var operation in operations)
                {
                    switch (operation)
                    {
                        case TransactionListModel { TransactionType: TransactionType.Buy } transaction:
                            buySum -= transaction.TotalAmount;
                            break;
                        case TransactionListModel { TransactionType: TransactionType.Sell } transaction:
                            sellSum += transaction.TotalAmount;
                            break;
                    }

                    if (operation.CurrencyCode == item.Code)
                    {
                        switch (operation)
                        {
                            case DonationListModel { Type: DonationType.Deposit }:
                                depositSum += operation.Quantity;
                                break;
                            case DonationListModel { Type: DonationType.Withdraw }:
                                withdrawSum -= operation.Quantity;
                                break;
                            case DonationListModel { Type: DonationType.Levy }:
                                levySum -= operation.Quantity;
                                break;
                        }
                    }
                }
            }
            else
            {
                foreach (var operation in operations.Where(operation => operation.CurrencyCode == item.Code))
                {
                    switch (operation)
                    {
                        case TransactionListModel { TransactionType: TransactionType.Buy }:
                            buySum += operation.Quantity;
                            break;
                        case TransactionListModel { TransactionType: TransactionType.Sell }:
                            sellSum -= operation.Quantity;
                            break;
                        case DonationListModel { Type: DonationType.Deposit }:
                            depositSum += operation.Quantity;
                            break;
                        case DonationListModel { Type: DonationType.Withdraw }:
                            withdrawSum -= operation.Quantity;
                            break;
                        case DonationListModel { Type: DonationType.Levy }:
                            levySum -= operation.Quantity;
                            break;
                    }
                }
            }

            // currency code
            contentTable.AddCell(item.Code);

            // initial state
            var initialBalance = pastCurrenciesData.SingleOrDefault(e => e.Code == item.Code)?.Quantity ?? 0;
            var initialStateCell = new Cell().SetTextAlignment(TextAlignment.RIGHT);
            initialStateCell.Add(new Paragraph(initialBalance.ToString(DecimalFormatTwoDecimals)));
            contentTable.AddCell(initialStateCell);

            // buy sum
            var buyCell = new Cell().SetTextAlignment(TextAlignment.RIGHT);
            buyCell.Add(new Paragraph(buySum.ToString(DecimalFormatTwoDecimals)));
            contentTable.AddCell(buyCell);

            // sell sum
            var sellCell = new Cell().SetTextAlignment(TextAlignment.RIGHT);
            sellCell.Add(new Paragraph(sellSum.ToString(DecimalFormatTwoDecimals)));
            contentTable.AddCell(sellCell);

            // deposit sum
            var depositCell = new Cell().SetTextAlignment(TextAlignment.RIGHT);
            depositCell.Add(new Paragraph(depositSum.ToString(DecimalFormatTwoDecimals)));
            contentTable.AddCell(depositCell);

            // withdraw sum
            var withdrawCell = new Cell().SetTextAlignment(TextAlignment.RIGHT);
            withdrawCell.Add(new Paragraph(withdrawSum.ToString(DecimalFormatTwoDecimals)));
            contentTable.AddCell(withdrawCell);

            // levy sum
            var levyCell = new Cell().SetTextAlignment(TextAlignment.RIGHT);
            levyCell.Add(new Paragraph(levySum.ToString(DecimalFormatTwoDecimals)));
            contentTable.AddCell(levyCell);

            // final state
            var finalStateCell = new Cell().SetTextAlignment(TextAlignment.RIGHT);
            finalStateCell.Add(new Paragraph(item.Quantity.ToString(DecimalFormatTwoDecimals)));
            contentTable.AddCell(finalStateCell);
        }

        #region Setting borders and aligns

        foreach (var element in contentTable.GetHeader().GetChildren())
        {
            var cell = (Cell)element;
            cell.SetBorder(null);
            cell.SetBorderBottom(new SolidBorder(1));
        }

        for (var i = 1; i < contentTable.GetHeader().GetChildren().Count; i++)
        {
            var cell = (Cell)contentTable.GetHeader().GetChildren().ElementAt(i);
            cell.SetTextAlignment(TextAlignment.RIGHT);
        }

        foreach (var element in contentTable.GetChildren())
        {
            var cell = (Cell)element;
            cell.SetBorder(null);
        }

        #endregion

        document.Add(contentTable);

        return document;
    }

    private static Document AddCashRegisterData(Document document, List<CurrencyHistoryModel> currencies, PdfFont boldFont, PdfFont commonFont)
    {
        var rm = new ResourceManager(typeof(PrinterTotalBalanceResources));

        var headerSectionCashRegister =
            new Paragraph(rm.GetString("SectionHeaderCashRegisterStatusInTotalBalanceTime"))
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFont(boldFont)
                .SetFontSize(HeaderFontSize);
        document.Add(headerSectionCashRegister);
        document.Add(new LineSeparator(new SolidLine()));

        var contentTable = new Table(5)
            .UseAllAvailableWidth()
            .SetFont(commonFont)
            .SetFontSize(CommonFontSize)
            .SetMarginTop(20);

        #region Header

        contentTable.AddHeaderCell(rm.GetString("ListHeaderItemCurrencyCode"));
        contentTable.AddHeaderCell(rm.GetString("ListHeaderItemCourseRate"));
        contentTable.AddHeaderCell(rm.GetString("ListHeaderItemQuantity"));
        contentTable.AddHeaderCell(rm.GetString("ListHeaderItemExchangeRateValue"));
        contentTable.AddHeaderCell(rm.GetString("ListHeaderItemAmount"));

        #endregion

        foreach (var currency in currencies)
        {
            var currencyCodeCell = new Cell();
            currencyCodeCell.Add(new Paragraph($"{currency.Code}").SetTextAlignment(TextAlignment.LEFT));
            contentTable.AddCell(currencyCodeCell);

            var courseRateCell = new Cell();
            var roundedAverageCourseRate = Math.Round(currency.AverageCourseRate, 6);
            courseRateCell.Add(new Paragraph(roundedAverageCourseRate.ToString(AverageCourseFormat)).SetTextAlignment(TextAlignment.RIGHT));
            contentTable.AddCell(courseRateCell);

            var quantityCell = new Cell();
            quantityCell.Add(new Paragraph(currency.Quantity.ToString(DecimalFormatTwoDecimals)).SetTextAlignment(TextAlignment.RIGHT).SetFont(boldFont));
            contentTable.AddCell(quantityCell);

            var exchangeRateCell = new Cell();
            exchangeRateCell.Add(
                new Paragraph(currency.Code == DomesticCurrencyCode
                    ? "-"
                    : currency.ExchangeRateValue.ToString(DecimalFormatTwoDecimals)).SetTextAlignment(TextAlignment.RIGHT));
            contentTable.AddCell(exchangeRateCell);

            var amountCell = new Cell();
            amountCell.Add(
                new Paragraph(currency.ExchangeRateValue.ToString(DecimalFormatTwoDecimals)).SetTextAlignment(TextAlignment.RIGHT));
            contentTable.AddCell(amountCell);
        }

        #region Footer

        var amountTotalCell = new Cell(1, 3).SetTextAlignment(TextAlignment.LEFT);
        amountTotalCell.Add(new Paragraph($"{rm.GetString("AmountTotalSummary")}"));
        contentTable.AddFooterCell(amountTotalCell);

        var totalExchangeRate = currencies.Where(c => c.Code != DomesticCurrencyCode).Sum(c => c.ExchangeRateValue);
        var totalExchangeRateCell = new Cell();
        totalExchangeRateCell.Add(
            new Paragraph(totalExchangeRate.ToString(DecimalFormatTwoDecimals)).SetFontSize(HeaderFontSize));
        contentTable.AddFooterCell(totalExchangeRateCell);

        var totalAmount = totalExchangeRate + currencies.SingleOrDefault(c => c.Code == DomesticCurrencyCode)?.ExchangeRateValue ?? 0;
        var totalAmountCell = new Cell();
        totalAmountCell.Add(new Paragraph(totalAmount.ToString(DecimalFormatTwoDecimals)).SetFontSize(HeaderFontSize));
        contentTable.AddFooterCell(totalAmountCell);

        #endregion

        #region Setting borders and text aligns

        var isFirst = true;
        foreach (var element in contentTable.GetHeader().GetChildren())
        {
            var cell = (Cell)element;
            cell.SetBorder(null);
            cell.SetBorderBottom(new SolidBorder(1));

            if (isFirst)
            {
                cell.SetTextAlignment(TextAlignment.LEFT);
                isFirst = false;
            }
            else
            {
                cell.SetTextAlignment(TextAlignment.RIGHT);
            }
        }

        foreach (var element in contentTable.GetChildren())
        {
            var cell = (Cell)element;
            cell.SetBorder(null);
        }

        isFirst = true;
        foreach (var element in contentTable.GetFooter().GetChildren())
        {
            var cell = (Cell)element;
            cell.SetBorder(null);
            cell.SetBorderTop(new SolidBorder(1));

            if (isFirst)
            {
                cell.SetTextAlignment(TextAlignment.LEFT);
                isFirst = false;
            }
            else
            {
                cell.SetTextAlignment(TextAlignment.RIGHT);
            }
        }

        #endregion

        document.Add(contentTable);

        return document;
    }

    private async Task<Document> AddDonationOperationsData(Document document, TotalBalanceModel model,
        PdfFont boldFont, PdfFont commonFont)
    {
        var donations = await _donationFacade.GetDonations(model.LastTotalBalance, model.Created);

        var rm = new ResourceManager(typeof(PrinterTotalBalanceResources));

        var headerOtherOperationsSection = new Paragraph(rm.GetString("SectionHeaderDonationOperations"))
            .SetTextAlignment(TextAlignment.CENTER)
            .SetFont(boldFont)
            .SetFontSize(HeaderFontSize);
        document.Add(headerOtherOperationsSection);
        document.Add(new LineSeparator(new SolidLine()));

        var contentTable = new Table(7)
            .UseAllAvailableWidth()
            .SetFont(commonFont)
            .SetFontSize(CommonFontSize)
            .SetMarginTop(20);
        contentTable.AddHeaderCell(rm.GetString("ListHeaderItemOperationId"));
        contentTable.AddHeaderCell(rm.GetString("ListHeaderItemCurrencyCode"));
        contentTable.AddHeaderCell(rm.GetString("ListHeaderItemDateTime"));
        contentTable.AddHeaderCell(rm.GetString("ListHeaderItemOperationType"));
        contentTable.AddHeaderCell(rm.GetString("ListHeaderItemQuantity"));
        contentTable.AddHeaderCell(rm.GetString("ListHeaderItemCourseRate"));
        contentTable.AddHeaderCell(rm.GetString("ListHeaderItemExchangeRateValue"));

        foreach (var donation in donations)
        {
            // Id
            contentTable.AddCell($"{donation.Id}");

            // Currency code
            contentTable.AddCell($"{donation.CurrencyCode}");

            // Time
            var timeCell = new Cell();
            timeCell.Add(new Paragraph($"{donation.Created}"));
            contentTable.AddCell(timeCell);

            // Donation type
            var donationTypeConverter = new DonationTypeToStringConverter();
            var donationType = (string)donationTypeConverter.Convert(donation.Type, typeof(DonationType), null, CultureInfo.CurrentCulture);
            var typeCell = new Cell();
            var donationText = donation.IsCanceled
                ? $"{donationType} - {rm.GetString("CanceledTextAfter")}"
                : $"{donationType}";
            typeCell.Add(new Paragraph($"{donationText}").SetTextAlignment(TextAlignment.RIGHT));
            contentTable.AddCell(typeCell);

            // Quantity
            var quantityCell = new Cell();
            quantityCell.Add(new Paragraph(donation.Quantity.ToString(DecimalFormatTwoDecimals)).SetTextAlignment(TextAlignment.RIGHT).SetFont(boldFont));
            contentTable.AddCell(quantityCell);

            // Course rate
            var courseCell = new Cell();
            courseCell.Add(
                new Paragraph(donation.CourseRate.ToString(AverageCourseFormat)).SetTextAlignment(TextAlignment.RIGHT));
            contentTable.AddCell(courseCell);

            // Exchange rate value
            var exchangeRateCell = new Cell();
            exchangeRateCell.Add(
                new Paragraph(donation.ExchangeRateValue.ToString(DecimalFormatTwoDecimals)).SetTextAlignment(TextAlignment.RIGHT));
            contentTable.AddCell(exchangeRateCell);

            // Note
            if (string.IsNullOrEmpty(donation.Note)) continue;
            contentTable.AddCell(string.Empty);
            var noteCell = new Cell(1, 6);
            var noteParagraph = new Paragraph($"{donation.Note}");
            noteCell.Add(noteParagraph);
            contentTable.AddCell(noteCell);
        }

        #region Setting borders and text aligns

        for (var i = 3; i < contentTable.GetHeader().GetChildren().Count; i++)
        {
            var cell = (Cell)contentTable.GetHeader().GetChildren().ElementAt(i);
            cell.SetTextAlignment(TextAlignment.RIGHT);
        }

        foreach (var element in contentTable.GetHeader().GetChildren())
        {
            var cell = (Cell)element;
            cell.SetBorder(null);
            cell.SetBorderBottom(new SolidBorder(1));
        }

        foreach (var element in contentTable.GetChildren())
        {
            var cell = (Cell)element;
            cell.SetBorder(null);
        }

        #endregion

        document.Add(contentTable);

        return document;
    }

    private static Document AddBuyAbbreviatedData(Document document, IEnumerable<TransactionListModel> notFilteredList,
        PdfFont boldFont, PdfFont commonFont)
    {
        var filteredList = notFilteredList
            .Where(t => !t.IsCanceled && t.TransactionType == TransactionType.Buy)
            .ToList();

        var rm = new ResourceManager(typeof(PrinterTotalBalanceResources));

        var headerSectionBuyAbbreviated =
            new Paragraph(rm.GetString("SectionHeaderBuyTransactionsAbbreviated"))
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFont(boldFont)
                .SetFontSize(HeaderFontSize);
        document.Add(headerSectionBuyAbbreviated);
        document.Add(new LineSeparator(new SolidLine()));

        var contentTable = new Table(8)
            .UseAllAvailableWidth()
            .SetFont(commonFont)
            .SetFontSize(CommonFontSize)
            .SetMarginTop(20);

        #region Header

        contentTable.AddHeaderCell(rm.GetString("ListHeaderItemOperationId"));
        contentTable.AddHeaderCell(rm.GetString("ListHeaderItemDateTime"));
        contentTable.AddHeaderCell(rm.GetString("ListHeaderItemCurrencyCode"));
        contentTable.AddHeaderCell(rm.GetString("ListHeaderItemCustomerName"));
        contentTable.AddHeaderCell(rm.GetString("ListHeaderItemIdentificationCardNumber"));
        contentTable.AddHeaderCell(rm.GetString("ListHeaderItemExchangeRateValue"));
        contentTable.AddHeaderCell(rm.GetString("ListHeaderItemRounding"));
        contentTable.AddHeaderCell(rm.GetString("ListHeaderItemTotal"));

        #endregion

        foreach (var transaction in filteredList)
        {
            // Id
            contentTable.AddCell($"{transaction.Id}");

            // Time
            contentTable.AddCell($"{transaction.Created}");

            // Currency code
            contentTable.AddCell($"{transaction.CurrencyCode}");

            // Customer name
            contentTable.AddCell($"{transaction.Customer?.WholeName}");

            // Customer card number
            contentTable.AddCell($"{transaction.Customer?.EvidenceNumber}");

            // Exchange rate value
            var exchangeRateCell = new Cell();
            exchangeRateCell.Add(
                new Paragraph(transaction.ExchangeRateValue.ToString(DecimalFormatTwoDecimals)).SetTextAlignment(TextAlignment.RIGHT));
            contentTable.AddCell(exchangeRateCell);

            // Rounding
            var roundingCell = new Cell();
            roundingCell.Add(
                new Paragraph(transaction.Rounding.ToString(DecimalFormatTwoDecimals)).SetTextAlignment(TextAlignment
                    .RIGHT));
            contentTable.AddCell(roundingCell);

            // Total amount
            var totalCell = new Cell();
            totalCell.Add(
                new Paragraph(transaction.TotalAmount.ToString(DecimalFormatTwoDecimals)).SetTextAlignment(TextAlignment
                    .RIGHT));
            contentTable.AddCell(totalCell);
        }

        #region Footer

        // Total sum label
        var totalSumCell = new Cell(1, 5);
        totalSumCell.Add(new Paragraph(rm.GetString("AmountTotalSummary")));
        contentTable.AddFooterCell(totalSumCell);

        // Exchange rate sum
        var exchangeRateValueSum = filteredList.Sum(t => t.ExchangeRateValue);
        contentTable.AddFooterCell(exchangeRateValueSum.ToString(DecimalFormatTwoDecimals));

        // Rounding sum
        var roundingSum = filteredList.Sum(t => t.Rounding);
        contentTable.AddFooterCell(roundingSum.ToString(DecimalFormatTwoDecimals));

        // Amount total sum
        var amountTotalSum = filteredList.Sum(t => t.TotalAmount);
        contentTable.AddFooterCell(amountTotalSum.ToString(DecimalFormatTwoDecimals));

        #endregion

        #region Setting borders and text aligns

        // Header
        for (var i = 5; i < contentTable.GetHeader().GetChildren().Count; i++)
        {
            var cell = (Cell)contentTable.GetHeader().GetChildren().ElementAt(i);
            cell.SetTextAlignment(TextAlignment.RIGHT);
        }

        foreach (var element in contentTable.GetHeader().GetChildren())
        {
            var cell = (Cell)element;
            cell.SetBorder(null);
            cell.SetBorderBottom(new SolidBorder(1));
        }

        // Content
        foreach (var element in contentTable.GetChildren())
        {
            var cell = (Cell)element;
            cell.SetBorder(null);
        }

        // Footer
        for (var i = 1; i < contentTable.GetFooter().GetChildren().Count; i++)
        {
            var cell = (Cell)contentTable.GetFooter().GetChildren().ElementAt(i);
            cell.SetTextAlignment(TextAlignment.RIGHT);
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

    private static Document AddSellAbbreviatedData(Document document, IEnumerable<TransactionListModel> notFilteredList,
        PdfFont boldFont, PdfFont commonFont)
    {
        var filteredList = notFilteredList
            .Where(t => !t.IsCanceled && t.TransactionType == TransactionType.Sell)
            .ToList();

        var rm = new ResourceManager(typeof(PrinterTotalBalanceResources));

        var sectionHeader =
            new Paragraph(rm.GetString("SectionHeaderSellTransactionsAbbreviated"))
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFont(boldFont)
                .SetFontSize(HeaderFontSize);
        document.Add(sectionHeader);
        document.Add(new LineSeparator(new SolidLine()));

        var contentTable = new Table(8)
            .UseAllAvailableWidth()
            .SetFont(commonFont)
            .SetFontSize(CommonFontSize)
            .SetMarginTop(20);

        #region Header

        contentTable.AddHeaderCell(rm.GetString("ListHeaderItemOperationId"));
        contentTable.AddHeaderCell(rm.GetString("ListHeaderItemDateTime"));
        contentTable.AddHeaderCell(rm.GetString("ListHeaderItemCurrencyCode"));
        contentTable.AddHeaderCell(rm.GetString("ListHeaderItemCustomerName"));
        contentTable.AddHeaderCell(rm.GetString("ListHeaderItemIdentificationCardNumber"));
        contentTable.AddHeaderCell(rm.GetString("ListHeaderItemExchangeRateValue"));
        contentTable.AddHeaderCell(rm.GetString("ListHeaderItemRounding"));
        contentTable.AddHeaderCell(rm.GetString("ListHeaderItemTotal"));

        #endregion

        foreach (var transaction in filteredList)
        {
            // Id
            contentTable.AddCell($"{transaction.Id}");

            // Time
            contentTable.AddCell($"{transaction.Created}");

            // Currency code
            contentTable.AddCell($"{transaction.CurrencyCode}");

            // Customer name
            contentTable.AddCell($"{transaction.Customer?.WholeName}");

            // Customer card number
            contentTable.AddCell($"{transaction.Customer?.EvidenceNumber}");

            // Exchange rate value
            var exchangeRateCell = new Cell();
            exchangeRateCell.Add(
                new Paragraph(transaction.ExchangeRateValue.ToString(DecimalFormatTwoDecimals)).SetTextAlignment(TextAlignment.RIGHT));
            contentTable.AddCell(exchangeRateCell);

            // Rounding
            var roundingCell = new Cell();
            roundingCell.Add(
                new Paragraph(transaction.Rounding.ToString(DecimalFormatTwoDecimals)).SetTextAlignment(TextAlignment
                    .RIGHT));
            contentTable.AddCell(roundingCell);

            // Total amount
            var totalCell = new Cell();
            totalCell.Add(
                new Paragraph(transaction.TotalAmount.ToString(DecimalFormatTwoDecimals)).SetTextAlignment(TextAlignment
                    .RIGHT));
            contentTable.AddCell(totalCell);
        }

        #region Footer

        // Total sum label
        var totalSumCell = new Cell(1, 5);
        totalSumCell.Add(new Paragraph(rm.GetString("AmountTotalSummary")));
        contentTable.AddFooterCell(totalSumCell);

        // Exchange rate sum
        var exchangeRateValueSum = filteredList.Sum(t => t.ExchangeRateValue);
        contentTable.AddFooterCell(exchangeRateValueSum.ToString(DecimalFormatTwoDecimals));

        // Rounding sum
        var roundingSum = filteredList.Sum(t => t.Rounding);
        contentTable.AddFooterCell(roundingSum.ToString(DecimalFormatTwoDecimals));

        // Amount total sum
        var amountTotalSum = filteredList.Sum(t => t.TotalAmount);
        contentTable.AddFooterCell(amountTotalSum.ToString(DecimalFormatTwoDecimals));

        #endregion

        #region Setting borders and text aligns

        // Header
        for (var i = 5; i < contentTable.GetHeader().GetChildren().Count; i++)
        {
            var cell = (Cell)contentTable.GetHeader().GetChildren().ElementAt(i);
            cell.SetTextAlignment(TextAlignment.RIGHT);
        }

        foreach (var element in contentTable.GetHeader().GetChildren())
        {
            var cell = (Cell)element;
            cell.SetBorder(null);
            cell.SetBorderBottom(new SolidBorder(1));
        }

        // Content
        foreach (var element in contentTable.GetChildren())
        {
            var cell = (Cell)element;
            cell.SetBorder(null);
        }

        // Footer
        for (var i = 1; i < contentTable.GetFooter().GetChildren().Count; i++)
        {
            var cell = (Cell)contentTable.GetFooter().GetChildren().ElementAt(i);
            cell.SetTextAlignment(TextAlignment.RIGHT);
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

    private static Document AddPurchaseReceiptData(Document document, IEnumerable<TransactionListModel> notFilteredList,
        PdfFont boldFont, PdfFont commonFont)
    {
        var filteredList = notFilteredList
            .Where(t => !t.IsCanceled && t.TransactionType == TransactionType.Buy)
            .GroupBy(t => new { t.CurrencyCode, t.CourseRate })
            .Select(g => new TotalBalanceReceiptModel
            {
                CurrencyCode = g.Key.CurrencyCode,
                CourseRate = g.Key.CourseRate,
                Quantity = g.Sum(t => t.Quantity)
            })
            .ToList();

        var rm = new ResourceManager(typeof(PrinterTotalBalanceResources));

        var headerSectionPurchaseReceipt =
            new Paragraph(rm.GetString("SectionHeaderPurchaseReceiptComplete"))
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFont(boldFont)
                .SetFontSize(HeaderFontSize);
        document.Add(headerSectionPurchaseReceipt);
        document.Add(new LineSeparator(new SolidLine()));

        var contentTable = new Table(5)
            .UseAllAvailableWidth()
            .SetFont(commonFont)
            .SetFontSize(CommonFontSize)
            .SetMarginTop(20);

        #region Header

        contentTable.AddHeaderCell(rm.GetString("ListHeaderItemCurrencyCode"));
        contentTable.AddHeaderCell(rm.GetString("ListHeaderItemState"));
        contentTable.AddHeaderCell(rm.GetString("ListHeaderItemCourseRate"));
        contentTable.AddHeaderCell(rm.GetString("ListHeaderItemQuantity"));
        contentTable.AddHeaderCell(rm.GetString("ListHeaderItemExchangeRateValue"));

        #endregion

        foreach (var transaction in filteredList)
        {
            // Currency code cell
            contentTable.AddCell(transaction.CurrencyCode);

            // State cell
            var currencyCodeConverter = new CurrencyCodeToStateConverter();
            var state = (string)currencyCodeConverter.Convert(transaction.CurrencyCode, typeof(string), null, CultureInfo.CurrentCulture);
            contentTable.AddCell(state);

            // Course rate cell
            var courseCell = new Cell();
            courseCell.Add(
                new Paragraph(transaction.CourseRate.ToString(AverageCourseFormat))
                    .SetTextAlignment(TextAlignment.RIGHT));
            contentTable.AddCell(courseCell);

            // Quantity cell
            var quantityCell = new Cell();
            quantityCell.Add(new Paragraph(transaction.Quantity.ToString(DecimalFormatTwoDecimals))
                .SetTextAlignment(TextAlignment.RIGHT));
            contentTable.AddCell(quantityCell);

            // Exchange rate value cell
            var exchangeRateCell = new Cell();
            exchangeRateCell.Add(new Paragraph(transaction.ExchangeRateValue.ToString(DecimalFormatTwoDecimals))
                .SetTextAlignment(TextAlignment.RIGHT));
            contentTable.AddCell(exchangeRateCell);
        }

        #region Footer

        var amountTotalCell = new Cell(1, 3).SetTextAlignment(TextAlignment.LEFT);
        amountTotalCell.Add(new Paragraph($"{rm.GetString("AmountTotalSummary")}"));
        contentTable.AddFooterCell(amountTotalCell);

        var totalExchangeRate = filteredList.Sum(t => t.ExchangeRateValue);
        var totalExchangeRateCell = new Cell(1, 2);
        totalExchangeRateCell.Add(
            new Paragraph(totalExchangeRate.ToString(DecimalFormatTwoDecimals)).SetFontSize(HeaderFontSize).SetTextAlignment(TextAlignment.RIGHT));
        contentTable.AddFooterCell(totalExchangeRateCell);

        #endregion

        #region Setting borders and text aligns

        for (var i = 2; i < contentTable.GetHeader().GetChildren().Count; i++)
        {
            var cell = (Cell)contentTable.GetHeader().GetChildren().ElementAt(i);
            cell.SetTextAlignment(TextAlignment.RIGHT);
        }

        foreach (var element in contentTable.GetHeader().GetChildren())
        {
            var cell = (Cell)element;
            cell.SetBorder(null);
            cell.SetBorderBottom(new SolidBorder(1));
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

    private static Document AddSalesReceiptData(Document document, IEnumerable<TransactionListModel> notFilteredList,
        PdfFont boldFont, PdfFont commonFont)
    {
        var filteredList = notFilteredList
            .Where(t => !t.IsCanceled && t.TransactionType == TransactionType.Sell)
            .GroupBy(t => new { t.CurrencyCode, t.CourseRate })
            .Select(g => new TotalBalanceReceiptModel
            {
                CurrencyCode = g.Key.CurrencyCode,
                CourseRate = g.Key.CourseRate,
                Quantity = g.Sum(t => t.Quantity)
            })
            .ToList();

        var rm = new ResourceManager(typeof(PrinterTotalBalanceResources));

        var headerSectionSalesReceipt =
            new Paragraph(rm.GetString("SectionHeaderSalesReceiptComplete"))
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFont(boldFont)
                .SetFontSize(HeaderFontSize);
        document.Add(headerSectionSalesReceipt);
        document.Add(new LineSeparator(new SolidLine()));

        var contentTable = new Table(5)
            .UseAllAvailableWidth()
            .SetFont(commonFont)
            .SetFontSize(CommonFontSize)
            .SetMarginTop(20);

        #region Header

        contentTable.AddHeaderCell(rm.GetString("ListHeaderItemCurrencyCode"));
        contentTable.AddHeaderCell(rm.GetString("ListHeaderItemState"));
        contentTable.AddHeaderCell(rm.GetString("ListHeaderItemCourseRate"));
        contentTable.AddHeaderCell(rm.GetString("ListHeaderItemQuantity"));
        contentTable.AddHeaderCell(rm.GetString("ListHeaderItemExchangeRateValue"));

        #endregion

        foreach (var transaction in filteredList)
        {
            // Currency code cell
            contentTable.AddCell(transaction.CurrencyCode);

            // State cell
            var currencyCodeConverter = new CurrencyCodeToStateConverter();
            var state = (string)currencyCodeConverter.Convert(transaction.CurrencyCode, typeof(string), null, CultureInfo.CurrentCulture);
            contentTable.AddCell(state);

            // Course rate cell
            var courseCell = new Cell();
            courseCell.Add(
                new Paragraph(transaction.CourseRate.ToString(AverageCourseFormat))
                    .SetTextAlignment(TextAlignment.RIGHT));
            contentTable.AddCell(courseCell);

            // Quantity cell
            var quantityCell = new Cell();
            quantityCell.Add(new Paragraph(transaction.Quantity.ToString(DecimalFormatTwoDecimals))
                .SetTextAlignment(TextAlignment.RIGHT));
            contentTable.AddCell(quantityCell);

            // Exchange rate value cell
            var exchangeRateCell = new Cell();
            exchangeRateCell.Add(new Paragraph(transaction.ExchangeRateValue.ToString(DecimalFormatTwoDecimals))
                .SetTextAlignment(TextAlignment.RIGHT));
            contentTable.AddCell(exchangeRateCell);
        }

        #region Footer

        var amountTotalCell = new Cell(1, 3).SetTextAlignment(TextAlignment.LEFT);
        amountTotalCell.Add(new Paragraph($"{rm.GetString("AmountTotalSummary")}"));
        contentTable.AddFooterCell(amountTotalCell);

        var totalExchangeRate = filteredList.Sum(t => t.ExchangeRateValue);
        var totalExchangeRateCell = new Cell(1, 2);
        totalExchangeRateCell.Add(
            new Paragraph(totalExchangeRate.ToString(DecimalFormatTwoDecimals)).SetFontSize(HeaderFontSize).SetTextAlignment(TextAlignment.RIGHT));
        contentTable.AddFooterCell(totalExchangeRateCell);

        #endregion

        #region Setting borders and text aligns

        for (var i = 2; i < contentTable.GetHeader().GetChildren().Count; i++)
        {
            var cell = (Cell)contentTable.GetHeader().GetChildren().ElementAt(i);
            cell.SetTextAlignment(TextAlignment.RIGHT);
        }

        foreach (var element in contentTable.GetHeader().GetChildren())
        {
            var cell = (Cell)element;
            cell.SetBorder(null);
            cell.SetBorderBottom(new SolidBorder(1));
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

    private static Document AddProfitAbbreviatedListData(Document document, List<CurrencyProfitModel> profitList, PdfFont boldFont,
        PdfFont commonFont)
    {
        var rm = new ResourceManager(typeof(PrinterTotalBalanceResources));

        var header = new Paragraph(rm.GetString("SectionHeaderProfitAbbreviated"))
            .SetTextAlignment(TextAlignment.CENTER)
            .SetFont(boldFont)
            .SetFontSize(HeaderFontSize);
        document.Add(header);
        document.Add(new LineSeparator(new SolidLine()));

        var contentTable = new Table(3)
            .UseAllAvailableWidth()
            .SetFont(commonFont)
            .SetFontSize(CommonFontSize)
            .SetMarginTop(20);

        #region Header

        contentTable.AddHeaderCell(rm.GetString("ListHeaderItemCurrencyCode"));
        contentTable.AddHeaderCell(rm.GetString("ListHeaderItemState"));
        contentTable.AddHeaderCell(rm.GetString("ListHeaderItemProfit"));

        #endregion

        foreach (var model in profitList)
        {
            // Currency code
            contentTable.AddCell(model.Code);

            // State
            var currencyCodeConverter = new CurrencyCodeToStateConverter();
            var state = (string)currencyCodeConverter.Convert(model.Code, typeof(string), null, CultureInfo.CurrentCulture);
            contentTable.AddCell(state);

            // Profit
            var profitCell = new Cell();
            profitCell.Add(new Paragraph(model.Profit.ToString(DecimalFormatTwoDecimals)).SetTextAlignment(TextAlignment.RIGHT));
            contentTable.AddCell(profitCell);
        }

        #region Footer

        var profitLabelCell = new Cell(1, 2);
        profitLabelCell.Add(new Paragraph(rm.GetString("ProfitTotalLabel")));
        contentTable.AddFooterCell(profitLabelCell);

        var profitSum = profitList.Sum(e => e.Profit);
        var profitSumCell = new Cell();
        profitSumCell.Add(new Paragraph(profitSum.ToString(DecimalFormatTwoDecimals)).SetFontSize(HeaderFontSize).SetTextAlignment(TextAlignment.RIGHT));
        contentTable.AddFooterCell(profitSumCell);

        #endregion

        #region Setting borders and text aligns

        var headerCellProfit = (Cell)contentTable.GetHeader().GetChildren().ElementAt(2);
        headerCellProfit.SetTextAlignment(TextAlignment.RIGHT);

        foreach (var element in contentTable.GetHeader().GetChildren())
        {
            var cell = (Cell)element;
            cell.SetBorder(null);
            cell.SetBorderBottom(new SolidBorder(1));
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

    private static Document AddProfitCompleteListData(Document document, List<OperationProfitModel> profits,
        PdfFont boldFont, PdfFont commonFont)
    {
        var rm = new ResourceManager(typeof(PrinterTotalBalanceResources));

        var header = new Paragraph(rm.GetString("SectionHeaderProfitComplete"))
            .SetTextAlignment(TextAlignment.CENTER)
            .SetFont(boldFont)
            .SetFontSize(HeaderFontSize);
        document.Add(header);
        document.Add(new LineSeparator(new SolidLine()));

        var contentTable = new Table(6)
            .UseAllAvailableWidth()
            .SetFont(commonFont)
            .SetFontSize(CommonFontSize)
            .SetMarginTop(20);

        #region Header

        contentTable.AddHeaderCell(rm.GetString("ListHeaderItemOperationId"));
        contentTable.AddHeaderCell(rm.GetString("ListHeaderItemCurrencyCode"));
        contentTable.AddHeaderCell(rm.GetString("ListHeaderItemQuantity"));
        contentTable.AddHeaderCell(rm.GetString("ListHeaderItemCourseRate"));
        contentTable.AddHeaderCell(rm.GetString("ListHeaderItemExchangeRateValue"));
        contentTable.AddHeaderCell(rm.GetString("ListHeaderItemProfit"));

        #endregion

        foreach (var operation in profits)
        {
            // Operation id
            contentTable.AddCell($"{operation.Id}");

            // Currency code
            contentTable.AddCell(operation.CurrencyCode);

            // Quantity cell
            var quantityCell = new Cell();
            quantityCell.Add(new Paragraph(operation.Quantity.ToString(DecimalFormatTwoDecimals))
                .SetTextAlignment(TextAlignment.RIGHT));
            contentTable.AddCell(quantityCell);

            // Course rate cell
            var courseCell = new Cell();
            courseCell.Add(
                new Paragraph(operation.CourseRate.ToString(AverageCourseFormat))
                    .SetTextAlignment(TextAlignment.RIGHT));
            contentTable.AddCell(courseCell);

            // Exchange rate cell
            var exchangeCell = new Cell();
            exchangeCell.Add(
                new Paragraph(operation.ExchangeRateValue.ToString(DecimalFormatTwoDecimals))
                    .SetTextAlignment(TextAlignment.RIGHT));
            contentTable.AddCell(exchangeCell);

            // Profit cell
            var profitCell = new Cell();
            profitCell.Add(new Paragraph(operation.Profit.ToString(DecimalFormatTwoDecimals))
                .SetTextAlignment(TextAlignment.RIGHT));
            contentTable.AddCell(profitCell);
        }

        #region Setting borders and text aligns

        for (var i = 2; i < contentTable.GetHeader().GetChildren().Count; i++)
        {
            var cell = (Cell)contentTable.GetHeader().GetChildren().ElementAt(i);
            cell.SetTextAlignment(TextAlignment.RIGHT);
        }

        foreach (var element in contentTable.GetHeader().GetChildren())
        {
            var cell = (Cell)element;
            cell.SetBorder(null);
            cell.SetBorderBottom(new SolidBorder(1));
        }

        foreach (var element in contentTable.GetChildren())
        {
            var cell = (Cell)element;
            cell.SetBorder(null);
        }

        #endregion

        document.Add(contentTable);

        return document;
    }

    private async Task<Document> AddAllOperationsData(Document document, List<OperationListModelBase> operations, List<TotalBalanceModel> totalBalances,
        PdfFont boldFont, PdfFont commonFont)
    {
        var rm = new ResourceManager(typeof(PrinterTotalBalanceResources));

        var header = new Paragraph(rm.GetString("SectionHeaderCurrenciesMovement"))
            .SetTextAlignment(TextAlignment.CENTER)
            .SetFont(boldFont)
            .SetFontSize(HeaderFontSize);
        document.Add(header);
        document.Add(new LineSeparator(new SolidLine()));

        var contentTable = new Table(8)
            .UseAllAvailableWidth()
            .SetFont(commonFont)
            .SetFontSize(SmallFontSize)
            .SetMarginTop(20);

        #region Header

        contentTable.AddHeaderCell(rm.GetString("ListHeaderItemDate"));
        contentTable.AddHeaderCell(rm.GetString("ListHeaderItemOperationId"));
        contentTable.AddHeaderCell(rm.GetString("ListHeaderItemOperationType"));
        contentTable.AddHeaderCell(rm.GetString("ListHeaderItemBuy"));
        contentTable.AddHeaderCell(rm.GetString("ListHeaderItemSell"));
        contentTable.AddHeaderCell(rm.GetString("ListHeaderItemDeposit"));
        contentTable.AddHeaderCell(rm.GetString("ListHeaderItemWithdraw"));
        contentTable.AddHeaderCell(rm.GetString("ListHeaderItemBalance"));

        #endregion

        var lastPrintedCode = string.Empty;
        var lastPrintedDate = DateTime.MinValue;
        var buySum = 0m;
        var sellSum = 0m;
        var depositSum = 0m;
        var withdrawSum = 0m;

        // Add empty item at the end of the operations to print total balance after last row
        operations.Add(new OperationListModelBase
        {
            Id = 0,
            Created = DateTime.Now,
            CourseRate = 0,
            CurrencyCode = string.Empty,
            IsCanceled = false,
            Quantity = 0
        });

        foreach (var item in operations)
        {
            var totalBalancesToPrint = totalBalances
                .Where(e => e.Created > lastPrintedDate && e.Created < item.Created || lastPrintedDate != DateTime.MinValue &&
                            e.Created > lastPrintedDate && item.CurrencyCode != lastPrintedCode)
                .ToList();
            foreach (var totalBalance in totalBalancesToPrint)
            {
                // Date
                var dateCellTb = new Cell().SetBorder(null);
                dateCellTb.Add(new Paragraph(totalBalance.Created.ToShortDateString()).SetFont(boldFont));
                contentTable.AddCell(dateCellTb);

                // Operation id
                var idCellTb = new Cell().SetBorder(null);
                idCellTb.Add(new Paragraph(totalBalance.Id.ToString()));
                contentTable.AddCell(idCellTb);

                // Operation type
                var typeCellTb = new Cell().SetBorder(null);
                var typeTb = totalBalance.Type == TotalBalanceType.Daily
                    ? rm.GetString("DailyTotalBalance")
                    : rm.GetString("MonthlyTotalBalance");
                typeCellTb.Add(new Paragraph(typeTb));
                contentTable.AddCell(typeCellTb);

                // Buy sum
                var buySumCell = new Cell().SetBorder(null);
                buySumCell.Add(
                    new Paragraph(buySum.ToString(DecimalFormatTwoDecimals)).SetFont(boldFont).SetTextAlignment(TextAlignment.RIGHT));
                contentTable.AddCell(buySumCell);

                // Sell sum
                var sellSumCell = new Cell().SetBorder(null);
                sellSumCell.Add(
                    new Paragraph(sellSum.ToString(DecimalFormatTwoDecimals)).SetFont(boldFont).SetTextAlignment(TextAlignment.RIGHT));
                contentTable.AddCell(sellSumCell);

                // Deposit sum
                var depositSumCell = new Cell().SetBorder(null);
                depositSumCell.Add(
                    new Paragraph(depositSum.ToString(DecimalFormatTwoDecimals)).SetFont(boldFont).SetTextAlignment(TextAlignment.RIGHT));
                contentTable.AddCell(depositSumCell);

                // Withdraw sum
                var withdrawSumCell = new Cell().SetBorder(null);
                withdrawSumCell.Add(
                    new Paragraph(withdrawSum.ToString(DecimalFormatTwoDecimals)).SetFont(boldFont).SetTextAlignment(TextAlignment.RIGHT));
                contentTable.AddCell(withdrawSumCell);

                // Balance
                var balanceCell = new Cell().SetBorder(null);
                var balance = await _currencyFacade.GetCurrencyBalance(lastPrintedCode, totalBalance.Created);
                balanceCell.Add(new Paragraph(balance.ToString(DecimalFormatTwoDecimals)).SetFont(boldFont).SetTextAlignment(TextAlignment.RIGHT));
                contentTable.AddCell(balanceCell);

                // Empty row
                contentTable.AddCell(new Cell(1, 8).SetBorder(null));

                buySum = 0;
                sellSum = 0;
                depositSum = 0;
                withdrawSum = 0;
            }

            // Last item skip, it is here only to print last total balance
            if (item.Id == 0) continue;

            // Currency rows header:
            // e.g. Currency code: EUR
            if (lastPrintedCode != item.CurrencyCode)
            {
                var codeLabelCell = new Cell().SetBorder(null).SetBorderBottom(new SolidBorder(1));
                codeLabelCell.Add(new Paragraph($"{rm.GetString("ListHeaderItemCurrencyCode")}:"));
                contentTable.AddCell(codeLabelCell);

                var codeCell = new Cell().SetBorder(null).SetBorderBottom(new SolidBorder(1));
                codeCell.Add(new Paragraph($"{item.CurrencyCode}").SetFont(boldFont));
                contentTable.AddCell(codeCell);

                var emptyCell = new Cell(1, 6).SetBorder(null);
                contentTable.AddCell(emptyCell);

                lastPrintedCode = item.CurrencyCode;
            }

            // Date
            var dateCell = new Cell().SetBorder(null);
            dateCell.Add(new Paragraph(item.Created.ToShortDateString()));
            contentTable.AddCell(dateCell);

            // Operation Id
            var idCell = new Cell().SetBorder(null);
            idCell.Add(new Paragraph(item.Id.ToString()));
            contentTable.AddCell(idCell);

            // Operation Type
            var typeCell = new Cell().SetBorder(null);
            var type = string.Empty;
            switch (item)
            {
                case TransactionListModel transactionItem:
                    {
                        var converter = new TransactionTypeToStringConverter();
                        type = (string)converter.Convert(transactionItem.TransactionType, typeof(TransactionType), null,
                            CultureInfo.CurrentCulture);
                        break;
                    }
                case DonationListModel donationItem:
                    {
                        var converter = new DonationTypeToStringConverter();
                        type = (string)converter.Convert(donationItem.Type, typeof(DonationType), null,
                            CultureInfo.CurrentCulture);
                        break;
                    }
            }

            if (item.IsCanceled)
            {
                type += $" - {rm.GetString("CanceledTextAfter")}";
            }
            typeCell.Add(new Paragraph(type));
            contentTable.AddCell(typeCell);

            // Quantity
            if (!item.IsCanceled)
            {
                switch (item)
                {
                    case TransactionListModel { TransactionType: TransactionType.Buy }:
                        var buyCell = new Cell().SetBorder(null);
                        buyCell.Add(
                            new Paragraph(item.Quantity.ToString(DecimalFormatTwoDecimals)).SetTextAlignment(TextAlignment
                                .RIGHT));
                        contentTable.AddCell(buyCell);
                        contentTable.AddCell(new Cell(1, 4).SetBorder(null));
                        buySum += item.Quantity;
                        break;
                    case TransactionListModel { TransactionType: TransactionType.Sell }:
                        contentTable.AddCell(new Cell().SetBorder(null));
                        var sellCell = new Cell().SetBorder(null);
                        sellCell.Add(
                            new Paragraph(item.Quantity.ToString(DecimalFormatTwoDecimals)).SetTextAlignment(TextAlignment
                                .RIGHT));
                        contentTable.AddCell(sellCell);
                        contentTable.AddCell(new Cell(1, 3).SetBorder(null));
                        sellSum -= item.Quantity;
                        break;
                    case DonationListModel { Type: DonationType.Deposit }:
                        contentTable.AddCell(new Cell(1, 2).SetBorder(null));
                        var depositCell = new Cell().SetBorder(null);
                        depositCell.Add(
                            new Paragraph(item.Quantity.ToString(DecimalFormatTwoDecimals)).SetTextAlignment(TextAlignment
                                .RIGHT));
                        contentTable.AddCell(depositCell);
                        contentTable.AddCell(new Cell(1, 2).SetBorder(null));
                        depositSum += item.Quantity;
                        break;
                    case DonationListModel { Type: DonationType.Withdraw or DonationType.Levy }:
                        contentTable.AddCell(new Cell(1, 3).SetBorder(null));
                        var withdrawCell = new Cell().SetBorder(null);
                        withdrawCell.Add(
                            new Paragraph(item.Quantity.ToString(DecimalFormatTwoDecimals)).SetTextAlignment(TextAlignment
                                .RIGHT));
                        contentTable.AddCell(withdrawCell);
                        contentTable.AddCell(new Cell().SetBorder(null));
                        withdrawSum -= item.Quantity;
                        break;
                }
            }
            else
            {
                contentTable.AddCell(new Cell(1, 5).SetBorder(null));
            }

            lastPrintedDate = item.Created;
        }

        #region Borders

        foreach (var element in contentTable.GetHeader().GetChildren())
        {
            var cell = (Cell)element;
            cell.SetFontSize(CommonFontSize);
            cell.SetBorder(null);
            cell.SetBorderBottom(new SolidBorder(1));
        }

        for (var i = 3; i < contentTable.GetHeader().GetChildren().Count; i++)
        {
            var cell = (Cell)contentTable.GetHeader().GetChildren().ElementAt(i);
            cell.SetTextAlignment(TextAlignment.RIGHT);
            cell.SetPaddings(0, 5, 0, 5);
        }

        #endregion

        document.Add(contentTable);

        return document;
    }

    private record TotalBalanceReceiptModel
    {
        public required string CurrencyCode { get; init; }
        public required decimal CourseRate { get; init; }
        public required decimal Quantity { get; init; }

        public decimal ExchangeRateValue => CourseRate != 0 ? Math.Round(Quantity / CourseRate, 2) : 0;
    }
}