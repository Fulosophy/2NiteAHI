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
public partial class Login : ContentPage
{
    public Login()
    {
        InitializeComponent();
    }
        async void ForgotButtonClicked(object sender, EventArgs e) { await Navigation.PushAsync(new Recovery()); }
        async void RegButtonClicked(object sender, EventArgs e) { await Navigation.PushAsync(new Register()); }
        async void SubmitButtonClicked(object sender, EventArgs e) { await Navigation.PushAsync(new LocationList()); }
    }
}