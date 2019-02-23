using System;
using System.Windows;

namespace WpfSeriaPort
{
    public partial class KantarSonucWindow : Window
    {
        KantarSerialPort kantar;
        public KantarSonucWindow(KantarSerialPort kantarPort)
        {
            InitializeComponent();
            kantar = kantarPort;
        }

        public KantarSerialPort KantarPort { get; set; }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            kantar.dataReceiveEvent += K_dataReceiveEvent;
        }

        private void K_dataReceiveEvent(int data)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                if (txtKantarSonuc.Text!= data.ToString())
                       txtKantarSonuc.Text = data.ToString();
                    
            }));

        }
       
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
