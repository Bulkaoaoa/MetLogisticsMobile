using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MetallLogistic.Classes;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MetallLogistic.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrationPage : ContentPage
    {
        public RegistrationPage()
        {
            InitializeComponent();
        }

        private void BtnRegistration_Clicked(object sender, EventArgs e)
        {
            var errors = "";
            if (string.IsNullOrWhiteSpace(EntryLogin.Text)) errors += "Вы не ввели логин\r\n";
            if (string.IsNullOrWhiteSpace(EntryPassword.Text)) errors += "Вы не ввели пароль\r\n";
            if (string.IsNullOrWhiteSpace(EntryCompanyName.Text)) errors += "Вы не ввели название компании\r\n";

            if (errors.Length == 0)
            {
                try
                {
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    var newClient = client.PostAsync($"{AppData.ConectionString}Clients?login={EntryLogin.Text}&password=" +
                        $"{EntryPassword.Text}&companyName={EntryCompanyName.Text}", null); //Будет работать?
                    Navigation.PopAsync();
                }
                catch 
                {
                    Toast.MakeText(Android.App.Application.Context, "У нас случилась ошибка, возможно такой логин или название компании уже существуют", ToastLength.Long).Show();

                }

            }
            else
                Toast.MakeText(Android.App.Application.Context, errors, ToastLength.Long).Show();
        }

        //private void ToolbarItem_Clicked(object sender, EventArgs e)
        //{
        //    Navigation.PopAsync();
        //}

    }
}