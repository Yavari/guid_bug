using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DuckDB.NET.Data;

namespace DuckDbTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (var duckDBConnection = new DuckDBConnection("Data Source=test.db"))
            {
                duckDBConnection.Open();

                using (var command = duckDBConnection.CreateCommand())
                {
                    command.CommandText = @"
CREATE TABLE IF NOT EXISTS DuckDbTest(Id UUID NOT NULL);
INSERT INTO DuckDbTest (Id) VALUES ('60c1c780-ca45-11ee-835e-a059506a776d');
INSERT INTO DuckDbTest (Id) VALUES ('60c1c774-ca45-11ee-835e-a059506a776d');";

                    var executeNonQuery = command.ExecuteNonQuery();

                    command.CommandText = "Select Id from DuckDbTest";
                    var queryResult = command.ExecuteReader();
                    while (queryResult.Read())
                    {
                        var val = queryResult.GetGuid(0);
                        Console.WriteLine(val);
                    }
                }
            }

        }
    }
}
