using FerumDesktop.Entities;
using FerumDesktop.Pages.GeneralPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace FerumDesktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            AppData.MainFrame = MainFrame;
            AppData.MainFrame.Navigate(new AuthorizationPage());
            Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.GetCultureInfo("ru-RU");
        }

        private void MainFrame_ContentRendered(object sender, EventArgs e)
        {
            if (AppData.MainFrame.Content is Page page)
            {
                if (page is AuthorizationPage)
                    ImgBack.Visibility = Visibility.Hidden;
                else
                    ImgBack.Visibility = Visibility.Visible;
            }
        }

        private void ImgBack_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (AppData.MainFrame.Content is Page page)
            {
                // не робит скорее всего, я в дебаге у page не увидел имя
                if (page.Title.Contains("main menu"))
                {
                    if (AppData.Message.MessageQuestion("Вы уверены, что хотите выйти?") == MessageBoxResult.Yes)
                    {
                        AppData.CurrentUser = null;
                        AppData.MainFrame.GoBack();
                    }
                }
            }

            if (AppData.MainFrame.CanGoBack)
                AppData.MainFrame.GoBack();
        }
    }
}
