using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.Win32;
using FiguresLibrary;

namespace FiguresApp
{
    public partial class MainWindow : Window
    {
        private List<Figure> allFigures = new List<Figure>();
        private List<RegularPolygons> polygons = new List<RegularPolygons>();
        private List<Circle> circles = new List<Circle>();
        private string currentFilePath = "";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoadBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.Title = "Выберите файл с данными фигур";

                if (openFileDialog.ShowDialog() == true)
                {
                    currentFilePath = openFileDialog.FileName;
                    ManagerFigures manager = new ManagerFigures(currentFilePath);
                    allFigures = manager.figures;
                    polygons = allFigures.OfType<RegularPolygons>().ToList();
                    circles = allFigures.OfType<Circle>().ToList();

                    Status.Text = $"Загружено фигур: {allFigures.Count}";
                    ShowMessage($"Фигуры загружены из файла: {System.IO.Path.GetFileName(currentFilePath)}! Теперь нажмите другие кнопки.", "Black");
                }
                else
                {
                    ShowMessage("Файл не выбран", "Red");
                }
            }
            catch (Exception ex)
            {
                ShowMessage($"Ошибка: {ex.Message}", "Red");
            }
        }

        private void ShowAllBtn_Click(object sender, RoutedEventArgs e)
        {
            if (allFigures.Count == 0)
            {
                ShowMessage("Сначала загрузите фигуры (кнопка 1)", "Red");
                return;
            }

            OutputPanel.Children.Clear();
            AddTextBlock("ВСЕ ФИГУРЫ", "Black", true);

            foreach (var figure in allFigures)
            {
                AddTextBlock(figure.GetInfo(), figure.ColorFigure);
                AddSeparator();
            }

            Status.Text = "Показаны все фигуры";
        }

        private void SortBtn_Click(object sender, RoutedEventArgs e)
        {
            if (polygons.Count == 0)
            {
                ShowMessage("Нет многоугольников для сортировки", "Red");
                return;
            }

            polygons.Sort();

            OutputPanel.Children.Clear();
            AddTextBlock("ОТСОРТИРОВАННЫЕ МНОГОУГОЛЬНИКИ", "Black", true);

            foreach (var polygon in polygons)
            {
                AddTextBlock(polygon.GetInfo(), polygon.ColorFigure);
                AddSeparator();
            }

            Status.Text = "Многоугольники отсортированы по площади";
        }

        private void GreenBtn_Click(object sender, RoutedEventArgs e)
        {
            if (circles.Count == 0)
            {
                ShowMessage("Нет окружностей", "Red");
                return;
            }

            var greenCircles = circles.Where(c =>
                c.ColorFigure.ToLower() == "green" &&
                c.IsInFirstQuarter()).ToList();

            OutputPanel.Children.Clear();
            AddTextBlock("ЗЕЛЕНЫЕ ОКРУЖНОСТИ В I ЧЕТВЕРТИ", "Black", true);

            if (greenCircles.Count == 0)
            {
                AddTextBlock("Нет зеленых окружностей в I четверти", "Red");
                Status.Text = "Зеленые окружности не найдены";
                return;
            }

            foreach (var circle in greenCircles)
            {
                string info = $"Длина окружности: {circle.GetLength():F2}\n" +
                             $"Центр: ({circle.GetCenter().x}, {circle.GetCenter().y})\n" +
                             $"Радиус: {Math.Sqrt(Math.Pow(circle.GetEdgePoint().x - circle.GetCenter().x, 2) + Math.Pow(circle.GetEdgePoint().y - circle.GetCenter().y, 2)):F2}";

                AddTextBlock(info, "Green");
                AddSeparator();
            }

            Status.Text = $"Найдено {greenCircles.Count} зеленых окружностей";
        }

        private void AddFigureBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(currentFilePath))
            {
                ShowMessage("Сначала загрузите файл (кнопка 1)", "Red");
                return;
            }

            try
            {
                // Диалог для выбора типа фигуры
                var dialog = new AddFigureDialog();
                if (dialog.ShowDialog() == true)
                {
                    Figure newFigure = dialog.GetFigure();
                    allFigures.Add(newFigure);

                    // Обновляем специализированные списки
                    if (newFigure is RegularPolygons polygon)
                        polygons.Add(polygon);
                    else if (newFigure is Circle circle)
                        circles.Add(circle);

                    Status.Text = $"Фигура добавлена. Всего фигур: {allFigures.Count}";
                    ShowMessage("Фигура успешно добавлена!", "Green");
                }
            }
            catch (Exception ex)
            {
                ShowMessage($"Ошибка при добавлении фигуры: {ex.Message}", "Red");
            }
        }

        private void RemoveFigureBtn_Click(object sender, RoutedEventArgs e)
        {
            if (allFigures.Count == 0)
            {
                ShowMessage("Нет фигур для удаления", "Red");
                return;
            }

            try
            {
                // Диалог для выбора фигуры для удаления
                var dialog = new RemoveFigureDialog(allFigures);
                if (dialog.ShowDialog() == true)
                {
                    Figure figureToRemove = dialog.SelectedFigure;
                    allFigures.Remove(figureToRemove);

                    // Обновляем специализированные списки
                    if (figureToRemove is RegularPolygons polygon)
                        polygons.Remove(polygon);
                    else if (figureToRemove is Circle circle)
                        circles.Remove(circle);

                    Status.Text = $"Фигура удалена. Осталось фигур: {allFigures.Count}";
                    ShowMessage("Фигура успешно удалена!", "Green");
                }
            }
            catch (Exception ex)
            {
                ShowMessage($"Ошибка при удалении фигуры: {ex.Message}", "Red");
            }
        }

        private void AddTextBlock(string text, string color, bool isBold = false)
        {
            TextBlock textBlock = new TextBlock();
            textBlock.Text = text;
            textBlock.TextWrapping = TextWrapping.Wrap;
            textBlock.Margin = new Thickness(0, 5, 0, 5);
            textBlock.FontFamily = new FontFamily("Courier New");

            try
            {
                var brush = new SolidColorBrush((Color)ColorConverter.ConvertFromString(color));
                textBlock.Foreground = brush;
            }
            catch
            {
                textBlock.Foreground = Brushes.Black;
            }

            if (isBold)
            {
                textBlock.FontWeight = FontWeights.Bold;
                textBlock.FontSize = 14;
            }

            OutputPanel.Children.Add(textBlock);
        }

        private void AddSeparator()
        {
            Separator separator = new Separator();
            separator.Margin = new Thickness(0, 5, 0, 5);
            separator.Height = 1;
            separator.Background = Brushes.Gray;
            OutputPanel.Children.Add(separator);
        }

        private void ShowMessage(string message, string color)
        {
            OutputPanel.Children.Clear();
            AddTextBlock(message, color);
        }

        private void FindAndEditBtn_Click(object sender, RoutedEventArgs e)
        {
            if (allFigures.Count == 0)
            {
                ShowMessage("Нет фигур для редактирования", "Red");
                return;
            }

            try
            {
                var dialog = new FindAndEditDialog(allFigures);
                if (dialog.ShowDialog() == true)
                {
                    Figure originalFigure = dialog.GetOriginalFigure;
                    Figure newFigure = dialog.GetNewFigure;

                    if (originalFigure != null && newFigure != null)
                    {
                        // Находим и заменяем фигуру в списках
                        int index = allFigures.IndexOf(originalFigure);
                        if (index >= 0)
                        {
                            allFigures[index] = newFigure;

                            // Обновляем специализированные списки
                            if (originalFigure is RegularPolygons oldPolygon)
                            {
                                polygons.Remove(oldPolygon);
                            }
                            else if (originalFigure is Circle oldCircle)
                            {
                                circles.Remove(oldCircle);
                            }

                            if (newFigure is RegularPolygons newPolygon)
                            {
                                polygons.Add(newPolygon);
                            }
                            else if (newFigure is Circle newCircle)
                            {
                                circles.Add(newCircle);
                            }

                            Status.Text = "Фигура успешно заменена";

                            // Показываем изменения
                            OutputPanel.Children.Clear();
                            AddTextBlock("ФИГУРА ЗАМЕНЕНА", "Black", true);
                            AddSeparator();
                            AddTextBlock("БЫЛО:", "Red", true);
                            AddTextBlock(originalFigure.GetInfo(), "Red");
                            AddSeparator();
                            AddTextBlock("СТАЛО:", "Green", true);
                            AddTextBlock(newFigure.GetInfo(), "Green");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ShowMessage($"Ошибка редактирования: {ex.Message}", "Red");
            }
        }
    }
}