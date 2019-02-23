using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace WpfSeriaPort
{
    public class KantarSerialPort
    {
        public SerialPort s_port;

        public event Action<int> dataReceiveEvent;

        public KantarSerialPort(String port, string baudrate, string parity, string databits, string stopbits)
        {
            String _port = port;
            int _baudrate = Convert.ToInt32(baudrate);
            Parity _parity = (Parity)Enum.Parse(typeof(Parity), parity);
            int _databits = Convert.ToInt32(databits);
            StopBits _stopbits = (StopBits)Enum.Parse(typeof(StopBits), stopbits);

            serialport_connect(_port, _baudrate, _parity, _databits, _stopbits);
        }

        private void serialport_connect(String port, int baudrate, Parity parity, int databits, StopBits stopbits)
        {
            DateTime dt = DateTime.Now;
            String dtn = dt.ToShortTimeString();

            s_port = new SerialPort(port, baudrate, parity, databits, stopbits);
            try
            {
                s_port.Open();
                MessageBox.Show("Bağlandı");
                s_port.DataReceived += new SerialDataReceivedEventHandler(sport_DataReceived);
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString(), "Error"); }
        }

        private void sport_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            Thread.Sleep(100);

            var data = s_port.ReadExisting();

            data = data.PadLeft(7, '0');

            var str_son7 = data.Substring(data.Length - 7);
            var strRakamlar = SadeceRakamlariGetir(str_son7);
            var dtonaj = decimal.Parse(strRakamlar)/10; 

            int i_tonaj_sonuc =Convert.ToInt32(Math.Round(dtonaj, 0, MidpointRounding.AwayFromZero));

            if (dataReceiveEvent!=null)
                dataReceiveEvent(i_tonaj_sonuc);
        }

        public void MesajGonder(string data)
        {
            s_port.Write(data);
        }
    
        private void PortKapat()
        {
            if (s_port.IsOpen)
            {
                s_port.Close();
                MessageBox.Show("Port Kapandı");
            }
        }

        private string SadeceRakamlariGetir(string input)
        {
            string str = input;

            StringBuilder convert = new StringBuilder();

            string pattern = @"\d+";
            Regex regex = new Regex(pattern);

            MatchCollection matches = regex.Matches(str);

            foreach (Match match in matches)
            {
                convert.Append(match.Groups[0].ToString());
            }

            var sonuc= convert.ToString().TrimStart('0').Trim();

            return sonuc;
        }


    }
}
