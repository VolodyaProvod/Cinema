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
    /// Логика взаимодействия для Фильм.xaml
    /// </summary>
    public partial class Фильм : Window
    {
        DataBase db = new DataBase();
        string CurrentFilm;

        public ImageBrush InputImage(string way)
        {
            var image = new ImageBrush();
            image.ImageSource = new BitmapImage(new Uri(way, UriKind.Relative));
            return image;
        }

        public void LoadFilm()
        {
            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommand command = new SqlCommand("SELECT * FROM Фильм where Название = '" + CurrentFilm + "'", db.getConnection());
            db.openConnection();
            adapter.SelectCommand = command;
            adapter.Fill(table);
            FilmName.Text = CurrentFilm;
            Description.Text = table.Rows[0][2].ToString();
        }

        public Фильм(string Film)
        {
            InitializeComponent();
            CurrentFilm = Film;
            LoadFilm();
            Back.Background = InputImage("img/Постеры/" + Film + ".jpg");
            Poster.Background = InputImage("img/Фильмы/" + Film + ".jpg");
        }
    }
}
