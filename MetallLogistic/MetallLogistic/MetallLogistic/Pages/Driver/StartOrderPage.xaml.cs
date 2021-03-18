﻿using MetallLogistic.Classes;
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
            Navigation.PushAsync(new Pages.Driver.StepPages.WaitingBehindGatesPage(_currOrder));
        }
    }
}