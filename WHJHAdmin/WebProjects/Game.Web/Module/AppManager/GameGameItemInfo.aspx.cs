using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;

using Game.Web.UI;
using Game.Utils;
using Game.Entity.Platform;
using Game.Kernel;
using Game.Entity.Accounts;
using Game.Entity.Enum;
using Game.Facade;

namespace Game.Web.Module.AppManager
{
    public partial class GameGameItemInfo : AdminPage
    {
        /// <summary>
        /// 页面加载
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                BindDataBaseAddr();
                GameGameItemDataBind();
            }
        }
        /// <summary>
        /// 数据保存
        /// </summary>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            GameGameItem gameGameItem = null;
            string gameID = CtrlHelper.GetText(txtGameID);
            if(!Utils.Validate.IsPositiveInt(gameID))
            {
                ShowError("游戏标识不合法，游戏标识只能为正整数");
                return;
            }
            int gid = Convert.ToInt32(gameID);
            if(IntParam > 0)
            {
                AuthUserOperationPermission(Permission.Edit);
                gameGameItem = FacadeManage.aidePlatformFacade.GetGameGameItemInfo(gid);
            }
            else
            {
                AuthUserOperationPermission(Permission.Add);
                GameGameItem item = FacadeManage.aidePlatformFacade.GetGameGameItemInfo(gid);
                if(item != null)
                {
                    ShowError("模块标识已经存在");
                    return;
                }
                gameGameItem = new GameGameItem();
                gameGameItem.SuportType = 31;
            }

            gameGameItem.GameID = Convert.ToInt32(gameID);
            gameGameItem.GameName = CtrlHelper.GetText(txtGameName);
            gameGameItem.DataBaseAddr = ddlDataBaseAddr.SelectedValue.Trim();
            gameGameItem.DataBaseName = CtrlHelper.GetText(txtDataBaseName);
            gameGameItem.ServerVersion = CalVersion2(CtrlHelper.GetText(txtServerVersion));
            gameGameItem.ClientVersion = CalVersion2(CtrlHelper.GetText(txtClientVersion));
            gameGameItem.ServerDLLName = CtrlHelper.GetText(txtServerDLLName);
            gameGameItem.ClientExeName = CtrlHelper.GetText(txtClientExeName);

            int result = IntParam > 0 ? FacadeManage.aidePlatformFacade.UpdateGameGameItem(gameGameItem) : FacadeManage.aidePlatformFacade.InsertGameGameItem(gameGameItem);
            if(result > 0)
            {
                ShowInfo("模块信息操作成功", "GameGameItemList.aspx", 1200);
            }
            else
            {
                ShowError("模块信息操作失败");
            }
        }

        /// <summary>
        /// 绑定数据库地址
        /// </summary>
        private void BindDataBaseAddr()
        {
            PagerSet pagerSet = FacadeManage.aidePlatformFacade.GetList(DataBaseInfo.Tablename, 1, 100, "", "ORDER BY DBInfoID ASC");
            ddlDataBaseAddr.DataSource = pagerSet.PageSet;
            ddlDataBaseAddr.DataTextField = "Information";
            ddlDataBaseAddr.DataValueField = "DBAddr";
            ddlDataBaseAddr.DataBind();
        }
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void GameGameItemDataBind()
        {
            if(IntParam > 0)
            {
                GameGameItem gameGameItem = FacadeManage.aidePlatformFacade.GetGameGameItemInfo(IntParam);
                if(gameGameItem != null)
                {
                    CtrlHelper.SetText(txtGameID, gameGameItem.GameID.ToString().Trim());
                    CtrlHelper.SetText(txtGameName, gameGameItem.GameName.Trim());
                    CtrlHelper.SetText(txtDataBaseName, gameGameItem.DataBaseName.Trim());
                    ddlDataBaseAddr.SelectedValue = gameGameItem.DataBaseAddr.Trim();
                    CtrlHelper.SetText(txtServerVersion, CalVersion(gameGameItem.ServerVersion));
                    CtrlHelper.SetText(txtClientVersion, CalVersion(gameGameItem.ClientVersion));
                    CtrlHelper.SetText(txtServerDLLName, gameGameItem.ServerDLLName.Trim());
                    CtrlHelper.SetText(txtClientExeName, gameGameItem.ClientExeName.Trim());
                }
            }
        }     
    }
}
