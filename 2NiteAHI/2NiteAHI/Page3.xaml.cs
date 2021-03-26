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
public partial class Page3 : ContentPage
    {
        public Page3()
        {
            InitializeComponent();
        }
        async void RegSubmitClicked(object sender, EventArgs e) { await Navigation.PushAsync(new Page1()); }

    }
}