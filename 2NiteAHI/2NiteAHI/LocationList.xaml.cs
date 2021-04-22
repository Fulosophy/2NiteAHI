using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using GoogleApi;
using System.Net;
using System.IO;

namespace _2NiteAHI
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    
    //Fixing an accidental merge sucks...
    public partial class LocationList : ContentPage
    {
        //variables
        private Dictionary<string, int> theBars = new Dictionary<string, int>();
        private string myloc;
        private int searchRadius = 1000;
        private string searchType = "bar";
        private object item;
        private bool hasbeen;

        public LocationList()
        {
            //Dictionary<string, int> theBars = new Dictionary<string, int>();
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
            //ListView ItemSelected options
            barListView.ItemSelected += (object sender, SelectedItemChangedEventArgs e) =>
            {
                item = e.SelectedItem;
                if (hasbeen == false)
                {
                    DisplayAlert("Yo! Bar Selected!", e.SelectedItem.ToString(), "OK", "Cancel");
                    hasbeen = true;
                }
                else if(hasbeen == true)
                {
                    DisplayAlert("Yo! New Bar?", e.SelectedItem.ToString(), "OK", "Cancel");
                }
            };
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

            //Web Request URL using variables for location(location.Latitude, location.Longitude), radius(searchRadius), and type(searchType)
            //Adding this code incase we can get it working without charging anything...
            string restUrl = $"https://maps.googleapis.com/maps/api/place/nearbysearch/json?location=" +
                location.Latitude + "," + location.Longitude +
                "&radius=" + searchRadius + "&type=" +
                searchType + "&key=AIzaSyAR55DfsuhAWriupO5t_uzWl-FwddZiBhY";

            //Web  Request
            HttpWebRequest webRequest = WebRequest.Create(restUrl) as HttpWebRequest;
            webRequest.Timeout = 2000;
            webRequest.Method = "GET";
            webRequest.BeginGetResponse(new AsyncCallback(RequestCompleted), webRequest);

            MyLocation = $"{addy.Locality},{addy.AdminArea}"; // Grabs the users current locations Address-Town and Address-State **Only works in USA**  
                    
        }
        private void RequestCompleted(IAsyncResult result)
        {
            var request = (HttpWebRequest)result.AsyncState;
            var response = (HttpWebResponse)request.EndGetResponse(result);

            using(var stream = response.GetResponseStream())
            {
                Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
                var r = new StreamReader(stream, encode);
                Char[] read = new char[256];
                int count = r.Read(read, 0, 256);

                while(count > 0)
                {
                    String str = new string(read, 0, count);
                    Console.Write(str);
                    count = r.Read(read, 0, 256);
                }
                Console.WriteLine("");
                r.Close();
                
                //Old readToEnd might be an alternative to the Char array...
                //var resp = r.ReadToEnd();
            }
        }
        public string MyLocation // Property to change the label text on start-up
        {
            get { return myloc; }
            set
            {          
                myloc = value; 
                OnPropertyChanged(nameof(MyLocation)); 
            }

        }
        //Ascending sort button
        //Gets a null error right now
        private void OnClick_Ascend(object sender, EventArgs e)
        {
            theBars = theBars.OrderBy(i => i.Value).ToDictionary(i => i.Key, i => i.Value);
            barListView.ItemsSource = theBars;
        }
        //Descending sort button
        //Gets a null error
        private void OnClick_Descend(object sender, EventArgs e)
        {
            theBars = theBars.OrderByDescending(i => i.Value).ToDictionary(i => i.Key, i => i.Value);
            barListView.ItemsSource = theBars;
        }

        private void OnClick_Peace(object sender, EventArgs e)
        {

        }
    }

}
