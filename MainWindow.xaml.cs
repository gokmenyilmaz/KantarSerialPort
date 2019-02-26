using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace WpfSeriaPort
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        KantarSerialPort k;
      

        private void BtnBaglan_Click(object sender, RoutedEventArgs e)
        {
            k = new KantarSerialPort(txtPort.Text, "9600", "None", "8", "One");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            k.MesajGonder(txtGidenMesaj.Text);
        }


        private void BtnKantardanAl_Click(object sender, RoutedEventArgs e)
        {
            if (k == null) BtnBaglan_Click(null, null);

            KantarSonucWindow w = new KantarSonucWindow(k);

            var cev=  w.ShowDialog();

            if(cev==true)
            {
                txtKantarPandap.Text = w.txtKantarSonuc.Text;
            }

            k.dinlemeyiDurdur();
           
        }
       
    }
}
