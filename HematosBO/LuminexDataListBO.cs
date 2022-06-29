using HematosDAL;
using System;
using System.Collections.Generic;
using System.Data;

namespace HematosBO
{
    public class LuminexDataListBO : ILuminexDataList, IDisposable
    {

        #region "Properties"

        public List<ILuminexData> LuminexDataList { get; set; }

        #endregion "Properties"

        #region "Methods"

        public LuminexDataListBO()
        {
            LuminexDataList = new List<ILuminexData>();
        }

        public static DataSet LoadLuminexData()
        {
            

            DataSet _luminexDataDS = LuminexDataDAL.RetrieveLuminexData();

            if (_luminexDataDS != null)
            {
                foreach (DataRow row in _luminexDataDS.Tables[0].Rows)
                {
                    using (LuminexDataBO _luminexData = new LuminexDataBO())
                    {
                        _luminexData.loadLuminexData(row);
                    }
                }
            }

            return _luminexDataDS;
        }

        void ILuminexDataList.LoadAssignments()
        {
            LoadLuminexData();
        }

        #endregion "Methods"

        // To detect redundant calls
        private bool disposedValue = false;

        // IDisposable
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: free other state (managed objects).
                    LuminexDataList = null;
                }
                GC.Collect();
                // TODO: free your own state (unmanaged objects).
                // TODO: set large fields to null.
            }
            disposedValue = true;
        }

        #region " IDisposable Support "

        // This code added by Visual Basic to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion " IDisposable Support "
    }
}