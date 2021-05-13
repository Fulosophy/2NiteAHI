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
        public Dictionary<string, double> winParkBarsProx = new Dictionary<string, double>();  
        public Dictionary<string, int> winterParkFoods = new Dictionary<string, int>();
        public Dictionary<string, double> winParkFoodsProx = new Dictionary<string, double>();

        // 2nd Location 
        public Dictionary<string, int> boiseBars = new Dictionary<string, int>();
        public Dictionary<string, double> boiseBarsProx = new Dictionary<string, double>();
        public Dictionary<string, int> boiseFoods = new Dictionary<string, int>();
        public Dictionary<string, double> boiseFoodsProx = new Dictionary<string, double>();

        // 3rd Location Manhattan
        public Dictionary<string, int> nycbars = new Dictionary<string, int>();
        public Dictionary<string, string> nycbarsprox = new Dictionary<string, string>();
        public Dictionary<string, int> nycfoods = new Dictionary<string, int>();
        public Dictionary<string, string> nycfoodsprox = new Dictionary<string, string>();

        // 4th Location Miami Beach 
        public Dictionary<string, int> miamiBeachBars = new Dictionary<string, int>();
        public Dictionary<string, double> miamiBeachBarsProx = new Dictionary<string, double>();
        public Dictionary<string, int> miamiBeachFoods = new Dictionary<string, int>();
        public Dictionary<string, double> miamiBeachFoodsProx = new Dictionary<string, double>();

        //VARIABLES
        private string myloc;
        private int locale;
        private int xlocale = 0;
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
            BindingContext = this;
            GetUserLoc(); 

            //LISTVIEW SELECTION OPERATIONS
            barListView.ItemSelected += async (object sender, SelectedItemChangedEventArgs e) =>
             {
                 //WINTER PARK INITIAL SELECT
                 if (hasbeen == false && locale == 0 && xlocale == 0)
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
                         RefreshCommand.Execute(barListView.ItemsSource = winterParkBars);

                     }
                     else { return; }
                 }
                 //WINTER PARK NEXT SELECT
                 else if (hasbeen == true && locale == 0 && xlocale == 0)
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
                 else if (hasbeen == false && locale == 1 && xlocale == 0)
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
                 else if (hasbeen == true && locale == 1 && xlocale == 0)
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
                 else if (hasbeen == false && locale == 0 && xlocale == 2)
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
                 else if (hasbeen == true && locale == 0 && xlocale == 2)
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
                 else if (hasbeen == false && locale == 1 && xlocale == 3)
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
                 else if (hasbeen == true && locale == 1 && xlocale == 3)
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
                else if (hasbeen == false && locale == 2 && xlocale == 0)
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


                     // 
                 }
                 // Nyc NEXT SELECT bar
                 else if (hasbeen == true && locale == 2 && xlocale == 0)
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
                 else if (hasbeen == false && locale == 2 && xlocale == 4)
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
                 else if (hasbeen == true && locale == 2 && xlocale == 4)
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
                 // Miami Beach INITIAL SELECT Bars
                 else if (hasbeen == false && locale == 2 && xlocale == 0)
                 {
                     var ans = await DisplayAlert("Yo! Checking in?", "Have fun!", "Yes", "Cancel");
                     if (ans == true) // pressing OK
                     {
                         selecteditem = e.SelectedItem.ToString(); // Changing event handler to string
                         selecteditem = selecteditem.Trim('[', ']'); // triming brackets

                         string[] splititems = selecteditem.Split(','); // delim comma


                         usercount = miamiBeachBars[splititems[0]]; // Grabbing current user count of bar/rest
                         miamiBeachBars[splititems[0]] = usercount + 1; // increment from current bar/rest count
                         temp = splititems[0]; // Saving the selected data for decrements

                         hasbeen = true;
                         RefreshCommand.Execute(barListView.ItemsSource = miamiBeachBars);

                     }
                     else { return; }


                     // 
                 }
                 // Miami Beach NEXT SELECT bar
                 else if (hasbeen == true && locale == 2 && xlocale == 0)
                 {
                     var anss = await DisplayAlert("Changing bars???", "Would You Like To Check In?", "Yes", "Cancel");
                     if (anss == true) // pressing OK
                     {
                         usercount = miamiBeachBars[temp];
                         miamiBeachBars[temp] = usercount - 1; // decrementing old bar

                         selecteditem = e.SelectedItem.ToString(); // Changing event handler to string
                         selecteditem = selecteditem.Trim('[', ']'); // triming brackets
                         string[] splititems = selecteditem.Split(','); // delim comma

                         usercount = miamiBeachBars[splititems[0]]; // Grabbing current user count of bar/rest
                         miamiBeachBars[splititems[0]] = usercount + 1; // Incrementing CHANGED bar
                         temp = splititems[0];
                         RefreshCommand.Execute(barListView.ItemsSource = miamiBeachBars);
                     }
                     else { return; }
                 }
                 // Miami Beach food intial select 
                 else if (hasbeen == false && locale == 2 && xlocale == 4)
                 {
                     var ansb = await DisplayAlert("Yo! Checking in?", "Have fun!", "Yes", "Cancel");
                     if (ansb == true)
                     {
                         selecteditem = e.SelectedItem.ToString();
                         selecteditem = selecteditem.Trim('[', ']');
                         string[] splititems = selecteditem.Split(',');

                         usercount = miamiBeachFoods[splititems[0]];
                         miamiBeachFoods[splititems[0]] = usercount + 1;
                         temp = splititems[0];

                         hasbeen = true;
                         RefreshCommand.Execute(barListView.ItemsSource = miamiBeachFoods);
                     }
                     else { return; }
                 }
                 // Miami Beach food next select
                 else if (hasbeen == true && locale == 2 && xlocale == 4)
                 {
                     var ansbb = await DisplayAlert("Yo New Restaurant?", "Would You Like To Check In?", "Yes", "Cancel");
                     if (ansbb == true)
                     {
                         usercount = miamiBeachFoods[temp];
                         miamiBeachFoods[temp] = usercount - 1;

                         selecteditem = e.SelectedItem.ToString();
                         selecteditem = selecteditem.Trim('[', ']');
                         string[] splititems = selecteditem.Split(',');

                         usercount = miamiBeachFoods[splititems[0]];
                         miamiBeachFoods[splititems[0]] = usercount + 1;
                         temp = splititems[0];
                         RefreshCommand.Execute(barListView.ItemsSource = miamiBeachFoods);
                     }
                     else { return; }
                 }

             };
        }

        async private void ToolbarItem_Clicked(object sender, EventArgs e) { await Navigation.PushAsync(new Settings()); }

        //LOCATION FUNCTIONS
        async private void GetUserLoc()
        {
            var location = await Geolocation.GetLocationAsync(new GeolocationRequest(GeolocationAccuracy.Default, TimeSpan.FromMinutes(1)));
            var GetAddy = await Geocoding.GetPlacemarksAsync(location.Latitude, location.Longitude); // Grabbing the users location details as a placemark.
            var addy = GetAddy?.FirstOrDefault();

            MyLocation = $"{addy.Locality},  {addy.AdminArea}"; // Grabs the users current locations Address-Town and Address-State **Only works in USA**  

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
            else if (addy.Locality == "New York")
            {
                locale = 2;
                BuildnycBars();
                nycbars = nycbars.OrderByDescending(i => i.Value).ToDictionary(i => i.Key, i => i.Value);
                barListView.ItemsSource = nycbars;
               
                
            }
            else if (addy.Locality == "Miami Beach")
            {
                locale = 3;
                BuildMiamiBeachBars();
                miamiBeachBars = miamiBeachBars.OrderByDescending(i => i.Value).ToDictionary(i => i.Key, i => i.Value);
                barListView.ItemsSource = miamiBeachBars;


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
                    if (locale == 0 && xlocale == 0)
                    {
                        barListView.ItemsSource = null; // resets the list back to null 
                        barListView.ItemsSource = winterParkBars; // refactors updated list
                        IsSoRefreshing = false;
                        winParkBarsProx.Clear();
                    }
                    else if (locale == 1 && xlocale == 0)
                    {
                        barListView.ItemsSource = null; // resets the list back to null 
                        barListView.ItemsSource = boiseBars; // refactors updated list
                        IsSoRefreshing = false;
                        boiseBarsProx.Clear();
                    }
                    else if (locale == 2 && xlocale == 0)
                    {
                        barListView.ItemsSource = null; // resets the list back to null 
                        barListView.ItemsSource = nycbars; // refactors updated list
                        IsSoRefreshing = false;
                        nycbarsprox.Clear();
                    }
                    else if (locale == 3 && xlocale == 0)
                    {
                        barListView.ItemsSource = null; // resets the list back to null 
                        barListView.ItemsSource = miamiBeachBars; // refactors updated list
                        IsSoRefreshing = false;
                        miamiBeachBarsProx.Clear();
                    }
                    else if (locale == 0 && xlocale == 2)
                    {
                        barListView.ItemsSource = null; // resets the list back to null 
                        barListView.ItemsSource = winterParkFoods; // refactors updated list
                        IsSoRefreshing = false;
                        winParkFoodsProx.Clear();
                    }
                    else if (locale == 1 && xlocale == 3)
                    {
                        barListView.ItemsSource = null; // resets the list back to null 
                        barListView.ItemsSource = boiseFoods; // refactors updated list
                        IsSoRefreshing = false;
                        boiseFoodsProx.Clear();
                    }
                   
                    else if (locale == 2 && xlocale == 4)
                    {
                        barListView.ItemsSource = null; // resets the list back to null 
                        barListView.ItemsSource = nycfoods; // refactors updated list
                        IsSoRefreshing = false;
                        nycfoodsprox.Clear();
                    }
                    else if (locale == 3 && xlocale == 5)
                    {
                        barListView.ItemsSource = null; // resets the list back to null 
                        barListView.ItemsSource = miamiBeachFoods; // refactors updated list
                        IsSoRefreshing = false;
                        miamiBeachFoodsProx.Clear();
                    }



                });
            }
        }

        //DICTIONARIES
            //BARS
        private void BuildWinterParkBars()
        {
            winterParkBars.Clear();
            winterParkBars.Add("The Brass Tap", usercount);
            winterParkBars.Add("Tequila Lounge Club", usercount); 
            winterParkBars.Add("Muldoons Saloon", usercount);
            winterParkBars.Add("The Geek Easy", usercount);
            winterParkBars.Add("The Haven At Forsyth", usercount);
            winterParkBars.Add("Debbie's Bar", usercount); 
            winterParkBars.Add("Ain't Misbehavin'", usercount);
            winterParkBars.Add("Rock and Brews", usercount);
            winterParkBars.Add("Oviedo Brewing Company", usercount);
            winterParkBars.Add("The Green Bar", usercount);
            winterParkBars.Add("The Green Parrot", usercount); 
            winterParkBars.Add("Miller's Ale House", usercount); 
            winterParkBars.Add("Devaney's Sports Pub", usercount);
            winterParkBars.Add("Majijis Hookah Lounge", usercount);
            winterParkBars.Add("Tactical Brewing Co.", usercount); 
            winterParkBars.Add("The Next", usercount); 
            winterParkBars.Add("Admiral Cigar Club", usercount);
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
        private void BuildnycBarsProx()
        {
            nycbarsprox.Clear();
            nycbarsprox.Add("The Jeffery Craft", "1523 meters away!");
            nycbarsprox.Add("Mercury Bar West", "1907 meters away!");
            nycbarsprox.Add("Down The Road Sports Bar", "1735 meters away!");
            nycbarsprox.Add("The House Of Brews", "1804 meters away!");
            nycbarsprox.Add("Deacon Brodie's Tavern", "1877 meters away!");
            nycbarsprox.Add("Wine Escape", "2035 meters away!") ;
            nycbarsprox.Add("Dalton's Bar & Grill", "2085 meters away!");
            nycbarsprox.Add("Dave's Tavern", "2223 meters away!");
            nycbarsprox.Add("Scruffy Duffy's Bar", "2048 meters away!");
            nycbarsprox.Add("The Press Lounge", "2028 meters away!");
            nycbarsprox.Add("Mom's Kitchen & Bar", "1709 meters away!");

        }
        private void BuildnycFoodProx()
        {
            nycfoodsprox.Clear();
            nycfoodsprox.Add("Authentic NYC Street Food", "2172 meters away! ");
            nycfoodsprox.Add("Cantina Rooftop", "2014 meters away!");
            nycfoodsprox.Add("Mamasitas Grill", "1314 meters away!");
            nycfoodsprox.Add("Cook Unity", "1803 meters away!");
            nycfoodsprox.Add("Burger Shot Beer", "1675 meters away!");
            nycfoodsprox.Add("Taboon", "1573 meters away! ");
            nycfoodsprox.Add("Le Soleil", "1395 meters away!");
            nycfoodsprox.Add("Justino's Pizza", "1247 meters away!");
            nycfoodsprox.Add("Fogon's", "1174 meters away!");



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
           winParkBarsProx.Add("The Geek Easy", 0.6719);
           winParkBarsProx.Add("The Haven At Forsyth", 0.9050);
           winParkBarsProx.Add("Debbie's Bar", 3.7190);
           winParkBarsProx.Add("Ain't Misbehavin'", 3.2320);
           winParkBarsProx.Add("Miller's Ale House", 0.2871);
           winParkBarsProx.Add("Devaney's Sports Pub", 1.6930);
           winParkBarsProx.Add("The Nest", 3.9230);
           winParkBarsProx.Add("Admiral Cigar Club", 3.9710);
           winParkBarsProx.Add("Tactical Brewing Co.", 4.0560);
           winParkBarsProx.Add("The Brass Tap", 7.0210);
           winParkBarsProx.Add("Redlight Redlight", 5.5550);
           winParkBarsProx.Add("Tequila Lounge Club", 1.4720);
           winParkBarsProx.Add("Muldoons Saloon", 2.0060);
           winParkBarsProx.Add("Majijis Hookah Lounge", 1.5520);
           winParkBarsProx.Add("Rock and Brews", 7.7530);
           winParkBarsProx.Add("Oviedo Brewing Company", 9.9090);
           winParkBarsProx.Add("The Green Bar", 7.1810);
           winParkBarsProx.Add("The Green Parrot", 7.6660150);
        }
        private void BuildBoiseBarsProx()
        {
            boiseBarsProx.Clear();
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
            boiseFoods.Add("Owyhee Tavern	Steakhouse", rng.Next() % 150);
            boiseFoods.Add("Bombay Grill", rng.Next() % 150);
            boiseFoods.Add("Txikitea", rng.Next() % 150);
            boiseFoods.Add("KIN", rng.Next() % 150);
            boiseFoods.Add("Main Street Deli", rng.Next() % 150);
            boiseFoods.Add("Fork", rng.Next() % 150);
            boiseFoods.Add("Even Stevens Sandwiches", rng.Next() % 150);
            boiseFoods.Add("Lucky Fins Seafood Grill", rng.Next() % 150);
        }
        private void BuildWinParkFoodsProx()
        {
            winParkFoodsProx.Clear();
            winParkFoodsProx.Add(" Chili's Bar & Grill ", 0.5835);
            winParkFoodsProx.Add("Firehouse Subs", 0.5540);
            winParkFoodsProx.Add("Arooga's", 0.5303);
            winParkFoodsProx.Add("Chewy Boba", 0.6737);
            winParkFoodsProx.Add("Steak 'n Shake", 0.8633);
            winParkFoodsProx.Add("El Pueblo Mexicqan", 1.4230);
            winParkFoodsProx.Add("Luke's Kitchen and Bar", 6.7470);
            winParkFoodsProx.Add("Buffalo Wild WIngs", 6.2890);
            winParkFoodsProx.Add("BJ's Restaurant and Brewhouse", 9.1900);
            winParkFoodsProx.Add("Don Julio Mexican Kitchen and Tequila Bar", 7.1960);
            winParkFoodsProx.Add("Texas Roadhous", 4.8070);
            winParkFoodsProx.Add("Starbucks", 1.2130);
            winParkFoodsProx.Add("Sonny's BBQ", 0.1739);
            winParkFoodsProx.Add("Gator's Dockside", 3.9420);
            winParkFoodsProx.Add("Cork & Plate", 4.0180);
            winParkFoodsProx.Add("La Placita 19'", 0.3135);
            winParkFoodsProx.Add("Rincon Latino", 0.4204);
            winParkFoodsProx.Add("Fire on the Bayou", 6.2520);
            winParkFoodsProx.Add("Thirsty Gator", 1.9780);
        }
        private void BuildBoiseFoodsProx()
        {
            boiseFoodsProx.Clear();
            boiseFoodsProx.Add("Del Taco",0.9709);
            boiseFoodsProx.Add("Meraki Greek Street Food",0.2481);
            boiseFoodsProx.Add("Trillium Restaurant",0.1435);
            boiseFoodsProx.Add("Boise Fry Company",0.3768);
            boiseFoodsProx.Add("Bittercreek Alehouse",0.3981);
            boiseFoodsProx.Add("Eureka",0.4533);
            boiseFoodsProx.Add("Chandlers Prime Steaks & Seafood",0.4253);
            boiseFoodsProx.Add("Manfred’s Kitchen",0.6363);
            boiseFoodsProx.Add("Owyhee Tavern	Steakhouse",0.6258);
            boiseFoodsProx.Add("Bombay Grill",0.4976);
            boiseFoodsProx.Add("Txikitea",0.9841);
            boiseFoodsProx.Add("KIN",0.4279);
            boiseFoodsProx.Add("Main Street Deli",0.4157);
            boiseFoodsProx.Add("Fork",0.3662);
            boiseFoodsProx.Add("Even Stevens Sandwiches",0.4837);
            boiseFoodsProx.Add("Lucky Fins Seafood Grill",0.2793);
        }
        private void BuildMiamiBeachBars()
        {
            miamiBeachBars.Add("Cafe Prima Pasta", rng.Next() % 150);
            miamiBeachBars.Add("The Tarern", rng.Next() % 150);
            miamiBeachBars.Add("Bolivar Resturant Bar Lounge", rng.Next() % 150);
            miamiBeachBars.Add("Maxine's Bistro and Bar", rng.Next() % 150);
            miamiBeachBars.Add("TGI Fridays", rng.Next() % 150);
            miamiBeachBars.Add("The Place", rng.Next() % 150);
            miamiBeachBars.Add("Finnegan's Way", rng.Next() % 150);
            miamiBeachBars.Add("Monty's Sunset", rng.Next() % 150);
            miamiBeachBars.Add("Norman's", rng.Next() % 150);
            miamiBeachBars.Add("Moreno's Cuba", rng.Next() % 150);
            miamiBeachBars.Add("Yard House", rng.Next() % 150);
            miamiBeachBars.Add("27 Restaurant & Bar", rng.Next() % 150);
            miamiBeachBars.Add("Broken Shaker", rng.Next() % 150);
            miamiBeachBars.Add("Bleau Bar", rng.Next() % 150);
            miamiBeachBars.Add("Wet Willie's", rng.Next() % 150);
            miamiBeachBars.Add("Segafredo L'Originale", rng.Next() % 150);
            miamiBeachBars.Add("Sweet Liberty Drinks & Supply Company", rng.Next() % 150);
            miamiBeachBars.Add("Clevelander Bar", rng.Next() % 150);
            miamiBeachBars.Add("Nautilus Bar & Grill ", rng.Next() % 150);
            miamiBeachBars.Add("Blue Ribbon Sushi Bar & Grill", rng.Next() % 150);
            miamiBeachBars.Add("Mac’s Club Deuce", rng.Next() % 150);
            miamiBeachBars.Add("Abbey Brewing Company", rng.Next() % 150);
            miamiBeachBars.Add("Twist", rng.Next() % 150);
            miamiBeachBars.Add("Palace Bar LLC", rng.Next() % 150);
            miamiBeachBars.Add("Kansas Bar & Grill", rng.Next() % 150);
            miamiBeachBars.Add("Playwright Irish Pub", rng.Next() % 150);
            miamiBeachBars.Add("Tavern at Oceanside", rng.Next() % 150);
            miamiBeachBars.Add("Venezia Grill Pizzeria Bar", rng.Next() % 150);
            miamiBeachBars.Add("Espanola Cigar Bar", rng.Next() % 150);
            miamiBeachBars.Add("The Regent Cocktail Club", rng.Next() % 150);
            miamiBeachBars.Add("Vanilla Cafe", rng.Next() % 150);
            miamiBeachBars.Add("Bungalow By The Sea", rng.Next() % 150);
            miamiBeachBars.Add("Bar Collins", rng.Next() % 150);
            miamiBeachBars.Add("Living Room Lounge", rng.Next() % 150);
            miamiBeachBars.Add("Tantra", rng.Next() % 150);
            miamiBeachBars.Add("High Tide", rng.Next() % 150);
            miamiBeachBars.Add("Mike's At Venetia", rng.Next() % 150);
            miamiBeachBars.Add("Lost Weekend", rng.Next() % 150);
            miamiBeachBars.Add("Bay Club", rng.Next() % 150);
            miamiBeachBars.Add("Drunk Bitch", rng.Next() % 150);
            miamiBeachBars.Add("South Pointe Tavern", rng.Next() % 150);
            miamiBeachBars.Add("Sky Bar At Shore Club South Beach", rng.Next() % 150);
            miamiBeachBars.Add("Kill Your Idol ", rng.Next() % 150);
            miamiBeachBars.Add("Miami Clubs", rng.Next() % 150);
            miamiBeachBars.Add("Mynt Lounge", rng.Next() % 150);
            miamiBeachBars.Add("Hyde Beach", rng.Next() % 150);
            miamiBeachBars.Add("Riki Tiki Bar and Grill", rng.Next() % 150);
            miamiBeachBars.Add("El Grito", rng.Next() % 150);
            miamiBeachBars.Add("Margarita Beach Club", rng.Next() % 150);
            miamiBeachBars.Add("Palace", rng.Next() % 150);

        }

        private void BuildMiamiBeachBarsProx()
        {
            miamiBeachBarsProx.Add("Cafe Prima Pasta",5.2);
            miamiBeachBarsProx.Add("The Tarern", 0.130);
            miamiBeachBarsProx.Add("Bolivar Resturant Bar Lounge", 4.3);
            miamiBeachBarsProx.Add("Maxine's Bistro and Bar", 4.3);
            miamiBeachBarsProx.Add("TGI Fridays", 4.6);
            miamiBeachBarsProx.Add("The Place", 4.3);
            miamiBeachBarsProx.Add("Finnegan's Way", 3.4);
            miamiBeachBarsProx.Add("Monty's Sunset", 6.2);
            miamiBeachBarsProx.Add("Norman's", 4.6);
            miamiBeachBarsProx.Add("Moreno's Cuba", 2.2);
            miamiBeachBarsProx.Add("Yard House", 4.0);
            miamiBeachBarsProx.Add("27 Restaurant & Bar", 1.0);
            miamiBeachBarsProx.Add("Broken Shaker", 1.0);
            miamiBeachBarsProx.Add("Bleau Bar", 0.750);
            miamiBeachBarsProx.Add("Wet Willie's", 4.4);
            miamiBeachBarsProx.Add("Segafredo L'Originale", 4.1);
            miamiBeachBarsProx.Add("Sweet Liberty Drinks & Supply Company", 2.2);
            miamiBeachBarsProx.Add("Clevelander Bar", 3.9);
            miamiBeachBarsProx.Add("Nautilus Bar & Grill", 6.4);
            miamiBeachBarsProx.Add("Blue Ribbon Sushi Bar & Grill", 2.2);
            miamiBeachBarsProx.Add("Mac’s Club Deuce", 3.4);
            miamiBeachBarsProx.Add("Abbey Brewing Company", 4.3);
            miamiBeachBarsProx.Add("Twist", 4.0);
            miamiBeachBarsProx.Add("Palace Bar LLC", 3.8);
            miamiBeachBarsProx.Add("Kansas Bar & Grill", 3.1);
            miamiBeachBarsProx.Add("Harat's Pub", 3.6);
            miamiBeachBarsProx.Add("Tavern at Oceanside", 4.8);
            miamiBeachBarsProx.Add("Venezia Grill Pizzeria Bar", 4.4);
            miamiBeachBarsProx.Add("Espanola Cigar Bar", 3.2);
            miamiBeachBarsProx.Add("The Regent Cocktail Club", 2.6);
            miamiBeachBarsProx.Add("Bungalow By The Sea", 0.77);
            miamiBeachBarsProx.Add("Bar Collins", 2.9);
            miamiBeachBarsProx.Add("Living Room Lounge", 0.500);
            miamiBeachBarsProx.Add("High Tide", 2.6);
            miamiBeachBarsProx.Add("Lost Weekend", 3.2);
            miamiBeachBarsProx.Add("Bay Club", 4.1);
            miamiBeachBarsProx.Add("Bacon Bitch", 4.0);
            miamiBeachBarsProx.Add("South Pointe Tavern", 7.0);
            miamiBeachBarsProx.Add("Sky Bar At Shore Club South Beach", 2.2);
            miamiBeachBarsProx.Add("Kill Your Idol ", 3.2);
            miamiBeachBarsProx.Add("Miami Clubs", 2.9);
            miamiBeachBarsProx.Add("Mynt Lounge", 2.2);
            miamiBeachBarsProx.Add("Hyde Beach", 2.7);
            miamiBeachBarsProx.Add("Riki Tiki Bar and Grill", 2.5);
            miamiBeachBarsProx.Add("Palace", 3.8);
        }

        private void BuildMiamiBeachFood()
        {

            miamiBeachFoods.Add("Cvi.Che 105", rng.Next() % 150);
            miamiBeachFoods.Add("Pane & Vino La Trattoria", rng.Next() % 150);
            miamiBeachFoods.Add("La Ventana Colombian Restaurant", rng.Next() % 150);
            miamiBeachFoods.Add("Santorini by Georgios", rng.Next() % 150);
            miamiBeachFoods.Add("Joe's Takeaway", rng.Next() % 150);
            miamiBeachFoods.Add("11th Street Diner", rng.Next() % 150);
            miamiBeachFoods.Add("Yardbird Southern Table & Bar", rng.Next() % 150);
            miamiBeachFoods.Add("La Sandwicherie", rng.Next() % 150);
            miamiBeachFoods.Add("Shake Shack", rng.Next() % 150);
            miamiBeachFoods.Add("La Cerveceria de Barrio Miami", rng.Next() % 150);
            miamiBeachFoods.Add("Cafe Prima Pasta", rng.Next() % 150);
            miamiBeachFoods.Add("Dolce Italian", rng.Next() % 150);
            miamiBeachFoods.Add("Hakkasan Miami", rng.Next() % 150);
            miamiBeachFoods.Add("A Fish Called Avalon", rng.Next() % 150);
            miamiBeachFoods.Add("The Tavern", rng.Next() % 150);
            miamiBeachFoods.Add("Churros Manolo Miami", rng.Next() % 150);
            miamiBeachFoods.Add("Mercato Della Pescheria Espanola Way", rng.Next() % 150);
            miamiBeachFoods.Add("Ola", rng.Next() % 150);
            miamiBeachFoods.Add("Katsuya South Beach", rng.Next() % 150);
            miamiBeachFoods.Add("Cleo South Beach", rng.Next() % 150);
            miamiBeachFoods.Add("il Pastaiolo", rng.Next() % 150);
            miamiBeachFoods.Add("Otentic Fresh Food Restaurant", rng.Next() % 150);
            miamiBeachFoods.Add("Scarpetta", rng.Next() % 150);
            miamiBeachFoods.Add("Five Guys", rng.Next() % 150);
            miamiBeachFoods.Add("Fogo de Chao Brazilian Steakhouse", rng.Next() % 150);
            miamiBeachFoods.Add("Il Bolognese on Ocean", rng.Next() % 150);
            miamiBeachFoods.Add("Spris Pizza", rng.Next() % 150);
            miamiBeachFoods.Add("Bolivar Restaurant Bar Lounge", rng.Next() % 150);
            miamiBeachFoods.Add("Osteria Del Teatro", rng.Next() % 150);
            miamiBeachFoods.Add("On Ocean 7 Cafe", rng.Next() % 150);
            miamiBeachFoods.Add("The Forge", rng.Next() % 150);
            miamiBeachFoods.Add("Spiga Ristorante Italiano", rng.Next() % 150);
            miamiBeachFoods.Add("Estiatorio Milos", rng.Next() % 150);
            miamiBeachFoods.Add("Havana 1957", rng.Next() % 150);
            miamiBeachFoods.Add("Texas de Brazil", rng.Next() % 150);
            miamiBeachFoods.Add("Chalan On The Beach", rng.Next() % 150);
            miamiBeachFoods.Add("Matador Room", rng.Next() % 150);
            miamiBeachFoods.Add("La Côte", rng.Next() % 150);
            miamiBeachFoods.Add("Bella Cuba", rng.Next() % 150);
            miamiBeachFoods.Add("Juvia", rng.Next() % 150);
            miamiBeachFoods.Add("Italymania Restaurant Pizzeria", rng.Next() % 150);
            miamiBeachFoods.Add("Pompei Osteria Napoletana", rng.Next() % 150);
            miamiBeachFoods.Add("La Locanda", rng.Next() % 150);
            miamiBeachFoods.Add("News Cafe", rng.Next() % 150);
            miamiBeachFoods.Add("Smith and Wollensky", rng.Next() % 150);
            miamiBeachFoods.Add("Makoto", rng.Next() % 150);


        }


        private void BuildMiamiBeachFoodsProx()
        {
            miamiBeachFoodsProx.Clear();
            miamiBeachFoodsProx.Add("Cvi.Che 105", 14.1);
            miamiBeachFoodsProx.Add("Pane & Vino La Trattoria", 3.2);
            miamiBeachFoodsProx.Add("La Ventana Colombian Restaurant", 5.0);
            miamiBeachFoodsProx.Add("Santorini by Georgios", 5.2);
            miamiBeachFoodsProx.Add("Joe's Takeaway", 6.4);
            miamiBeachFoodsProx.Add("11th Street Diner", 3.8);
            miamiBeachFoodsProx.Add("Yardbird Southern Table & Bar", 4.4);
            miamiBeachFoodsProx.Add("La Sandwicherie", 3.4);
            miamiBeachFoodsProx.Add("Shake Shack", 3.4);
            miamiBeachFoodsProx.Add("La Cerveceria de Barrio Miami", 3.9);
            miamiBeachFoodsProx.Add("Cafe Prima Pasta", 5.2);
            miamiBeachFoodsProx.Add("Dolce Italian", 2.6);
            miamiBeachFoodsProx.Add("Hakkasan Miami", 0.800);
            miamiBeachFoodsProx.Add("A Fish Called Avalon", 4.3);
            miamiBeachFoodsProx.Add("The Tavern", 0.130);
            miamiBeachFoodsProx.Add("Churros Manolo Miami", 5.3);
            miamiBeachFoodsProx.Add("Mercato Della Pescheria Espanola Way", 3.2);
            miamiBeachFoodsProx.Add("Ola", 1.7);
            miamiBeachFoodsProx.Add("Katsuya South Beach", 2.6);
            miamiBeachFoodsProx.Add("Cleo South Beach", 2.4);
            miamiBeachFoodsProx.Add("il Pastaiolo", 3.6);
            miamiBeachFoodsProx.Add("Otentic Fresh Food Restaurant", 4.6);
            miamiBeachFoodsProx.Add("Scarpetta", 0.600);
            miamiBeachFoodsProx.Add("Five Guys", 3.2);
            miamiBeachFoodsProx.Add("Fogo de Chao Brazilian Steakhouse", 6.6);
            miamiBeachFoodsProx.Add("Il Bolognese on Ocean", 3.4);
            miamiBeachFoodsProx.Add("Spris Pizza", 3.8);
            miamiBeachFoodsProx.Add("Bolivar Restaurant Bar Lounge", 4.3);
            miamiBeachFoodsProx.Add("Osteria Del Teatro", 3.6);
            miamiBeachFoodsProx.Add("On Ocean 7 Cafe", 4.3);
            miamiBeachFoodsProx.Add("The Forge", 0.700);
            miamiBeachFoodsProx.Add("Spiga Ristorante Italiano", 3.5);
            miamiBeachFoodsProx.Add("Estiatorio Milos", 5.3);
            miamiBeachFoodsProx.Add("Havana 1957", 3.2);
            miamiBeachFoodsProx.Add("Texas de Brazil", 6.2);
            miamiBeachFoodsProx.Add("Chalan On The Beach", 3.0);
            miamiBeachFoodsProx.Add("Matador Room", 0.900);
            miamiBeachFoodsProx.Add("La Côte", 0.700);
            miamiBeachFoodsProx.Add("Bella Cuba", 2.9);
            miamiBeachFoodsProx.Add("Juvia", 4.1);
            miamiBeachFoodsProx.Add("Italymania Restaurant Pizzeria", 3.6);
            miamiBeachFoodsProx.Add("Pompei Osteria Napoletana", 3.4);
            miamiBeachFoodsProx.Add("La Locanda", 4.8);
            miamiBeachFoodsProx.Add("News Cafe", 4.4);
            miamiBeachFoodsProx.Add("Smith and Wollensky", 7.4);
            miamiBeachFoodsProx.Add("Makoto", 9.1);
        }


        //BUTTONS
        //ASCENDING
        private void OnClick_Ascend(object sender, EventArgs e)
        {
            //OrderBy Value
            if (locale == 0 && xlocale == 0)
            {
                winterParkBars = winterParkBars.OrderBy(i => i.Value).ToDictionary(i => i.Key, i => i.Value);
                barListView.ItemsSource = winterParkBars;
                winParkBarsProx.Clear();
            }
            else if (locale == 1 && xlocale == 0)
            {
                boiseBars = boiseBars.OrderBy(i => i.Value).ToDictionary(i => i.Key, i => i.Value);
                barListView.ItemsSource = boiseBars;
                boiseBarsProx.Clear();
            }
            else if (locale == 2 && xlocale == 0)
            {
                nycbars = nycbars.OrderBy(i => i.Value).ToDictionary(i => i.Key, i => i.Value);
                barListView.ItemsSource = nycbars;
                nycbarsprox.Clear();
            }
            else if (locale == 0 && xlocale == 2)
            {
                winterParkFoods = winterParkFoods.OrderBy(i=>i.Value).ToDictionary(i=> i.Key, i => i.Value);
                barListView.ItemsSource = winterParkFoods;
                winParkFoodsProx.Clear();
            }
            else if (locale == 1 && xlocale == 3)
            {
                boiseFoods = boiseFoods.OrderBy(i=>i.Value).ToDictionary(i=>i.Key, i=>i.Value);
                barListView.ItemsSource = boiseFoods;
                boiseFoodsProx.Clear();
            }
            else if(locale == 2 && xlocale == 4)
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
            if (locale == 0 && xlocale == 0)
            {
                winterParkBars = winterParkBars.OrderByDescending(i => i.Value).ToDictionary(i => i.Key, i => i.Value);
                barListView.ItemsSource = winterParkBars;
                winParkBarsProx.Clear();
            }
            else if (locale == 1 && xlocale == 0)
            {
                boiseBars = boiseBars.OrderByDescending(i => i.Value).ToDictionary(i => i.Key, i => i.Value);
                barListView.ItemsSource = boiseBars;
                boiseBarsProx.Clear();
            }
            else if (locale == 2 && xlocale == 0)
            {
                nycbars = nycbars.OrderByDescending(i => i.Value).ToDictionary(i => i.Key, i => i.Value);
                barListView.ItemsSource = nycbars;
                nycbarsprox.Clear();
            }
            else if (locale == 0 && xlocale == 2)
            {
                winterParkFoods = winterParkFoods.OrderByDescending(i => i.Value).ToDictionary(i => i.Key, i => i.Value);
                barListView.ItemsSource = winterParkFoods;
                winParkFoodsProx.Clear();
            }
            else if (locale == 1 && xlocale == 3)
            {
                boiseFoods = boiseFoods.OrderByDescending(i => i.Value).ToDictionary(i => i.Key, i => i.Value);
                barListView.ItemsSource = boiseFoods;
                boiseFoodsProx.Clear();
            }
           
            else if (locale == 2 && xlocale == 4)
            {
                nycfoods = nycfoods.OrderByDescending(i => i.Value).ToDictionary(i => i.Key, i => i.Value);
                barListView.ItemsSource = nycfoods;
                nycfoodsprox.Clear();
            }
           
        }
            //PROXIMITY
        private void OnClick_Proximity(object sender, EventArgs e)
        {
            if (locale == 0 && xlocale == 0)
            {
                BuildWinParkBarsProx();
                winParkBarsProx = winParkBarsProx.OrderBy(i => i.Value).ToDictionary(i => i.Key, i => i.Value);
                barListView.ItemsSource = winParkBarsProx;
            }
            else if (locale == 1 && xlocale == 0)
            {
                BuildBoiseBarsProx();
                boiseBarsProx = boiseBarsProx.OrderBy(i => i.Value).ToDictionary(i => i.Key, i => i.Value);
                barListView.ItemsSource = boiseBarsProx;
            }
            else if (locale == 0 && xlocale == 2)
            {
                BuildWinParkFoodsProx();
                winParkFoodsProx = winParkFoodsProx.OrderBy(i => i.Value).ToDictionary(i => i.Key, i => i.Value);
                barListView.ItemsSource = winParkFoodsProx;
            }
            else if (locale == 1 && xlocale == 3)
            {
                BuildBoiseFoodsProx();
                boiseFoodsProx = boiseFoodsProx.OrderBy(i => i.Value).ToDictionary(i => i.Key, i => i.Value);
                barListView.ItemsSource = boiseFoodsProx;
            }
            else if(locale == 2 && xlocale == 0 )
            {
                BuildnycBarsProx();
                nycbarsprox = nycbarsprox.OrderBy(i => i.Value).ToDictionary(i => i.Key, i => i.Value);
                barListView.ItemsSource = nycbarsprox;
            }
            else if (locale == 2 && xlocale == 4)
            {
                BuildnycFoodProx();
                nycfoodsprox = nycfoodsprox.OrderBy(i => i.Value).ToDictionary(i => i.Key, i => i.Value);
                barListView.ItemsSource = nycfoodsprox;
            }
            else if (locale == 2 && xlocale == 0)
            {
                BuildMiamiBeachBarsProx();
                miamiBeachBarsProx = miamiBeachBarsProx.OrderBy(i => i.Value).ToDictionary(i => i.Key, i => i.Value);
                barListView.ItemsSource = miamiBeachBarsProx;
            }
            else if (locale == 2 && xlocale == 4)
            {
                BuildMiamiBeachFoodsProx();
                miamiBeachFoodsProx = miamiBeachFoodsProx.OrderBy(i => i.Value).ToDictionary(i => i.Key, i => i.Value);
                barListView.ItemsSource = miamiBeachFoodsProx;
            }
        }
            //RESTAURANTS
        private void OnClick_Restaurants(object sender, EventArgs e)
        {
            if (locale == 0)
            {

                if (restPressed == true)
                {
                    return;
                }
                else
                {
                    hasbeen = false;
                    xlocale = 2;
                    BuildWinterParkFoods();
                    winterParkFoods = winterParkFoods.OrderBy(i => i.Value).ToDictionary(i => i.Key, i => i.Value);
                    barListView.ItemsSource = winterParkFoods;
                    restPressed = true;
                    barPressed = false;
                }
            }
            else if (locale == 1)
            {
                if (restPressed == true)
                {
                    return;
                }
                else
                {
                    hasbeen = false;
                    xlocale = 3;
                    BuildBoiseFoods();
                    boiseFoods = boiseFoods.OrderBy(i => i.Value).ToDictionary(i => i.Key, i => i.Value);
                    barListView.ItemsSource = boiseFoods;
                    restPressed = true;
                    barPressed = false;
                }
            }
            else if (locale == 2)
            {
                if (restPressed == true)
                {
                    return;
                }
                else
                {
                    hasbeen = false;
                    xlocale = 4;
                    Buildnycfoods();
                    nycfoods = nycfoods.OrderBy(i => i.Value).ToDictionary(i => i.Key, i => i.Value);
                    barListView.ItemsSource = nycfoods;
                    restPressed = true;
                    barPressed = false;
                }

            }
            else if (locale == 3)
            {
                if (restPressed == true)
                {
                    return;
                }
                else
                {
                    hasbeen = false;
                    xlocale = 5;
                    BuildMiamiBeachFood();
                    miamiBeachFoods = miamiBeachFoods.OrderBy(i => i.Value).ToDictionary(i => i.Key, i => i.Value);
                    barListView.ItemsSource = miamiBeachFoods;
                    restPressed = true;
                    barPressed = false;
                }

            }
        }
            //BARS
        private void OnClick_Bars(object sender, EventArgs e)
        {
            if (locale == 0)
            {
                if (barPressed == true)
                {
                    return;
                }
                else
                {
                    xlocale = 0;
                    BuildWinParkFoodsProx();
                    winterParkBars = winterParkBars.OrderByDescending(i => i.Value).ToDictionary(i => i.Key, i => i.Value);
                    barListView.ItemsSource = winterParkBars;
                    barPressed = true;
                    restPressed = false;
                }
            }
            else if (locale == 1)
            {
                if (barPressed == true)
                {
                    return;

                }
                else
                {
                    xlocale = 0;
                    BuildBoiseBars();
                    boiseBars = boiseBars.OrderByDescending(i => i.Value).ToDictionary(i => i.Key, i => i.Value);
                    barListView.ItemsSource = boiseBars;
                    barPressed = true;
                    restPressed = false;
                }
            }
            else if (locale == 2)
            {
                if (barPressed == true)
                {
                    return;

                }
                else
                {
                    xlocale = 0;
                    BuildnycBars();
                    nycbars = nycbars.OrderByDescending(i => i.Value).ToDictionary(i => i.Key, i => i.Value);
                    barListView.ItemsSource = nycbars;
                    barPressed = true;
                    restPressed = false;
                }

            }
            else if (locale == 3)
            {
                if (barPressed == true)
                {
                    return;

                }
                else
                {
                    xlocale = 0;
                    BuildMiamiBeachBars();
                    miamiBeachBars = miamiBeachBars.OrderByDescending(i => i.Value).ToDictionary(i => i.Key, i => i.Value);
                    barListView.ItemsSource = miamiBeachBars;
                    barPressed = true;
                    restPressed = false;
                }

            }
        }
        //PEACE-OUT
        private async void OnClick_Peace(object sender, EventArgs e)
        {
            if(locale == 0 && xlocale == 0)
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
            else if (locale == 0 && xlocale == 2)
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
            else if (locale == 1 && xlocale == 0)
            {
                usercount = boiseBars[temp];
                var excode = await DisplayAlert("Leaving So Soon?", "Would You Like To Close The App?", "Yes", "No");
                if (excode == true)
                {
                    boiseBars[temp] = usercount - 1; // decrementing old bar // only if true
                    Thread.Sleep(3000);
                    Environment.FailFast("");
                }
                else { return; }
            }
            else if (locale == 1 && xlocale == 3)
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
            else if (locale == 2 && xlocale == 0)
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
            else if (locale == 2 && xlocale == 4)
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
            else if (locale == 3 && xlocale == 0)
            {
                usercount = miamiBeachBars[temp];
                var excode = await DisplayAlert("Leaving So Soon?", "Would You Like To Close The App?", "Yes", "No");
                if (excode == true)
                {
                    miamiBeachBars[temp] = usercount - 1; // decrementing old bar // only if true
                    Thread.Sleep(3000);
                    Environment.FailFast("");
                }
                else { return; }
            }
            else if (locale == 3 && xlocale == 5)
            {
                usercount = miamiBeachFoods[temp];
                var excode = await DisplayAlert("Leaving So Soon?", "Would You Like To Close The App?", "Yes", "No");
                if (excode == true)
                {
                    miamiBeachFoods[temp] = usercount - 1; // decrementing old bar // only if true
                    Thread.Sleep(3000);
                    Environment.FailFast("");
                }
                else { return; }
            }

        }
    }
}
