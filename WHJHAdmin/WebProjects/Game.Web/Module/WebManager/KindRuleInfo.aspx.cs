using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Game.Web.UI;
using Game.Entity.NativeWeb;
using Game.Facade;
using Game.Utils;
using Game.Entity.Enum;
using Game.Entity.Platform;
using Game.Kernel;

namespace Game.Web.Module.WebManager
{
    public partial class KindRuleInfo : AdminPage
    {
        /// <summary>
        /// 页面加载
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                BindGame();
                BindData();
            }
        }
        /// <summary>
        /// 数据保存
        /// </summary>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            GameRule rule = null;
            int kindid = Convert.ToInt32(ddlGame.SelectedValue);
            if(IntParam > 0)
            {
                AuthUserOperationPermission(Permission.Edit);
                rule = FacadeManage.aideNativeWebFacade.GetGameRuleInfo(IntParam);
            }
            else
            {
                AuthUserOperationPermission(Permission.Add);
                rule = FacadeManage.aideNativeWebFacade.GetGameRuleInfo(kindid);
                if(rule != null)
                {
                    ShowError("游戏规则已存在");
                    return;
                }
                rule = new GameRule();
                rule.CollectDate = DateTime.Now;
            }
            string filepath = upImage.FilePath;
            if(string.IsNullOrEmpty(filepath))
            {
                ShowError("游戏图标未上传");
                return;
            }

            rule.KindIcon = filepath.Substring(7, filepath.Length - 7);
            rule.KindID = kindid;
            rule.KindName = ddlGame.SelectedItem.Text;
            rule.KindIntro = CtrlHelper.GetText(txtIntro);
            rule.KindRule = txtRule.Text;
            rule.SortID = CtrlHelper.GetInt(txtSortID, 0);

            int result = IntParam > 0 ? FacadeManage.aideNativeWebFacade.UpdateGameRule(rule) : FacadeManage.aideNativeWebFacade.InsertGameRule(rule);
            if(result > 0)
            {
                ShowInfo("游戏规则操作成功", "KindRuleList.aspx", 1000);
            }
            else
            {
                ShowError("游戏规则操作失败");
            }
        }
        /// <summary>
        /// 绑定游戏列表
        /// </summary>
        protected void BindGame()
        {
            PagerSet ps = FacadeManage.aidePlatformFacade.GetList(GameGameItem.Tablename, 1, 1000, "", "ORDER BY GameID DESC");
            ddlGame.DataSource = ps.PageSet;
            ddlGame.DataTextField = "GameName";
            ddlGame.DataValueField = "GameID";
            ddlGame.DataBind();
        }
        /// <summary>
        /// 数据绑定
        /// </summary>
        protected void BindData()
        {
            if(IntParam > 0)
            {
                GameRule rule = FacadeManage.aideNativeWebFacade.GetGameRuleInfo(IntParam);
                if(rule != null)
                {
                    ddlGame.SelectedValue = rule.KindID.ToString();
                    CtrlHelper.SetText(txtIntro, rule.KindIntro);
                    txtRule.Text = rule.KindRule;
                    if(!string.IsNullOrEmpty(rule.KindIcon))
                    {
                        upImage.FilePath = "/Upload" + rule.KindIcon;
                    }
                    ddlGame.Enabled = false;
                    CtrlHelper.SetText(txtSortID, rule.SortID.ToString());
                }
            }
        }
    }
}