using PlayWpf.ViewModel;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace PlayWpf
{
    public partial class MainWindow : Window
    {
        MainWindowViewModel viewModel = new MainWindowViewModel();
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }

        private void ButtonAddName_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtName.Text) && !lstNames.Items.Contains(txtName.Text))
            {
                lstNames.Items.Add(txtName.Text);
                txtName.Clear();
            }
        }

        private void fileExitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        
        private void messageBox_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Oops!!! An unexpected error occurred, please contact administrator (-1).", "", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void OnOKClick(object sender, RoutedEventArgs e)
        {
            CheckName();
            CheckZip();
            CheckBirthday();
        }

        private void CheckName()
        {
            var name = NameTextBox.Text;
            if (name == null || name.Length <= 3)
                ShowAsError(NameTextBox);
            else
                ShowAsSuccess(NameTextBox);
        }

        private void CheckZip()
        {
            var zip = ZipTextBox.Text;
            if (zip == null || zip.Length != 5)
            {
                ShowAsError(ZipTextBox);
                return;
            }
            int n;
            bool number = int.TryParse(zip, out n);
            if (number)
                ShowAsSuccess(ZipTextBox);
            else
                ShowAsError(ZipTextBox);
        }

        private void CheckBirthday()
        {
            var birthday = BirthdayTextBox.Text;
            DateTime dt;
            bool isDate = DateTime.TryParse(birthday, out dt);
            if (isDate)
                ShowAsSuccess(BirthdayTextBox);
            else
                ShowAsError(BirthdayTextBox);
        }

        private void ShowAsError(TextBox tb)
        {
            tb.Background = Brushes.IndianRed;
        }

        private void ShowAsSuccess(TextBox tb)
        {
            tb.Background = Brushes.LightGreen;
        }
    }
}
