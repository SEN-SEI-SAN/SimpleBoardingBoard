using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBoardingBoard
{
    public static class stringResource
    {
        //インデックス0 : 日本語
        //インデックス1 : 英語

        //状態表示
        //このメンバに入っているテキストは、stateAdmin.State列挙型の数値と同期している。
        public static String[,] stateText =
        {
            { "出発準備中",  "Stand-by" },                            // 0 standby
            { "事前改札ご案内中", "Pre-boarding" },                   // 1 preboard
            { "優先搭乗ご案内中", "Priority-boarding" },              // 2 priority
            { "搭乗ご案内中", "Boarding" },                           // 3 congestion
            { "搭乗ご案内中", "Boarding" },                           // 4 boarding
            { "ご搭乗最終案内", "Final call" },                       // 5 finalcall
            { "出発済", "Departed" },                                 // 6 endshow
            { "天候調査中", "Checking Weather"},                      // 7 checkweather
            { "欠航", "Cancelled" },                                  // 8 cancelled
            { "右クリックメニューからデータを入れてください", "" },   // 9 noinput 
        };

 

        //現在の時刻タイトル
        public static String[] nowTimeTitle =
        {
            "現在の時刻",
            "Now Time",
        };

        //便名タイトル
        public static String[] fltNoTitle =
        {
            "便名",
            "Flight No",
        };

        //目的地タイトル
        public static String[] toTitle =
        {
            "行先",
            "To",
        };

        //定刻タイトル
        public static String[] timeTitle =
        {
            "定刻",
            "Departure time",
        };

        //時刻変更タイトル
        public static String[] chgTimeTitle =
        {
            "変更",
            "Changed",
        };

        //備考タイトル
        public static String[] remarksTitle =
        {
            "備考",
            "Remarks",
        };

        //備考種1。"**"より後方のお客様
        public static String[,] congText =
        {
            {"列より後方のお客様","" },
            {"First boarding at ", " seats and higher" },
        };

        //備考種2。"天候調査中"
        public static String[] chkWeather =
        {
            "に天候調査を行います。",
            "Will check weather at ",
        };

        //備考種3。"条件付き運行"
        public static String[] remarksFlight =
        {
            "に向かうか、引き返す場合があります。",
            "Maybe return, or change destination at ",
        };

        //備考種4。"機内ご案内時刻"
        public static String[] startBoardingTime =
        {
            "頃に機内ご案内予定。",
            "Start boarding at ",
        };

        //備考種5, "欠航"
        public static String[] CancelledTxt =
        {
            "により当便は欠航します。",
            "Flight cannelled. Because ",
        };

        //発生事由
        public static String[,] CancelReason =
        {
            { "悪天候","bad weather" },
            { "機材故障","aircraft fail." },
        };
        
    }
}
