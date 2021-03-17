using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            //Сделать проверку существует ли такой логин уже
            if (string.IsNullOrWhiteSpace(EntryPassword.Text)) errors += "Вы не ввели пароль\r\n";
            if (string.IsNullOrWhiteSpace(EntryCompanyName.Text)) errors += "Вы не ввели название компании\r\n";
            //Сделать проверку существует ли такое название компании

            if (errors.Length == 0)
            {
                //Регистрация юзера
                //Переход назад(На авторизацию)
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