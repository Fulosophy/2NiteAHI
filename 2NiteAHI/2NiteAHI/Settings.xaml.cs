using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using System.Threading;
using Xamarin.Forms.Maps;
using GoogleApi;
using GoogleApi.Entities.Places;

namespace _2NiteAHI
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class Settings : ContentPage
{
    public Settings()
    {
        InitializeComponent();
    }

   
    

        private void GPSLOC_Toggled(object sender, ToggledEventArgs e)
        {
           
            if (GPSLOC.IsToggled == false)
            {
               // logic here to disable GPS LOCATION
            }
        }

        private void NightTheme_Toggled(object sender, ToggledEventArgs e)
        {
            if (NightTheme.IsToggled == false)
            {
                // logic here to change to NIGHTTIME THEME
            }
        }
    }
}