using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace _2NiteAHI
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    
    //Fixing an accidental merge sucks...
    public partial class LocationList : ContentPage
    {
       

        public LocationList()
        {
            InitializeComponent();
            BindingContext = this;
            GetUserLoc(); // Grabbing the users Postal Code and Town Name


        }
        async private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Settings());        
        }
        async private void GetUserLoc()
        {
            // Grabbing User Location and Zip Code
            var request = new GeolocationRequest(GeolocationAccuracy.High); // Requesting a location pull
            var location = await Geolocation.GetLocationAsync(request);
            var GetAddy = await Geocoding.GetPlacemarksAsync(location.Latitude, location.Longitude); // Grabbing the users location details as a placemark.
            var addy = GetAddy?.FirstOrDefault(); 
            MyLocation = $"{addy.Locality},{addy.PostalCode}"; // Grabs the users current locations Address-Town and Address-PostalCode
        }
        private string myloc;
        public string MyLocation // Property to change the label text on start-up
        {
            get { return myloc; }
            set
            {          
                myloc = value; 
                OnPropertyChanged(nameof(MyLocation)); 
            }
        }
    }
}