using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ExchangeApp.BL.Facades.Interfaces;
using ExchangeApp.BL.Models.Company;

namespace ExchangeApp.App.ViewModels.Settings;

public partial class SettingsInfoCompanyViewModel : ViewModelBase
{
    private readonly ISettingsFacade _settingsFacade;

    public SettingsInfoCompanyViewModel(ISettingsFacade settingsFacade)
    {
        _settingsFacade = settingsFacade;
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        CompanyModel = await _settingsFacade.GetCompanyDataAsync() ?? CompanyDetailModel.Empty;
    }

    [ObservableProperty] 
    private CompanyDetailModel _companyModel = null!;

    [RelayCommand]
    private async Task SaveAsync()
    {
        await _settingsFacade.UpdateSettingsDataAsync(CompanyModel);
        await Shell.Current.GoToAsync("..");
    }
}
