using System;
using System.Collections.Generic;
using System.Text;
using MvvmHelpers;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace _2NiteAHI.ViewModel
{
    public class MyViewModel : BaseViewModel
    {
        public bool DayTheme
        {
            get { return Preferences.Get("DayTheme", false); }

            set
            {
                if (value)
                {
                    App.Current.UserAppTheme = OSAppTheme.Dark;
                }
                else
                {
                    App.Current.UserAppTheme = OSAppTheme.Light;
                }
                Preferences.Set("DayTheme", value);
                OnPropertyChanged(nameof(DayTheme));
            }
        }
    }

}

