using HematosDAL;
using HematosViewModel;
using System.Collections.Generic;

namespace HematosBO
{
    public interface IHematosBO
    {
        bool DatabaseReady();

        void SetDAL(AntibodyDAL dal); // to enable dependency injection

        bool ValidUserCredentials(string userName, string password);

        List<string> RetriveSiteCodes();

        List<string> RetriveLotIDs();

        string CheckForDefaultSiteCode();

        List<string> GetBatches(string LotID);

        List<SampleVM> GetSampleList(string BatchID, int SampleType);

        SampleVM CheckSampleComments(SampleVM sample);
        SampleVM GetOldSampleData(SampleVM sample);
        string ConvertResultString(string specCode);
        string ReadSavePath();

        void SaveFilePath(string FilePath);

        string[] RetriveLumInfo(string batchID);

        void SaveSiteCode(string SiteCode);

        bool ExportData_LMX(string trimmedSiteCode, string SelectedLotID, string BatchID, List<SampleVM> SelectedSamples, string FilePath, string luminexID, bool NewSample);

        bool ExportData_SA(string trimmedSiteCode, string SelectedLotID, string BatchID, List<SampleVM> SelectedSamples, string FilePath, string AbClassCode, string luminexID, bool NewSample);

        bool ExportData_ID(string trimmedSiteCode, string SelectedLotID, string BatchID, List<SampleVM> SelectedSamples, string FilePath, string AbClassCode, string luminexID, bool NewSample);
    }
}