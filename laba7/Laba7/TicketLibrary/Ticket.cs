
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketLibrary;

namespace TicketLibrary
{
    /// <summary>
    /// Представляет билет на транспорт между станциями
    /// </summary>
    public class Ticket
    {
        private static readonly string[] Cities = { "Гомель", "Речица", "Светлогорск", "Жлобин", "Бобруйск" };

        /// <summary>
        /// Получает станцию отправления
        /// </summary>
        public string From { get; }

        /// <summary>
        /// Получает станцию назначения
        /// </summary>
        public string To { get; }

        /// <summary>
        /// Получает транспортное средство
        /// </summary>
        public Transport Transport { get; }

        /// <summary>
        /// Получает расчетную цену билета
        /// </summary>
        public int Price => CalculatePrice();

        /// <summary>
        /// Инициализирует новый экземпляр класса Ticket
        /// </summary>
        /// <param name="from">Станция отправления</param>
        /// <param name="to">Станция назначения</param>
        /// <param name="transport">Транспортное средство</param>
        public Ticket(string from, string to, Transport transport)
        {
            From = from;
            To = to;
            Transport = transport;
        }

        /// <summary>
        /// Считает цену данного билета
        /// </summary>
        /// <returns>Цена за транспорт и дальность дистанции</returns>
        private int CalculatePrice()
        {
            int fromIndex = Array.IndexOf(Cities, From);
            int toIndex = Array.IndexOf(Cities, To);

            if (fromIndex == -1 || toIndex == -1)
                return 0;

            int stationsCount = Math.Abs(toIndex - fromIndex);
            return Transport.Price * stationsCount;
        }

        /// <summary>
        /// Получает список станций маршрута между From и To
        /// </summary>
        /// <returns>Список станций маршрута</returns>
        public List<string> GetRouteStations()
        {
            var stations = new List<string>();
            int fromIndex = Array.IndexOf(Cities, From);
            int toIndex = Array.IndexOf(Cities, To);

            if (fromIndex == -1 || toIndex == -1) 
                return stations;

            int start = Math.Min(fromIndex, toIndex);
            int end = Math.Max(fromIndex, toIndex);

            for (int i = start; i <= end; i++)
            {
                stations.Add(Cities[i]);
            }

            return stations;
        }

        /// <summary>
        /// Получает список занятых отрезков маршрута (сегментов)
        /// </summary>
        /// <returns>Список пар (fromIndex, toIndex)</returns>
        public List<(int from, int to)> GetSegments()
        {
            int fromIndex = Array.IndexOf(Cities, From);
            int toIndex = Array.IndexOf(Cities, To);

            var segments = new List<(int, int)>();

            if (fromIndex == -1 || toIndex == -1)
                return segments;

            int start = Math.Min(fromIndex, toIndex);
            int end = Math.Max(fromIndex, toIndex);

            for (int i = start; i < end; i++)
            {
                segments.Add((i, i + 1));
            }

            return segments;
        }

    }
}
