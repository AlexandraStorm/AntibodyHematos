using HematosDBFacade;
using System;
using System.Collections.Generic;
using System.Data;

namespace HematosDAL
{
    public class UserDAL
    {
        public static bool IsValidPassword(string userName, string password)
        {
            Dictionary<string, object> @params = new Dictionary<string, object>();
            @params.Add("UserName", userName);
            @params.Add("Password", DBFacade.Encrypt(password, "TeSt"));

            DBFacade dbFac = new DBFacade();
            int result = Convert.ToInt32(dbFac.ExecuteScalar(true, "CheckPassword", @params));

            if (result == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static DataSet RetrieveUser(string userName)
        {
            Dictionary<string, object> @params = new Dictionary<string, object>();
            @params.Add("UserName", userName);

            DBFacade dbFac = new DBFacade();
            DataSet result = dbFac.ExecuteProc("GetUser", @params);

            return result;
        }
    }
}