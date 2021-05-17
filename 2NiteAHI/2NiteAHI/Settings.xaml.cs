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
            if (e.Value){ App.Current.UserAppTheme = OSAppTheme.Light;}
            else { App.Current.UserAppTheme = OSAppTheme.Dark; }
        }
    }
}
