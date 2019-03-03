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

namespace SimpleBoardingBoard
{
    /// <summary>
    /// topOfWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class topOfWindow : Page
    {
        public topOfWindow()
        {
            InitializeComponent();
        }

        private void btMinimize_Click(object sender, RoutedEventArgs e)
        {
            MainWindow parent = (MainWindow)Window.GetWindow(btMinimize);
            parent.setMinimize();

        }

        private void btMaximize_Click(object sender, RoutedEventArgs e)
        {
            MainWindow parent = (MainWindow)Window.GetWindow(btMaximize);
            parent.setMaximize();

        }

        private void btClose_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }


        private void btShare_Click(object sender, RoutedEventArgs e)
        {
            MainWindow parent = (MainWindow)Window.GetWindow(btShare);
            parent.callShareButton();

        }

    }
}
