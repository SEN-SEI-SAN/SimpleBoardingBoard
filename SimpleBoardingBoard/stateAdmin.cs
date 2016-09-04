using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace SimpleBoardingBoard
{
    public class stateAdmin 
    {
        //表示するメッセージを決める状態を定義する
        public enum State
        {

            standby = 0x00,
            preboard = 0x01,
            priority = 0x02,
            congestion = 0x03,
            boarding = 0x04,
            finalcall = 0x05,
            endshow = 0x06,

            checkwether = 0x07,
            cancelled = 0x08,

            noinput = 0x09,

        }

        //言語の定義
        public enum Language
        {
            japanese = 0,
            english,
        }

        //イベントの定義
        public enum EventDef
        {
            notselect = 0,
            checkweather,
            changeTime,
            remarksFlight,
            cancel,
        }

        //欠航理由の定義
        public enum CancelReason
        {
            badweather=0,
            fail,
            notselect,
        }

        //日/英自動切換えの時間定義
        public enum changeAutoLangTime
        {
            start = 0,
            now = 20,
            invalid = 0xff,
        }

        public enum codesharemax
        {
            value = 4,
        }

        //保存ファイル名
        public static String inputDataSaveFileName = "inputData.xml";

        //現状態を格納
        public stateAdmin.State nowState { get; set; }

        //現言語を格納。0：日 1：英
        public stateAdmin.Language nowLang;

        //備考1～4を格納
        public String[,] strRemarks { get; set; }

        //備考種1出力済み
        public bool bSetRemarks1 { get; set; }

        //備考種2出力済み
        public bool bSetRemarks2 { get; set; }

        //備考種3出力済み
        public bool bSetRemarks3 { get; set; }

        //備考種4出力済み
        public bool bSetRemarks4 { get; set; }

        //出発時刻変更したかフラグ
        public bool bChangeDepTime;

        //変更時刻
        public DateTime dtChangeDepTime { get; set; }

        //キャンセル理由
        public stateAdmin.CancelReason reason;

        //入力データ
        public inputData iData { get; set; }

        //言語自動切換え時間カウント値
        public uint uiChangeLangTime;

        //主運行画像取得成功flg
        public bool bMainFltImg;

        //主運行ロゴ画像
        public BitmapImage bmpMainFlt;

        //コードシェア画像取得成功flg
        public bool[] bShareFltImg;

        //コードシェアロゴ画像
        public BitmapImage[] bmpShareFlt;

        //主運行名前テキスト取得
        public String getMainFltName()
        {
            if (this.bMainFltImg == true)
                return "";
            else
                return this.iData.strMainFltName;
        }

        //コードシェア名前テキスト取得
        public String getShareFltName(int idx)
        {
            if (this.bShareFltImg[idx] == true)
                return "";
            else
                return this.iData.strFltShareName[idx];
        }

        //引数キーワードと一致する画像をロードする
        public BitmapImage getImgWithKeyword(String key)
        {
            BitmapImage retImg;

            retImg = new BitmapImage();
            try
            {
                retImg.BeginInit();
                retImg.UriSource = new Uri(System.IO.Path.GetFullPath(".\\logo\\" + key + ".png"));
                retImg.EndInit();                
            }
            catch (Exception ex)
            {
                retImg = null;
            }

            return retImg;
        }

        //主運行画像return
        public BitmapImage getMainFltImg()
        {
            if (this.bMainFltImg == true)
                return this.bmpMainFlt;
            else
                return null;
        }

        //シェア画像return
        public BitmapImage getShareFltImg(int idx)
        {
            if (this.bShareFltImg[idx] == true)
                return this.bmpShareFlt[idx];
            else
                return null;
        }

        public void inputDataFromFileAuto()
        {

            System.Xml.Serialization.XmlSerializer serial =
                new System.Xml.Serialization.XmlSerializer(typeof(inputData));

            //オープンをトライ。成功時のみ読み込み
            try
            {
                System.IO.StreamReader sr =
                    new System.IO.StreamReader(stateAdmin.inputDataSaveFileName, new System.Text.UTF8Encoding(false));

                this.iData = (inputData)serial.Deserialize(sr);

                sr.Close();

                this.iData.bInputCompFlg = true;
                restartState();
            }
            catch
            {
                return;
            }

        }

        public bool outputDataToFile()
        {

            System.Xml.Serialization.XmlSerializer serial =
                new System.Xml.Serialization.XmlSerializer(typeof(inputData));

            try
            {
                System.IO.StreamWriter sw = new System.IO.StreamWriter(stateAdmin.inputDataSaveFileName, false, new System.Text.UTF8Encoding(false));
                serial.Serialize(sw, this.iData);

                sw.Close();

                return true;

            }
            catch (Exception ex)
            {
               return false;
            }

        }

        //コンストラクタ
        public stateAdmin()
        {
            this.nowState = stateAdmin.State.noinput;
            this.strRemarks = new String[2,4];
            this.iData = new inputData();
            this.nowLang = stateAdmin.Language.japanese;
            this.bChangeDepTime = false;
            this.reason = stateAdmin.CancelReason.badweather;
            this.bSetRemarks1 = false;
            this.bSetRemarks2 = false;
            this.bSetRemarks3 = false;
            this.bSetRemarks4 = false;
            this.dtChangeDepTime = new DateTime(0);
            this.uiChangeLangTime = (uint)changeAutoLangTime.invalid;
            this.bMainFltImg = false;
            this.bmpShareFlt = new BitmapImage[4];

            this.bShareFltImg = new bool[4];

            for (int idx=0; idx<(int)stateAdmin.codesharemax.value; idx++)
            {
                this.bShareFltImg[idx] = false;
            }

            //データファイル読み取り反映
            this.inputDataFromFileAuto();
        }

        //状態を出発準備に戻す
        //備考を削除する
        public void restartState()
        {
            if (this.iData.bInputCompFlg == false)
                return;
            this.nowState = stateAdmin.State.standby;
            this.strRemarks = new String[2, 4];
            this.bChangeDepTime = false;
            this.reason = stateAdmin.CancelReason.badweather;
            this.bSetRemarks1 = false;
            this.bSetRemarks2 = false;
            this.bSetRemarks3 = false;
            this.bSetRemarks4 = false;
            this.dtChangeDepTime = new DateTime(0);
            this.uiChangeLangTime = (uint)changeAutoLangTime.invalid;

            //備考1へ案内時刻
            this.addRemarksJa(this.getStartBoardingTime(Language.japanese));
            this.addRemarksEn(this.getStartBoardingTime(Language.english));

            //ロゴ画像読み込み
            this.bmpMainFlt = this.getImgWithKeyword(this.iData.strMainFltName);
            if (this.bmpMainFlt != null)
                this.bMainFltImg = true;
            else
                this.bMainFltImg = false;

            for (int idx = 0; idx < (int)stateAdmin.codesharemax.value; idx++)
            {
                this.bmpShareFlt[idx] = this.getImgWithKeyword(this.iData.strFltShareName[idx]);

                if (this.bmpShareFlt[idx] != null)
                    this.bShareFltImg[idx] = true;
                else
                    this.bShareFltImg[idx] = false;
            }


        }


        //状態を未入力に戻す
        public void resetState()
        {
            this.nowState = stateAdmin.State.noinput;
            this.strRemarks = new String[2,4];
            this.iData = new inputData();
            this.nowLang = stateAdmin.Language.japanese;
            this.bChangeDepTime = false;
            this.reason = stateAdmin.CancelReason.badweather;
            this.bSetRemarks1 = false;
            this.bSetRemarks2 = false;
            this.bSetRemarks3 = false;
            this.bSetRemarks4 = false;
            this.dtChangeDepTime = new DateTime(0);
            this.uiChangeLangTime = (uint)changeAutoLangTime.invalid;
            this.bMainFltImg = false;
            for (int idx = 0; idx < (int)stateAdmin.codesharemax.value; idx++)
            {
                this.bShareFltImg[idx] = false;
            }

        }


        //状態を一段階進める。
        //出発済み(endshow)以降には進めさせない。
        //欠航をセットした後は何もしない。
        public void next()
        {
            if (this.nowState == stateAdmin.State.cancelled)
                return;

            if ((this.nowState < stateAdmin.State.endshow) &&
                (this.nowState >= stateAdmin.State.standby))
            {
                this.nowState++;
                //混雑搭乗段階
                if (this.nowState == stateAdmin.State.congestion) 
                {
                    //混雑フラグON
                    if (this.iData.bCongFlg == true)
                    { 
                        //備考群に追加
                       //this.addCongText();
                    }
                    else
                    {
                        //フラグOFFの場合、搭乗段階へ進める
                        this.nowState++;
                    }
                }
            }

        }

        //状態を一段階戻す
        //出発準備中(standby)以前には戻さない。
        //欠航をセットした後は何もしない。
        public void back()
        {
            if (this.nowState == stateAdmin.State.cancelled)
                return;

            if ((this.nowState > stateAdmin.State.standby) &&
                (this.nowState <= stateAdmin.State.endshow))
            {
                this.nowState--;

                //混雑搭乗段階
                if (this.nowState == stateAdmin.State.congestion)
                {
                    //混雑フラグON
                    if (this.iData.bCongFlg == true)
                    {
                        //備考群に追加
                        //this.addCongText();
                    }
                    else
                    {
                        //フラグOFFの場合、優先搭乗へ戻す
                        this.nowState--;
                    }
                }

            }
        }


        //状態を欠航に設定する
        //既存の日・英の備考1～4は全て削除し、備考1に欠航テキストを入れる
        //コール元は表示更新すること
        //引数： 0 悪天候
        //       1 機材故障
        //       上記以外を設定した場合は何もしない
        public void setCancel(stateAdmin.CancelReason lreason)
        {
            if (lreason > stateAdmin.CancelReason.fail)
                return;

            this.reason = lreason;

            this.strRemarks = new String[2,4];
            this.strRemarks[0,0] = this.pullCancelled(stateAdmin.Language.japanese);
            this.strRemarks[1,0] = this.pullCancelled(stateAdmin.Language.english);

            this.nowState = stateAdmin.State.cancelled;

        }

        //混雑メッセージを備考群に追加
        public void addCongText()
        {
            if (this.bSetRemarks1 == true)
                return;
            this.addRemarksJa(this.getCongText(stateAdmin.Language.japanese));
            this.addRemarksEn(this.getCongText(stateAdmin.Language.english));
            this.bSetRemarks1 = true;
        }

        //天候調査メッセージを備考群に追加
        public void setCheckWeather()
        {
            if (this.bSetRemarks2 == true)
                return;
            this.addRemarksJa(this.getChkWeather(stateAdmin.Language.japanese));
            this.addRemarksEn(this.getChkWeather(stateAdmin.Language.english));
            this.bSetRemarks2 = true;
        }

        //条件付き運行を備考群に設定
        public void setRemarksFlight()
        {
            if (this.bSetRemarks3 == true)
                return;
            this.addRemarksJa(this.getRemarksFlight(stateAdmin.Language.japanese));
            this.addRemarksEn(this.getRemarksFlight(stateAdmin.Language.english));
            this.bSetRemarks3 = true;
        }

        //機内案内時刻を設定
        public void setStartBoardingTimet()
        {
            if (this.bSetRemarks4 == true)
                return;
            this.addRemarksJa(this.getStartBoardingTime(stateAdmin.Language.japanese));
            this.addRemarksEn(this.getStartBoardingTime(stateAdmin.Language.english));
            this.bSetRemarks4 = true;
        }

        //日本語用備考を備考テキスト群に追加する
        //備考入力欄0～3のうち、空いているところに設定
        //欠航の設定の時にこのメソッドを使わないこと
        //コール元は表示更新すること
        //引数：設定するテキスト
        //      ここでは引数の文字列を設定するのみ
        //戻り値：挿入した備考番号 0～3
        //        0xFF は全部埋まっていて未実施
        public byte addRemarksJa(String strRemarksText)
        {
            byte remarksNo = 0;

            for ( ;remarksNo < this.strRemarks.GetLength(1); remarksNo++)
            {
                if (this.strRemarks[0,remarksNo] == null)
                {
                    this.strRemarks[0,remarksNo] = strRemarksText;
                    break;
                }
            }

            if (remarksNo == this.strRemarks.GetLength(1))
            {
                remarksNo = 0xFF;
            }

            return remarksNo;
        }

        //英語用備考を備考テキスト群に追加する
        //備考入力欄0～3のうち、開いているところに設定
        //欠航の設定の時にこのメソッドを使わないこと
        //コール元は表示更新すること
        //引数：設定するテキスト
        //      ここでは引数の文字列を設定するのみ
        //戻り値：挿入した備考番号 0～3
        //        0xFF は全部埋まっていて未実施
        public byte addRemarksEn(String strRemarksText)
        {
            byte remarksNo = 0;

            for (; remarksNo < this.strRemarks.GetLength(1); remarksNo++)
            {
                if (this.strRemarks[1,remarksNo] == null)
                {
                    this.strRemarks[1,remarksNo] = strRemarksText;
                    break;
                }
            }

            if (remarksNo == this.strRemarks.GetLength(1))
            {
                remarksNo = 0xFF;
            }

            return remarksNo;
        }



        //状態テキスト出力
        public String getStateText()
        {
            if (this.nowState == State.congestion)
                return this.getCongText(this.nowLang);
            else
                return stringResource.stateText[(uint)this.nowState, (uint)this.nowLang ];
        }

        //時刻タイトル出力
        public String getNowTimeTitle()
        {
            return stringResource.nowTimeTitle[(uint)this.nowLang];
        }

        //便名タイトル出力
        public String getFltNoTitle()
        {
            return stringResource.fltNoTitle[(uint)this.nowLang];
        }

        //目的地タイトル出力
        public String getToTitle()
        {
            return stringResource.toTitle[(uint)this.nowLang];
        }

        //定刻タイトル出力
        public String getTimeTitle()
        {
            return stringResource.timeTitle[(uint)this.nowLang];
        }

        //時刻変更タイトル出力
        public String getChgTimeTitle()
        {
            if (this.bChangeDepTime == true)
            {
                return stringResource.chgTimeTitle[(uint)this.nowLang];
            }
            else
            {
                return "";
            }
        }

        //備考タイトル出力
        public String getRemarksTitle()
        {
            return stringResource.remarksTitle[(uint)this.nowLang];
        }

        //備考種1 混雑テキスト出力
        public String getCongText(stateAdmin.Language lang)
        {
            String returnText;

            if (lang == stateAdmin.Language.japanese)
            {
                returnText = this.iData.strCongAfterNum + stringResource.congText[(uint)lang, 0];
            }
            else
            {
                returnText = stringResource.congText[(uint)lang, 0] + this.iData.strCongAfterNum + stringResource.congText[(uint)lang, 1];
            }

            return returnText;
        }

        //備考種2 天候調査テキスト出力
        //出発時刻の1時間前とする
        public String getChkWeather(stateAdmin.Language lang)
        {
            String returnText;
            DateTime setTime;

            //時刻変更された場合はそちらを優先
            if (this.bChangeDepTime == true)
            {
                setTime = this.dtChangeDepTime;
            }
            else
            {
                setTime = this.iData.dtDepTime;
            }

            setTime = setTime.AddHours(-1);

            if (lang == stateAdmin.Language.japanese)
            {
                returnText = setTime.ToString("HH:mm") + stringResource.chkWeather[(uint)lang];
            }
            else
            {
                returnText = stringResource.chkWeather[(uint)lang] + setTime.ToString("HH:mm");
            }

            return returnText;
        }

        //備考種3 条件付き運行テキスト出力
        public String getRemarksFlight(stateAdmin.Language lang)
        {
            String returnText;

            if (lang == stateAdmin.Language.japanese)
            {
                returnText = this.iData.strDvJa + stringResource.remarksFlight[(uint)lang];
            }
            else
            {
                returnText = stringResource.remarksFlight[(uint)lang] + this.iData.strDvEn;
            }

            return returnText;
        }

        //備考種4 機内案内時刻テキスト出力
        //出発の10分前
        public String getStartBoardingTime(stateAdmin.Language lang)
        {
            DateTime setTime;
            String returnText;

            //時刻変更された場合はそちらを優先
            if (this.bChangeDepTime == true)
            {
                setTime = this.dtChangeDepTime;
            }
            else
            {
                setTime = this.iData.dtDepTime;
            }

            setTime = setTime.AddMinutes(-10);
            if (lang == stateAdmin.Language.japanese)
            {
                returnText = setTime.ToString("HH:mm") + stringResource.startBoardingTime[(uint)lang];
            }
            else
            {
                returnText = stringResource.startBoardingTime[(uint)lang] + setTime.ToString("HH:mm");
            }

            return returnText;
        }

        //備考種5 欠航テキスト出力
        public String pullCancelled(stateAdmin.Language lang)
        {
            String returnText;

            if (lang == stateAdmin.Language.japanese)
            {
                returnText = stringResource.CancelReason[(uint)this.reason, (uint)lang] + stringResource.CancelledTxt[(uint)lang];
            }
            else
            {
                returnText = stringResource.CancelledTxt[(uint)lang] + stringResource.CancelReason[(uint)this.reason, (uint)lang];
            }

            return returnText;
        }

        //現在時刻出力
        public String getNowTime()
        {
            String returnText;

            //現在時刻変更
            if ((this.iData.bInputCompFlg == true) &&
                (this.iData.bChgNowTimeFlg == true))
            {
                returnText = this.iData.dtChangeNowTime.ToString("HH:mm");
            }
            else
            {
                returnText = DateTime.Now.ToString("HH:mm");
            }


            return returnText;
        }

        //現在時刻更新
        public void updateNowTime()
        {
            //現在時刻変更
            if ((this.iData.bInputCompFlg == true) &&
                (this.iData.bChgNowTimeFlg == true) &&
                (this.iData.bTickNowTime == true)
                )
            {
                this.iData.dtChangeNowTime = this.iData.dtChangeNowTime.AddSeconds(1);
            }
        }

        //目的地出力
        public String getTo()
        {
            String returnText;

            if (this.nowLang == stateAdmin.Language.japanese)
            {
                returnText = this.iData.strToJa;
            }
            else
            {
                returnText = this.iData.strToEn;
            }

            return returnText;
        }

        //現在時刻出力
        public String getDepTime()
        {
            return this.iData.dtDepTime.ToString("HH:mm");
        }

        //変更時刻出力
        public String getChangeTime()
        {
            if (this.bChangeDepTime == true)
            {
                return this.dtChangeDepTime.ToString("HH:mm");
            }
            else
            {
                return "";
            }
        }

        //変更時刻設定
        public void setChangeTime(int minute)
        {
            this.bChangeDepTime = true;

            this.dtChangeDepTime = this.iData.dtDepTime.AddMinutes(minute);

            //備考スロット１の案内時刻を更新
            this.strRemarks[0, 0] = this.getStartBoardingTime(Language.japanese);
            this.strRemarks[1, 0] = this.getStartBoardingTime(Language.english);

        }

        //備考出力
        public String getRemarks(int idx)
        {
            if (idx > 3)
                return "";

            return this.strRemarks[(uint)this.nowLang, idx];

        }

        //言語自動切換えカウントを進める
        //戻り値がtrueなら切り替えタイミング
        public bool tickAndCheckLangAutoChange()
        {
            if (this.uiChangeLangTime == (uint)changeAutoLangTime.invalid)
                return false;

            this.uiChangeLangTime++;

            if ((this.uiChangeLangTime) >= ((uint)changeAutoLangTime.now))
            {
                this.uiChangeLangTime = (uint)changeAutoLangTime.start;
                return true;
            }

            return false;
        }

        //言語自動切換え設定
        public void setChangeLangAuto(bool set)
        {
            if (set == true)
                this.uiChangeLangTime = (uint)changeAutoLangTime.start;
            else
                this.uiChangeLangTime = (uint)changeAutoLangTime.invalid;
        }

        //自動切換え有効無効スイッチ
        public void switchAutoLangSettings()
        {
            if ( (this.iData.bInputCompFlg == false) &&
                 (this.iData.bEngFlg == false))
                return;

            if (this.uiChangeLangTime == (uint)changeAutoLangTime.invalid)
                this.setChangeLangAuto(true);
            else
                this.setChangeLangAuto(false);

        }

        //日・英切り替え
        public void switchLang()
        {
            if ((this.nowState == stateAdmin.State.noinput) ||
                (this.iData.bEngFlg == false))
                return;

            if (this.nowLang == stateAdmin.Language.japanese)
            {
                this.nowLang = stateAdmin.Language.english;
            }
            else
            {
                this.nowLang = stateAdmin.Language.japanese;
            }

        }


    }
}
