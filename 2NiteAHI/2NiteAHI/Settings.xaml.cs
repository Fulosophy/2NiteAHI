using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace _2NiteAHI
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Settings : ContentPage
    {
        public string _Email { get; set; }
        public string _Username { get; set; }
        public string _PhoneNumber { get; set; }

        public Settings()
        {
            InitializeComponent();
            _Email = App.email;
            _Username = App.username;
            _PhoneNumber = App.phoneNumber;
            BindingContext = this;
        }
        public void NightTheme_Toggled(object sender, ToggledEventArgs e)
        {
            if (e.Value)
            {
                if (App.Current.UserAppTheme == OSAppTheme.Light)
                {
                    App.Current.UserAppTheme = OSAppTheme.Dark;
                }
                else
                {
                    App.Current.UserAppTheme = OSAppTheme.Light;
                }
            }
        }
        public void Idaho_Toggled(object sender, ToggledEventArgs e)
        {
            if (e.Value)
            {
                WinParkFLSwitch.IsToggled = false;
                NewYorkNYSwitch.IsToggled = false;
                App.isMyLocation = 1;
                App.MyLocation = "Boise, Idaho";
                App.locale = 1;
            }
        }
        public void Florida_Toggled(object sender, ToggledEventArgs e)
        {
            if (e.Value)
            {
                BoiseIDSwitch.IsToggled = false;
                NewYorkNYSwitch.IsToggled = false;
                App.isMyLocation = 2;
                App.MyLocation = "Winter Park, Florida";
                App.locale = 0;
            }
        }
        public void NewYork_Toggled(object sender, ToggledEventArgs e)
        {
            if (e.Value)
            {
                BoiseIDSwitch.IsToggled = false;
                WinParkFLSwitch.IsToggled = false;
                App.isMyLocation = 3;
                App.MyLocation = "New York, New York";
                App.locale = 2;
            }
        }
    }
}