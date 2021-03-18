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
    /// Interaction logic for ReportProblemPage.xaml
    /// </summary>
    public partial class ReportProblemPage : Page
    {
        private TypeOfTrouble _selectTypeOfTrouble;
        private readonly StepOfOrder _stepOfOrder;
        public ReportProblemPage(StepOfOrder stepOfOrder)
        {
            InitializeComponent();
            _stepOfOrder = stepOfOrder;
            Load();
        }

        private void Load()
        {
            try
            {
                ICTypeOfProblems.ItemsSource = AppData.Context.TypeOfTrouble.ToList();
            }
            catch (Exception)
            {
                AppData.Message.MessageCatch();
            }
        }

        private void BtnSend_Click(object sender, RoutedEventArgs e)
        {
            if (_selectTypeOfTrouble == null && string.IsNullOrWhiteSpace(TbxDescription.Text))
            {
                AppData.Message.MessageError("Заполните данные");
            }
            else if (_selectTypeOfTrouble == null)
            {
                AppData.Message.MessageError("Выберите тип проблемы");
            }
            else if (string.IsNullOrWhiteSpace(TbxDescription.Text))
            {
                AppData.Message.MessageError("Введите описание проблемы");
            }
            else
            {
                AppData.Context.Trouble.Add(new Trouble
                {
                    Description = TbxDescription.Text,
                    TypeOfTrouble = _selectTypeOfTrouble,
                    StepOfOrder = _stepOfOrder
                });
                AppData.Context.SaveChanges();

                AppData.Message.MessageInfo("Проблема создана и скоро будет отправлена на рассмотрение диспетчеру.");
                AppData.MainFrame.GoBack();
            }
        }

        private void RBtnProblem_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton RBtn = sender as RadioButton;
            if (RBtn.IsChecked == true)
                _selectTypeOfTrouble = RBtn.DataContext as TypeOfTrouble;
        }
    }
}
