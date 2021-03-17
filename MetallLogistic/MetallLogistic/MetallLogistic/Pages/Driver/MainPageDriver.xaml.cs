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
            //Переход на страницу начала заказа
        }
    }
}