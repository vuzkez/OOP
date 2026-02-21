using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using TicketLibrary;

namespace TicketApp
{
    public partial class MainWindow : Window
    {
        private TicketManager _ticketManager;
        private string[] _cities = { "Гомель", "Речица", "Светлогорск", "Жлобин", "Бобруйск" };

        public MainWindow()
        {
            InitializeComponent();
            InitializeTransports();
            LoadCities();
            UpdateInfo();
        }

        private void InitializeTransports()
        {
            var transports = new Dictionary<string, Transport>
            {
                { "Ford", new Ford(100, 5) },
                { "Mercedes", new Mercedes(150, 3) },
                { "Man", new Man(120, 4) },
                { "Gaz", new Gaz(80, 6) }
            };

            _ticketManager = new TicketManager(transports);

            cmbTransport.ItemsSource = transports;
            cmbTransport.SelectedIndex = 0;
        }

        private void LoadCities()
        {
            cmbFrom.ItemsSource = _cities;
            cmbTo.ItemsSource = _cities;

            cmbFrom.SelectedIndex = 0;
            cmbTo.SelectedIndex = 1;
        }

        private void UpdateInfo()
        {
            if (cmbTransport.SelectedItem == null ||
                cmbFrom.SelectedItem == null ||
                cmbTo.SelectedItem == null)
                return;

            var pair = (KeyValuePair<string, Transport>)cmbTransport.SelectedItem;

            string transportType = pair.Key;
            string from = cmbFrom.SelectedItem.ToString();
            string to = cmbTo.SelectedItem.ToString();

            int availableSeats = _ticketManager.GetAvailableSeats(transportType, from, to);

            txtAvailableSeats.Text =
                $"Маршрут: {from} → {to}\n" +
                $"Транспорт: {transportType}\n" +
                $"Всего мест: {pair.Value.CountSeats}\n" +
                $"Свободно мест: {availableSeats}\n" +
                $"Продано билетов: {GetSoldCountForTransport(pair.Value)}";
        }

        private int GetSoldCountForTransport(Transport transport)
        {
            return _ticketManager
                .GetSoldTickets()
                .Count(t => t.Transport == transport);
        }

        private void btnSellTicket_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateSelection()) return;

            var pair = (KeyValuePair<string, Transport>)cmbTransport.SelectedItem;
            string from = cmbFrom.SelectedItem.ToString();
            string to = cmbTo.SelectedItem.ToString();

            if (_ticketManager.SellTicket(pair.Key, from, to))
            {
                MessageBox.Show("Билет успешно продан!", "Успех",
                    MessageBoxButton.OK, MessageBoxImage.Information);

                UpdateInfo();
            }
            else
            {
                MessageBox.Show("Нет доступных мест на всём маршруте!",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnRemoveTicket_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateSelection()) return;

            var pair = (KeyValuePair<string, Transport>)cmbTransport.SelectedItem;
            string from = cmbFrom.SelectedItem.ToString();
            string to = cmbTo.SelectedItem.ToString();

            var confirm = MessageBox.Show(
                $"Удалить билет?\nМаршрут: {from} → {to}\nТранспорт: {pair.Key}",
                "Подтверждение",
                MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (confirm == MessageBoxResult.Yes)
            {
                if (_ticketManager.RemoveTicket(pair.Key, from, to))
                {
                    MessageBox.Show("Билет удалён.",
                        "Удалено", MessageBoxButton.OK, MessageBoxImage.Information);
                    UpdateInfo();
                }
                else
                {
                    MessageBox.Show("Такого билета нет.",
                        "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        private void btnCheckSeats_Click(object sender, RoutedEventArgs e)
        {
            UpdateInfo();
        }

        private bool ValidateSelection()
        {
            if (cmbTransport.SelectedItem == null ||
                cmbFrom.SelectedItem == null ||
                cmbTo.SelectedItem == null)
            {
                MessageBox.Show("Выберите транспорт и станции!");
                return false;
            }
            return true;
        }

        private void btnShowAllRoutes_Click(object sender, RoutedEventArgs e)
        {
            var transports = _ticketManager.GetTransports();

            string[] cities = _cities;

            var routes = new List<object>();

            foreach (var pair in transports)
            {
                string transportName = pair.Key;

                for (int i = 0; i < cities.Length; i++)
                {
                    for (int j = i + 1; j < cities.Length; j++)
                    {
                        string from = cities[i];
                        string to = cities[j];

                        // Считаем доступные места по сегментам
                        int available = _ticketManager.GetAvailableSeats(transportName, from, to);

                        if (available > 0) // показываем только возможные маршруты
                        {
                            routes.Add(new
                            {
                                TransportName = transportName,
                                From = from,
                                To = to,
                                Available = available
                            });
                        }
                    }
                }
            }

            if (routes.Count == 0)
            {
                MessageBox.Show("Нет ни одного маршрута, на который можно продать билет.",
                                "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var wnd = new AvailableRoutesWindow(routes)
            {
                Owner = this
            };

            wnd.ShowDialog();
        }

        private void btnEditRoute_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateSelection()) return;

            var pair = (KeyValuePair<string, Transport>)cmbTransport.SelectedItem;

            string oldFrom = cmbFrom.SelectedItem.ToString();
            string oldTo = cmbTo.SelectedItem.ToString();

            var dlg = new EditRouteDialog(_cities);
            dlg.Owner = this;

            if (dlg.ShowDialog() == true)
            {
                bool success = _ticketManager.EditTicketRoute(
                    pair.Key,
                    oldFrom, oldTo,
                    dlg.NewFrom, dlg.NewTo
                );

                if (success)
                {
                    MessageBox.Show("Маршрут успешно изменён!",
                                    "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    UpdateInfo();
                }
                else
                {
                    MessageBox.Show("Невозможно изменить маршрут — нет свободных мест.",
                                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        private void btnEditTransport_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateSelection()) return;

            string oldTransport = ((KeyValuePair<string, Transport>)cmbTransport.SelectedItem).Key;
            string from = cmbFrom.SelectedItem.ToString();
            string to = cmbTo.SelectedItem.ToString();

            var dlg = new EditTransportDialog(_ticketManager.GetTransports().Keys.ToList());
            dlg.Owner = this;

            if (dlg.ShowDialog() == true)
            {
                bool success = _ticketManager.EditTicketTransport(
                    oldTransport,
                    dlg.NewTransport,
                    from, to
                );

                if (success)
                {
                    MessageBox.Show("Транспорт успешно изменён!",
                                    "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                    UpdateInfo();
                }
                else
                {
                    MessageBox.Show("Невозможно перенести билет на этот транспорт.",
                                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }


        private void cmbTransport_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateInfo();
        }

        private void cmbFrom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateInfo();
        }

        private void cmbTo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateInfo();
        }
    }
}
