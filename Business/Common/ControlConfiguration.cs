using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace Business.Common
{
    public static class ControlConfiguration
    {
        public static void InsertSelect(this DropDownList dropDownList)
        {
            dropDownList.Items.Insert(0, new ListItem() { Text = "--Select--", Value = "0", Selected = true, Enabled = true });
        }
    }
}
