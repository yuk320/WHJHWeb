using System;
using System.Web.UI.WebControls;
using Game.Entity.Accounts;
using Game.Entity.Enum;
using Game.Web.UI;
using Game.Facade;
using Game.Utils;
using Game.Kernel;
using Game.Entity.Treasure;
using Game.Entity.Platform;

namespace Game.Web.Module.AccountManager
{
    public partial class UserPlaying : AdminPage
    {
        #region 事件

        protected void Page_Load(object sender, EventArgs e)
        {
            AuthUserOperationPermission(Permission.Read);
            if (IsPostBack) return;
            BindServerList();
            BindingData();
        }

        protected void btnQueryOrder_Click(object sender, EventArgs e)
        {
            BindingData();
        }

        protected void btnCleanLocker_Click(object sender, EventArgs e)
        {
            AuthUserOperationPermission(Permission.Add);
            if (StrCIdList.Length == 0)
            {
                ShowError("请选择需要清除卡线的玩家");
                return;
            }

            int result = FacadeManage.aideTreasureFacade.CleanGameScoreLocker(StrCIdList);
            if (result > 0)
            {
                ShowInfo("清除卡线成功");
                BindingData();
            }
            else
            {
                ShowError("清除卡线失败");
            }
        }

        protected void anpPage_PageChanged(object sender, EventArgs e)
        {
            BindingData();
        }

        #endregion

        #region 数据方法

        protected void BindingData()
        {
            string type = ddlSearchType.SelectedValue;
            string search = CtrlHelper.GetText(txtSearch);
            string where = string.Empty;

            if (!string.IsNullOrEmpty(search))
            {
                int userId;
                switch (type)
                {
                    case "3":
                        if (!Utils.Validate.IsPositiveInt(search))
                        {
                            ShowError("操作失败！游戏ID只能为整数");
                            return;
                        }
                        var model = FacadeManage.aideAccountsFacade.GetAccountInfoByGameId(Convert.ToInt32(search));
                        userId = model?.UserID ?? 0;
                        break;
                    case "4":
                        if (!Utils.Validate.IsPositiveInt(search))
                        {
                            ShowError("操作失败！用户ID只能为数字");
                            return;
                        }
                        userId = Convert.ToInt32(search);
                        break;
                    default:
                        userId = 0;
                        break;
                }
                if (userId == 0)
                {
                    litNoData.Visible = true;
                    rptData.Visible = false;
                    return;
                }
                where = " WHERE UserID=" + userId;
            }

            int serverId = Convert.ToInt32(ddlServerID.SelectedValue);
            if (serverId != 0)
            {
                where = string.IsNullOrEmpty(where)
                    ? $" WHERE ServerID={serverId}"
                    : where +
                      $" AND ServerID={serverId}";
            }

            PagerSet pagerSet = FacadeManage.aideTreasureFacade.GetList(GameScoreLocker.Tablename,
                anpPage.CurrentPageIndex, anpPage.PageSize, where, " ORDER BY CollectDate ASC");
            anpPage.RecordCount = pagerSet.RecordCount;

            if (pagerSet.PageSet.Tables[0].Rows.Count > 0)
            {
                rptData.DataSource = pagerSet.PageSet;
                rptData.DataBind();
                litNoData.Visible = false;
                rptData.Visible = true;
            }
            else
            {
                litNoData.Visible = true;
                rptData.Visible = false;
            }
        }

        //绑定房间
        private void BindServerList()
        {
            PagerSet pagerSet = FacadeManage.aidePlatformFacade.GetList("GameRoomInfo", 1, int.MaxValue,
                "WHERE ServerType=1", "ORDER BY GameID ASC");

            if (pagerSet.PageSet.Tables[0].Rows.Count > 0)
            {
                ddlServerID.DataSource = pagerSet.PageSet;
                ddlServerID.DataTextField = "ServerName";
                ddlServerID.DataValueField = "ServerID";
                ddlServerID.DataBind();
            }

            ddlServerID.Items.Insert(0, new ListItem("全部金币房间", "0"));
        }

        #endregion

        #region 公开方法

        /// <summary>
        /// 获取用户信息转为Table td
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public string GetAccountsInfo(int userId)
        {
            AccountsInfo account = FacadeManage.aideAccountsFacade.GetAccountInfoByUserId(userId);
            return
                $"<td>{account?.Accounts ?? ""}</td><td>{account?.NickName ?? ""}</td><td>{account?.GameID ?? 0}</td>";
        }

        /// <summary>
        /// 通过游戏房间获取Table td
        /// </summary>
        /// <param name="serverId"></param>
        /// <returns></returns>
        public string GetGameRoomInfo(int serverId)
        {
            GameRoomInfo roomInfo = FacadeManage.aidePlatformFacade.GetGameRoomInfoInfo(serverId);
            if (roomInfo == null) return "<td></td><td></td>";
            GameGameItem kindInfo = FacadeManage.aidePlatformFacade.GetGameGameItemInfo(roomInfo.KindID);
            return $"<td>{kindInfo.GameName}</td><td>{roomInfo.ServerName}</td>";
        }

        #endregion
    }
}
