﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace SimpleBoardingBoard
{
    public class viewModel : viewModelBase
    {
        public stateAdmin sAdmin;

        public viewModel()
        {
            sAdmin = new stateAdmin();
        }

        //ゲート番号
        public String vmGateNum
        {
            get { return this.sAdmin.iData.strGateNumber; }

            set
            {
                this.sAdmin.iData.strGateNumber = value;

                applyChange("vmGateNum");
            }
        }

        //状態
        public String vmState
        {
            get { return this.sAdmin.getStateText(); }
        }

        //時刻タイトル
        public String vmNowTimeTitle
        {
            get { return this.sAdmin.getNowTimeTitle(); }
        }

        //現在の時刻
        public String vmNowTime
        {
            get { return this.sAdmin.getNowTime(); }
        }

        //便名タイトル
        public String vmFltNoTitle
        {
            get { return this.sAdmin.getFltNoTitle(); }
        }

        //主運行
        //航空会社コード
        public String vmMainFltName
        {
            get { return this.sAdmin.getMainFltName(); }

            set {
                this.sAdmin.iData.strMainFltName = value;

                applyChange("vmMainFltName");
            }
        }
                
        //航空会社便名
        public String vmMainFltNum
        {
            get { return this.sAdmin.iData.strMainFltNum; }

            set
            {
                this.sAdmin.iData.strMainFltNum = value;

                applyChange("vmMainFltNum");
            }
        }

        //ロゴ
        public BitmapImage vmMainFltLogoImg
        {
            get { return this.sAdmin.getMainFltImg(); }
        }


        //コードシェア1
        public String vmFltShare1
        {
            get { return this.sAdmin.iData.strFltShare[0]; }

            set
            {
                this.sAdmin.iData.strFltShare[0] = value;
                applyChange("vmFltShare1");
            }
        }

        //コードシェア2
        public String vmFltShare2
        {
            get { return this.sAdmin.iData.strFltShare[1]; }

            set
            {
                this.sAdmin.iData.strFltShare[1] = value;
                applyChange("vmFltShare2");
            }
        }

        //コードシェア3
        public String vmFltShare3
        {
            get { return this.sAdmin.iData.strFltShare[2]; }

            set
            {
                this.sAdmin.iData.strFltShare[2] = value;
                applyChange("vmFltShare3");
            }
        }

        //コードシェア4
        public String vmFltShare4
        {
            get { return this.sAdmin.iData.strFltShare[3]; }

            set
            {
                this.sAdmin.iData.strFltShare[3] = value;
                applyChange("vmFltShare4");
            }
        }



        //目的地タイトル
        public String vmToTitle
        {
            get { return this.sAdmin.getToTitle(); }
        }

        //目的地
        public String vmToData
        {
            get { return this.sAdmin.getTo(); }
        }

        //定刻タイトル
        public String vmTimeTitle
        {
            get { return this.sAdmin.getTimeTitle(); }
        }

        //時刻変更タイトル
        public String vmChangeTimeTitle
        {
            get { return this.sAdmin.getChgTimeTitle(); }
        }

        //備考タイトル
        public String vmRemarksTitle
        {
            get { return this.sAdmin.getRemarksTitle(); }
        }

        //定刻
        public String vmTimeData
        {
            get { return this.sAdmin.getDepTime(); }
        }

        //変更時刻
        public String vmChangeTimeData
        {
            get { return this.sAdmin.getChangeTime(); }
        }

        //備考段1
        public String vmRemarks1
        {
            get { return this.sAdmin.getRemarks(0); }
        }

        //備考段2
        public String vmRemarks2
        {
            get { return this.sAdmin.getRemarks(1); }
        }

        //備考段3
        public String vmRemarks3
        {
            get { return this.sAdmin.getRemarks(2); }
        }

        //備考段4
        public String vmRemarks4
        {
            get { return this.sAdmin.getRemarks(3); }
        }

        //状態・データを更新
        //即時表示変化するデータ（以降の状態変化では変わらない情報）
        //のプロパティ更新イベントを投げる
        public void applyInputData(stateAdmin newState)
        {
            this.sAdmin = newState;

            //ゲート番号
            applyChange("vmGateNum");

            //現在時刻
            applyChange("vmNowTime");

            //便名
            applyChange("vmMainFltName");
            applyChange("vmMainFltLogoImg");
            applyChange("vmMainFltNum");

            //コードシェア
            applyChange("vmFltShare1");
            applyChange("vmFltShare2");
            applyChange("vmFltShare3");
            applyChange("vmFltShare4");

            //目的地
            applyChange("vmToData");

            //定刻
            applyChange("vmTimeData");

        }

        //状態変更で変わるメッセージに対してイベントを投げる
        public void sendMsgForChangeState()
        {
            //状態
            applyChange("vmState");
            //備考1～4
            applyChange("vmRemarks1");
            applyChange("vmRemarks2");
            applyChange("vmRemarks3");
            applyChange("vmRemarks4");

        }

        //全ての表示へイベントを投げる
        public void sendMsgAll()
        {
            applyChange("vmGateNum");
            applyChange("vmState");
            applyChange("vmNowTimeTitle");
            applyChange("vmNowTime");
            applyChange("vmFltNoTitle");
            applyChange("vmMainFltName");
            applyChange("vmMainFltLogoImg");
            applyChange("vmMainFltNum");
            applyChange("vmFltShare1");
            applyChange("vmFltShare2");
            applyChange("vmFltShare3");
            applyChange("vmFltShare4");
            applyChange("vmToTitle");
            applyChange("vmToData");
            applyChange("vmTimeTitle");
            applyChange("vmChangeTimeTitle");
            applyChange("vmRemarksTitle");
            applyChange("vmTimeData");
            applyChange("vmChangeTimeData");
            applyChange("vmRemarks1");
            applyChange("vmRemarks2");
            applyChange("vmRemarks3");
            applyChange("vmRemarks4");



        }
        //状態を最初（出発準備に戻す）
        public void restartState()
        {
            this.sAdmin.restartState();
            
            //表示更新
            this.sendMsgAll();
        }

        //状態を未入力に戻す
        public void resetState()
        {
            this.sAdmin.resetState();
            //表示更新
            this.sendMsgAll();

        }

        //次の状態へ
        public void next()
        {
            this.sAdmin.next();
            //表示更新
            this.sendMsgForChangeState();
        }

        //前の状態へ
        public void back()
        {
            this.sAdmin.back();
            //表示更新
            this.sendMsgForChangeState();
        }

        //手動日・英切り替え
        public void switchLangManual()
        {
            sAdmin.switchLang();

            //手動が押されたので、自動は無効にする
            sAdmin.setChangeLangAuto(false);

            //表示更新                        
            this.sendMsgAll();
        }

        //自動日・英切り替え
        public void switchLangAuto()
        {
            sAdmin.switchLang();
            //表示更新                        
            this.sendMsgAll();

        }

        public void switchLangAutoSettings()
        {
            sAdmin.switchAutoLangSettings();
          
        }

        //言語自動切換えtick
        public void tickAutoLangTime()
        {
            bool isnow = sAdmin.tickAndCheckLangAutoChange();
            if (isnow == true)
                this.switchLangAuto(); 
        }

        //時刻更新
        public void refreshTime()
        {
            this.sAdmin.updateNowTime();
            //表示更新
            applyChange("vmNowTime");
        }

        public void addEvent(stateAdmin.EventDef eventDef, int changeTime, stateAdmin.CancelReason cancelReason)
        {
            switch (eventDef)
            {
                case stateAdmin.EventDef.checkweather:
                    this.sAdmin.setCheckWeather();
                    break;
                case stateAdmin.EventDef.changeTime:
                    this.sAdmin.setChangeTime(changeTime);
                    break;
                case stateAdmin.EventDef.remarksFlight:
                    this.sAdmin.setRemarksFlight();
                    break;
                case stateAdmin.EventDef.cancel:
                    this.sAdmin.setCancel(cancelReason);
                    break;
                default:
                    break;
            }

            this.sendMsgAll();

        }

    }
}
