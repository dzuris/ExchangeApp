using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ExchangeApp.BL.Models;

public abstract record ModelBase : INotifyPropertyChanged, IModel
{
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}