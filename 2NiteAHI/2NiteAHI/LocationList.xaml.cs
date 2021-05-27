using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Windows.Input;
using System.Threading;

namespace _2NiteAHI
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LocationList : ContentPage
    {
        //CONTAINERS
        // 1st Location
        public Dictionary<string, int> winterParkBars = new Dictionary<string, int>();
        public Dictionary<string, string> winParkBarsProx = new Dictionary<string, string>();
        public Dictionary<string, int> winterParkFoods = new Dictionary<string, int>();
        public Dictionary<string, string> winParkFoodsProx = new Dictionary<string, string>();

        // 2nd Location
        public Dictionary<string, int> boiseBars = new Dictionary<string, int>();
        public Dictionary<string, string> boiseBarsProx = new Dictionary<string, string>();
        public Dictionary<string, int> boiseFoods = new Dictionary<string, int>();
        public Dictionary<string, string> boiseFoodsProx = new Dictionary<string, string>();

        // 3rd Location Manhattan
        public Dictionary<string, int> nycbars = new Dictionary<string, int>();
        public Dictionary<string, string> nycbarsprox = new Dictionary<string, string>();
        public Dictionary<string, int> nycfoods = new Dictionary<string, int>();
        public Dictionary<string, string> nycfoodsprox = new Dictionary<string, string>();

        //VARIABLES
        private string myloc;

        private string selecteditem;
        private int usercount;
        private bool hasbeen;
        private bool _isSoRefreshing = false;
        private bool restPressed, barPressed;
        private string temp;

        //OBJECTS
        Random rng = new Random();

        //CENTERING COORDINATES FOR RELATIVITY
        //private double winterParkGPSLat = 28.596580917445976;
        //private double winterParkGPSLong = -81.30134241645047;
        //private double boiseGPSLat = 43.61322012630053;
        //private double boiseGPSLong = -116.20277481510922;

        //MAIN
        public LocationList()
        {
            InitializeComponent();
            DisplayAlert("Yo! Welcome to 2Nite!",
                "The list in this view displays local Bars or Restaurants depending on which button you click.\n\n" +
                "The number below each place represents how many users are at that location.\n\n" +
                "The Peace-Out button is used to remove yourself from the list and exit the app.\n\n" +
                "The ASC/DESC buttons are used to reorder the lists.\n\n" +
                "The Proximity button shows the distance in meters to each location with the closest at top.\n\n" +
                "Use Settings to see your personal info or change from Day & Night mode, and location\n\n",
                "OK");
            BindingContext = this;
            GetUserLoc();

            //LISTVIEW SELECTION OPERATIONS
            barListView.ItemSelected += async (object sender, SelectedItemChangedEventArgs e) =>
            {
                //WINTER PARK INITIAL SELECT
                if (hasbeen == false && App.locale == 0 && App.xlocale == 0)
                {
                    var ans = await DisplayAlert("Yo! Checking in?", "Have fun!", "Yes", "Cancel");
                    if (ans == true) // pressing OK
                    {
                        selecteditem = e.SelectedItem.ToString(); // Changing event handler to string
                        selecteditem = selecteditem.Trim('[', ']'); // triming brackets
                        string[] splititems = selecteditem.Split(','); // delim comma
                        usercount = winterParkBars[splititems[0]]; // Grabbing current user count of bar/rest
                        winterParkBars[splititems[0]] = usercount + 1; // increment from current bar/rest count
                        temp = splititems[0]; // Saving the selected data for decrements
                        hasbeen = true;
                        GetUserLoc();
                        RefreshCommand.Execute(barListView.ItemsSource = winterParkBars);
                    }
                    else { return; }
                }

                //WINTER PARK NEXT SELECT
                else if (hasbeen == true && App.locale == 0 && App.xlocale == 0)
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
                        RefreshCommand.Execute(barListView.ItemsSource = winterParkBars);
                    }
                    else { return; }
                }

                //BOISE INITIAL SELECT
                else if (hasbeen == false && App.locale == 1 && App.xlocale == 0)
                {
                    var ansb = await DisplayAlert("Yo! Checking in??", "Have fun!", "Yes", "Cancel");
                    if (ansb == true)
                    {
                        selecteditem = e.SelectedItem.ToString();
                        selecteditem = selecteditem.Trim('[', ']');
                        string[] splititems = selecteditem.Split(',');
                        usercount = boiseBars[splititems[0]];
                        boiseBars[splititems[0]] = usercount + 1;
                        temp = splititems[0];
                        hasbeen = true;
                        RefreshCommand.Execute(barListView.ItemsSource = boiseBars);
                    }
                    else { return; }
                }

                //BOISE NEXT SELECT
                else if (hasbeen == true && App.locale == 1 && App.xlocale == 0)
                {
                    var ansbb = await DisplayAlert("Yo New Bar?", "Would You Like To Check In?", "Yes", "Cancel");
                    if (ansbb == true)
                    {
                        usercount = boiseBars[temp];
                        boiseBars[temp] = usercount - 1;
                        selecteditem = e.SelectedItem.ToString();
                        selecteditem = selecteditem.Trim('[', ']');
                        string[] splititems = selecteditem.Split(',');
                        usercount = boiseBars[splititems[0]];
                        boiseBars[splititems[0]] = usercount + 1;
                        temp = splititems[0];
                        RefreshCommand.Execute(barListView.ItemsSource = boiseBars);
                    }
                    else { return; }
                }

                //WINTER PARK FOOD SELECT
                else if (hasbeen == false && App.locale == 0 && App.xlocale == 2)
                {
                    var ansb = await DisplayAlert("Yo! Checking in?", "Have fun!", "Yes", "Cancel");
                    if (ansb == true)
                    {
                        selecteditem = e.SelectedItem.ToString();
                        selecteditem = selecteditem.Trim('[', ']');
                        string[] splititems = selecteditem.Split(',');
                        usercount = winterParkFoods[splititems[0]];
                        winterParkFoods[splititems[0]] = usercount + 1;
                        temp = splititems[0];
                        hasbeen = true;
                        RefreshCommand.Execute(barListView.ItemsSource = winterParkFoods);
                    }
                    else { return; }
                }

                //WINTER PARK FOOD NEXT SELECT
                else if (hasbeen == true && App.locale == 0 && App.xlocale == 2)
                {
                    var ansbb = await DisplayAlert("Yo New Restaurant?", "Would You Like To Check In?", "Yes", "Cancel");
                    if (ansbb == true)
                    {
                        usercount = winterParkFoods[temp];
                        winterParkFoods[temp] = usercount - 1;
                        selecteditem = e.SelectedItem.ToString();
                        selecteditem = selecteditem.Trim('[', ']');
                        string[] splititems = selecteditem.Split(',');
                        usercount = winterParkFoods[splititems[0]];
                        winterParkFoods[splititems[0]] = usercount + 1;
                        temp = splititems[0];
                        RefreshCommand.Execute(barListView.ItemsSource = winterParkFoods);
                    }
                    else { return; }
                }

                //BOISE FOOD SELECT
                else if (hasbeen == false && App.locale == 1 && App.xlocale == 3)
                {
                    var ansb = await DisplayAlert("Yo! Checking in?", "Have fun!", "Yes", "Cancel");
                    if (ansb == true)
                    {
                        selecteditem = e.SelectedItem.ToString();
                        selecteditem = selecteditem.Trim('[', ']');
                        string[] splititems = selecteditem.Split(',');
                        usercount = boiseFoods[splititems[0]];
                        boiseFoods[splititems[0]] = usercount + 1;
                        temp = splititems[0];
                        hasbeen = true;
                        RefreshCommand.Execute(barListView.ItemsSource = boiseFoods);
                    }
                    else { return; }
                }

                //BOISE FOOD NEXT SELECT
                else if (hasbeen == true && App.locale == 1 && App.xlocale == 3)
                {
                    var ansbb = await DisplayAlert("Yo New Restaurant?", "Would You Like To Check In?", "Yes", "Cancel");
                    if (ansbb == true)
                    {
                        usercount = boiseFoods[temp];
                        boiseFoods[temp] = usercount - 1;
                        selecteditem = e.SelectedItem.ToString();
                        selecteditem = selecteditem.Trim('[', ']');
                        string[] splititems = selecteditem.Split(',');
                        usercount = boiseFoods[splititems[0]];
                        boiseFoods[splititems[0]] = usercount + 1;
                        temp = splititems[0];
                        RefreshCommand.Execute(barListView.ItemsSource = boiseFoods);
                    }
                    else { return; }
                }

                // Nyc INITIAL SELECT Bars
                else if (hasbeen == false && App.locale == 2 && App.xlocale == 0)
                {
                    var ans = await DisplayAlert("Yo! Checking in?", "Have fun!", "Yes", "Cancel");
                    if (ans == true) // pressing OK
                    {
                        selecteditem = e.SelectedItem.ToString(); // Changing event handler to string
                        selecteditem = selecteditem.Trim('[', ']'); // triming brackets
                        string[] splititems = selecteditem.Split(','); // delim comma
                        usercount = nycbars[splititems[0]]; // Grabbing current user count of bar/rest
                        nycbars[splititems[0]] = usercount + 1; // increment from current bar/rest count
                        temp = splititems[0]; // Saving the selected data for decrements
                        hasbeen = true;
                        RefreshCommand.Execute(barListView.ItemsSource = nycbars);
                    }
                    else { return; }
                }

                // Nyc NEXT SELECT bar
                else if (hasbeen == true && App.locale == 2 && App.xlocale == 0)
                {
                    var anss = await DisplayAlert("Changing bars???", "Would You Like To Check In?", "Yes", "Cancel");
                    if (anss == true) // pressing OK
                    {
                        usercount = nycbars[temp];
                        nycbars[temp] = usercount - 1; // decrementing old bar
                        selecteditem = e.SelectedItem.ToString(); // Changing event handler to string
                        selecteditem = selecteditem.Trim('[', ']'); // triming brackets
                        string[] splititems = selecteditem.Split(','); // delim comma
                        usercount = nycbars[splititems[0]]; // Grabbing current user count of bar/rest
                        nycbars[splititems[0]] = usercount + 1; // Incrementing CHANGED bar
                        temp = splititems[0];
                        RefreshCommand.Execute(barListView.ItemsSource = nycbars);
                    }
                    else { return; }
                }

                // nyc food intial select
                else if (hasbeen == false && App.locale == 2 && App.xlocale == 4)
                {
                    var ansb = await DisplayAlert("Yo! Checking in?", "Have fun!", "Yes", "Cancel");
                    if (ansb == true)
                    {
                        selecteditem = e.SelectedItem.ToString();
                        selecteditem = selecteditem.Trim('[', ']');
                        string[] splititems = selecteditem.Split(',');
                        usercount = nycfoods[splititems[0]];
                        nycfoods[splititems[0]] = usercount + 1;
                        temp = splititems[0];
                        hasbeen = true;
                        RefreshCommand.Execute(barListView.ItemsSource = nycfoods);
                    }
                    else { return; }
                }

                // nyc food next select
                else if (hasbeen == true && App.locale == 2 && App.xlocale == 4)
                {
                    var ansbb = await DisplayAlert("Yo New Restaurant?", "Would You Like To Check In?", "Yes", "Cancel");
                    if (ansbb == true)
                    {
                        usercount = nycfoods[temp];
                        nycfoods[temp] = usercount - 1;
                        selecteditem = e.SelectedItem.ToString();
                        selecteditem = selecteditem.Trim('[', ']');
                        string[] splititems = selecteditem.Split(',');
                        usercount = nycfoods[splititems[0]];
                        nycfoods[splititems[0]] = usercount + 1;
                        temp = splititems[0];
                        RefreshCommand.Execute(barListView.ItemsSource = nycfoods);
                    }
                    else { return; }
                }
            };
        }

        async private void ToolbarItem_Clicked(object sender, EventArgs e) { await Navigation.PushAsync(new Settings()); }

        //LOCATION FUNCTIONS
        async private void GetUserLoc()
        {
            if (App.isMyLocation == 0) 
            {
                var location = await Geolocation.GetLocationAsync(new GeolocationRequest(GeolocationAccuracy.Default, TimeSpan.FromMinutes(1)));
                var GetAddy = await Geocoding.GetPlacemarksAsync(location.Latitude, location.Longitude); // Grabbing the users location details as a placemark.
                var addy = GetAddy?.FirstOrDefault();
                MyLocation = $"{addy.Locality},  {addy.AdminArea}";
                App.MyLocation = MyLocation;

                if (MyLocation == "Boise, Idaho")
                {
                    App.isMyLocation = 1;
                    App.locale = 1;
                }
                else if (MyLocation == "Winter Park, Florida")
                {
                    App.isMyLocation = 2;
                    App.locale = 0;
                }
                else if (MyLocation == "New York, New York")
                {
                    App.isMyLocation = 3;
                    App.locale = 2;
                }
            }

            if (App.locale == 0 || App.isMyLocation == 2)
            {
                App.MyLocation = "Winter Park, Florida";
                App.locale = 0;
                BuildWinterParkBars();
                winterParkBars = winterParkBars.OrderByDescending(i => i.Value).ToDictionary(i => i.Key, i => i.Value);
                barListView.ItemsSource = winterParkBars;
                MyLocation = "Winter Park, Florida";
            }
            else if (App.locale == 1 || App.isMyLocation == 1)
            {
                App.MyLocation = "Boise, Idaho";
                App.locale = 1;
                BuildBoiseBars();
                boiseBars = boiseBars.OrderByDescending(i => i.Value).ToDictionary(i => i.Key, i => i.Value);
                barListView.ItemsSource = boiseBars;
                MyLocation = "Boise, Idaho";

            }
            else if (App.locale == 2 || App.isMyLocation == 3)
            {
                App.MyLocation = "New York, New York";
                App.locale = 2;
                BuildnycBars();
                nycbars = nycbars.OrderByDescending(i => i.Value).ToDictionary(i => i.Key, i => i.Value);
                barListView.ItemsSource = nycbars;
                MyLocation = "New York, New York";
            }
        }
        public string MyLocation
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

        public ICommand RefreshCommand
        {
            get
            {
                return new Command(() =>
                {
                    if (App.locale == 0 && App.xlocale == 0)
                    {
                        barListView.ItemsSource = null; // resets the list back to null
                        barListView.ItemsSource = winterParkBars; // refactors updated list
                        IsSoRefreshing = false;
                        winParkBarsProx.Clear();
                    }
                    else if (App.locale == 1 && App.xlocale == 0)
                    {
                        barListView.ItemsSource = null; // resets the list back to null
                        barListView.ItemsSource = boiseBars; // refactors updated list
                        IsSoRefreshing = false;
                        boiseBarsProx.Clear();
                    }
                    else if (App.locale == 2 && App.xlocale == 0)
                    {
                        barListView.ItemsSource = null; // resets the list back to null
                        barListView.ItemsSource = nycbars; // refactors updated list
                        IsSoRefreshing = false;
                        nycbarsprox.Clear();
                    }
                    else if (App.locale == 0 && App.xlocale == 2)
                    {
                        barListView.ItemsSource = null; // resets the list back to null
                        barListView.ItemsSource = winterParkFoods; // refactors updated list
                        IsSoRefreshing = false;
                        winParkFoodsProx.Clear();
                    }
                    else if (App.locale == 1 && App.xlocale == 3)
                    {
                        barListView.ItemsSource = null; // resets the list back to null
                        barListView.ItemsSource = boiseFoods; // refactors updated list
                        IsSoRefreshing = false;
                        boiseFoodsProx.Clear();
                    }
                    else if (App.locale == 2 && App.xlocale == 4)
                    {
                        barListView.ItemsSource = null; // resets the list back to null
                        barListView.ItemsSource = nycfoods; // refactors updated list
                        IsSoRefreshing = false;
                        nycfoodsprox.Clear();
                    }
                });
            }
        }

        //DICTIONARIES BUILDERS
        //BARS
        private void BuildWinterParkBars()
        {
            winterParkBars.Clear();
            winterParkBars.Add("The Brass Tap", rng.Next() % 150);
            winterParkBars.Add("Tequila Lounge Club", rng.Next() % 150);
            winterParkBars.Add("Muldoons Saloon", rng.Next() % 150);
            winterParkBars.Add("The Geek Easy", rng.Next() % 150);
            winterParkBars.Add("The Haven At Forsyth", rng.Next() % 150);
            winterParkBars.Add("Debbie's Bar", rng.Next() % 150);
            winterParkBars.Add("Ain't Misbehavin'", rng.Next() % 150);
            winterParkBars.Add("Rock and Brews", rng.Next() % 150);
            winterParkBars.Add("Oviedo Brewing Company", rng.Next() % 150);
            winterParkBars.Add("The Green Bar", rng.Next() % 150);
            winterParkBars.Add("The Green Parrot", rng.Next() % 150);
            winterParkBars.Add("Miller's Ale House", rng.Next() % 150);
            winterParkBars.Add("Devaney's Sports Pub", rng.Next() % 150);
            winterParkBars.Add("Majijis Hookah Lounge", rng.Next() % 150);
            winterParkBars.Add("Tactical Brewing Co.", rng.Next() % 150);
            winterParkBars.Add("The Next", rng.Next() % 150);
            winterParkBars.Add("Admiral Cigar Club", rng.Next() % 150);
        }
        private void BuildnycBars()
        {
            nycbars.Clear();
            nycbars.Add("The Jeffery Craft", rng.Next() % 150);
            nycbars.Add("Mercury Bar West", rng.Next() % 150);
            nycbars.Add("Down The Road Sports Bar", rng.Next() % 150);
            nycbars.Add("The House Of Brews", rng.Next() % 150);
            nycbars.Add("Deacon Brodie's Tavern", rng.Next() % 150);
            nycbars.Add("Wine Escape", rng.Next() % 150);
            nycbars.Add("Dalton's Bar & Grill", rng.Next() % 150);
            nycbars.Add("Dave's Tavern", rng.Next() % 150);
            nycbars.Add("Scruffy Duffy's Bar", rng.Next() % 150);
            nycbars.Add("The Press Lounge", rng.Next() % 150);
            nycbars.Add("Mom's Kitchen & Bar", rng.Next() % 150);
        }
        private void BuildBoiseBars()
        {
            boiseBars.Clear();
            boiseBars.Add("Pengilly's Saloon", rng.Next() % 150);
            boiseBars.Add("Whiskey Bar", rng.Next() % 150);
            boiseBars.Add("Press & Pony", rng.Next() % 150);
            boiseBars.Add("The Handlebar", rng.Next() % 150);
            boiseBars.Add("The Atlas Bar", rng.Next() % 150);
            boiseBars.Add("The Spacebar", rng.Next() % 150);
            boiseBars.Add("Neurolox", rng.Next() % 150);
            boiseBars.Add("Taphouse", rng.Next() % 150);
            boiseBars.Add("Water Bear Bar", rng.Next() % 150);
            boiseBars.Add("Cactus Bar", rng.Next() % 150);
            boiseBars.Add("The Mode Lounge", rng.Next() % 150);
            boiseBars.Add("Mulligans'", rng.Next() % 150);
            boiseBars.Add("Bardenay Restaurant & Distillery", rng.Next() % 150);
            boiseBars.Add("Tom Grainey's", rng.Next() % 150);
            boiseBars.Add("Humpin' Hannah's", rng.Next() % 150);
            boiseBars.Add("Boise Brewing", rng.Next() % 150);
            boiseBars.Add("Payette Brewing", rng.Next() % 150);
            boiseBars.Add("10 Barrel Brewing", rng.Next() % 150);
            boiseBars.Add("White Dog Brewing", rng.Next() % 150);
            boiseBars.Add("Barbarian Brewing", rng.Next() % 150);
            boiseBars.Add("Clairvoyant Brewing", rng.Next() % 150);
            boiseBars.Add("Lost Grove Brewing", rng.Next() % 150);
            boiseBars.Add("Cloud 9 Brewing", rng.Next() % 150);
            boiseBars.Add("Woodland Brewing ", rng.Next() % 150);
            boiseBars.Add("Edge Brewing", rng.Next() % 150);
            boiseBars.Add("Highlands Hollow Brewhouse", rng.Next() % 150);
            boiseBars.Add("Ram Restaurant & Brewery", rng.Next() % 150);
            boiseBars.Add("Bittercreek Alehouse", rng.Next() % 150);
            boiseBars.Add("The Silly Birch", rng.Next() % 150);
            boiseBars.Add("The Gas Lantern Drinking Company", rng.Next() % 150);
            boiseBars.Add("Double Tap Pub", rng.Next() % 150);
            boiseBars.Add("Bar Gernika", rng.Next() % 150);
            boiseBars.Add("Dirty Little Roddy's", rng.Next() % 150);
            boiseBars.Add("Amsterdam Lounge", rng.Next() % 150);
        }
        //PROXIMITY OF BARS
        private void BuildWinParkBarsProx()
        {
            winParkBarsProx.Clear();
            winParkBarsProx.Add("The Geek Easy", "671m");
            winParkBarsProx.Add("The Haven At Forsyth", "905m");
            winParkBarsProx.Add("Debbie's Bar", "3719m");
            winParkBarsProx.Add("Ain't Misbehavin'", "3232m");
            winParkBarsProx.Add("Miller's Ale House", "287m");
            winParkBarsProx.Add("Devaney's Sports Pub", "1693m");
            winParkBarsProx.Add("The Nest", "3923m");
            winParkBarsProx.Add("Admiral Cigar Club", "3971m");
            winParkBarsProx.Add("Tactical Brewing Co.", "4056m");
            winParkBarsProx.Add("The Brass Tap", "7021m");
            winParkBarsProx.Add("Redlight Redlight", "5555m");
            winParkBarsProx.Add("Tequila Lounge Club", "1427m");
            winParkBarsProx.Add("Muldoons Saloon", "2006m");
            winParkBarsProx.Add("Majijis Hookah Lounge", "1552m");
            winParkBarsProx.Add("Rock and Brews", "7753m");
            winParkBarsProx.Add("Oviedo Brewing Company", "9909m");
            winParkBarsProx.Add("The Green Bar", "7181m");
            winParkBarsProx.Add("The Green Parrot", "7666m");
        }
        private void BuildBoiseBarsProx()
        {
            boiseBarsProx.Clear();
            boiseBarsProx.Add("Pengilly's Saloon", "270m");
            boiseBarsProx.Add("Whiskey Bar", "170m");
            boiseBarsProx.Add("Press & Pony", "741m");
            boiseBarsProx.Add("The Handlebar", "1376m");
            boiseBarsProx.Add("The Atlas Bar", "866m");
            boiseBarsProx.Add("The Spacebar", "942m");
            boiseBarsProx.Add("Neurolox", "1233m");
            boiseBarsProx.Add("Taphouse", "402m");
            boiseBarsProx.Add("Water Bear Bar", "594m");
            boiseBarsProx.Add("Cactus Bar", "232m");
            boiseBarsProx.Add("The Mode Lounge", "401m");
            boiseBarsProx.Add("Mulligans'", "557m");
            boiseBarsProx.Add("Bardenay Restaurant & Distillery", "165m");
            boiseBarsProx.Add("Tom Grainey's", "202m");
            boiseBarsProx.Add("Humpin' Hannah's", "269m");
            boiseBarsProx.Add("Boise Brewing", "107m");
            boiseBarsProx.Add("Payette Brewing", "1002m");
            boiseBarsProx.Add("10 Barrel Brewing", "574m");
            boiseBarsProx.Add("White Dog Brewing", "335m");
            boiseBarsProx.Add("Barbarian Brewing", "567m");
            boiseBarsProx.Add("Clairvoyant Brewing", "570m");
            boiseBarsProx.Add("Lost Grove Brewing", "2136m");
            boiseBarsProx.Add("Cloud 9 Brewing", "1522m");
            boiseBarsProx.Add("Woodland Brewing ", "630m");
            boiseBarsProx.Add("Edge Brewing", "587m");
            boiseBarsProx.Add("Highlands Hollow Brewhouse", "3313m");
            boiseBarsProx.Add("Ram Restaurant & Brewery", "1283m");
            boiseBarsProx.Add("Bittercreek Alehouse", "351m");
            boiseBarsProx.Add("The Silly Birch", "177m");
            boiseBarsProx.Add("The Gas Lantern Drinking Company", "363m");
            boiseBarsProx.Add("Double Tap Pub", "277m");
            boiseBarsProx.Add("Bar Gernika", "185m");
            boiseBarsProx.Add("Dirty Little Roddy's", "170m");
            boiseBarsProx.Add("Amsterdam Lounge", "190m");
        }
        private void BuildnycBarsProx()
        {
            nycbarsprox.Clear();
            nycbarsprox.Add("The Jeffery Craft", "1523m");
            nycbarsprox.Add("Mercury Bar West", "1907m");
            nycbarsprox.Add("Down The Road Sports Bar", "1735m");
            nycbarsprox.Add("The House Of Brews", "1804m");
            nycbarsprox.Add("Deacon Brodie's Tavern", "1877m");
            nycbarsprox.Add("Wine Escape", "2035m");
            nycbarsprox.Add("Dalton's Bar & Grill", "2085m");
            nycbarsprox.Add("Dave's Tavern", "2223m");
            nycbarsprox.Add("Scruffy Duffy's Bar", "2048m");
            nycbarsprox.Add("The Press Lounge", "2028m");
            nycbarsprox.Add("Mom's Kitchen & Bar", "1709m!");
        }
        //FOODS
        private void BuildWinterParkFoods()
        {
            winterParkFoods.Clear();
            winterParkFoods.Add("Chili's Bar & Grill", rng.Next() % 150);
            winterParkFoods.Add("Firehouse Subs", rng.Next() % 150);
            winterParkFoods.Add("Arooga's", rng.Next() % 150);
            winterParkFoods.Add("Chewy Boba", rng.Next() % 150);
            winterParkFoods.Add("Steak 'n Shake", rng.Next() % 150);
            winterParkFoods.Add("El Pueblo Mexicqan", rng.Next() % 150);
            winterParkFoods.Add("Luke's Kitchen and Bar", rng.Next() % 150);
            winterParkFoods.Add("Buffalo Wild WIngs", rng.Next() % 150);
            winterParkFoods.Add("BJ's Restaurant and Brewhouse", rng.Next() % 150);
            winterParkFoods.Add("Don Julio Mexican Kitchen and Tequila Bar", rng.Next() % 150);
            winterParkFoods.Add("Texas Roadhous", rng.Next() % 150);
            winterParkFoods.Add("Starbucks", rng.Next() % 150);
            winterParkFoods.Add("Sonny's BBQ", rng.Next() % 150);
            winterParkFoods.Add("Gator's Dockside", rng.Next() % 150);
            winterParkFoods.Add("Cork & Plate", rng.Next() % 150);
            winterParkFoods.Add("La Placita 19'", rng.Next() % 150);
            winterParkFoods.Add("Rincon Latino", rng.Next() % 150);
            winterParkFoods.Add("Thirsty Gator", rng.Next() % 150);
            winterParkFoods.Add("Fire on the Bayou", rng.Next() % 150);
        }
        private void BuildBoiseFoods()
        {
            boiseFoods.Clear();
            boiseFoods.Add("Del Taco", rng.Next() % 150);
            boiseFoods.Add("Meraki Greek Street Food", rng.Next() % 150);
            boiseFoods.Add("Trillium Restaurant", rng.Next() % 150);
            boiseFoods.Add("Boise Fry Company", rng.Next() % 150);
            boiseFoods.Add("Bittercreek Alehouse", rng.Next() % 150);
            boiseFoods.Add("Eureka", rng.Next() % 150);
            boiseFoods.Add("Chandlers Prime Steaks & Seafood", rng.Next() % 150);
            boiseFoods.Add("Manfred’s Kitchen", rng.Next() % 150);
            boiseFoods.Add("Owyhee Tavern  Steakhouse", rng.Next() % 150);
            boiseFoods.Add("Bombay Grill", rng.Next() % 150);
            boiseFoods.Add("Txikitea", rng.Next() % 150);
            boiseFoods.Add("KIN", rng.Next() % 150);
            boiseFoods.Add("Main Street Deli", rng.Next() % 150);
            boiseFoods.Add("Fork", rng.Next() % 150);
            boiseFoods.Add("Even Stevens Sandwiches", rng.Next() % 150);
            boiseFoods.Add("Lucky Fins Seafood Grill", rng.Next() % 150);
        }
        private void Buildnycfoods()
        {
            nycfoods.Clear();
            nycfoods.Add("Authentic NYC Street Food", rng.Next() % 150);
            nycfoods.Add("Cantina Rooftop", rng.Next() % 150);
            nycfoods.Add("Cook Unity", rng.Next() % 150);
            nycfoods.Add("Burger Shot Beer", rng.Next() % 150);
            nycfoods.Add("Taboon", rng.Next() % 150);
            nycfoods.Add("Mamasita Grill", rng.Next() % 150);
            nycfoods.Add("Le Soleil", rng.Next() % 150);
            nycfoods.Add("Justino's Pizza", rng.Next() % 150);
            nycfoods.Add("Fogon's", rng.Next() % 150);
        }
        //PROXIMITY OF FOODS
        private void BuildWinParkFoodsProx()
        {
            winParkFoodsProx.Clear();
            winParkFoodsProx.Add(" Chili's Bar & Grill ", "584m");
            winParkFoodsProx.Add("Firehouse Subs", "554m");
            winParkFoodsProx.Add("Arooga's", "530m");
            winParkFoodsProx.Add("Chewy Boba", "674m");
            winParkFoodsProx.Add("Steak 'n Shake", "863m");
            winParkFoodsProx.Add("El Pueblo Mexicqan", "1423m");
            winParkFoodsProx.Add("Luke's Kitchen and Bar", "6747m");
            winParkFoodsProx.Add("Buffalo Wild WIngs", "6289m");
            winParkFoodsProx.Add("BJ's Restaurant and Brewhouse", "9190m");
            winParkFoodsProx.Add("Don Julio Mexican Kitchen and Tequila Bar", "7196m");
            winParkFoodsProx.Add("Texas Roadhous", "4180m");
            winParkFoodsProx.Add("Starbucks", "1213m");
            winParkFoodsProx.Add("Sonny's BBQ", "174m");
            winParkFoodsProx.Add("Gator's Dockside", "3942m");
            winParkFoodsProx.Add("Cork & Plate", "1480m");
            winParkFoodsProx.Add("La Placita 19'", "314m");
            winParkFoodsProx.Add("Rincon Latino", "420m");
            winParkFoodsProx.Add("Fire on the Bayou", "6252m");
            winParkFoodsProx.Add("Thirsty Gator", "1978m");
        }
        private void BuildBoiseFoodsProx()
        {
            boiseFoodsProx.Clear();
            boiseFoodsProx.Add("Del Taco", "970m");
            boiseFoodsProx.Add("Meraki Greek Street Food", "248m");
            boiseFoodsProx.Add("Trillium Restaurant", "143m");
            boiseFoodsProx.Add("Boise Fry Company", "377m");
            boiseFoodsProx.Add("Bittercreek Alehouse", "398m");
            boiseFoodsProx.Add("Eureka", "453m");
            boiseFoodsProx.Add("Chandlers Prime Steaks & Seafood", "425m");
            boiseFoodsProx.Add("Manfred’s Kitchen", "636m");
            boiseFoodsProx.Add("Owyhee Tavern    Steakhouse", "626m");
            boiseFoodsProx.Add("Bombay Grill", "498m");
            boiseFoodsProx.Add("Txikitea", "984m");
            boiseFoodsProx.Add("KIN", "428m");
            boiseFoodsProx.Add("Main Street Deli", "416m");
            boiseFoodsProx.Add("Fork", "366m");
            boiseFoodsProx.Add("Even Stevens Sandwiches", "488m");
            boiseFoodsProx.Add("Lucky Fins Seafood Grill", "280m");
        }
        private void BuildnycFoodProx()
        {
            nycfoodsprox.Clear();
            nycfoodsprox.Add("Authentic NYC Street Food", "2172m");
            nycfoodsprox.Add("Cantina Rooftop", "2014m");
            nycfoodsprox.Add("Mamasitas Grill", "1314m");
            nycfoodsprox.Add("Cook Unity", "1803m");
            nycfoodsprox.Add("Burger Shot Beer", "1675m");
            nycfoodsprox.Add("Taboon", "1573m");
            nycfoodsprox.Add("Le Soleil", "1395m");
            nycfoodsprox.Add("Justino's Pizza", "1247m");
            nycfoodsprox.Add("Fogon's", "1174m");
        }

        //BUTTONS
        //ASCENDING
        private void OnClick_Ascend(object sender, EventArgs e)
        {
            //OrderBy Value
            if (App.locale == 0 && App.xlocale == 0)
            {
                winterParkBars = winterParkBars.OrderBy(i => i.Value).ToDictionary(i => i.Key, i => i.Value);
                barListView.ItemsSource = winterParkBars;
                winParkBarsProx.Clear();
            }
            else if (App.locale == 1 && App.xlocale == 0)
            {
                boiseBars = boiseBars.OrderBy(i => i.Value).ToDictionary(i => i.Key, i => i.Value);
                barListView.ItemsSource = boiseBars;
                boiseBarsProx.Clear();
            }
            else if (App.locale == 2 && App.xlocale == 0)
            {
                nycbars = nycbars.OrderBy(i => i.Value).ToDictionary(i => i.Key, i => i.Value);
                barListView.ItemsSource = nycbars;
                nycbarsprox.Clear();
            }
            else if (App.locale == 0 && App.xlocale == 2)
            {
                winterParkFoods = winterParkFoods.OrderBy(i => i.Value).ToDictionary(i => i.Key, i => i.Value);
                barListView.ItemsSource = winterParkFoods;
                winParkFoodsProx.Clear();
            }
            else if (App.locale == 1 && App.xlocale == 3)
            {
                boiseFoods = boiseFoods.OrderBy(i => i.Value).ToDictionary(i => i.Key, i => i.Value);
                barListView.ItemsSource = boiseFoods;
                boiseFoodsProx.Clear();
            }
            else if (App.locale == 2 && App.xlocale == 4)
            {
                nycfoods = nycfoods.OrderBy(i => i.Value).ToDictionary(i => i.Key, i => i.Value);
                barListView.ItemsSource = nycfoods;
                nycfoodsprox.Clear();
            }
        }
        //DESCENDING
        private void OnClick_Descend(object sender, EventArgs e)
        {
            //OrderBy Value
            if (App.locale == 0 && App.xlocale == 0)
            {
                winterParkBars = winterParkBars.OrderByDescending(i => i.Value).ToDictionary(i => i.Key, i => i.Value);
                barListView.ItemsSource = winterParkBars;
                winParkBarsProx.Clear();
            }
            else if (App.locale == 1 && App.xlocale == 0)
            {
                boiseBars = boiseBars.OrderByDescending(i => i.Value).ToDictionary(i => i.Key, i => i.Value);
                barListView.ItemsSource = boiseBars;
                boiseBarsProx.Clear();
            }
            else if (App.locale == 2 && App.xlocale == 0)
            {
                nycbars = nycbars.OrderByDescending(i => i.Value).ToDictionary(i => i.Key, i => i.Value);
                barListView.ItemsSource = nycbars;
                nycbarsprox.Clear();
            }
            else if (App.locale == 0 && App.xlocale == 2)
            {
                winterParkFoods = winterParkFoods.OrderByDescending(i => i.Value).ToDictionary(i => i.Key, i => i.Value);
                barListView.ItemsSource = winterParkFoods;
                winParkFoodsProx.Clear();
            }
            else if (App.locale == 1 && App.xlocale == 3)
            {
                boiseFoods = boiseFoods.OrderByDescending(i => i.Value).ToDictionary(i => i.Key, i => i.Value);
                barListView.ItemsSource = boiseFoods;
                boiseFoodsProx.Clear();
            }
            else if (App.locale == 2 && App.xlocale == 4)
            {
                nycfoods = nycfoods.OrderByDescending(i => i.Value).ToDictionary(i => i.Key, i => i.Value);
                barListView.ItemsSource = nycfoods;
                nycfoodsprox.Clear();
            }
        }
        //PROXIMITY
        private void OnClick_Proximity(object sender, EventArgs e)
        {
            if (App.locale == 0 && App.xlocale == 0)
            {
                BuildWinParkBarsProx();
                winParkBarsProx = winParkBarsProx.OrderBy(i => i.Value).ToDictionary(i => i.Key, i => i.Value);
                barListView.ItemsSource = winParkBarsProx;
            }
            else if (App.locale == 1 && App.xlocale == 0)
            {
                BuildBoiseBarsProx();
                boiseBarsProx = boiseBarsProx.OrderBy(i => i.Value).ToDictionary(i => i.Key, i => i.Value);
                barListView.ItemsSource = boiseBarsProx;
            }
            else if (App.locale == 0 && App.xlocale == 2)
            {
                BuildWinParkFoodsProx();
                winParkFoodsProx = winParkFoodsProx.OrderBy(i => i.Value).ToDictionary(i => i.Key, i => i.Value);
                barListView.ItemsSource = winParkFoodsProx;
            }
            else if (App.locale == 1 && App.xlocale == 3)
            {
                BuildBoiseFoodsProx();
                boiseFoodsProx = boiseFoodsProx.OrderBy(i => i.Value).ToDictionary(i => i.Key, i => i.Value);
                barListView.ItemsSource = boiseFoodsProx;
            }
            else if (App.locale == 2 && App.xlocale == 0)
            {
                BuildnycBarsProx();
                nycbarsprox = nycbarsprox.OrderBy(i => i.Value).ToDictionary(i => i.Key, i => i.Value);
                barListView.ItemsSource = nycbarsprox;
            }
            else if (App.locale == 2 && App.xlocale == 4)
            {
                BuildnycFoodProx();
                nycfoodsprox = nycfoodsprox.OrderBy(i => i.Value).ToDictionary(i => i.Key, i => i.Value);
                barListView.ItemsSource = nycfoodsprox;
            }
        }
        //RESTAURANTS
        private void OnClick_Restaurants(object sender, EventArgs e)
        {
            if (App.locale == 0)
            {
                if (restPressed == true) { return; }
                else
                {
                    hasbeen = false;
                    App.xlocale = 2;
                    BuildWinterParkFoods();
                    winterParkFoods = winterParkFoods.OrderBy(i => i.Value).ToDictionary(i => i.Key, i => i.Value);
                    barListView.ItemsSource = winterParkFoods;
                    restPressed = true;
                    barPressed = false;
                }
            }
            else if (App.locale == 1)
            {
                if (restPressed == true) { return; }
                else
                {
                    hasbeen = false;
                    App.xlocale = 3;
                    BuildBoiseFoods();
                    boiseFoods = boiseFoods.OrderBy(i => i.Value).ToDictionary(i => i.Key, i => i.Value);
                    barListView.ItemsSource = boiseFoods;
                    restPressed = true;
                    barPressed = false;
                }
            }
            else if (App.locale == 2)
            {
                if (restPressed == true) { return; }
                else
                {
                    hasbeen = false;
                    App.xlocale = 4;
                    Buildnycfoods();
                    nycfoods = nycfoods.OrderBy(i => i.Value).ToDictionary(i => i.Key, i => i.Value);
                    barListView.ItemsSource = nycfoods;
                    restPressed = true;
                    barPressed = false;
                }
            }
        }
        //BARS
        private void OnClick_Bars(object sender, EventArgs e)
        {
            GetUserLoc();
            if (App.locale == 0 && App.isMyLocation == 2)
            {
                if (barPressed == true) { return; }
                else if (barPressed == false)
                {
                    App.xlocale = 0;
                    BuildWinParkFoodsProx();
                    winterParkBars = winterParkBars.OrderByDescending(i => i.Value).ToDictionary(i => i.Key, i => i.Value);
                    barListView.ItemsSource = winterParkBars;
                    barPressed = true;
                    restPressed = false;
                }
            }
            else if (App.locale == 1 && App.isMyLocation == 1)
            {
                if (barPressed == true) { return; }
                else if(barPressed == false)
                {
                    App.xlocale = 0;
                    BuildBoiseBars();
                    boiseBars = boiseBars.OrderByDescending(i => i.Value).ToDictionary(i => i.Key, i => i.Value);
                    barListView.ItemsSource = boiseBars;
                    barPressed = true;
                    restPressed = false;
                }
            }
            else if (App.locale == 2 && App.isMyLocation == 3)
            {
                if (barPressed == true) { return; }
                else if(barPressed == false)
                {
                    App.xlocale = 0;
                    BuildnycBars();
                    nycbars = nycbars.OrderByDescending(i => i.Value).ToDictionary(i => i.Key, i => i.Value);
                    barListView.ItemsSource = nycbars;
                    barPressed = true;
                    restPressed = false;
                }
            }
        }
        //PEACE-OUT
        private async void OnClick_Peace(object sender, EventArgs e)
        {
            if (App.locale == 0 && App.xlocale == 0)
            {
                usercount = winterParkBars[temp];
                var excode = await DisplayAlert("Leaving So Soon?", "Would You Like To Close The App?", "Yes", "No");
                if (excode == true)
                {
                    winterParkBars[temp] = usercount - 1; // decrementing old bar // only if true
                    Thread.Sleep(3000);
                    Environment.FailFast("");
                }
                else { return; }
            }
            else if (App.locale == 0 && App.xlocale == 2)
            {
                usercount = winterParkFoods[temp];
                var excode = await DisplayAlert("Leaving So Soon?", "Would You Like To Close The App?", "Yes", "No");
                if (excode == true)
                {
                    winterParkFoods[temp] = usercount - 1; // decrementing old bar // only if true
                    Thread.Sleep(3000);
                    Environment.FailFast("");
                }
                else { return; }
            }
            else if (App.locale == 1 && App.xlocale == 0)
            {

                try { usercount = boiseBars[temp]; }
                catch { System.ArgumentNullException ex; }

                var excode = await DisplayAlert("Leaving So Soon?", "Would You Like To Close The App?", "Yes", "No");
                if (excode == true)
                {
                    try{boiseBars[temp] = usercount - 1; } // decrementing old bar // only if true
                    catch { System.ArgumentNullException ex; }
                    Thread.Sleep(3000);
                    Environment.FailFast("");
                }
                else { return; }
            }
            else if (App.locale == 1 && App.xlocale == 3)
            {
                usercount = boiseFoods[temp];
                var excode = await DisplayAlert("Leaving So Soon?", "Would You Like To Close The App?", "Yes", "No");
                if (excode == true)
                {
                    boiseFoods[temp] = usercount - 1; // decrementing old bar // only if true
                    Thread.Sleep(3000);
                    Environment.FailFast("");
                }
                else { return; }
            }
            else if (App.locale == 2 && App.xlocale == 0)
            {
                usercount = nycbars[temp];
                var excode = await DisplayAlert("Leaving So Soon?", "Would You Like To Close The App?", "Yes", "No");
                if (excode == true)
                {
                    nycbars[temp] = usercount - 1; // decrementing old bar // only if true
                    Thread.Sleep(3000);
                    Environment.FailFast("");
                }
                else { return; }
            }
            else if (App.locale == 2 && App.xlocale == 4)
            {
                usercount = nycfoods[temp];
                var excode = await DisplayAlert("Leaving So Soon?", "Would You Like To Close The App?", "Yes", "No");
                if (excode == true)
                {
                    nycfoods[temp] = usercount - 1; // decrementing old bar // only if true
                    Thread.Sleep(3000);
                    Environment.FailFast("");
                }
                else { return; }
            }
        }
    }
}