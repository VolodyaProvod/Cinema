using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace WpfApp4
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class RegistrationPage : Window
    {
        DataBase db = new DataBase();

        public RegistrationPage()
        {
            InitializeComponent();
        }

        private void Регистрация(object sender, RoutedEventArgs e)
        {
            if (Фамилия.Text == "Фамилия")
            {
                MessageBox.Show("Введите Фамилию");
                Фамилия.Focus();
            }
            else if (!Regex.IsMatch(Фамилия.Text, @"^[А-Я][а-я]+(-[А-Я][а-я]+)?$"))
            {
                MessageBox.Show("Введите корректную фамилию");
                Фамилия.Select(0, Фамилия.Text.Length);
                Фамилия.Focus();
            }
            if (Имя.Text == "Имя")
            {
                MessageBox.Show("Введите Имя");
                Имя.Focus();
            }
            else if (!Regex.IsMatch(Имя.Text, @"^[А-Я][а-я]+(-[А-Я][а-я]+)?$"))
            {
                MessageBox.Show("Введите корректное имя");
                Имя.Select(0, Имя.Text.Length);
                Имя.Focus();
            }
            if (Улица.Text == "Улица")
            {
                MessageBox.Show("Введите Улицу");
                Улица.Focus();
            }
            else if (!Regex.IsMatch(Улица.Text, @"^[А-Я][а-я]+(-[А-Я][а-я]+)?$"))
            {
                MessageBox.Show("Введите корректную улицу");
                Улица.Select(0, Улица.Text.Length);
                Улица.Focus();
            }
            if (Логин.Text == "Электронный адрес")
            {
                MessageBox.Show("Введите электронный адрес");
                Логин.Focus();
            }
            else if (!Regex.IsMatch(Логин.Text, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
            {
                MessageBox.Show("Введите корректный электронный адрес");
                Логин.Select(0, Логин.Text.Length);
                Логин.Focus();
            }
            else
            {
                if (Пароль.Password.Length == 0)
                {
                    MessageBox.Show("Введите пароль");
                    Пароль.Focus();
                }
                else if (Пароль_Copy.Password.Length == 0)
                {
                    MessageBox.Show("Повторите пароль");
                    Пароль_Copy.Focus();
                }
                else if (Пароль.Password != Пароль_Copy.Password)
                {
                    MessageBox.Show("Пароли не совпадают");
                    Пароль_Copy.Focus();
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("select count(Логин) from Пользователь where Логин = '" + Логин.Text + "'", db.getConnection());
                    db.openConnection();
                    int count = (int)cmd.ExecuteScalar();

                    if (count == 0)
                    {
                        cmd = new SqlCommand($"INSERT INTO Пользователь (Имя,Фамилия,Логин,Пароль,Улица) VALUES (@1,@2,@3,@4,@5)", db.getConnection());
                        cmd.Parameters.AddWithValue("@1", Имя.Text);
                        cmd.Parameters.AddWithValue("@2", Фамилия.Text);
                        cmd.Parameters.AddWithValue("@3", Логин.Text);
                        cmd.Parameters.AddWithValue("@4", Пароль.Password);
                        cmd.Parameters.AddWithValue("@5", Улица.Text);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Вы успешно зарегистрировались");
                        this.Hide();
                        MainPage Window = new MainPage(Логин.Text);
                        Window.Show();
                        db.closeConnection();
                    }
                    else
                    {
                        MessageBox.Show("Пользователь с таким электронным адресом уже существует");
                        db.closeConnection();
                    }
                    
                }
            }


        }

        private void Имя_MouseDown(object sender, RoutedEventArgs e)
        {
            if (Имя.Text == "Имя")
                Имя.Text = "";
        }

        private void Фамилия_MouseDown(object sender, RoutedEventArgs e)
        {
            if (Фамилия.Text == "Фамилия")
                Фамилия.Text = "";
        }

        private void Улица_MouseDown(object sender, RoutedEventArgs e)
        {
            if (Улица.Text == "Улица")
                Улица.Text = "";
        }

        private void Логин_MouseDown(object sender, RoutedEventArgs e)
        {
            if (Логин.Text == "Электронный адрес")
                Логин.Text = "";
        }
        

        private void Фамилия_MouseLeave(object sender, MouseEventArgs e)
        {
            if (Фамилия.Text == "")
                Фамилия.Text = "Фамилия";
        }

        private void Имя_MouseLeave(object sender, MouseEventArgs e)
        {
            if (Имя.Text == "")
                Имя.Text = "Имя";
        }

        private void Улица_MouseLeave(object sender, MouseEventArgs e)
        {
            if (Улица.Text == "")
                Улица.Text = "Улица";
        }

        private void Логин_MouseLeave(object sender, MouseEventArgs e)
        {
            if (Логин.Text == "")
                Логин.Text = "Электронный адрес";
        }

        private void Войти(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Authorization Window = new Authorization();
            Window.Show();
        }
    }
}
