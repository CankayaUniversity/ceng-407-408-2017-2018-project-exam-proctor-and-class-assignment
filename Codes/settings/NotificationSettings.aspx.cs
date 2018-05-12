using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using Newtonsoft.Json;
using library;

public partial class settings_NotificationSettings : BaseClass
{
    public static string logFileName = "page_NotificationSettings";

    protected void Page_Load(object sender, EventArgs e)
    {
        sIpAddress = HttpContext.Current.Request.UserHostAddress;
        LogMessage("### Information ### => ipAddress = " + sIpAddress, "Ip List", logFileName);
    }

    public class NotificationSettings
    {
        public string userPhoneNumber { get; set; }
        public string userPhoneNumber2 { get; set; }
        public string userEmail { get; set; }
        public string userEmail2 { get; set; }
        public string platformValue { get; set; }
        public string userApprovalTime { get; set; }
        public string userApprovalDay { get; set; }
        public int userApprovalPlatform { get; set; }
        public int isInformedCheck { get; set; }
        public int defaultDayTime { get; set; }
    } 

    [System.Web.Services.WebMethod]
    public static string getNotificationSettings()
    {

        string connectionstring = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection connection = new SqlConnection(connectionstring);
        SqlCommand command = new SqlCommand();
        SqlDataReader reader = null;
        string TCNumber = HttpContext.Current.Session["sTCNumber"].ToString();

        //string TCNumber = "55216486568";
        string userPhoneNumber=string.Empty;
        string userPhoneNumber2=string.Empty;
        string userEmail=string.Empty;
        string userEmail2=string.Empty;
        string userSelectedApprovalPlatformValue=string.Empty;
        string userApprovalTime=string.Empty;
        string userApprovalDay=string.Empty;
        int userApprovalPlatform=0;
        bool isInformedCheck = false;
        int counter = 0;

        string Sql = "EXEC [ExamProctorProject].[dbo].[spGetNotificationSettings] " + sqlValStatic(HttpContext.Current.Session["sTCNumber"].ToString(), "'");
        LogMessageStatic("### SP Get Notification Settings Control ### => Sql  :   " + Sql, HttpContext.Current.Session["sTCNumber"].ToString(), logFileName);
        try
        {
            connection.Open();
            command = new SqlCommand("[ExamProctorProject].[dbo].[spGetNotificationSettings]", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@TCNumber", TCNumber);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                userPhoneNumber = reader["userPhoneNumber"].ToString();
                userPhoneNumber2 = reader["userPhoneNumber2"].ToString();
                userEmail = reader["userEmail"].ToString();
                userEmail2 = reader["userEmail2"].ToString();
                userSelectedApprovalPlatformValue = reader["userSelectedApprovalPlatformValue"].ToString();
                userApprovalTime = reader["userApprovalTime"].ToString();
                userApprovalDay = reader["userApprovalDay"].ToString();
                userApprovalPlatform = Convert.ToInt32(reader["userApprovalPlatform"].ToString());
                isInformedCheck = Convert.ToBoolean(reader["isInformedCheck"].ToString());
                counter++;
            }         
        }

        catch (Exception ex)
        {
            counter = 0;
            LogMessageStatic("!!! Error !!! => [ExamProctorProject].[dbo].[spGetNotificationSettings] , Error Message = " + ex.Message.ToString(), HttpContext.Current.Session["sTCNumber"].ToString(), logFileName);
        }

        finally
        {
            command.Dispose();
            reader = null;
            connection.Close();
        }

        LogMessageStatic("### Information ### => TC Number = " + TCNumber, HttpContext.Current.Session["sTCNumber"].ToString(), logFileName);

        List<NotificationSettings> list = new List<NotificationSettings>();
        if (counter>0)
        {
            NotificationSettings notificationSettings = new NotificationSettings();
            notificationSettings.userPhoneNumber =userPhoneNumber;
            notificationSettings.userPhoneNumber2 =userPhoneNumber2;
            notificationSettings.userEmail =userEmail;
            notificationSettings.userEmail2 =userEmail2;
            notificationSettings.platformValue =userSelectedApprovalPlatformValue;
            notificationSettings.userApprovalTime =userApprovalTime;
            notificationSettings.userApprovalDay=userApprovalDay;
            notificationSettings.userApprovalPlatform=userApprovalPlatform;
            if (isInformedCheck)
	        {
                notificationSettings.isInformedCheck = 1;
            }
            else
            {
                notificationSettings.isInformedCheck = 0;
            }

            if (string.IsNullOrEmpty(userApprovalDay.Replace(" ", "")) && string.IsNullOrEmpty(userApprovalDay.Replace(" ", "")))
            {
                notificationSettings.defaultDayTime = 1;
            }
         
            list.Add(notificationSettings);
        }
       

        return JsonConvert.SerializeObject(list);

    }

    [System.Web.Services.WebMethod]
    public static string btnSaveClick(int userApprovalPlatform, string platformValue, string userApprovalTime, int userApprovalDay, int isInformedCheck)
          
    {
        string response = string.Empty;
        string connectionstring = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection connection = new SqlConnection(connectionstring);
        SqlCommand command = new SqlCommand();
        SqlDataReader reader = null;
        string TCNumber = HttpContext.Current.Session["sTCNumber"].ToString();

        //string TCNumber = "1";
        bool isInformedCheck_ = false;
        if (isInformedCheck==1)
        {
            isInformedCheck_ = true;
        }
        string Sql = "EXEC [ExamProctorProject].[dbo].[spAddNotificationSettings] " + sqlValStatic(HttpContext.Current.Session["sTCNumber"].ToString(), "'");
        LogMessageStatic("### SP Add Notification Settings Control ### => Sql  :   " + Sql, HttpContext.Current.Session["sTCNumber"].ToString(), logFileName);
        try
        {
            connection.Open();
            command = new SqlCommand("[ExamProctorProject].[dbo].[spAddNotificationSettings]", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@TCNumber", TCNumber);
            command.Parameters.AddWithValue("@userApprovalPlatform", userApprovalPlatform);
            command.Parameters.AddWithValue("@userSelectedApprovalPlatformValue", platformValue);
            command.Parameters.AddWithValue("@userApprovalTime", userApprovalTime);
            command.Parameters.AddWithValue("@userApprovalDay", userApprovalDay);
            command.Parameters.AddWithValue("@isInformedCheck", isInformedCheck_);

            reader = command.ExecuteReader();
            while (reader.Read())
            {
                response = reader[0].ToString();
                LogMessageStatic("### Information ### => Response = " + response, HttpContext.Current.Session["sTCNumber"].ToString(), logFileName);
            }
        }

        catch (Exception ex)
        {
            LogMessageStatic("!!! Error !!! => [ExamProctorProject].[dbo].[spAddNotificationSettings] , Error Message = " + ex.Message.ToString(), HttpContext.Current.Session["sTCNumber"].ToString(), logFileName);
        }

        finally
        {
            command.Dispose();
            reader = null;
            connection.Close();
        }

        LogMessageStatic("### Information ### => TC Number = " + TCNumber, HttpContext.Current.Session["sTCNumber"].ToString(), logFileName);
        LogMessageStatic("### Information ### => User Approval Platform = " + userApprovalPlatform, HttpContext.Current.Session["sTCNumber"].ToString(), logFileName);
        LogMessageStatic("### Information ### => Platform Value = " + platformValue, HttpContext.Current.Session["sTCNumber"].ToString(), logFileName);
        LogMessageStatic("### Information ### => User Approval Time = " + userApprovalTime, HttpContext.Current.Session["sTCNumber"].ToString(), logFileName);
        LogMessageStatic("### Information ### => User Approval Day = " + userApprovalDay, HttpContext.Current.Session["sTCNumber"].ToString(), logFileName);
        LogMessageStatic("### Information ### => Is Informed Check_ = " + isInformedCheck_, HttpContext.Current.Session["sTCNumber"].ToString(), logFileName);

        return response;
    }
}