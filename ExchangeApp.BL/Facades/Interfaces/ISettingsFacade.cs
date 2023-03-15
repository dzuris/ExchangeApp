using ExchangeApp.BL.Models;
using ExchangeApp.BL.Models.Company;
using ExchangeApp.Common.Enums;

namespace ExchangeApp.BL.Facades.Interfaces;

public interface ISettingsFacade : IFacade
{
    Task<SettingsDataModel?> GetSettingsDataAsync();
    Task<string?> GetSaveFolderPathAsync();
    Task<bool> ShouldSaveTransactionsAutomaticallyAsync();
    Task<bool> ShouldSaveDonationsAutomaticallyAsync();
    Task<CompanyDetailModel?> GetCompanyDataAsync();
    Task<BranchDetailModel?> GetBranchDataAsync();
    Task UpdateSettingsDataAsync(SettingsDataModel model);
    Task UpdateSettingsDataAsync(CompanyDetailModel model);
    Task UpdateSettingsDataAsync(BranchDetailModel model);
}