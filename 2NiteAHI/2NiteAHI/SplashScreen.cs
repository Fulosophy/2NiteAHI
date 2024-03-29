﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace _2NiteAHI
{
    class SplashScreen : ContentPage
    {
        readonly Image fade;
        public SplashScreen()
        {
            NavigationPage.SetHasNavigationBar(this, false);

            var sub = new AbsoluteLayout();
            fade = new Image
            {
                Source = "fireName",
                WidthRequest = 500,
                HeightRequest = 500
            };
            AbsoluteLayout.SetLayoutFlags(fade, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(fade, new Rectangle(0.5, 0.5, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));

            sub.Children.Add(fade);
            this.BackgroundColor = Color.FromHex("#000e4a");
            //this.BackgroundColor = Color.FromHex("#429de4");
            this.Content = sub;
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await fade.ScaleTo(1, 1000);
            await fade.ScaleTo(0.5, 800, Easing.SpringIn);
            await Task.WhenAny<bool>
            (
                fade.ScaleTo(1.2, 700, Easing.BounceOut),
                fade.RelRotateTo(360, 700, Easing.CubicOut)
            );
            await fade.ScaleTo(0.8, 500, Easing.SpringIn);
            Application.Current.MainPage = new NavigationPage(new PrivacyPolicy()) 
            { 
                BarBackgroundColor = Color.Black,
                BarTextColor = Color.DarkOrchid
            };
        }
    }
}
