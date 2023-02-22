using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ExchangeApp.App.ViewModels;

public class ViewModelBase : INotifyPropertyChanged, IViewModel
{
    private bool _isBusy;

    public bool IsBusy
    {
        get => _isBusy;
        set
        {
            if (_isBusy.Equals(value))
            {
                return;
            }
            _isBusy = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(IsNotBusy));
        }
    }

    public bool IsNotBusy => !IsBusy;

    public event PropertyChangedEventHandler PropertyChanged;

    public void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public async Task OnAppearingAsync()
    {
        await LoadDataAsync();
    }

    protected virtual Task LoadDataAsync() 
        => Task.CompletedTask;
}