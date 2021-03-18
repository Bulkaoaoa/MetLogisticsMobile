using System;
using System.Collections.Generic;
using System.Text;

namespace MetallLogistic.Classes
{
    public class Order
    {
        public string Id { get; set; }
        public DateTime DateOfCreate { get; set; }
        public int ClientId { get; set; }
        public int CourierId { get; set; }
        public decimal Effectivness { get; set; }
        public DateTime DateTimeOfArrivle { get; set; } //Вот это и три поля выше могут быть null

        public string FullOrderId
        {
            get
            {
                return $"Заказ №{Id}";
            }
        }
        public string StartFullOrderId
        {
            get
            {
                return $"Начать заказ №{Id}";
            }
        }
    }
}
