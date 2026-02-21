using System.Windows;

namespace FiguresApp
{
    public partial class FindFigureDialog : Window
    {
        public FindFigureDialog()
        {
            InitializeComponent();
        }

        public string SearchType => TypeTextBox.Text;
        public string SearchColor => ColorTextBox.Text;

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TypeTextBox.Text) || string.IsNullOrEmpty(ColorTextBox.Text))
            {
                MessageBox.Show("Заполните все поля");
                return;
            }
            DialogResult = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}