using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Xml;

namespace HematosDAL
{
    public class UserBO
    {
        #region "Public / Private Members"

        private int _userID;
        private string _userName;
        private string _firstName;
        private string _lastName;
        private int _roleID = 0;

        private const int USER = 5;
        private const int LABTECH = 4;
        private const int SUPERVISOR = 3;
        private const int TECHSUPPORT = 2;
        private const int ADMIN = 1;

        #endregion "Public / Private Members"

        #region "Properties"

        public int UserID
        {
            get { return _userID; }
        }

        public string UserName
        {
            get { return _userName; }
        }

        public string FirstName
        {
            get { return _firstName; }
        }

        public string LastName
        {
            get { return _lastName; }
        }

        public int RoleID
        {
            get { return _roleID; }
        }

        public Dictionary<string, bool> UserViews { get; set; }

        public bool Enabled { get; set; }

        public bool HasRightToConfirm
        {
            get { return _roleID == ADMIN || _roleID == LABTECH || _roleID == SUPERVISOR; }
        }

        #endregion "Properties"

        #region "Constructors"

        public UserBO()
        {
        }

        public UserBO(string username)
        {
            GetUser(username);
        }

        private void User(string userName)
        {
            GetUser(userName);
        }

        #endregion "Constructors"

        #region "Methods"

        public static bool IsValidPassword(string userName, string password)
        {
            return UserDAL.IsValidPassword(userName, password);
        }

        public UserBO GetInstance()
        {
            // ByVal userName As String
            //   If (_myInstance Is Nothing) Then
            string filePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\LIFECODES\\UserClassObj.xml";
            if (File.Exists(filePath))
            {
                //'Dim serializer As New SoapFormatter()
                //'Using fStream As New FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read)
                //'    _myInstance = CType(serializer.Deserialize(fStream), User)
                //'End Using
                try
                {
                    XmlDocument m_xmld = new XmlDocument();
                    XmlNodeList m_nodelist = null;
                    XmlNode m_node = null;
                    //Create the XML Document
                    //Load the Xml file
                    m_xmld.Load(filePath);
                    //Get the list of name nodes
                    _userID = int.Parse(((XmlNode)m_xmld.SelectNodes("/USER/Userid")[0]).InnerText);
                    _userName = ((XmlNode)m_xmld.SelectNodes("/USER/Username")[0]).InnerText;
                    _firstName = ((XmlNode)m_xmld.SelectNodes("/USER/Firstname")[0]).InnerText;
                    _lastName = ((XmlNode)m_xmld.SelectNodes("/USER/Lastname")[0]).InnerText;
                    _roleID = int.Parse(((XmlNode)m_xmld.SelectNodes("/USER/Roleid")[0]).InnerText);
                    Enabled = bool.Parse(((XmlNode)m_xmld.SelectNodes("/USER/Enable")[0]).InnerText);
                    //Loop through the nodes
                    m_nodelist = m_xmld.SelectNodes("/USER/Userview");
                    UserViews = new Dictionary<string, bool>();
                    foreach (XmlNode m_node_loopVariable in m_nodelist)
                    {
                        m_node = m_node_loopVariable;
                        UserViews.Add(m_node.ChildNodes[0].InnerText, bool.Parse(m_node.ChildNodes[1].InnerText));
                    }
                }
                catch (Exception errorVariable)
                {
                    //Error trapping
                    Console.Write(errorVariable.ToString());
                }
            }
            // End If

            return this;
        }

        public bool IsValidPassword(string password)
        {
            return UserDAL.IsValidPassword(_userName, password);
        }

        private void GetUser(string userName)
        {
            DataSet userDS = UserDAL.RetrieveUser(userName);

            if (userDS.Tables[0].Rows.Count > 0)
            {
                _userID = int.Parse(userDS.Tables[0].Rows[0]["UserID"].ToString());
                _firstName = userDS.Tables[0].Rows[0]["FirstName"].ToString();
                _lastName = userDS.Tables[0].Rows[0]["LastName"].ToString();
                _userName = userDS.Tables[0].Rows[0]["UserName"].ToString();
                _roleID = int.Parse(userDS.Tables[0].Rows[0]["RoleID"].ToString());
                Enabled = bool.Parse(userDS.Tables[0].Rows[0]["Enabled"].ToString());

                UserViews = new Dictionary<string, bool>();
                foreach (DataRow row in userDS.Tables[1].Rows)
                {
                    if (!UserViews.ContainsKey(row["ViewName"].ToString()))
                        UserViews.Add(row["ViewName"].ToString(), bool.Parse(row["Visibilty"].ToString()));
                }
            }
        }

        #endregion "Methods"

        // To detect redundant calls
        private bool disposedValue = false;

        // IDisposable
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: free other state (managed objects).
                    UserViews.Clear();
                    UserViews = null;
                }
                GC.Collect();
                // TODO: free your own state (unmanaged objects).
                // TODO: set large fields to null.
            }
            disposedValue = true;
        }

        #region " IDisposable Support "

        // This code added by Visual Basic to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion " IDisposable Support "
    }
}