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
        private Dictionary<string, int> theBars;
        public LocationList()
        {
            Dictionary<string, int> theBars = new Dictionary<string, int>();
            
            theBars.Add("Pengilly's", 100);
            theBars.Add("Cactus", 95);
            theBars.Add("Handlebar", 89);
            theBars.Add("Spacebar", 76);
            theBars.Add("Neurolux", 70);
            theBars.Add("Broadway", 67);
            theBars.Add("Press & Pony", 38);
            theBars.Add("TapHouse", 21);
            theBars.Add("Mulligans", 19);
            theBars.Add("Mark's Lounge", 13);
            theBars.Add("Stockyard", 8);
            theBars.Add("ThatOtherPlaceThatNoOneGoesTo", 1);
            
            InitializeComponent();
            BindingContext = this;
            GetUserLoc(); // Grabbing the users Postal Code and Town Name
            
            barListView.ItemsSource = theBars;            
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

        //Asc & Desc Button Functions
        /*private void ListAsc_Clicked(object sender, EventArgs e)
        {
            var result = theBars.OrderByDescending(i => i.Key);
            barListView.ItemsSource = result;
        }
        private void ListDesc_Clicked(object sender, EventArgs e)
        {
            var result = theBars.OrderByAscending(i => i.Key);
            barListView.ItemsSource = result;
        }
        */
    }

}
