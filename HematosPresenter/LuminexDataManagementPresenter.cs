using DevExpress.XtraGrid.Views.Grid;
using HematosBO;
using System;
using System.Data;
using System.Reflection;

namespace HematosPresenter
{
    public class LuminexDataManagementPresenter
    {
        private ILuminexDataManagement _view;
        private bool updateLum;

        public LuminexDataManagementPresenter(ILuminexDataManagement view)
        {
            _view = view;

            SetLabels();
            LoadLuminexDataGrid();
            _view.gridRowSelection += luminexGridRowSelection;
        }

        private void SetLabels()
        {
            System.Resources.ResourceManager myManager = new System.Resources.ResourceManager("HematosPresenter.Resources.LuminexDataManagementResource", Assembly.GetExecutingAssembly());
            _view.btnClear = myManager.GetString("ButtonClear");
            _view.btnSave = myManager.GetString("ButtonSave");
            _view.lblSerialNum = myManager.GetString("LabelSerialNumber");
            _view.lblLuminexID = myManager.GetString("LabelLuminexID");
            _view.lblLuminexServer = myManager.GetString("LabelLuminexServer");
            _view.FormTitle = myManager.GetString("FormTitle");
            _view.btnSelect = myManager.GetString("ButtonSelect");
        }

        private void LoadLuminexDataGrid()
        {
            dynamic luminexDataList = new LuminexDataListBO();
            DataSet LumDS = new DataSet();

            LumDS = luminexDataList.LoadLuminexData();
            _view.gridLuminexData.DataSource = null;
            _view.gridLuminexData.DataSource = LumDS.Tables[0];

            GridView myGridViewTmp = new GridView();
            myGridViewTmp = (GridView)_view.gridLuminexData.MainView;
            myGridViewTmp.Columns["LumID"].Visible = false;
            updateLum = false;
        }

        public void ClearLuminexDataForm()
        {
            var _with1 = _view;
            updateLum = false;
            //_with1.txtLumID.Enabled = true;
            //_with1.txtLumID.Text = "";
            _with1.txtLuminexID.Text = string.Empty;
            _with1.txtLuminexServer.Text = string.Empty;
            _with1.txtSerialNumber.Text = string.Empty;
        }

        public void CheckLumXMLfile(string filepath)
        {
            System.IO.FileInfo file = new System.IO.FileInfo(filepath);
            System.Xml.XmlDocument ServerListXML = null;
            System.Xml.XmlDeclaration ServerListXMLDeclaration = null;
            System.Xml.XmlElement ServerListXMLRoot = null;

            if (file.Exists == false)
            {
                ServerListXML = new System.Xml.XmlDocument();

                ServerListXMLDeclaration = ServerListXML.CreateXmlDeclaration("1.0", null, string.Empty);
                ServerListXML.InsertBefore(ServerListXMLDeclaration, ServerListXML.DocumentElement);
                ServerListXMLRoot = ServerListXML.CreateElement("ServerName");
                ServerListXML.InsertAfter(ServerListXMLRoot, ServerListXMLDeclaration);
                ServerListXML.Save(filepath);
            }
            else
            {
                ServerListXML = new System.Xml.XmlDocument();
                try
                {
                    ServerListXML.Load(filepath);
                }
                catch (Exception)
                {
                }
                if (string.IsNullOrEmpty(ServerListXML.InnerXml))
                {
                    ServerListXMLDeclaration = ServerListXML.CreateXmlDeclaration("1.0", null, string.Empty);
                    ServerListXML.InsertBefore(ServerListXMLDeclaration, ServerListXML.DocumentElement);
                    ServerListXMLRoot = ServerListXML.CreateElement("ServerName");
                    ServerListXML.InsertAfter(ServerListXMLRoot, ServerListXMLDeclaration);
                    ServerListXML.Save(filepath);
                }
            }
            file.Refresh();
            file = null;
        }

        public int SaveLuminexData()
        {
            LuminexDataBO luminex = new LuminexDataBO();

            luminex.SerialNumber = _view.txtSerialNumber.Text;
            luminex.LuminexID = _view.txtLuminexID.Text;
            luminex.LuminexServer = _view.txtLuminexServer.Text;

            int result = 0;
            if (updateLum == false)
            {
                if (luminex.TestLuminexServers(_view.txtLuminexServer.Text))
                {
                    result = luminex.InsertLuminexData("Supervisor");
                }
                else
                {
                    return -4;
                }
            }
            else
            {
                int[] selRows = ((GridView)_view.gridLuminexData.MainView).GetSelectedRows();
                DataRowView selRow = (DataRowView)((GridView)_view.gridLuminexData.MainView).GetRow(selRows[0]);
                luminex.LumID = Convert.ToInt32(selRow["LumID"]);

                result = luminex.UpdateLuminexData("Supervisor");
            }

            UpdateGrid();
            luminex = null;
            return result;
        }

        //private void SaveXMLLuminexServerList(string server)
        //{
        //    string XMLPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\Lifecodes\\XMLLumServerList.XML";
        //    bool NoFileOrData = false;

        //    try
        //    {
        //        CheckLumXMLfile(XMLPath);
        //        if (System.IO.File.Exists(XMLPath))
        //        {
        //            System.Xml.XmlReader rd = System.Xml.XmlReader.Create(XMLPath);
        //            //check for data... if none then try again to get lumserver data
        //            rd.MoveToContent();
        //            if (string.IsNullOrEmpty(rd.Value))
        //            {
        //                NoFileOrData = true;
        //            }
        //            rd.Close();
        //        }
        //        else
        //        {
        //            //go out and get lumserver data
        //            NoFileOrData = true;
        //        }
        //        if (NoFileOrData)
        //        {
        //            LuminexDataBO luminex = new LuminexDataBO();
        //            if ((luminex.TestLuminexServers(server)))
        //            {
        //                //_DBMgmtBO = new DBMgmtBO();
        //                //_DBMgmtBO.Servers = _autoBatchStatusBO.Servers
        //                //_DBMgmtBO.createXMLServerList(XMLPath, server);
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {
        //    }
        //}

        //private void UpdateXMLLuminexServerList(string server)
        //{
        //    string XMLPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\Lifecodes\\XMLLumServerList.XML";
        //    LuminexDataBO Lumdal = new LuminexDataBO();
        //    System.Xml.XmlDocument ServerListXML = new System.Xml.XmlDocument();
        //    System.Xml.XmlDeclaration ServerListXMLDeclaration = ServerListXML.CreateXmlDeclaration("1.0", null, string.Empty);
        //    System.Xml.XmlElement ServerListXMLRoot = null;
        //    DataSet ds = Lumdal.RetrieveLuminexData();
        //    CheckLumXMLfile(XMLPath);

        //    ServerListXML.InsertBefore(ServerListXMLDeclaration, ServerListXML.DocumentElement);
        //    ServerListXMLRoot = ServerListXML.CreateElement("ServerName");
        //    ServerListXML.InsertAfter(ServerListXMLRoot, ServerListXMLDeclaration);

        //    foreach (DataRow row in ds.Tables[0].Rows)
        //    {
        //        System.Xml.XmlNode root = ServerListXML.SelectSingleNode("ServerName");
        //        System.Xml.XmlNode servernametag = ServerListXML.CreateNode(System.Xml.XmlNodeType.Element, "Server", "");
        //        servernametag.InnerText = row[3].ToString();
        //        root.AppendChild(servernametag);
        //    }
        //    ServerListXML.Save(XMLPath);
        //}

        private void UpdateGrid()
        {
            LoadLuminexDataGrid();
            _view.gridLuminexData.RefreshDataSource();
        }

        private void luminexGridRowSelection(object sender, EventArgs e)
        {
            int[] selRows = ((GridView)_view.gridLuminexData.MainView).GetSelectedRows();
            DataRowView selRow = (DataRowView)((GridView)_view.gridLuminexData.MainView).GetRow(selRows[0]);

            //_view.txtLumID.Enabled = false;

            //_view.txtLumID.Text = selRow["LumID"].ToString();
            _view.txtSerialNumber.Text = selRow["SerialNumber"].ToString();
            _view.txtLuminexID.Text = selRow["LuminexID"].ToString();
            _view.txtLuminexServer.Text = selRow["LuminexServer"].ToString();
            updateLum = true;
        }
    }
}