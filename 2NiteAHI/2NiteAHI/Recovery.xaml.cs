﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace _2NiteAHI
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class Recovery : ContentPage
    {
        public Recovery()
        {
            InitializeComponent();
        }
        async void PWSubmitClicked(object sender, EventArgs e) { await Navigation.PushAsync(new Login()); }
    }
}