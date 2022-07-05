using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Location = Microsoft.Maui.Devices.Sensors.Location;

namespace jtcWhatsNearby.ViewModel
{
    public partial class SearchPageViewModel : BaseViewModel
    {
        PlaceService placeService;
   
        IConnectivity connectivity;
        IGeolocation geolocation;

        [ObservableProperty]
        string latitude;
        [ObservableProperty]
        string longitude;
        [ObservableProperty]
        string radius;
        [ObservableProperty]
        CancellationTokenSource cancelTokenSource;
        [ObservableProperty]
        bool isCheckingLocation;
        [ObservableProperty]
        string type;
        [ObservableProperty]
        List<Place> places;
        [ObservableProperty]
        List<Result> dataList;

        public SearchPageViewModel(PlaceService placeService, IConnectivity connectivity, IGeolocation geolocation)
        {
            Title = "What's Nearby";
            this.placeService = placeService;
            this.connectivity = connectivity;
            this.geolocation = geolocation;
        }


        [ICommand]
        async Task GetPlacesAsync()
        {
            if (IsBusy)
                return;

            if (connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await Shell.Current.DisplayAlert("No Internet", $"Check your internet connection and try again!", "OK");
                return;
            }

            if (type is null || type.Length < 3)
            {
                await Shell.Current.DisplayAlert("Enter Search Type", $"Search Type Incomplete \n\n" + $"Please enter a business type to search for and try a again!", "OK");
                return;
            }

            Type = Type.ToLower();

            if (radius is null || radius.Length < 3)
                Radius = "3000";

            try
            {
                
                IsBusy = true;
                IsCheckingLocation = true;

                GeolocationRequest request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(15));
                cancelTokenSource = new CancellationTokenSource();

                Location location = await Geolocation.Default.GetLocationAsync(request, cancelTokenSource.Token);

                if (location != null)
                {
                    latitude = location.Latitude.ToString();
                    longitude = location.Longitude.ToString();

                    
                }

                var placeData = await placeService.GetPlaces(GenerateRequestUrl(Constants.ApiUrl));

                

                DataList = new List<Result>();
                

                foreach (var result in placeData.Results)
                    DataList.Add(result);
                
                
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await Shell.Current.DisplayAlert("Error !", $"Unable to get places: Please try another search type", "OK");
            }
            finally
            {
                IsBusy = false;
                IsRefreshing = false;
                IsCheckingLocation = false;
            }
        }

        public void CancelRequest()
        {
            if (isCheckingLocation && cancelTokenSource != null && cancelTokenSource.Token.IsCancellationRequested == false)
                cancelTokenSource.Cancel();
        }

        [ICommand]
        async Task GoToDetailsAsync(Result result)
        {
            if (result is null)
                return;

           
            await Shell.Current.GoToAsync($"{nameof(DetailsPage)}", true, new Dictionary<string,object>{{"Result", result}});
        }

        string GenerateRequestUrl(string endpoint)
        {
            string requestUri = endpoint;
            requestUri += $"/FindPlacesNearby";
            requestUri += $"?location=";
            requestUri += $"{latitude},{longitude}";
            requestUri += $"&type={type}";
            requestUri += $"&radius={radius}";
            requestUri += $"&language=en";

            return requestUri;
        }
    }
}
