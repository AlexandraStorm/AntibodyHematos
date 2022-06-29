using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HematosBO;
using HematosViewModel;
namespace HematosPresenter
{
    public class LociRulesPresenter
    {
        private Interfaces.ILociRules myView;
        public LociRulesPresenter(Interfaces.ILociRules _lociView)
        {
            myView = _lociView;
        }

        public List<LociData> LoadData()
        {
            List<LociData> lociDatas = new List<LociData>();
            LociRulesBO lrBO = new LociRulesBO();
            foreach (DataRow dr in lrBO.GetRules().Rows)
            {
                lociDatas.Add(new LociData
                {
                    locus = dr["loci"].ToString(),
                    Serology = dr["serology"].ToString(),
                    Allelic = dr["allelename"].ToString(),
                    selectedAllelicAll = dr["selectedAllelicAll"].ToString(),
                    useMedianRawValuesAll = dr["useMedianRawValuesAll"].ToString(),
                    selectedAllelicMany = dr["selectedAllelicMany"].ToString(),
                    useMedainRawValuesMany = dr["useMedainRawValuesMany"].ToString(),
                    selectAllelicOne = dr["selectAllelicOne"].ToString(),
                    useMedianRawValuesOne = dr["useMedianRawValuesOne"].ToString(),
                    isDirty = false
                });
            }
             return lociDatas;
        }

        public bool UpdateRules(List<LociData> lociDatas)
        {
            StringBuilder query = new StringBuilder();
            foreach(LociData ld in lociDatas)
            {
                query.AppendLine(CreateUpdateSQL(ld));
            }
            LociRulesBO lociBO = new LociRulesBO();
            return lociBO.UpdateRules(query.ToString());
        }
        public bool UpdateAllRule(string column, string value)
        {
            LociRulesBO loci = new LociRulesBO();
            return loci.UpdateRules(CreateUpdateAllSQL(column, value));
        }
        public string CreateUpdateSQL(LociData dr)
        {
            string sql = $"UPDATE [dbo].[tbHematosLociRules] SET Serology = '{dr.Serology}', selectedAllelicAll = '{ dr.selectedAllelicAll }'," +
                $" useMedianRawValuesAll = '{dr.useMedianRawValuesAll}', selectedAllelicMany = '{dr.selectedAllelicMany}', useMedainRawValuesMany = '{dr.useMedainRawValuesMany}', " +
                $" SelectAllelicOne = '{dr.selectAllelicOne}', useMedianRawValuesOne = '{dr.useMedianRawValuesOne}' WHERE AlleleName = '{dr.Allelic}';";
            return sql;
        }
        public string CreateUpdateAllSQL(string column, string value)
        {
            string sql = $"UPDATE [dbo].[tbHematosLociRules] SET  {column} = '{value}'";
            return sql;
        }
        /// <summary>
        /// Restore to baseline file
        /// </summary>
        /// <returns></returns>
        public bool Restore()
        {
            LociRulesBO lociBO = new LociRulesBO();
            return lociBO.RestoreRules();
        }
    }
}
