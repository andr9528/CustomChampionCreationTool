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
         * string[] restrict = new string[4];

            restrict[2] = "Resources";
            resourcesTable = repo.GetSchema("Tables", restrict);

            restrict[2] = "Champions";
            championsTable = repo.GetSchema("Tables", restrict);

            restrict[2] = "Abilities";
         *  abilitiesTable = repo.GetSchema("Tables", restrict);
         * 
         * /

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

        private static DataTable resourcesTable;
        private static DataTable championsTable;
        private static DataTable abilitiesTable;

        private static DataTable table;
        private static List<string> tables = new List<string>();

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

            string[] restrict = new string[4];

            restrict[2] = tables[0];
            resourcesTable = repo.GetSchema("Tables", restrict);

            restrict[2] = tables[1];
            championsTable = repo.GetSchema("Tables", restrict);

            restrict[2] = tables[2];
            abilitiesTable = repo.GetSchema("Tables", restrict);
        }

        public static List<Ability> GetAbilities()
        {
            List<Ability> outputList = new List<Ability>();

            return outputList;
        }

        public static List<Resource> GetResources()
        {
            List<Resource> outputList = new List<Resource>();

            Resource reCreate = new Resource(0, "dummy", 0, 0);

            foreach (DataRow row in resourcesTable.Rows)
            {
                reCreate.MaxValue = int.Parse(row["MaxValue"].ToString());
                reCreate.Name = row["Name"].ToString();
                reCreate.ID = int.Parse(row["Id"].ToString());
                reCreate.MinValue = int.Parse(row["MinValue"].ToString());

                outputList.Add(reCreate);
            }

            return outputList;
        }

        public static string Test1()
        {
            string output = "";

            foreach (DataRow row in resourcesTable.Rows)
            {
                output += row[2];
            }
            return output;
        }
        public static List<string> Test2()
        {
            return tables;
        }
    }
}
