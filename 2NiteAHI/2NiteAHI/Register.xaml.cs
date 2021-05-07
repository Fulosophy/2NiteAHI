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
        public Register()
        {
            InitializeComponent();
            Device.SetFlags(new[] { "Shapes_experimental", "MediaElement_Experimental" });
        }
        async void RegSubmitClicked(object sender, EventArgs e) { await Navigation.PushAsync(new Login()); }

    }
}