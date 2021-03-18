using System;
using System.Collections.Generic;
using System.Text;

namespace MetallLogistic.Classes
{
    public class StepOfOrder
    {
        public int Id { get; set; }
        public bool isDone { get; set; }
        public Nullable<int> TimeSpent { get; set; }
        public Nullable<System.DateTime> DateOfStart { get; set; }
        public Nullable<System.DateTime> DateOfEnd { get; set; }
        public Movement Movement { get; set; }
        public Order Order { get; set; }
        public Proccess Proccess { get; set; }
        public Shipment Shipment { get; set; }
        public List<Trouble> Trouble
        {
            get; set;
        }

        public string ImagePath
        {
            get
            {
                switch (Proccess.Name)
                {
                    case "Ожидание за воротами":
                        return "WaitingBehindGates.png";
                    case "Проезд за ворота":
                        return "MovingThroghtGates.png";
                    case "Перемещение на склад":
                        return "MovingToWareHouse.png";
                    case "Погрузка":
                        return "LoadingThings.png";
                    case "Ожидание погрузки":
                        return "WaitingForLoad.png"; 
                    case "Ожидание выезда":
                        return "WaitingForExit.png";
                    case "Выезд с территории":
                        return "Delivering.png";
                    case "Перемещение на выезд":
                        return "MovingToExit.png";
                    default:
                        return "FixingProblems.png";
                }
            }
        }

        public string FullStepText
        {
            get
            {
                switch (Proccess.Name)
                {
                    case "Ожидание за воротами":
                        return "Жду за воротами...";
                    case "Проезд за ворота":
                        return "Проезжаю через ворота...";
                    case "Перемещение на склад":
                        return $"Еду на склад №{Movement.Warehouse.Name}...";
                    case "Погрузка":
                        return $"Погрузка {Shipment.Count} штрихкодов...";
                    case "Ожидание погрузки":
                        return "Жду погрузки...";
                    case "Ожидание выезда":
                        return "Жду выезда...";
                    case "Выезд с территории":
                        return "Выезжаю со склада...";
                    case "Перемещение на выезд":
                        return "Еду на выезд...";
                    default:
                        return "Что то пошло не так...";
                }
            }
        }
    }
}
