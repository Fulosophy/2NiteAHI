using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

//accidental merges suck.. a lot...
namespace _2NiteAHI
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class Login : ContentPage
{
    public string userSel, pwdSel;
    public Login()
    {
        InitializeComponent();
    }
        async void ForgotButtonClicked(object sender, EventArgs e) { await Navigation.PushAsync(new Recovery()); }
        async void RegButtonClicked(object sender, EventArgs e) { await Navigation.PushAsync(new Register()); }
        async void SubmitButtonClicked(object sender, EventArgs e) 
        {
            if (userSel != App.username) { await DisplayAlert("Username not found!", "Please try again!", "Ok"); }
            else if (pwdSel != App.pwd) { await DisplayAlert("Incorrect Password!", "Please try again!", "Ok"); }
            else if (userSel != App.username && pwdSel != App.pwd) { await DisplayAlert("Please enter a password", "Please try again!", "Ok"); }
            else { await Navigation.PushAsync(new LocationList()); }
            GetUsername.Text = string.Empty;
            GetPassword.Text = string.Empty;
        }
        void Entry_Username(object sender, TextChangedEventArgs e) { userSel = e.NewTextValue; }
        void Entry_Password(object sender, TextChangedEventArgs e) { pwdSel = e.NewTextValue; }
    }
}