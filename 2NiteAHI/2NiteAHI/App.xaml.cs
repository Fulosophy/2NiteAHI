using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace _2NiteAHI
{
    public partial class App : Application
    {
        public static string username, pwd, email, phoneNumber;
        public App()
        {
            InitializeComponent();

            username = "FSU";
            pwd = "Powerhouse";
            email = "besteamever@fsu.com";
            phoneNumber = "208-841-8666";

            MainPage = new NavigationPage(new SplashScreen());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
