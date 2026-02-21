using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace TicketLibrary
{
    /// <summary>
    /// Представляет транспортное средство марки Gaz
    /// </summary>
    public class Gaz : Transport
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса Gaz
        /// </summary>
        /// <param name="price">Цена за проезд между соседними станциями</param>
        /// <param name="countSeats">Количество мест в транспорте</param>
        public Gaz(int price, int countSeats) : base(price, countSeats) { }
    }
}
