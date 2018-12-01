using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebAppAegisCRM.Settings
{
    public partial class UserSettings : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadUserSettings();
            }
        }

        protected void CheckListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = Convert.ToInt32(Request.Form["__EVENTTARGET"].Substring(Request.Form["__EVENTTARGET"].LastIndexOf('$') + 1));
            CheckBoxList cbl = (CheckBoxList)sender;
            int settingValue = int.Parse(cbl.Items[index].Value);
            string settingName = cbl.Items[index].Text.Trim().Replace(" ","_");
            int userId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
            SaveSettings(userId, settingName, settingValue, cbl.Items[index].Selected);
        }

        private void SaveSettings(int userId, string settingName, int settingValue, bool IsChecked)
        {
            Business.Settings.UserSettings objUserSettings = new Business.Settings.UserSettings();
            Entity.Settings.UserSettings userSettings = new Entity.Settings.UserSettings()
            {
                IsActive = IsChecked,
                SettingName=settingName,
                SettingValue = settingValue,
                UserId = userId
            };
            objUserSettings.Save(userSettings);
        }

        private void LoadUserSettings()
        {
            UncheckAll();
            int userId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
            Business.Settings.UserSettings objUserSettings = new Business.Settings.UserSettings();
            DataTable DT = objUserSettings.GetByUserId(userId).Tables[1];

            foreach (DataRow dr in DT.Rows)
            {
                if (ChkDashboardSettings.Items.FindByValue(dr["SettingsValue"].ToString()) != null)
                    ChkDashboardSettings.Items.FindByValue(dr["SettingsValue"].ToString()).Selected = true;
            }
        }

        private void UncheckAll()
        {
            foreach (ListItem lstItem in ChkDashboardSettings.Items)
                lstItem.Selected = false;
        }
    }
}