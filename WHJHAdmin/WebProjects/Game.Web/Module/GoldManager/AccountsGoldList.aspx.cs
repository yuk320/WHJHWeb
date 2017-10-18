using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Game.Web.UI;
using Game.Kernel;
using System.Text;
using Game.Utils;
using Game.Entity.Accounts;
using Game.Entity;
using Game.Entity.Enum;
using Game.Entity.GameScore;
using Game.Facade;

namespace Game.Web.Module.GoldManager
{
    public partial class AccountsGoldList : AdminPage
    {
        /// <summary>
        /// 页面加载
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                GoldDataBind();
            }
        }
        /// <summary>
        /// 数据分页
        /// </summary>
        protected void anpPage_PageChanged(object sender, EventArgs e)
        {
            GoldDataBind();
        }
        /// <summary>
        /// 数据查询
        /// </summary>
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            string queryContent = CtrlHelper.GetTextAndFilter(txtSearch);
            int searchType = int.Parse(ddlSearchType.SelectedValue);
            StringBuilder condition = new StringBuilder();
            condition.Append(" WHERE (1=1) ");

            if(!string.IsNullOrEmpty(queryContent))
            {
                switch(searchType)
                {
                    case 2:
                        if(Utils.Validate.IsPositiveInt(queryContent))
                            condition.AppendFormat(" AND UserID={0}", GetUserIDByGameID(Convert.ToInt32(queryContent)));
                        else
                        {
                            ShowError("你输入的游戏ID必须为正整数");
                            return;
                        }
                        break;
                    case 3:
                        if(Utils.Validate.IsPositiveInt(queryContent))
                            condition.AppendFormat(" AND Score>={0} ", queryContent);
                        else
                        {
                            ShowError("你输入的游戏币数必须为正整数");
                            return;
                        }
                        break;
                    case 4:
                        if(Utils.Validate.IsPositiveInt(queryContent))
                            condition.AppendFormat(" AND Score<={0} ", queryContent);
                        else
                        {
                            ShowError("你输入的游戏币数必须为正整数");
                            return;
                        }
                        break;
                    case 5:
                        if(Utils.Validate.IsPositiveInt(queryContent))
                            condition.AppendFormat(" AND InsureScore>={0} ", queryContent);
                        else
                        {
                            ShowError("你输入的游戏币数必须为正整数");
                            return;
                        }
                        break;
                    case 6:
                        if(Utils.Validate.IsPositiveInt(queryContent))
                            condition.AppendFormat(" AND InsureScore<={0} ", queryContent);
                        else
                        {
                            ShowError("你输入的游戏币数必须为正整数");
                            return;
                        }
                        break;
                    case 7:
                        if(Utils.Validate.IsPositiveInt(queryContent))
                            condition.AppendFormat(" AND Revenue>={0} ", queryContent);
                        else
                        {
                            ShowError("你输入的税收数必须为正整数");
                            return;
                        }
                        break;
                    case 8:
                        if(Utils.Validate.IsPositiveInt(queryContent))
                            condition.AppendFormat(" AND Revenue<={0} ", queryContent);
                        else
                        {
                            ShowError("你输入的税收数必须为正整数");
                            return;
                        }
                        break;
                    default:
                        break;
                }
            }
            ViewState["SearchItems"] = condition.ToString();
            GoldDataBind();
        }
        /// <summary>
        /// 数据绑定
        /// </summary>  
        private void GoldDataBind()
        {
            PagerSet pagerSet = FacadeManage.aideTreasureFacade.GetList(GameScoreInfo.Tablename, anpPage.CurrentPageIndex, anpPage.PageSize, SearchItems, Orderby);
            anpPage.RecordCount = pagerSet.RecordCount;
            litNoData.Visible = pagerSet.PageSet.Tables[0].Rows.Count > 0 ? false : true;
            rptDataList.DataSource = pagerSet.PageSet;
            rptDataList.DataBind();
        }
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public string GetAccountsInfo(int userid)
        {
            AccountsInfo info = FacadeManage.aideAccountsFacade.GetAccountInfoByUserId(userid);
            if(info != null)
            {
                return string.Format("<td>{0}</td><td><a class=\"l\" href=\"javascript: void(0)\" onclick=\"openWindowOwn('/Module/AccountManager/AccountsBaseInfo.aspx?param={1}', 'RecordPersonRoom', 850,790);\">{2}</a></td>", info.GameID, info.UserID, info.NickName);
            }
            return "<td></td><td></td>";
        }
        /// <summary>
        /// 查询条件
        /// </summary>
        public string SearchItems
        {
            get
            {
                if(ViewState["SearchItems"] == null)
                {
                    StringBuilder condition = new StringBuilder();
                    condition.Append(" WHERE 1=1 ");
                    ViewState["SearchItems"] = condition.ToString();
                }

                return (string)ViewState["SearchItems"];
            }
            set
            {
                ViewState["SearchItems"] = value;
            }
        }
        /// <summary>
        /// 排序条件
        /// </summary>
        public string Orderby
        {
            get
            {
                if(ViewState["Orderby"] == null)
                {
                    ViewState["Orderby"] = "ORDER BY UserID DESC";
                }
                return (string)ViewState["Orderby"];
            }
            set
            {
                ViewState["Orderby"] = value;
            }
        }
    }
}