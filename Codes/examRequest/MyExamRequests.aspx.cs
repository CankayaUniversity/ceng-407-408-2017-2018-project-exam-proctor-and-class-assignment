using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Web.Script.Services;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using library;
public partial class examRequest_MyExamRequests :  BaseClass
{
    public static string logFileName = "page_MyExamRequests";

    protected void Page_Load(object sender, EventArgs e)
    {
        sIpAddress = HttpContext.Current.Request.UserHostAddress;
        LogMessage("### Information ### => ipAddress = " + sIpAddress, "Ip List", logFileName);
    }

    public class FacultyNames
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    [System.Web.Services.WebMethod]
    public static string getMyExamRequests()
    {

        string connectionstring = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection connection = new SqlConnection(connectionstring);
        SqlCommand command = new SqlCommand();
        SqlDataReader reader = null;

        List<FacultyNames> list = new List<FacultyNames>();
        string Sql = "EXEC [ExamProctorProject].[dbo].[spGetMyExamRequests] " + sqlValStatic(HttpContext.Current.Session["sTCNumber"].ToString(), "'");
        LogMessageStatic("### SP Get Faculty Names Control ### => Sql  :   " + Sql, HttpContext.Current.Session["sTCNumber"].ToString(), logFileName);
        try
        {
            connection.Open();
            command = new SqlCommand("[ExamProctorProject].[dbo].[spGetMyExamRequests]", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                FacultyNames facultyNames = new FacultyNames();
                facultyNames.id = Convert.ToInt32(reader["id"].ToString());
                facultyNames.name = reader["name"].ToString();
                list.Add(facultyNames);

                LogMessageStatic("### Information ### => Faculty ID = " + facultyNames.id.ToString(), HttpContext.Current.Session["sTCNumber"].ToString(), logFileName);
                LogMessageStatic("### Information ### => Faculty Name = " + facultyNames.name.ToString(), HttpContext.Current.Session["sTCNumber"].ToString(), logFileName);
            }
        }

        catch (Exception ex)
        {
            LogMessageStatic("!!! Error !!! => [ExamProctorProject].[dbo].[spGetMyExamRequests] , Error Message = " + ex.Message.ToString(), HttpContext.Current.Session["sTCNumber"].ToString(), logFileName);
        }

        finally
        {
            command.Dispose();
            reader = null;
            connection.Close();
        }
        return JsonConvert.SerializeObject(list);
    }
}