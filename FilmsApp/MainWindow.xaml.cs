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
        private NpgsqlDataAdapter adapter = new NpgsqlDataAdapter();
        public DataSet dataset = new DataSet();
        public MainWindow()
        {
            InitializeComponent();
            var filmsTable = CreateDataTable();
            FillTable(filmsTable);
        }
        private DataTable CreateDataTable()
        {
            DataTable table = new DataTable("Films");
            table.Columns.Add("Id", typeof(int));
            table.Columns.Add("name", typeof(string));
            table.Columns.Add("rate", typeof(double));
            table.Columns.Add("fdate", typeof(DateTime));
            return table;
        }
        private void FillTable(DataTable table)
        {
            using (NpgsqlConnection con = new NpgsqlConnection(strCon))
            {
                con.Open();
                var cmd = new NpgsqlCommand("select * from film", con);
                table.Load(cmd.ExecuteReader());
                //adapter = new NpgsqlDataAdapter(cmd);
                //adapter.Fill(dataset);
                dataset.Tables.Add(table);
                FilmsGrid.ItemsSource = dataset?.Tables["Films"].DefaultView;
            }
            
        }
    }
}
