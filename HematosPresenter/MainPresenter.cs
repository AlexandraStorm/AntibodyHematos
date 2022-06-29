using HematosBO;
using HematosViewModel;
using System;
using System.Collections.Generic;
using System.Data;

namespace HematosPresenter
{
    public class MainPresenter : IDisposable
    {
        private readonly IMainView _view;
        private readonly IHematosBO IhematosBO;
        public List<string> LumNames;
        public DataSet SiteCodes;
        public bool _dbOK = false;

        public MainPresenter(IMainView view)
        {
            _view = view;
            IhematosBO = new HematosBO.HematosBO();
            if (!IhematosBO.DatabaseReady())
            {
                _dbOK = false;
            }
            else
            {
                _dbOK = true;
            }
        }

        public bool VaildateUser(string UserName, string Password)
        {
            return IhematosBO.ValidUserCredentials(UserName, Password);
        }

        public List<string> LoadSiteCodeComboBx()
        {
            return IhematosBO.RetriveSiteCodes();
        }

        public List<string> LoadLotIDs()
        {
            return IhematosBO.RetriveLotIDs();
        }

        public string CheckForDefaultSiteCode()
        {
            return IhematosBO.CheckForDefaultSiteCode();
        }

        public List<string> GetBatches(string LotID)
        {
            return IhematosBO.GetBatches(LotID);
        }

        public void GetSamples()
        {
            if (_view.selectedBatch != null)
            {
                _view.SampleList = IhematosBO.GetSampleList(_view.selectedBatch, _view.filterSelection);
                string[] LuminexInfo = IhematosBO.RetriveLumInfo(_view.selectedBatch);
                _view.LumID = LuminexInfo[0];
                _view.LumSN = LuminexInfo[1];
                _view.LumServer = LuminexInfo[2];
            }
        }

        public void ReadSavePath()
        {
            _view.SaveFilePath = IhematosBO.ReadSavePath();
        }

        public void SetSavePath()
        {
            IhematosBO.SaveFilePath(_view.SaveFilePath);
        }

        public void SaveSiteCode()
        {
            string trimmedSiteCode = TrimSiteCode();
            IhematosBO.SaveSiteCode(trimmedSiteCode);
        }

        private string TrimSiteCode()
        {
            string[] trimmed = _view.SiteCode.Split('-');
            return trimmed[0].Trim();
        }

        public bool ExportData(string SelectedLotID, string AbClassCode, string LuminexID, bool NewSample)
        {
            string trimmedSiteCode = TrimSiteCode();
            if (SelectedLotID.Contains("SA"))
            {
                if (IhematosBO.ExportData_SA(trimmedSiteCode, SelectedLotID, _view.selectedBatch, _view.SelectedSamples, _view.SaveFilePath, AbClassCode, LuminexID, NewSample))
                    return true;
                return false;
            }
            else if (SelectedLotID.Contains("LM2Q") || SelectedLotID.Contains("LM1"))
            {
                if (IhematosBO.ExportData_ID(trimmedSiteCode, SelectedLotID, _view.selectedBatch, _view.SelectedSamples, _view.SaveFilePath, AbClassCode, LuminexID, NewSample))
                    return true;
                return false;
            }
            else
            {
                if (IhematosBO.ExportData_LMX(trimmedSiteCode, SelectedLotID, _view.selectedBatch, _view.SelectedSamples, _view.SaveFilePath, LuminexID, NewSample))
                    return true;
                return false;
            }
        }

        public SampleVM CheckSampleComments(SampleVM sample)
        {
            return IhematosBO.CheckSampleComments(sample);
        }
        public string ConvertCode(string sampleSpec)
        {
            return IhematosBO.ConvertResultString(sampleSpec);
        }
        public SampleVM RetrieveSavedSampleData(SampleVM sample)
        {
            return IhematosBO.GetOldSampleData(sample);
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
            if (SiteCodes != null)
            {
                SiteCodes.Dispose();
                SiteCodes = null;
            }
        }
    }
}