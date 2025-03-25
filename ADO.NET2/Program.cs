using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;
using System.Security.Claims;
using System.Configuration;
using System.Management;


namespace ADO.NET2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //1)
            const int PADDING = 30;
            //const string CONNECTION_STRING = "Data Source=(localdb)\\MSSQLLocalDB;" +
            //                                 "Initial Catalog=Movies;" +
            //                                  "Integrated Security=True;" +
            //                                  "Connect Timeout=30;Encrypt=False;" +
            //                                    "TrustServerCertificate=False;" +
            //                                    "ApplicationIntent=ReadWrite;" +
            //                                    "MultiSubnetFailover=False";
            string CONNECTION_STRING = ConfigurationManager.ConnectionStrings["Movies"].ConnectionString;
            Console.WriteLine(CONNECTION_STRING);

            //2)
            SqlConnection connection = new SqlConnection(CONNECTION_STRING);
            //

            //3)
            string cmd = "SELECT * FROM Movies,Directors WHERE director=director_id";
            SqlCommand command = new SqlCommand(cmd, connection);

            //4) 
           connection.Open();

            SqlDataReader reader = command.ExecuteReader();


            //5
            if(reader.HasRows)
            {
                Console.WriteLine("====================================================================================================");
                for (int i = 0; i < reader.FieldCount; i++)
                Console.Write(reader.GetName(i).PadRight(PADDING));
                    Console.WriteLine();
                Console.WriteLine("====================================================================================================");

                while (reader.Read())
                {
                    //Console.WriteLine($"{reader[0].ToString().PadRight(5)}{reader[2].ToString().PadRight(15)}\t{reader[1].ToString().PadRight(15)}");
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        Console.Write(reader[i].ToString().PadRight(PADDING));
                    }
                    Console.WriteLine();    
                }
            }
            //6)
            reader.Close();
            connection.Close();

        }
    }
}
