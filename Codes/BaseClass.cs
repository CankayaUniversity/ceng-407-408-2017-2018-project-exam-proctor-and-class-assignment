using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.IO;
using System.Xml;
namespace library
{
    public class BaseClass : System.Web.UI.Page
    {
        public BaseClass()
        {

        }
        #region non static
        #region Configuration

        public string sConfigurationXml
        {
            get
            {
                if (Session["sConfigurationXml"] == null)
                {
                    FileStream fs = new FileStream(GetWebConfigParameter("XmlConfigFilePath"), FileMode.Open);
                    StreamReader sr = new StreamReader(fs);
                    Session["sConfigurationXml"] = sr.ReadToEnd();
                    sr.Close();
                    fs.Close();
                    return sConfigurationXml;
                }
                return ((string)Session["sConfigurationXml"]);
            }
            set { Session["sConfigurationXml"] = value; }
        }
        public string GetXmlConfigParameter(string KeyName)
        {
            string retVal = null;
            try
            {
                XmlDocument xdoc = new XmlDocument();
                xdoc.Load(GetWebConfigParameter("XmlConfigFilePath"));

                XmlNode node = xdoc.DocumentElement.SelectSingleNode(string.Format("parameter[@key='{0}']", KeyName));

                retVal = node.Attributes["value"].Value;
            }
            catch (Exception)
            {
                return null;

            }

            return retVal;
        }
        protected string GetWebConfigParameter(string KeyName)
        {
            return System.Configuration.ConfigurationSettings.AppSettings[KeyName];
        }
        public string GetXmlConfigParameter(string Category, string KeyName)
        {
            try
            {
                XmlDocument xdoc = new XmlDocument();

                if (GetWebConfigParameter("LoadConfigurationXmlOnce") == "1")
                    xdoc.LoadXml(sConfigurationXml);
                else
                    xdoc.Load(GetWebConfigParameter("XmlConfigFilePath"));

                XmlNodeList nodes = null;
                if (string.IsNullOrEmpty(KeyName))
                    nodes = xdoc.DocumentElement.SelectNodes("parameter[@category='" + Category + "']");
                else
                    nodes = xdoc.DocumentElement.SelectNodes("parameter[@category='" + Category + "'][@key='" + KeyName + "']");

                return nodes.Item(0).Attributes["value"].Value;
            }
            catch (Exception)
            {
                return null;
            }
        }

        #endregion
        #region Log
        public  void LogMessage(string message, string foldername, string filename)
        {
            string error = string.Empty;
            try
            {

                string LogFilePath = GetXmlConfigParameter("LogFilePath"); ;
                string UseLog = GetXmlConfigParameter("UseLog");


                if (LogFilePath == null || UseLog == null || UseLog == "0" || !Directory.Exists(LogFilePath))
                    return;

                LogFilePath += string.Format("\\{0}", DateTime.Now.ToString("dd.MM.yyyy"));
                LogFilePath += string.Format("\\{0}", foldername);

                try
                {
                    if (!Directory.Exists(LogFilePath))
                        Directory.CreateDirectory(LogFilePath);
                }
                catch
                {
                    return;
                }

                string filePath = string.Format("{0}\\{1}.txt", LogFilePath, filename);
                StreamWriter _StreamWriter = null;
                try
                {

                    _StreamWriter = !File.Exists(filePath) ? File.CreateText(filePath) : File.AppendText(filePath);
                    string strMsg = string.Format("{0} \t {1}", DateTime.Now.ToString("HH:mm:ss:ff"), message);
                    _StreamWriter.WriteLine(strMsg);
                }
                catch (Exception ex)
                { error = ex.Message.ToString(); }
                finally
                {
                    if (_StreamWriter != null)
                    {
                        _StreamWriter.Flush();
                        _StreamWriter.Close();
                        _StreamWriter = null;
                    }
                }
            }
            catch (Exception ex)
            { error = ex.Message.ToString(); }

        }
        #endregion
        #endregion

        #region static
        #region Configuration

      
        public static string GetXmlConfigParameterStatic(string KeyName)
        {
            string retVal = null;
            try
            {
                XmlDocument xdoc = new XmlDocument();
                xdoc.Load(GetWebConfigParameterStatic("XmlConfigFilePath"));

                XmlNode node = xdoc.DocumentElement.SelectSingleNode(string.Format("parameter[@key='{0}']", KeyName));

                retVal = node.Attributes["value"].Value;
            }
            catch (Exception)
            {
                return null;

            }

            return retVal;
        }
        protected static string GetWebConfigParameterStatic(string KeyName)
        {
            return System.Configuration.ConfigurationSettings.AppSettings[KeyName];
        }
        public static string GetXmlConfigParameterStatic(string Category, string KeyName)
        {
            try
            {
                XmlDocument xdoc = new XmlDocument();

                if (GetWebConfigParameterStatic("LoadConfigurationXmlOnce") == "1")
                    xdoc.LoadXml(HttpContext.Current.Session["sConfigurationXml"].ToString() );
                else
                    xdoc.Load(GetWebConfigParameterStatic("XmlConfigFilePath"));

                XmlNodeList nodes = null;
                if (string.IsNullOrEmpty(KeyName))
                    nodes = xdoc.DocumentElement.SelectNodes("parameter[@category='" + Category + "']");
                else
                    nodes = xdoc.DocumentElement.SelectNodes("parameter[@category='" + Category + "'][@key='" + KeyName + "']");

                return nodes.Item(0).Attributes["value"].Value;
            }
            catch (Exception)
            {
                return null;
            }
        }

        #endregion
        #region Log
        public static void LogMessageStatic(string message, string foldername, string filename)
        {
            string error = string.Empty;
            try
            {

                string LogFilePath = GetXmlConfigParameterStatic("LogFilePath"); ;
                string UseLog = GetXmlConfigParameterStatic("UseLog");


                if (LogFilePath == null || UseLog == null || UseLog == "0" || !Directory.Exists(LogFilePath))
                    return;

                LogFilePath += string.Format("\\{0}", DateTime.Now.ToString("dd.MM.yyyy"));
                LogFilePath += string.Format("\\{0}", foldername);

                try
                {
                    if (!Directory.Exists(LogFilePath))
                        Directory.CreateDirectory(LogFilePath);
                }
                catch
                {
                    return;
                }

                string filePath = string.Format("{0}\\{1}.txt", LogFilePath, filename);
                StreamWriter _StreamWriter = null;
                try
                {

                    _StreamWriter = !File.Exists(filePath) ? File.CreateText(filePath) : File.AppendText(filePath);
                    string strMsg = string.Format("{0} \t {1}", DateTime.Now.ToString("HH:mm:ss:ff"), message);
                    _StreamWriter.WriteLine(strMsg);
                }
                catch (Exception ex)
                { error = ex.Message.ToString(); }
                finally
                {
                    if (_StreamWriter != null)
                    {
                        _StreamWriter.Flush();
                        _StreamWriter.Close();
                        _StreamWriter = null;
                    }
                }
            }
            catch (Exception ex)
            { error = ex.Message.ToString(); }

        }
        #endregion
        #endregion

        #region Sessions
        #region Int_Sessions
        public int sLoginControl
        {
            get
            {
                if (Session["sLoginControl"] == null)
                {
                    return 0;
                }
                else
                {
                    return ((int)Session["sLoginControl"]);
                }
            }
            set { Session["sLoginControl"] = value; }
        }
       

        public int sUserTypeID
        {
            get
            {
                if (Session["sUserTypeID"] == null)
                {
                    return 0;
                }
                else
                {
                    return ((int)Session["sUserTypeID"]);
                }
            }
            set
            {
                Session["sUserTypeID"] = value;
            }
        }
        public int sStudentNo
        {
            get
            {
                if (Session["sStudentNo"] == null)
                {
                    return 0;
                }
                else
                {
                    return ((int)Session["sStudentNo"]);
                }
            }
            set
            {
                Session["sStudentNo"] = value;
            }
        }
        public int sFacultyID
        {
            get
            {
                if (Session["sFacultyID"] == null)
                {
                    return 0;
                }
                else
                {
                    return ((int)Session["sFacultyID"]);
                }
            }
            set
            {
                Session["sFacultyID"] = value;
            }
        }
        public int sDepartmentID
        {
            get
            {
                if (Session["sDepartmentID"] == null)
                {
                    return 0;
                }
                else
                {
                    return ((int)Session["sDepartmentID"]);
                }
            }
            set
            {
                Session["sDepartmentID"] = value;
            }
        }
        public int sClassDeg
        {
            get
            {
                if (Session["sClassDeg"] == null)
                {
                    return 0;
                }
                else
                {
                    return ((int)Session["sClassDeg"]);
                }
            }
            set
            {
                Session["sClassDeg"] = value;
            }
        }
        #endregion
        #region String_Sessions
        public string sTCNumber
        {
            get
            {
                if (Session["sTCNumber"] == null)
                {
                    return string.Empty;
                }
                else
                {
                    return ((string)Session["sTCNumber"]);
                }
            }
            set { Session["sTCNumber"] = value; }
        }
        public string sIpAddress
        {
            get
            {
                if (Session["sIpAddress"] == null)
                {
                    return string.Empty;
                }
                else
                {
                    return ((string)Session["sIpAddress"]);
                }
            }
            set { Session["sIpAddress"] = value; }
        }
        public string sTitle
        {
            get
            {
                if (Session["sTitle"] == null)
                {
                    return string.Empty;
                }
                else
                {
                    return ((string)Session["sTitle"]);
                }
            }
            set
            {
                Session["sTitle"] = value;
            }
        }
        #endregion
        #endregion
        public string sqlVal(string val, string delimeter)
        {
            if (val == null || val == "") return "NULL";

            val = val.Replace("'", "''");
            return delimeter + val + delimeter;
        }

        public static string sqlValStatic(string val, string delimeter)
        {
            if (val == null || val == "") return "NULL";

            val = val.Replace("'", "''");
            return delimeter + val + delimeter;
        }

    }
}

