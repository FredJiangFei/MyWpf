using PlayWpf.Models;
using PlayWpf.ViewModel;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
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
            //stu2 = new Student();
            //Binding binding = new Binding("Text") { Source = t1 };
            //t2.SetBinding(TextBox.TextProperty, binding);
        }

        private void OnOKClick(object sender, RoutedEventArgs e)
        {
            CheckName();
            CheckZip();
            CheckBirthday();
        }

        private void CheckName()
        {
            var name = NameControl.Tag?.ToString();
            if (name == null || name.Length <= 3)
                ShowAsError(NameControl);
            else
                ShowAsSuccess(NameControl);
        }

        private void CheckZip()
        {
            var zip = ZipControl.Tag?.ToString();
            if (zip == null || zip.Length != 5)
            {
                ShowAsError(ZipControl);
                return;
            }
            int n;
            bool number = int.TryParse(zip, out n);
            if (number)
                ShowAsSuccess(ZipControl);
            else
                ShowAsError(ZipControl);
        }

        private void CheckBirthday()
        {
            var birthday = BirthdayControl.Tag?.ToString();
            DateTime dt;
            bool isDate = DateTime.TryParse(birthday, out dt);
            if (isDate)
                ShowAsSuccess(BirthdayControl);
            else
                ShowAsError(BirthdayControl);
        }

        private void ShowAsError(Control tb)
        {
            tb.Background = Brushes.IndianRed;
        }

        private void ShowAsSuccess(Control tb)
        {
            tb.Background = Brushes.LightGreen;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show(stu2.GetValue(Student.NameProperty).ToString());
            var stu = new Student();
            stu.Name = this.t1.Text;
            t2.Text = stu.Name;
        }
    }
}
