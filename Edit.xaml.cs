using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WPF___OOP
{
    /// <summary>
    /// Interaction logic for Edit.xaml
    /// </summary>
    public partial class Edit : Window
    {
        public Edit()
        {
            InitializeComponent();

            /*I grab the selected movies information using a query*/
            SqlManagement sqlManagement = new SqlManagement();

            string[] results =  sqlManagement.RunQuery($"SELECT * FROM movieList WHERE movieTitle = '{((MainWindow)Application.Current.MainWindow).ItemList.SelectedItem}'").Split(',');
            TitleTextBox.Text = results[0];
            InstructorTextBox.Text = results[1];
            YearTextBox.Text = results[2];



        }
        
        private void TitleTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            /*Once focused on the text disappears if the text is "Title... */
            if (TitleTextBox.Text == "Title...")
            {
                TitleTextBox.Text = string.Empty;
            }
            TitleTextBox.Foreground = Brushes.Black;
        }

        private void TitleTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            /*If I leave the TextBox empty "Title..." gets written in it*/
            if (TitleTextBox.Text == string.Empty)
            {
                TitleTextBox.Foreground = Brushes.Gray;
                TitleTextBox.Text = "Title...";
            }

        }

        private void InstructorTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            /*Once focused on the text disappears if the text is "Instructor... */
            if (InstructorTextBox.Text == "Instructor...")
            {
                InstructorTextBox.Text = string.Empty;
            }
            InstructorTextBox.Foreground = Brushes.Black;
        }

        private void InstructorTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            /*If I leave the TextBox empty "Instructor..." gets written in it*/
            if (InstructorTextBox.Text == string.Empty)
            {
                InstructorTextBox.Foreground = Brushes.Gray;
                InstructorTextBox.Text = "Instructor...";
            }
        }

        private void YearTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            /*Once focused on the text disappears if the text is "Year... */
            if (YearTextBox.Text == "Year...")
            {
                YearTextBox.Text = string.Empty;
            }
            YearTextBox.Foreground = Brushes.Black;
        }

        private void YearTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            /*If I leave the TextBox empty "Year..." gets written in it or if it has contents they're checked to see if valid, if not they get replaced*/
            int result = 0;
            if ((YearTextBox.Text == string.Empty) || (int.TryParse(YearTextBox.Text, out result) != true))
            {

                YearTextBox.Foreground = Brushes.Gray;
                YearTextBox.Text = "Year...";


            }
            else if ((YearTextBox.Text != string.Empty) && (int.TryParse(YearTextBox.Text, out result) == true))
            {
                if (result < 1850 || result > DateTime.Now.Year)
                {
                    YearTextBox.Foreground = Brushes.Gray;
                    YearTextBox.Text = "Year...";
                }
            }


        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            /*I check if all TextBoxes have valid values in them, if not they won't be saved*/
            if (TitleTextBox.Text == "Title..." || InstructorTextBox.Text == "Instructor..." || YearTextBox.Text == "Year...")
            {
                MessageBox.Show("All values must be given!");
            }
            else
            {
                /*If the values are valid you have to confirm that you want to save it*/
                MessageBoxResult warning;
                warning = MessageBox.Show("Are you sure you want to add this?", "Warning!", MessageBoxButton.YesNo);
                if (warning == MessageBoxResult.No)
                {
                    this.Close();
                }
                else if (warning == MessageBoxResult.Yes)
                {
                    /*Here I update the values that exist already since this */
                    SqlManagement sqlManagement = new SqlManagement();

                    sqlManagement.RunNonQuery($"UPDATE movieList SET movieTitle = '{TitleTextBox.Text}', " +
                        $"movieInstructor = '{InstructorTextBox.Text}', movieYear = {Convert.ToInt32(YearTextBox.Text)} WHERE " +
                        $"movieTitle = '{((MainWindow)Application.Current.MainWindow).ItemList.SelectedItem}'");

                    

                }
            }



        }
    }
}

