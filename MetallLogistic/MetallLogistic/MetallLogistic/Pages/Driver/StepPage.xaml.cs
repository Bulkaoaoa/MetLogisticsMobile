using Android.Widget;
using MetallLogistic.Classes;
using Newtonsoft.Json;
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
    public partial class StepPage : ContentPage
    {
        List<StepOfOrder> _lastStep;
        Order _currOrder;
        public StepPage(Order currOrder)
        {
            InitializeComponent();
            _currOrder = currOrder;
            GetStep();
        }

        public async void GenreateThings()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            var listOfSteOfOrders = client.PostAsync($"{AppData.ConectionString}StepOfOrders?orderId={_currOrder.Id}", null);
        }
        public void GetStep()
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var buffLastStep = client.GetStringAsync($"{AppData.ConectionString}Courier/GetStep?orderId={_currOrder}").Result;
                //_lastStep = JsonConvert.DeserializeObject<List<StepOfOrder>>(buffLastStep);
                //this.BindingContext = _lastStep.FirstOrDefault();
            }
            catch
            {
                Toast.MakeText(Android.App.Application.Context, "Упс... у нас или у вас проблемы с интернет соединением :С", ToastLength.Long).Show();
            }
        }

        public void PutStep()
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var task = client.PutAsync($"{AppData.ConectionString}StepOfOrders?orderId={_currOrder.Id}",
                    new StringContent(JsonConvert.SerializeObject(_lastStep), Encoding.UTF8, "application/json"));
                _lastStep = JsonConvert.DeserializeObject<List<StepOfOrder>>(task.Result.Content.ReadAsStringAsync().Result);
                this.BindingContext = _lastStep.FirstOrDefault();
            }
            catch
            {
                GetStep();
                Toast.MakeText(Android.App.Application.Context, "Упс... у нас или у вас проблемы с интернет соединением :С", ToastLength.Long).Show();
            }
        }
        private void BtnNext_Clicked(object sender, EventArgs e)
        {
            //if (_lastStep.Count == 0)
            //    //Navigation.PopAsync();
            //else
                PutStep();
        }

        private void LabelProblemsTap_Tapped(object sender, EventArgs e)
        {

        }

        private void ImgMapTap_Tapped(object sender, EventArgs e)
        {

        }
    }
}