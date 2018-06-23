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
        private static string connStr = "Data Source=ANDRE-PC;Initial Catalog=CustomChampionCreationTool;Integrated Security=True";
        private static SqlConnection repo;

        private static DataTable table;
        private static List<string> tables = new List<string>(); // 0 = Resources, 1 = Champions, 2 = Abilities

        private static List<string> test = new List<string>();

        internal static void Initialize()
        {
            repo = new SqlConnection(connStr);
            
            repo.Open();

            table = repo.GetSchema("tables");

            foreach (DataRow row in table.Rows)
            {
                string tablename = (string)row[2];
                tables.Add(tablename);
            }
        }

        internal static void UpdateResource(Resource source)
        {
            string query = String.Format(@"UPDATE {0} SET Name = '{1}', MaxValue = '{2}',"
             + "MinValue = '{3}', MaxedAtStart = '{4}' WHERE Id='{5}'",
                tables[0], source.Name, source.MaxValue, source.MinValue, source.MaxedAtStart, source.ID);

            SqlCommand cmd = new SqlCommand(query, repo);
           
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

            string query = String.Format("SELECT * FROM dbo.{0}", tables[0]);
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
            string query = String.Format("INSERT INTO dbo.{0} (Id, Name, MaxValue, MinValue, MaxedAtStart)" +
                "VALUE ({1}, {2}, {3}, {4}, {5})",
                tables[0], source.ID, source.Name, source.MaxValue, source.MinValue, source.MaxedAtStart);

            SqlCommand cmd = new SqlCommand(query, repo);

            cmd.ExecuteNonQuery();
        }
        internal static void DeleteResource(Resource source)
        {
            string query = String.Format(@"DELETE FROM {0} WHERE Id='{1}'", tables[0], source.ID);

            SqlCommand cmd = new SqlCommand(query, repo);
            cmd.ExecuteNonQuery();
        }
    }
}
