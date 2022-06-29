using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HematosViewModel
{
    public class LociData
    {
        public string locus { get; set; }
        public string Serology { get; set; }
        public string Allelic { get; set; }
        public string selectedAllelicAll { get; set; }
        public string useMedianRawValuesAll { get; set; }
        public string selectedAllelicMany { get; set; }
        public string useMedainRawValuesMany { get; set; }
        public string selectAllelicOne { get; set; }
        public string useMedianRawValuesOne { get; set; }
        public bool isDirty { get; set; }
    }
}
