using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using GoogleApi;

namespace _2NiteAHI
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    
    //Fixing an accidental merge sucks...
    public partial class LocationList : ContentPage
    {

        public LocationList()
        {
            string x1 = "Pengilly's";
            string x2 = "Adrian's";
            string x3 = "Cactus";
            string x4 = "Handlebar";
            string x5 = "Spacebar";
            string x6 = "Neurolux";
            string x7 = "Broadway";
            string x8 = "Press & Pony";
            string x9 = "TapHouse";
            string x10 = "Mulligans";
            string x11 = "Mark's Lounge";
            string x12 = "Stockyard";
            string x13 = "ThatOtherPlaceThatNoOneGoesTo";

            InitializeComponent();
            BindingContext = this;
            GetUserLoc(); // Grabbing the users Postal Code and Town Name
            
            barListView.ItemsSource = new string[] { x1,x2,x3,x4,x5,x6,x7,x8,x9,x10,x11,x12,x13 };
        }
        
        async private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Settings());        
        }
        async private void GetUserLoc()
        {
            var location = await Geolocation.GetLocationAsync(new GeolocationRequest(GeolocationAccuracy.Default, TimeSpan.FromMinutes(1)));
            var GetAddy = await Geocoding.GetPlacemarksAsync(location.Latitude, location.Longitude); // Grabbing the users location details as a placemark.
            var addy = GetAddy?.FirstOrDefault();
            MyLocation = $"{addy.Locality},{addy.AdminArea}"; // Grabs the users current locations Address-Town and Address-State **Only works in USA**            
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
