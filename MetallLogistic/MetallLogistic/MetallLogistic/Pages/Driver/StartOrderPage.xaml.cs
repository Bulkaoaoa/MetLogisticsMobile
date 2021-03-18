using Android.Widget;
using MetallLogistic.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MetallLogistic.Pages.Driver
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StartOrderPage : ContentPage
    {
        Order _currOrder;
        public StartOrderPage(Order currOrder)
        {
            InitializeComponent();
            this.BindingContext = currOrder;
            _currOrder = currOrder;
        }

        private async void BtnGo_Clicked(object sender, EventArgs e)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                var listOfSteOfOrders = await client.PostAsync($"{AppData.ConectionString}StepOfOrders?orderId={_currOrder.Id}", null);
                BtnGo.IsEnabled = false;
                Navigation.PushAsync(new Pages.Driver.StepPage(_currOrder));
            }
            catch 
            {
                Toast.MakeText(Android.App.Application.Context, "Упс... у нас или у вас проблемы с интернет соединением :С", ToastLength.Long);
            }
        }
    }
}