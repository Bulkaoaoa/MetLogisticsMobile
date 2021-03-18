using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MetallLogistic
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Pages.RegistrationPage());
        }

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            if (SwitchIsDriver.IsToggled == true)
                SwitchIsDriver.IsToggled = false;
            else
                SwitchIsDriver.IsToggled = true;
        }

        private void BtnLogin_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Pages.Driver.MainPageDriver());
        }
    }
}
