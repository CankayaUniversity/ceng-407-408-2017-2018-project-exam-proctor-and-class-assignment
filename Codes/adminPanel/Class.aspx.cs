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

public partial class adminPanel_Class : BaseClass
{
    public static string logFileName = "page_AdminClass";

    protected void Page_Load(object sender, EventArgs e)
    {
        sIpAddress = HttpContext.Current.Request.UserHostAddress;
        LogMessage("### Information ### => ipAddress = " + sIpAddress, "Ip List", logFileName);
    }

    public class Common
    {
        public int id { get; set; }
        public string name { get; set; }
        public int classCapacity { get; set; }
        public string selectValue { get; set; }
    } 

    [System.Web.Services.WebMethod]
    public static string getClassNames()
    {        
        string connectionstring = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection connection = new SqlConnection(connectionstring);
        SqlCommand command = new SqlCommand();
        SqlDataReader reader = null;

        List<Common> list = new List<Common>();

        string Sql = "EXEC [ExamProctorProject].[dbo].[spGetClassNames] " + sqlValStatic(HttpContext.Current.Session["sTCNumber"].ToString(), "'");
        LogMessageStatic("### SP Get Class Names Control ### => Sql  :   " + Sql, HttpContext.Current.Session["sTCNumber"].ToString(), logFileName);

        try
        {
            connection.Open();
            command = new SqlCommand("[ExamProctorProject].[dbo].[spGetClassNames]", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            reader = command.ExecuteReader();
            int counter = 0;
            while (reader.Read())
            {
                Common classNames = new Common();
                classNames.id = Convert.ToInt32(reader["Id"].ToString());
                classNames.name = reader["className"].ToString();
                classNames.selectValue = reader["classTypeName"].ToString();
                classNames.classCapacity = Convert.ToInt32(reader["classCapacity"].ToString()); 
                list.Add(classNames);
                counter++;

                LogMessageStatic("### Information ### => Class ID = " + classNames.id.ToString(), HttpContext.Current.Session["sTCNumber"].ToString(), logFileName);
                LogMessageStatic("### Information ### => Class Name= " + classNames.name.ToString(), HttpContext.Current.Session["sTCNumber"].ToString(), logFileName);
                LogMessageStatic("### Information ### => Class Selected Value = " + classNames.selectValue.ToString(), HttpContext.Current.Session["sTCNumber"].ToString(), logFileName);
                LogMessageStatic("### Information ### => Class Capacity = " + classNames.classCapacity.ToString(), HttpContext.Current.Session["sTCNumber"].ToString(), logFileName);
            }
        }

        catch (Exception ex)
        {
            LogMessageStatic("!!! Error !!! => [ExamProctorProject].[dbo].[spGetClassNames] , Error Message = " + ex.Message.ToString(), HttpContext.Current.Session["sTCNumber"].ToString(), logFileName);
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
    public static string getClassTypes()
    {
        List<Common> list = new List<Common>();
        Common classTypes = new Common();
        classTypes.id = 0;
        classTypes.name = "Choose...";
        list.Add(classTypes);

        string connectionstring = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection connection = new SqlConnection(connectionstring);
        SqlCommand command = new SqlCommand();
        SqlDataReader reader = null;

        string Sql = "EXEC [ExamProctorProject].[dbo].[spGetClassTypes] " + sqlValStatic(HttpContext.Current.Session["sTCNumber"].ToString(), "'");
        LogMessageStatic("### SP Get Class Types Control ### => Sql  :   " + Sql, HttpContext.Current.Session["sTCNumber"].ToString(), logFileName);

        try
        {
            connection.Open();
            command = new SqlCommand("[ExamProctorProject].[dbo].[spGetClassTypes]", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                classTypes = new Common();
                classTypes.id = Convert.ToInt32(reader["Id"].ToString());
                classTypes.name = reader["classTypeName"].ToString();
                list.Add(classTypes);
                LogMessageStatic("### Information ### => Class Type ID = " + classTypes.id.ToString(), HttpContext.Current.Session["sTCNumber"].ToString(), logFileName);
                LogMessageStatic("### Information ### => Class Type Name= " + classTypes.name.ToString(), HttpContext.Current.Session["sTCNumber"].ToString(), logFileName);
            }
        }

        catch (Exception ex)
        {
            LogMessageStatic("!!! Error !!! => [ExamProctorProject].[dbo].[spGetClassTypes] , Error Message = " + ex.Message.ToString(), HttpContext.Current.Session["sTCNumber"].ToString(), logFileName);
        }

        finally
        {
            command.Dispose();
            reader = null;
            connection.Close();
        }
        LogMessageStatic("### Information ### => Class Types = " + classTypes, HttpContext.Current.Session["sTCNumber"].ToString(), logFileName);//????? olmalı mı
        return JsonConvert.SerializeObject(list);
    }

    [System.Web.Services.WebMethod]
    public static string add_update_delete_className(int processType, int classId, string className, int classCapacity, string classTypeName, string ipAddress)
    {
        string result = string.Empty;

        string connectionstring = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection connection = new SqlConnection(connectionstring);
        SqlCommand command = new SqlCommand();
        SqlDataReader reader = null;

        string commandText = string.Empty;

        #region Sql add_update_delete_className
        try
        {
            connection.Open();
            
            if (processType == 1)
            {

                string Sql = "EXEC [ExamProctorProject].[dbo].[spAddClass] " + sqlValStatic(HttpContext.Current.Session["sTCNumber"].ToString(), "'");
                LogMessageStatic("### SP Add Class Control ### => Sql  :   " + Sql, HttpContext.Current.Session["sTCNumber"].ToString(), logFileName);

                command = new SqlCommand("[ExamProctorProject].[dbo].[spAddClass]", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@className", className);
                command.Parameters.AddWithValue("@classTypeName", classTypeName);
                command.Parameters.AddWithValue("@classCapacity", classCapacity);
                command.Parameters.AddWithValue("@ipAddress", ipAddress);

                LogMessageStatic("### Information ### => Class Name = " + className, HttpContext.Current.Session["sTCNumber"].ToString(), logFileName);
                LogMessageStatic("### Information ### => Class Type Name = " + classTypeName, HttpContext.Current.Session["sTCNumber"].ToString(), logFileName);
                LogMessageStatic("### Information ### => Class Capacity = " + classCapacity, HttpContext.Current.Session["sTCNumber"].ToString(), logFileName);
                LogMessageStatic("### Information ### => IP Address = " + ipAddress, HttpContext.Current.Session["sTCNumber"].ToString(), logFileName);
            }
            else if (processType == 2)
            {

                string Sql = "EXEC [ExamProctorProject].[dbo].[spUpdateClass] " + sqlValStatic(HttpContext.Current.Session["sTCNumber"].ToString(), "'");
                LogMessageStatic("### SP Update Class Control ### => Sql  :   " + Sql, HttpContext.Current.Session["sTCNumber"].ToString(), logFileName);

                command = new SqlCommand("[ExamProctorProject].[dbo].[spUpdateClass]", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@classId", classId);
                command.Parameters.AddWithValue("@className", className);                
                command.Parameters.AddWithValue("@classTypeName", classTypeName);
                command.Parameters.AddWithValue("@classCapacity", classCapacity);
                command.Parameters.AddWithValue("@ipAddress", ipAddress);

                LogMessageStatic("### Information ### => Class ID = " + classId, HttpContext.Current.Session["sTCNumber"].ToString(), logFileName);
                LogMessageStatic("### Information ### => Class Name = " + className, HttpContext.Current.Session["sTCNumber"].ToString(), logFileName);
                LogMessageStatic("### Information ### => Class Type Name = " + classTypeName, HttpContext.Current.Session["sTCNumber"].ToString(), logFileName);
                LogMessageStatic("### Information ### => Class Capacity = " + classCapacity, HttpContext.Current.Session["sTCNumber"].ToString(), logFileName);
                LogMessageStatic("### Information ### => IP Address = " + ipAddress, HttpContext.Current.Session["sTCNumber"].ToString(), logFileName);

            }
            else if (processType == -1)
            {
                string Sql = "EXEC [ExamProctorProject].[dbo].[spDeleteClass] " + sqlValStatic(HttpContext.Current.Session["sTCNumber"].ToString(), "'");
                LogMessageStatic("### SP Delete Class Control ### => Sql  :   " + Sql, HttpContext.Current.Session["sTCNumber"].ToString(), logFileName);

                command = new SqlCommand("[ExamProctorProject].[dbo].[spDeleteClass]", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@classId", classId);
                command.Parameters.AddWithValue("@ipAddress", ipAddress);

                LogMessageStatic("### Information ### => Class ID = " + classId, HttpContext.Current.Session["sTCNumber"].ToString(), logFileName);
                LogMessageStatic("### Information ### => IP Address = " + ipAddress, HttpContext.Current.Session["sTCNumber"].ToString(), logFileName);
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
            LogMessageStatic("!!! Error !!!, Error Message = " + result, HttpContext.Current.Session["sTCNumber"].ToString(), logFileName);
            result = "System Error";
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