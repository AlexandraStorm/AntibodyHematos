using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using HematosDBFacade;

namespace HematosDAL
{
    public interface IAntibodyDAL
    {
        bool Initialize();
        void SetDBFacade(IDBFacade dbfacade); // enable dependency injection
        bool CheckCredentials(string userName, string password);
        List<string> GetSampleList(string BatchID, int tableID);
    }
}
