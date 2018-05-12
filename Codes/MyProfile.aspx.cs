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

public partial class MyProfile : BaseClass
{
    public static string logFileName = "page_MyProfile";

    protected void Page_Load(object sender, EventArgs e)
    {
        sIpAddress = HttpContext.Current.Request.UserHostAddress;
        LogMessage("### Information ### => ipAddress = " + sIpAddress, "Ip List", logFileName);
    }

    public class UserInfo
    {
        public string TCNumber { get; set; }
        public string Username { get; set; }
        public string Surname { get; set; }
        public string Title { get; set; }
        public string FacultyName { get; set; }
        public string DepartmentName { get; set; }
        public string ClassDegree { get; set; }
        public string StudentNo { get; set; }
        public string TelephoneNumber1 { get; set; }
        public string TelephoneNumber2 { get; set; }
        public string EmailAddress1 { get; set; }
        public string EmailAddress2 { get; set; }
    } 

    [System.Web.Services.WebMethod]
    public static string getUserInfo()
    {

        string connectionstring = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection connection = new SqlConnection(connectionstring);
        SqlCommand command = new SqlCommand();
        SqlDataReader reader = null;
        string TCNumber = HttpContext.Current.Session["sTCNumber"].ToString();

        //string TCNumber = "55216486568";
        string txtUsername = string.Empty;
        string txtSurname = string.Empty;
        string txtTitle = string.Empty;
        string txtFacultyName = string.Empty;
        string txtDepartmentName = string.Empty;
        string txtClassDegree = string.Empty;
        string txtStudentNo = string.Empty;
        string txtTelephoneNumber1 = string.Empty;
        string txtTelephoneNumber2 = string.Empty;
        string txtEmailAddress1 = string.Empty;
        string txtEmailAddress2 = string.Empty;
        int counter = 0;

        string Sql = "EXEC [ExamProctorProject].[dbo].[spGetUsersInfo]" + sqlValStatic(HttpContext.Current.Session["sTCNumber"].ToString(), "'");
        LogMessageStatic("### SP Get User Info Control ### => Sql :   " + Sql, HttpContext.Current.Session["sTCNumber"].ToString(), logFileName);

        try
        {
            connection.Open();
            command = new SqlCommand("[ExamProctorProject].[dbo].[spGetUsersInfo]", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@TCNumber", TCNumber);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                txtUsername = reader["userName"].ToString();
                txtSurname = reader["userSurname"].ToString();
                txtTitle = reader["userTitle"].ToString();
                txtFacultyName = reader["facultyName"].ToString();
                txtDepartmentName = reader["departmentName"].ToString();
                txtClassDegree = reader["userClassDegree"].ToString();
                txtStudentNo = reader["userStudentNo"].ToString();
                txtTelephoneNumber1 = reader["userPhoneNumber"].ToString();
                txtTelephoneNumber2 = reader["userPhoneNumber2"].ToString();
                txtEmailAddress1 = reader["userEmail"].ToString();
                txtEmailAddress2 = reader["userEmail2"].ToString();
                counter++;
            }
        }

        catch (Exception ex)
        {
            counter = 0;
            LogMessageStatic("!!! Error !!! => [ExamProctorProject].[dbo].[spGetUsersInfo] , Error Message = " + ex.Message.ToString(), HttpContext.Current.Session["sTCNumber"].ToString(), logFileName);
        }

        finally
        {
            command.Dispose();
            reader = null;
            connection.Close();
        }

        LogMessageStatic("### Information ### => Username = " + txtUsername.ToString(), TCNumber, logFileName);
        LogMessageStatic("### Information ### => Surname = " + txtSurname.ToString(), TCNumber, logFileName);
        LogMessageStatic("### Information ### => Title = " + txtTitle.ToString(), TCNumber, logFileName);
        LogMessageStatic("### Information ### => Faculty Name = " + txtFacultyName.ToString(), TCNumber, logFileName);
        LogMessageStatic("### Information ### => Department Name = " + txtDepartmentName.ToString(), TCNumber, logFileName);
        LogMessageStatic("### Information ### => Class Degree = " + txtClassDegree.ToString(), TCNumber, logFileName);
        LogMessageStatic("### Information ### => Student No = " + txtStudentNo.ToString(), TCNumber, logFileName);
        LogMessageStatic("### Information ### => Telephone Number 1 = " + txtTelephoneNumber1.ToString(), TCNumber, logFileName);
        LogMessageStatic("### Information ### => Telephone Number 2 = " + txtTelephoneNumber2.ToString(), TCNumber, logFileName);
        LogMessageStatic("### Information ### => Email Address = " + txtEmailAddress1.ToString(), TCNumber, logFileName);
        LogMessageStatic("### Information ### => Email ddress = " + txtEmailAddress2.ToString(), TCNumber, logFileName);


        List<UserInfo> list = new List<UserInfo>();
        if (counter > 0)
        {
            UserInfo userInfo_ = new UserInfo();
            userInfo_.TCNumber = TCNumber;
            userInfo_.Username = txtUsername;
            userInfo_.Surname = txtSurname;
            userInfo_.Title = txtTitle;
            userInfo_.FacultyName = txtFacultyName;
            userInfo_.DepartmentName = txtDepartmentName;
            userInfo_.ClassDegree = txtClassDegree;
            userInfo_.StudentNo = txtStudentNo;
            userInfo_.TelephoneNumber1 = txtTelephoneNumber1;
            userInfo_.TelephoneNumber2 = txtTelephoneNumber2;
            userInfo_.EmailAddress1 = txtEmailAddress1;
            userInfo_.EmailAddress2 = txtEmailAddress2;
            list.Add(userInfo_);
        }

        return JsonConvert.SerializeObject(list);
    }

    [System.Web.Services.WebMethod]
    public static string btnSaveClick(string TCNumber, string TelephoneNumber2, string EmailAddress2)
    {
        string response = string.Empty;
        string connectionstring = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection connection = new SqlConnection(connectionstring);
        SqlCommand command = new SqlCommand();
        SqlDataReader reader = null;

        string Sql = "EXEC [ExamProctorProject].[dbo].[spUpdateUsersInfo]" + sqlValStatic(HttpContext.Current.Session["sTCNumber"].ToString(), "'");
        LogMessageStatic("### SP Update Users Info Control ### => Sql :   " + Sql, HttpContext.Current.Session["sTCNumber"].ToString(), logFileName);

        try
        {
            connection.Open();
            command = new SqlCommand("[ExamProctorProject].[dbo].[spUpdateUsersInfo]", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@TCNumber", TCNumber);
            command.Parameters.AddWithValue("@TelephoneNumber2", TelephoneNumber2);
            command.Parameters.AddWithValue("@EmailAddress2", EmailAddress2);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                response = reader[0].ToString();
            }

        }

        catch (Exception ex)
        {
            LogMessageStatic("!!! Error !!! => [ExamProctorProject].[dbo].[spUpdateUsersInfo] , Error Message = " + ex.Message.ToString(), HttpContext.Current.Session["sTCNumber"].ToString(), logFileName);
        }

        finally
        {
            command.Dispose();
            reader = null;
            connection.Close();
        }

        LogMessageStatic("### Information ### => TC Number = " + TCNumber, HttpContext.Current.Session["sTCNumber"].ToString(), logFileName);
        LogMessageStatic("### Information ### => Telephone Number 2 = " + TelephoneNumber2, HttpContext.Current.Session["sTCNumber"].ToString(), logFileName);
        LogMessageStatic("### Information ### => Email Address 2 = " + EmailAddress2, HttpContext.Current.Session["sTCNumber"].ToString(), logFileName);
        return response;
    }
}