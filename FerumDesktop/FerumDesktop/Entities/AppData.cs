using FerumDesktop.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace FerumDesktop.Entities
{
    public class AppData
    {
        public static Frame MainFrame;
        public static FerumEntities Context = new FerumEntities();
        public static MessagesClass Message = new MessagesClass();
        public static User CurrentUser;
    }
}
