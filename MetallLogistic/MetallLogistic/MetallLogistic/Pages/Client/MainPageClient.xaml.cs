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

namespace MetallLogistic.Pages.Client
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPageClient : ContentPage
    {
        public MainPageClient()
        {
            InitializeComponent();
            //Логика если лист пустой, то мы показываем картинку
            UpdateLv();
        }

        private void UpdateLv()
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var response = client.GetStringAsync($"{AppData.ConectionString}Client/OrdersToDay?id={AppData.CurrClientId}").Result;
                var listOfOrders = JsonConvert.DeserializeObject<List<Order>>(response);
                LvMyOrdersToday.ItemsSource = listOfOrders.ToList();
                if (listOfOrders.Count == 0)
                {
                    ImgNull.IsVisible = true;
                    LvMyOrdersToday.IsVisible = false;
                }
                else
                {
                    ImgNull.IsVisible = false;
                    LvMyOrdersToday.IsVisible = true;
                }
            }
            catch
            {
                Toast.MakeText(Android.App.Application.Context, "Упс... у нас или у вас проблемы с интернет соединением :С", ToastLength.Long);
            }
        }
        private void LvMyOrdersToday_Refreshing(object sender, EventArgs e)
        {

        }

        private void EntrySearch_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void FabTap_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Client.NomenclaturePage());
        }
    }
}