using PlayWpf.Models;
using PlayWpf.ViewModel;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace PlayWpf
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        MainWindowViewModel viewModel = new MainWindowViewModel();

        Student stu2;
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = viewModel;
            Text = "Initial Text";

            stu2 = new Student();
            Binding binding = new Binding("Text") { Source = t1 };

            stu2.SetBinding(Student.NameProperty, binding);
            t2.SetBinding(TextBox.TextProperty, new Binding("Name") { Source = stu2 });
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

        private string _Text;
        public string Text
        {
            get { return _Text; }
            set
            {
                if (value != _Text)
                {
                    _Text = value;
                    NotifyPropertyChanged("Text");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show(stu2.Name);
            Text = "clicked";

            var stu = new Student();
            School.SetGrade(stu, 6);
            int grade = School.GetGrade(stu);
            MessageBox.Show(grade.ToString());
        }
    }
}
