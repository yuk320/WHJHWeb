using System;
using Game.Web.UI;
using Game.Entity.NativeWeb;
using Game.Facade;
using Game.Utils;
using Game.Entity.Enum;

namespace Game.Web.Module.WebManager
{
    public partial class QuestionInfo : AdminPage
    {
        /// <summary>
        /// 页面加载
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                BindData();
            }
        }
        /// <summary>
        /// 数据保存
        /// </summary>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            Question quest = new Question()
            {
                QuestionTitle = txtQuestion.Text.Trim(),
                Answer = txtAnswer.Text.Trim(),
                SortID = Convert.ToInt32(txtSortID.Text)
            };
           
            if(IntParam > 0)
            {
                AuthUserOperationPermission(Permission.Edit);
                quest.ID = IntParam;
            }
            else
            {
                AuthUserOperationPermission(Permission.Add);
            }
            quest.QuestionTitle = CtrlHelper.GetText(txtQuestion);
            quest.Answer = CtrlHelper.GetText(txtAnswer);
            quest.SortID = CtrlHelper.GetInt(txtSortID,0);

            int result = FacadeManage.aideNativeWebFacade.SaveQuestionInfo(quest);
            if(result > 0)
            {
                ShowInfo("常见问题操作成功", "QuestionList.aspx", 1000);
            }
            else
            {
                ShowError("常见问题操作失败");
            }
        }
        /// <summary>
        /// 数据绑定
        /// </summary>
        protected void BindData()
        {
            if(IntParam > 0)
            {
                Question quest = FacadeManage.aideNativeWebFacade.GetQuestionInfo(IntParam);
                if(quest != null)
                {
                    CtrlHelper.SetText(txtAnswer,quest.Answer);
                    CtrlHelper.SetText(txtQuestion,quest.QuestionTitle);
                    CtrlHelper.SetText(txtSortID,quest.SortID.ToString());
                }
            }
        }
    }
}