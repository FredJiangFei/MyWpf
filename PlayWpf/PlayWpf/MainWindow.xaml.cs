using PlayWpf.ViewModel;
using System.Windows;

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
    }
}
