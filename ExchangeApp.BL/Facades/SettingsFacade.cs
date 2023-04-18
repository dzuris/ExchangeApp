using System.Text.Json;
using ExchangeApp.BL.Facades.Interfaces;
using ExchangeApp.BL.Models;
using ExchangeApp.BL.Models.Company;

namespace ExchangeApp.BL.Facades;

public class SettingsFacade : ISettingsFacade
{
    private readonly string _fileNameData = "settings_data.json";
    private readonly string _fileNameCompany = "company.json";
    private readonly string _fileNameBranch = "branch.json";

    public SettingsFacade()
    {
        var baseDirectory = FileSystem.AppDataDirectory;

        _fileNameData = Path.Combine(baseDirectory, _fileNameData);
        _fileNameCompany = Path.Combine(baseDirectory, _fileNameCompany);
        _fileNameBranch = Path.Combine(baseDirectory, _fileNameBranch);
    }

    public async Task<SettingsDataModel?> GetSettingsDataAsync()
    {
        if (!File.Exists(_fileNameData))
        {
            return null;
        }

        var jsonString = await File.ReadAllTextAsync(_fileNameData);
        var data = JsonSerializer.Deserialize<SettingsDataModel>(jsonString);

        return data;
    }

    public async Task<string?> GetSaveFolderPathAsync()
    {
        if (!File.Exists(_fileNameData))
        {
            return null;
        }

        var jsonString = await File.ReadAllTextAsync(_fileNameData);
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
        if (!File.Exists(_fileNameData))
        {
            return false;
        }

        var jsonString = await File.ReadAllTextAsync(_fileNameData);
        var data = JsonSerializer.Deserialize<SettingsDataModel>(jsonString);

        return data?.AutomaticTransactionSaveOption ?? false;
    }

    public async Task<bool> ShouldSaveDonationsAutomaticallyAsync()
    {
        if (!File.Exists(_fileNameData))
        {
            return false;
        }

        var jsonString = await File.ReadAllTextAsync(_fileNameData);
        var data = JsonSerializer.Deserialize<SettingsDataModel>(jsonString);

        return data?.AutomaticDonationSaveOption ?? false;
    }

    public async Task<bool> ShouldSaveTotalBalanceAutomaticallyAsync()
    {
        if (!File.Exists(_fileNameData))
        {
            return false;
        }

        var jsonString = await File.ReadAllTextAsync(_fileNameData);
        var data = JsonSerializer.Deserialize<SettingsDataModel>(jsonString);

        return data?.AutomaticTotalBalanceSaveOption ?? false;
    }

    public async Task<CompanyDetailModel?> GetCompanyDataAsync()
    {
        if (!File.Exists(_fileNameCompany))
        {
            return null;
        }

        var jsonString = await File.ReadAllTextAsync(_fileNameCompany);
        var data = JsonSerializer.Deserialize<CompanyDetailModel>(jsonString);

        return data;
    }

    public async Task<BranchDetailModel?> GetBranchDataAsync()
    {
        if (!File.Exists(_fileNameBranch))
        {
            return null;
        }

        var jsonString = await File.ReadAllTextAsync(_fileNameBranch);
        var data = JsonSerializer.Deserialize<BranchDetailModel>(jsonString);

        return data;
    }

    public async Task UpdateSettingsDataAsync(SettingsDataModel model)
    {
        if (!File.Exists(_fileNameData))
        {
            await using (File.Create(_fileNameData)) { }
        }

        var json = JsonSerializer.Serialize(model);
        await File.WriteAllTextAsync(_fileNameData, json);
    }

    public async Task UpdateSettingsDataAsync(CompanyDetailModel model)
    {
        if (!File.Exists(_fileNameCompany))
        {
            await using (File.Create(_fileNameCompany)) { }
        }

        var json = JsonSerializer.Serialize(model);
        await File.WriteAllTextAsync(_fileNameCompany, json);
    }

    public async Task UpdateSettingsDataAsync(BranchDetailModel model)
    {
        if (!File.Exists(_fileNameBranch))
        {
            await using (File.Create(_fileNameBranch)) { }
        }

        var json = JsonSerializer.Serialize(model);
        await File.WriteAllTextAsync(_fileNameBranch, json);
    }
}