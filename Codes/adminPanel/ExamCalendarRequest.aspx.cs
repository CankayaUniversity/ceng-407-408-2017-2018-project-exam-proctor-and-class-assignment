﻿using library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class adminPanel_ExamCalendarRequest : BaseClass
{
    public static string logFileName = "page_AdminExamCalendarRequest";

    protected void Page_Load(object sender, EventArgs e)
    {
        sIpAddress = HttpContext.Current.Request.UserHostAddress;
        LogMessage("### Information ### => ipAddress = " + sIpAddress, "Ip List", logFileName);
    }
}