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
    public partial class NomenclaturePage : ContentPage
    {
        public NomenclaturePage()
        {
            InitializeComponent();
        }

        private void EntrySearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            //Тут поиск по названию номенклатуры
        }

        private void FabTap_Tapped(object sender, EventArgs e)
        {
            //Тут переход в корзину
            Navigation.PushAsync(new Pages.Client.CartPage());
        }

        private void LvNomenclatures_Refreshing(object sender, EventArgs e)
        {
            //Вот тут обновляем лист
        }

        private void SwipeItemAddToCart_Invoked(object sender, EventArgs e)
        {
            //Вот тут добавляем в локальный лист номенклатуру
            //Составлять все через FindName? Я че то пока мало это вижу работающим. Капец
        }
    }
}