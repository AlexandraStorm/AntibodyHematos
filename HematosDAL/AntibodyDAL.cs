using HematosDBFacade;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Xml;

namespace HematosDAL
{
    public class AntibodyDAL : IDisposable
    {
        private DBFacade dbFac;
        private readonly string _configFile;
        protected IDBFacade _dbFacade = null;
        private readonly ExeConfigurationFileMap configFileMap = new ExeConfigurationFileMap();

        public AntibodyDAL()
        {
            try
            {
                _configFile = string.Format("{0}/{1}/External_matchit.config", Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Lifecodes");
                dbFac = new DBFacade(_configFile, true);
            }
            catch (Exception ex)
            {
                ExceptionPolicy.HandleException(ex, "Log Only Policy");
                throw;
            }
        }

        public void SetDBFacade(IDBFacade dbfacade)
        {
            _dbFacade = dbfacade;
        }

        public bool CheckCredentials(string userName, string password)
        {
            return UserDAL.IsValidPassword(userName, password);
        }

        public string GetSampleComment(string batchID, string sampleID)
        {
            try
            {
                DataSet SampleInfo = new DataSet();
                string results;
                SampleInfo = dbFac.ExecuteSQL(string.Format("SELECT comments FROM dbo.tbAntibodyMethod WHERE sessionID = '{0}' and sampleID = '{1}'", batchID, sampleID));
                if (SampleInfo != null)
                    results = (from i in SampleInfo.Tables[0].AsEnumerable()
                               select i.Field<string>("comments")).FirstOrDefault();
                else
                    results = "";

                return (SampleInfo.Tables.Count > 0) ? results : null;
            }
            catch (Exception ex)
            {
                ExceptionPolicy.HandleException(ex, "Log Only Policy");
                return null;
            }
        }
        public static List<string> GetSiteCodes()
        {
            List<string> _sitecodes = new List<string>();

            string xmlfilepath = string.Format("{0}/{1}/HematosSiteCodes.xml", Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "LIFECODES");
            try
            {
                using (FileStream fsSiteCodes = new FileStream(xmlfilepath, FileMode.Open, FileAccess.Read))
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(xmlfilepath);
                    XmlNodeList city = doc.GetElementsByTagName("City");
                    XmlNodeList sitecode = doc.GetElementsByTagName("SiteCode");

                    // Find number of cities in file
                    int NodeCount = 0;
                    using (var reader = XmlReader.Create(xmlfilepath))
                    {
                        while (reader.Read())
                        {
                            if (reader.NodeType == XmlNodeType.Element && reader.Name == "City")
                                NodeCount++;
                        }
                    }

                    for (int i = 0; i < NodeCount; i++)
                    {
                        string _cityname = null;
                        string _sitecode = null;
                        string displayList = null;

                        _cityname = city[i].InnerText;
                        _sitecode = sitecode[i].InnerText;
                        displayList = String.Format("{0} - {1}", _sitecode, _cityname);
                        _sitecodes.Add(displayList);
                    }

                }
                
            }
            catch (Exception ex)
            {
                ExceptionPolicy.HandleException(ex, "Log Only Policy");
                throw;
            }

            return _sitecodes;
        }

        public DataTable GetExportedSampleData(string batchID, string sampleID)
        {
            try
            {
                DataSet samples = new DataSet();
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("batchID", batchID);
                param.Add("sampleID", sampleID);
                samples = dbFac.ExecuteProc("GetExportedHematosAbData", param);

                return samples.Tables[0];
            }
            catch (Exception ex)
            {
                ExceptionPolicy.HandleException(ex, "Log Only Policy");
                return null;
            }
        }

        public bool Initialize()
        {
            if (_dbFacade == null)
            {
                _dbFacade = new DBFacade(); // no param ==> use the default matchit.config file
            }
            return true;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            if (dbFac != null)
            {
                dbFac.Dispose();
                dbFac = null;
            }
        }

        ~AntibodyDAL()
        {
            _dbFacade = null;
        }

        public void UpdateDB()
        {
            bool scriptExec;
            SqlConnection con = new SqlConnection(dbFac.GetSqlConnectionString());
            ServerConnection servConn = new ServerConnection(con);
            Server srv = new Server(servConn);
            string script = File.ReadAllText(String.Format(@"{0}\SQL Scripts\UpdateScript.sql", AppDomain.CurrentDomain.BaseDirectory));

            try
            {
                if (!string.IsNullOrEmpty(script))
                {
                    srv.ConnectionContext.BeginTransaction();
                    scriptExec = Convert.ToBoolean(srv.ConnectionContext.ExecuteNonQuery(script));
                    srv.ConnectionContext.CommitTransaction();
                }
                scriptExec = true;
            }
            catch (SmoException smoEx)
            {
                // error in sql call throws sqlexception and bubbles up
                // custom error
                srv.ConnectionContext.RollBackTransaction();
                bool rethrow = ExceptionPolicy.HandleException(smoEx, "Log Only Policy");
                scriptExec = false;
                if (rethrow)
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                // general non-sqlerror throws exception and bubbles up
                // custom error
                // error in sql call throws sqlexception and bubbles up
                // custom error
                srv.ConnectionContext.RollBackTransaction();
                bool rethrow = ExceptionPolicy.HandleException(ex, "Log Only Policy");
                scriptExec = false;
                if (rethrow)
                {
                    throw;
                }
            }
        }

        public List<string> GetLotIDs()
        {
            try
            {
                DataSet LotIDs = new DataSet();
                List<string> results;
                LotIDs = dbFac.ExecuteProc("GetLotsHematosAb");
                if (LotIDs != null)
                    results = (from i in LotIDs.Tables[0].AsEnumerable()
                               select i.Field<string>("LotID")).ToList();
                else
                    results = new List<string>();

                return (LotIDs.Tables.Count > 0) ? results : null;
            }
            catch (Exception ex)
            {
                 ExceptionPolicy.HandleException(ex, "Log Only Policy");
                 return null;
            }
        }

        public List<string> GetBatches(string lotid)
        {
            try
            {
                DataSet Batches = new DataSet();
                List<string> results;
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("lotid", lotid);
                Batches = dbFac.ExecuteProc("GetBatchesHematosAb", param);
                if (Batches.Tables.Count > 0)
                    results = (from b in Batches.Tables[0].AsEnumerable()
                               select b.Field<string>("BatchID")).ToList();
                else
                    results = new List<string>();

                return (Batches.Tables.Count > 0) ? results : null;
            }
            catch (Exception ex)
            {
                ExceptionPolicy.HandleException(ex, "Log Only Policy");
                return null;
            }
        }

        public DataTable GetSampleList(string BatchID, int SampleType)
        {
            try
            {
                DataSet samples = new DataSet();
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("batchID", BatchID);
                param.Add("Processed", SampleType);
                samples = dbFac.ExecuteProc("GetSamplesHematosAb", param);

                return samples.Tables[0];
            }
            catch (Exception ex)
            {
                ExceptionPolicy.HandleException(ex, "Log Only Policy");
                return null;
            }
        }

        public string ReadConfigFile(string SettingID)
        {
            configFileMap.ExeConfigFilename = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\LIFECODES\\External_HematosAb.config";
            System.Configuration.Configuration config = ConfigurationManager.OpenMappedExeConfiguration(configFileMap, ConfigurationUserLevel.None);
            return config.AppSettings.Settings[SettingID].Value;
        }

        public void WriteConfigFile(string SettingID, string SettingValue)
        {
            configFileMap.ExeConfigFilename = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\LIFECODES\\External_HematosAb.config";
            System.Configuration.Configuration config = ConfigurationManager.OpenMappedExeConfiguration(configFileMap, ConfigurationUserLevel.None);
            config.AppSettings.Settings[SettingID].Value = SettingValue;
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }

        public string[] GetLumInfo(string batchID)
        {
            DataSet ds = new DataSet();
            string[] LumInfo = new string[3]; // 0 = LumID, 1= SN, 2 = ServerName
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("batchID", batchID);
            try
            {
                ds = dbFac.ExecuteProc("GetLumInfoHematosAb", param);
                if (ds.Tables.Count > 0)
                {
                    //LumID = (from i in ds.Tables[0].AsEnumerable() select i.Field<string>("LumID")).ToString();
                    if (ds.Tables[0].Columns["LumID"] != null)
                    {
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            LumInfo[0] = row.Field<string>("LumID");
                            LumInfo[1] = row.Field<string>("SerialNumber");
                            LumInfo[2] = null;
                        }
                    }
                    else
                    {
                        if (ds.Tables[0].Rows[0]["submittedBy"].ToString().ToLower() == "auto")
                        {
                            foreach (DataRow row in ds.Tables[0].Rows)
                            {
                                LumInfo[0] = null;
                                LumInfo[1] = null;
                                LumInfo[2] = row.Field<string>("SerialNumber");
                            }
                        }
                        else
                        {
                            foreach (DataRow row in ds.Tables[0].Rows)
                            {
                                LumInfo[0] = null;
                                LumInfo[1] = row.Field<string>("SerialNumber");
                                LumInfo[2] = null;
                            }
                        }
                    }
                }
                return LumInfo;
            }
            catch (Exception ex)
            {
                ExceptionPolicy.HandleException(ex, "Log Only Policy");
                return LumInfo;
            }
        }

        public static bool CreateFile(List<string> FileData, string FileName)
        {
            try
            {
                using (StreamWriter outputFile = new StreamWriter(FileName))
                {
                    foreach (string line in FileData)
                    {
                        outputFile.WriteLine(line);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                ExceptionPolicy.HandleException(ex, "Log Only Policy");
                return false;
            }
        }

        public DataTable GetSampleInfo_ID(string BatchID, string sample)
        {
            DataSet ds = new DataSet();
            try
            {
                
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("BatchID", BatchID);
                param.Add("SampleID", sample);
                ds = dbFac.ExecuteProc("GetSampleInfo_IDHematosAb", param);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                ExceptionPolicy.HandleException(ex, "Log Only Policy");
                return ds.Tables[0];
            }            
        }

        public DataTable GetSampleInfo_SA(string BatchID, string sample)
        {
            DataSet ds = new DataSet();
            try
            {
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("BatchID", BatchID);
                param.Add("SampleID", sample);
                ds = dbFac.ExecuteProc("GetSampleInfo_SAHematosAb", param);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                ExceptionPolicy.HandleException(ex, "Log Only Policy");
                return ds.Tables[0];
            }            
        }

        public DataTable GetSampleInfo_LMX(string BatchID, string sample)
        {
            DataSet ds = new DataSet();
            try
            {
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("BatchID", BatchID);
                param.Add("SampleID", sample);
                ds = dbFac.ExecuteProc("GetSampleInfo_LMXHematosAb", param);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                ExceptionPolicy.HandleException(ex, "Log Only Policy");
                return ds.Tables[0];
            }            
        }

        public bool InsertHematosABData(string FileName, string LotID, string BatchID, string sampleID, string luminexID, string trimmedSiteCode, string SpecCode, string Comments)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("FileName", FileName);
            param.Add("LotID", LotID);
            param.Add("BatchID", BatchID);
            param.Add("SampleID", sampleID);
            param.Add("LuminexID", luminexID);
            param.Add("SiteCode", trimmedSiteCode);
            param.Add("SpecCode", SpecCode);
            param.Add("Comments", Comments);
            try
            {
                int result = Convert.ToInt32(dbFac.ExecuteScalar("InsertHematosABData", param));
                if (result == 0)
                    return true;
                else return false;
            }
            catch (Exception ex)
            {
                ExceptionPolicy.HandleException(ex, "Log Only Policy");
                return false;
            }            
        }

        public bool UpdateHematosABData(string FileName, string BatchID, string sampleID, string luminexID, string trimmedSiteCode, string SpecCode, string Comments)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("FileName", FileName);
            param.Add("BatchID", BatchID);
            param.Add("SampleID", sampleID);
            param.Add("LuminexID", luminexID);
            param.Add("SiteCode", trimmedSiteCode);
            param.Add("SpecCode", SpecCode);
            param.Add("Comments", Comments);
            try
            {
                int result = Convert.ToInt32(dbFac.ExecuteScalar("UpdateHematosABData", param));
                if (result == 0)
                    return true;
                else return false;
            }
            catch (Exception ex)
            {
                ExceptionPolicy.HandleException(ex, "Log Only Policy");
                return false;
            }
        }
    }
}