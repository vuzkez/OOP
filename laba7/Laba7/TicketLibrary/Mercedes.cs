using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketLibrary
{
    /// <summary>
    /// Представляет транспортное средство марки Mercedes
    /// </summary>
    public class Mercedes : Transport
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса Mercedes
        /// </summary>
        /// <param name="price">Цена за проезд между соседними станциями</param>
        /// <param name="countSeats">Количество мест в транспорте</param>
        public Mercedes(int price, int countSeats) : base(price, countSeats) { }
    }
}
