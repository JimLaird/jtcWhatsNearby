using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jtcWhatsNearby.ViewModel
{
    [QueryProperty("Result", "Result")]
    public partial class DetailsViewModel : BaseViewModel
    {
        IMap map;
        IPhoneDialer phoneDialer;
        IBrowser browser;

        public DetailsViewModel(IMap map, IPhoneDialer phoneDialer, IBrowser browser)
        {
            this.map = map;
            this.phoneDialer = phoneDialer;
            this.browser = browser;
        }

        [ObservableProperty]
        Result result;

        [ICommand]
        async Task OpenMapAsync()
        {
            try
            {
                await map.OpenAsync(Result.Location.Lat, Result.Location.Lng,
                    new MapLaunchOptions
                    {
                        Name = Result.Name,
                        NavigationMode = NavigationMode.None,
                        
                    });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await Shell.Current.DisplayAlert("Error !", $"Unable to open map: {ex.Message}", "OK");
            }
        }

        [ICommand]
        async Task CallAsync()
        {
            if (Result.PhoneNumber is null || Result.PhoneNumber.Length < 8)
                return;

            try
            {
                if (phoneDialer.IsSupported)
                    await Task.Run(async() => phoneDialer.Open(Result.PhoneNumber));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await Shell.Current.DisplayAlert("Error !", $"Unable to open phone dialler: {ex.Message}", "OK");
            }
        }

        [ICommand]
        async Task OpenBrowserAsync()
        {
            if (Result.Website is null || !Result.Website.IsAbsoluteUri)
                return;

            try
            {
                await browser.OpenAsync(Result.Website,
                    new BrowserLaunchOptions
                    {
                        LaunchMode = BrowserLaunchMode.SystemPreferred
                        
                    });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await Shell.Current.DisplayAlert("Error !", $"Unable to open browser: {ex.Message}", "OK");
            }
        }
    }
}
