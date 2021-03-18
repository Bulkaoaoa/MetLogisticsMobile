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

        private void BtnGo_Clicked(object sender, EventArgs e)
        {
            //try
            //{
            //BtnGo.IsEnabled = false;
            //HttpClient client = new HttpClient();
            //client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            //var bff = client.GetStringAsync($"{AppData.ConectionString}Courier/GetNewStep?orderId={_currOrder.Id}").Result;
            Navigation.PushAsync(new Pages.Driver.StepPages.WaitingBehindGatesPage(_currOrder));
            //}
            //catch 
            //{
            //    Toast.MakeText(Android.App.Application.Context, "Упс... у нас или у вас проблемы с интернет соединением :С", ToastLength.Long).Show();
            //}
        }
    }
}