using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace _2NiteAHI
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page1 : ContentPage
    {
        public Dictionary<String, int> winterParkBars = new Dictionary<string, int>();

        public Page1()
        {
            InitializeComponent();
            BuildWinterParkBars();
            bars.ItemsSource = winterParkBars;
        }
        private void BuildWinterParkBars()
        {

            winterParkBars.Add("Chili's Bar & Grill", 10);
            winterParkBars.Add("Firehouse Subs", 10);
            winterParkBars.Add("Arooga's", 10);
            winterParkBars.Add("Chewy Boba", 10);
            winterParkBars.Add("The Geek Easy", 10);
            winterParkBars.Add("Steak 'n Shake", 10);
            winterParkBars.Add("The Haven At Forsyth", 10);
            winterParkBars.Add("El Pueblo Mexicqan", 10);
            winterParkBars.Add("Debbie's Bar", 10);
            winterParkBars.Add("Ain't Misbehavin'", 10);
            winterParkBars.Add("Miller's Ale House", 10);
            winterParkBars.Add("Devaney's Sports Pub", 10);
            winterParkBars.Add("The Next", 10);
            winterParkBars.Add("Admiral Cigar Club", 10);
            winterParkBars.Add("Gator's Dockside", 10);
            winterParkBars.Add("Cork & Plate", 10);
            winterParkBars.Add("Tactical Brewing Co.", 10);
            winterParkBars.Add("The Brass Tap", 10);
            winterParkBars.Add("Sonny's BBQ", 10);
            winterParkBars.Add("La Placita 19'", 10);
            winterParkBars.Add("Rincon Latino", 10);
            winterParkBars.Add("Starbucks", 10);
            winterParkBars.Add("Tequila Lounge Club", 10);
            winterParkBars.Add("Muldoons Saloon", 10);
            winterParkBars.Add("Texas Roadhous", 10);
            winterParkBars.Add("Majijis Hookah Lounge", 10);
            winterParkBars.Add("Thirsty Gator", 10);
            winterParkBars.Add("Don Julio Mexican Kitchen and Tequila Bar", 10);
            winterParkBars.Add("Fire on the Bayou", 10);
            winterParkBars.Add("Rock and Brews", 10);
            winterParkBars.Add("BJ's Restaurant and Brewhouse", 10);
            winterParkBars.Add("Oviedo Brewing Company", 10);
            winterParkBars.Add("Buffalo Wild WIngs", 10);
            winterParkBars.Add("The Green Bar", 10);
            winterParkBars.Add("The Green Parrot", 10);
            winterParkBars.Add("Luke's Kitchen and Bar", 10);
        }
    }   
}