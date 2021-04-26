using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmHelpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace _2NiteAHI
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DayModeTheme : ContentPage
    {
        public DayModeTheme()
        {
            InitializeComponent();

        }

        public object NightTheme { get ; private set; }

        private void NightTheme_Toggled(object sender, ToggledEventArgs e)
        {
            // logic here to change to NIGHTTIME THEME
            if (e.Value)
            {
                App.Current.UserAppTheme = OSAppTheme.Dark;
            }
            else
            {
                App.Current.UserAppTheme = OSAppTheme.Light;
            }
            

        }
    }
}