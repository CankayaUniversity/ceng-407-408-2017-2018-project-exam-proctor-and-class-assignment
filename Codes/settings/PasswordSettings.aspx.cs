using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using library;
using System.Data.SqlClient;
using System.Configuration;

public partial class settings_PasswordSettings : BaseClass
{
    public static string logFileName = "page_PasswordSettings";
    protected void Page_Load(object sender, EventArgs e)
    {
        sIpAddress = HttpContext.Current.Request.UserHostAddress;
        LogMessage("### Information ### => ipAddress = " + sIpAddress, "Ip List", logFileName);
    }
    [System.Web.Services.WebMethod]
    public static string btnSaveClick(string oldPassword, string newPassword, string configPassword)
    {

        string connectionstring = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection connection = new SqlConnection(connectionstring);
        SqlCommand command = new SqlCommand();
        SqlDataReader reader = null;

        if (newPassword != configPassword)
        {
            return "Passwords could not match.";
        }
        string sTCNumber = HttpContext.Current.Session["sTCNumber"].ToString();

        string response = string.Empty;
        try
        {
            connection.Open();
            command = new SqlCommand("[ExamProctorProject].[dbo].[spUpdatePassword]", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@TCNumber", sTCNumber);
            command.Parameters.AddWithValue("@oldPassword", oldPassword);
            command.Parameters.AddWithValue("@newPassword", newPassword);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                response = reader[0].ToString();
            }
        }

        catch (Exception ex)
        {
            LogMessageStatic("!!! Error !!! => [ExamProctorProject].[dbo].[spGetPhoneNum] , Error Message = " + ex.Message.ToString(), sTCNumber, logFileName);
        }

        finally
        {
            command.Dispose();
            reader = null;
            connection.Close();
        }
        LogMessageStatic("### Information ### => TC Number  = " + sTCNumber, sTCNumber, logFileName);
        LogMessageStatic("### Information ### => Old Password  = " + oldPassword, sTCNumber, logFileName);
        LogMessageStatic("### Information ### => New Password  = " + newPassword, sTCNumber, logFileName);
        return response;

    }
}