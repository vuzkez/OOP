using System.Collections.Generic;
using System.Windows;

namespace TicketApp
{
    public partial class EditTransportDialog : Window
    {
        public string NewTransport { get; private set; }

        public EditTransportDialog(List<string> transportNames)
        {
            InitializeComponent();

            cmbTransport.ItemsSource = transportNames;
            cmbTransport.SelectedIndex = 0;
        }

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            if (cmbTransport.SelectedItem == null)
            {
                MessageBox.Show("Выберите транспорт.");
                return;
            }

            NewTransport = cmbTransport.SelectedItem.ToString();
            DialogResult = true;
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
