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
    public partial class Register : ContentPage
    {
        //VARIABLES
        public string repwd;
        User user1;

        public Register()
        {
            InitializeComponent();
        }

        //BUTTONS
        async void RegSubmitClicked(object sender, EventArgs e) 
        {
            if (repwd != App.pwd)
            {
                await DisplayAlert("Please enter the same password as above", " ", "Ok");
            }
            await Navigation.PushAsync(new Login());
            
        }

        //ENTRIES
        void Entry_Email(object sender, TextChangedEventArgs e)
        {
            App.email = e.NewTextValue;
            
        }
        void Entry_Username(object sender, TextChangedEventArgs e)
        {
            App.username = e.NewTextValue;
        }
        void Entry_Password(object sender, TextChangedEventArgs e)
        {
            App.pwd = e.NewTextValue;
        }
        void Entry_RePassword(object sender, TextChangedEventArgs e)
        {
            repwd = e.NewTextValue;
        }
        void Entry_PhoneNumber(object sender, TextChangedEventArgs e)
        {
            App.phoneNumber = e.NewTextValue;

        }
    }
    public class User
    {
        public string username { get; set; }
        public string pwd { get; set; }
        public string email { get; set; }
        public string phnumber { get; set; }

        public User(string _username, string _pwd, string _email, string _phnumber)
        {
            username = _username;
            pwd = _pwd;
            email = _email;
            phnumber = _phnumber;
        }
    }
}