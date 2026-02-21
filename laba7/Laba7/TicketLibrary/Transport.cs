using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketLibrary
{
    /// <summary>
    /// Базовый класс, представляющий транспортное средство
    /// </summary>
    public class Transport
    {
        private int _countSeats;
        private int _price;

        /// <summary>
        /// Инициализирует новый экземпляр класса Transport
        /// </summary>
        /// <param name="price">Цена за проезд между соседними станциями</param>
        /// <param name="countSeats">Количество мест в транспорте</param>
        /// <exception cref="Exception">Выбрасывается, когда количество мест меньше или равно 0</exception>
        public Transport(int price, int countSeats)
        {
            _price = price <= 0 ? 1 : price;
            _countSeats = countSeats <= 0 ? throw new Exception("Count seats <= 0") : countSeats;
        }

        /// <summary>
        /// Получает количество мест в транспорте
        /// </summary>
        public int CountSeats => _countSeats;

        /// <summary>
        /// Получает цену за проезд между соседними станциями
        /// </summary>
        public int Price => _price;
    }
}
