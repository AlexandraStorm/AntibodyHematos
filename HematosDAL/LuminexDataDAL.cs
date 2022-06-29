using HematosDBFacade;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using System;
using System.Collections.Generic;
using System.Data;
using System.ServiceModel;

namespace HematosDAL
{
    public class LuminexDataDAL : IDisposable
    {
        private ServiceReference1.EvolutionBatchListenerClient _listenerClient;

        public void Dispose()
        {
        }

        public static DataSet RetrieveLuminexData()
        {
            DBFacade dbFac = new DBFacade();
            return dbFac.ExecuteProc("GetLuminexData");
        }

        public static int UpdateLuminexData(int lumID, string serialNumber, string luminexID, string luminexServer, string updatedBy)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("LumID", lumID);
            param.Add("SerialNumber", serialNumber);
            param.Add("LuminexID", luminexID);
            param.Add("LuminexServer", luminexServer);
            param.Add("UpdatedBy", updatedBy);
            DBFacade dbFac = new DBFacade();
            return Convert.ToInt32(dbFac.ExecuteScalar("UpdateLuminexData", param));
        }

        public static int InsertLuminexData(string serialNumber, string luminexID, string luminexServer, string updatedBy)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("SerialNumber", serialNumber);
            param.Add("LuminexID", luminexID);
            param.Add("LuminexServer", luminexServer);
            param.Add("UpdatedBy", updatedBy);
            DBFacade dbFac = new DBFacade();
            return Convert.ToInt32(dbFac.ExecuteScalar("InsertLuminexData", param));
        }

        public static bool TestLumServerIS(string servername)
        {
            Server srv = new Server(sqlConnection(servername, "sa", "assay"));
            try
            {
                foreach (Database dbCurr in srv.Databases)
                {
                    DatabaseList dbProperties = new DatabaseList();
                    if (dbCurr.Name == "IVD")
                    {
                        dbProperties.Name = dbCurr.Name;
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionPolicy.HandleException(ex, "Log Only Policy");
            }

            return false;
        }

        public bool TestLumServerXP(string servername)
        {
            string tcpip = GetTCPIP(servername);
            try
            {
                _listenerClient = new ServiceReference1.EvolutionBatchListenerClient("BasicHttpBinding_IEvolutionBatchListener", new EndpointAddress(String.Format("http://{0}:7575/ClassLibrary1", tcpip)));
                bool isAvailable = _listenerClient.CheckXponentStatus();
                return isAvailable;
            }
            catch (Exception ex)
            {
                ExceptionPolicy.HandleException(ex, "Log Only Policy");
            }
            return false;
        }

        private static string GetTCPIP(string servername)
        {
            string ipAddress = string.Empty;
            int iIP = 0;
            try
            {
                System.Net.IPHostEntry ipE = System.Net.Dns.GetHostEntry(servername);
                System.Net.IPAddress[] ipA = ipE.AddressList;

                for (iIP = 0; iIP <= ipA.GetUpperBound(0); iIP++)
                {
                    if (ipA[iIP].AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        ipAddress = ipA[iIP].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionPolicy.HandleException(ex, "Log Only Policy");
            }

            return ipAddress;
        }

        private static ServerConnection sqlConnection(string server, string user, string password)
        {
            ServerConnection sqlConn = new ServerConnection() { ConnectTimeout = 5, ServerInstance = server, LoginSecure = false, Login = user, Password = password };
            return sqlConn;
        }
    }
}