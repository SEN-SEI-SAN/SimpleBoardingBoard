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
using System.Windows.Shapes;

namespace SimpleBoardingBoard
{
    /// <summary>
    /// AddEventWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class AddEventWindow : Window
    {
 

        public bool isOK;

        public stateAdmin sAdmin;

        public stateAdmin.EventDef eventDef;

        public stateAdmin.CancelReason cancelReason;

        public AddEventWindow(stateAdmin aAdmin)
        {
            InitializeComponent();

            eventDef = stateAdmin.EventDef.notselect;

            cancelReason = stateAdmin.CancelReason.notselect;

            this.sAdmin = aAdmin;

            isOK = false;

            //副項目は最初は選択不可
            this.sldDeleyMinutes.IsEnabled = false;

            this.rdBadWeather.IsEnabled = false;
            this.rdFailure.IsEnabled = false;

            //既に出したイベントは選択させない
            //天候調査
            if (sAdmin.bSetRemarks2 == true)
                this.rdCheckWeather.IsEnabled = false;
            //出発時刻変更 => 何度でも変更可

            //条件付き運行
            if (sAdmin.bSetRemarks3 == true)
                this.rdRemarksFlt.IsEnabled = false;

            //欠航 => 欠航をセットした後はこのウィンドウ自体が出せない


        }

        private void btInsert_Click(object sender, RoutedEventArgs e)
        {
            if (eventDef == stateAdmin.EventDef.notselect)
            {
                tbInfo.Text = "イベントが選択されていません";
                return;
            }

            if (eventDef == stateAdmin.EventDef.cancel && cancelReason == stateAdmin.CancelReason.notselect)
            {
                tbInfo.Text = "欠航理由が選択されていません";
                return;
            }
            
            isOK = true;
            this.Close();
        }

        private void btCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void rGroup_Checked(object sender, RoutedEventArgs e)
        {
            var rbutton = (RadioButton)sender;

            //副項目をいったん選択不可
            this.sldDeleyMinutes.IsEnabled = false;
            this.rdBadWeather.IsEnabled = false;
            this.rdFailure.IsEnabled = false;

            if (rbutton == this.rdCheckWeather)
                eventDef = stateAdmin.EventDef.checkweather;
                
            if (rbutton == this.rdChangeTime)
            {
                eventDef = stateAdmin.EventDef.changeTime;

                this.sldDeleyMinutes.IsEnabled = true;

            }

            if (rbutton == this.rdRemarksFlt)
                eventDef = stateAdmin.EventDef.remarksFlight;
                
            if (rbutton == this.rdCancel)
            {
                eventDef = stateAdmin.EventDef.cancel;

                this.rdBadWeather.IsEnabled = true;
                this.rdFailure.IsEnabled = true;
            }

         }

        private void rdGroup2_Checked(object sender, RoutedEventArgs e)
        {
            var rbutton = (RadioButton)sender;

            if (rbutton == this.rdBadWeather)
                cancelReason = stateAdmin.CancelReason.badweather;

            if (rbutton == this.rdFailure)
                cancelReason = stateAdmin.CancelReason.fail;
        }
    }
}
