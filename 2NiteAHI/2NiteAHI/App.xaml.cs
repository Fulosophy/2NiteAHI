﻿using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace _2NiteAHI
{
    public partial class App : Application
    {
        public App()
        {
            Device.SetFlags(new string[] {"AppTheme_Experimental"});
            Device.SetFlags(new string[] { "Shapes_Experimental" , "MediaElemental_Experimental" });
            InitializeComponent();
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
