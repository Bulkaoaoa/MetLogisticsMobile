using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MetallLogistic.Pages.Client
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CartPage : ContentPage
    {
        public CartPage()
        {
            InitializeComponent();
        }

        private void SwipeItemDeleteFromCart_Invoked(object sender, EventArgs e)
        {
            //Удалять из корзины
        }

        private void BtnConfirmOrder_Clicked(object sender, EventArgs e)
        {
            //Вот тут будем формировать OrderNomenclature
            //После этого Переходим на страницу своих заказов
            //TODO: Сделать на каждой странице метод обновления и на Loading/Appearing сделать обновление
        }

        private void EntrySearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            //Поиск по номенклатуре
        }

        private void LvNomenclatures_Refreshing(object sender, EventArgs e)
        {

        }
    }
}