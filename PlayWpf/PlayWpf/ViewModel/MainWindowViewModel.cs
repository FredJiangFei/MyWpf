using PlayWpf.Core;
using Prism.Commands;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO.Ports;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace PlayWpf.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private string connectStatusDescription = "Disconnected";
        private string selectPort = "USB";
        private string selectedBaudRate = "9600";
        private static readonly object lockObj = new object();
        private readonly SerialPort sp = new SerialPort();
        private bool connectStatus;
        private readonly DispatcherTimer timer = new DispatcherTimer();

        public MainWindowViewModel()
        {
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Tick += Timer_Tick;
            var currentMachiePorts = SerialPort.GetPortNames();
            foreach (var item in currentMachiePorts)
            {
                this.PortList.Add(item);
            }
        }
        
        public ObservableCollection<string> PortList { get; set; } = new ObservableCollection<string> { "USB" };

        public ObservableCollection<string> BaudRateList { get; set; } = new ObservableCollection<string> { "1200", "2400", "4800", "9600", "19200", "38400", "57600", "115200" };

        public string SelectedPort
        {
            get => selectPort;
            set
            {
                selectPort = value;
                NotifyPropertyChanged();
            }
        }

        public string SelectedBaudRate
        {
            get => selectedBaudRate;
            set
            {
                selectedBaudRate = value;
                NotifyPropertyChanged();
            }
        }

        private void ConnectSerialPort()
        {
            try
            {
                if (sp.IsOpen)
                {
                    return;
                }

                var portName = this.SelectedPort;
                if ("USB".Equals(this.SelectedPort))
                {
                    portName = Tools.FindPort();
                }

                this.sp.PortName = portName;
                this.sp.BaudRate = "USB".Equals(portName) ? 0 : Convert.ToInt32(this.SelectedBaudRate);
                this.sp.DataBits = 8;
                this.sp.StopBits = StopBits.One;
                this.sp.Parity = Parity.None;
                this.sp.ReadTimeout = 5000;
                this.sp.WriteTimeout = 5000;
                this.sp.NewLine = "\r";
                sp.Open();
                var portDescription = "USB".Equals(this.SelectedPort) ? $"USB ({portName})" : this.SelectedPort;
                this.ConnectStatusDescription = sp.IsOpen ? $"Connected to {portDescription} @ {this.SelectedBaudRate} Baud" : "Disconnected";
                this.ConnectStatus = sp.IsOpen;
            }
            catch
            {
                this.ConnectStatusDescription = "Connected to USB…";
                this.ConnectStatus = false;
            }
        }


        public ICommand ConnectClick
        {
            get
            {
                return new DelegateCommand<Button>(btn =>
                {
                    lock (lockObj)
                    {
                        try
                        {
                            if (sp.IsOpen)
                            {
                                this.ConnectStatusDescription = "Disconnected";
                                this.ConnectStatus = false;
                                sp.Close();
                                timer.Stop();
                                return;
                            }

                            ConnectSerialPort();
                            this.timer.Start();
                        }
                        catch (Exception ex)
                        {
                            this.ConnectStatus = false;
                        }
                    }
                });
            }
        }


        private void Timer_Tick(object sender, EventArgs e)
        {
            if (!sp.IsOpen)
            {
                this.ConnectStatusDescription = "Disconnected";
                this.ConnectStatus = sp.IsOpen;
                timer.Stop();
            }
        }

        public bool ConnectStatus
        {
            get => connectStatus;
            set
            {
                connectStatus = value;
                NotifyPropertyChanged();
            }
        }

        public string ConnectStatusDescription
        {
            get => connectStatusDescription;
            set
            {
                connectStatusDescription = value;
                NotifyPropertyChanged();
            }
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
