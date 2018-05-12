using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using library;
using System.Data.SqlClient;
using System.Configuration;

public partial class Login : BaseClass
{
    public static string logFileName = "page_Login";

    protected void Page_Load(object sender, EventArgs e)
    {
        sIpAddress = HttpContext.Current.Request.UserHostAddress;
        LogMessage("### Information ### => ipAddress = " + sIpAddress, "Ip List", logFileName);
        sLoginControl = 1;
        sTCNumber = string.Empty;
        sUserTypeID = 0;
        sDepartmentID = 0;
        sFacultyID = 0;
        sClassDeg = 0;
        sStudentNo = 0;
        sTitle = string.Empty;
    }

    protected void btnSignIn_Click(object sender, EventArgs e)
    {
        string counter = string.Empty;
        string TCNumber = txtUserTC.Text;
        string password = txtPassword.Text;
        sIpAddress = HttpContext.Current.Request.UserHostAddress;
        LogMessage("### Information ### => ipAddress = " + sIpAddress, TCNumber, logFileName);

        SqlConnection connection = new SqlConnection(GetXmlConfigParameter("database", "ConnectionString"));
        SqlCommand command = new SqlCommand();
        SqlDataReader reader = null;

        string Sql = "EXEC [ExamProctorProject].[dbo].[spLoginControl]" + sqlVal(TCNumber, "'") + sqlVal(password, "'");
        LogMessage("### SP Login Control ### => Sql :   " + Sql, TCNumber, logFileName);

        #region TCNumber & Password Control
        try
        {
            connection.Open();
            command = new SqlCommand("[ExamProctorProject].[dbo].[spLoginControl]", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@TCNo", TCNumber);
            command.Parameters.AddWithValue("@password", password);
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                counter = reader["numofContact"].ToString();
            }
        }
        catch (Exception ex)
        {
            LogMessage("!!! Error !!! => [ExamProctorProject].[dbo].[spLoginControl] , Error Message = " + ex.Message.ToString(), TCNumber, logFileName);
            counter = string.Empty;
        }

        finally
        {
            command.Dispose();
            reader = null;
            connection.Close();
        }

        LogMessage("### Information ### => counter = " + counter.ToString(), TCNumber, logFileName);
        #endregion

        #region Get User Information
        if (counter == "1")
        {
            int error = 0;
            sTCNumber = TCNumber;
            try
            {
                connection = new SqlConnection(GetXmlConfigParameter("database", "ConnectionString"));
                connection.Open();
                command = new SqlCommand("[ExamProctorProject].[dbo].[spAllInformation]", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@sLogin", TCNumber);
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    sUserTypeID = Convert.ToInt32(reader["UserTypeID"]);

                    if (sUserTypeID != 7)
                    {
                        sDepartmentID = Convert.ToInt32(reader["UserDepartmentID"]);
                        sFacultyID = Convert.ToInt32(reader["UserFacultyID"]);
                        if (sUserTypeID == 1)
                        {
                            sStudentNo = Convert.ToInt32(reader["StudentNumber"]);
                            sClassDeg = Convert.ToInt32(reader["UserDegree"]);
                        }
                    }
                    sTitle = reader["UserTitle"].ToString();
                }

                LogMessage("### Information ### => sUserTypeID = " + sUserTypeID.ToString(), TCNumber, logFileName);
                LogMessage("### Information ### => sDepartmentID = " + sDepartmentID.ToString(), TCNumber, logFileName);
                LogMessage("### Information ### => sFacultyID = " + sFacultyID.ToString(), TCNumber, logFileName);
                LogMessage("### Information ### => sClassDegree = " + sClassDeg.ToString(), TCNumber, logFileName);
                LogMessage("### Information ### => sStudentNo = " + sStudentNo.ToString(), TCNumber, logFileName);
                LogMessage("### Information ### => sTitle = " + sTitle.ToString(), TCNumber, logFileName);

            }
            catch (Exception ex)
            {
                error = 1;
                counter = string.Empty;
                LogMessage("!!! Error !!! => [ExamProctorProject].[dbo].[spAllInformation] , Error Message = " + ex.Message.ToString(), TCNumber, logFileName);

            }
            finally
            {
                command.Dispose();
                reader = null;
                connection.Close();
            }
            if (error == 0)
            {
                sLoginControl = 1;
                LogMessage("### Information ### => Response.Redirect(LoginAccess.aspx) ", TCNumber, logFileName);

                // ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                //getPhoneNumber();
                Response.Redirect("HomePage.aspx");
            }
            else
            {
                //ShowMessage("There is no user with this information, please check your information");
            }

        }
        else
        {
            //ShowMessage("There is no user with this information, please check your information");
        }
        #endregion
    }

    [System.Web.Services.WebMethod]
    public static List<Common> getPhoneNumber()
    {
        string sTCNumber = HttpContext.Current.Session["sTCNumber"].ToString();

        string firstphoneNum = string.Empty;
        string secondphoneNum = string.Empty;
        string connectionstring = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection connection = new SqlConnection(connectionstring);
        SqlCommand command = new SqlCommand();
        SqlDataReader reader = null;

        string Sql = "EXEC [ExamProctorProject].[dbo].[spGetPhoneNum] " + sqlValStatic(sTCNumber, "'");
        LogMessageStatic("### SP Phone Number Control ### => Sql  :   " + Sql, sTCNumber, logFileName);

        try
        {
            connection.Open();
            command = new SqlCommand("[ExamProctorProject].[dbo].[spGetPhoneNum]", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@sLogin", sTCNumber);
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                firstphoneNum = reader["UserFirstPhone"].ToString();
                secondphoneNum = reader["UserSecondPhone"].ToString();
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
        LogMessageStatic("### Information ### => firstPhoneNumber = " + firstphoneNum.ToString(), sTCNumber, logFileName);
        LogMessageStatic("### Information ### => secondPhoneNumber = " + secondphoneNum.ToString(), sTCNumber, logFileName);

        List<Common> phoneNumbers = new List<Common>();
        Common phoneNumber = new Common();
        phoneNumber.Text = "Choose...";
        phoneNumber.Value = "0";
        phoneNumbers.Add(phoneNumber);


        if (!string.IsNullOrEmpty(firstphoneNum.Replace(" ", "")))
        {
            phoneNumber = new Common();
            phoneNumber.Text = firstphoneNum;
            phoneNumber.Value = "1";
            phoneNumbers.Add(phoneNumber);
        }
        if (!string.IsNullOrEmpty(secondphoneNum.Replace(" ", "")))
        {
            phoneNumber = new Common();
            phoneNumber.Text = secondphoneNum;
            phoneNumber.Value = "2";
            phoneNumbers.Add(phoneNumber);
        }
        return phoneNumbers;

    }

    [System.Web.Services.WebMethod]
    public static List<Common> getEmailAddr()
    {
        string sTCNumber = HttpContext.Current.Session["sTCNumber"].ToString();
        string firstemailAddr = string.Empty;
        string secondemailAddr = string.Empty;
        string connectionstring = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection connection = new SqlConnection(connectionstring);
        SqlCommand command = new SqlCommand();
        SqlDataReader reader = null;
        string Sql = "EXEC [ExamProctorProject].[dbo].[spGetEmail]" + sqlValStatic(sTCNumber, "'");
        LogMessageStatic("### SP GET EMAIL CONTROL ### => TC = " + Sql, sTCNumber, logFileName);

        #region spGetEmail
        try
        {
            connection.Open();
            command = new SqlCommand("ExamProctorProject.dbo.spGetEmail", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@sLogin", sTCNumber);
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                firstemailAddr = reader["UserFirstEmail"].ToString();
                secondemailAddr = reader["UserSecondEmail"].ToString();
            }
        }

        catch (Exception ex)
        {
            //LogMessage("!!! Error !!! => [ExamProctorProject].[dbo].[spGetEmail] , Error Message = " + ex.Message.ToString(), sTCNumber, logFileName);
        }

        finally
        {
            command.Dispose();
            reader = null;
            connection.Close();
        }


        List<Common> emailAddr = new List<Common>();
        Common emailAddrress = new Common();
        emailAddrress.Text = "Choose...";
        emailAddrress.Value = "0";
        emailAddr.Add(emailAddrress);


        if (!string.IsNullOrEmpty(firstemailAddr.Replace(" ", "")))
        {
            emailAddrress = new Common();
            emailAddrress.Text = firstemailAddr;
            emailAddrress.Value = "1";
            emailAddr.Add(emailAddrress);
        }
        if (!string.IsNullOrEmpty(secondemailAddr.Replace(" ", "")))
        {
            emailAddrress = new Common();
            emailAddrress.Text = secondemailAddr;
            emailAddrress.Value = "2";
            emailAddr.Add(emailAddrress);
        }
        return emailAddr;


        #endregion
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
    public static string sendCode(string TCNumber, string selectedText, int senderType, string ipAddress, string activationCode_)
    {
        string result = "-1";
        try
        {
            if (selectedText == "Choose...")
            {
                return result;
            }

            string activationCode = generateActCode();
            string activationCodeControlResult = activationCodeControl(activationCode);
            while (activationCodeControlResult != "0")
            {
                activationCode = generateActCode();
                activationCodeControlResult = activationCodeControl(activationCode);
            }

            if (senderType == 1)
            {
                ws_Sms.Service1 sms_service = new ws_Sms.Service1();
                string response = sms_service.sendSingleSMS("demo.deb", "D9M1A5K2", selectedText, "Activation code: " + activationCode + "Please enter the activation code in 3 minutes... ", "TTmesaj", "0", "0");
                if (response.Contains("OK"))
                {
                    result = "1";
                    int resultInsert = 0;
                    response = response.Replace("*OK*", "");
                    insertActivationCode(TCNumber, activationCode, response, ipAddress, ref resultInsert);
                    if (resultInsert != 1)
                    {
                        //alert verilecek lütfen daha sonra tekrar  deneyiniz.
                    }
                }
                else
                {
                    result = "-1";
                }

            }
            else if (senderType == 2)
            {
                string response = "OK";
                if (response.Contains("OK"))
                {
                    result = "1";
                }
                else
                {
                    result = "-1";
                }
            }

            if (senderType == 3)
            {
                int check = 0;
                check = activationCodeDbCheck(TCNumber, activationCode_, ipAddress);
                if (check > 0)
                {
                    result = "100";
                }
                else
                {
                    result = "-100";
                }
            }
        }
        catch (Exception)
        {

            result = "-1";
        }
        return result;
    }

    [System.Web.Services.WebMethod]
    public static string signUp(string userTC)
    {
        string result = string.Empty;

        string connectionstring = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection connection = new SqlConnection(connectionstring);
        SqlCommand command = new SqlCommand();
        SqlDataReader reader = null;

        #region Sim1-Register
        try
        {
            connection.Open();
            command = new SqlCommand("[CollegeDB].[dbo].[spuserRegister]", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@UserTC", userTC);
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                result = reader[0].ToString();
            }

        }
        catch (Exception)
        {
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

    [System.Web.Services.WebMethod]
    public static string forgottenPassword(string email)
    {
        string result = string.Empty;

        string connectionstring = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection connection = new SqlConnection(connectionstring);
        SqlCommand command = new SqlCommand();
        SqlDataReader reader = null;

        #region Sim1-Register
        try
        {
            connection.Open();
            command = new SqlCommand("[ExamProctorProject].[dbo].[spForgottenPassword]", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Email", email);
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                result = reader[0].ToString();
            }

        }
        catch (Exception)
        {

            result = "System Error";
        }

        finally
        {
            command.Dispose();
            reader = null;
            connection.Close();
        }

        if (result == "1")
        {
            // SendMail
        }
        else if (result == "0")
        {
            result = "There is no such an email address!";
        }


        return result;
        #endregion
    }

    public static string generateActCode()
    {
        string allowedChars = "";
        allowedChars = "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z,";
        allowedChars += "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,";
        allowedChars += "1,2,3,4,5,6,7,8,9,0";
        char[] sep = { ',' };
        string[] arr = allowedChars.Split(sep);
        string passwordString = "";
        string temp = "";
        Random rand = new Random();
        for (int i = 0; i < 6; i++)
        {
            temp = arr[rand.Next(0, arr.Length)];
            passwordString += temp;
        }
        return passwordString;
    }

    public static string activationCodeControl(string activationCode)
    {
        string connectionstring = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection connection = new SqlConnection(connectionstring);
        SqlCommand command = new SqlCommand();
        SqlDataReader reader = null;
        string counter = "-1";

        #region ActCodeControl
        try
        {
            connection.Open();
            command = new SqlCommand("[ExamProctorProject].[dbo].[spActCodeControl]", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@actCode", activationCode);
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                counter = reader[0].ToString();
            }

        }
        catch (Exception ex)
        {
            counter = "-1";
        }

        finally
        {
            command.Dispose();
            reader = null;
            connection.Close();
        }

        return counter;

        #endregion
    }

    public static void insertActivationCode(string TCNumber, string activationCode, string messageId, string ipAddress, ref int result)
    {
        result = 0;
        string connectionstring = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection connection = new SqlConnection(connectionstring);
        SqlCommand command = new SqlCommand();
        #region spAddActivationCode
        try
        {
            connection.Open();
            command = new SqlCommand("[ExamProctorProject].[dbo].[spAddActivationCode]", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@TCNumber", TCNumber);
            command.Parameters.AddWithValue("@messageId", messageId);
            command.Parameters.AddWithValue("@activationCode", activationCode);
            command.Parameters.AddWithValue("@ipAddress", ipAddress);

            command.ExecuteNonQuery();
            result = 1;
        }
        catch (Exception ex)
        {
            result = -1;
        }
        finally
        {
            command.Dispose();
            connection.Close();
        }

        #endregion
    }

    public static int activationCodeDbCheck(string TCNumber, string activationCode, string ipAddress)
    {
        int counter = 0;
        string connectionstring = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection connection = new SqlConnection(connectionstring);
        SqlCommand command = new SqlCommand();
        SqlDataReader reader = null;

        #region ActCodeControl
        try
        {
            connection.Open();
            command = new SqlCommand("[ExamProctorProject].[dbo].[spActCodeCheck]", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@TCNumber", TCNumber);
            command.Parameters.AddWithValue("@activationCode", activationCode);
            command.Parameters.AddWithValue("@ipAddress", ipAddress);
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                counter = Convert.ToInt32(reader[0].ToString());
            }

        }
        catch (Exception ex)
        {
            counter = -1;
        }

        finally
        {
            command.Dispose();
            reader = null;
            connection.Close();
        }

        return counter;

        #endregion
    }
}