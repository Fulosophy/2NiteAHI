using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.Threading;
using Xamarin.Forms.Maps;
using GoogleApi;
using GoogleApi.Entities.Places;

namespace _2NiteAHI
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            DisplayCurLoc();

        }
        public async void DisplayCurLoc()
        {
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Medium);
                var location = await Geolocation.GetLocationAsync(request);


                if (location != null)
                {
                    Position p = new Position(location.Latitude, location.Longitude);
                    MapSpan mapspan = MapSpan.FromCenterAndRadius(p, Distance.FromMeters(.444));
                    map.MoveToRegion(mapspan);
                    Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                }
            }
            catch (FeatureNotSupportedException)
            {
                // Handle not supported on device exception
            }
            catch (FeatureNotEnabledException)
            {
                // Handle not enabled on device exception
            }
            catch (PermissionException)
            {
                // Handle permission exception
            }
            catch (Exception)
            {
                // Unable to get location
            }

        }

        async private void ToolbarItem_Clicked(object sender, EventArgs e)
        {

            await Navigation.PushAsync(new Settings());
            
            
        }
    }
}
