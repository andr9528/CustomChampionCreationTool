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
        /*
         * Format for Resources in the Repo
         *      [ID] [Name] [MinValue] [MaxValue]
         * 
         * 
         * 
         * */
        private static string repoLoca = @"C:\Users\andre\Dropbox\Game Idées\CustomChampionCreationTool\CustomChampionCreationTool";
        private static string connStr = Properties.Settings.Default.RepoConnectionString;
        private static SqlConnection repo;

        private static DataTable table; 
        private static List<string> tables = new List<string>(); // 0 = Resources, 1 = Champions, 2 = Abilities

        public static void Initialize()
        {
            AppDomain.CurrentDomain.SetData("DataDirectory", repoLoca);

            repo = new SqlConnection(connStr);

            repo.Open();

            table = repo.GetSchema("Tables");

            foreach (DataRow row in table.Rows)
            {
                string tablename = (string)row[2];
                tables.Add(tablename);
            }

        }

        public static List<Ability> GetAbilities()
        {
            List<Ability> outputList = new List<Ability>();

            return outputList;
        }

        public static List<Resource> GetResources()
        {
            List<Resource> outputList = new List<Resource>();

            string query = "SELECT * FROM dbo." + tables[0];
            SqlDataAdapter adapt = new SqlDataAdapter(query, repo);

            DataSet resources = new DataSet();
            adapt.Fill(resources, tables[0]);

            foreach (DataRow row in resources.Tables[tables[0]].Rows)
            {
                Resource reCreate = new Resource()
                {
                    MaxValue = (int)row["MaxValue"],
                    Name = row["Name"].ToString().Trim(' '),
                    ID = (int)row["Id"],
                    MinValue = (int)row["MinValue"]
                };

                outputList.Add(reCreate);
            }

            return outputList;
        }

        public static string Test1()
        {
            string output = "";

            

            return output;
        }
        public static List<string> Test2()
        {
            List<string> output = new List<string>();

            output = tables;

            return output;
        }
    }
}
