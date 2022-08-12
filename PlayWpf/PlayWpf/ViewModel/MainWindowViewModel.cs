using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO.Ports;

namespace PlayWpf.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public MainWindowViewModel()
        {
            var currentMachiePorts = SerialPort.GetPortNames();
            foreach (var item in currentMachiePorts)
            {
                this.PortList.Add(item);
            }
        }
        
        public ObservableCollection<string> PortList { get; set; } = new ObservableCollection<string> { "USB" };

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
