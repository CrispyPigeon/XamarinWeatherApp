using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using DAL.WeatherApi;
using DBL.Service;
using Model.DbItems;
using Model.FinallyItems;
using MvvmCross.Commands;
using WeatherMobileApp.Core.Exceptions;
using WeatherMobileApp.Core.Services.Converters;
using WeatherMobileApp.Core.Services.Weather;
using Xamarin.Essentials;

namespace WeatherMobileApp.Core.ViewModels.Home
{
    public class HomeViewModel : BaseViewModel
    {
        private WeatherItem _currentWeather;
        public WeatherItem CurrentWeather
        {
            get => _currentWeather;
            set => SetProperty(ref _currentWeather, value);
        }

        private List<WeatherItem> _weathersToday;
        public List<WeatherItem> WeathersToday
        {
            get => _weathersToday;
            set => SetProperty(ref _weathersToday, value);
        }

        private readonly IOpenWeatherApi _openWeatherApi;
        private readonly IWeatherViewService _weatherViewService;

        private readonly IDatabaseService<WeatherDbItem> _weatherDb;
        private readonly IDatabaseService<CountryDbItem> _countryDb;

        public IMvxAsyncCommand RefreshDataCommand { get; }
        public IMvxAsyncCommand ShowInfoCommand { get; }

        public HomeViewModel(IOpenWeatherApi openWeatherApi,
                             IWeatherViewService weatherViewService,
                             IDatabaseService<WeatherDbItem> weatherDb,
                             IDatabaseService<CountryDbItem> countryDb)
        {
            _openWeatherApi = openWeatherApi;
            _weatherViewService = weatherViewService;

            _weatherDb = weatherDb;
            _countryDb = countryDb;

            RefreshDataCommand = new MvxAsyncCommand(RefreshDataAsync);
            ShowInfoCommand = new MvxAsyncCommand(ShowInfoAsync);
        }

        public override async void ViewAppeared()
        {
            base.ViewAppeared();

            await RefreshDataAsync();
        }

        private async Task RefreshDataAsync()
        {
            var status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
            if (status != PermissionStatus.Granted)
            {
                ShowAlert(Localization.Localization.ErrorTitle, Localization.Localization.UknownErrorMessage);
                return;
            }

            await LoadingCommand(async (Delegate stopLoading) =>
            {
                try
                {
                    var weathersDb = await _weatherDb.GetAll();
                    var countriesDb = await _countryDb.GetAll();
                    var todayWeathers = _weatherViewService.Get24HWeathers(weathersDb, countriesDb);

                    if (todayWeathers == null || todayWeathers.Count == 0)
                    {
                        if (CheckConnection())
                        {
                            var request = new GeolocationRequest(GeolocationAccuracy.Medium);
                            var location = await Geolocation.GetLocationAsync(request);

                            if (location == null)
                                throw new Exception();

                            var result = await _openWeatherApi.GetWeathersAsync(Localization.Localization.Lang,
                                                                                location.Latitude,
                                                                                location.Longitude);

                            var country = ApiItemsToDbItemsConverters.WeatherResponseToCountryDbItem(result);
                            var weathers = ApiItemsToDbItemsConverters.WeatherResponseToWeatherDbItems(result, country.Id);

                            await SaveToDatabase(weathers, country);

                            todayWeathers = _weatherViewService.Get24HWeathers(weathers, new List<CountryDbItem> { country });
                        }
                    }

                    WeathersToday = todayWeathers;
                    CurrentWeather = _weatherViewService.GetCurrentWeather(todayWeathers);
                }
                catch (InternetConnectionException)
                {
                    stopLoading.DynamicInvoke();
                    ShowAlert(Localization.Localization.ErrorTitle, Localization.Localization.NoInternetConnectionMessage);
                }
                catch
                {
                    stopLoading.DynamicInvoke();
                    ShowAlert(Localization.Localization.ErrorTitle, Localization.Localization.UknownErrorMessage);
                }
            });
        }

        private async Task ShowInfoAsync()
        {
            ShowAlert(Localization.Localization.InfoTitle, Localization.Localization.InfoMessage);
        }

        private async Task SaveToDatabase(List<WeatherDbItem> weathers, CountryDbItem country)
        {
            await _countryDb.DeleteAll();
            await _weatherDb.DeleteAll();

            await _countryDb.Add(country);
            await _weatherDb.AddRange(weathers);
        }
    }
}
