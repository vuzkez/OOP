using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketLibrary
{
    /// <summary>
    /// Представляет транспортное средство марки Ford
    /// </summary>
    public class Ford : Transport
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса Ford
        /// </summary>
        /// <param name="price">Цена за проезд между соседними станциями</param>
        /// <param name="countSeats">Количество мест в транспорте</param>
        public Ford(int price, int countSeats) : base(price, countSeats) { }
    }
}
