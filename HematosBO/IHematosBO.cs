using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using HematosDAL;

namespace HematosBO
{
    public interface IHematosBO
    {
        bool DatabaseReady();
        void SetDAL(IAntibodyDAL dal); // to enable dependency injection
        bool ValidUserCredentials(string userName, string password);
        List<string> RetriveSiteCodes();
        List<string> RetriveLotIDs();
        string CheckForDefaultSiteCode();
        List<string> GetBatches(string LotID);
        List<string> GetSampleList(string BatchID, int tableID);

        string ReadSavePath();

        void SaveFilePath(string FilePath);

        string RetriveLumID(string batchID);

        void SaveSiteCode(string SiteCode);

        bool ExportData(string trimmedSiteCode, string SelectedLotID, string BatchID, List<string> SelectedSamples, string FilePath);
    }
}
