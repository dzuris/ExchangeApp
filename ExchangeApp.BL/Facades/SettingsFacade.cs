using System.Text.Json;
using ExchangeApp.BL.Facades.Interfaces;
using ExchangeApp.BL.Models;
using ExchangeApp.BL.Models.Company;

namespace ExchangeApp.BL.Facades;

public class SettingsFacade : ISettingsFacade
{
    private const string FileNameData = "settings_data.json";
    private const string FileNameCompany = "company.json";
    private const string FileNameBranch = "branch.json";

    //public SettingsFacade()
    //{
    //    var folder = Environment.SpecialFolder.LocalApplicationData;
    //    var baseDirectory = Environment.GetFolderPath(folder);
    //    baseDirectory = Path.Combine(baseDirectory, "exchangeApp");

    //    if (!Directory.Exists(baseDirectory))
    //    {
    //        Directory.CreateDirectory(baseDirectory);
    //    }

    //    _fileNameData = Path.Combine(baseDirectory, "settings_data.json");
    //    _fileNameCompany = Path.Combine(baseDirectory, "company.json");
    //    _fileNameBranch = Path.Combine(baseDirectory, "branch.json");
    //}

    private static string GetSettingsDataFileName()
    {
        if (!File.Exists(FileNameData))
        {
            File.Create(FileNameData);
        }

        return FileNameData;
    }

    private static string GetCompanyFileName()
    {
        if (!File.Exists(FileNameCompany))
        {
            File.Create(FileNameCompany);
        }

        return FileNameCompany;
    }

    private static string GetBranchFileName()
    {
        if (!File.Exists(FileNameBranch))
        {
            File.Create(FileNameBranch);
        }

        return FileNameBranch;
    }

    public async Task<SettingsDataModel?> GetSettingsDataAsync()
    {
        if (!File.Exists(FileNameData))
        {
            return null;
        }

        var jsonString = await File.ReadAllTextAsync(FileNameData);
        var data = JsonSerializer.Deserialize<SettingsDataModel>(jsonString);

        return data;
    }

    public async Task<string?> GetSaveFolderPathAsync()
    {
        if (!File.Exists(FileNameData))
        {
            return null;
        }

        var jsonString = await File.ReadAllTextAsync(FileNameData);
        var data = JsonSerializer.Deserialize<SettingsDataModel>(jsonString);

        var result = data?.FolderPath;
        
        if (!Directory.Exists(result))
        {
            return null;
        }

        return string.IsNullOrWhiteSpace(result) ? null : result;
    }

    public async Task<bool> ShouldSaveTransactionsAutomaticallyAsync()
    {
        if (!File.Exists(FileNameData))
        {
            return false;
        }

        var jsonString = await File.ReadAllTextAsync(FileNameData);
        var data = JsonSerializer.Deserialize<SettingsDataModel>(jsonString);

        return data?.AutomaticTransactionSaveOption ?? false;
    }

    public async Task<bool> ShouldSaveDonationsAutomaticallyAsync()
    {
        if (!File.Exists(FileNameData))
        {
            return false;
        }

        var jsonString = await File.ReadAllTextAsync(FileNameData);
        var data = JsonSerializer.Deserialize<SettingsDataModel>(jsonString);

        return data?.AutomaticDonationSaveOption ?? false;
    }

    public async Task<bool> ShouldSaveTotalBalanceAutomaticallyAsync()
    {
        if (!File.Exists(FileNameData))
        {
            return false;
        }

        var jsonString = await File.ReadAllTextAsync(FileNameData);
        var data = JsonSerializer.Deserialize<SettingsDataModel>(jsonString);

        return data?.AutomaticTotalBalanceSaveOption ?? false;
    }

    public async Task<CompanyDetailModel?> GetCompanyDataAsync()
    {
        if (!File.Exists(FileNameCompany))
        {
            return null;
        }

        var jsonString = await File.ReadAllTextAsync(FileNameCompany);
        var data = JsonSerializer.Deserialize<CompanyDetailModel>(jsonString);

        return data;
    }

    public async Task<BranchDetailModel?> GetBranchDataAsync()
    {
        if (!File.Exists(FileNameBranch))
        {
            return null;
        }

        var jsonString = await File.ReadAllTextAsync(FileNameBranch);
        var data = JsonSerializer.Deserialize<BranchDetailModel>(jsonString);

        return data;
    }

    public async Task UpdateSettingsDataAsync(SettingsDataModel model)
    {
        var json = JsonSerializer.Serialize(model);
        await File.WriteAllTextAsync(GetSettingsDataFileName(), json);
    }

    public async Task UpdateSettingsDataAsync(CompanyDetailModel model)
    {
        var json = JsonSerializer.Serialize(model);
        await File.WriteAllTextAsync(GetCompanyFileName(), json);
    }

    public async Task UpdateSettingsDataAsync(BranchDetailModel model)
    {
        var json = JsonSerializer.Serialize(model);
        await File.WriteAllTextAsync(GetBranchFileName(), json);
    }
}