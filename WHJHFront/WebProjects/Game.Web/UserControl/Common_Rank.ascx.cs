using Game.Entity.Accounts;
using Game.Entity.NativeWeb;
using Game.Facade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Game.Web.UserControl
{
    public partial class Common_Rank : System.Web.UI.UserControl
    {
        //控件变量
        protected string RankType { get; set; }
        protected string RankData { get; set; }

        /// <summary>
        /// 控件加载
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                //显示排行榜类型
                SystemStatusInfo status = FacadeManage.aideAccountsFacade.GetSystemStatusInfo(AppConfig.ConfigInfoKey.JJRankingListType.ToString());
                if(status != null && status.StatusValue > 0)
                {
                    //1-财富，2-消耗，3-财富和消耗，4-战绩，5-财富和战绩,6-消耗和战绩,7-财富和消耗和战绩
                    DataSet ds = FacadeManage.aideNativeWebFacade.GetDayRankingData(status.StatusValue);
                    switch(status.StatusValue)
                    {
                        case 1:
                            RankType = AppendTitle(1, "财富", true);
                            RankData = AppendHtml(ds.Tables[0], 1, false, false);
                            break;
                        case 2:
                            RankType = AppendTitle(2, "消耗", true);
                            RankData = AppendHtml(ds.Tables[0], 2, false, false);
                            break;
                        case 3:
                            RankType = AppendTitle(1, "财富", true) + AppendTitle(2, "消耗", false);
                            RankData = AppendHtml(ds.Tables[0], 1, false, false) + AppendHtml(ds.Tables[1], 2, true, false);
                            break;
                        case 4:
                            RankType = AppendTitle(4, "战绩", true);
                            RankData = AppendHtml(ds.Tables[0], 4, false, true);
                            break;
                        case 5:
                            RankType = AppendTitle(1, "财富", true) + AppendTitle(4, "战绩", false);
                            RankData = AppendHtml(ds.Tables[0], 1, false, false) + AppendHtml(ds.Tables[1], 4, true, true);
                            break;
                        case 6:
                            RankType = AppendTitle(2, "消耗", true) + AppendTitle(4, "战绩", false);
                            RankData = AppendHtml(ds.Tables[0], 2, false, false) + AppendHtml(ds.Tables[1], 4, true, true);
                            break;
                        case 7:
                            RankType = AppendTitle(1, "财富", true) + AppendTitle(2, "消耗", false) + AppendTitle(4, "战绩", false);
                            RankData = AppendHtml(ds.Tables[0], 1, false, false) + AppendHtml(ds.Tables[1], 2, true, false) + AppendHtml(ds.Tables[2], 4, true, true);
                            break;
                        default:
                            break;
                    }
                }
            }
        }
        /// <summary>
        /// 拼接HTML排行名称
        /// </summary>
        /// <param name="typeid">排行类型</param>
        /// <param name="typename">排行名称</param>
        /// <param name="first">是否第一个显示</param>
        /// <returns></returns>
        private string AppendTitle(int typeid, string typename, bool first)
        {
            return string.Format("<li data-type=\"{0}\" {1}><a>{2}榜</a></li>", typeid, first ? "class=\"active\"" : "", typename);
        }
        /// <summary>
        /// 拼接HTML排行数据
        /// </summary>
        /// <param name="table">数据排行</param>
        /// <param name="typeid">排行类型</param>
        /// <param name="hide">是否默认隐藏HTML</param>
        /// <param name="flag">是否为战绩排行</param>
        /// <returns></returns>
        private string AppendHtml(DataTable table, int typeid, bool hide, bool flag)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<ul class=\"ui-rank-details{0}\" data-type=\"{1}\">", hide? " hide" : "", typeid);
            if(table != null && table.Rows.Count > 0)
            {
                DataRow row = null;
                for(int i = 0; i < table.Rows.Count; i++)
                {
                    row = table.Rows[i];
                    sb.Append("<li class=\"fn-clear\">");
                    sb.AppendFormat("<span class=\"ui-rank-num\">{0}</span>",
                       i >= 3 ? (i + 1).ToString() : string.Format("<img src=\"/image/{0}.png\">", (i + 1)));
                    sb.AppendFormat("<span class=\"ui-rank-name {0}\">{1}</span>", (i > 2 ? "" : "top-rank "), row["NickName"].ToString());
                    sb.AppendFormat("<span class=\"ui-score fn-right {0}\">{1}</span>", (i > 2 ? "" : "top-rank "), flag ? row["Score"].ToString() : row["Diamond"].ToString());
                    sb.Append("</li>");
                }
            }
            sb.Append("</ul>");
            return sb.ToString();
        }
    }
}