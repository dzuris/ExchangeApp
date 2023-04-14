using System.Text.Json;
using ExchangeApp.BL.Facades.Interfaces;
using ExchangeApp.BL.Models;
using ExchangeApp.BL.Models.Company;

namespace ExchangeApp.BL.Facades;

public class SettingsFacade : ISettingsFacade
{
    private readonly string _fileNameData;
    private readonly string _fileNameCompany;
    private readonly string _fileNameBranch;

    public SettingsFacade()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        //var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        var baseDirectory = Path.Combine(path, "ExchangeApp");

        if (!Directory.Exists(baseDirectory))
        {
            Directory.CreateDirectory(baseDirectory);
        }

        _fileNameData = Path.Combine(baseDirectory, "settings_data.json");
        _fileNameCompany = Path.Combine(baseDirectory, "company.json");
        _fileNameBranch = Path.Combine(baseDirectory, "branch.json");

        if (!File.Exists(_fileNameData)) File.Create(_fileNameData);
        if (!File.Exists(_fileNameCompany)) File.Create(_fileNameCompany);
        if (!File.Exists(_fileNameBranch)) File.Create(_fileNameBranch);
    }

    public async Task<SettingsDataModel?> GetSettingsDataAsync()
    {
        var jsonString = await File.ReadAllTextAsync(_fileNameData);
        var data = JsonSerializer.Deserialize<SettingsDataModel>(jsonString);

        return data;
    }

    public async Task<string?> GetSaveFolderPathAsync()
    {
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
        var jsonString = await File.ReadAllTextAsync(_fileNameData);
        var data = JsonSerializer.Deserialize<SettingsDataModel>(jsonString);

        return data?.AutomaticTransactionSaveOption ?? false;
    }

    public async Task<bool> ShouldSaveDonationsAutomaticallyAsync()
    {
        var jsonString = await File.ReadAllTextAsync(_fileNameData);
        var data = JsonSerializer.Deserialize<SettingsDataModel>(jsonString);

        return data?.AutomaticDonationSaveOption ?? false;
    }

    public async Task<bool> ShouldSaveTotalBalanceAutomaticallyAsync()
    {
        var jsonString = await File.ReadAllTextAsync(_fileNameData);
        var data = JsonSerializer.Deserialize<SettingsDataModel>(jsonString);

        return data?.AutomaticTotalBalanceSaveOption ?? false;
    }

    public async Task<CompanyDetailModel?> GetCompanyDataAsync()
    {
        var jsonString = await File.ReadAllTextAsync(_fileNameCompany);
        var data = JsonSerializer.Deserialize<CompanyDetailModel>(jsonString);

        return data;
    }

    public async Task<BranchDetailModel?> GetBranchDataAsync()
    {
        var jsonString = await File.ReadAllTextAsync(_fileNameBranch);
        var data = JsonSerializer.Deserialize<BranchDetailModel>(jsonString);

        return data;
    }

    public async Task UpdateSettingsDataAsync(SettingsDataModel model)
    {
        var json = JsonSerializer.Serialize(model);
        await File.WriteAllTextAsync(_fileNameData, json);
    }

    public async Task UpdateSettingsDataAsync(CompanyDetailModel model)
    {
        var json = JsonSerializer.Serialize(model);
        await File.WriteAllTextAsync(_fileNameCompany, json);
    }

    public async Task UpdateSettingsDataAsync(BranchDetailModel model)
    {
        var json = JsonSerializer.Serialize(model);
        await File.WriteAllTextAsync(_fileNameBranch, json);
    }
}