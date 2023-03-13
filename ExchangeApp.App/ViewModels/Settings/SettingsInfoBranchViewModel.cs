using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ExchangeApp.BL.Facades.Interfaces;
using ExchangeApp.BL.Models.Company;

namespace ExchangeApp.App.ViewModels.Settings;

public partial class SettingsInfoBranchViewModel : ViewModelBase
{
    private readonly ISettingsFacade _settingsFacade;

    public SettingsInfoBranchViewModel(ISettingsFacade settingsFacade)
    {
        _settingsFacade = settingsFacade;
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        BranchModel = await _settingsFacade.GetBranchDataAsync() ?? BranchDetailModel.Empty;
    }

    [ObservableProperty]
    private BranchDetailModel _branchModel = null!;

    [RelayCommand]
    private async Task SaveAsync()
    {
        await _settingsFacade.UpdateSettingsDataAsync(BranchModel);
        await Shell.Current.GoToAsync("..");
    }
}
