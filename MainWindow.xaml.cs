using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF___OOP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        { /*I open an instance of my SqlManagement and use a method that returns a list which I display*/
            SqlManagement sqlManagement = new SqlManagement();

            InitializeComponent();
            List<string> menuItems = sqlManagement.RunQueryList($"SELECT movieTitle FROM MovieList");
            foreach (var item in menuItems)
            {
                ItemList.Items.Add(item);
            }
            
        }

        private void AddNew_Click(object sender, RoutedEventArgs e)
        {
            /*Open a new window*/
            AddNew addNew = new AddNew();
            addNew.Show();
            
            

        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            /*Open a new window*/
            Edit edit = new Edit();
            edit.Show();
            
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        { /*I run a non query which deletes the selected movie*/
            SqlManagement sqlManagement = new SqlManagement();
            sqlManagement.RunNonQuery($"DELETE FROM movieList WHERE movieTitle = '{ItemList.SelectedItem.ToString()}'");
        }
    }
}
