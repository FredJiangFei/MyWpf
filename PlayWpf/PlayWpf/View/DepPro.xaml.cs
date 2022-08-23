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

namespace PlayWpf.View
{
    /// <summary>
    /// Interaction logic for DepPro.xaml
    /// </summary>
    public partial class DepPro : UserControl
    {
        public DepPro()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty MyTextPropertyProperty =
            DependencyProperty.Register("MyTextProperty", typeof(string), typeof(DepPro), new UIPropertyMetadata(String.Empty));
        public string MyTextProperty
        {
            get { return (string)GetValue(MyTextPropertyProperty); }
            set { SetValue(MyTextPropertyProperty, value); }
        }
    }
}
