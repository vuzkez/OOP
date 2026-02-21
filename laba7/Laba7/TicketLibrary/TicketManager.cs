using System;
using System.Collections.Generic;
using System.Linq;

namespace TicketLibrary
{
    /// <summary>
    /// Управляет продажей билетов и отслеживанием доступных мест
    /// </summary>
    public class TicketManager
    {
        /// <summary>
        /// Список доступных транспортных средств, где ключ — имя модели транспорта.
        /// </summary>
        private readonly Dictionary<string, Transport> _transports;

        /// <summary>
        /// Хранит все проданные билеты.
        /// </summary>
        private readonly List<Ticket> _soldTickets;

        public TicketManager(Dictionary<string, Transport> transports)
        {
            _transports = transports;
            _soldTickets = new List<Ticket>();
        }

        /// <summary>
        /// Продает билет на указанный транспорт между станциями
        /// </summary>
        public bool SellTicket(string transportType, string from, string to)
        {
            if (!_transports.ContainsKey(transportType))
                return false;

            var transport = _transports[transportType];
            var ticket = new Ticket(from, to, transport);

            if (CanSellTicket(ticket))
            {
                _soldTickets.Add(ticket);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Проверяет можно ли продать билет с учетом занятых сегментов маршрута
        /// </summary>
        private bool CanSellTicket(Ticket newTicket)
        {
            var transport = newTicket.Transport;
            var newSegments = newTicket.GetSegments();

            // выбираем билеты только этого транспорта
            var tickets = _soldTickets.Where(t => t.Transport == transport).ToList();

            foreach (var seg in newSegments)
            {
                int countOnSegment = tickets.Count(t =>
                    t.GetSegments().Any(s => s == seg));

                if (countOnSegment >= transport.CountSeats)
                    return false; // нет места на каком-то сегменте
            }

            return true;
        }

        /// <summary>
        /// Получает количество доступных мест между станциями
        /// </summary>
        public int GetAvailableSeats(string transportType, string from, string to)
        {
            if (!_transports.ContainsKey(transportType))
                return 0;

            var transport = _transports[transportType];
            int maxSeats = transport.CountSeats;

            var tempTickets = new List<Ticket>(_soldTickets);
            int result = 0;

            for (int i = 0; i < maxSeats; i++)
            {
                var testTicket = new Ticket(from, to, transport);

                if (CanSellTicketWithTempList(testTicket, tempTickets))
                {
                    tempTickets.Add(testTicket);
                    result++;
                }
                else
                {
                    break;
                }
            }

            return result;
        }

        /// <summary>
        /// Проверка продажи билета с временным списком билетов
        /// </summary>
        private bool CanSellTicketWithTempList(Ticket newTicket, List<Ticket> tempTickets)
        {
            var transport = newTicket.Transport;
            var newSegments = newTicket.GetSegments();

            var tickets = tempTickets.Where(t => t.Transport == transport).ToList();

            foreach (var seg in newSegments)
            {
                int countOnSegment = tickets.Count(t =>
                    t.GetSegments().Any(s => s == seg));

                if (countOnSegment >= transport.CountSeats)
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Удаляет билет
        /// </summary>
        public bool RemoveTicket(string transportType, string from, string to)
        {
            if (!_transports.ContainsKey(transportType))
                return false;

            var transport = _transports[transportType];

            var ticket = _soldTickets.FirstOrDefault(t =>
                t.Transport == transport &&
                t.From == from &&
                t.To == to);

            if (ticket != null)
            {
                _soldTickets.Remove(ticket);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Количество проданных билетов
        /// </summary>
        public int SoldTicketsCount => _soldTickets.Count;

        /// <summary>
        /// Возвращает список всех проданных билетов.
        /// </summary>
        public List<Ticket> GetSoldTickets() => _soldTickets;

        /// <summary>
        /// Возвращает словарь всех транспортов.
        /// </summary>
        public Dictionary<string, Transport> GetTransports() => _transports;

        /// <summary>
        /// Изменяет маршрут уже проданного билета, если это возможно.
        /// </summary>
        /// <param name="transportType">Тип транспорта.</param>
        /// <param name="oldFrom">Старый пункт отправления.</param>
        /// <param name="oldTo">Старый пункт назначения.</param>
        /// <param name="newFrom">Новый пункт отправления.</param>
        /// <param name="newTo">Новый пункт назначения.</param>
        /// <returns>true — маршрут изменён; false — не удалось изменить.</returns>
        public bool EditTicketRoute(string transportType, string oldFrom, string oldTo,string newFrom, string newTo)
        {
            if (!_transports.ContainsKey(transportType))
                return false;

            var transport = _transports[transportType];

            var ticket = _soldTickets.FirstOrDefault(t =>
                t.Transport == transport &&
                t.From == oldFrom &&
                t.To == oldTo);

            if (ticket == null)
                return false;

            var newTicket = new Ticket(newFrom, newTo, transport);

            if (!CanSellTicket(newTicket))
                return false;

            _soldTickets.Remove(ticket);
            _soldTickets.Add(newTicket);

            return true;
        }

        /// <summary>
        /// Переносит билет с одного транспорта на другой (если возможно).
        /// </summary>
        /// <param name="oldTransportType">Название старого транспорта.</param>
        /// <param name="newTransportType">Название нового транспорта.</param>
        /// <param name="from">Откуда.</param>
        /// <param name="to">Куда.</param>
        /// <returns>true — билет перенесён; false — не удалось изменить транспорт.</returns>
        public bool EditTicketTransport(string oldTransportType, string newTransportType,string from, string to)
        {
            if (!_transports.ContainsKey(oldTransportType) ||
                !_transports.ContainsKey(newTransportType))
                return false;

            var oldTransport = _transports[oldTransportType];
            var newTransport = _transports[newTransportType];

            var ticket = _soldTickets.FirstOrDefault(t =>
                t.Transport == oldTransport &&
                t.From == from &&
                t.To == to);

            if (ticket == null)
                return false;

            var newTicket = new Ticket(from, to, newTransport);

            if (!CanSellTicket(newTicket))
                return false;

            _soldTickets.Remove(ticket);
            _soldTickets.Add(newTicket);

            return true;
        }

    }
}
