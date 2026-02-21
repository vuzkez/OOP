using System;
using System.Windows.Forms;
using MatrixType;
using MatrixExceptionsLibrary;

namespace MainWindow
{
    public partial class Form1 : Form
    {
        private Matrix matrixA, matrixB, resultMatrix;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBoxMatrixSelect.Items.Add("Матрица A");
            comboBoxMatrixSelect.Items.Add("Матрица B");
            comboBoxMatrixSelect.SelectedIndex = 0;
        }

        // Создать матрицу A
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int r = (int)numericUpDown1.Value;
                int c = (int)numericUpDown2.Value;
                matrixA = new Matrix(new double[r, c]);
                textBox1.Text = $"A: {r}x{c} создана";
            }
            catch (MatrixSizeException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }

        // Создать матрицу B
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                int r = (int)numericUpDown3.Value;
                int c = (int)numericUpDown4.Value;
                matrixB = new Matrix(new double[r, c]);
                textBox1.Text = $"B: {r}x{c} создана";
            }
            catch (MatrixSizeException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }

        // Умножить A*B
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (matrixA == null || matrixB == null)
                {
                    MessageBox.Show("Сначала создайте матрицы!");
                    return;
                }

                resultMatrix = matrixA * matrixB;
                textBox1.Text = $"Результат: {resultMatrix.CountRows}x{resultMatrix.CountCols}";
                ShowMatrix(resultMatrix, textBox2);
            }
            catch (MatrixSizeException ex)
            {
                MessageBox.Show($"Несовместимые размеры!\n{ex.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }

        // Умножить матрицу на число (A или B)
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                Matrix selectedMatrix = null;
                string matrixName = "";

                if (comboBoxMatrixSelect.SelectedIndex == 0) // Матрица A
                {
                    selectedMatrix = matrixA;
                    matrixName = "A";
                }
                else if (comboBoxMatrixSelect.SelectedIndex == 1) // Матрица B
                {
                    selectedMatrix = matrixB;
                    matrixName = "B";
                }

                if (selectedMatrix == null)
                {
                    MessageBox.Show($"Сначала создайте матрицу {matrixName}!");
                    return;
                }

                if (!double.TryParse(textBox3.Text, out double num))
                {
                    MessageBox.Show("Введите число!");
                    return;
                }

                resultMatrix = selectedMatrix * num;
                textBox1.Text = $"{matrixName} × {num}";
                ShowMatrix(resultMatrix, textBox2);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }

        // Показать матрицу в TextBox
        private void ShowMatrix(Matrix m, TextBox tb)
        {
            tb.Clear();
            for (int i = 0; i < m.CountRows; i++)
            {
                for (int j = 0; j < m.CountCols; j++)
                {
                    tb.Text += $"{m[i, j]:F2}\t";
                }
                tb.Text += Environment.NewLine;
            }
        }

        // Заполнить A случайными
        private void button5_Click(object sender, EventArgs e)
        {
            if (matrixA == null)
            {
                MessageBox.Show("Сначала создайте матрицу A!");
                return;
            }

            Random rnd = new Random();
            for (int i = 0; i < matrixA.CountRows; i++)
                for (int j = 0; j < matrixA.CountCols; j++)
                    matrixA[i, j] = rnd.Next(1, 10);

            textBox1.Text = "Матрица A заполнена";
            ShowMatrix(matrixA, textBox4);
        }

        // Заполнить B случайными
        private void button6_Click(object sender, EventArgs e)
        {
            if (matrixB == null)
            {
                MessageBox.Show("Сначала создайте матрицу B!");
                return;
            }

            Random rnd = new Random();
            for (int i = 0; i < matrixB.CountRows; i++)
                for (int j = 0; j < matrixB.CountCols; j++)
                    matrixB[i, j] = rnd.Next(1, 10);

            textBox1.Text = "Матрица B заполнена";
            ShowMatrix(matrixB, textBox5);
        }

        // Очистить всё
        private void button7_Click(object sender, EventArgs e)
        {
            matrixA = matrixB = resultMatrix = null;
            textBox1.Text = "Очищено";
            textBox2.Clear();
            textBox4.Clear();
            textBox5.Clear();
        }
    }
}