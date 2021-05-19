using Xamarin.Forms;

namespace _2NiteAHI
{
    public partial class App : Application
    {
        public static string username, pwd, email, phoneNumber, MyLocation;
        public static int isMyLocation = 0;
        public App()
        {
            App.Current.UserAppTheme = OSAppTheme.Dark;
            InitializeComponent();
            username = "FSU";
            pwd = "Powerhouse";
            email = "besteamever@fsu.com";
            phoneNumber = "208-841-8666";

            MainPage = new NavigationPage(new SplashScreen());
        }
        protected override void OnStart() { }
        protected override void OnSleep() { }
        protected override void OnResume() { }
    }
}