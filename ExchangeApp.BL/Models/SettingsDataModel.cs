using ExchangeApp.Common.Enums;

namespace ExchangeApp.BL.Models;

public record SettingsDataModel : ModelBase
{
    public string FolderPath { get; set; } = string.Empty;
    public bool AutomaticTransactionSaveOption { get; set; }
    public bool AutomaticDonationSaveOption { get; set; }
    public DonationSaveFormatEnum DonationSaveForm { get; set; }

    public static SettingsDataModel Empty => new()
    {
        FolderPath = string.Empty,
        AutomaticTransactionSaveOption = false,
        AutomaticDonationSaveOption = false,
        DonationSaveForm = DonationSaveFormatEnum.OmegaTxt
    };
}