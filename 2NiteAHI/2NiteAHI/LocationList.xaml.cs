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
            var location = await Geolocation.GetLocationAsync(new GeolocationRequest(GeolocationAccuracy.Default, TimeSpan.FromMinutes(1)));
            var GetAddy = await Geocoding.GetPlacemarksAsync(location.Latitude, location.Longitude); // Grabbing the users location details as a placemark.
            var addy = GetAddy?.FirstOrDefault(); 
            MyLocation = $"{addy.Locality},{addy.AdminArea}"; // Grabs the users current locations Address-Town and Address-PostalCode

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
        public string GetZipBopCode(string zipBopCode)
        {
            int zipCode = Int32.Parse(zipBopCode);
            //state assignment string
            string st = " ";
            //Conditions to assign state abbreviation based on zipCode
            if (zipCode >= 35000 && zipCode <= 36999) { st = " AL"; }//Alabama 
            else if (zipCode >= 99500 && zipCode <= 99999) { st = " AK"; }//Alaska 
            else if (zipCode >= 85000 && zipCode <= 86999) { st = " AZ"; }//Arizona 
            else if (zipCode >= 71600 && zipCode <= 72999) { st = " AR"; }//Arkansas 
            else if (zipCode >= 90000 && zipCode <= 96699) { st = " CA"; }//Cali 
            else if (zipCode >= 80000 && zipCode <= 81999) { st = " CO"; }//Colorado 
            else if ((zipCode >= 6000 && zipCode <= 6389) || (zipCode >= 6391 && zipCode <= 6999)) { st = " CT"; }//Connecticut 
            else if (zipCode >= 19700 && zipCode <= 19999) { st = " DE"; }//Delaware 
            else if (zipCode >= 32000 && zipCode <= 34999) { st = " FL"; }//Florida 
            else if ((zipCode >= 30000 && zipCode <= 31999) || (zipCode >= 39800 && zipCode <= 39999)) { st = " GA"; }//Georgia 
            else if (zipCode >= 96700 && zipCode <= 96999) { st = " HI"; }//Hawaii 
            else if (zipCode >= 83200 && zipCode <= 83999) { st = " ID"; }//Idaho 
            else if (zipCode >= 60000 && zipCode <= 62999) { st = " IL"; }//Illinois 
            else if (zipCode >= 46000 && zipCode <= 47999) { st = " IN"; }//Indiana 
            else if (zipCode >= 50000 && zipCode <= 52999) { st = " IA"; }//Iowa 
            else if (zipCode >= 66000 && zipCode <= 67999) { st = " KS"; }//Kansas 
            else if (zipCode >= 40000 && zipCode <= 42999) { st = " KY"; }//Kentucky 
            else if (zipCode >= 70000 && zipCode <= 71599) { st = " LA"; }//Louisiana 
            else if (zipCode >= 3900 && zipCode <= 4999) { st = " ME"; }//Maine 
            else if (zipCode >= 20600 && zipCode <= 21999) { st = " MD"; }//Maryland 
            else if ((zipCode >= 1000 && zipCode <= 2799) || (zipCode == 5501) || (zipCode == 5544)) { st = " MA"; }//Massachusetts 
            else if (zipCode >= 48000 && zipCode <= 49999) { st = " MI"; }//Michigan 
            else if (zipCode >= 55000 && zipCode <= 56899) { st = " MN"; }//Minnesota 
            else if (zipCode >= 38600 && zipCode <= 39999) { st = " MS"; }//Mississippi 
            else if (zipCode >= 63000 && zipCode <= 65999) { st = " MO"; }//Missouri 
            else if (zipCode >= 59000 && zipCode <= 59999) { st = " MT"; }//Montana 
            else if (zipCode >= 27000 && zipCode <= 28999) { st = " NC"; }//N. Carolina 
            else if (zipCode >= 58000 && zipCode <= 58999) { st = " ND"; }//N. Dakota 
            else if (zipCode >= 68000 && zipCode <= 69999) { st = " NE"; }//Nebraska 
            else if (zipCode >= 88900 && zipCode <= 89999) { st = " NV"; }//Nevada 
            else if (zipCode >= 3000 && zipCode <= 3899) { st = " NH"; }//N. Hampshire 
            else if (zipCode >= 7000 && zipCode <= 8999) { st = " NJ"; }//N. Jersey 
            else if (zipCode >= 87000 && zipCode <= 88499) { st = " NM"; }//N. Mexico 
            else if ((zipCode >= 10000 && zipCode <= 14999) || (zipCode == 6390) || (zipCode == 501) || (zipCode == 544)) { st = " NY"; }//N. York 
            else if (zipCode >= 43000 && zipCode <= 45999) { st = " OH"; }//Ohio 
            else if ((zipCode >= 73000 && zipCode <= 73199) || (zipCode >= 73400 && zipCode <= 74999)) { st = " OK"; }//Oklahoma 
            else if (zipCode >= 97000 && zipCode <= 97999) { st = " OR"; }//Oregon 
            else if (zipCode >= 15000 && zipCode <= 19699) { st = " PA"; }//Pennsylvania 
            else if (zipCode >= 300 && zipCode <= 999) { st = " PR"; }//Puerto Rico 
            else if (zipCode >= 2800 && zipCode <= 2999) { st = " RI"; }//RHode Island 
            else if (zipCode >= 29000 && zipCode <= 29999) { st = " SC"; }//S. Carolina 
            else if (zipCode >= 57000 && zipCode <= 57999) { st = " SD"; }//S. Dakota 
            else if (zipCode >= 37000 && zipCode <= 38599) { st = " TN"; }//Tennessee 
            else if ((zipCode >= 75000 && zipCode <= 79999) || (zipCode >= 73301 && zipCode <= 73399) || (zipCode >= 88500 && zipCode <= 88599)) { st = " TX"; }//Texas 
            else if (zipCode >= 84000 && zipCode <= 84999) { st = " UT"; }//Utah 
            else if (zipCode >= 5000 && zipCode <= 5999) { st = " VT"; }//Vermont 
            else if ((zipCode >= 20100 && zipCode <= 20199) || (zipCode >= 22000 && zipCode <= 24699) || (zipCode == 20598)) { st = " VA"; }//Virgina 
            else if ((zipCode >= 20000 && zipCode <= 20099) || (zipCode >= 20200 && zipCode <= 20599) || (zipCode >= 56900 && zipCode <= 56999)) { st = " DC"; }//Wash. DC 
            else if (zipCode >= 98000 && zipCode <= 99499) { st = " WA"; }//Washington 
            else if (zipCode >= 24700 && zipCode <= 26999) { st = " WV"; }//W. Virginia 
            else if (zipCode >= 53000 && zipCode <= 54999) { st = " WI"; }//Wisconsin 
            else if (zipCode >= 82000 && zipCode <= 83199) { st = " WY"; }//Wyoming 
            else { st = zipCode.ToString(); }
        return st;
        }
    }
}