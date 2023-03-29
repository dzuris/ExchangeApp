using CommunityToolkit.Maui.Storage;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ExchangeApp.BL.Facades.Interfaces;
using ExchangeApp.BL.Models;

namespace ExchangeApp.App.ViewModels.Settings;

public partial class SettingsGeneralViewModel : ViewModelBase
{
    private readonly ISettingsFacade _settingsFacade;

    public SettingsGeneralViewModel(ISettingsFacade settingsFacade)
    {
        _settingsFacade = settingsFacade;
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        SettingsData = await _settingsFacade.GetSettingsDataAsync() ?? SettingsDataModel.Empty;
    }

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