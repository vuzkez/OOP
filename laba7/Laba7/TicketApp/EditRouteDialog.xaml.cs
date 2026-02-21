using System.Windows;

namespace TicketApp
{
    public partial class EditRouteDialog : Window
    {
        public string NewFrom { get; private set; }
        public string NewTo { get; private set; }

        public EditRouteDialog(string[] cities)
        {
            InitializeComponent();

            cmbNewFrom.ItemsSource = cities;
            cmbNewTo.ItemsSource = cities;

            cmbNewFrom.SelectedIndex = 0;
            cmbNewTo.SelectedIndex = 1;
        }

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            if (cmbNewFrom.SelectedItem == null || cmbNewTo.SelectedItem == null)
            {
                MessageBox.Show("Выберите станции.");
                return;
            }

            if (cmbNewFrom.SelectedItem.ToString() == cmbNewTo.SelectedItem.ToString())
            {
                MessageBox.Show("Станции должны быть разными.");
                return;
            }

            NewFrom = cmbNewFrom.SelectedItem.ToString();
            NewTo = cmbNewTo.SelectedItem.ToString();

            DialogResult = true;
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
