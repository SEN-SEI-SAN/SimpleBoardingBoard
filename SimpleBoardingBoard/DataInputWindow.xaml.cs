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
    /// DataInputWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class DataInputWindow : Window
    {
        public stateAdmin sAdmin;

        public DataInputWindow() { }

        public DataInputWindow(stateAdmin inpuState)
        {
            InitializeComponent();
            this.sAdmin = inpuState;

            //一回もデータが入力されていない。
            if (this.sAdmin.iData.bInputCompFlg == false)
            {
                this.firstInit();
            }
            else
            {
                this.dataToText();
            }
        }

        public bool isOK { get; set; }

        //初期処理
        private void firstInit()
        {
            this.txtToEn.IsEnabled = false;
            this.txtDvEn.IsEnabled = false;
            this.sldNowTimeHour.IsEnabled = false;
            this.sldNowTimeMinutes.IsEnabled = false;
            this.chkTickChgNowTime.IsEnabled = false;
            this.txtCongNum.IsEnabled = false;
        }

        //データを各要素に設定
        private void dataToText()
        {
            //ゲート番号
            this.txtGateNo.Text = this.sAdmin.iData.strGateNumber;

            //主運行
            this.txtMainFltName.Text = this.sAdmin.iData.strMainFltName;
            this.txtMainFltNum.Text = this.sAdmin.iData.strMainFltNum;

            //コードシェア
            this.txtShare1Name.Text = this.sAdmin.iData.strFltShareName[0];
            this.txtShare2Name.Text = this.sAdmin.iData.strFltShareName[1];
            this.txtShare3Name.Text = this.sAdmin.iData.strFltShareName[2];
            this.txtShare4Name.Text = this.sAdmin.iData.strFltShareName[3];

            this.txtShare1Num.Text = this.sAdmin.iData.strFltShareNum[0];
            this.txtShare2Num.Text = this.sAdmin.iData.strFltShareNum[1];
            this.txtShare3Num.Text = this.sAdmin.iData.strFltShareNum[2];
            this.txtShare4Num.Text = this.sAdmin.iData.strFltShareNum[3];

            //行き先・代替地
            //空港
            this.txtToJa.Text = this.sAdmin.iData.strToJa;
            this.txtDvJa.Text = this.sAdmin.iData.strDvJa;
            //都市
            this.txtToCityJa.Text = this.sAdmin.iData.strToCityJa;
            this.txtDvCityJa.Text = this.sAdmin.iData.strDvCityJa;

            //英語ありフラグ
            this.chkEng.IsChecked = this.sAdmin.iData.bEngFlg;

            if (this.sAdmin.iData.bEngFlg == true)
            { 
                this.txtToEn.IsEnabled = true;
                this.txtToEn.Text = this.sAdmin.iData.strToEn;
                this.txtToCityEn.Text = this.sAdmin.iData.strToCityEn;

                this.txtDvEn.IsEnabled = true;
                this.txtDvEn.Text = this.sAdmin.iData.strDvEn;
                this.txtDvCityEn.Text = this.sAdmin.iData.strDvCityEn;


            }
            else
            {
                this.txtToEn.IsEnabled = false;
                this.txtDvEn.IsEnabled = false;
            }

            //定刻
            this.sldTimeHour.Value = this.sAdmin.iData.dtDepTime.Hour;
            this.sldTimeMinutes.Value = this.sAdmin.iData.dtDepTime.Minute;

            //現在時刻変更フラグ
            this.chkChgNowTime.IsChecked = this.sAdmin.iData.bChgNowTimeFlg;

            //変更現在時刻
            if (this.sAdmin.iData.bChgNowTimeFlg == true)
            {
                this.sldNowTimeHour.IsEnabled = true;
                this.sldNowTimeHour.Value = this.sAdmin.iData.dtChangeNowTime.Hour;

                this.sldNowTimeMinutes.IsEnabled = true;
                this.sldNowTimeMinutes.Value = this.sAdmin.iData.dtChangeNowTime.Minute;

                this.chkTickChgNowTime.IsEnabled = true;
                this.chkTickChgNowTime.IsChecked = this.sAdmin.iData.bTickNowTime;
            }
            else
            {   this.sldNowTimeHour.IsEnabled = false;
                this.sldNowTimeMinutes.IsEnabled = false;
                this.chkTickChgNowTime.IsEnabled = false;

            }

            //混雑フラグ
            this.chkCong.IsChecked = this.sAdmin.iData.bCongFlg;

            if (this.sAdmin.iData.bCongFlg)
            {
                this.txtCongNum.IsEnabled = true;
                this.txtCongNum.Text = this.sAdmin.iData.strCongAfterNum;
            }
            else
            {
                this.txtCongNum.IsEnabled = false;
            }
        }

        //要素の入力値をデータに設定
        private void setData()
        {
            //ゲート番号
            this.sAdmin.iData.strGateNumber = this.txtGateNo.Text;

            //主運行社
            this.sAdmin.iData.strMainFltName = this.txtMainFltName.Text;
            this.sAdmin.iData.strMainFltNum = this.txtMainFltNum.Text;
            
            //コードシェア
            this.sAdmin.iData.strFltShareName[0] = this.txtShare1Name.Text;
            this.sAdmin.iData.strFltShareName[1] = this.txtShare2Name.Text;
            this.sAdmin.iData.strFltShareName[2] = this.txtShare3Name.Text;
            this.sAdmin.iData.strFltShareName[3] = this.txtShare4Name.Text;
            this.sAdmin.iData.strFltShareNum[0] = this.txtShare1Num.Text;
            this.sAdmin.iData.strFltShareNum[1] = this.txtShare2Num.Text;
            this.sAdmin.iData.strFltShareNum[2] = this.txtShare3Num.Text;
            this.sAdmin.iData.strFltShareNum[3] = this.txtShare4Num.Text;


            //目的地・代替地
            //空港
            this.sAdmin.iData.strToJa = this.txtToJa.Text;
            this.sAdmin.iData.strDvJa = this.txtDvJa.Text;
            //都市
            this.sAdmin.iData.strToCityJa = this.txtToCityJa.Text;
            this.sAdmin.iData.strDvCityJa = this.txtDvCityJa.Text;

            this.sAdmin.iData.bEngFlg = (bool)this.chkEng.IsChecked;
            if (this.sAdmin.iData.bEngFlg == true)
            {
                this.sAdmin.iData.strToEn = this.txtToEn.Text;
                this.sAdmin.iData.strDvEn = this.txtDvEn.Text;
                this.sAdmin.iData.strToCityEn = this.txtToCityEn.Text;
                this.sAdmin.iData.strDvCityEn = this.txtDvCityEn.Text;
            }
            else
            {
                this.sAdmin.iData.strToEn = "";
                this.sAdmin.iData.strDvEn = "";
            }

            //定刻
            this.sAdmin.iData.dtDepTime = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day,
                                                        (int)this.sldTimeHour.Value,
                                                        (int)this.sldTimeMinutes.Value,
                                                        0);

            //現在時刻変更
            this.sAdmin.iData.bChgNowTimeFlg = (bool)this.chkChgNowTime.IsChecked;

            if (this.sAdmin.iData.bChgNowTimeFlg == true)
            {
                this.sAdmin.iData.dtChangeNowTime = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day,
                                                                (int)this.sldNowTimeHour.Value,
                                                                (int)this.sldNowTimeMinutes.Value,
                                                                0);

                this.sAdmin.iData.bTickNowTime = (bool)this.chkTickChgNowTime.IsChecked;
            }
            else
            {
                this.sAdmin.iData.bTickNowTime = false;

            }

            //混雑
            this.sAdmin.iData.bCongFlg = (bool)this.chkCong.IsChecked;

            if (this.sAdmin.iData.bCongFlg)
            {
                this.sAdmin.iData.strCongAfterNum = this.txtCongNum.Text;
            }
            else
            {
                this.sAdmin.iData.strCongAfterNum = "";
            }

            bool bResult = this.sAdmin.outputDataToFile();

            if (bResult == false)
            {
                MessageBox.Show("入力データを、ファイル名 " + stateAdmin.inputDataSaveFileName + " で、\r\n" +
                               "実行ファイルがあるフォルダへ保存しようとしましたが、失敗しました。\r\n" +
                               "\r\n" +
                               stateAdmin.inputDataSaveFileName + " が既にあり、別のプログラムで開かれている、ファイル書き込みの権限がない、\r\n" +
                               "ディスク容量がいっぱいなどが原因のようです。\r\n \r\n" +
                               "保存は失敗しましたが、表示はスタートします。");
            }

            //設定が完了したら、入力済みフラグをONにする
            this.sAdmin.iData.bInputCompFlg = true;


        }

 


        private void btCancel_Click(object sender, RoutedEventArgs e)
        {
            this.isOK = false;
            this.Close();
        }

        private void chkEng_Checked(object sender, RoutedEventArgs e)
        {
            this.txtToEn.IsEnabled = true;
            this.txtDvEn.IsEnabled = true;
        }

        private void chkEng_Unchecked(object sender, RoutedEventArgs e)
        {
            this.txtToEn.IsEnabled = false;
            this.txtDvEn.IsEnabled = false;
        }

        private void chkChgNowTime_Checked(object sender, RoutedEventArgs e)
        {
            this.sldNowTimeHour.IsEnabled = true;
            this.sldNowTimeMinutes.IsEnabled = true;
            this.chkTickChgNowTime.IsEnabled = true;
        }

        private void chkChgNowTime_Unchecked(object sender, RoutedEventArgs e)
        {
            this.sldNowTimeHour.IsEnabled = false;
            this.sldNowTimeMinutes.IsEnabled = false;
            this.chkTickChgNowTime.IsEnabled = false;
        }

        private void chkCong_Checked(object sender, RoutedEventArgs e)
        {
            this.txtCongNum.IsEnabled = true;
        }

        private void chkCong_Unchecked(object sender, RoutedEventArgs e)
        {
            this.txtCongNum.IsEnabled = false;
        }

        private void btInsert_Click(object sender, RoutedEventArgs e)
        {
            this.setData();
            Application.Current.Properties["stateAdmin"] = this.sAdmin;
            this.isOK = true;
            this.Close();
        }
    }
}
