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

        AboutData = data;
    }

    [ObservableProperty] private AboutData _aboutData = new();
}

public record AboutData
{
    public string ProductName { get; set; } = string.Empty;
    public string AuthorName { get; set; } = string.Empty;
    public string AuthorContact { get; set; } = string.Empty;
}
