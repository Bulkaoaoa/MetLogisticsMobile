using Android.Widget;
using MetallLogistic.Classes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MetallLogistic
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Pages.RegistrationPage());
        }

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            if (SwitchIsDriver.IsToggled == true)
                SwitchIsDriver.IsToggled = false;
            else
                SwitchIsDriver.IsToggled = true;
        }

        private void BtnLogin_Clicked(object sender, EventArgs e)
        {
            var errors = "";
            if (string.IsNullOrWhiteSpace(EntryLogin.Text)) errors += "Вы не ввели логин \r\n";
            if (string.IsNullOrWhiteSpace(EntryPassword.Text)) errors += "Вы не ввели пароль \r\n";

            if (errors.Length == 0)
            {
                if (SwitchIsDriver.IsToggled == true) //Ищем по водителям/курьерам
                {
                    try
                    {
                        HttpClient client = new HttpClient();
                        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                        var task = client.GetStringAsync($"{AppData.ConectionString}ClientOrCourier/Authorization?login={EntryLogin.Text}" +
                            $"&password={EntryPassword.Text}&isDriver=true").Result;
                        var currCourier = JsonConvert.DeserializeObject<Courier>(task);
                        if (currCourier != null)
                        {
                            AppData.CurrCourierId = currCourier.Id;
                            AppData.CurrClientId = currCourier.ClientId;
                            //Вот это дело пока не работает, нужно будет узнать шо там да как
                        }
                        Navigation.PushAsync(new Pages.Driver.MainPageDriver());
                    }
                    catch
                    {
                        Toast.MakeText(Android.App.Application.Context, "Такого пользователя не существует или у вас проблемы с подключением к интернету", ToastLength.Long).Show();
                    }
                }
                else //Ищем по клиентам
                {
                    try
                    {
                        HttpClient client = new HttpClient();
                        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                        var task = client.GetStringAsync($"{AppData.ConectionString}ClientOrCourier/Authorization?login={EntryLogin.Text}" +
                            $"&password={EntryPassword.Text}&isDriver=false").Result;
                        var currClient = JsonConvert.DeserializeObject<Client>(task);
                        if (currClient != null)
                        {
                            AppData.CurrClientId = currClient.Id;
                            Navigation.PushAsync(new Pages.Client.MainPageClient());
                        }
                        //Вот это дело пока не работает, нужно будет узнать шо там да как
                    }
                    catch
                    {
                        Toast.MakeText(Android.App.Application.Context, "Такого пользователя не существует или у вас проблемы с подключением к интернету", ToastLength.Long).Show();
                    }
                }
            }
            else
                Toast.MakeText(Android.App.Application.Context, errors, ToastLength.Long).Show();
            //Navigation.PushAsync(new Pages.Driver.MainPageDriver());
        }
    }
}
