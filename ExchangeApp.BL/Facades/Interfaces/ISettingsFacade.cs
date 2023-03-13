using ExchangeApp.BL.Models;
using ExchangeApp.BL.Models.Company;
using ExchangeApp.Common.Enums;

namespace ExchangeApp.BL.Facades.Interfaces;

public interface ISettingsFacade : IFacade
{
    Task<SettingsDataModel?> GetSettingsDataAsync();
    Task<CompanyDetailModel?> GetCompanyDataAsync();
    Task<BranchDetailModel?> GetBranchDataAsync();
    Task UpdateSettingsDataAsync(SettingsDataModel model);
    Task UpdateSettingsDataAsync(CompanyDetailModel model);
    Task UpdateSettingsDataAsync(BranchDetailModel model);
}