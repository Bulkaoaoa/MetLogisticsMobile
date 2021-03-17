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
    public partial class MainPageClient : ContentPage
    {
        public MainPageClient()
        {
            InitializeComponent();
            //Логика если лист пустой, то мы показываем картинку
            if (true)
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