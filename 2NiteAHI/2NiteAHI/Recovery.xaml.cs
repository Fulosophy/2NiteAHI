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
public partial class Recovery : ContentPage
    {
        public string userEmail;
        public string YourUsername { get; set; }
        public string YourPassword { get; set; }

        public Recovery()
        {
            InitializeComponent();
        }
        async private void ToolbarItem_Clicked(object sender, EventArgs e) { await Navigation.PushAsync(new Settings()); }
        async void PWSubmitClicked(object sender, EventArgs e)
        {
            if (userEmail != App.email)
            {
                await DisplayAlert("Email doesn't match!", "Please try again!", "Ok");
            }
            else
            {
                await DisplayAlert("YOUR INFORMATION", "USERNAME:  " + App.username + "\n" + "PASSWORD:  " + App.pwd, "Ok");
            }
        }

        void Entry_EmailPwdRecovery(object sender, TextChangedEventArgs e)
        {
            userEmail = e.NewTextValue;
        }

        async void rLoginClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Login());

        }
    }
}