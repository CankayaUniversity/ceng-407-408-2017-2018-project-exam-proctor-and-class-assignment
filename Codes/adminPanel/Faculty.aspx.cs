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

public partial class adminPanel_Faculty : BaseClass
{
    public static string logFileName = "page_AdminFaculty";

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
    public static string getFacultyName()
    {
       
        string connectionstring = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection connection = new SqlConnection(connectionstring);
        SqlCommand command = new SqlCommand();
        SqlDataReader reader = null;
     
        List<FacultyNames> list= new List<FacultyNames>();
        string Sql = "EXEC [ExamProctorProject].[dbo].[spGetFacultyNames] " + sqlValStatic(HttpContext.Current.Session["sTCNumber"].ToString(), "'");
        LogMessageStatic("### SP Get Faculty Names Control ### => Sql  :   " + Sql, HttpContext.Current.Session["sTCNumber"].ToString(), logFileName);
        try
        {
            connection.Open();
            command = new SqlCommand("[ExamProctorProject].[dbo].[spGetFacultyNames]", connection);
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
            LogMessageStatic("!!! Error !!! => [ExamProctorProject].[dbo].[spGetFacultyNames] , Error Message = " + ex.Message.ToString(), HttpContext.Current.Session["sTCNumber"].ToString(), logFileName);
        }

        finally
        {
            command.Dispose();
            reader = null;
            connection.Close();
        }
        return JsonConvert.SerializeObject(list); 
   
    }

    [System.Web.Services.WebMethod]
    public static string add_update_delete_faculty(int processType, int facultyId, string facultyName)
    {
        string result = string.Empty;

        string connectionstring = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection connection = new SqlConnection(connectionstring);
        SqlCommand command = new SqlCommand();
        SqlDataReader reader = null;

        string commandText =string.Empty;


        #region Sql add_update_delete_faculty
        try
        {
            connection.Open();
            if (processType == 1)
            {
                string Sql = "EXEC [ExamProctorProject].[dbo].[spAddFaculty] " + sqlValStatic(HttpContext.Current.Session["sTCNumber"].ToString(), "'");
                LogMessageStatic("### SP Add Faculty Control ### => Sql  :   " + Sql, HttpContext.Current.Session["sTCNumber"].ToString(), logFileName);

                command = new SqlCommand("[ExamProctorProject].[dbo].[spAddFaculty]", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@facultyName", facultyName);

                LogMessageStatic("### Information ### => Faculty Name = " + facultyName, HttpContext.Current.Session["sTCNumber"].ToString(), logFileName);
            }

            else if (processType == 2)
            {
                string Sql = "EXEC [ExamProctorProject].[dbo].[spUpdateFacultyName] " + sqlValStatic(HttpContext.Current.Session["sTCNumber"].ToString(), "'");
                LogMessageStatic("### SP Update Faculty Control ### => Sql  :   " + Sql, HttpContext.Current.Session["sTCNumber"].ToString(), logFileName);

                command = new SqlCommand("[ExamProctorProject].[dbo].[spUpdateFacultyName]", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", facultyId);
                command.Parameters.AddWithValue("@newFacultyName", facultyName);

                LogMessageStatic("### Information ### => Faculty ID = " + facultyId, HttpContext.Current.Session["sTCNumber"].ToString(), logFileName);
                LogMessageStatic("### Information ### => New Faculty Name = " + facultyName, HttpContext.Current.Session["sTCNumber"].ToString(), logFileName);
            }

            else if (processType == -1)
            {
                string Sql = "EXEC [ExamProctorProject].[dbo].[spDeleteFacultyName] " + sqlValStatic(HttpContext.Current.Session["sTCNumber"].ToString(), "'");
                LogMessageStatic("### SP Delete Faculty Control ### => Sql  :   " + Sql, HttpContext.Current.Session["sTCNumber"].ToString(), logFileName);

                command = new SqlCommand("[ExamProctorProject].[dbo].[spDeleteFacultyName]", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", facultyId);

                LogMessageStatic("### Information ### => Faculty ID = " + facultyId, HttpContext.Current.Session["sTCNumber"].ToString(), logFileName);
            }
            
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                result = reader[0].ToString();
                LogMessageStatic("### Information ### => Result = " + result, HttpContext.Current.Session["sTCNumber"].ToString(), logFileName);
            }

        }
        catch (Exception ex) 
        {
            result = "System Error";
            LogMessageStatic("!!! Error !!!, Error Message = " + result, HttpContext.Current.Session["sTCNumber"].ToString(), logFileName);
        }

        finally
        {
            command.Dispose();
            reader = null;
            connection.Close();
        }
        return result;
        #endregion
    }
}