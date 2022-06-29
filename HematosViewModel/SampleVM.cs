using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HematosViewModel
{
    public class SampleVM
    {
        public string batchID { get; set; }
        public string sampleID { get; set; }
        public int CheckSpec { get; set; }
        public string specification { get; set; }
        public string comment { get; set; }
        public bool useSpec { get; set; }
        public bool CheckComment { get; set; }

    }
}
