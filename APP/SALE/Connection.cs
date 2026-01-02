using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SALE
{
    internal class Connection
    {
        private static string stringConnection = @"Data Source=DANH\SQLEXPRESS;Initial Catalog=PetCareX_DB;Integrated Security=True";
        public static SqlConnection GetConnection()
        {
            return new SqlConnection(stringConnection);
        } 

    }
}
