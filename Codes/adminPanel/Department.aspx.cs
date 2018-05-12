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

public partial class adminPanel_Department : BaseClass
{
    public static string logFileName = "page_AdminDepartment";

    protected void Page_Load(object sender, EventArgs e)
    {
        sIpAddress = HttpContext.Current.Request.UserHostAddress;
        LogMessage("### Information ### => ipAddress = " + sIpAddress, "Ip List", logFileName);
    }
    public class Common
    {
        private string _text;
        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }
        private string _value;
        public string Value
        {
            get { return _value; }
            set { _value = value; }
        }
    }

    [System.Web.Services.WebMethod]
    public static List<Common> getFacultyName()
    {
        List<Common> facultyNames = new List<Common>();
        Common facultyName_ = new Common();
        facultyName_.Text = "Choose...";
        facultyName_.Value = "0";
        facultyNames.Add(facultyName_);

        string facultyName = string.Empty;
        string connectionstring = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection connection = new SqlConnection(connectionstring);
        SqlCommand command = new SqlCommand();
        SqlDataReader reader = null;

        string Sql = "EXEC [ExamProctorProject].[dbo].[spGetFacultyNames] " + sqlValStatic(HttpContext.Current.Session["sTCNumber"].ToString(), "'");
        LogMessageStatic("### SP Get Faculty Names Control ### => Sql  :   " + Sql, HttpContext.Current.Session["sTCNumber"].ToString(), logFileName);

        try
        {
            connection.Open();
            command = new SqlCommand("[ExamProctorProject].[dbo].[spGetFacultyNames]", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            reader = command.ExecuteReader();
            int counter = 1;
            while (reader.Read())
            {
                facultyName = reader[0].ToString();
                if (!string.IsNullOrEmpty(facultyName.Replace(" ", "")))
                {
                    counter++;
                    facultyName_ = new Common();
                    facultyName_.Text = facultyName;
                    facultyName_.Value = counter.ToString();
                    facultyNames.Add(facultyName_);
                }
                LogMessageStatic("### Information ### => Faculty Name = " + facultyName, HttpContext.Current.Session["sTCNumber"].ToString(), logFileName);
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
        LogMessageStatic("### Information ### => Faculty Name = " + facultyName.ToString(), HttpContext.Current.Session["sTCNumber"].ToString(), logFileName);
        return facultyNames;
    }


    public class Departmens
    {
        public int id { get; set; }
        public string name { get; set; }
        public string yearNumber { get; set; }
    } 

    [System.Web.Services.WebMethod]
    public static string getDepartmenName(string facultyName)
    {
        string connectionstring = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection connection = new SqlConnection(connectionstring);
        SqlCommand command = new SqlCommand();
        SqlDataReader reader = null;

        string Sql = "EXEC [ExamProctorProject].[dbo].[spGetDepartmentNames] " + sqlValStatic(HttpContext.Current.Session["sTCNumber"].ToString(), "'");
        LogMessageStatic("### SP Get Department Names Control ### => Sql  :   " + Sql, HttpContext.Current.Session["sTCNumber"].ToString(), logFileName);

        List<Departmens> list = new List<Departmens>();
        try
        {
            connection.Open();
            command = new SqlCommand("[ExamProctorProject].[dbo].[spGetDepartmentNames]", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@FacultyName", facultyName);
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                Departmens departmenNames = new Departmens();
                departmenNames.id = Convert.ToInt32(reader["id"].ToString());
                departmenNames.name = reader["name"].ToString();
                departmenNames.yearNumber = reader["yearNumber"].ToString();
                list.Add(departmenNames);

                LogMessageStatic("### Information ### => Department ID = " + departmenNames.id.ToString(), HttpContext.Current.Session["sTCNumber"].ToString(), logFileName);
                LogMessageStatic("### Information ### => Department Name = " + departmenNames.name.ToString(), HttpContext.Current.Session["sTCNumber"].ToString(), logFileName);
                LogMessageStatic("### Information ### => Department Year Number = " + departmenNames.yearNumber.ToString(), HttpContext.Current.Session["sTCNumber"].ToString(), logFileName);
            }
        }

        catch (Exception ex)
        {
            LogMessageStatic("!!! Error !!! => [ExamProctorProject].[dbo].[spGetDepartmentNames] , Error Message = " + ex.Message.ToString(), HttpContext.Current.Session["sTCNumber"].ToString(), logFileName);
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
    public static string add_update_delete_department(int processType, string facultyName, string departmentName, int yearNumber, string ipAddress, int departmentId)
    {
        string result = string.Empty;

        string connectionstring = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection connection = new SqlConnection(connectionstring);
        SqlCommand command = new SqlCommand();
        SqlDataReader reader = null;

        string commandText = string.Empty;

        #region Sql add_update_delete_department
        try
        {
            connection.Open();

            if (processType == 1)
            {
                string Sql = "EXEC [ExamProctorProject].[dbo].[spAddDepartment] " + sqlValStatic(HttpContext.Current.Session["sTCNumber"].ToString(), "'");
                LogMessageStatic("### SP Add Department Control ### => Sql  :   " + Sql, HttpContext.Current.Session["sTCNumber"].ToString(), logFileName);

                command = new SqlCommand("[ExamProctorProject].[dbo].[spAddDepartment]", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@facultyName", facultyName);
                command.Parameters.AddWithValue("@departmentName", departmentName);
                command.Parameters.AddWithValue("@yearNumber", yearNumber);
                command.Parameters.AddWithValue("@ipAddress", ipAddress);

                LogMessageStatic("### Information ### => Faculty Name = " + facultyName, HttpContext.Current.Session["sTCNumber"].ToString(), logFileName);
                LogMessageStatic("### Information ### => Department Name = " + departmentName, HttpContext.Current.Session["sTCNumber"].ToString(), logFileName);
                LogMessageStatic("### Information ### => Year Number = " + yearNumber, HttpContext.Current.Session["sTCNumber"].ToString(), logFileName);
                LogMessageStatic("### Information ### => IP Address = " + ipAddress, HttpContext.Current.Session["sTCNumber"].ToString(), logFileName);
            }
            else if (processType == 2)
            {

                string Sql = "EXEC [ExamProctorProject].[dbo].[spUpdateDepartmentName] " + sqlValStatic(HttpContext.Current.Session["sTCNumber"].ToString(), "'");
                LogMessageStatic("### SP Update Department Control ### => Sql  :   " + Sql, HttpContext.Current.Session["sTCNumber"].ToString(), logFileName);

                command = new SqlCommand("[ExamProctorProject].[dbo].[spUpdateDepartmentName]", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@departmentId", departmentId);
                command.Parameters.AddWithValue("@departmentName", departmentName);
                command.Parameters.AddWithValue("@yearNumber", yearNumber);
                command.Parameters.AddWithValue("@ipAddress", ipAddress);

                LogMessageStatic("### Information ### => Department ID = " + departmentId, HttpContext.Current.Session["sTCNumber"].ToString(), logFileName);
                LogMessageStatic("### Information ### => Department Name = " + departmentName, HttpContext.Current.Session["sTCNumber"].ToString(), logFileName);
                LogMessageStatic("### Information ### => Year Number = " + yearNumber, HttpContext.Current.Session["sTCNumber"].ToString(), logFileName);
                LogMessageStatic("### Information ### => IP Address = " + ipAddress, HttpContext.Current.Session["sTCNumber"].ToString(), logFileName);
            }
            else if (processType == -1)
            {

                string Sql = "EXEC [ExamProctorProject].[dbo].[spDeleteDepartmentName] " + sqlValStatic(HttpContext.Current.Session["sTCNumber"].ToString(), "'");
                LogMessageStatic("### SP Delete Department Control ### => Sql  :   " + Sql, HttpContext.Current.Session["sTCNumber"].ToString(), logFileName);

                command = new SqlCommand("[ExamProctorProject].[dbo].[spDeleteDepartmentName]", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", departmentId);
                command.Parameters.AddWithValue("@ipAddress", ipAddress);

                LogMessageStatic("### Information ### => Department ID = " + departmentId, HttpContext.Current.Session["sTCNumber"].ToString(), logFileName);             
                LogMessageStatic("### Information ### => IP Address = " + ipAddress, HttpContext.Current.Session["sTCNumber"].ToString(), logFileName);
            }

            reader = command.ExecuteReader();

            while (reader.Read())
            {
                result = reader[0].ToString();
                LogMessageStatic("### Information ### => Result = " + result, HttpContext.Current.Session["sTCNumber"].ToString(), logFileName);
            }

        }
        catch (Exception) 
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