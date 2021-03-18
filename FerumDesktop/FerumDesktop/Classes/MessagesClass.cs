using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FerumDesktop.Classes
{
    public class MessagesClass
    {
        public MessageBoxResult MessageError(string message)
        {
            return MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public MessageBoxResult MessageInfo(string message)
        {
            return MessageBox.Show(message, "Information", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public MessageBoxResult MessageCatch()
        {
            return MessageBox.Show("Отсутствует соединение с базой данных. Пожалуйста, свяжитесь с системным администратором", 
                "Information", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public MessageBoxResult MessageWarning(string message)
        {
            return MessageBox.Show(message, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        public MessageBoxResult MessageQuestion(string message)
        {
            return MessageBox.Show(message, "Warning", MessageBoxButton.YesNo, MessageBoxImage.Question);
        }
    }
}
