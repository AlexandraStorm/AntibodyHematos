using HematosBO;
using HematosDAL;
using HematosPresenter;
using NUnit.Framework;
using System.Collections.Generic;
using System;
using System.Reflection;
using HematosViewModel;
using System.Data;

namespace UnitTestProject
{
    [TestFixture]
    public class UnitTest : IDisposable
    {
        protected IAntibodyDAL antibodyDAL;
        protected IHematosBO hematosBO;
        protected MainPresenter _presenter;
        protected IMainView _IMainView;
        private string _siteCode;

        public TestContext TestContext { get; set; }

        public void Dispose()
        {
            if (_presenter != null)
            {
                _presenter.Dispose();
                _presenter = null;
            }
        }

        [SetUp]
        public void MyClassInitialize()
        {
            _presenter = new MainPresenter(_IMainView);
            antibodyDAL = new AntibodyDAL();
            hematosBO = new HematosBO.HematosBO();

            antibodyDAL.Initialize();

        }

        // Use ClassCleanup to run code after all tests in a class have run
        [TearDown]
        public void MyClassCleanup()
        {
            antibodyDAL = null;
            hematosBO = null;
        }

        [Test, Category("Database Check")]
        public void DatabaseCheck()
        {
            Assert.IsTrue(hematosBO.DatabaseReady());
        }

        [Test, Category("Load Site Codes")]
        public void LoadSiteCodes()
        {
            List<string> SiteCodes = _presenter.LoadSiteCodeComboBx();
            Assert.AreEqual(6, SiteCodes.Count);
        }

        [Test, Category("Load Site Codes")]
        public void LoadSiteCodes_withDefault()
        {
            hematosBO.SaveSiteCode("JL");
            Assert.AreEqual("JL", _presenter.CheckForDefaultSiteCode());
        }

        [Test, Category("Load Site Codes")]
        public void RetriveSiteCodes()
        {
            List<string> SiteCodes = hematosBO.RetriveSiteCodes();
            Assert.AreEqual(6, SiteCodes.Count);
        }

        //[Test, Category("Load Site Codes")]
        //public void TrimSiteCode()
        //{
        //    _IMainView.SiteCode = "Tooting - T1";
        //    Assert.AreEqual("T1", _presenter.TrimSiteCode());
        //}


        [Test, Category("Load Lot IDs")]
        public void LoadLotIDs()
        {
            List<string> LotIDs = _presenter.LoadLotIDs();
            Assert.AreEqual(86, LotIDs.Count);
        }

        [Test, Category("Load Lot IDs")]
        public void RetriveLotIDs()
        {
            List<string> LotIDs = hematosBO.RetriveLotIDs();
            Assert.AreEqual(86, LotIDs.Count);
        }

        [Test, Category("Load Batches")]
        public void LoadBatches()
        {
            List<string> BatchIDs = _presenter.GetBatches("09203A 06143B-SA1");
            Assert.AreEqual(39, BatchIDs.Count);
        }

        [Test, Category("Get Unprocessed Samples")]
        public void GetSamplesUnprocessed()
        {
            DataTable samplelist = antibodyDAL.GetSampleList("ASHI_LSA2_051612", 0);
            Assert.AreEqual(3, samplelist.Rows.Count);
        }

        [Test, Category("Get Processed Samples")]
        public void GetSamplesProcessed()
        {
            DataTable samplelist = antibodyDAL.GetSampleList("ASHI_LSA2_051612", 1);
            Assert.AreEqual(0, samplelist.Rows.Count);
        }

        [Test, Category("Load Luminex Info")]
        public void LoadLumID()
        {
            string[] LumInfo = hematosBO.RetriveLumInfo("ASHI_LSA2_051612");
            Assert.AreEqual("01", LumInfo[0]);
        }

        [Test, Category("Load Luminex Info")]
        public void LoadLumID_fail()
        {
            string[] LumInfo = hematosBO.RetriveLumInfo("110318LMXC");
            Assert.AreEqual("Luminex ID Not Found", LumInfo[0]);
        }

        [Test, Category("Load Luminex Info")]
        public void LoadLumSN()
        {
            string[] LumInfo = hematosBO.RetriveLumInfo("ASHI_LSA2_051612");
            Assert.AreEqual("LX10001180006R", LumInfo[1]);
        }

        [Test, Category("Load Luminex Info")]
        public void LoadLumSN_fail()
        {
            string[] LumInfo = hematosBO.RetriveLumInfo(null);
            Assert.AreEqual(null, LumInfo[1]);
        }

        [Test, Category("Drop Folder Path")]
        public void SetandRetrieveDropFolderLocation()
        {
            const string exportloc = "C:\\Temp";
            hematosBO.SaveFilePath(exportloc);
            Assert.AreEqual(exportloc, hematosBO.ReadSavePath());
        }

        [Test, Category("User Login")]
        public void CheckCredentialsTest_Fail()
        {
            Assert.IsFalse(antibodyDAL.CheckCredentials("supervisor", "lifematch"));
        }

        [Test, Category("User Login")]
        public void CheckCredentialsTest_Passed()
        {
            Assert.IsTrue(antibodyDAL.CheckCredentials("supervisor", "lifecodes"));
        }

        [Test, Category("Export Data SA")]
        public void ExportData_SA_new_POS()
        {
            string SelectedLotID = "06164B 06184J-SA1";
            string selectedBatchID = "20141211LSA1 etude immucor";
            string sitecode = "J1";
            bool newsample = true;
            string exportPath = "C:\\Temp";
            string AbClassCode = "G";
            string lumID = "Test01";
            List<SampleVM> sampleList = new List<SampleVM>();
            sampleList.Add(new SampleVM() { sampleID = "22222" });

            Assert.IsTrue(hematosBO.ExportData_SA(sitecode, SelectedLotID, selectedBatchID, sampleList, exportPath, AbClassCode, lumID, newsample));
        }

        [Test, Category("Export Data SA")]
        public void ExportData_SA_new_Neg()
        {
            string SelectedLotID = "06164B 06184J-SA1";
            string selectedBatchID = "20141211LSA1 etude immucor";
            string sitecode = "J1";
            bool newsample = true;
            string exportPath = "C:\\Temp";
            string AbClassCode = "G";
            string lumID = "Test01";
            List<SampleVM> sampleList = new List<SampleVM>();
            sampleList.Add(new SampleVM() { sampleID = "00000" });

            Assert.IsTrue(hematosBO.ExportData_SA(sitecode, SelectedLotID, selectedBatchID, sampleList, exportPath, AbClassCode, lumID, newsample));
        }

        [Test, Category("Export Data SA")]
        public void ExportData_SA_NotNewSample()
        {
            string SelectedLotID = "06164B 06184J-SA1";
            string selectedBatchID = "20141211LSA1 etude immucor";
            string sitecode = "J1";
            bool newsample = false;
            string exportPath = "C:\\Temp";
            string AbClassCode = "G";
            string lumID = "Test01";
            List<SampleVM> sampleList = new List<SampleVM>();
            sampleList.Add(new SampleVM() { sampleID = "00000" });

            Assert.IsTrue(hematosBO.ExportData_SA(sitecode, SelectedLotID, selectedBatchID, sampleList, exportPath, AbClassCode, lumID, newsample));
        }

        [Test, Category("Export Data ID")]
        public void ExportData_ID_new_POS()
        {
            string SelectedLotID = "3001831 3001819-LM1";
            string selectedBatchID = "20141211lm1 etude immucor";
            string sitecode = "J1";
            bool newsample = true;
            string exportPath = "C:\\Temp";
            string AbClassCode = "G";
            string lumID = "Test01";
            List<SampleVM> sampleList = new List<SampleVM>();
            sampleList.Add(new SampleVM() { sampleID = "22222" });

            Assert.IsTrue(hematosBO.ExportData_SA(sitecode, SelectedLotID, selectedBatchID, sampleList, exportPath, AbClassCode, lumID, newsample));
        }

        [Test, Category("Export Data ID")]
        public void ExportData_ID_new_Neg()
        {
            string SelectedLotID = "3001831 3001819-LM1";
            string selectedBatchID = "20141211lm1 etude immucor";
            string sitecode = "J1";
            bool newsample = true;
            string exportPath = "C:\\Temp";
            string AbClassCode = "G";
            string lumID = "Test01";
            List<SampleVM> sampleList = new List<SampleVM>();
            sampleList.Add(new SampleVM() { sampleID = "00000" });

            Assert.IsTrue(hematosBO.ExportData_SA(sitecode, SelectedLotID, selectedBatchID, sampleList, exportPath, AbClassCode, lumID, newsample));
        }

        [Test, Category("Export Data ID")]
        public void ExportData_ID_NotNewSample()
        {
            string SelectedLotID = "3001831 3001819-LM1";
            string selectedBatchID = "20141211lm1 etude immucor";
            string sitecode = "J1";
            bool newsample = false;
            string exportPath = "C:\\Temp";
            string AbClassCode = "G";
            string lumID = "Test01";
            List<SampleVM> sampleList = new List<SampleVM>();
            sampleList.Add(new SampleVM() { sampleID = "00000" });

            Assert.IsTrue(hematosBO.ExportData_SA(sitecode, SelectedLotID, selectedBatchID, sampleList, exportPath, AbClassCode, lumID, newsample));
        }

        [Test, Category("Export Data LMX")]
        public void ExportData_LMX_NEW()
        {
            string SelectedLotID = "3001955 3001891-LMX";
            string selectedBatchID = "2014 12 04  lmx";
            string sitecode = "J1";
            bool newsample = true;
            string exportPath = "C:\\Temp";
            string AbClassCode = "G";
            string lumID = "Test01";
            List<SampleVM> sampleList = new List<SampleVM>();
            sampleList.Add(new SampleVM() { sampleID = "Baniene 12 04" });
           
            Assert.IsTrue(hematosBO.ExportData_SA(sitecode, SelectedLotID, selectedBatchID, sampleList, exportPath, AbClassCode, lumID, newsample));
        }

        [Test, Category("Export Data LMX")]
        public void ExportData_LMX_OLD()
        {
            string SelectedLotID = "3001955 3001891-LMX";
            string selectedBatchID = "2014 12 04  lmx";
            string sitecode = "J1";
            bool newsample = false;
            string exportPath = "C:\\Temp";
            string AbClassCode = "G";
            string lumID = "Test01";
            List<SampleVM> sampleList = new List<SampleVM>();
            sampleList.Add(new SampleVM() { sampleID = "Baniene 12 04" });

            Assert.IsTrue(hematosBO.ExportData_SA(sitecode, SelectedLotID, selectedBatchID, sampleList, exportPath, AbClassCode, lumID, newsample));
        }
    }
}