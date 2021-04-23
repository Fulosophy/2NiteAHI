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
        public List<Bars> WinterBarList = new List<Bars>();
        public List<Bars> BoiseBarList = new List<Bars>();
        public List<Foods> WinterFoodsList = new List<Foods>();
        public List<Foods> BoiseFoodsList = new List<Foods>();
        public List<string> ListOLocations = new List<string>
        {
            "Bars",
            "Restaurant"
        };

        private string myloc;
        private int locale;
        private int count;
        private string previousBar;
        private int searchRadius = 1000;
        private string searchType = "bar";
        private object item;
        private bool hasbeen;
        private bool _isSoRefreshing = false;

        //CENTERING COORDINATES FOR RELATIVITY
        private double winterParkGPSLat = 28.596580917445976;
        private double winterParkGPSLong = -81.30134241645047;
        private double boiseGPSLat = 43.61322012630053;
        private double boiseGPSLong = -116.20277481510922;

        public LocationList()
        {
            InitializeComponent();
            BindingContext = this;
            GetUserLoc(); // Grabbing the users Postal Code and Town Name

            //BAR/FOOD PICKER
            barOrFood.ItemsSource = ListOLocations;
            barOrFood.SelectedItem = "Bars";

            //LISTVIEW SELECTION OPERATIONS
            barListView.ItemSelected += (object sender, SelectedItemChangedEventArgs e) =>
            {
                item = e.SelectedItem;
                if (hasbeen == false)
                {
                    DisplayAlert("Yo! Bar Selected!", e.SelectedItem.ToString(), "OK", "Cancel");
                    hasbeen = true;
                    //INCREMENT BAR COUNT
                    //winterParkBars.TryGetValue((e.SelectedItem.ToString()), out count);
                    //winterParkBars[e.SelectedItem.ToString()] = count++;
                    //barListView.ItemsSource = winterParkBars;
                    //previousBar = e.SelectedItem.ToString();
                }
                else if (hasbeen == true)
                {
                    //DEINCREMENT COUNT
                    //winterParkBars.TryGetValue((previousBar), out count);
                    //winterParkBars[previousBar] = count--;
                    //INCREMENT NEW BAR COUNT
                    DisplayAlert("Yo! New Bar?", e.SelectedItem.ToString(), "OK", "Cancel");
                    //winterParkBars.TryGetValue((e.SelectedItem.ToString()), out count);
                    //winterParkBars[e.SelectedItem.ToString()] = count++;
                    //barListView.ItemsSource = winterParkBars;
                    // = e.SelectedItem.ToString();
                }
            };
        }

        async private void ToolbarItem_Clicked(object sender, EventArgs e) { await Navigation.PushAsync(new Settings()); }
        async private void GetUserLoc()
        {
            var location = await Geolocation.GetLocationAsync(new GeolocationRequest(GeolocationAccuracy.Default, TimeSpan.FromMinutes(1)));
            var GetAddy = await Geocoding.GetPlacemarksAsync(location.Latitude, location.Longitude); // Grabbing the users location details as a placemark.
            var addy = GetAddy?.FirstOrDefault();

            /*Web Request URL using variables for location(location.Latitude, location.Longitude), radius(searchRadius), and type(searchType)
            Adding this code incase we can get it working without charging anything...
            string restUrl = $"https://maps.googleapis.com/maps/api/place/nearbysearch/json?location=" +
                location.Latitude + "," + location.Longitude +
                "&radius=" + searchRadius + "&type=" +
                searchType + "&key=AIzaSyAR55DfsuhAWriupO5t_uzWl-FwddZiBhY";

            //Web  Request
            HttpWebRequest webRequest = WebRequest.Create(restUrl) as HttpWebRequest;
            webRequest.Timeout = 2000;
            webRequest.Method = "GET";
            webRequest.BeginGetResponse(new AsyncCallback(RequestCompleted), webRequest);*/

            MyLocation = $"{addy.Locality},{addy.AdminArea}"; // Grabs the users current locations Address-Town and Address-State **Only works in USA**  
            if (addy.Locality == "Winter Park" && barOrFood.SelectedItem == "Bars")
            {
                locale = 0;
                BuildWinterParkBars();
                winterParkBars = winterParkBars.OrderByDescending(i => i.Value).ToDictionary(i => i.Key, i => i.Value);
                AddWinterPBars();
                barListView.ItemsSource = winterParkBars;
            }
            else if (addy.Locality == "Boise")
            {
                locale = 1;
                BuildBoiseBars();
                boiseBars = boiseBars.OrderByDescending(i => i.Value).ToDictionary(i => i.Key, i => i.Value);
                AddBoiseBars();
                barListView.ItemsSource = boiseBars;
            }
        }
        //Get nearby search function, requires payment, leaving in for possible use later
        //private void RequestCompleted(IAsyncResult result)
        //{
        //    var request = (HttpWebRequest)result.AsyncState;
        //    var response = (HttpWebResponse)request.EndGetResponse(result);

        //    using(var stream = response.GetResponseStream())
        //    {
        //        Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
        //        var r = new StreamReader(stream, encode);
        //        Char[] read = new char[256];
        //        int count = r.Read(read, 0, 256);

        //        while(count > 0)
        //        {
        //            String str = new string(read, 0, count);
        //            Console.Write(str);
        //            count = r.Read(read, 0, 256);
        //        }
        //        Console.WriteLine("");
        //        r.Close();

        //        //Old readToEnd might be an alternative to the Char array...
        //        //var resp = r.ReadToEnd();
        //    }
        //}
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
                return new Command(async () =>
                {
                    if (locale == 0)
                    {
                        barListView.ItemsSource = winterParkBars;
                        IsSoRefreshing = false;
                    }
                    else if (locale == 1)
                    {
                        barListView.ItemsSource = boiseBars;
                        IsSoRefreshing = false;
                    }
                });
            }
        }


        //DICTIONARIES
        private void BuildWinterParkBars()
        {
            winterParkBars.Add("Chili's Bar & Grill", rng.Next() % 150); winterParkBars.Add("Firehouse Subs", rng.Next() % 150);
            winterParkBars.Add("Arooga's", rng.Next() % 150); winterParkBars.Add("Chewy Boba", rng.Next() % 150);
            winterParkBars.Add("The Geek Easy", rng.Next() % 150); winterParkBars.Add("Steak 'n Shake", rng.Next() % 150);
            winterParkBars.Add("The Haven At Forsyth", rng.Next() % 150); winterParkBars.Add("El Pueblo Mexicqan", rng.Next() % 150);
            winterParkBars.Add("Debbie's Bar", rng.Next() % 150); winterParkBars.Add("Ain't Misbehavin'", rng.Next() % 150);
            winterParkBars.Add("Miller's Ale House", rng.Next() % 150); winterParkBars.Add("Devaney's Sports Pub", rng.Next() % 150);
            winterParkBars.Add("The Next", rng.Next() % 150); winterParkBars.Add("Admiral Cigar Club", rng.Next() % 150);
            winterParkBars.Add("Gator's Dockside", rng.Next() % 150); winterParkBars.Add("Cork & Plate", rng.Next() % 150);
            winterParkBars.Add("Tactical Brewing Co.", rng.Next() % 150); winterParkBars.Add("The Brass Tap", rng.Next() % 150);
            winterParkBars.Add("Sonny's BBQ", rng.Next() % 150); winterParkBars.Add("La Placita 19'", rng.Next() % 150);
            winterParkBars.Add("Rincon Latino", rng.Next() % 150); winterParkBars.Add("Starbucks", rng.Next() % 150);
            winterParkBars.Add("Tequila Lounge Club", rng.Next() % 150); winterParkBars.Add("Muldoons Saloon", rng.Next() % 150);
            winterParkBars.Add("Texas Roadhous", rng.Next() % 150); winterParkBars.Add("Majijis Hookah Lounge", rng.Next() % 150);
            winterParkBars.Add("Thirsty Gator", rng.Next() % 150); winterParkBars.Add("Don Julio Mexican Kitchen and Tequila Bar", rng.Next() % 150);
            winterParkBars.Add("Fire on the Bayou", rng.Next() % 150); winterParkBars.Add("Rock and Brews", rng.Next() % 150);
            winterParkBars.Add("BJ's Restaurant and Brewhouse", rng.Next() % 150); winterParkBars.Add("Oviedo Brewing Company", rng.Next() % 150);
            winterParkBars.Add("Buffalo Wild WIngs", rng.Next() % 150); winterParkBars.Add("The Green Bar", rng.Next() % 150);
            winterParkBars.Add("The Green Parrot", rng.Next() % 150); winterParkBars.Add("Luke's Kitchen and Bar", rng.Next() % 150);
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

        //OBJECT LISTS
        private void AddWinterPBars()
        {
            WinterBarList.Add(new Bars("Chili's Bar and Grill", 0, 28.59403087, -81.30656552));
            WinterBarList.Add(new Bars("Firehouse Subs", 0, 28.59258033, -81.30474884));
            WinterBarList.Add(new Bars("Arooga's", 0, 28.5946283, -81.30629838));
            WinterBarList.Add(new Bars("Chewy Boba", 0, 28.59641411, -81.30824074));
            WinterBarList.Add(new Bars("The Geek Easy", 0, 28.59623985, -81.30821367));
            WinterBarList.Add(new Bars("Steak 'n Shake", 0, 28.60258523, -81.30694758));
            WinterBarList.Add(new Bars("The Haven At Forsyth", 0, 28.60490524, -81.29871379));
            WinterBarList.Add(new Bars("El Pueblo Mexican", 0, 28.60721189, -81.29323053));
            WinterBarList.Add(new Bars("Debbie's Bar", 0, 28.6275651, -81.31567948));
            WinterBarList.Add(new Bars("Ain't Misbehavin'", 0, 28.6241107, -81.3145854));
            WinterBarList.Add(new Bars("Miller's Ale House", 0, 28.5968018, -81.30427209));
            WinterBarList.Add(new Bars("Devaney's Sports Pub", 0, 28.59624766, -81.28400846));
            WinterBarList.Add(new Bars("The Nest", 0, 28.56893991, -81.32630872));
            WinterBarList.Add(new Bars("Admiral Cigar Club", 0, 28.56827604, -81.32613435));
            WinterBarList.Add(new Bars("Gator's Dockside", 0, 28.56837216, -81.32579108));
            WinterBarList.Add(new Bars("Cork & Plate", 0, 28.56784356, -81.32629655));
            WinterBarList.Add(new Bars("Tactical Brewing Co.", 0, 28.56769248, -81.32670457));
            WinterBarList.Add(new Bars("The Brass Tap", 0, 28.56672798, -81.36469765));
            WinterBarList.Add(new Bars("Redlight Redlight", 0, 28.56767017, -81.34773377));
            WinterBarList.Add(new Bars("Sonny's BBQ", 0, 28.59687612, -81.29959271));
            WinterBarList.Add(new Bars("La Placita 19'", 0, 28.59522661, -81.29852606));
            WinterBarList.Add(new Bars("Rincon Latino", 0, 28.59376795, -81.2984654));
            WinterBarList.Add(new Bars("Starbucks", 0, 28.59708728, -81.28893602));
            WinterBarList.Add(new Bars("Tequila Lounge Club", 0, 28.60751177, -81.29283761));
            WinterBarList.Add(new Bars("Muldoons Saloon", 0, 28.60984284, -81.28740572));
            WinterBarList.Add(new Bars("Texas Roadhouse", 0, 28.6198656, -81.25984926));
            WinterBarList.Add(new Bars("Maljis Hookah Lounge", 0, 28.59824995, -81.28555668));
            WinterBarList.Add(new Bars("Thirsty Gator", 0, 28.5841514, -81.28685249));
            WinterBarList.Add(new Bars("Don Julio Mexican Kitchen & Tequila Bar", 0, 28.5363086, -81.27450985));
            WinterBarList.Add(new Bars("Fire on the Bayou", 0, 28.62552378, -81.24643478));
            WinterBarList.Add(new Bars("Rock And Brews", 0, 28.64804403, -81.24774664));
            WinterBarList.Add(new Bars("BJ's Restaurant And Brewhouse", 0, 28.65629162, -81.23624532));
            WinterBarList.Add(new Bars("Oviedo Brewing Company", 0, 28.66404091, -81.23501079));
            WinterBarList.Add(new Bars("Buffalo Wild Wings", 0, 28.64960344, -81.32376412));
            WinterBarList.Add(new Bars("The Green Bar", 0, 28.65544075, -81.33161763));
            WinterBarList.Add(new Bars("The Green Parrot", 0, 28.65886767, -81.33500794));
            WinterBarList.Add(new Bars("Luke's Kitchen And Bar", 0, 28.62303613, -81.36354082));

        }
        private void AddBoiseBars()
        {
            BoiseBarList.Add(new Bars("Pengilly's Saloon", 0, 43.61526479660417, -116.20096442501762));
            BoiseBarList.Add(new Bars("Whiskey Bar", 0, 43.61397620118385, -116.20092685483587));
            BoiseBarList.Add(new Bars("Press & Pony", 0, 43.61955267406604, -116.19989838611181));
            BoiseBarList.Add(new Bars("The Handlebar", 0, 43.62334065566668, -116.21260132802183));
            BoiseBarList.Add(new Bars("The Atlas Bar", 0, 43.62054684129219, -116.20642151846245));
            BoiseBarList.Add(new Bars("The Spacebar", 0, 43.6215409920796, -116.20058503162238));
            BoiseBarList.Add(new Bars("Neurolox", 0, 43.623777771246935, -116.20745148672832));
            BoiseBarList.Add(new Bars("Taphouse", 0, 43.616818629432025, -116.20333161366477));
            BoiseBarList.Add(new Bars("Water Bear Bar", 0, 43.6185545959522, -116.20249992182366));
            BoiseBarList.Add(new Bars("Cactus Bar", 0, 43.61458159135494, -116.20058503162238));
            BoiseBarList.Add(new Bars("The Mode Lounge", 0, 43.616818629432025, -116.20298829090947));
            BoiseBarList.Add(new Bars("Mulligans'", 0, 43.61731573770315, -116.20676484121773));
            BoiseBarList.Add(new Bars("Bardenay Restaurant &Distillery", 0, 43.61458159135494, -116.20195832264359));
            BoiseBarList.Add(new Bars("Tom Grainey's", 0, 43.61483015525094, -116.20161499988826));
            BoiseBarList.Add(new Bars("Humpin' Hannah's", 0, 43.61507871811954, -116.20264496815417));
            BoiseBarList.Add(new Bars("Boise Brewing", 0, 43.61234447005752, -116.20333161366477));
            BoiseBarList.Add(new Bars("Payette Brewing", 0, 43.61354488095449, -116.21521045240796));
            BoiseBarList.Add(new Bars("10 Barrel Brewing", 0, 43.61835157886452, -116.20195832262522));
            BoiseBarList.Add(new Bars("White Dog Brewing", 0, 43.61180487491952, -116.20644813352556));
            BoiseBarList.Add(new Bars("Barbarian Brewing", 0, 43.61706718408128, -116.20745148672832));
            BoiseBarList.Add(new Bars("Clairvoyant Brewing", 0, 43.62375372371132, -116.22496094718149));
            BoiseBarList.Add(new Bars("Lost Grove Brewing", 0, 43.60832045775409, -116.213142927281));
            BoiseBarList.Add(new Bars("Cloud 9 Brewing", 0, 43.625631610848075, -116.21074556463934));
            BoiseBarList.Add(new Bars("Woodland Brewing", 0, 43.616818629432025, -116.20882477774951));
            BoiseBarList.Add(new Bars("Edge Brewing", 0, 43.618309941916394, -116.20470490468595));
            BoiseBarList.Add(new Bars("Highlands Hollow Brewhouse", 0, 43.642717475209565, -116.2085466154226));
            BoiseBarList.Add(new Bars("Ram Restaurant &Brewery", 0, 43.60443202147887, -116.19245784641521));
            BoiseBarList.Add(new Bars("Bittercreek Alehouse", 0, 43.61632151705125, -116.20195832264359));
            BoiseBarList.Add(new Bars("The Silly Birch", 0, 43.61408446048073, -116.20092835437768));
            BoiseBarList.Add(new Bars("The Gas Lantern Drinking Company", 0, 43.611163062308826, -116.20627754494802));
            BoiseBarList.Add(new Bars("Double Tap Pub", 0, 43.61314866093511, -116.2061517149943));
            BoiseBarList.Add(new Bars("Bar Gernika", 0, 43.61483015525094, -116.20333161366477));
            BoiseBarList.Add(new Bars("Dirty Little Roddy's", 0, 43.61420840058599, -116.20115869777291));
            BoiseBarList.Add(new Bars("Amsterdam Lounge", 0, 43.61483015525094, -116.20195832264359));

        }
        private void AddWinterPFoods() { }
        private void AddBoiseFoods() { }

        //ASCENDING BUTTON
        private void OnClick_Ascend(object sender, EventArgs e)
        {
            //OrderBy Value
            if (locale == 0)
            {
                winterParkBars = winterParkBars.OrderBy(i => i.Value).ToDictionary(i => i.Key, i => i.Value);
                barListView.ItemsSource = winterParkBars;
            }
            else if (locale == 1)
            {
                boiseBars = boiseBars.OrderBy(i => i.Value).ToDictionary(i => i.Key, i => i.Value);
                barListView.ItemsSource = boiseBars;

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

        }
    }
    //OBJECTS
    public class Bars
    {
        public string Bar { get; set; }
        public int Users { get; set; }
        public double BarLat { get; set; }
        public double BarLong { get; set; }
        public Bars(string _Bar, int _Users, double _BarLat, double _BarLong)
        {
            Bar = _Bar;
            Users = _Users;
            BarLat = _BarLat;
            BarLong = _BarLong;
        }
    }
    public class Foods
    {
        public string Food { get; set; }
        public int Users { get; set; }
        public double FoodLat { get; set; }
        public double FoodLong { get; set; }
        public Foods(string _Food, int _Users, double _FoodLat, double _FoodLong)
        {
            Food = _Food;
            Users = _Users;
            FoodLat = _FoodLat;
            FoodLong = _FoodLong;
        }
    }

}
