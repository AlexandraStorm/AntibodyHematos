using HematosDAL;
using System.Data;
using System;

namespace HematosBO
{
    public class LuminexDataBO : ILuminexData, IDisposable
    {
        private readonly LuminexDataDAL lumDAL = new LuminexDataDAL();

        public int LumID { get; set; }

        public string LuminexID { get; set; }

        public string LuminexServer { get; set; }

        public string SerialNumber { get; set; }

        public void Dispose()
        {
            if (lumDAL != null)
                lumDAL.Dispose();
        }

        public void loadLuminexData(DataRow luminexRow)
        {
            LumID = (int)luminexRow["LumID"];
            LuminexID = luminexRow["LuminexID"].ToString();
            LuminexServer = luminexRow["LuminexServer"].ToString();
            SerialNumber = luminexRow["SerialNumber"].ToString();
        }

        public int InsertLuminexData(string userName)
        {
            return LuminexDataDAL.InsertLuminexData(SerialNumber, LuminexID, LuminexServer, userName);
        }

        public int UpdateLuminexData(string userName)
        {
            return LuminexDataDAL.UpdateLuminexData(LumID, SerialNumber, LuminexID, LuminexServer, userName);
        }

        public bool TestLuminexServers(string servername)
        {
            if (!LuminexDataDAL.TestLumServerIS(servername))
            {
                return lumDAL.TestLumServerXP(servername);
            }

            return true;
        }

        public static DataSet RetrieveLuminexData()
        {
            return LuminexDataDAL.RetrieveLuminexData();
        }
    }
}