using System;
using System.Windows;
using System.Windows.Controls;
using FiguresLibrary;

namespace FiguresApp
{
    public partial class AddFigureDialog : Window
    {
        public AddFigureDialog()
        {
            InitializeComponent();
            TypeComboBox.Items.Add("Окружность");
            TypeComboBox.Items.Add("Многоугольник");
            TypeComboBox.SelectedIndex = 0;
        }

        private void TypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CirclePanel.Visibility = TypeComboBox.SelectedIndex == 0 ? Visibility.Visible : Visibility.Collapsed;
            PolygonPanel.Visibility = TypeComboBox.SelectedIndex == 1 ? Visibility.Visible : Visibility.Collapsed;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string color = ColorTextBox.Text.Trim();
                if (string.IsNullOrEmpty(color))
                {
                    MessageBox.Show("Введите цвет фигуры");
                    return;
                }

                if (TypeComboBox.SelectedIndex == 0) // Окружность
                {
                    int centerX = int.Parse(CenterXTextBox.Text);
                    int centerY = int.Parse(CenterYTextBox.Text);
                    int edgeX = int.Parse(EdgeXTextBox.Text);
                    int edgeY = int.Parse(EdgeYTextBox.Text);

                    _figure = new Circle("окружность", color,
                        new FiguresLibrary.Point(centerX, centerY),
                        new FiguresLibrary.Point(edgeX, edgeY));
                }
                else // Многоугольник
                {
                    int sideLength = int.Parse(SideLengthTextBox.Text);
                    int sidesCount = int.Parse(SidesCountTextBox.Text);

                    if (sidesCount < 3)
                    {
                        MessageBox.Show("Количество сторон должно быть не менее 3");
                        return;
                    }

                    int[] sides = new int[sidesCount];
                    for (int i = 0; i < sidesCount; i++)
                    {
                        sides[i] = sideLength;
                    }

                    _figure = new RegularPolygons("многоугольник", color, sides);
                }

                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private Figure _figure;
        public Figure GetFigure() => _figure;
    }
}