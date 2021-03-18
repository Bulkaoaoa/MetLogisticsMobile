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
    public partial class MainPageDriver : ContentPage
    {
        public MainPageDriver()
        {
            InitializeComponent();

            //List<Order> orders = new List<Order>();
            //orders.Add( new Order()
            //{
            //    Id = "123243",
            //    DateTimeOfArrivle = new DateTime(2021, 3, 18, 13, 20, 0)
            //});
            //orders.Add(new Order()
            //{
            //    Id = "252536",
            //    DateTimeOfArrivle = new DateTime(2021, 3, 18, 13, 20, 0)
            //});
            //orders.Add(new Order()
            //{
            //    Id = "347586",
            //    DateTimeOfArrivle = new DateTime(2021, 3, 18, 13, 20, 0)
            //});

            //LVMyOrders.ItemsSource = orders.ToList();
            UpdateLv();
        }

        private void UpdateLv()
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var response = client.GetStringAsync($"{AppData.ConectionString}Courier/GetOrder?courierId={AppData.CurrCourierId}").Result;
                var listOfOrders = JsonConvert.DeserializeObject<List<Order>>(response);
                LVMyOrders.ItemsSource = listOfOrders.ToList();
            }
            catch 
            {
                Toast.MakeText(Android.App.Application.Context, "Упс... у нас или у вас проблемы с интернет соединением :С", ToastLength.Long).Show();
            }
        }
        private void EntrySearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            //Поиск
        }

        private void LVMyOrders_Refreshing(object sender, EventArgs e)
        {
            LVMyOrders.IsRefreshing = true;
            UpdateLv();
            LVMyOrders.IsRefreshing = false;
        }

        private void LVMyOrders_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Navigation.PushAsync(new Pages.Driver.StartOrderPage(LVMyOrders.SelectedItem as Order)); //Упадет же, говнище ебаное
            //Navigation.PushAsync(new Pages.Driver.StepPage(LVMyOrders.SelectedItem as Order));
        }
    }
}