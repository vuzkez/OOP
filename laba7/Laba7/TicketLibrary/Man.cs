namespace TicketLibrary
{
    /// <summary>
    /// Представляет транспортное средство марки Man
    /// </summary>
    public class Man : Transport
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса Man
        /// </summary>
        /// <param name="price">Цена за проезд между соседними станциями</param>
        /// <param name="countSeats">Количество мест в транспорте</param>
        public Man(int price, int countSeats) : base(price, countSeats) { }
    }
}