using MetallLogistic.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MetallLogistic.Pages.Driver.StepPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MovingToWareHousePage : ContentPage
    {
        Order _currOrder;
        public MovingToWareHousePage(Order currOrder)
        {
            InitializeComponent();
            this.BindingContext = currOrder;
            _currOrder = currOrder;
        }

        private void BtnNext_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Pages.Driver.StepPages.WaitingForLodingPage(_currOrder));
        }

        private void LabelProblemsTap_Tapped(object sender, EventArgs e)
        {

        }

        private void ImgMapTap_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Pages.Driver.MapPage());

        }
    }
}