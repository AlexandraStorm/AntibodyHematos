using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.IO;
using HematosDBFacade;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;

namespace HematosDAL
{
    public class LociRulesDAL : IDisposable
    {
        private DBFacade dbFac;
        private readonly string _configFile;
        protected IDBFacade _dbFacade = null;
        private readonly ExeConfigurationFileMap configFileMap = new ExeConfigurationFileMap();

        public LociRulesDAL()
        {
            try
            {
                _configFile = string.Format("{0}/{1}/External_matchit.config", Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Lifecodes");
                dbFac = new DBFacade(_configFile, true);
            }
            catch (Exception ex)
            {
                ExceptionPolicy.HandleException(ex, "Log Only Policy");
                throw;
            }
        }

        public DataTable GetRulesFromDB()
        {
            string sql = "SELECT * FROM [dbo].[tbHematosLociRules]";
            try
            {
                DataSet samples = new DataSet();
                samples = dbFac.ExecuteSQL(sql);

                return samples.Tables[0];
            }
            catch (Exception ex)
            {
                ExceptionPolicy.HandleException(ex, "Log Only Policy");
                return null;
            }
        }        
        public bool UpdateRuleTable(string updatesql)
        {
            try
            {
                dbFac.ExecuteScalar(updatesql);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool RestoreFromFile()
        {
            bool scriptExec;
            SqlConnection con = new SqlConnection(dbFac.GetSqlConnectionString());
            ServerConnection servConn = new ServerConnection(con);
            Server srv = new Server(servConn);
            string script = File.ReadAllText(String.Format(@"{0}\SQL Scripts\BaseSerologyFile.sql", AppDomain.CurrentDomain.BaseDirectory));

            try
            {
                if (!string.IsNullOrEmpty(script))
                {
                    srv.ConnectionContext.BeginTransaction();
                    scriptExec = Convert.ToBoolean(srv.ConnectionContext.ExecuteNonQuery(script));
                    srv.ConnectionContext.CommitTransaction();
                }
                scriptExec = true;
            }
            catch (SmoException smoEx)
            {
                // error in sql call throws sqlexception and bubbles up
                // custom error
                srv.ConnectionContext.RollBackTransaction();
                bool rethrow = ExceptionPolicy.HandleException(smoEx, "Log Only Policy");
                scriptExec = false;
                if (rethrow)
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                // general non-sqlerror throws exception and bubbles up
                // custom error
                // error in sql call throws sqlexception and bubbles up
                // custom error
                srv.ConnectionContext.RollBackTransaction();
                bool rethrow = ExceptionPolicy.HandleException(ex, "Log Only Policy");
                scriptExec = false;
                if (rethrow)
                {
                    throw;
                }
            }
            return scriptExec;
        }
        public DataSet GetRulesforUsage()
        {
            try
            {
                DataSet samples = new DataSet();
                samples = dbFac.ExecuteProc("GetHematosLociRules");

                return samples;
            }
            catch (Exception ex)
            {
                ExceptionPolicy.HandleException(ex, "Log Only Policy");
                return null;
            }
        }
        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~LociRulesDAL()
        // {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
