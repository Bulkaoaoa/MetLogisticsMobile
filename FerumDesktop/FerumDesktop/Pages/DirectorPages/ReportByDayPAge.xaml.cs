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

namespace FerumDesktop.Pages.DirectorPages
{
    /// <summary>
    /// Interaction logic for ReportByDayPAge.xaml
    /// </summary>
    public partial class ReportByDayPAge : Page
    {
        public ReportByDayPAge()
        {
            InitializeComponent();
            List<ReportShipmend> reportShipmends = new List<ReportShipmend>();
            List<StepOfOrder> stepOfOrdersShipmentToDay = AppData.Context.StepOfOrder.ToList().Where(p => p.DateOfEnd != null).ToList()
                .Where(p => p.DateOfEnd.Value.Date == DateTime.Now.Date && p.Shipment != null).ToList();
            Random random = new Random();
            List<Trouble> troubles = AppData.Context.Trouble.ToList();
            List<TroubleReport> troublesReport = new List<TroubleReport>();
            foreach (var item in stepOfOrdersShipmentToDay.GroupBy(p => p.NumenclatureName))
            {
                reportShipmends.Add(new ReportShipmend
                {
                    NumenclatureName = item.Key,
                    TotalCount = stepOfOrdersShipmentToDay.Where(p => p.NumenclatureName == item.Key).ToList().Sum(p => p.Shipment.Count),
                    AbsEffective = Convert.ToDecimal(random.Next(70, 101))
                });
            }
            DgReportDay.ItemsSource = reportShipmends;
            foreach (var item in troubles)
            {
                Warehouse warehouse = null;
                if (item.StepOfOrder.Shipment != null)
                {
                    warehouse = item.StepOfOrder.Shipment.NomenclatureOfWarehouse.Cell.Warehouse;
                }
                troublesReport.Add(new TroubleReport
                {
                    Description = item.Description,
                    TypeOfTroubleName = item.TypeOfTrouble.Name,
                    WarehouseName = warehouse == null ? null : warehouse.Name
                });
            }
            DgDaysTrouble.ItemsSource = troublesReport;
        }

        public class ReportShipmend
        {
            public string NumenclatureName { get; set; }
            public decimal TotalCount { get; set; }

            public decimal AbsEffective { get; set; }
        }
        public class TroubleReport
        {
            public string Description { get; set; }
            public String TypeOfTroubleName { get; set; }

            public String WarehouseName { get; set; }
        }
    }
}
