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
    
    //Fixing an accidental merge sucks...
    public partial class LocationList : ContentPage
    {
        public LocationList()
        {
            InitializeComponent();
        }
        async private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Settings());        
        }
    }
}