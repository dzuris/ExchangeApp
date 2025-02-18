﻿using System.Collections.ObjectModel;
using System.Resources;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ExchangeApp.App.Resources.Texts;
using ExchangeApp.App.Services.Interfaces;
using ExchangeApp.BL.Facades;
using ExchangeApp.BL.Facades.Interfaces;
using ExchangeApp.BL.Models.TotalBalance;
using ExchangeApp.Common.Enums;
using Microsoft.Maui.Devices.Sensors;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;

namespace ExchangeApp.App.ViewModels.TotalBalance;

public partial class TotalBalanceViewModel : ViewModelBase
{
    private readonly ITotalBalanceFacade _totalBalanceFacade;
    private readonly IPrinterService _printerService;
    private readonly ISettingsFacade _settingsFacade;

    public TotalBalanceViewModel(ITotalBalanceFacade totalBalanceFacade, IPrinterService printerService, ISettingsFacade settingsFacade)
    {
        _totalBalanceFacade = totalBalanceFacade;
        _printerService = printerService;
        _settingsFacade = settingsFacade;
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        TotalBalanceList = await _totalBalanceFacade.GetAllAsync();
    }

    public List<TotalBalanceFilterOption> FilterOptions 
        => Enum.GetValues(typeof(TotalBalanceFilterOption)).Cast<TotalBalanceFilterOption>().ToList();

    [ObservableProperty] 
    private TotalBalanceFilterOption _selectedTotalBalanceFilterOption;

    [ObservableProperty]
    private ObservableCollection<TotalBalanceModel> _totalBalanceList = new();

    [ObservableProperty]
    private DateTime? _filterDateFrom;

    [ObservableProperty]
    private DateTime? _filterDateUntil;

    [RelayCommand]
    private async Task FilterAsync()
    {
        var untilDate = FilterDateUntil;
        if (FilterDateUntil.HasValue)
        {
            untilDate = FilterDateUntil.Value.AddDays(1);
        }

        TotalBalanceList = await _totalBalanceFacade.GetFilteredAsync(SelectedTotalBalanceFilterOption, FilterDateFrom, untilDate);
    }

    [RelayCommand]
    private async Task RefreshAsync()
    {
        SelectedTotalBalanceFilterOption = TotalBalanceFilterOption.All;
        FilterDateFrom = null;
        FilterDateUntil = null;
        TotalBalanceList = await _totalBalanceFacade.GetAllAsync();
    }

    [RelayCommand]
    private async Task CreateDailyTotalBalanceAsync()
    {
        var model = new TotalBalanceModel
        {
            Created = DateTime.Now,
            Type = TotalBalanceType.Daily
        };

        var rm = new ResourceManager(typeof(TotalBalancePageResources));

        var result = await Application.Current?.MainPage?.DisplayAlert(
            rm.GetString("AlertConfirmationTitle"),
            string.Format(rm.GetString("AlertConfirmationDailyMessage")!, model.Created),
            rm.GetString("AlertButtonYes"),
            rm.GetString("AlertButtonNo"))!;

        if (!result) return;

        try
        {
            var id = await _totalBalanceFacade.InsertAsync(model);
            model.Id = id;

            try
            {
                if (await _settingsFacade.ShouldSaveTotalBalanceAutomaticallyAsync())
                {
                    await _printerService.SavePdf(model);
                }
            }
            catch (ArgumentNullException)
            {
                // TODO Select folder or error message
            }
        }
        catch
        {
            await Application.Current.MainPage?.DisplayAlert(
                rm.GetString("AlertTitleErrorCreation"),
                rm.GetString("AlertMessageErrorAlreadyExists"),
                rm.GetString("AlertButtonOk"))!;
            return;
        }

        TotalBalanceList.Add(model);

        await Application.Current.MainPage?.DisplayAlert(
            rm.GetString("AlertTitleCreationDone"),
            rm.GetString("AlertMessageCreationDone"),
            rm.GetString("AlertButtonOk"))!;
    }

    [RelayCommand]
    private async Task CreateMonthlyTotalBalanceAsync()
    {
        var rm = new ResourceManager(typeof(TotalBalancePageResources));

        if (!await _totalBalanceFacade.CanCreateMonthlyTotalBalance())
        {
            await Application.Current?.MainPage?.DisplayAlert(
                rm.GetString("AlertTitleErrorCreation"),
                rm.GetString("AlertMessageErrorCanNotCreateMonthlyBalance"),
                rm.GetString("AlertButtonOk"))!;
            return;
        }

        var model = new TotalBalanceModel
        {
            Created = DateTime.Now,
            Type = TotalBalanceType.Monthly
        };

        var result = await Application.Current?.MainPage?.DisplayAlert(
            rm.GetString("AlertConfirmationTitle"),
            string.Format(rm.GetString("AlertConfirmationMonthlyMessage")!, model.Created.ToString("MMMM yyyy")),
            rm.GetString("AlertButtonYes"),
            rm.GetString("AlertButtonNo"))!;

        if (!result) return;

        try
        {
            var id = await _totalBalanceFacade.InsertAsync(model);
            model.Id = id;

            try
            {
                if (await _settingsFacade.ShouldSaveTotalBalanceAutomaticallyAsync())
                {
                    await _printerService.SavePdf(model);
                }
            }
            catch (ArgumentNullException)
            {
                // TODO Select folder or show error message
            }
        }
        catch
        {
            await Application.Current.MainPage?.DisplayAlert(
                rm.GetString("AlertTitleErrorCreation"),
                rm.GetString("AlertMessageErrorAlreadyExists"),
                rm.GetString("AlertButtonOk"))!;
            return;
        }

        TotalBalanceList.Add(model);

        await Application.Current.MainPage?.DisplayAlert(
            rm.GetString("AlertTitleCreationDone"),
            rm.GetString("AlertMessageCreationDone"),
            rm.GetString("AlertButtonOk"))!;
    }

    [RelayCommand]
    private async Task DownloadTotalBalanceAsync(TotalBalanceModel model)
    {
        try
        {
            await _printerService.SavePdf(model);
        }
        catch (ArgumentNullException)
        {
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        var rm = new ResourceManager(typeof(TotalBalancePageResources));

        await Application.Current?.MainPage?.DisplayAlert(
            rm.GetString("AlertTitleDownloaded"),
            rm.GetString("AlertMessageDownloaded"),
            rm.GetString("AlertButtonOk"))!;
    }

    [RelayCommand]
    private async Task PrintTotalBalanceAsync(TotalBalanceModel model)
    {
        await _printerService.Print(model);
    }
}