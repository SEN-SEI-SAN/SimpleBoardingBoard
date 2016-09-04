using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBoardingBoard
{
    public class inputData 
    {
        /* ゲートNo */
        public String strGateNumber { get; set; }

        /* 主運行航空会社コード */
        public String strMainFltName { get; set; }

        /* 便番号 */
        public String strMainFltNum { get; set; }

        /* コードシェア便1～4 コード*/
        public String[] strFltShareName { get; set; }

        /* コードシェア便1～4 便番号*/
        public String[] strFltShareNum { get; set; }


        /* 英語有無フラグ */
        public Boolean bEngFlg { get; set; }

        /* 目的地日本語 */
        public String strToJa { get; set; }

        /* 目的地英語 (任意) */
        public String strToEn { get; set; }

        /* 代替地日本語 */
        public String strDvJa { get; set; }

        /* 代替地英語 (任意) */
        public String strDvEn { get; set; }

        /* 定刻 */
        public DateTime dtDepTime { get; set; }

        /* 混雑フラグ */
        public Boolean bCongFlg { get; set; }

        /* **列より後ろの番号 (任意)*/
        public String strCongAfterNum { get; set; }

        /* 現在時刻を変更するかフラグ */
        public Boolean bChgNowTimeFlg { get; set; }

        /* 変更後現在時刻 (任意) */
        public DateTime dtChangeNowTime { get; set; }

        /* 現在時刻を進めるかフラグ */
        public Boolean bTickNowTime { get; set; }

        /* (非UI設定) 入力済フラグ */
        public Boolean bInputCompFlg { get; set; }

        public inputData()
        {
            this.strGateNumber = "";

            this.strMainFltName = "";

            this.strMainFltNum = "";

            this.strFltShareName = new String[4];
            this.strFltShareNum = new String[4];

            this.strFltShareName[0] = "";
            this.strFltShareName[1] = "";
            this.strFltShareName[2] = "";
            this.strFltShareName[3] = "";

            this.strFltShareNum[0] = "";
            this.strFltShareNum[1] = "";
            this.strFltShareNum[2] = "";
            this.strFltShareNum[3] = "";

            this.bEngFlg = false;

            this.strToJa = "";

            this.strToEn = "";

            this.strDvJa = "";

            this.strDvEn = "";

            this.dtDepTime = new DateTime(0);

            this.bCongFlg = false;

            this.bChgNowTimeFlg = false;

            this.bTickNowTime = true;
            
            this.bInputCompFlg = false;
        }
    }
}
