using MetallLogistic.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
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

            List<Order> orders = new List<Order>();
            orders.Add( new Order()
            {
                Id = "123243",
                DateTimeOfArrival = new DateTime(2021, 3, 18, 13, 20, 0)
            });
            orders.Add(new Order()
            {
                Id = "252536",
                DateTimeOfArrival = new DateTime(2021, 3, 18, 13, 20, 0)
            });
            orders.Add(new Order()
            {
                Id = "347586",
                DateTimeOfArrival = new DateTime(2021, 3, 18, 13, 20, 0)
            });

            LVMyOrders.ItemsSource = orders.ToList();
        }

        private void EntrySearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            //Поиск
        }

        private void LVMyOrders_Refreshing(object sender, EventArgs e)
        {
            //Метод обновы
        }

        private void LVMyOrders_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Navigation.PushAsync(new Pages.Driver.StartOrderPage(LVMyOrders.SelectedItem as Order)); //Упадет же, говнище ебаное
        }
    }
}