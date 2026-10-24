using System;
using System.Globalization;
using System.Windows.Forms;

namespace Lab_8_tooni_mashin
{
    public partial class Form1 : Form
    {
        TextBox display;      // тоо харагдах талбар
        Label memLabel;       // санах ойн "M" тэмдэг (label1)

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
    }
}