using HematosDAL;
using HematosViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace HematosBO
{
    public class HematosBO : IHematosBO, IDisposable
    {
        private AntibodyDAL antibodyDAL = new AntibodyDAL();
        private AntibodyDAL IantibodyDAL;
        private bool _databaseOK;

        public HematosBO()
        {
            InitializeDAL();
        }

        private void InitializeDAL()
        {
            if (antibodyDAL.Initialize())
            {
                antibodyDAL.UpdateDB();
                _databaseOK = true;
            }
            else
            {
                _databaseOK = false;
            }
        }

        public List<string> RetriveSiteCodes()
        {
            return AntibodyDAL.GetSiteCodes();
        }

        public List<string> RetriveLotIDs()
        {
            return antibodyDAL.GetLotIDs();
        }

        // enables dependency injection
        public void SetDAL(AntibodyDAL dal)
        {
            IantibodyDAL = dal;
            InitializeDAL();
        }

        public bool DatabaseReady()
        {
            return _databaseOK;
        }

        public bool ValidUserCredentials(string userName, string password)
        {
            return UserDAL.IsValidPassword(userName, password);
        }

        public List<string> GetBatches(string LotID)
        {
            return antibodyDAL.GetBatches(LotID);
        }

        public List<SampleVM> GetSampleList(string BatchID, int SampleType)
        {
            List<SampleVM> sampleList = new List<SampleVM>();            

            DataTable results = antibodyDAL.GetSampleList(BatchID, SampleType);

            if (results.Rows.Count > 0)
            {
               foreach (DataRow row in results.Rows)
               {
                   SampleVM sample = new SampleVM() { batchID = row["batchID"].ToString(), sampleID = row["sampleID"].ToString(), CheckSpec = (int)row["CheckSpecification"] };
                   sampleList.Add(sample);
               }
            }

            return sampleList;
        }

        public SampleVM GetOldSampleData(SampleVM sample)
        {
            DataTable results = antibodyDAL.GetExportedSampleData(sample.batchID, sample.sampleID);

            if (results.Rows.Count > 0)
            {
                foreach (DataRow row in results.Rows)
                {
                    sample.specification = row["SpecCode"].ToString();
                    sample.comment = row["Comments"].ToString();
                    sample.useSpec = true;
                }
            }

            return sample;
        }

        public SampleVM CheckSampleComments(SampleVM sample)
        {
            string comment = antibodyDAL.GetSampleComment(sample.batchID, sample.sampleID);

            if (string.IsNullOrEmpty(comment))
            {
                sample.useSpec = false;
                sample.CheckComment = true;
                sample.comment = null;
            }
            else
            {
                string spec = CheckComment(comment);

                if (!string.IsNullOrEmpty(spec))
                {
                    sample.specification = spec;
                    sample.useSpec = true;
                }
                else
                {
                    sample.comment = spec;
                    sample.useSpec = false;
                    sample.CheckComment = true;
                }               
            }

            return sample;
        }

        public string ReadSavePath()
        {
            return antibodyDAL.ReadConfigFile("ExportSaveFilePath");
        }

        public void SaveFilePath(string FilePath)
        {
            antibodyDAL.WriteConfigFile("ExportSaveFilePath", FilePath);
        }

        public void SaveSiteCode(string SiteCode)
        {
            antibodyDAL.WriteConfigFile("DefaultSiteCode", SiteCode);
        }

        public string CheckForDefaultSiteCode()
        {
            return antibodyDAL.ReadConfigFile("DefaultSiteCode");
        }

        public string[] RetriveLumInfo(string batchID)
        {
            string[] LuminexInfo = antibodyDAL.GetLumInfo(batchID);
            if (String.IsNullOrEmpty(LuminexInfo[0]))
            {
                LuminexInfo[0] = "Luminex ID Not Found";
                return LuminexInfo;
                // return null;
            }
            else
                return LuminexInfo;
        }

        /*
        File Layout for SA and ID
        Create File - file name: XXLUMX02SABIDyyyymmddhhnnss.csv XX= site code
        Line 1 Site Code Luminex ID (only once per file)
        Line 2 Sample Info: Sample ID, Approved username, Lot ID, completed username, completed date (euro date), comments
        Line 3: Test Method code and Result
        Line 4: Testing Kit Codes
        Line 5: Antibody Class Codes
        Line 6-n: Specificity Test Code: if pos: F8100, 'Antigen', 'MFI'
         */

        public bool ExportData_ID(string trimmedSiteCode, string selectedLotID, string batchID, List<SampleVM> selectedSamples, string filePath, string abClassCode, string luminexID, bool newSample)
        {
            List<string> fileData = new List<string>();
            string fileName = FileNameSAID(filePath, trimmedSiteCode, luminexID);
            fileData.Add(trimmedSiteCode + luminexID);

            foreach (SampleVM sample in selectedSamples)
            {
                DataTable SampleInfo = antibodyDAL.GetSampleInfo_ID(batchID, sample.sampleID);
                if (SampleInfo.Rows.Count >= 1)
                {
                    string completedBy = SampleInfo.Rows[0]["completedBy"].ToString();
                    string completedDt = SampleInfo.Rows[0]["completedDt"].ToString();
                    string approvedBy = SampleInfo.Rows[0]["approvedBy"].ToString();
                    string comments;
                    if (sample.useSpec == false)
                    {
                        comments = SampleInfo.Rows[0]["comments"].ToString();
                    }
                    else
                        comments = sample.comment;

                    string Line2 = String.Format("{0}, {1}, {2}, {3}, {4}, {5}", sample.sampleID, approvedBy, selectedLotID, completedBy, completedDt, comments);
                    fileData.Add(Line2);

                    if (sample.useSpec)
                    {
                        if (selectedLotID.Contains("LM1"))
                        {
                            if (sample.specification == "U" || sample.specification == "MULTI")
                            {
                                fileData.Add("F7006, P");
                                fileData.Add("F8003, LIFEIDI");
                                fileData.Add("F8001, " + abClassCode);
                            }
                            else if (sample.specification == "I" || sample.specification == "UU")
                            {
                                fileData.Add("F7006, U");
                                fileData.Add("F8003, LIFEIDI");
                                fileData.Add("F8001, " + abClassCode);
                            }
                            else
                            {
                                fileData.Add(String.Format("F7006, {0}", sample.specification));
                                fileData.Add("F8003, LIFEIDI");
                                fileData.Add("F8001, " + abClassCode);
                            }
                        }
                        else
                        {
                            if (sample.specification == "U" || sample.specification == "MULTI")
                            {
                                fileData.Add("F7007, P");
                                fileData.Add("F8003, LIFEIDII");
                                fileData.Add("F8001, " + abClassCode);
                            }
                            else if (sample.specification == "I" || sample.specification == "UU")
                            {
                                fileData.Add("F7007, U");
                                fileData.Add("F8003, LIFEIDII");
                                fileData.Add("F8001, " + abClassCode);
                            }
                            else
                            {
                                fileData.Add(String.Format("F7007, {0}", sample.specification));
                                fileData.Add("F8003, LIFEIDII");
                                fileData.Add("F8001, " + abClassCode);
                            }
                        }
                        
                        if (sample.specification == "N")
                        {
                            fileData.Add("F8100, NEG, 0");
                        }
                        else if (sample.specification == "UU")
                        {
                            fileData.Add("F8100, U, 0");
                        }
                        else
                        {
                            fileData.Add(String.Format("F8100, {0}, 0", sample.specification));
                        }                        
                    }
                    else
                    {

                        if (SampleInfo.Columns.Contains("Antigen"))
                        {
                            if (selectedLotID.Contains("LM1"))
                            {
                                fileData.Add("F7006, P");
                                fileData.Add("F8003, LIFEIDI");
                                fileData.Add("F8001, " + abClassCode);
                            }
                            else
                            {
                                fileData.Add("F7007, P");
                                fileData.Add("F8003, LIFEIDII");
                                fileData.Add("F8001, " + abClassCode);
                            }

                            foreach (DataRow row in SampleInfo.Rows)
                            {
                                string ag = row.Field<string>("Antigen").ToUpper();

                                if (ag == "A6601" || ag == "A6602")
                                {
                                    ag = "A66";
                                }

                                fileData.Add(String.Format("F8100, {0}, {1}", ag, row.Field<int>("Strength")));
                            }
                        }
                        else
                        {
                            if (selectedLotID.Contains("LM1"))
                            {
                                fileData.Add("F7006, N");
                                fileData.Add("F8003, LIFEIDI");
                                fileData.Add("F8001, " + abClassCode);
                            }
                            else
                            {
                                fileData.Add("F7007, N");
                                fileData.Add("F8003, LIFEIDII");
                                fileData.Add("F8001, " + abClassCode);
                            }

                            fileData.Add("F8100, NEG, 0");
                        }
                    }
                }
            }

            if (newSample == true)
            {
                if (AntibodyDAL.CreateFile(fileData, fileName))
                {
                    return InsertExportHistoryTable(trimmedSiteCode, selectedLotID, batchID, selectedSamples, fileName, luminexID);
                }
                return false;
            }
            else
            {
                if (AntibodyDAL.CreateFile(fileData, fileName))
                {
                    return UpdateExportHistoryTable(trimmedSiteCode, batchID, selectedSamples, fileName, luminexID);
                }
                return false;
            }
        }

        public bool ExportData_SA(string trimmedSiteCode, string selectedLotID, string batchID, List<SampleVM> selectedSamples, string filePath, string abClassCode, string luminexID, bool newSample)
        {
            LociRulesDAL lociRulesDAL = new LociRulesDAL();
            List<string> fileData = new List<string>();
            string fileName = FileNameSAID(filePath, trimmedSiteCode, luminexID);
            fileData.Add(trimmedSiteCode + luminexID);
            List<HematosSerology> nbstSerology;
            NHSBTSerology nHSBT = new NHSBTSerology();
            nHSBT.LoadFile();
            nbstSerology = nHSBT.alleleList;
            DataSet rules = lociRulesDAL.GetRulesforUsage();
            foreach (SampleVM sample in selectedSamples)
            {
                DataTable SampleInfo = antibodyDAL.GetSampleInfo_SA(batchID, sample.sampleID);
                if (SampleInfo.Rows.Count >= 1)
                {
                    string completedBy = SampleInfo.Rows[0]["completedBy"].ToString();
                    string completedDt = SampleInfo.Rows[0]["completedDt"].ToString();
                    string approvedBy = SampleInfo.Rows[0]["approvedBy"].ToString();
                    string comments;
                    if (sample.useSpec == false)
                    {
                        comments = SampleInfo.Rows[0]["comments"].ToString();
                    }
                    else
                        comments = sample.comment;

                    string Line2 = String.Format("{0}, {1}, {2}, {3}, {4}, {5}", sample.sampleID, approvedBy, selectedLotID, completedBy, completedDt, comments);
                    fileData.Add(Line2);

                    if (sample.useSpec)
                    {                        
                        if (selectedLotID.Contains("SA1"))
                        {
                            if (sample.specification == "U" || sample.specification == "MULTI")
                            {
                                fileData.Add("F7025, P");
                                fileData.Add("F8003, LIFESAI");
                                fileData.Add("F8001, " + abClassCode);
                            }
                            else if  (sample.specification == "I" || sample.specification == "UU")
                            {
                                fileData.Add("F7025, U");
                                fileData.Add("F8003, LIFESAI");
                                fileData.Add("F8001, " + abClassCode);
                            }
                            else
                            {
                                fileData.Add(String.Format("F7025, {0}", sample.specification));
                                fileData.Add("F8003, LIFESAI");
                                fileData.Add("F8001, " + abClassCode);
                            }                            
                        }
                        else
                        {
                            if (sample.specification == "U" || sample.specification == "MULTI")
                            {
                                fileData.Add("F7026, P");
                                fileData.Add("F8003, LIFESAII");
                                fileData.Add("F8001, " + abClassCode);
                            }
                            else if (sample.specification == "I" || sample.specification == "UU")
                            {
                                fileData.Add("F7026, U");
                                fileData.Add("F8003, LIFESAII");
                                fileData.Add("F8001, " + abClassCode);
                            }
                            else
                            {
                                fileData.Add(String.Format("F7026, {0}", sample.specification));
                                fileData.Add("F8003, LIFESAII");
                                fileData.Add("F8001, " + abClassCode);
                            }                            
                        }

                        if (sample.specification == "N")
                        {
                            fileData.Add("F8100, NEG, 0");
                        }
                        else if (sample.specification == "UU")
                        {
                            fileData.Add("F8100, U, 0");
                        }
                        else
                        {
                            fileData.Add(String.Format("F8100, {0}, 0", sample.specification));
                        }  
                    }
                    else
                    {

                        if (SampleInfo.Columns.Contains("antigen"))
                        {
                            if (selectedLotID.Contains("SA1"))
                            {
                                fileData.Add("F7025, P");
                                fileData.Add("F8003, LIFESAI");
                                fileData.Add("F8001, " + abClassCode);
                            }
                            else
                            {
                                fileData.Add("F7026, P");
                                fileData.Add("F8003, LIFESAII");
                                fileData.Add("F8001, " + abClassCode);
                            }
                            if (selectedLotID.Contains("SA1"))
                            {
                                var sampleserology = SampleInfo.AsEnumerable()
                                    .Where(x => x.Field<string>("assignment") == "Positive")
                                    .Select(r => new
                                    {
                                        Serology = r.Field<string>("Serology"),
                                    }).Distinct().ToList();
                                List<string> OneOnly = new List<string>();
                                List<string> ManyOnly = new List<string>();
                                foreach (dynamic d in sampleserology)
                                {
                                    var neg = SampleInfo.AsEnumerable()
                                        .Where(x => x.Field<string>("Serology") == d.Serology && x.Field<string>("assignment") == "Negative").ToList();
                                    var totals = SampleInfo.AsEnumerable()
                                        .Where(x => x.Field<string>("Serology") == d.Serology).ToList();
                                    if (neg.Count > 0)
                                    {
                                        if (Math.Abs(neg.Count - totals.Count) == 1)
                                        {
                                            OneOnly.Add(neg[0].Field<string>("Serology"));
                                        }
                                        else
                                        {
                                            ManyOnly.Add(neg[0].Field<string>("Serology"));
                                        }
                                    }
                                }
                                foreach (string x in ManyOnly)
                                {
                                    if (sampleserology.Contains(new { Serology = x }))
                                    {
                                        sampleserology.Remove(new { Serology = x });
                                    }
                                }
                                foreach (string x in OneOnly)
                                {
                                    if (sampleserology.Contains(new { Serology = x }))
                                    {
                                        sampleserology.Remove(new { Serology = x });
                                    }
                                }
                                List<AppliedRules> ruleset = new List<AppliedRules>();
                                foreach (dynamic d in sampleserology)
                                {
                                    var query1 = (from assign1 in SampleInfo.AsEnumerable()
                                                  where assign1.Field<string>("Serology") == d.Serology &&
                                                  assign1.Field<string>("assignment") == "Positive"
                                                  select new AppliedRules
                                                  {
                                                      allelename = assign1.Field<string>("antigen"),
                                                      serologyValue = d.Serology,
                                                      allelicType = assign1.Field<string>("SELECTALL"),
                                                      meanValue = assign1.Field<string>("USEALL"),
                                                      allelesortorder = assign1.Field<int>("serologysortorder")
                                                  }).Distinct().ToList();
                                    ruleset.Add(query1[0]);
                                }
                                foreach (string c in ManyOnly)
                                {
                                    var query2 = (from assign2 in SampleInfo.AsEnumerable()
                                                  where assign2.Field<string>("Serology") == c &&
                                                  assign2.Field<string>("assignment") == "Positive"
                                                  select new AppliedRules
                                                  {
                                                      allelename = assign2.Field<string>("antigen"),
                                                      serologyValue = c,
                                                      allelicType = assign2.Field<string>("SELECTMANY"),
                                                      meanValue = assign2.Field<string>("USEMANY"),
                                                      allelesortorder = assign2.Field<int>("antigensortorder")
                                                  }).ToList();
                                    foreach (AppliedRules a in query2)
                                    {
                                        ruleset.Add(a);
                                    }
                                }
                                foreach (string b in OneOnly)
                                {
                                    var query3 = (from assign3 in SampleInfo.AsEnumerable()
                                                  where assign3.Field<string>("Serology") == b &&
                                                  assign3.Field<string>("assignment") == "Positive"
                                                  select new AppliedRules
                                                  {
                                                      allelename = assign3.Field<string>("antigen"),
                                                      serologyValue = b,
                                                      allelicType = assign3.Field<string>("SELECTONE"),
                                                      meanValue = assign3.Field<string>("USEONE"),
                                                      allelesortorder = assign3.Field<int>("antigensortorder")
                                                  }).ToList();
                                    foreach (AppliedRules a in query3)
                                    {
                                        ruleset.Add(a);
                                    }
                                }
                                var ruleset1 = ruleset.OrderBy(c => c.serologyValue.Replace("*","")).ThenBy(c => c.allelesortorder);
                                string currentValues = string.Empty;
                                foreach (AppliedRules r in ruleset1.ToList())
                                {
                                    
                                    if(r.allelicType.ToUpper() == "SEROLOGY")
                                    {
                                        var rowData = (from assign in SampleInfo.AsEnumerable()
                                                       where assign.Field<string>("Serology") == r.serologyValue
                                                       group assign by assign.Field<string>("Serology") into g
                                                       select new
                                                       {
                                                           mfi = g.Average(x => x.Field<decimal>("rawValue")),
                                                           adjust1 = g.Average(x => x.Field<int?>("adjust1"))
                                                       }).ToList();
                                        var useVal = r.meanValue == "Raw Value" ? (double)(Math.Round(rowData[0].mfi,0)) : Math.Round((double)rowData[0].adjust1,0);                                        
                                        //make sure we are creating double values.
                                        if(currentValues != $"{r.serologyValue},{useVal}")
                                        {
                                            fileData.Add(String.Format("F8100, {0}, {1}", r.serologyValue.ToUpper(), useVal)); //Cw to CW
                                        }
                                        currentValues = $"{r.serologyValue},{useVal}";
                                    }
                                    else
                                    {
                                        var rowData = (from assign in SampleInfo.AsEnumerable()
                                                       where assign.Field<string>("antigen") == r.allelename
                                                       select new
                                                       {
                                                           mfi = assign.Field<decimal>("rawValue"),
                                                           adjust1 = assign.Field<int?>("adjust1")
                                                       }).ToList();
                                        var useVal = r.meanValue == "Raw Value" ? (double)(Math.Round(rowData[0].mfi, 0)) : Math.Round((double)rowData[0].adjust1, 0);
                                        fileData.Add(String.Format("F8100, {0}, {1}", r.allelename.ToUpper(), useVal)); //Cw to CW
                                    }
                                }
                            }
                            else
                            {
                                //
                                var sampleserology = SampleInfo.AsEnumerable()
                                    .Where(x => x.Field<string>("assignment") == "Positive" &&  x.Field<int>("tail") == 1)
                                    .Select(r => new
                                    {
                                        Serology = r.Field<string>("Serology").Trim(),
                                    }).Distinct().ToList();
                                List<string> OneOnly = new List<string>();
                                List<string> ManyOnly = new List<string>();
                                foreach (dynamic d in sampleserology)
                                {
                                    var neg = SampleInfo.AsEnumerable()
                                        .Where(x => x.Field<string>("Serology") == d.Serology && x.Field<int>("tail") == 0 && x.Field<string>("assignment") == "Negative").ToList();
                                    var totals = SampleInfo.AsEnumerable()
                                        .Where(x => x.Field<string>("Serology") == d.Serology).ToList();

                                    if (neg.Count > 0)
                                    {
                                        if (Math.Abs(neg.Count - totals.Count) == 1)
                                        {
                                            OneOnly.Add(neg[0].Field<string>("Serology"));
                                        }
                                        else
                                        {
                                            ManyOnly.Add(neg[0].Field<string>("Serology"));
                                        }
                                    }
                                }
                                foreach (string x in ManyOnly)
                                {
                                    if (sampleserology.Contains(new { Serology = x }))
                                    {
                                        sampleserology.Remove(new { Serology = x });
                                    }
                                }
                                foreach (string x in OneOnly)
                                {
                                    if (sampleserology.Contains(new { Serology = x }))
                                    {
                                        sampleserology.Remove(new { Serology = x });
                                    }
                                }
                                List<AppliedRules> ruleset = new List<AppliedRules>();
                                foreach (dynamic d in sampleserology)
                                {
                                    var query1 = (from assign1 in SampleInfo.AsEnumerable()
                                                  where assign1.Field<string>("Serology") == d.Serology &&
                                                  assign1.Field<string>("assignment") == "Positive" &&
                                                  assign1.Field<int>("tail") == 1
                                                  select new AppliedRules
                                                  {
                                                      allelename = assign1.Field<string>("antigen"),
                                                      serologyValue = d.Serology,
                                                      allelicType = assign1.Field<string>("SELECTALL"),
                                                      meanValue = assign1.Field<string>("USEALL")
                                                  }).Distinct().ToList();
                                    ruleset.Add(query1[0]);
                                }
                                foreach (string c in ManyOnly)
                                {
                                    var query2 = (from assign2 in SampleInfo.AsEnumerable()
                                                  where assign2.Field<string>("Serology") == c &&
                                                 assign2.Field<string>("assignment") == "Positive" &&
                                                  assign2.Field<int>("tail") == 1
                                                  select new AppliedRules
                                                  {
                                                      allelename = assign2.Field<string>("antigen"),
                                                      serologyValue = c,
                                                      allelicType = assign2.Field<string>("SELECTMANY"),
                                                      meanValue = assign2.Field<string>("USEMANY")
                                                  }).ToList();
                                    foreach (AppliedRules a in query2)
                                    {
                                        ruleset.Add(a);
                                    }
                                }
                                foreach (string b in OneOnly)
                                {
                                    var query3 = (from assign3 in SampleInfo.AsEnumerable()
                                                  where assign3.Field<string>("Serology") == b &&
                                                  assign3.Field<string>("assignment") == "Positive" &&
                                                  assign3.Field<int>("tail") == 1
                                                  select new AppliedRules
                                                  {
                                                      allelename = assign3.Field<string>("antigen"),
                                                      serologyValue = b,
                                                      allelicType = assign3.Field<string>("SELECTONE"),
                                                      meanValue = assign3.Field<string>("USEONE")
                                                  }).ToList();
                                    foreach (AppliedRules a in query3)
                                    {
                                        ruleset.Add(a);
                                    }
                                }
                                List<AppliedRules> DQ = (from x in ruleset where (x.serologyValue.Substring(0, 2) == "DQ") select x).ToList();
                                List<AppliedRules> DP = (from x in ruleset where (x.serologyValue.Substring(0, 2) == "DP") select x).ToList();
                                List<AppliedRules> DR = (from x in ruleset where (x.serologyValue.Substring(0, 2) == "DR") select x).ToList();
                                var dqSort = DQ.OrderBy(x => x.serologyValue).ToList();
                                var drSort = DR.OrderBy(x => x.serologyValue).ToList();
                                var dpSort = DP.OrderBy(x => x.serologyValue).ToList();
                                List<AppliedRules> ruleset1 = dpSort;
                                ruleset1.AddRange(dqSort);
                                ruleset1.AddRange(drSort);
                                string currentValues = string.Empty;
                                List<string> completealleles = new List<string>();
                                foreach (AppliedRules r in ruleset1.ToList())
                                {
                                    if (r.allelicType.ToUpper() == "SEROLOGY")
                                    {
                                        var rowData = (from assign in SampleInfo.AsEnumerable()
                                                       where assign.Field<string>("Serology") == r.serologyValue
                                                       group assign by assign.Field<string>("Serology") into g
                                                       select new
                                                       {
                                                           mfi = g.Average(x => x.Field<decimal>("rawValue")),
                                                           adjust1 = g.Average(x => x.Field<int?>("ADJ"))
                                                       }).ToList();
                                        var useVal = r.meanValue == "Raw Value" ? (double)(Math.Round(rowData[0].mfi, 0)) : Math.Round((double)rowData[0].adjust1, 0);
                                        //make sure we are creating double values.
                                        if (currentValues != $"{r.serologyValue},{useVal}")
                                        {
                                            fileData.Add(String.Format("F8100, {0}, {1}", r.serologyValue.ToUpper(), useVal)); //Cw to CW
                                        }
                                        currentValues = $"{r.serologyValue},{useVal}";
                                    }
                                    else
                                    {
                                        if (!completealleles.Contains(r.allelename))
                                        {
                                            var rowData = (from assign in SampleInfo.AsEnumerable()
                                                           where assign.Field<string>("antigen") == r.allelename
                                                           group assign by assign.Field<string>("antigen") into x
                                                           select new
                                                           {
                                                               adjust1 = x.Average(s => s.Field<int?>("ADJ")),
                                                               mfi = x.Average(s => s.Field<decimal>("rawValue"))
                                                               
                                                           }).ToList();
                                            var useVal = r.meanValue == "Raw Value" ? (double)(Math.Round(rowData[0].mfi, 0)) : Math.Round((double)rowData[0].adjust1, 0);
                                            fileData.Add(String.Format("F8100, {0}, {1}", r.allelename.ToUpper(), useVal)); //Cw to CW
                                            completealleles.Add(r.allelename);
                                        }
                                        
                                    }

                                }
                            }                           
                        }
                        else
                        {
                            if (selectedLotID.Contains("SA1"))
                            {
                                fileData.Add("F7025, N");
                                fileData.Add("F8003, LIFESAI");
                                fileData.Add("F8001, " + abClassCode);
                            }
                            else
                            {
                                fileData.Add("F7026, N");
                                fileData.Add("F8003, LIFESAII");
                                fileData.Add("F8001, " + abClassCode);
                            }

                            fileData.Add("F8100, NEG, 0");
                        }
                    }
                }
            }

            if (newSample == true)
            {
                if (AntibodyDAL.CreateFile(fileData, fileName))
                {
                    return InsertExportHistoryTable(trimmedSiteCode, selectedLotID, batchID, selectedSamples, fileName, luminexID);
                }
                return false;
            }
            else
            {
                if (AntibodyDAL.CreateFile(fileData, fileName))
                {
                    return UpdateExportHistoryTable(trimmedSiteCode, batchID, selectedSamples, fileName, luminexID);
                }
                return false;
            }
        }
        private List<AppliedRules> sortresults(List<AppliedRules> current)
        {
            List<AppliedRules> SortedAlleleList = new List<AppliedRules>();
            var ruleArray = current.ToArray();
            AlphanumComparatorFast comparer = new AlphanumComparatorFast();
            Array.Sort(ruleArray, comparer);
            SortedAlleleList = ruleArray.ToList();
            return SortedAlleleList;
        }
        private static string FileNameSAID(string FilePath, string SiteCode, string luminexID)
        {
            DateTime now = DateTime.Now;
            string timeStamp = now.ToString("yyyyMMddHHmmss"); // YYYYMMDDHHmmss
            string _filename = String.Format("{0}{1}SABID{2}.txt", SiteCode, luminexID, timeStamp);

            return String.Format("{0}\\{1}", FilePath, _filename);
        }

        /*LMX File Format
        Create File - file name: XXLUMX02Screenyyyymmddhhnnss.csv XX= site code
        Line 1 Site Code Luminex ID (only once per file)
        Line 2 Sample Info - Class I - Sample ID, F70021, approved username, LOT ID, completed username, completed date (euro date), comments
        Line 3 Sample Info - Class II - Sample ID, F70022, approved username, LOT ID, completed username, completed date (euro date), comments
         */

        public bool ExportData_LMX(string trimmedSiteCode, string selectedLotID, string batchID, List<SampleVM> selectedSamples, string filePath, string luminexID, bool newSample)
        {
            List<string> fileData = new List<string>();
            string fileName = FileNameLMX(filePath, trimmedSiteCode, luminexID);
            fileData.Add(trimmedSiteCode + luminexID);

            foreach (SampleVM sample in selectedSamples)
            {
                 DataTable SampleInfo = antibodyDAL.GetSampleInfo_LMX(batchID, sample.sampleID);
                 if (SampleInfo.Rows.Count >= 1)
                 {
                     string ClassIResults = ConvertResultString(SampleInfo.Rows[0]["ClassIResults"].ToString());
                     string ClassIIResults = ConvertResultString(SampleInfo.Rows[0]["ClassIIResults"].ToString());           
                     string completedBy = SampleInfo.Rows[0]["completedBy"].ToString();
                     string completedDt = SampleInfo.Rows[0]["completedDt"].ToString();
                     string approvedBy = SampleInfo.Rows[0]["approvedBy"].ToString();
                     string comments = SampleInfo.Rows[0]["comments"].ToString();

                     string ClassILine2 = String.Format("{0},F70021,{1},{2},{3},{4},{5},{6}", sample.sampleID, ClassIResults, approvedBy, selectedLotID, completedBy, completedDt, comments);
                     fileData.Add(ClassILine2);
                     string ClassIILine3 = String.Format("{0},F70022,{1},{2},{3},{4},{5},{6}", sample.sampleID, ClassIIResults, approvedBy, selectedLotID, completedBy, completedDt, comments);
                     fileData.Add(ClassIILine3);
                 }
            }

            if (newSample == true)
            {
                if (AntibodyDAL.CreateFile(fileData, fileName))
                {
                    return InsertExportHistoryTable(trimmedSiteCode, selectedLotID, batchID, selectedSamples, fileName, luminexID);
                }
                return false;
            }
            else
            {
                if (AntibodyDAL.CreateFile(fileData, fileName))
                {
                    return UpdateExportHistoryTable(trimmedSiteCode, batchID, selectedSamples, fileName, luminexID);
                }
                return false;
            }
        }

        public string ConvertResultString(string Results)
        {
            switch(Results.ToLower())
            {
                case "positive":
                    return "P";                
                case "invalid":
                    return "I";               
                case "multispecific":
                    return "MULTI";              
                case "unspecified":
                    return "U";                
                case "unidentified":
                    return "UU";
                case "unspecified (p)":
                    return "U";
                case "unspecified (u)":
                    return "UU";
                case "not tested":
                    return "NT";               
                default:
                    return "N";
            }
        }

        private string CheckComment(string comment)
        {
            switch (comment.ToLower().Trim())
            {
                case "i":
                    return "I";
                case "multi":
                    return "MULTI";
                case "u":
                    return "U";
                case "uu":
                    return "UU";
                case "nt":
                    return "NT";
                default:
                    return null;  // This will indicate that there is either a real comment here or an error not matching the result.
            }
        }

        private bool UpdateExportHistoryTable(string trimmedSiteCode, string batchID, List<SampleVM> selectedSamples, string fileName, string luminexID)
        {
            bool result = false;
            foreach (SampleVM sample in selectedSamples)
            {
                result = antibodyDAL.UpdateHematosABData(fileName, batchID, sample.sampleID, luminexID, trimmedSiteCode, sample.specification, sample.comment);
            }
            return result;
        }

        private bool InsertExportHistoryTable(string trimmedSiteCode, string selectedLotID, string batchID, List<SampleVM> selectedSamples, string fileName, string luminexID)
        {
            bool result = false;
            foreach (SampleVM sample in selectedSamples)
            {
                result = antibodyDAL.InsertHematosABData(fileName, selectedLotID, batchID, sample.sampleID, luminexID, trimmedSiteCode, sample.specification, sample.comment);
            }
            return result;
        }

        private static string FileNameLMX(string FilePath, string SiteCode, string luminexID)
        {
            DateTime now = DateTime.Now;
            string timeStamp = now.ToString("yyyyMMddHHmmss"); // YYYYMMDDHHmmss
            string _filename = String.Format("{0}{1}Screen{2}.txt", SiteCode, luminexID, timeStamp);

            return String.Format("{0}\\{1}", FilePath, _filename);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            if (antibodyDAL != null)
            {
                antibodyDAL.Dispose();
                antibodyDAL = null;
            }
        }
    }

    internal class AppliedRules
    {
        internal string serologyValue;
        internal string allelicType;
        internal string meanValue;
        internal string allelename;
        internal int serologysortOrder;
        internal int allelesortorder;
    }
}