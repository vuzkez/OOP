using TicketLibrary;

namespace TicketTests
{
    [TestClass]
    public class TicketTests
    {
        [TestMethod]
        public void TransportCreation_ValidParameters_CreatesSuccessfully()
        {
            // Arrange & Act
            var transport = new Mercedes(100, 5);

            // Assert
            Assert.AreEqual(100, transport.Price);
            Assert.AreEqual(5, transport.CountSeats);
        }

        [TestMethod]
        [ExpectedException(typeof(System.Exception))]
        public void TransportCreation_ZeroSeats_ThrowsException()
        {
            // Arrange & Act & Assert
            var transport = new Ford(100, 0);
        }

        [TestMethod]
        public void TicketPriceCalculation_CorrectDistance_ReturnsCorrectPrice()
        {
            // Arrange
            var transport = new Gaz(50, 10);
            var ticket = new Ticket("Гомель", "Речица", transport);

            // Act
            int price = ticket.Price;

            // Assert
            Assert.AreEqual(50, price); // 1 станция * 50
        }

        [TestMethod]
        public void TicketPriceCalculation_LongDistance_ReturnsCorrectPrice()
        {
            // Arrange
            var transport = new Man(30, 10);
            var ticket = new Ticket("Гомель", "Жлобин", transport);

            // Act
            int price = ticket.Price;

            // Assert
            Assert.AreEqual(90, price); // 3 станции * 30
        }

        [TestMethod]
        public void GetRouteStations_ValidCities_ReturnsCorrectStations()
        {
            // Arrange
            var transport = new Ford(100, 5);
            var ticket = new Ticket("Речица", "Жлобин", transport);

            // Act
            var stations = ticket.GetRouteStations();

            // Assert
            Assert.AreEqual(3, stations.Count);
            Assert.AreEqual("Речица", stations[0]);
            Assert.AreEqual("Светлогорск", stations[1]);
            Assert.AreEqual("Жлобин", stations[2]);
        }

        [TestMethod]
        public void GetRouteStations_ReverseOrder_ReturnsCorrectStations()
        {
            // Arrange
            var transport = new Mercedes(100, 5);
            var ticket = new Ticket("Жлобин", "Речица", transport);

            // Act
            var stations = ticket.GetRouteStations();

            // Assert
            Assert.AreEqual(3, stations.Count);
            Assert.AreEqual("Речица", stations[0]);
            Assert.AreEqual("Светлогорск", stations[1]);
            Assert.AreEqual("Жлобин", stations[2]);
        }
    }

    [TestClass]
    public class TicketManagerTests
    {
        private Dictionary<string, Transport> _transports;
        private TicketManager _ticketManager;

        [TestInitialize]
        public void TestInitialize()
        {
            _transports = new Dictionary<string, Transport>
            {
                { "Mercedes", new Mercedes(100, 2) }, // Всего 2 места
                { "Ford", new Ford(80, 3) }           // Всего 3 места
            };

            _ticketManager = new TicketManager(_transports);
        }

        [TestMethod]
        public void SellTicket_FirstTicket_Success()
        {
            // Act
            bool result = _ticketManager.SellTicket("Mercedes", "Гомель", "Речица");

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GetAvailableSeats_EmptyTransport_ReturnsAllSeats()
        {
            // Act
            int availableSeats = _ticketManager.GetAvailableSeats("Ford", "Гомель", "Бобруйск");

            // Assert
            Assert.AreEqual(3, availableSeats);
        }

        [TestMethod]
        public void SellTicket_InvalidTransportType_Fails()
        {
            // Act
            bool result = _ticketManager.SellTicket("InvalidTransport", "Гомель", "Речица");

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GetAvailableSeats_InvalidTransportType_ReturnsZero()
        {
            // Act
            int availableSeats = _ticketManager.GetAvailableSeats("InvalidTransport", "Гомель", "Речица");

            // Assert
            Assert.AreEqual(0, availableSeats);
        }
    }
}