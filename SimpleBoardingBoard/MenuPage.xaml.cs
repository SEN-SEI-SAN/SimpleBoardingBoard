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
    /// MenuPage.xaml の相互作用ロジック
    /// </summary>
    public partial class MenuPage : Page
    {
        public MenuPage()
        {
            InitializeComponent();
        }

        void setSAdmin(stateAdmin sAdmin)
        {

        }

        private void btAddData_Click(object sender, RoutedEventArgs e)
        {
            MainWindow parent = (MainWindow)Window.GetWindow(btAddData);
            parent.menuShowHide(Visibility.Hidden);            
            parent.callInputWindow();
        }


        private void Page_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //何もしない
            return;
        }

        private void btReset_Click(object sender, RoutedEventArgs e)
        {
            MainWindow parent = (MainWindow)Window.GetWindow(btAddData);
            parent.menuShowHide(Visibility.Hidden); 
            parent.callReset();

        }

        private void btRestart_Click(object sender, RoutedEventArgs e)
        {
            MainWindow parent = (MainWindow)Window.GetWindow(btAddData);
            parent.menuShowHide(Visibility.Hidden);
            parent.callRestart();

        }

        private void btBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow parent = (MainWindow)Window.GetWindow(btAddData);
            parent.callBack();

        }

        private void btNext_Click(object sender, RoutedEventArgs e)
        {
            MainWindow parent = (MainWindow)Window.GetWindow(btAddData);
            parent.callNext();

        }

        private void btJaEn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow parent = (MainWindow)Window.GetWindow(btAddData);
            parent.callJaEn();

        }

        private void btAddEvent_Click(object sender, RoutedEventArgs e)
        {
            MainWindow parent = (MainWindow)Window.GetWindow(btAddData);
            parent.callAddEvent();
        }


        private void btJaEnAuto_Click(object sender, RoutedEventArgs e)
        {
            MainWindow parent = (MainWindow)Window.GetWindow(btAddData);
            parent.callLangAuto();
        }
    }
}
