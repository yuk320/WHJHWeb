using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Game.Web.UI;
using Game.Utils;
using Game.Entity.Platform;
using Game.Kernel;
using Game.Entity.Accounts;
using Game.Entity.Enum;
using Game.Facade;

namespace Game.Web.Module.AppManager
{
    public partial class DataBaseInfoInfo : AdminPage
    {
        /// <summary>
        /// 页面加载
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                if(IntParam > 0)
                {
                    DataBaseInfo dataBaseInfo = FacadeManage.aidePlatformFacade.GetDataBaseInfo(IntParam);
                    if(dataBaseInfo != null)
                    {
                        CtrlHelper.SetText(txtInformation, dataBaseInfo.Information.Trim());
                        CtrlHelper.SetText(txtDBAddr, dataBaseInfo.DBAddr.Trim());
                        CtrlHelper.SetText(txtDBPort, dataBaseInfo.DBPort.ToString().Trim());
                        CtrlHelper.SetText(txtDBUser, CWHEncryptNet.XorCrevasse(dataBaseInfo.DBUser.Trim()));
                        if(dataBaseInfo.DBPassword != "")
                        {
                            txtDBPassword.Attributes.Add("value", "********");
                            CtrlHelper.SetText(hdfDBPassword, dataBaseInfo.DBPassword.Trim());
                        }
                        CtrlHelper.SetText(txtMachineID, dataBaseInfo.MachineID.Trim());
                    }
                }
            }
        }
        /// <summary>
        /// 保存数据
        /// </summary>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if(!Utils.Validate.IsPositiveInt(txtDBPort.Text))
            {
                ShowError("端口不规范，端口只能为正整数");
                return;
            }
            string address = CtrlHelper.GetText(txtDBAddr);
            DataBaseInfo dataBaseInfo = new DataBaseInfo();
            if(IntParam > 0)
            {
                AuthUserOperationPermission(Permission.Edit);
                dataBaseInfo = FacadeManage.aidePlatformFacade.GetDataBaseInfo(IntParam);
            }
            else
            {
                AuthUserOperationPermission(Permission.Add);
                DataBaseInfo info = FacadeManage.aidePlatformFacade.GetDataBaseInfo(address);
                if(info != null)
                {
                    ShowError("地址已经存在");
                    return;
                }
            }
            dataBaseInfo.DBAddr = address;
            dataBaseInfo.DBPort = CtrlHelper.GetInt(txtDBPort, 0);
            dataBaseInfo.DBUser = CWHEncryptNet.XorEncrypt(CtrlHelper.GetText(txtDBUser));
            if(CtrlHelper.GetText(hdfDBPassword) == "********")
            {
                dataBaseInfo.DBPassword = CWHEncryptNet.XorEncrypt(CtrlHelper.GetText(txtDBPassword));
            }
            else
            {
                dataBaseInfo.DBPassword = CtrlHelper.GetText(hdfDBPassword);
            }
            dataBaseInfo.MachineID = CtrlHelper.GetText(txtMachineID);
            dataBaseInfo.Information = CtrlHelper.GetText(txtInformation);
            int result = IntParam > 0 ? FacadeManage.aidePlatformFacade.UpdateDataBase(dataBaseInfo) : FacadeManage.aidePlatformFacade.InsertDataBase(dataBaseInfo);
            if(result>0)
            {
                ShowInfo("机器信息操作成功", "DataBaseInfoList.aspx", 1200);
            }
            else
            {
                ShowError("机器信息失败成功");
            }

        }    
    }
}
