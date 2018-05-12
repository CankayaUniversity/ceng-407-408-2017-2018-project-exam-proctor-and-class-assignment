using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using library;

public partial class examRequest_MakeExamRequest : BaseClass
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
            ddlMyCourses.Items.Clear();
            ddlClassType.Items.Clear();
            getMyCourses();
            getClassTypes();


            for (int i = 0; i < 100; i=i+10)
            {
                ddlExamDuration.Items.Add(i.ToString());
            }
           
        }      
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
    public static List<Common> getMyCourses1()
    {
        List<Common> myCourses = new List<Common>();
        Common myCourseName_ = new Common();

        string courseName = string.Empty;
        string connectionstring = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection connection = new SqlConnection(connectionstring);
        SqlCommand command = new SqlCommand();
        SqlDataReader reader = null;

        //string Sql = "EXEC [ExamProctorProject].[dbo].[spGetPhoneNum] " + sqlVal(sTCNumber, "'");
        //LogMessage("### SP Phone Number Control ### => Sql  :   " + Sql, sTCNumber, logFileName);
        string TCNumber = HttpContext.Current.Session["sTCNumber"].ToString();
        //string TCNumber = "32978257698";


        try
        {
            connection.Open();
            command = new SqlCommand("[ExamProctorProject].[dbo].[spGetMyCourse]", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@TCNumber", TCNumber);
            reader = command.ExecuteReader();
            int counter = 1;
            while (reader.Read())
            {
                courseName = reader[0].ToString();
                if (!string.IsNullOrEmpty(courseName.Replace(" ", "")))
                {
                    counter++;
                    myCourseName_ = new Common();
                    myCourseName_.Text = courseName;
                    myCourseName_.Value = counter.ToString();
                    myCourses.Add(myCourseName_);
                }

            }
        }

        catch (Exception ex)
        {
            //LogMessage("!!! Error !!! => [ExamProctorProject].[dbo].[spGetPhoneNum] , Error Message = " + ex.Message.ToString(), sTCNumber, logFileName);
        }

        finally
        {
            command.Dispose();
            reader = null;
            connection.Close();
        }
        //LogMessage("### Information ### => firstPhoneNumber = " + firstphoneNum.ToString(), sTCNumber, logFileName);
        //LogMessage("### Information ### => secondPhoneNumber = " + secondphoneNum.ToString(), sTCNumber, logFileName);
        return myCourses;

    }


    [System.Web.Services.WebMethod]
    public static List<Common> getClassTypes1()
    {
        List<Common> list = new List<Common>();
        Common classTypes = new Common();
     

        string connectionstring = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection connection = new SqlConnection(connectionstring);
        SqlCommand command = new SqlCommand();
        SqlDataReader reader = null;

        try
        {
            connection.Open();
            command = new SqlCommand("[ExamProctorProject].[dbo].[spGetClassTypes]", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                classTypes = new Common();
                classTypes.Value = reader["Id"].ToString();
                classTypes.Text = reader["classTypeName"].ToString();
                list.Add(classTypes);
            }
        }

        catch (Exception ex)
        {
            //LogMessage("!!! Error !!! => [ExamProctorProject].[dbo].[spGetPhoneNum] , Error Message = " + ex.Message.ToString(), sTCNumber, logFileName);
        }

        finally
        {
            command.Dispose();
            reader = null;
            connection.Close();
        }
        return list;
    }

    public void getMyCourses()
    {
        SqlConnection connection = new SqlConnection(GetXmlConfigParameter("database", "ConnectionString"));
        SqlCommand command = new SqlCommand();
        SqlDataReader reader = null;

        //string Sql = "EXEC [ExamProctorProject].[dbo].[spGetPhoneNum] " + sqlVal(sTCNumber, "'");
        //LogMessage("### SP Phone Number Control ### => Sql  :   " + Sql, sTCNumber, logFileName);
        string TCNumber = HttpContext.Current.Session["sTCNumber"].ToString();
        //string TCNumber = "32978257698";


        try
        {
            connection.Open();
            command = new SqlCommand("[ExamProctorProject].[dbo].[spGetMyCourse]", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@TCNumber", TCNumber);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                ddlMyCourses.Items.Add(new ListItem(reader[0].ToString(), reader[0].ToString()));
            }
        }

        catch (Exception ex)
        {
            //LogMessage("!!! Error !!! => [ExamProctorProject].[dbo].[spGetPhoneNum] , Error Message = " + ex.Message.ToString(), sTCNumber, logFileName);
        }

        finally
        {
            command.Dispose();
            reader = null;
            connection.Close();
        }
        //LogMessage("### Information ### => firstPhoneNumber = " + firstphoneNum.ToString(), sTCNumber, logFileName);
        //LogMessage("### Information ### => secondPhoneNumber = " + secondphoneNum.ToString(), sTCNumber, logFileName);

    }
    public void getClassTypes()
    {
        SqlConnection connection = new SqlConnection(GetXmlConfigParameter("database", "ConnectionString"));
        SqlCommand command = new SqlCommand();
        SqlDataReader reader = null;

        try
        {
            connection.Open();
            command = new SqlCommand("[ExamProctorProject].[dbo].[spGetClassTypes]", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                ddlClassType.Items.Add(new ListItem(reader["classTypeName"].ToString(), reader["classTypeName"].ToString()));
            }
        }

        catch (Exception ex)
        {
            //LogMessage("!!! Error !!! => [ExamProctorProject].[dbo].[spGetPhoneNum] , Error Message = " + ex.Message.ToString(), sTCNumber, logFileName);
        }

        finally
        {
            command.Dispose();
            reader = null;
            connection.Close();
        }
    }
}