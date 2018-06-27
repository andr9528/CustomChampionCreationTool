using CCCTLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace CCCTDBFacade
{
  public class Facade : IFacade
    {
        #region Global Values & Initialize
        private static readonly string connStr = "Data Source=ANDRE-PC;Initial Catalog=CustomChampionCreationTool;Integrated Security=True";
        private static SqlConnection repo;

        private static DataTable table;
        private static List<string> tables = new List<string>(); // 0 = Resources, 1 = Champions, 2 = Abilities

        public ReturnMessage Initialize()
        {
            try
            {
                repo = new SqlConnection(connStr);

                repo.Open();

                table = repo.GetSchema("tables");

                foreach (DataRow row in table.Rows)
                {
                    string tablename = (string)row[2];
                    tables.Add(tablename);
                }
                return new ReturnMessage() { WasSuccesful = true, Message = "No Problems"};
            }
            catch (Exception e)
            {
                return new ReturnMessage() { WasSuccesful = false, Message = "Something went awry", Exception = e.Message};
            }
        }
        #endregion

        #region Resource Handeling
        public ReturnMessage UpdateResource(Resource source)
        {
            try
            {
                string query = String.Format(@"UPDATE {0} SET Name = '{1}', MaxValue = '{2}',"
                     + "MinValue = '{3}', MaxedAtStart = '{4}' WHERE Id='{5}'",
                        tables[0], source.Name, source.MaxValue, source.MinValue, source.MaxedAtStart, source.ID);

                SqlCommand cmd = new SqlCommand(query, repo);

                cmd.ExecuteNonQuery();

                return new ReturnMessage() { WasSuccesful = true, Message = "No Problems" };
            }
            catch (Exception e)
            {
                return new ReturnMessage() { WasSuccesful = false, Message = "Something went awry", Exception = e.Message };
            }
        }

        public Tuple<List<Resource>, ReturnMessage> GetResources()
        {
            try
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
                        ID = (int)row["ID"],
                        MinValue = (int)row["MinValue"],
                        MaxedAtStart = (bool)row["MaxedAtStart"]
                    };

                    outputList.Add(reCreate);
                }
                ReturnMessage message = new ReturnMessage() { WasSuccesful = true, Message = "No Problems" };

                return new Tuple<List<Resource>, ReturnMessage>(outputList, message);
            }
            catch (Exception e)
            {
                ReturnMessage message = new ReturnMessage() { WasSuccesful = false, Message = "Something went awry", Exception = e.Message };

                return new Tuple<List<Resource>, ReturnMessage>(null, message);
            }
        }

        public ReturnMessage NewResource(Resource source)
        {
            try
            {
                string format = "INSERT INTO dbo.{0} (Id, Name, MaxValue, MinValue, MaxedAtStart) " +
                        "VALUES ('{1}', '{2}', '{3}', '{4}', '{5}')";
                object[] args = { tables[0], source.ID, source.Name, source.MaxValue, source.MinValue, source.MaxedAtStart };

                string query = String.Format(format, args);

                SqlCommand cmd = new SqlCommand(query, repo);

                cmd.ExecuteNonQuery();

                return new ReturnMessage() { WasSuccesful = true, Message = "No Problems" };
            }
            catch (Exception e)
            {
                return new ReturnMessage() { WasSuccesful = false, Message = "Something went awry", Exception = e.Message };
            }
        }

        public ReturnMessage DeleteResource(Resource source)
        {
            try
            {
                string query = String.Format(@"DELETE FROM {0} WHERE Id='{1}'", tables[0], source.ID);

                SqlCommand cmd = new SqlCommand(query, repo);
                cmd.ExecuteNonQuery();

                return new ReturnMessage() { WasSuccesful = true, Message = "No Problems" };
            }
            catch (Exception e)
            {
                return new ReturnMessage() { WasSuccesful = false, Message = "Something went awry", Exception = e.Message };
            }
        }
        #endregion

        #region Ability Handeling
        public ReturnMessage UpdateAbility(Ability source)
        {
            throw new NotImplementedException();

            try
            {


                return new ReturnMessage() { WasSuccesful = true, Message = "No Problems" };
            }
            catch (Exception e)
            {
                return new ReturnMessage() { WasSuccesful = false, Message = "Something went awry", Exception = e.Message };
            }
        }

        public Tuple<List<Ability>, ReturnMessage> GetAbilities()
        {
            try
            {
                List<Ability> outputList = new List<Ability>();
                List<Resource> resourceList = GetResources().Item1;

                string query = String.Format("SELECT * FROM dbo.{0}", tables[2]);
                SqlDataAdapter adapt = new SqlDataAdapter(query, repo);

                DataSet database = new DataSet();
                adapt.Fill(database, tables[2]);

                foreach (DataRow row in database.Tables[tables[2]].Rows)
                {
                    Ability reCreate = new Ability()
                    {
                        Name = row["Name"].ToString().Trim(' '),
                        ID = (int)row["ID"],
                        ResourceUse = resourceList.Find(x => x.ID == (int)row["ResourceID"]),
                        Slot = (LibRepo.AbilitySlot)row["AbilitySlot"],
                        HaveActive = (bool)row["HaveActive"],
                        IsToogleAble = (bool)row["IsToogleAble"],
                        HaveEmpoweredOrAlternative = (bool)row["HaveEmpoweredOrAlternative"],
                        HavePassive = (bool)row["HaveEmpoweredOrAlternative"],
                        DescriptionAct = row["DescriptionAct"].ToString().Trim(' '),
                        DamageAct = row["DamageAct"].ToString().Trim(' '),
                        CooldownAct = row["CooldownAct"].ToString().Trim(' '),
                        RangeAct = row["RangeAct"].ToString().Trim(' '),
                        ResourceCostAct = row["ResourceCostAct"].ToString().Trim(' '),
                        DescriptionEmpAlt = row["DescriptionEmpAlt"].ToString().Trim(' '),
                        DamageEmpAlt = row["DamageEmpAlt"].ToString().Trim(' '),
                        CooldownEmpAlt = row["CooldownEmpAlt"].ToString().Trim(' '),
                        RangeEmpAlt = row["RangeEmpAlt"].ToString().Trim(' '),
                        ResourceCostEmpAlt = row["ResourceCostEmpAlt"].ToString().Trim(' '),
                        DescriptionPas = row["DescriptionPas"].ToString().Trim(' '),
                        RangePas = row["RangePas"].ToString().Trim(' '),
                        DamagePas = row["DamagePas"].ToString().Trim(' '),
                        CooldownPas = row["CooldownPas"].ToString().Trim(' ')
                    };

                    outputList.Add(reCreate);
                }

                ReturnMessage message = new ReturnMessage() { WasSuccesful = true, Message = "No Problems" };

                return new Tuple<List<Ability>, ReturnMessage>(outputList, message);
            }
            catch (Exception e)
            {
                ReturnMessage message = new ReturnMessage() { WasSuccesful = false, Message = "Something went awry", Exception = e.Message };

                return new Tuple<List<Ability>, ReturnMessage>(null, message);
            }
        }

        public ReturnMessage NewAbility(Ability source)
        {
            try
            {
                string format = "INSERT INTO dbo.{0} (Id, ResourceID, Name, AbilitySlot, HaveActive, IsToogleAble, " +
                        "HaveEmpoweredOrAlternative, HavePassive, DescriptionAct, DamageAct, CooldownAct, RangeAct, ResourceCostAct, " +
                        "DescriptionEmpAlt, DamageEmpAlt, CooldownEmpAlt, RangeEmpAlt, ResourceCostEmpAlt, DescriptionPas, RangePas, DamagePas, CooldownPas) " +
                       "VALUES ('{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', " +
                       "'{12}', '{13}', '{14}', '{15}', '{16}', '{17}', '{18}', '{19}', '{20}', '{21}', '{22}')";
                object[] args = {tables[2], source.ID, source.ResourceUse.ID, source.Name, (int)source.Slot, source.HaveActive, source.IsToogleAble,
               source.HaveEmpoweredOrAlternative, source.HavePassive, source.DescriptionAct, source.DamageAct, source.CooldownAct,
               source.RangeAct, source.ResourceCostAct, source.DescriptionEmpAlt, source.DamageEmpAlt, source.CooldownEmpAlt,
               source.RangeEmpAlt, source.ResourceCostEmpAlt, source.DescriptionPas, source.RangePas, source.DamagePas, source.CooldownPas };

                string query = String.Format(format, args);

                SqlCommand cmd = new SqlCommand(query, repo);

                cmd.ExecuteNonQuery();

                return new ReturnMessage() { WasSuccesful = true, Message = "No Problems" };
            }
            catch (Exception e)
            {
                return new ReturnMessage() { WasSuccesful = false, Message = "Something went awry", Exception = e.Message };
            }
        }

        public ReturnMessage DeleteAbility(Ability source)
        {
            try
            {
                string query = String.Format(@"DELETE FROM {0} WHERE Id='{1}'", tables[2], source.ID);

                SqlCommand cmd = new SqlCommand(query, repo);
                cmd.ExecuteNonQuery();

                return new ReturnMessage() { WasSuccesful = true, Message = "No Problems" };
            }
            catch (Exception e)
            {
                return new ReturnMessage() { WasSuccesful = false, Message = "Something went awry", Exception = e.Message };
            }
        }
        #endregion

        #region Champion Handeling
        public ReturnMessage UpdateChampion(Champion source)
        {
            throw new NotImplementedException();

            try
            {


                return new ReturnMessage() { WasSuccesful = true, Message = "No Problems" };
            }
            catch (Exception e)
            {
                return new ReturnMessage() { WasSuccesful = false, Message = "Something went awry", Exception = e.Message };
            }
        }
        public Tuple<List<Champion>, ReturnMessage> GetChampions()
        {
            throw new NotImplementedException();

            try
            {
                List<Champion> outputList = new List<Champion>();

                ReturnMessage message = new ReturnMessage() { WasSuccesful = true, Message = "No Problems" };

                return new Tuple<List<Champion>, ReturnMessage>(outputList, message);
            }
            catch (Exception e)
            {
                ReturnMessage message = new ReturnMessage() { WasSuccesful = false, Message = "Something went awry", Exception = e.Message };

                return new Tuple<List<Champion>, ReturnMessage>(null, message);
            }
        }
        public ReturnMessage NewChampion(Champion source)
        {
            throw new NotImplementedException();

            try
            {


                return new ReturnMessage() { WasSuccesful = true, Message = "No Problems" };
            }
            catch (Exception e)
            {
                return new ReturnMessage() { WasSuccesful = false, Message = "Something went awry", Exception = e.Message };
            }
        }
        public ReturnMessage DeleteChampion(Champion source)
        {
            throw new NotImplementedException();

            try
            {


                return new ReturnMessage() { WasSuccesful = true, Message = "No Problems" };
            }
            catch (Exception e)
            {
                return new ReturnMessage() { WasSuccesful = false, Message = "Something went awry", Exception = e.Message };
            }
        }
        #endregion

        #region Test Methods
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }
        #endregion
    }
}
