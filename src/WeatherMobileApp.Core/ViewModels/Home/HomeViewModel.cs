using System;
using System.Collections.Generic;
using System.Text;
using DAL.WeatherApi;
using Xamarin.Essentials;

namespace WeatherMobileApp.Core.ViewModels.Home
{
    public class HomeViewModel : BaseViewModel
    {
        private readonly IOpenWeatherApi _openWeatherApi;

        public HomeViewModel(IOpenWeatherApi openWeatherApi)
        {
            _openWeatherApi = openWeatherApi;
        }

        public override async void ViewAppeared()
        {
            base.ViewAppeared();
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Medium);
                var location = await Geolocation.GetLocationAsync(request);

                if (location != null)
                {
                   var res = await _openWeatherApi.GetWeathersAsync(Localization.Localization.Lang, location.Latitude, location.Longitude);
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
            }
            catch (Exception ex)
            {
                // Unable to get location
            }
        }
    }
}
