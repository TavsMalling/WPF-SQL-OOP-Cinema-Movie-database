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
    /// Interaction logic for AddNew.xaml
    /// </summary>
    public partial class AddNew : Window
    {
        public AddNew()
        {
            InitializeComponent();
        }

        private void TitleTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (TitleTextBox.Text == "Title...")
            {
                TitleTextBox.Text = string.Empty;
            }
            TitleTextBox.Foreground = Brushes.Black;
        }

        private void TitleTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (TitleTextBox.Text == string.Empty)
            {
                TitleTextBox.Foreground = Brushes.Gray;
                TitleTextBox.Text = "Title...";
            }

        }

        private void InstructorTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (InstructorTextBox.Text == "Instructor...")
            {
                InstructorTextBox.Text = string.Empty;
            }
            InstructorTextBox.Foreground = Brushes.Black;
        }

        private void InstructorTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (InstructorTextBox.Text == string.Empty)
            {
                InstructorTextBox.Foreground = Brushes.Gray;
                InstructorTextBox.Text = "Instructor...";
            }
        }

        private void YearTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (YearTextBox.Text == "Year...")
            {
                YearTextBox.Text = string.Empty;
            }
            YearTextBox.Foreground = Brushes.Black;
        }

        private void YearTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
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
            if (TitleTextBox.Text == "Title..." || InstructorTextBox.Text == "Instructor..." || YearTextBox.Text == "Year...")
            {
                MessageBox.Show("All values must be given!");
            }
            else
            {

                MessageBoxResult warning;
                warning = MessageBox.Show("Are you sure you want to add this?", "Warning!", MessageBoxButton.YesNo);
                if (warning == MessageBoxResult.No)
                {
                    this.Close();
                }
                else if (warning == MessageBoxResult.Yes)
                {
                    SqlManagement sqlManagement = new SqlManagement();

                    sqlManagement.RunNonQuery($"INSERT INTO movieList VALUES ('{TitleTextBox.Text}', '{InstructorTextBox.Text}', {Convert.ToInt32(YearTextBox.Text)})");

                }
            }



        }
    }
}

