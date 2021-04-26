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
using System.Collections.ObjectModel;
using _2NiteAHI;
using System.Windows.Input;
using System.Threading;
namespace _2NiteAHI
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LocationList : ContentPage
    {
        //variables
        public Dictionary<String, int> winterParkBars = new Dictionary<string, int>();
        public Dictionary<string, double> winParkBarsProx = new Dictionary<string, double>();
        public Dictionary<String, int> boiseBars = new Dictionary<string, int>();
        public Dictionary<string, double> boiseBarsProx = new Dictionary<string, double>();
        public Dictionary<String, int> winterParkFoods = new Dictionary<string, int>();
        public Dictionary<String, int> boiseFoods = new Dictionary<string, int>();
        Random rng = new Random();
        public List<string> ListOLocations = new List<string>
        {
            "Bars",
            "Restaurant"
        };

        private string myloc;
        private int locale;
        private string selecteditem;
        private int usercount;
        private string previousBar;
        private int searchRadius = 1000;
        private string searchType = "bar";
        private object item;
        private bool hasbeen;
        private bool _isSoRefreshing = false;
        private string temp;

        //CENTERING COORDINATES FOR RELATIVITY
        //private double winterParkGPSLat = 28.596580917445976;
        //private double winterParkGPSLong = -81.30134241645047;
        //private double boiseGPSLat = 43.61322012630053;
        //private double boiseGPSLong = -116.20277481510922;

        public LocationList()
        {
            InitializeComponent();
            BindingContext = this;
            GetUserLoc(); // Grabbing the users Postal Code and Town Name

            //BAR/FOOD PICKER
            barOrFood.ItemsSource = ListOLocations;
            barOrFood.SelectedItem = "Bars";

            //LISTVIEW SELECTION OPERATIONS


            barListView.ItemSelected += async (object sender, SelectedItemChangedEventArgs e) =>
             {
                 
                 if (hasbeen == false)
                 {
                     var ans = await DisplayAlert("Yo New Bar?", "Would You Like To Check In?", "Yes", "Cancel");
                     if (ans == true) // pressing OK
                     {                        
                         selecteditem = e.SelectedItem.ToString(); // Changing event handler to string
                         selecteditem = selecteditem.Trim('[', ']'); // triming brackets
                         string[] splititems = selecteditem.Split(','); // delim comma
                         usercount = winterParkBars[splititems[0]]; // Grabbing current user count of bar/rest
                         winterParkBars[splititems[0]] = usercount + 1; // increment from current bar/rest count
                         temp = splititems[0]; // Saving the selected data for decrements                       
                         hasbeen = true;
                     }
                     else
                     {
                         return;
                     }
                 }
                 else if (hasbeen == true)
                 {
                    var anss = await DisplayAlert("Changing bars???", "Would You Like To Check In?", "Yes", "Cancel");
                     if (anss == true) // pressing OK
                     {
                         usercount = winterParkBars[temp];
                         winterParkBars[temp] = usercount - 1; // decrementing old bar
                         selecteditem = e.SelectedItem.ToString(); // Changing event handler to string
                         selecteditem = selecteditem.Trim('[', ']'); // triming brackets
                         string[] splititems = selecteditem.Split(','); // delim comma

                         usercount = winterParkBars[splititems[0]]; // Grabbing current user count of bar/rest
                         winterParkBars[splititems[0]] = usercount + 1; // Incrementing CHANGED bar
                         temp = splititems[0];
                     }
                     else
                     {
                         return;
                     }
                 }

             };

            Console.WriteLine("Test");


            
        }

        private void listSelected()
        {
            throw new NotImplementedException();
        }

        async private void ToolbarItem_Clicked(object sender, EventArgs e) { await Navigation.PushAsync(new Settings()); }
        async private void GetUserLoc()
        {
            var location = await Geolocation.GetLocationAsync(new GeolocationRequest(GeolocationAccuracy.Default, TimeSpan.FromMinutes(1)));
            var GetAddy = await Geocoding.GetPlacemarksAsync(location.Latitude, location.Longitude); // Grabbing the users location details as a placemark.
            var addy = GetAddy?.FirstOrDefault();

         

            MyLocation = $"{addy.Locality},{addy.AdminArea}"; // Grabs the users current locations Address-Town and Address-State **Only works in USA**  
            if (addy.Locality == "Winter Park")
            {
                locale = 0;
                BuildWinterParkBars();
                winterParkBars = winterParkBars.OrderByDescending(i => i.Value).ToDictionary(i => i.Key, i => i.Value);
                
                barListView.ItemsSource = winterParkBars;
            }
            else if (addy.Locality == "Boise")
            {
                locale = 1;
                BuildBoiseBars();
                boiseBars = boiseBars.OrderByDescending(i => i.Value).ToDictionary(i => i.Key, i => i.Value);
                
                barListView.ItemsSource = boiseBars;
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

        //LISTVIEW REFRESH PROPERTIES
        public bool IsSoRefreshing
        {
            get { return _isSoRefreshing; }
            set
            {
                _isSoRefreshing = value;
                OnPropertyChanged(nameof(IsSoRefreshing));
            }
        }
        public ICommand RefreshCommand // Refreshing List View
        {
            
            get
            {
                return new Command(() =>
                {
                 
                    if (locale == 0)
                    {
                        barListView.ItemsSource = null;
                        barListView.ItemsSource = winterParkBars;
                        IsSoRefreshing = false;
                    
                        winParkBarsProx.Clear();
                    }
                    else if (locale == 1)
                    {
                        barListView.ItemsSource = null;
                        barListView.ItemsSource = boiseBars;
                        IsSoRefreshing = false;
                        boiseBarsProx.Clear();
                    }
                });
            }
        }


        //DICTIONARIES
        private void BuildWinterParkBars()
        {
            
            winterParkBars.Add("Chili's Bar & Grill", usercount); 
            winterParkBars.Add("Firehouse Subs", usercount);
            winterParkBars.Add("Arooga's", usercount); 
            winterParkBars.Add("Chewy Boba", usercount);
            winterParkBars.Add("The Geek Easy", usercount);
            winterParkBars.Add("Steak 'n Shake", usercount);
            winterParkBars.Add("The Haven At Forsyth", usercount);
            winterParkBars.Add("El Pueblo Mexicqan", usercount);
            winterParkBars.Add("Debbie's Bar", usercount); 
            winterParkBars.Add("Ain't Misbehavin'", usercount);
            winterParkBars.Add("Miller's Ale House", usercount); 
            winterParkBars.Add("Devaney's Sports Pub", usercount);
            winterParkBars.Add("The Next", usercount); 
            winterParkBars.Add("Admiral Cigar Club", usercount);
            winterParkBars.Add("Gator's Dockside", usercount); 
            winterParkBars.Add("Cork & Plate", usercount);
            winterParkBars.Add("Tactical Brewing Co.", usercount); 
            winterParkBars.Add("The Brass Tap", usercount);
            winterParkBars.Add("Sonny's BBQ", usercount); 
            winterParkBars.Add("La Placita 19'", usercount);
            winterParkBars.Add("Rincon Latino", usercount);
            winterParkBars.Add("Starbucks", usercount);
            winterParkBars.Add("Tequila Lounge Club", usercount); 
            winterParkBars.Add("Muldoons Saloon", usercount);
            winterParkBars.Add("Texas Roadhous", usercount); 
            winterParkBars.Add("Majijis Hookah Lounge", usercount);
            winterParkBars.Add("Thirsty Gator", usercount); 
            winterParkBars.Add("Don Julio Mexican Kitchen and Tequila Bar", usercount);
            winterParkBars.Add("Fire on the Bayou", usercount); 
            winterParkBars.Add("Rock and Brews", usercount);
            winterParkBars.Add("BJ's Restaurant and Brewhouse", usercount); 
            winterParkBars.Add("Oviedo Brewing Company", usercount);
            winterParkBars.Add("Buffalo Wild WIngs", usercount); 
            winterParkBars.Add("The Green Bar", usercount);
            winterParkBars.Add("The Green Parrot", usercount); 
            winterParkBars.Add("Luke's Kitchen and Bar", usercount);
        }
        private void BuildWinParkBarsProx()
        {
            winParkBarsProx.Add("Chili's Bar & Grill", 0.5835);
            winParkBarsProx.Add("Firehouse Subs", 0.5540);
            winParkBarsProx.Add("Arooga's", 0.5303); 
            winParkBarsProx.Add("Chewy Boba", 0.6737);
            winParkBarsProx.Add("The Geek Easy", 0.6719); 
            winParkBarsProx.Add("Steak 'n Shake", 0.8633);
            winParkBarsProx.Add("The Haven At Forsyth", 0.9050); 
            winParkBarsProx.Add("El Pueblo Mexicqan", 1.4230);
            winParkBarsProx.Add("Debbie's Bar", 3.7190); 
            winParkBarsProx.Add("Ain't Misbehavin'", 3.2320);
            winParkBarsProx.Add("Miller's Ale House", 0.2871); 
            winParkBarsProx.Add("Devaney's Sports Pub", 1.6930);
            winParkBarsProx.Add("The Nest", 3.9230); 
            winParkBarsProx.Add("Admiral Cigar Club", 3.9710);
            winParkBarsProx.Add("Gator's Dockside", 3.9420); 
            winParkBarsProx.Add("Cork & Plate", 4.0180);
            winParkBarsProx.Add("Tactical Brewing Co.", 4.0560); 
            winParkBarsProx.Add("The Brass Tap", 7.0210);
            winParkBarsProx.Add("Redlight Redlight", 5.5550);
            winParkBarsProx.Add("Sonny's BBQ", 0.1739); 
            winParkBarsProx.Add("La Placita 19'", 0.3135);
            winParkBarsProx.Add("Rincon Latino", 0.4204); 
            winParkBarsProx.Add("Starbucks", 1.2130);
            winParkBarsProx.Add("Tequila Lounge Club", 1.4720); 
            winParkBarsProx.Add("Muldoons Saloon", 2.0060);
            winParkBarsProx.Add("Texas Roadhous", 4.8070); 
            winParkBarsProx.Add("Majijis Hookah Lounge", 1.5520);
            winParkBarsProx.Add("Thirsty Gator", 1.9780); 
            winParkBarsProx.Add("Don Julio Mexican Kitchen and Tequila Bar", 7.1960);
            winParkBarsProx.Add("Fire on the Bayou", 6.2520); 
            winParkBarsProx.Add("Rock and Brews", 7.7530);
            winParkBarsProx.Add("BJ's Restaurant and Brewhouse", 9.1900); 
            winParkBarsProx.Add("Oviedo Brewing Company", 9.9090);
            winParkBarsProx.Add("Buffalo Wild WIngs", 6.2890); 
            winParkBarsProx.Add("The Green Bar", 7.1810);
            winParkBarsProx.Add("The Green Parrot", 7.6660150); 
            winParkBarsProx.Add("Luke's Kitchen and Bar", 6.7470);
        }
        private void BuildBoiseBars()
        {
            boiseBars.Add("Pengilly's Saloon", rng.Next() % 150); boiseBars.Add("Whiskey Bar", rng.Next() % 150);
            boiseBars.Add("Press & Pony", rng.Next() % 150); boiseBars.Add("The Handlebar", rng.Next() % 150);
            boiseBars.Add("The Atlas Bar", rng.Next() % 150); boiseBars.Add("The Spacebar", rng.Next() % 150);
            boiseBars.Add("Neurolox", rng.Next() % 150); boiseBars.Add("Taphouse", rng.Next() % 150);
            boiseBars.Add("Water Bear Bar", rng.Next() % 150); boiseBars.Add("Cactus Bar", rng.Next() % 150);
            boiseBars.Add("The Mode Lounge", rng.Next() % 150); boiseBars.Add("Mulligans'", rng.Next() % 150);
            boiseBars.Add("Bardenay Restaurant & Distillery", rng.Next() % 150); boiseBars.Add("Tom Grainey's", rng.Next() % 150);
            boiseBars.Add("Humpin' Hannah's", rng.Next() % 150); boiseBars.Add("Boise Brewing", rng.Next() % 150);
            boiseBars.Add("Payette Brewing", rng.Next() % 150); boiseBars.Add("10 Barrel Brewing", rng.Next() % 150);
            boiseBars.Add("White Dog Brewing", rng.Next() % 150); boiseBars.Add("Barbarian Brewing", rng.Next() % 150);
            boiseBars.Add("Clairvoyant Brewing", rng.Next() % 150); boiseBars.Add("Lost Grove Brewing", rng.Next() % 150);
            boiseBars.Add("Cloud 9 Brewing", rng.Next() % 150); boiseBars.Add("Woodland Brewing ", rng.Next() % 150);
            boiseBars.Add("Edge Brewing", rng.Next() % 150); boiseBars.Add("Highlands Hollow Brewhouse", rng.Next() % 150);
            boiseBars.Add("Ram Restaurant & Brewery", rng.Next() % 150); boiseBars.Add("Bittercreek Alehouse", rng.Next() % 150);
            boiseBars.Add("The Silly Birch", rng.Next() % 150); boiseBars.Add("The Gas Lantern Drinking Company", rng.Next() % 150);
            boiseBars.Add("Double Tap Pub", rng.Next() % 150); boiseBars.Add("Bar Gernika", rng.Next() % 150);
            boiseBars.Add("Dirty Little Roddy's", rng.Next() % 150); boiseBars.Add("Amsterdam Lounge", rng.Next() % 150);
        }
        private void BuildBoiseBarsProx()
        {
            boiseBarsProx.Add("Pengilly's Saloon", 0.2701); 
            boiseBarsProx.Add("Whiskey Bar", 0.1709);
            boiseBarsProx.Add("Press & Pony", 0.7412); 
            boiseBarsProx.Add("The Handlebar", 1.3760);
            boiseBarsProx.Add("The Atlas Bar", 0.8660); 
            boiseBarsProx.Add("The Spacebar", 0.9419);
            boiseBarsProx.Add("Neurolox", 1.2330); 
            boiseBarsProx.Add("Taphouse", 0.4026);
            boiseBarsProx.Add("Water Bear Bar", 0.5936); 
            boiseBarsProx.Add("Cactus Bar", 0.2324);
            boiseBarsProx.Add("The Mode Lounge", 0.4005); 
            boiseBarsProx.Add("Mulligans'", 0.5573);
            boiseBarsProx.Add("Bardenay Restaurant & Distillery", 0.1650); 
            boiseBarsProx.Add("Tom Grainey's", 0.2019);
            boiseBarsProx.Add("Humpin' Hannah's", 0.2069); 
            boiseBarsProx.Add("Boise Brewing", 0.1072);
            boiseBarsProx.Add("Payette Brewing", 1.0020); 
            boiseBarsProx.Add("10 Barrel Brewing", 0.5744);
            boiseBarsProx.Add("White Dog Brewing", 0.3350); 
            boiseBarsProx.Add("Barbarian Brewing", 0.5699);
            boiseBarsProx.Add("Clairvoyant Brewing", 0.5699); 
            boiseBarsProx.Add("Lost Grove Brewing", 2.1360);
            boiseBarsProx.Add("Cloud 9 Brewing", 1.5220); 
            boiseBarsProx.Add("Woodland Brewing ", 0.6303);
            boiseBarsProx.Add("Edge Brewing", 0.5869); 
            boiseBarsProx.Add("Highlands Hollow Brewhouse", 3.3130);
            boiseBarsProx.Add("Ram Restaurant & Brewery", 1.2830); 
            boiseBarsProx.Add("Bittercreek Alehouse", 0.3511);
            boiseBarsProx.Add("The Silly Birch", 0.1770); 
            boiseBarsProx.Add("The Gas Lantern Drinking Company", 0.3631);
            boiseBarsProx.Add("Double Tap Pub", 0.2720); 
            boiseBarsProx.Add("Bar Gernika", 0.1846);
            boiseBarsProx.Add("Dirty Little Roddy's", 0.1703); 
            boiseBarsProx.Add("Amsterdam Lounge", 0.1907);
        }
        private void BuildWinterParkFoods()
        {
            /*winterParkFoods.Add("", 0);*/
        }
        private void BuildBoiseFoods()
        {
            /*BoiseFoods.Add("", 0);*/
        }

        //ASCENDING BUTTON
        private void OnClick_Ascend(object sender, EventArgs e)
        {
            //OrderBy Value
            if (locale == 0)
            {
                winterParkBars = winterParkBars.OrderBy(i => i.Value).ToDictionary(i => i.Key, i => i.Value);
                barListView.ItemsSource = winterParkBars;
                winParkBarsProx.Clear();
            }
            else if (locale == 1)
            {
                boiseBars = boiseBars.OrderBy(i => i.Value).ToDictionary(i => i.Key, i => i.Value);
                barListView.ItemsSource = boiseBars;
                boiseBarsProx.Clear();
            }

        }
        //DESCENDING BUTTON
        private void OnClick_Descend(object sender, EventArgs e)
        {
            //OrderBy Value
            if (locale == 0)
            {
                winterParkBars = winterParkBars.OrderByDescending(i => i.Value).ToDictionary(i => i.Key, i => i.Value);
                barListView.ItemsSource = winterParkBars;
            }
            else if (locale == 1)
            {
                boiseBars = boiseBars.OrderByDescending(i => i.Value).ToDictionary(i => i.Key, i => i.Value);
                barListView.ItemsSource = boiseBars;
            }
        }
        //PROXIMITY BUTTON
        private void OnClick_Proximity(object sender, EventArgs e)
        {
            if (locale == 0)
            {
                BuildWinParkBarsProx();
                winParkBarsProx = winParkBarsProx.OrderBy(i => i.Value).ToDictionary(i => i.Key, i => i.Value);
                barListView.ItemsSource = winParkBarsProx;
            }
            else if (locale == 1)
            {
                BuildBoiseBarsProx();
                boiseBarsProx = boiseBarsProx.OrderBy(i => i.Value).ToDictionary(i => i.Key, i => i.Value);
                barListView.ItemsSource = boiseBarsProx;
            }
        }
        //PEACE-OUT BUTTON
        private void OnClick_Peace(object sender, EventArgs e)
        {
            usercount = winterParkBars[temp];
            winterParkBars[temp] = usercount - 1; // decrementing old bar
            DisplayAlert("Leaving So Soon?", "See ya next time!","OK");

            
            
            Thread.Sleep(3000);
            Environment.FailFast("");




        }
    }
}
