using System.ComponentModel;
using System.Runtime.CompilerServices;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ExchangeApp.App.ViewModels;

public class ViewModelBase : ObservableObject, IViewModel
{
    public async Task OnAppearingAsync()
    {
        await LoadDataAsync();
    }

    protected virtual Task LoadDataAsync() 
        => Task.CompletedTask;
}