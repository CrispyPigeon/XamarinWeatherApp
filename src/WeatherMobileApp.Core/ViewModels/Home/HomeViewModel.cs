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
        private readonly IUserDialogs _dialogs;

        private readonly IDatabaseService<WeatherDbItem> _weatherDb;
        private readonly IDatabaseService<CountryDbItem> _countryDb;

        public IMvxAsyncCommand RefreshDataCommand { get; }

        public HomeViewModel(IOpenWeatherApi openWeatherApi,
                             IWeatherViewService weatherViewService,
                             IUserDialogs dialogs,
                             IDatabaseService<WeatherDbItem> weatherDb,
                             IDatabaseService<CountryDbItem> countryDb) : base(dialogs)
        {
            _openWeatherApi = openWeatherApi;
            _weatherViewService = weatherViewService;
            _dialogs = dialogs;

            _weatherDb = weatherDb;
            _countryDb = countryDb;

            RefreshDataCommand = new MvxAsyncCommand(RefreshData);
        }

        public override async void ViewAppeared()
        {
            base.ViewAppeared();

            await LoadingCommand(async (Delegate stopLoading) =>
            {
                try
                {
                    var weathersDb = await _weatherDb.GetAll();
                    var countriesDb = await _countryDb.GetAll();
                    var todayWeathers = _weatherViewService.GetTodayWeathers(weathersDb, countriesDb);

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

                            SaveToDatabase(weathers, country);

                            todayWeathers = _weatherViewService.GetTodayWeathers(weathers, country);
                        }
                    }

                    WeathersToday = todayWeathers;
                    CurrentWeather = _weatherViewService.GetCurrentWeather(todayWeathers);
                }
                catch (InternetConnectionException)
                {
                    stopLoading.DynamicInvoke();
                    _dialogs.Alert(Localization.Localization.NoInternetConnectionMessage,
                                   Localization.Localization.ErrorTitle);
                }
                catch (PermissionException)
                {
                    stopLoading.DynamicInvoke();
                    _dialogs.Alert(Localization.Localization.NoLocationPermissionMessage,
                                   Localization.Localization.ErrorTitle);
                }
                catch
                {
                    stopLoading.DynamicInvoke();
                    _dialogs.Alert(Localization.Localization.UknownErrorMessage,
                                   Localization.Localization.ErrorTitle);
                }
            });
        }

        private void SaveToDatabase(List<WeatherDbItem> weathers, CountryDbItem country)
        {
            _countryDb.DeleteAll();
            _weatherDb.DeleteAll();

            _countryDb.AddOrUpdate(country);
            _weatherDb.AddOrUpdateAll(weathers);
        }

        private async Task RefreshData()
        {
            
        }
    }
}
