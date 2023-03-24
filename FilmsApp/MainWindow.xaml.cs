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
using System.Windows.Controls.Primitives;

namespace FilmsApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string strCon = @"Server=localhost;Port=5432;Username=postgres;Password=admin;Database=films";
        public DataSet dataset = new DataSet();
        private List<ComboBox> comboBoxes;

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
                List<ComboBox> t = new List<ComboBox>();
                FillComboBoxes(t);
            }     
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            //FilmsGrid.SelectedItems
        }
        private List<ComboBox> CreateListOfCB(List<ComboBox> comboBoxes)
        {
            return new List<ComboBox>()
            {
                NameBox,
                RateBox,
                DateBox
            };
        }
        /// <summary>
        ///  Заполняет каждый ComboBox соответсвующими данными таблицы.
        ///  Метод имеет два необязательных параметра:
        ///  <param name="t">Список combobox'оф, который будет использован для приязки данных.</param>
        ///  <param name="grid">При передачи grid'a в метод, метод понимает, что пользователь работает с grid'ом.
        ///  Пользоавтель выбирает строку и все элементы строки передаются в combobox'ы в виде текста.</param>
        /// </summary>
        private void FillComboBoxes(List<ComboBox> t = null!, DataGrid grid = null!)
        {
            comboBoxes = CreateListOfCB(t);
            if (grid == null)
            {            
                foreach (var box in comboBoxes)
                {
                    box.ItemsSource = dataset?.Tables["Films"].DefaultView;
                }
            }
            else
            {
                foreach (DataRowView rowView in FilmsGrid.SelectedItems)
                {
                    if (rowView != null)
                    {
                        DataRow row = rowView.Row;
                        var data = row.ItemArray.ToList();
                        Films film = new Films(data);
                        NameBox.Text = film.Fname;
                        RateBox.Text = film.Rate.ToString();
                        DateBox.Text = film.Date.ToString();
                    }
                }
            }
        }

        private void FilmsGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FillComboBoxes(grid: FilmsGrid);
        }
    }
}
