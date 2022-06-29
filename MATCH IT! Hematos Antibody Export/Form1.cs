using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormPrototype.Model;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace WinFormPrototype
{
    public partial class Form1 : Form
    {
        private List<LociData> lociData;
        public Form1()
        {
            InitializeComponent();            
        }

        private void gridControl1_Load(object sender, EventArgs e)
        {
            string sampleData = File.ReadAllText("./Data/sampledata.json");
            BuildClassStructure(sampleData);
            gridControl1.DataSource = lociData;
        }
        private void BuildClassStructure(string filedata)
        {
            JObject jObject = JObject.Parse(filedata);

            lociData = JsonConvert.DeserializeObject<List<LociData>>(jObject.First.First.ToString());

        }
    }
}
