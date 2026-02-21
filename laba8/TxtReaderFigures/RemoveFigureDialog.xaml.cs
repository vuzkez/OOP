using System.Collections.Generic;
using System.Linq;
using System.Windows;
using FiguresLibrary;

namespace FiguresApp
{
    public partial class RemoveFigureDialog : Window
    {
        public RemoveFigureDialog(List<Figure> figures)
        {
            InitializeComponent();
            FiguresComboBox.ItemsSource = figures;
            FiguresComboBox.DisplayMemberPath = "TypeFigure";
        }

        public Figure SelectedFigure => FiguresComboBox.SelectedItem as Figure;

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (FiguresComboBox.SelectedItem == null)
            {
                MessageBox.Show("Выберите фигуру для удаления");
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