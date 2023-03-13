using CommunityToolkit.Maui.Storage;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ExchangeApp.BL.Facades.Interfaces;
using ExchangeApp.BL.Models;
using ExchangeApp.Common.Enums;

namespace ExchangeApp.App.ViewModels.Settings;

public partial class SettingsAutomaticOperationsSaveViewModel : ViewModelBase
{
    private readonly ISettingsFacade _settingsFacade;

    public SettingsAutomaticOperationsSaveViewModel(ISettingsFacade settingsFacade)
    {
        _settingsFacade = settingsFacade;
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        SettingsData = await _settingsFacade.GetSettingsDataAsync() ?? SettingsDataModel.Empty;
    }

    public List<DonationSaveFormatEnum> DonationSaveFormats =>
        Enum.GetValues(typeof(DonationSaveFormatEnum)).Cast<DonationSaveFormatEnum>().ToList();

    [ObservableProperty]
    private SettingsDataModel _settingsData = null!;

    [RelayCommand]
    private async Task PickFolderAsync()
    {
        var folder = await FolderPicker.PickAsync(default);
        SettingsData.FolderPath = folder.Folder?.Path ?? "Unknown result";

        OnPropertyChanged(nameof(SettingsData));
    }

    [RelayCommand]
    private async Task SaveAsync()
    {
        await _settingsFacade.UpdateSettingsDataAsync(SettingsData);
        await Shell.Current.GoToAsync("..");
    }
}