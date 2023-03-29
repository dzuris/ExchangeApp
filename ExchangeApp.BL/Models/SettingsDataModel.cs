namespace ExchangeApp.BL.Models;

public record SettingsDataModel : ModelBase
{
    public string FolderPath { get; set; } = string.Empty;
    public bool AutomaticTransactionSaveOption { get; set; }
    public bool AutomaticDonationSaveOption { get; set; }
    public bool AutomaticTotalBalanceSaveOption { get; set; }

    public static SettingsDataModel Empty => new()
    {
        FolderPath = string.Empty,
        AutomaticTransactionSaveOption = false,
        AutomaticDonationSaveOption = false,
        AutomaticTotalBalanceSaveOption = false
    };
}