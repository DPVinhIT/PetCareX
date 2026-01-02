using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace RECEPTIONIST
{
    internal class Connection
    {
        private static string stringConnection = @"Data Source=DANH\SQLEXPRESS;Initial Catalog=PetCareX_DB;Integrated Security=True;";
        public static SqlConnection GetConnection()
        {
            return new SqlConnection(stringConnection);
        }
        public static SqlConnection GetConnectionv2()
        {
            return new SqlConnection(stringConnection + "TrustServerCertificate=True");
        }

    }
}
