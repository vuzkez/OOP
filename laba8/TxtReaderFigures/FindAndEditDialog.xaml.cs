using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using FiguresLibrary;

namespace FiguresApp
{
    public partial class FindAndEditDialog : Window
    {
        private List<Figure> allFigures;
        private Figure foundFigure;

        public FindAndEditDialog(List<Figure> figures)
        {
            InitializeComponent();
            allFigures = figures;

            // Настройка начальных значений
            SearchTypeComboBox.SelectedIndex = 0;
            NewColorComboBox.SelectedIndex = 0;
            NewTypeComboBox.SelectedIndex = 0;
            NewSidesCount.SelectedIndex = 1; // 4 стороны (квадрат)
        }

        public Figure GetOriginalFigure => foundFigure;
        public Figure GetNewFigure { get; private set; }

        private void FindButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string searchType = (SearchTypeComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
                string searchColor = SearchColorTextBox.Text.Trim().ToLower();

                // Ищем первую подходящую фигуру
                foundFigure = allFigures.FirstOrDefault(f =>
                {
                    // Проверка типа
                    bool typeMatch = searchType == "Любой" ||
                                   (searchType == "Окружность" && f is Circle) ||
                                   (searchType == "Многоугольник" && f is RegularPolygons);

                    // Проверка цвета
                    bool colorMatch = string.IsNullOrEmpty(searchColor) ||
                                     f.ColorFigure.ToLower() == searchColor;

                    return typeMatch && colorMatch;
                });

                if (foundFigure != null)
                {
                    CurrentTypeText.Text = foundFigure.TypeFigure;
                    CurrentColorText.Text = foundFigure.ColorFigure;
                    CurrentAreaText.Text = foundFigure.GetArea().ToString("F2");

                    // Автоматически заполняем поля для редактирования
                    AutoFillEditFields();
                    ApplyButton.IsEnabled = true;
                }
                else
                {
                    ClearFigureInfo();
                    ApplyButton.IsEnabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка поиска: {ex.Message}", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AutoFillEditFields()
        {
            if (foundFigure is Circle circle)
            {
                // Устанавливаем тип "Окружность"
                NewTypeComboBox.SelectedIndex = 0;

                // Заполняем параметры окружности
                NewCenterX.Text = circle.GetCenter().x.ToString();
                NewCenterY.Text = circle.GetCenter().y.ToString();
                NewEdgeX.Text = circle.GetEdgePoint().x.ToString();
                NewEdgeY.Text = circle.GetEdgePoint().y.ToString();

                // Настраиваем видимость панелей
                CircleParamsGroup.Visibility = Visibility.Visible;
                PolygonParamsGroup.Visibility = Visibility.Collapsed;
            }
            else if (foundFigure is RegularPolygons polygon)
            {
                // Устанавливаем тип "Многоугольник"
                NewTypeComboBox.SelectedIndex = 1;

                // Определяем количество сторон
                int sidesCount = polygon.GetArea().ToString().Length > 0 ? 4 : 3; // Простое приближение
                SetComboBoxValue(NewSidesCount, sidesCount.ToString());

                // Настраиваем видимость панелей
                CircleParamsGroup.Visibility = Visibility.Collapsed;
                PolygonParamsGroup.Visibility = Visibility.Visible;
            }

            // Устанавливаем текущий цвет
            SetComboBoxColor(NewColorComboBox, foundFigure.ColorFigure);
        }

        private void NewTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedType = (NewTypeComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();

            if (selectedType == "Окружность")
            {
                CircleParamsGroup.Visibility = Visibility.Visible;
                PolygonParamsGroup.Visibility = Visibility.Collapsed;
            }
            else if (selectedType == "Многоугольник")
            {
                CircleParamsGroup.Visibility = Visibility.Collapsed;
                PolygonParamsGroup.Visibility = Visibility.Visible;
            }
        }

        private void ApplyButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (foundFigure == null)
                {
                    MessageBox.Show("Сначала найдите фигуру для замены", "Ошибка",
                                  MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Создаем новую фигуру на основе выбранных параметров
                string newColor = (NewColorComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
                string newType = (NewTypeComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();

                if (newType == "Окружность")
                {
                    int centerX = int.Parse(NewCenterX.Text);
                    int centerY = int.Parse(NewCenterY.Text);
                    int edgeX = int.Parse(NewEdgeX.Text);
                    int edgeY = int.Parse(NewEdgeY.Text);

                    GetNewFigure = new Circle("окружность", newColor,
                        new FiguresLibrary.Point(centerX, centerY),
                        new FiguresLibrary.Point(edgeX, edgeY));
                }
                else if (newType == "Многоугольник")
                {
                    int sideLength = int.Parse(NewSideLength.Text);
                    int sidesCount = int.Parse((NewSidesCount.SelectedItem as ComboBoxItem)?.Content.ToString());

                    if (sidesCount < 3)
                    {
                        MessageBox.Show("Количество сторон должно быть не менее 3", "Ошибка",
                                      MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    int[] sides = new int[sidesCount];
                    for (int i = 0; i < sidesCount; i++)
                    {
                        sides[i] = sideLength;
                    }

                    GetNewFigure = new RegularPolygons("многоугольник", newColor, sides);
                }

                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка создания новой фигуры: {ex.Message}", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void ClearFigureInfo()
        {
            CurrentTypeText.Text = "";
            CurrentColorText.Text = "";
            CurrentAreaText.Text = "";
        }

        private void SetComboBoxColor(ComboBox comboBox, string color)
        {
            foreach (ComboBoxItem item in comboBox.Items)
            {
                if (item.Content.ToString().Equals(color, StringComparison.OrdinalIgnoreCase))
                {
                    comboBox.SelectedItem = item;
                    return;
                }
            }
            comboBox.SelectedIndex = 0;
        }

        private void SetComboBoxValue(ComboBox comboBox, string value)
        {
            foreach (ComboBoxItem item in comboBox.Items)
            {
                if (item.Content.ToString() == value)
                {
                    comboBox.SelectedItem = item;
                    return;
                }
            }
            comboBox.SelectedIndex = 0;
        }
    }
}