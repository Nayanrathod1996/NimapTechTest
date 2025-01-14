using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace NimaApp.CommanFunction
{
    public class Commanfun
    {
        public int GetTotalProductCount()
        {
            int totalCount = 0;
            string cs = ConfigurationManager.ConnectionStrings["dbms"].ConnectionString; ;

            using (var conn = new SqlConnection(cs))
            {
                using (var cmd = new SqlCommand("SELECT COUNT(*) FROM ProductMaster", conn))
                {
                    conn.Open();
                    totalCount = (int)cmd.ExecuteScalar();
                }
            }

            return totalCount;
        }

        public string ConnectionString()
        {

            return ConfigurationManager.ConnectionStrings["dbms"].ConnectionString ;
        }



    }
}