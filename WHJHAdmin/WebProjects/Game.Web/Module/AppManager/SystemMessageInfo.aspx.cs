using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

using Game.Web.UI;
using Game.Utils;
using Game.Entity.Platform;
using Game.Kernel;
using Game.Facade;
using Game.Entity.Enum;

namespace Game.Web.Module.AppManager
{
    public partial class SystemMessageInfo : AdminPage
    {
        /// <summary>
        /// 页面加载
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                SystemMessageDataBind();
                KindsDataBind();
            }
        }
        /// <summary>
        /// 数据保存
        /// </summary>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            SystemMessage systemMessage = new SystemMessage();
            if(IntParam > 0)
            {
                AuthUserOperationPermission(Permission.Edit);
                systemMessage = FacadeManage.aidePlatformFacade.GetSystemMessageInfo(IntParam);
                systemMessage.UpdateMasterID = userExt.UserID;
                systemMessage.UpdateDate = DateTime.Now;
            }
            else
            {
                AuthUserOperationPermission(Permission.Add);
                systemMessage.CreateMasterID = userExt.UserID;
                systemMessage.CreateDate = DateTime.Now;
            }

            systemMessage.MessageString = CtrlHelper.GetText(txtMessageString);
            systemMessage.MessageType = Convert.ToInt32(ddlMessageType.SelectedValue.Trim());
            systemMessage.StartTime = Convert.ToDateTime(CtrlHelper.GetText(txtStartTime));
            systemMessage.ConcludeTime = Convert.ToDateTime(CtrlHelper.GetText(txtConcludeTime));
            systemMessage.TimeRate = CtrlHelper.GetInt(txtTimeRate, 0);
            systemMessage.CollectNote = CtrlHelper.GetText(txtCollectNote);

            StringBuilder sb = new StringBuilder();
            if(!tvKinds.Nodes[0].Checked)
            {
                foreach(TreeNode node in tvKinds.CheckedNodes)
                {
                    if(node.Value != "")
                    {
                        sb.Append(node.Value.ToString() + ",");
                    }
                }
            }
            else
            {
                sb.Length = 0;
                sb.Append("0");
            }

            if(sb.ToString() == "" || sb.ToString() == null)
            {
                ShowError("请选择要控制的房间！");
                return;
            }
            systemMessage.ServerRange = sb.ToString();
            int result = IntParam > 0 ? FacadeManage.aidePlatformFacade.UpdateSystemMessage(systemMessage) : FacadeManage.aidePlatformFacade.InsertSystemMessage(systemMessage);
            if(result > 0)
            {
                ShowInfo("系统消息操作成功", "SystemMessageList.aspx", 1200);
            }
            else
            {
                ShowError("系统消息操作失败");
            }
        }
        /// <summary>
        /// 游戏信息列表
        /// </summary>
        private void KindsDataBind()
        {
            tvKinds.Nodes.Clear();

            TreeNode rootNode = new TreeNode();
            rootNode.Text = "游戏列表";
            rootNode.Value = "-1";
            rootNode.SelectAction = TreeNodeSelectAction.None;
            rootNode.Expand();
            rootNode.ShowCheckBox = true;
            tvKinds.Nodes.Add(rootNode);

            if(Servers != "0")
            {
                string[] servers = Servers.Split(',');

                DataSet ds = FacadeManage.aidePlatformFacade.GetList(GameGameItem.Tablename, 1, Int32.MaxValue, "WHERE 1=1", "ORDER BY GameID ASC").PageSet;
                if(ds.Tables[0].Rows.Count > 0)
                {
                    foreach(DataRow dr in ds.Tables[0].Rows)
                    {
                        TreeNode nodeGame = new TreeNode();
                        nodeGame.Text = dr["GameName"].ToString();
                        nodeGame.Value = "";
                        nodeGame.ShowCheckBox = true;
                        nodeGame.SelectAction = TreeNodeSelectAction.None;
                        rootNode.ChildNodes.Add(nodeGame);

                        int GameID = Convert.ToInt32(dr["GameID"]);
                        DataSet dsServer = FacadeManage.aidePlatformFacade.GetList(GameRoomInfo.Tablename, 1, Int32.MaxValue, string.Format("WHERE GameID={0} AND Nullity=0", GameID), "ORDER BY ServerID ASC").PageSet;
                        if(dsServer.Tables[0].Rows.Count == 0)
                            continue;

                        int j = 0;
                        foreach(DataRow drServer in dsServer.Tables[0].Rows)
                        {
                            TreeNode nodeServer = new TreeNode();
                            nodeServer.Text = drServer["ServerName"].ToString();
                            nodeServer.Value = drServer["ServerID"].ToString().Trim();
                            nodeServer.ShowCheckBox = true;
                            nodeServer.SelectAction = TreeNodeSelectAction.None;

                            for(int i = 0; i < servers.Length; i++)
                            {
                                if(servers[i].Trim() == "") continue;
                                if(servers[i].Trim() == drServer["ServerID"].ToString().Trim())
                                {
                                    nodeServer.Checked = true;
                                    j++;
                                    break;
                                }
                            }
                            nodeGame.ChildNodes.Add(nodeServer);
                        }
                        if(j == dsServer.Tables[0].Rows.Count && j != 0) nodeGame.Checked = true;
                    }
                }
            }
            else
            {
                BindKindAndServer(rootNode);
                tvKinds.Nodes[0].Checked = true;
            }
        }

        /// <summary>
        /// 绑定游戏和房间数据
        /// </summary>
        private void BindKindAndServer(TreeNode rootNode)
        {
            DataSet ds = FacadeManage.aidePlatformFacade.GetList(GameGameItem.Tablename, 1, Int32.MaxValue, "WHERE 1=1", "ORDER BY GameID ASC").PageSet;
            if(ds.Tables[0].Rows.Count > 0)
            {
                foreach(DataRow dr in ds.Tables[0].Rows)
                {
                    TreeNode nodeGame = new TreeNode();
                    nodeGame.Text = dr["GameName"].ToString();
                    nodeGame.Value = "";
                    nodeGame.ShowCheckBox = true;
                    nodeGame.SelectAction = TreeNodeSelectAction.None;
                    nodeGame.Checked = true;
                    rootNode.ChildNodes.Add(nodeGame);

                    int kindID = Convert.ToInt32(dr["GameID"]);
                    DataSet dsServer = FacadeManage.aidePlatformFacade.GetList(GameRoomInfo.Tablename, 1, Int32.MaxValue, string.Format("WHERE KindID={0} AND Nullity=0", kindID), "ORDER BY ServerID ASC").PageSet;
                    if(dsServer.Tables[0].Rows.Count == 0)
                        continue;

                    foreach(DataRow drServer in dsServer.Tables[0].Rows)
                    {
                        TreeNode nodeServer = new TreeNode();
                        nodeServer.Text = drServer["ServerName"].ToString();
                        nodeServer.Value = drServer["ServerID"].ToString().Trim();
                        nodeServer.ShowCheckBox = true;
                        nodeServer.SelectAction = TreeNodeSelectAction.None;
                        nodeServer.Checked = true;

                        nodeGame.ChildNodes.Add(nodeServer);
                    }
                }
            }

        }
        /// <summary>
        /// 数据绑定
        /// </summary>
        private void SystemMessageDataBind()
        {
            if(IntParam > 0)
            {
                SystemMessage systemMessage = FacadeManage.aidePlatformFacade.GetSystemMessageInfo(IntParam);
                if(systemMessage != null)
                {
                    CtrlHelper.SetText(txtMessageString, systemMessage.MessageString.Trim());
                    ddlMessageType.SelectedValue = systemMessage.MessageType.ToString().Trim();
                    CtrlHelper.SetText(txtStartTime, systemMessage.StartTime.ToString().Trim());
                    CtrlHelper.SetText(txtConcludeTime, systemMessage.ConcludeTime.ToString().Trim());
                    CtrlHelper.SetText(txtTimeRate, systemMessage.TimeRate.ToString().Trim());
                    CtrlHelper.SetText(txtCollectNote, systemMessage.CollectNote.Trim());
                    Servers = systemMessage.ServerRange;
                }
            }
        }
        /// <summary>
        /// 房间范围记录
        /// </summary>
        public string Servers
        {
            get
            {
                if(ViewState["Server"] == null)
                    ViewState["Server"] = "";
                return ViewState["Server"].ToString();
            }
            set
            {
                ViewState["Server"] = value;
            }
        }
    }
}
