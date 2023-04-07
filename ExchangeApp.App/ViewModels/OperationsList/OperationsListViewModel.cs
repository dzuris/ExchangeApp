using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ExchangeApp.App.Views.Donation;
using ExchangeApp.App.Views.Transaction;
using ExchangeApp.BL.Facades.Interfaces;
using ExchangeApp.BL.Models;
using ExchangeApp.Common.Enums;

namespace ExchangeApp.App.ViewModels.OperationsList;

public partial class OperationsListViewModel : ViewModelBase
{
    private int _pageNumber = 1;
    private const int PageSize = 20;
    private readonly IOperationFacade _operationFacade;

    public OperationsListViewModel(IOperationFacade operationFacade)
    {
        _operationFacade = operationFacade;
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        FilterUsed = false;
        IsLoadMoreButtonVisible = true;

        _pageNumber = 1;
        Operations = await _operationFacade.GetOperationsAsync(PageSize, _pageNumber);

        if (Operations.Count < PageSize)
        {
            IsLoadMoreButtonVisible = false;
        }
    }

    public List<OperationFilterOption> FilterOptions
        => Enum.GetValues(typeof(OperationFilterOption)).Cast<OperationFilterOption>().ToList();

    [ObservableProperty]
    private ObservableCollection<OperationListModelBase> _operations = new();

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsNotLoading))]
    private bool _isLoading;

    public bool IsNotLoading => !IsLoading;

    [ObservableProperty]
    private bool _filterUsed;

    [ObservableProperty]
    private OperationFilterOption _selectedOperationFilterOption;

    [ObservableProperty] 
    private string _idNumberFilter = string.Empty;

    [ObservableProperty] 
    private bool _isLoadMoreButtonVisible = true;

    [RelayCommand]
    private async Task GoToTransactionDetailAsync(int id)
    {
        await Shell.Current.GoToAsync($"{nameof(TransactionDetailPage)}?id={id}");
    }

    [RelayCommand]
    private async Task GoToDonationDetailAsync(int id)
    {
        await Shell.Current.GoToAsync($"{nameof(DonationDetailPage)}?id={id}");
    }

    private int? _idFilterArgument;
    private OperationFilterOption _operationFilterOptionArgument;
    [RelayCommand]
    private async Task LoadMoreOperationsAsync()
    {
        if (IsLoading)
            return;

        IsLoading = true;

        _pageNumber++;

        try
        {
            ObservableCollection<OperationListModelBase> newOperations;

            if (FilterUsed)
            {
                newOperations = await _operationFacade.GetFilteredOperationsAsync(PageSize, _pageNumber, _operationFilterOptionArgument,
                    _idFilterArgument, null, null);
            }
            else
            {
                newOperations = await _operationFacade.GetOperationsAsync(PageSize, _pageNumber);
            }

            if (newOperations.Count < PageSize)
            {
                IsLoadMoreButtonVisible = false;
            }

            if (newOperations.Count == 0)
            {
                _pageNumber--;
            }

            foreach (var operation in newOperations)
            {
                Operations.Add(operation);
            }
        }
        catch
        {
            _pageNumber--;
        }
        finally
        {
            IsLoading = false;
        }
    }

    [RelayCommand]
    private async Task FilterAsync()
    {
        FilterUsed = true;
        IsLoadMoreButtonVisible = true;

        if (int.TryParse(IdNumberFilter, out var idFilterResult))
        {
            _idFilterArgument = idFilterResult;
        }
        else
        {
            _idFilterArgument = null;
        }

        _pageNumber = 1;
        _operationFilterOptionArgument = SelectedOperationFilterOption;
        Operations = await _operationFacade.GetFilteredOperationsAsync(PageSize, _pageNumber, _operationFilterOptionArgument, _idFilterArgument, null, null);

        if (Operations.Count < PageSize)
        {
            IsLoadMoreButtonVisible = false;
        }
    }

    [RelayCommand]
    private async Task ClearFilterAsync()
    {
        FilterUsed = false;
        IsLoadMoreButtonVisible = true;

        SelectedOperationFilterOption = OperationFilterOption.AllOperations;
        IdNumberFilter = string.Empty;

        _pageNumber = 1;
        Operations = await _operationFacade.GetOperationsAsync(PageSize, _pageNumber);

        if (Operations.Count < PageSize)
        {
            IsLoadMoreButtonVisible = false;
        }
    }
}