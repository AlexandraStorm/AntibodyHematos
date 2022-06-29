using System;
using System.Collections.Generic;
using System.IO;
using System.Data;
using LumenWorks.Framework.IO.Csv;

namespace HematosBO
{
    public class HematosSerology
    {
        public string NHSBTCODE { get; set; }
        public string Description { get; set; }
        public string Allelic { get; set; }
        public string Serology { get; set; }
    }

    public class NHSBTSerology
    {
        public List<HematosSerology> alleleList { get; set; }

        public NHSBTSerology()
        {
            alleleList = new List<HematosSerology>();
        }
        public void LoadFile()
        {
            var csvTable = new DataTable();
            using (var csvReader = new CsvReader(new StreamReader(System.IO.File.OpenRead(String.Format(@"{0}\serology\Active HLA Antibody.csv", AppDomain.CurrentDomain.BaseDirectory))), true))
            {
                csvTable.Load(csvReader);
            }
            for (int i = 0; i < csvTable.Rows.Count; i++)
            {
                alleleList.Add(new HematosSerology {
                    NHSBTCODE = csvTable.Rows[i][0].ToString(), Description = csvTable.Rows[i][1].ToString(), Allelic = csvTable.Rows[i][2].ToString(),
                    Serology = csvTable.Rows[i][3].ToString()
                });
            }
            csvTable.Dispose();
        }
    }
}
