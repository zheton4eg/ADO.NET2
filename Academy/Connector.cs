using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.InteropServices;

namespace Academy
{
    class Connector
    {
        readonly string CONECTION_STRING;// = ConfigurationManager.ConnectionStrings["PV_319_IMPORT"].ConnectionString;
        SqlConnection connection;

        public Connector(string connection_string)
        {
            CONECTION_STRING = ConfigurationManager.ConnectionStrings["PV_319_IMPORT"].ConnectionString;        
            connection = new SqlConnection(CONECTION_STRING);
            AllocConsole();
            Console.WriteLine(connection_string);

        }
        ~Connector()
        {
            FreeConsole();
        }
        public DataTable Select(string columns, string tables, string condition="")
        {
            DataTable table = null;
            string cmd = $"SELECT {columns} FROM {tables}";
            if (condition != "") cmd += $" WHERE {condition}";
            cmd += ";";
            SqlCommand command = new SqlCommand(cmd,connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(); 

            if(reader.HasRows)
            {
                table = new DataTable();

                for(int i =0;i<reader.FieldCount;i++)
                {
                    table.Columns.Add();
                }
                while(reader.Read())
                {
                    DataRow row = table.NewRow();
                    for(int i=0;i<reader.FieldCount;i++)
                    {
                        row[i] = reader[i];
                    }
                    table.Rows.Add(row);
                }
            }
            reader.Close();
            connection.Close();
            return table;
        }
        [DllImport("kernel32.dll")]
        public static extern bool AllocConsole();
        [DllImport("kernel32.dll")]
        public static extern bool FreeConsole();

    }
}
