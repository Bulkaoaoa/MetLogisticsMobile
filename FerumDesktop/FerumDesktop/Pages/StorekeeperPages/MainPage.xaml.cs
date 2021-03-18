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

namespace FerumDesktop.Pages.StorekeeperPages
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        private StepOfOrder _stepOfOrder;
        private List<StepOfOrder> QueueList;
        public MainPage()
        {
            InitializeComponent();
        }


        private void Load()
        {
            try
            {
                if (_stepOfOrder == null)
                {
                    TbkNoOrders.Visibility = Visibility.Visible;
                    SPOrders.Visibility = Visibility.Hidden;
                }
                else
                {
                    TbkNoOrders.Visibility = Visibility.Hidden;
                    SPOrders.Visibility = Visibility.Visible;
                    DataContext = _stepOfOrder.Order;
                }
            }
            catch (Exception)
            {
                AppData.Message.MessageCatch();
            }
        }
        private void BtnConfirmShipment_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TbkProblem_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            AppData.MainFrame.Navigate(new ReportProblemPage(_stepOfOrder));
        }

        private void ImgInfo_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void BtnQueueOfCars_Click(object sender, RoutedEventArgs e)
        {
            if (QueueList.Count == 0)
                AppData.Message.MessageInfo("Никого нет в очереди. Попробуйте обновить данные");
            else
                AppData.Message.MessageInfo($"Страница очереди ещё не готова.\nОчередь на кран: {QueueList.Count()}");
        }

        private void BtnRefreshData_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<StepOfOrder> StepOfOrders = AppData.Context.StepOfOrder.ToList()
                    .Where(i => i.ProcessId == 3 && i.isDone == true).ToList().Where(i =>
                    i.Movement.Warehouse == AppData.CurrentUser.Storekeeper.Warehouse.Last()).ToList();

                QueueList = new List<StepOfOrder>();
                List<StepOfOrder> StepsList = new List<StepOfOrder>();

                if (StepOfOrders.Count > 0)
                {
                    foreach (var step in StepOfOrders)
                    {
                        int index = AppData.Context.StepOfOrder.ToList().IndexOf(step) + 1;

                        if (AppData.Context.StepOfOrder.ToList()[index].isDone == false)
                        {
                            QueueList.Add(AppData.Context.StepOfOrder.ToList()[index]);
                        }
                        else
                        {
                            StepsList = AppData.Context.StepOfOrder.ToList().Where(i => i.Order == step.Order).ToList();
                            int index2 = StepsList.IndexOf(step) + 2;

                            if (StepsList[index2].isDone == false)
                                _stepOfOrder = StepsList[index2];
                        }
                    }
                }
                Load();
            }
            catch (Exception)
            {
                AppData.Message.MessageCatch();
            }
        }
    }
}
