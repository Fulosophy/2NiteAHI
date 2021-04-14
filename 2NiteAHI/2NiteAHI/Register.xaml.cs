using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using _2NiteAHI.Helper;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace _2NiteAHI
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Register : ContentPage
    {

        public Register()
        {
            InitializeComponent();
            //NavigationPage.SetHasBackButton(this, false);
            GetEmail.ReturnCommand = new Command(() => GetUsername.Focus());
            GetUsername.ReturnCommand = new Command(() => GetPassword.Focus());
            GetPassword.ReturnCommand = new Command(() => GetVerifyPassword.Focus());
            GetVerifyPassword.ReturnCommand = new Command(() => GetNumber.Focus());
        }
        private async void RegSubmitClicked(object sender, EventArgs e) 
        {
            Users users = new Users();
            UserDatabase userDB = new UserDatabase();

            if ((string.IsNullOrWhiteSpace(GetUsername.Text)) || (string.IsNullOrWhiteSpace(GetEmail.Text)) ||
                (string.IsNullOrWhiteSpace(GetPassword.Text)) || (string.IsNullOrWhiteSpace(GetNumber.Text)) ||
                (string.IsNullOrEmpty(GetUsername.Text)) || (string.IsNullOrEmpty(GetEmail.Text)) ||
                (string.IsNullOrEmpty(GetPassword.Text)) || (string.IsNullOrEmpty(GetNumber.Text)))
            { await DisplayAlert("Enter Data", "Enter Valid Data", "OK"); }
            else if(!string.Equals(GetPassword.Text, GetVerifyPassword.Text))
            {
                PwMismatchWarn.Text = "Please enter Same Password";
                GetPassword.Text = string.Empty;
                GetVerifyPassword.Text = string.Empty;
                PwMismatchWarn.TextColor = Color.IndianRed;
                PwMismatchWarn.IsVisible = true;
            }
            else if(GetNumber.Text.Length < 10)
            {
                GetNumber.Text = string.Empty;
                NumberNotValid.Text = "Please enter 10 digit Number";
                NumberNotValid.TextColor = Color.IndianRed;
                NumberNotValid.IsVisible = true;
            }
            else
            {
                users.email = GetEmail.Text;
                users.username = GetUsername.Text;
                users.password = GetPassword.Text;
                users.pNumber = GetNumber.Text;
                try
                {
                    var returnvalue = userDB.AddUser(users);
                    if(returnvalue == "Registered")
                    {
                        await DisplayAlert("User Add", returnvalue, "OK");
                        await Navigation.PushAsync(new Login());
                    }
                    else
                    {
                        await DisplayAlert("User Add", returnvalue, "OK");
                        PwMismatchWarn.IsVisible = false;
                        GetEmail.Text = string.Empty;
                        GetUsername.Text = string.Empty;
                        GetPassword.Text = string.Empty;
                        GetVerifyPassword.Text = string.Empty;
                        GetNumber.Text = string.Empty;
                    }
                }
                catch(Exception ex)
                {
                    Debug.WriteLine(ex);
                }
            }


             
        }

    }
}