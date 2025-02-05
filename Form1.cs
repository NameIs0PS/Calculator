using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Calculator
{
    public partial class Form1 : Form
    {
        private double currentValue = 0;
        private double previousValue = 0;
        private string operation = "";
        public Form1()
        {
            InitializeComponent();
        }

        private void btnNumber_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            txtDisplay.Text += button.Text; // Добавляем нажатую цифру в дисплей
        }

        private void btnAdd_Click(object sender, EventArgs e) => SetOperation("+");
        private void btnSubtract_Click(object sender, EventArgs e) => SetOperation("-");
        private void btnMultiply_Click(object sender, EventArgs e) => SetOperation("*");
        private void btnDivide_Click(object sender, EventArgs e) => SetOperation("/");

        private void SetOperation(string op)
        {
            // Сохраняем текущее значение из текстового поля
            if (!string.IsNullOrEmpty(txtDisplay.Text))
            {
                previousValue = double.Parse(txtDisplay.Text);
            }

            operation = op; // Устанавливаем текущую операцию
            txtDisplay.Clear(); // Очищаем дисплей для ввода следующего числа
        }

        private void btnEquals_Click(object sender, EventArgs e)
        {
            switch (operation)
            {
                case "+":
                    currentValue = previousValue + double.Parse(txtDisplay.Text);
                    break;
                case "-":
                    currentValue = previousValue - double.Parse(txtDisplay.Text);
                    break;
                case "*":
                    currentValue = previousValue * double.Parse(txtDisplay.Text);
                    break;
                case "/":
                    if (double.Parse(txtDisplay.Text) == 0)
                    {
                        MessageBox.Show("Ошибка: Деление на ноль!");
                        return;
                    }
                    currentValue = previousValue / double.Parse(txtDisplay.Text);
                    break;
            }
            txtDisplay.Text = currentValue.ToString();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtDisplay.Clear();
            currentValue = 0;
            previousValue = 0;
            operation = "";
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            File.AppendAllText("results.txt", $"{DateTime.Now}: {currentValue}\n");
            MessageBox.Show("Результат сохранен!");
        }
        private void chkDarkTheme_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDarkTheme.Checked) // Если флажок установлен
            {
                this.BackColor = System.Drawing.Color.Black; // Устанавливаем темный фон формы
                txtDisplay.BackColor = System.Drawing.Color.Gray; // Устанавливаем темный фон для TextBox
                txtDisplay.ForeColor = System.Drawing.Color.White; // Устанавливаем белый текст для TextBox
                chkDarkTheme.ForeColor = System.Drawing.Color.White; 
                // Устанавливаем цвет для всех кнопок
                foreach (Control control in this.Controls)
                {
                    if (control is Button button)
                    {
                        button.BackColor = System.Drawing.Color.Maroon; // Темный фон для кнопок
                        button.ForeColor = System.Drawing.Color.White; // Белый текст для кнопок
                    }
                }
            }
            else // Если флажок снят
            {
                this.BackColor = System.Drawing.Color.White; 
                txtDisplay.BackColor = System.Drawing.Color.LightGray; 
                txtDisplay.ForeColor = System.Drawing.Color.Black; 
                chkDarkTheme.ForeColor = System.Drawing.Color.Black;
                // Устанавливаем цвет для всех кнопок
                foreach (Control control in this.Controls)
                {
                    if (control is Button button)
                    {
                        button.BackColor = System.Drawing.Color.LightGray; 
                        button.ForeColor = System.Drawing.Color.Black; 
                    }
                }
            }
        }
    }
}
