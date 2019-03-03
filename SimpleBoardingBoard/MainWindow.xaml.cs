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
using System.Windows.Threading;



namespace SimpleBoardingBoard
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {

        private viewModel vm;

//        private stateAdmin sAdmin;

        public MainWindow()
        {
            InitializeComponent();
            
            vm = new viewModel();

             setTextBlock();

            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += timer_Tick;
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();

            this.beforeTop = Top;
            this.beforeLeft = Left;

            this.DataContext = vm;

        }

        //入力ダイアログ
        public void callInputWindow()
        {
            

            var dataInputWindow = new DataInputWindow(this.vm.sAdmin);

            dataInputWindow.ShowDialog();
            //入力された
            if (dataInputWindow.isOK == true)
            {
                this.vm.applyInputData(dataInputWindow.sAdmin);
                //開始
                this.vm.restartState();
            }
        }

        //イベント設定ウィンドウ
        public void callAddEvent()
        {
            if ((this.vm.sAdmin.iData.bInputCompFlg == false) ||
                (this.vm.sAdmin.nowState == stateAdmin.State.cancelled))
                return;

            var addEventWindow = new AddEventWindow(this.vm.sAdmin);

            addEventWindow.ShowDialog();
            //入力された
            if (addEventWindow.isOK == true)
            {
                this.vm.addEvent(addEventWindow.eventDef, (int)addEventWindow.sldDeleyMinutes.Value,addEventWindow.cancelReason);             
            }

        }

        //リセット
        public void callReset()
        {
            this.vm.resetState();
        }

        //リスタート
        public void callRestart()
        {
            this.vm.restartState();
        }

        //back
        public void callBack()
        {
            this.vm.back();
        }

        //next
        public void callNext()
        {
            this.vm.next();
        }

        //日/英切り替え
        public void callJaEn()
        {
            this.vm.switchLangManual();
        }

        private double beforeTop;
        private double beforeLeft;
        private Point beforeMousePos;

        void MainWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //ボタン押下時点のウィンドウ位置
            this.beforeTop = Top;
            this.beforeLeft = Left;

            //ボタン押下時点のマウス相対位置
            this.beforeMousePos = e.GetPosition(this);


            //非最小、非最大化時のみウィンドウのドラッグ処理を起こす
            if (this.WindowState == System.Windows.WindowState.Normal)
            { 
                if (e.ButtonState != MouseButtonState.Pressed) return;
                this.DragMove();
            }
        }

        private void Window_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Boolean isClick = false;

            //ボタンリリース時のマウス相対位置
            Point now = e.GetPosition(this);

            // "ドラッグではなくクリック"と判定する条件
            // 1.ウィンドウ位置が変わっていない
            if ((this.beforeTop == Top) && (this.beforeLeft == Left))
            {
                isClick = true;
            }
            // 2.最大化時、マウス相対位置が変わっていない (変わっているならfalseに倒す)
            if ((this.WindowState == System.Windows.WindowState.Maximized) &&
                ((this.beforeMousePos.X != now.X) || (this.beforeMousePos.Y != now.Y))
               )               
            {
                isClick = false;
            }

            if (isClick == true)
            {
                //メニューが出ていたら非表示
                if (this.menuFrame.Visibility == System.Windows.Visibility.Visible)
                {
                    this.menuShowHide(System.Windows.Visibility.Hidden);
                } else
                {
                    this.vm.next();
                }
            }

        }


        


        private void Window_MouseMove(object sender, MouseEventArgs e)
        {

        }

        //一秒ごとに呼ばれるイベント
        void timer_Tick(object sender, EventArgs e)
        {
            //現在時刻リフレッシュ
            this.vm.refreshTime();

            //言語自動切換えリフレッシュ・設定
            this.vm.tickAutoLangTime();
        }

        private void setTextBlock()
        {
            
        }

        public void menuShowHide(System.Windows.Visibility state)
        {
            this.menuFrame.Visibility = state;
            this.topFrame.Visibility = state;
        }


        private void Window_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            //メニュー表示 / 非表示切り替え
            if (this.menuFrame.Visibility == System.Windows.Visibility.Hidden) { 
                this.menuShowHide(System.Windows.Visibility.Visible);
            }
            else
            {
                this.menuShowHide(System.Windows.Visibility.Hidden);

            }
        }

        private void menuFrame_MouseEnter(object sender, MouseEventArgs e)
        {

        }

        private void menuFrame_MouseLeave(object sender, MouseEventArgs e)
        {

        }

        public void setMinimize()
        {
            this.WindowState = WindowState.Minimized;
        }

        public void setMaximize()
        {
            if (this.WindowState == WindowState.Normal)
                this.WindowState = WindowState.Maximized;
            else if (this.WindowState == WindowState.Maximized)
                this.WindowState = WindowState.Normal;
            else
                return;

        }

        public void callLangAuto()
        {
            this.vm.switchLangAutoSettings();
        }


        //保存ファイル名
        public static String screenShotFileName = "screenshot.png";

        public void callShareButton()
        {
            //メニュー隠す
            this.menuShowHide(System.Windows.Visibility.Hidden);

            int width;
            int height;

            if ((this.WindowState == System.Windows.WindowState.Maximized))
            {
                width = (int)SystemParameters.PrimaryScreenWidth;
                height = (int)SystemParameters.PrimaryScreenHeight;
            }
            else
            {
                width = (int)this.Width;
                height = (int)this.Height;
            }

            //スクリーンショット保存
            RenderTargetBitmap bmp = new RenderTargetBitmap(width, height, 96.0d, 96.0d, PixelFormats.Pbgra32);
            bmp.Render(this);

            try {
                System.IO.FileStream fs = new System.IO.FileStream(MainWindow.screenShotFileName, System.IO.FileMode.Create);
                PngBitmapEncoder pbe = new PngBitmapEncoder();
                pbe.Frames.Add(BitmapFrame.Create(bmp));
                pbe.Save(fs);
                fs.Close();

                System.Diagnostics.Process p = System.Diagnostics.Process.Start(MainWindow.screenShotFileName);
            }
            catch (Exception ex)
            {
                //何もしない
                return;
            }

            
        }

   
    }

}
