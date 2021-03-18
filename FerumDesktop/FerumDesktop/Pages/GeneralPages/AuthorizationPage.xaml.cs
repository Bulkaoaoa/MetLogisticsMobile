using FerumDesktop.Classes;
using FerumDesktop.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FerumDesktop.Pages.GeneralPages
{
    /// <summary>
    /// Interaction logic for AuthorizationPage.xaml
    /// </summary>
    public partial class AuthorizationPage : Page
    {
        private readonly GetHashString GetHash = new GetHashString();
        public AuthorizationPage()
        {
            InitializeComponent();
            TbxLogin.Focus();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(TbxLogin.Text) && string.IsNullOrWhiteSpace(PbxPassword.Password))
                {
                    AppData.Message.MessageError("Введите данные");
                }
                else if (string.IsNullOrWhiteSpace(TbxLogin.Text))
                {
                    AppData.Message.MessageError("Введите логин");
                    TbxLogin.Focus();
                }
                else if (string.IsNullOrWhiteSpace(PbxPassword.Password))
                {
                    AppData.Message.MessageError("Введите пароль");
                    PbxPassword.Focus();
                }
                else if (AppData.Context.User.ToList().FirstOrDefault(i =>
                i.Login == GetHash.GetGuid(TbxLogin.Text)) == null)
                {
                    AppData.Message.MessageError("Пользователь не найден. Впишите другое имя");
                    TbxLogin.Focus();
                }
                else
                {
                    AppData.CurrentUser = AppData.Context.User.ToList().FirstOrDefault(i =>
                    i.Login == GetHash.GetGuid(TbxLogin.Text) && i.Password == GetHash.GetGuid(PbxPassword.Password));

                    switch (AppData.CurrentUser.Role.Name)
                    {
                        case "Storekeeper":
                            AppData.MainFrame.Navigate(new StorekeeperPages.MainPage());
                            break;
                        case "Dispatcher":
                            AppData.MainFrame.Navigate(new DispatcherPages.MainPage());
                            break;
                        case "Director":
                            AppData.MainFrame.Navigate(new DirectorPages.MainPage());
                            break;
                        default:
                            AppData.Message.MessageInfo("Функционал для этой роли ещё не реализован");
                            break;
                    }
                }
            }
            catch (Exception)
            {
                AppData.Message.MessageCatch();
            }
        }
    }
}
