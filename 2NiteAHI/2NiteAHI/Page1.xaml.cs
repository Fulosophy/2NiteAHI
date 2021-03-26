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
public partial class Page1 : ContentPage
{
    public Page1()
    {
        InitializeComponent();
    }
        async void ForgotButtonClicked(object sender, EventArgs e) { await Navigation.PushAsync(new Page2()); }
        async void RegButtonClicked(object sender, EventArgs e) { await Navigation.PushAsync(new Page3()); }
        async void SubmitButtonClicked(object sender, EventArgs e) { await Navigation.PushAsync(new MainPage()); }
    }
}