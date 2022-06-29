using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HematosDAL;

namespace HematosBO
{
    public class LociRulesBO
    {
        public DataTable GetRules()
        {
            LociRulesDAL lociRulesDAL = new LociRulesDAL();
            return lociRulesDAL.GetRulesFromDB();
        }
        
        public bool UpdateRules(string query)
        {
            LociRulesDAL loci = new LociRulesDAL();
            return loci.UpdateRuleTable(query);
        }
        public bool RestoreRules()
        {
            LociRulesDAL loci = new LociRulesDAL();
            return loci.RestoreFromFile();
        }
    }
}
