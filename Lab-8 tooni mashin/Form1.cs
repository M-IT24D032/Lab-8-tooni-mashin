using System;
using System.Globalization;
using System.Windows.Forms;

namespace Lab_8_tooni_mashin
{
    public partial class Form1 : Form
    {
        TextBox display;      // тоо харагдах талбар
        Label memLabel;       // санах ойн "M" тэмдэг (label1)

        double firstNumber = 0;   // эхний тоо
        string operation = "";    // сонгосон үйлдэл (+, -, *, /)
        bool startNew = true;     // true бол дараагийн тоо шинээр эхэлнэ

        public Form1()
        {
            InitializeComponent();

            // Форм дээрх control бүрийг шалгаад, товч бүрийг нэг method-той холбоно
            foreach (Control c in Controls)
            {
                if (c is Button)
                {
                    c.Click += Button_Click;
                }
                if (c is TextBox)
                {
                    display = (TextBox)c;
                }
                if (c is Label)
                {
                    memLabel = (Label)c;
                }
            }

            display.ReadOnly = true;
            display.TextAlign = HorizontalAlignment.Right;
            display.Text = "0";
            memLabel.Text = "";
        }

        // Аль ч товч дарахад энэ method ажиллана
        private void Button_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            string text = b.Text;

            if ("0123456789".Contains(text))
            {
                AddDigit(text);
            }
            else if (text == ".")
            {
                AddDot();
            }
            else if (text == "+" || text == "-" || text == "*" || text == "/")
            {
                PressOperator(text);
            }
            else if (text == "=")
            {
                PressEquals();
            }
            else if (text == "Backspace")
            {
                Backspace();
            }
            else if (text == "CE")
            {
                display.Text = "0";
                startNew = true;
            }
            else if (text == "C")
            {
                ClearAll();
            }
            else if (text == "+/-")
            {
                ChangeSign();
            }
        }

        // ---------- Тоо оруулах ----------
        private void AddDigit(string digit)
        {
            if (startNew == true)
            {
                display.Text = "0";
                startNew = false;
            }

            if (display.Text == "0")
            {
                display.Text = digit;
            }
            else
            {
                display.Text = display.Text + digit;
            }
        }

        private void AddDot()
        {
            if (startNew == true)
            {
                display.Text = "0";
                startNew = false;
            }

            // цэг аль хэдийн байвал дахин нэмэхгүй
            if (display.Text.Contains(".") == false)
            {
                display.Text = display.Text + ".";
            }
        }

        // ---------- Тоог унших, харуулах ----------
        // Дэлгэц дээрх бичвэрийг тоо болгоно
        // (InvariantCulture нь цэгийг таслал гэж андуурахгүйн тулд)
        private double GetNumber()
        {
            return double.Parse(display.Text, CultureInfo.InvariantCulture);
        }

        // Тоог дэлгэц дээр харуулна
        private void ShowNumber(double number)
        {
            display.Text = number.ToString(CultureInfo.InvariantCulture);
        }

        // ---------- Үйлдлүүд ----------
        private void PressOperator(string op)
        {
            // өмнө нь үйлдэл сонгосон бол эхлээд тооцоолно (жишээ нь 2 + 3 +)
            if (operation != "" && startNew == false)
            {
                Calculate();
            }

            firstNumber = GetNumber();
            operation = op;
            startNew = true;
        }

        private void PressEquals()
        {
            if (operation != "")
            {
                Calculate();
                operation = "";
            }
            startNew = true;
        }

        // firstNumber ба дэлгэц дээрх тоон дээр үйлдлийг хийнэ
        private void Calculate()
        {
            double secondNumber = GetNumber();
            double result = 0;

            if (operation == "+")
            {
                result = firstNumber + secondNumber;
            }
            else if (operation == "-")
            {
                result = firstNumber - secondNumber;
            }
            else if (operation == "*")
            {
                result = firstNumber * secondNumber;
            }
            else if (operation == "/")
            {
                if (secondNumber == 0)
                {
                    MessageBox.Show("Тэгд хувааж болохгүй!");
                    ClearAll();
                    return;
                }
                result = firstNumber / secondNumber;
            }

            ShowNumber(result);
        }

        private void ClearAll()
        {
            firstNumber = 0;
            operation = "";
            startNew = true;
            display.Text = "0";
        }

        // ---------- Цэвэрлэх, тэмдэг солих ----------
        private void Backspace()
        {
            if (startNew == true)
            {
                return;
            }

            if (display.Text.Length > 1)
            {
                // сүүлийн тэмдэгтийг хасна
                display.Text = display.Text.Substring(0, display.Text.Length - 1);
            }
            else
            {
                display.Text = "0";
            }

            if (display.Text == "-")
            {
                display.Text = "0";
            }
        }

        private void ChangeSign()
        {
            double number = GetNumber();
            if (number != 0)
            {
                ShowNumber(-number);
            }
        }
    }
}