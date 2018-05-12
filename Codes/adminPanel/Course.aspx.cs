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

public partial class adminPanel_Course : BaseClass
{
    public static string logFileName = "page_AdminCourse";

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
        LogMessageStatic("### SP Faculty Name Control ### => Sql  :   " + Sql, HttpContext.Current.Session["sTCNumber"].ToString(), logFileName);

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
        LogMessageStatic("### Information ### => Faculty Name = " + facultyName, HttpContext.Current.Session["sTCNumber"].ToString(), logFileName);     
        return facultyNames;
    }

    [System.Web.Services.WebMethod]
    public static List<Common> getDepartmentName(string facultyName)
    {
        List<Common> departmentNames = new List<Common>();
        Common departmentName_ = new Common();
        departmentName_.Text = "Choose...";
        departmentName_.Value = "0";
        departmentNames.Add(departmentName_);

        string departmentName = string.Empty;
        string connectionstring = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection connection = new SqlConnection(connectionstring);
        SqlCommand command = new SqlCommand();
        SqlDataReader reader = null;

        string Sql = "EXEC [ExamProctorProject].[dbo].[spGetDepartmentNames] " + sqlValStatic(HttpContext.Current.Session["sTCNumber"].ToString(), "'");
        LogMessageStatic("### SP Department Name Control ### => Sql  :   " + Sql, HttpContext.Current.Session["sTCNumber"].ToString(), logFileName);
        try
        {
            connection.Open();
            command = new SqlCommand("[ExamProctorProject].[dbo].[spGetDepartmentNames]", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@FacultyName", facultyName);
            reader = command.ExecuteReader();
            int counter = 1;
            while (reader.Read())
            {
                departmentName = reader["name"].ToString();
                if (!string.IsNullOrEmpty(departmentName.Replace(" ", "")))
                {
                    counter++;
                    departmentName_ = new Common();
                    departmentName_.Text = departmentName;
                    departmentName_.Value = counter.ToString();
                    departmentNames.Add(departmentName_);
                }
                LogMessageStatic("### Information ### => Department Name = " + departmentName, HttpContext.Current.Session["sTCNumber"].ToString(), logFileName);
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
        LogMessageStatic("### Information ### => Department Name = " + departmentName, HttpContext.Current.Session["sTCNumber"].ToString(), logFileName);
        return departmentNames;
    }

    [System.Web.Services.WebMethod]
    public static List<Common> getClassDegrees(string facultyName, string departmentName)
    {
        List<Common> classDegrees = new List<Common>();
        Common classDegrees_ = new Common();
        classDegrees_.Text = "All";
        classDegrees_.Value = "0";
        classDegrees.Add(classDegrees_);

        int maxYearNumber = 0;
        string connectionstring = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection connection = new SqlConnection(connectionstring);
        SqlCommand command = new SqlCommand();
        SqlDataReader reader = null;

        string Sql = "EXEC [ExamProctorProject].[dbo].[spGetDepartmentYearNumber] " + sqlValStatic(HttpContext.Current.Session["sTCNumber"].ToString(), "'");
        LogMessageStatic("### SP Get Department Year Number Control ### => Sql  :   " + Sql, HttpContext.Current.Session["sTCNumber"].ToString(), logFileName);
        try
        {
            connection.Open();
            command = new SqlCommand("[ExamProctorProject].[dbo].[spGetDepartmentYearNumber]", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@facultyName", facultyName);
            command.Parameters.AddWithValue("@departmentName", departmentName);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                try
                {
                    maxYearNumber = Convert.ToInt32(reader["yearNumber"].ToString());

                }
                catch (Exception ex)
                {
                    maxYearNumber = 0;
                    LogMessageStatic("!!! Error !!! => [ExamProctorProject].[dbo].[spGetDepartmentYearNumber] , Error Message = " + ex.Message.ToString(), HttpContext.Current.Session["sTCNumber"].ToString(), logFileName);
                }
            }
     
            if (maxYearNumber>0)
            {
                for (int i = 1; i <= maxYearNumber; i++)
                {
                    classDegrees_ = new Common();
                    classDegrees_.Text = i.ToString();
                    classDegrees_.Value = i.ToString();
                    classDegrees.Add(classDegrees_);
                }                
            }
        }
        catch (Exception ex)
        {
            LogMessageStatic("!!! Error !!! => [ExamProctorProject].[dbo].[spGetDepartmentYearNumber] , Error Message = " + ex.Message.ToString(), HttpContext.Current.Session["sTCNumber"].ToString(), logFileName);
        }

        finally
        {
            command.Dispose();
            reader = null;
            connection.Close();
        }
        LogMessageStatic("### Information ### => Maximum Year Number = " + maxYearNumber.ToString(), HttpContext.Current.Session["sTCNumber"].ToString(), logFileName);
        return classDegrees;
    }

    public class Courses
    {
        public string id { get; set; }
        public string name { get; set; }
        public int classDegree { get; set; }
    } 

    [System.Web.Services.WebMethod]
    public static string getCourse(string facultyName, string departmentName, string classDegree)
    {
        
        string connectionstring = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection connection = new SqlConnection(connectionstring);
        SqlCommand command = new SqlCommand();
        SqlDataReader reader = null;

        string Sql = "EXEC [ExamProctorProject].[dbo].[spGetCourseNames] " + sqlValStatic(HttpContext.Current.Session["sTCNumber"].ToString(), "'");
        LogMessageStatic("### SP Get Course Names Control ### => Sql  :   " + Sql, HttpContext.Current.Session["sTCNumber"].ToString(), logFileName);

        List<Courses> list = new List<Courses>();
        try
        {
            connection.Open();
            command = new SqlCommand("[ExamProctorProject].[dbo].[spGetCourseNames]", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@facultyName", facultyName);
            command.Parameters.AddWithValue("@departmentName", departmentName);
            command.Parameters.AddWithValue("@classDegree", classDegree);
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                Courses courses = new Courses();
                courses.id = reader["Id"].ToString();
                courses.name = reader["courseName"].ToString();
                courses.classDegree = Convert.ToInt32(reader["classDegree"].ToString());
                list.Add(courses);

                LogMessageStatic("### Information ### => Course ID = " + courses.id.ToString(), HttpContext.Current.Session["sTCNumber"].ToString(), logFileName);
                LogMessageStatic("### Information ### => Course Name = " + courses.name.ToString(), HttpContext.Current.Session["sTCNumber"].ToString(), logFileName);
                LogMessageStatic("### Information ### => Course Class Degree = " + courses.classDegree.ToString(), HttpContext.Current.Session["sTCNumber"].ToString(), logFileName);
            }
        }

        catch (Exception ex)
        {
            LogMessageStatic("!!! Error !!! => [ExamProctorProject].[dbo].[spGetCourseNames] , Error Message = " + ex.Message.ToString(), HttpContext.Current.Session["sTCNumber"].ToString(), logFileName);
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
    public static string add_update_delete_course(int processType, string facultyName, string departmentName, string courseName,int classDegree, string courseId, string ipAddress)
    {

        string result = string.Empty;
        string courseFacultyDepartmentClassIndexId = string.Empty;
        if (processType!=1)
        {
            string [] split_ = courseId.Split('-');
            courseId = split_[0];
            courseFacultyDepartmentClassIndexId = split_[1];
        }
        
        string connectionstring = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection connection = new SqlConnection(connectionstring);
        SqlCommand command = new SqlCommand();
        SqlDataReader reader = null;

        string commandText = string.Empty;

        #region Sql add_update_delete_course
        try
        {
            connection.Open();
            if (processType == 1)
            {
                string Sql = "EXEC [ExamProctorProject].[dbo].[spAddCourse] " + sqlValStatic(HttpContext.Current.Session["sTCNumber"].ToString(), "'");
                LogMessageStatic("### SP Add Course Control ### => Sql  :   " + Sql, HttpContext.Current.Session["sTCNumber"].ToString(), logFileName);

                command = new SqlCommand("[ExamProctorProject].[dbo].[spAddCourse]", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@facultyName", facultyName);
                command.Parameters.AddWithValue("@departmentName", departmentName);
                command.Parameters.AddWithValue("@courseName", courseName);
                command.Parameters.AddWithValue("@classDegree", classDegree);
                command.Parameters.AddWithValue("@ipAddress", ipAddress);

                LogMessageStatic("### Information ### => Faculty Name = " + facultyName, HttpContext.Current.Session["sTCNumber"].ToString(), logFileName);
                LogMessageStatic("### Information ### => Department Name = " + departmentName, HttpContext.Current.Session["sTCNumber"].ToString(), logFileName);
                LogMessageStatic("### Information ### => Course Name = " + courseName, HttpContext.Current.Session["sTCNumber"].ToString(), logFileName);
                LogMessageStatic("### Information ### => Class Degree = " + classDegree, HttpContext.Current.Session["sTCNumber"].ToString(), logFileName);
                LogMessageStatic("### Information ### => IP Address = " + ipAddress, HttpContext.Current.Session["sTCNumber"].ToString(), logFileName);

            }
            else if (processType == 2)
            {
                string Sql = "EXEC [ExamProctorProject].[dbo].[spUpdateCourse] " + sqlValStatic(HttpContext.Current.Session["sTCNumber"].ToString(), "'");
                LogMessageStatic("### SP Update Course Control ### => Sql  :   " + Sql, HttpContext.Current.Session["sTCNumber"].ToString(), logFileName);

                command = new SqlCommand("[ExamProctorProject].[dbo].[spUpdateCourse]", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@courseId", courseId);
                command.Parameters.AddWithValue("@courseFacultyDepartmentClassIndexId", courseFacultyDepartmentClassIndexId);
                command.Parameters.AddWithValue("@courseName", courseName);
                command.Parameters.AddWithValue("@classDegree", classDegree);
                command.Parameters.AddWithValue("@ipAddress", ipAddress);

                LogMessageStatic("### Information ### => Course ID = " + courseId, HttpContext.Current.Session["sTCNumber"].ToString(), logFileName);
                LogMessageStatic("### Information ### => Course Faculty Department Class Index Id = " + courseFacultyDepartmentClassIndexId, HttpContext.Current.Session["sTCNumber"].ToString(), logFileName);
                LogMessageStatic("### Information ### => Course Name = " + courseName, HttpContext.Current.Session["sTCNumber"].ToString(), logFileName);
                LogMessageStatic("### Information ### => Class Degree = " + classDegree, HttpContext.Current.Session["sTCNumber"].ToString(), logFileName);
                LogMessageStatic("### Information ### => IP Address = " + ipAddress, HttpContext.Current.Session["sTCNumber"].ToString(), logFileName);

            }
            else if (processType == -1)
            {
                string Sql = "EXEC [ExamProctorProject].[dbo].[spDeleteCourse] " + sqlValStatic(HttpContext.Current.Session["sTCNumber"].ToString(), "'");
                LogMessageStatic("### SP Delete Course Control ### => Sql  :   " + Sql, HttpContext.Current.Session["sTCNumber"].ToString(), logFileName);

                command = new SqlCommand("[ExamProctorProject].[dbo].[spDeleteCourse]", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@courseId", courseId);
                command.Parameters.AddWithValue("@courseFacultyDepartmentClassIndexId", courseFacultyDepartmentClassIndexId);
                command.Parameters.AddWithValue("@ipAddress", ipAddress);


                LogMessageStatic("### Information ### => Course ID = " + courseId, HttpContext.Current.Session["sTCNumber"].ToString(), logFileName);
                LogMessageStatic("### Information ### => Course Name = " + courseFacultyDepartmentClassIndexId, HttpContext.Current.Session["sTCNumber"].ToString(), logFileName);
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