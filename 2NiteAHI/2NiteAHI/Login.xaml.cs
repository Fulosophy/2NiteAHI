using System;
using System.ComponentModel;
using System.Windows.Input;
using _2NiteAHI.Helper;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace _2NiteAHI
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        public Login()
        {
            InitializeComponent();
            //UserDatabase userData = new UserDatabase();
            //NavigationPage.SetHasBackButton(this, false);

        }

        async void ForgotButtonClicked(object sender, EventArgs e) { await Navigation.PushAsync(new Recovery()); }
        async void RegButtonClicked(object sender, EventArgs e) { await Navigation.PushAsync(new Register()); }
        async void SubmitButtonClicked(object sender, EventArgs e) { await Navigation.PushAsync(new MainPage()); }

    }
}