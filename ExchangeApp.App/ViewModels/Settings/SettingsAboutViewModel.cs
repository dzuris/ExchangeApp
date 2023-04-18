using System.ComponentModel;
using System.Text.Json;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ExchangeApp.App.ViewModels.Settings;

public partial class SettingsAboutViewModel : ViewModelBase
{
    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        await using var stream = await FileSystem.OpenAppPackageFileAsync("about.json");
        using var reader = new StreamReader(stream);

        var jsonText = await reader.ReadToEndAsync();
        var data = JsonSerializer.Deserialize<AboutData>(jsonText);

        if (data is null) return;

        License = data.License;
        AuthorName = data.AuthorName;
        AuthorContact = data.AuthorContact;
    }

    [ObservableProperty] private string _license = string.Empty;
    [ObservableProperty] private string _authorName = string.Empty;
    [ObservableProperty] private string _authorContact = string.Empty;
}

internal record AboutData
{
    public string License { get; set; } = string.Empty;
    public string AuthorName { get; set; } = string.Empty;
    public string AuthorContact { get; set; } = string.Empty;
}
