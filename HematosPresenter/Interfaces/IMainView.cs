using HematosViewModel;
using System.Collections.Generic;

namespace HematosPresenter
{
    public interface IMainView
    {
        List<string> BatchSearchList { get; set; }

        List<string> BatchList { get; set; }

        string selectedBatch { get; set; }

        string SaveFilePath { get; set; }

        int filterSelection { get; }

        List<SampleVM> SampleList { get; set; }

        List<SampleVM> SelectedSamples { get; set; }

        string LumID { get; set; }

        string SiteCode { get; set; }

        string LumSN { get; set; }

        string LumServer { get; set; }
    }
}