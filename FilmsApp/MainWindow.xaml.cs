using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections;

namespace FilmsApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string strCon = @"Server=localhost;Port=5432;Username=postgres;Password=admin;Database=films";
        public DataSet dataset = new DataSet();
        public MainWindow()
        {
            InitializeComponent();
            var filmsTable = CreateDataTable();
            FillTable(filmsTable);
        }
        private DataTable CreateDataTable()
        {
            DataTable films = new DataTable("Films");
            films.Columns.Add("Id", typeof(int));
            films.Columns.Add("name", typeof(string));
            films.Columns.Add("rate", typeof(double));
            films.Columns.Add("fdate", typeof(DateTime));
            return films;
        }
        private void FillTable(DataTable table)
        {
            using (NpgsqlConnection con = new NpgsqlConnection(strCon))
            {
                con.Open();
                var cmd = new NpgsqlCommand("select * from film", con);
                table.Load(cmd.ExecuteReader());
                dataset.Tables.Add(table);
                FilmsGrid.ItemsSource = dataset?.Tables["Films"].DefaultView;
            }     
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
