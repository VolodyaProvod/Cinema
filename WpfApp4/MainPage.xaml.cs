using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Data.Sql;

namespace WpfApp4
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Window
    {
        DataBase db = new DataBase();
        string Username;
        string[] mainfilms = new string[5];

        void AdminButton()
        {
            SqlCommand cmd = new SqlCommand("select isAdmin from Пользователь where Логин = '" + Username + "'", db.getConnection());
            db.openConnection();
            bool isAdmin = (bool)cmd.ExecuteScalar();
            if (isAdmin)
            {
                admin.Visibility = Visibility.Visible;
            }
            db.closeConnection();
        }

        void LoadFilms()
        {
            DataTable films = new DataTable();
            SqlDataAdapter adapterP = new SqlDataAdapter();
            SqlCommand cmdP = new SqlCommand("select Название from Фильм", db.getConnection());
            adapterP.SelectCommand = cmdP;
            adapterP.Fill(films);
            int count = films.Rows.Count - 1;
            for (int i = 0; i < 5; i++, count--)
            {
                mainfilms[i] = films.Rows[count][0].ToString();
            }
            FirstF.Background = InputImage("img/Фильмы/" + mainfilms[0] + ".jpg");
            SecF.Background = InputImage("img/Фильмы/" + mainfilms[1] + ".jpg");
            ThirdF.Background = InputImage("img/Фильмы/" + mainfilms[2] + ".jpg");
            FourthF.Background = InputImage("img/Фильмы/" + mainfilms[3] + ".jpg");
            FifthF.Background = InputImage("img/Фильмы/" + mainfilms[4] + ".jpg");
            //Премьера1.Text = premier.Rows[3][0].ToString();
        }

        void LoadCinema()
        {
            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommand command = new SqlCommand("SELECT * FROM Кинотеатр", db.getConnection());
            db.openConnection();
            adapter.SelectCommand = command;
            adapter.Fill(table);
            if (table.Rows.Count > 0)
            {
                int count = 0;
                for (int i = 0; i < table.Rows.Count; i++)
                {
                   // mycanvas.Children.Add(new Label
                   // {
                   //     FontFamily = new FontFamily("PF Centro Sans Pro Thin"),
                   //     FontSize = 20,
                   //     Content = table.Rows[i][1].ToString(),
                   //     Foreground = System.Windows.Media.Brushes.White,
                   //     Margin = new Thickness (10,count,0,0),
                   //     Cursor = Cursors.Hand

                   // });
                   //count += 25;
                    //labels[i].Click += new System.EventHandler(labelsCinema_Click);
                    //labels[i].MouseEnter += new System.EventHandler(labelsCinema_MouseEnter);
                    //labels[i].MouseLeave += new System.EventHandler(labelsCinema_MouseLeave);
                }
            }
        }

        public ImageBrush InputImage (string way)
        {
            var image = new ImageBrush();
            image.ImageSource = new BitmapImage(new Uri(way, UriKind.Relative));
            return image;
        }
        
        public MainPage(string login)
        {
            InitializeComponent();
            Username = login;
            LoadFilms();
            admin.Visibility = Visibility.Hidden;
            AdminButton();
            UserL.Content = Username;
            ObservableCollection<string> list = new ObservableCollection<string>();
            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommand cmd = new SqlCommand("select Название from Кинотеатр", db.getConnection());
            adapter.SelectCommand = cmd;
            adapter.Fill(table);
            for (int i = 0; i<table.Rows.Count; ++i)
            {
                list.Add(table.Rows[i][0].ToString());
            }
            Поиск.ItemsSource = list;
        }



        private void Поиск_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void Поиск_KeyDown(object sender, KeyEventArgs e)
        {
            Поиск.IsDropDownOpen = true;
        }

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    if (Поиск.SelectedItem.ToString() == "Беларусь")
        //    {
        //        this.Hide();
        //        RegistrationPage Window = new RegistrationPage();
        //        Window.Show();
        //    }🔎
        //}

        private void UserL_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 1) //Note that this is a lie, this does not check for a "real" click
            {
                Label label = new Label();
                label.FontFamily = new FontFamily("PF Centro Sans Pro Thin");
                label.FontSize = 20;
                label.Content = "Избранное";
                label.Foreground = System.Windows.Media.Brushes.White;
                label.Cursor = Cursors.Hand;
                label.MouseEnter += MouseIn;
                label.MouseLeave += MouseOut;
                Top.Children.Add(label);
            }
        }

        private void MouseIn(object sender, MouseEventArgs e)
        {
            Label lable = new Label();
            lable = (Label)sender;
            lable.Foreground = Brushes.Aqua;
        }

        private void MouseOut(object sender, MouseEventArgs e)
        {
            Label lable = new Label();
            lable = (Label)sender;
            lable.Foreground = Brushes.White;
        }

        private void FirstF_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Фильм Window = new Фильм(mainfilms[0]);
            Window.Show();
        }

        private void SecF_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Фильм Window = new Фильм(mainfilms[1]);
            Window.Show();
        }

        private void FifthF_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Фильм Window = new Фильм(mainfilms[4]);
            Window.Show();
        }

        private void FourthF_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Фильм Window = new Фильм(mainfilms[3]);
            Window.Show();
        }

        private void ThirdF_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Фильм Window = new Фильм(mainfilms[2]);
            Window.Show();
        }
    }
}
