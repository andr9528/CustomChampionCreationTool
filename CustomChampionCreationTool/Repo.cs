using CustomChampionCreationTool.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Threading;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;


namespace CustomChampionCreationTool
{
    public static class Repo
    {
        public enum AbilitySlot { Passive, Q, W, E, R }
        private static string repoLocaDesk = @"C:\Users\andre\Dropbox\Game Idées\CustomChampionCreationTool\CustomChampionCreationTool";
        private static string repoLocaLap = @"C:\Users\Andre\Dropbox\Game Idées\CustomChampionCreationTool\CustomChampionCreationTool";
        private static string connStr = Properties.Settings.Default.RepoConnectionString;
        private static SqlConnection repo;

        private static DataTable table; 
        private static List<string> tables = new List<string>(); // 0 = Resources, 1 = Champions, 2 = Abilities

        internal static void Initialize()
        {
            AppDomain.CurrentDomain.SetData("DataDirectory", repoLocaDesk);

            repo = new SqlConnection(connStr);

            repo.Open();

            table = repo.GetSchema("Tables");

            foreach (DataRow row in table.Rows)
            {
                string tablename = (string)row[2];
                tables.Add(tablename);
            }
        }

        internal static void UpdateResource(Resource source)
        {
            string query = @"UPDATE " + tables[0] + " (Name, MaxValue, MinValue, MaxedAtStart) "
                + "VALUES (@Name, @MaxValue, @MinValue, @MaxedAtStart) WHERE Id='" + source.ID + "'";

            SqlCommand cmd = new SqlCommand(query, repo);

            cmd.Parameters.AddWithValue("@Name", source.Name);
            cmd.Parameters.AddWithValue("@MaxValue", source.MaxValue);
            cmd.Parameters.AddWithValue("@MinValue", source.MinValue);
            cmd.Parameters.AddWithValue("@MaxedAtStart", source.MaxedAtStart);

            cmd.ExecuteNonQuery();
        }


        internal static List<Ability> GetAbilities()
        {
            List<Ability> outputList = new List<Ability>();

            return outputList;
        }

        internal static List<Resource> GetResources()
        {
            List<Resource> outputList = new List<Resource>();

            string query = "SELECT * FROM dbo." + tables[0];
            SqlDataAdapter adapt = new SqlDataAdapter(query, repo);

            DataSet database = new DataSet();
            adapt.Fill(database, tables[0]);

            foreach (DataRow row in database.Tables[tables[0]].Rows)
            {
                Resource reCreate = new Resource()
                {
                    MaxValue = (int)row["MaxValue"],
                    Name = row["Name"].ToString().Trim(' '),
                    ID = (int)row["Id"],
                    MinValue = (int)row["MinValue"],
                    MaxedAtStart = (bool)row["MaxedAtStart"] 
                };

                outputList.Add(reCreate);
            }

            return outputList;
        }

        internal static void NewResource(Resource source)
        {
            string query = "INSERT INTO dbo." + tables[0]
                + "(Id, Name, MaxValue, MinValue, MaxedAtStart) VALUES "
                + "(@Id, @Name, @MaxValue, @MinValue, @MaxedAtStart)";

            SqlCommand cmd = new SqlCommand(query, repo);

            cmd.Parameters.AddWithValue("@Id", source.ID);
            cmd.Parameters.AddWithValue("@Name", source.Name);
            cmd.Parameters.AddWithValue("@MaxValue", source.MaxValue);
            cmd.Parameters.AddWithValue("@MinValue", source.MinValue);
            cmd.Parameters.AddWithValue("@MaxedAtStart", source.MaxedAtStart);

            cmd.ExecuteNonQuery();
        }
        internal static void DeleteResource(Resource source)
        {
            string query = @"DELETE FROM " + tables[0] + " WHERE Id='" + source.ID + "'";

            SqlCommand cmd = new SqlCommand(query, repo);
            cmd.ExecuteNonQuery();
        }

        internal static string Test1()
        {
            string output = "";

            

            return output;
        }
        internal static List<string> Test2()
        {
            List<string> output = new List<string>();

            output = tables;

            return output;
        }
    }
}
