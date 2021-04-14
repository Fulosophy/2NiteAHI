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
            NavigationPage.SetHasBackButton(this, false);
            GetEmail.ReturnCommand = new Command(() => GetPassword.Focus());
        }
        string log;
        private async void userIDCheck(object sender, EventArgs e)
        {
            UserDatabase userData = new UserDatabase();
            if((string.IsNullOrWhiteSpace(GetEmail.Text)) || (string.IsNullOrWhiteSpace(GetPassword.Text)))
            {
                await DisplayAlert("Alert", "Enter Email", "OK");
            }
            else
            {
                log = GetEmail.Text;
                var textresult = userData.updateUserValidation(GetEmail.Text);
                if (textresult)
                {
                    await DisplayAlert("User Mail Id Not Exist", "Enter Correct User Name", "OK");
                }
            }
        }

        async void ForgotButtonClicked(object sender, EventArgs e) { await Navigation.PushAsync(new Recovery()); }
        async void RegButtonClicked(object sender, EventArgs e) { await Navigation.PushAsync(new Register()); }

        async void SubmitButtonClicked(object sender, EventArgs e) 
        {
            UserDatabase userData = new UserDatabase();

            if (GetEmail.Text != null && GetPassword.Text != null)
            {
                var validData = userData.LoginValidate(GetEmail.Text, GetPassword.Text);
                if (validData)
                {
                    await Navigation.PushAsync(new MainPage());
                }
                else
                {
                    await DisplayAlert("Login Failed", "Email or Password Incorrect", "OK");
                }
            }
            else
            {
                await DisplayAlert("Oh no!", "Please enter Email & Password", "OK");
            }
        }
    }
}