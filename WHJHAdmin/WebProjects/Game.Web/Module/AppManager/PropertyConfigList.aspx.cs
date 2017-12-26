using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Game.Utils;
using Game.Kernel;
using Game.Web.UI;
using Game.Facade;
using Game.Entity.Enum;
using System.Data;
using System.Text;
using Game.Entity.Platform;
using Game.Entity.Treasure;
using Game.Entity.NativeWeb;

namespace Game.Web.Module.AppManager
{
    public partial class PropertyConfigList : AdminPage
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
        /// 数据分页
        /// </summary>
        protected void anpNews_PageChanged(object sender, EventArgs e)
        {
            BindData();
        }
        //批量禁用
        protected void btnDisable_Click(object sender, EventArgs e)
        {
            //判断权限
            AuthUserOperationPermission(Permission.Edit);
            string strQuery = "SET Nullity=1 WHERE ID in (" + StrCIdList + ")";
            try
            {
                FacadeManage.aidePlatformFacade.SetProperty(strQuery);
                MessageBox("禁用成功");
            }
            catch
            {
                MessageBox("禁用失败");
            }
            BindData();
        }
        //批量启用
        protected void btnEnable_Click(object sender, EventArgs e)
        {
            //判断权限
            AuthUserOperationPermission(Permission.Edit);
            string strQuery = "SET Nullity=0 WHERE ID in (" + StrCIdList + ")";
            try
            {
                FacadeManage.aidePlatformFacade.SetProperty(strQuery);
                MessageBox("启用成功");
            }
            catch
            {
                MessageBox("启用失败");
            }
            BindData();
        }
        /// <summary>
        /// 数据绑定
        /// </summary>
        private void BindData()
        {
            PagerSet pagerSet = FacadeManage.aidePlatformFacade.GetList(GameProperty.Tablename, anpNews.CurrentPageIndex, anpNews.PageSize, "", " ORDER BY Kind ASC,SortID ASC");
            anpNews.RecordCount = pagerSet.RecordCount;
            litNoData.Visible = pagerSet.PageSet.Tables[0].Rows.Count <= 0;
            rptDataList.DataSource = pagerSet.PageSet;
            rptDataList.DataBind();
        }

        //道具发行范围
        protected string GetIssueArea(int intIssueArea)
        {
            StringBuilder sb = new StringBuilder();
            IList<EnumDescription> arrIssueArea = IssueAreaHelper.GetIssueAreaList(typeof(IssueArea));

            foreach (EnumDescription v in arrIssueArea)
            {
                if (v.EnumValue == (intIssueArea & v.EnumValue))
                    sb.AppendFormat("{0},", IssueAreaHelper.GetIssueAreaDes((IssueArea)v.EnumValue));
            }

            return sb.ToString().TrimEnd(',');
        }
        //道具使用范围
        protected string GetServiceArea(int intServiceArea)
        {
            StringBuilder sb = new StringBuilder();
            IList<EnumDescription> arrServiceArea = ServiceAreaHelper.GetServiceAreaList(typeof(ServiceArea));

            foreach (EnumDescription v in arrServiceArea)
            {
                if (v.EnumValue == (intServiceArea & v.EnumValue))
                    sb.AppendFormat("{0},", ServiceAreaHelper.GetServiceAreaDes((ServiceArea)v.EnumValue));
            }

            return sb.ToString().TrimEnd(',');
        }

        /// <summary>
        /// 推荐名称
        /// </summary>
        /// <param name="recommend"></param>
        /// <returns></returns>
        protected string GetRecommendName(byte recommend)
        {
            string rValue;
            switch (recommend)
            {
                case 0:
                    rValue = "<span>否</span>";
                    break;
                case 1:
                    rValue = "<span class='hong'>是</span>";
                    break;
                default:
                    rValue = "<span>否</span>";
                    break;
            }
            return rValue;
        }
    }
}