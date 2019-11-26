using System.Linq;
using System.Windows;
using System.Data.SqlClient;

namespace WpfApp4
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Authorization : Window
    {
        DataBase db = new DataBase();

        public Authorization()
        {
            InitializeComponent();
        }

        private void Регистрация(object sender, RoutedEventArgs e)
        {
            this.Hide();
            RegistrationPage Window = new RegistrationPage();
            Window.Show();
        }

        private void Вход(object sender, RoutedEventArgs e)
        {
            SqlCommand cmd = new SqlCommand("select Пароль from Пользователь where Логин = '" + Логин.Text + "'", db.getConnection());
            db.openConnection();
            string password = (string)cmd.ExecuteScalar();
            if (password == Пароль.Password)
            {
                MessageBox.Show("Вход успешно выполнен");
                this.Hide();
                MainPage Window = new MainPage(Логин.Text);
                Window.Show();
                db.closeConnection();
            }
            else
            {
                MessageBox.Show("Неправильный логин, или пароль");
                db.closeConnection();
            }
        }
    }
}
