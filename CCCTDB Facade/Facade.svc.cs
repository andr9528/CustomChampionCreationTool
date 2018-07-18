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
        private static readonly string connStr = "Data Source=WWW12;Initial Catalog=CustomChampionCreationTool;Integrated Security=True";
        private static SqlConnection repo;

        private static DataTable table;
        private static List<string> tables = new List<string>(); // 0 = Resources, 1 = Abilities, 2 = Champions

        public ReturnMessage Initialize()
        {
            try
            {
                repo = new SqlConnection(connStr);

                if (repo.State != ConnectionState.Open)
                {
                    repo.Open();
                }

                table = repo.GetSchema("tables");

                foreach (DataRow row in table.Rows)
                {
                    string tablename = (string)row[2];
                    tables.Add(tablename);
                }
                return new ReturnMessage() { WasSuccesful = true, Message = "No Problems", Where = "Initialize Service", DBScheme = table, DBState = repo.State };
            }
            catch (Exception e)
            {
                return new ReturnMessage() { WasSuccesful = false, Message = "Something went awry", Exception = e, Where = "Initialize Service", DBState = repo.State};
            }
            finally
            {
                repo.Close();
            }
        }
        #endregion

        #region Resource Handeling
        public ReturnMessage UpdateResource(Resource source)
        {
            try
            {
                if (repo.State != ConnectionState.Open)
                {
                    repo.Open();
                }
                string query = String.Format(@"UPDATE {0} SET Name = '{1}', MaxValue = '{2}',"
                     + "MinValue = '{3}', MaxedAtStart = '{4}' WHERE ID='{5}'",
                        tables[0], source.Name, source.MaxValue, source.MinValue, source.MaxedAtStart, source.ID);

                SqlCommand cmd = new SqlCommand(query, repo);

                cmd.ExecuteNonQuery();

                return new ReturnMessage() { WasSuccesful = true, Message = "No Problems", Where = "UpdateResource", DBState = repo.State };
            }
            catch (Exception e)
            {
                return new ReturnMessage() { WasSuccesful = false, Message = "Something went awry", Exception = e, Where = "UpdateResource", DBState = repo.State };
            }
            finally
            {
                repo.Close();
            }
        }

        public Tuple<List<Resource>, ReturnMessage> GetResources()
        {
            try
            {
                if (repo.State != ConnectionState.Open)
                {
                    repo.Open();
                }
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
                ReturnMessage message = new ReturnMessage() { WasSuccesful = true, Message = "No Problems", Where = "GetResources", DBState = repo.State };

                return new Tuple<List<Resource>, ReturnMessage>(outputList, message);
            }
            catch (Exception e)
            {
                ReturnMessage message = new ReturnMessage() { WasSuccesful = false, Message = "Something went awry", Exception = e, Where = "GetResources", DBState = repo.State };

                return new Tuple<List<Resource>, ReturnMessage>(null, message);
            }
            finally
            {
                repo.Close();
            }
        }

        public ReturnMessage NewResource(Resource source)
        {
            try
            {
                if (repo.State != ConnectionState.Open)
                {
                    repo.Open();
                }
                string format = "INSERT INTO dbo.{0} (Id, Name, MaxValue, MinValue, MaxedAtStart) " +
                        "VALUES ('{1}', '{2}', '{3}', '{4}', '{5}')";
                object[] args = { tables[0], source.ID, source.Name, source.MaxValue, source.MinValue, source.MaxedAtStart };

                string query = String.Format(format, args);

                SqlCommand cmd = new SqlCommand(query, repo);

                cmd.ExecuteNonQuery();

                return new ReturnMessage() { WasSuccesful = true, Message = "No Problems", Where = "NewResource", DBState = repo.State };
            }
            catch (Exception e)
            {
                return new ReturnMessage() { WasSuccesful = false, Message = "Something went awry", Exception = e, Where = "NewResource", DBState = repo.State };
            }
            finally
            {
                repo.Close();
            }
        }

        public ReturnMessage DeleteResource(Resource source)
        {
            try
            {
                if (repo.State != ConnectionState.Open)
                {
                    repo.Open();
                }
                string query = String.Format(@"DELETE FROM {0} WHERE Id= @ID", tables[0]);

                SqlCommand cmd = new SqlCommand(query, repo);

                cmd.Parameters.AddWithValue("@ID", source.ID);

                cmd.ExecuteNonQuery();

                return new ReturnMessage() { WasSuccesful = true, Message = "No Problems", Where = "DeleteResource", DBState = repo.State };
            }
            catch (Exception e)
            {
                return new ReturnMessage() { WasSuccesful = false, Message = "Something went awry", Exception = e, Where = "DeleteResource", DBState = repo.State };
            }
            finally
            {
                repo.Close();
            }
        }
        #endregion

        #region Ability Handeling
        public ReturnMessage UpdateAbility(Ability source)
        {
            try
            {
                if (repo.State != ConnectionState.Open)
                {
                    repo.Open();
                }
                string format = "@UPDATE {0} SET ResourceID = '{1}', Name = '{2}', AbilitySlot = '{3}', HaveActive = '{4}', IsToogleAble = '{5}', " +
                        "HaveEmpoweredOrAlternative = '{6}', HavePassive = '{7}', DescriptionAct = '{8}', DamageAct = '{9}', CooldownAct = '{10}', " +
                        "RangeAct = '{11}', ResourceCostAct = '{12}', DescriptionEmpAlt = '{13}', DamageEmpAlt = '{14}', CooldownEmpAlt = '{15}', " +
                        "RangeEmpAlt = '{16}', ResourceCostEmpAlt = '{17}', DescriptionPas = '{18}', RangePas = '{19}', DamagePas = '{20}', " +
                        "CooldownPas = '{21}' WHERE ID = '{22}'";
                object[] args = {tables[1], source.ResourceUse.ID, source.Name, (int)source.Slot, source.HaveActive, source.IsToogleAble,
               source.HaveEmpoweredOrAlternative, source.HavePassive, source.DescriptionAct, source.DamageAct, source.CooldownAct,
               source.RangeAct, source.ResourceCostAct, source.DescriptionEmpAlt, source.DamageEmpAlt, source.CooldownEmpAlt,
               source.RangeEmpAlt, source.ResourceCostEmpAlt, source.DescriptionPas, source.RangePas, source.DamagePas,
               source.CooldownPas, source.ID };

                string query = String.Format(format, args);

                SqlCommand cmd = new SqlCommand(query, repo);

                cmd.ExecuteNonQuery();

                return new ReturnMessage() { WasSuccesful = true, Message = "No Problems", Where = "UpdateAbility", DBState = repo.State };
            }
            catch (Exception e)
            {
                return new ReturnMessage() { WasSuccesful = false, Message = "Something went awry", Exception = e, Where = "UpdateAbility", DBState = repo.State };
            }
            finally
            {
                repo.Close();
            }
        }

        public Tuple<List<Ability>, ReturnMessage> GetAbilities()
        {
            try
            {
                if (repo.State != ConnectionState.Open)
                {
                    repo.Open();
                }
                List<Ability> outputList = new List<Ability>();
                List<Resource> resourceList = GetResources().Item1;
                if (repo.State != ConnectionState.Open)
                {
                    repo.Open();
                }

                string query = String.Format("SELECT * FROM dbo.{0}", tables[1]);
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
                        HavePassive = (bool)row["HavePassive"],
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

                ReturnMessage message = new ReturnMessage() { WasSuccesful = true, Message = "No Problems", Where = "GetAbilities", DBState = repo.State };

                return new Tuple<List<Ability>, ReturnMessage>(outputList, message);
            }
            catch (Exception e)
            {
                ReturnMessage message = new ReturnMessage() { WasSuccesful = false, Message = "Something went awry", Exception = e, Where = "GetAbilities", DBState = repo.State };

                return new Tuple<List<Ability>, ReturnMessage>(null, message);
            }
            finally
            {
                repo.Close();
            }
        }

        public ReturnMessage NewAbility(Ability source)
        {
            try
            {
                if (repo.State != ConnectionState.Open)
                {
                    repo.Open();
                }
                string format = "INSERT INTO dbo.{0} (ID, ResourceID, Name, AbilitySlot, HaveActive, IsToogleAble, " +
                        "HaveEmpoweredOrAlternative, HavePassive, DescriptionAct, DamageAct, CooldownAct, RangeAct, ResourceCostAct, " +
                        "DescriptionEmpAlt, DamageEmpAlt, CooldownEmpAlt, RangeEmpAlt, ResourceCostEmpAlt, DescriptionPas, RangePas, DamagePas, CooldownPas) " +
                       "VALUES (@ID, @ResourceID, @Name, @AbilitySlot, @HaveActive, @IsToogleAble, @HaveEmpoweredOrAlternative, @HavePassive, " +
                       "@DescriptionAct, @DamageAct, @CooldownAct, @RangeAct, @ResourceCostAct, " +
                       "@DescriptionEmpAlt, @DamageEmpAlt, @CooldownEmpAlt, @RangeEmpAlt, @ResourceCostEmpAlt, " +
                       "@DescriptionPas, @RangePas, @DamagePas, @CooldownPas)";
                object[] args = {tables[1]};

                string query = String.Format(format, args);

                SqlCommand cmd = new SqlCommand(query, repo);

                cmd.Parameters.AddWithValue("@Id", source.ID);
                cmd.Parameters.AddWithValue("@ResourceID", source.ResourceUse.ID);
                cmd.Parameters.AddWithValue("@Name", source.Name);
                cmd.Parameters.AddWithValue("@AbilitySlot", source.Slot);
                cmd.Parameters.AddWithValue("@HaveActive", source.HaveActive);
                cmd.Parameters.AddWithValue("@IsToogleAble", source.IsToogleAble);
                cmd.Parameters.AddWithValue("@HaveEmpoweredOrAlternative", source.HaveEmpoweredOrAlternative);
                cmd.Parameters.AddWithValue("@HavePassive", source.HavePassive);
                cmd.Parameters.AddWithValue("@DescriptionAct", source.DescriptionAct);
                cmd.Parameters.AddWithValue("@DamageAct", source.DamageAct);
                cmd.Parameters.AddWithValue("@CooldownAct", source.CooldownAct);
                cmd.Parameters.AddWithValue("@RangeAct", source.RangeAct);
                cmd.Parameters.AddWithValue("@ResourceCostAct", source.ResourceCostAct);
                cmd.Parameters.AddWithValue("@DescriptionEmpAlt", source.DescriptionEmpAlt);
                cmd.Parameters.AddWithValue("@DamageEmpAlt", source.DamageEmpAlt);
                cmd.Parameters.AddWithValue("@CooldownEmpAlt", source.CooldownEmpAlt);
                cmd.Parameters.AddWithValue("@RangeEmpAlt", source.RangeEmpAlt);
                cmd.Parameters.AddWithValue("@ResourceCostEmpAlt", source.ResourceCostEmpAlt);
                cmd.Parameters.AddWithValue("@DescriptionPas", source.DescriptionPas);
                cmd.Parameters.AddWithValue("@RangePas", source.RangePas);
                cmd.Parameters.AddWithValue("@DamagePas", source.DamagePas);
                cmd.Parameters.AddWithValue("@CooldownPas", source.CooldownPas);

                cmd.ExecuteNonQuery();

                return new ReturnMessage() { WasSuccesful = true, Message = "No Problems", Where = "NewAbility", DBState = repo.State };
            }
            catch (Exception e)
            {
                return new ReturnMessage() { WasSuccesful = false, Message = "Something went awry", Exception = e, Where = "NewAbility", DBState = repo.State };
            }
            finally
            {
                repo.Close();
            }
        }

        public ReturnMessage DeleteAbility(Ability source)
        {
            try
            {
                if (repo.State != ConnectionState.Open)
                {
                    repo.Open();
                }
                string query = String.Format(@"DELETE FROM {0} WHERE Id='{1}'", tables[1], source.ID);

                SqlCommand cmd = new SqlCommand(query, repo);
                cmd.ExecuteNonQuery();

                return new ReturnMessage() { WasSuccesful = true, Message = "No Problems", Where = "DeleteAbility", DBState = repo.State };
            }
            catch (Exception e)
            {
                return new ReturnMessage() { WasSuccesful = false, Message = "Something went awry", Exception = e, Where = "DeleteAbility", DBState = repo.State };
            }
            finally
            {
                repo.Close();
            }
        }
        #endregion

        #region Champion Handeling
        public ReturnMessage UpdateChampion(Champion source)
        {
            try
            {
                if (repo.State != ConnectionState.Open)
                {
                    repo.Open();
                }
                string format = "@UPDATE {0} SET ResourceID = '{1}', PassiveID = '{2}', QID = '{3}', WID = '{4}', EID = '{5}', " +
                       "RID = '{6}', Name = '{7}', HealthStart = '{8}', HealthGrowth = '{9}', HealthRegenStart = '{10}', HealthRegenGrowth = '{11}', " +
                       "ResourceStart = '{12}', ResourceGrowth = '{13}', ResourceRegenStart = '{14}', ResourceRegenGrowth = '{15}', " +
                       "AttackDamageStart = '{16}', AttackDamageGrowth = '{17}', AbilityPowerStart = '{18}', AbilityPowerGrowth = '{19}', " +
                       "AttackSpeedStart = '{20}', AttackSpeedGrowth = '{21}', RangeStart = '{22}', RangeGrowth = '{23}', " +
                       "CriticalStrikeChanceStart = '{24}', CriticalStrikeChanceGrowth = '{25}', ArmorStart = '{26}', ArmorGrowth = '{27}', " +
                       "MagicResistStart = '{28}', MagicResistGrowth = '{29}', MoveSpeedStart = '{30}', MoveSpeedGrowth = '{31}' WHERE ID = {32}"; 
                object[] args = {tables[2], source.Resource.ID, source.PassiveAbility.ID, source.QAbility.ID,
                    source.WAbility.ID, source.EAbility.ID, source.RAbility.ID, source.Name,
                    source.HealthStart, source.HealthGrowth, source.HealthRegenStart, source.HealthRegenGrowth,
                    source.ResourceStart, source.ResourceGrowth, source.ResourceRegenStart, source.ResourceRegenGrowth,
                    source.AttackDamageStart, source.AttackDamageGrowth, source.AbilityPowerStart, source.AbilityPowerGrowth,
                    source.AttackSpeedStart, source.AttackSpeedGrowth, source.RangeStart, source.RangeGrowth,
                    source.CriticalStrikeChanceStart, source.CriticalStrikeChanceGrowth, source.ArmorStart, source.ArmorGrowth,
                    source.MagicResistStart, source.MagicResistGrowth, source.MoveSpeedStart, source.MoveSpeedGrowth, source.ID};

                string query = String.Format(format, args);

                SqlCommand cmd = new SqlCommand(query, repo);

                cmd.ExecuteNonQuery();

                return new ReturnMessage() { WasSuccesful = true, Message = "No Problems", Where = "UpdateChampion", DBState = repo.State };
            }
            catch (Exception e)
            {
                return new ReturnMessage() { WasSuccesful = false, Message = "Something went awry", Exception = e, Where = "UpdateChampion", DBState = repo.State };
            }
            finally
            {
                repo.Close();
            }
        }
        public Tuple<List<Champion>, ReturnMessage> GetChampions()
        {
            try
            {
                if (repo.State != ConnectionState.Open)
                {
                    repo.Open();
                }
                List<Champion> outputList = new List<Champion>();
                List<Ability> abilityList = GetAbilities().Item1;
                List<Resource> resourceList = GetResources().Item1;
                if (repo.State != ConnectionState.Open)
                {
                    repo.Open();
                }

                string query = String.Format("SELECT * FROM dbo.{0}", tables[2]);
                SqlDataAdapter adapt = new SqlDataAdapter(query, repo);

                DataSet database = new DataSet();
                adapt.Fill(database, tables[2]);

                foreach (DataRow row in database.Tables[tables[2]].Rows)
                {
                    Champion reCreate = new Champion()
                    {
                        ID = (int)row["ID"],
                        Resource = resourceList.Find(x => x.ID == (int)row["ResourceID"]),
                        PassiveAbility = abilityList.Find(x => x.ID == (int)row["PassiveID"]),
                        QAbility = abilityList.Find(x => x.ID == (int)row["QID"]),
                        WAbility = abilityList.Find(x => x.ID == (int)row["WID"]),
                        EAbility = abilityList.Find(x => x.ID == (int)row["EID"]),
                        RAbility = abilityList.Find(x => x.ID == (int)row["RID"]),
                        Name = row["Name"].ToString().Trim(' '),
                        HealthStart = row["HealthStart"].ToString().Trim(' '),
                        HealthGrowth = row["HealthGrowth"].ToString().Trim(' '),
                        HealthRegenStart = row["HealthRegenStart"].ToString().Trim(' '),
                        HealthRegenGrowth = row["HealthRegenGrowth"].ToString().Trim(' '),
                        ResourceStart = row["ResourceStart"].ToString().Trim(' '),
                        ResourceGrowth = row["ResourceGrowth"].ToString().Trim(' '),
                        ResourceRegenStart = row["ResourceRegenStart"].ToString().Trim(' '),
                        ResourceRegenGrowth = row["ResourceRegenGrowth"].ToString().Trim(' '),
                        AttackDamageStart = row["AttackDamageStart"].ToString().Trim(' '),
                        AttackDamageGrowth = row["AttackDamageGrowth"].ToString().Trim(' '),
                        AbilityPowerStart = row["AbilityPowerStart"].ToString().Trim(' '),
                        AbilityPowerGrowth = row["AbilityPowerGrowth"].ToString().Trim(' '),
                        AttackSpeedStart = row["AttackSpeedStart"].ToString().Trim(' '),
                        AttackSpeedGrowth = row["AttackSpeedGrowth"].ToString().Trim(' '),
                        RangeStart = row["RangeStart"].ToString().Trim(' '),
                        RangeGrowth = row["RangeGrowth"].ToString().Trim(' '),
                        CriticalStrikeChanceStart = row["CriticalStrikeChanceStart"].ToString().Trim(' '),
                        CriticalStrikeChanceGrowth = row["CriticalStrikeChanceGrowth"].ToString().Trim(' '),
                        ArmorStart = row["ArmorStart"].ToString().Trim(' '),
                        ArmorGrowth = row["ArmorGrowth"].ToString().Trim(' '),
                        MagicResistStart = row["MagicResistStart"].ToString().Trim(' '),
                        MagicResistGrowth = row["MagicResistGrowth"].ToString().Trim(' '),
                        MoveSpeedStart = row["MoveSpeedStart"].ToString().Trim(' '),
                        MoveSpeedGrowth = row["MoveSpeedGrowth"].ToString().Trim(' ')
                    };

                    outputList.Add(reCreate);
                }

                ReturnMessage message = new ReturnMessage() { WasSuccesful = true, Message = "No Problems", Where = "GetChampions", DBState = repo.State };

                return new Tuple<List<Champion>, ReturnMessage>(outputList, message);
            }
            catch (Exception e)
            {
                ReturnMessage message = new ReturnMessage() { WasSuccesful = false, Message = "Something went awry", Exception = e, Where = "GetChampions", DBState = repo.State };

                return new Tuple<List<Champion>, ReturnMessage>(null, message);
            }
            finally
            {
                repo.Close();
            }
        }
        public ReturnMessage NewChampion(Champion source)
        {
            try
            {
                if (repo.State != ConnectionState.Open)
                {
                    repo.Open();
                }
                string format = "INSERT INTO dbo.{0} (Id, ResourceID, PassiveID, QID, WID, EID, " +
                        "RID, Name, HealthStart, HealthGrowth, HealthRegenStart, HealthRegenGrowth, " +
                        "ResourceStart, ResourceGrowth, ResourceRegenStart, ResourceRegenGrowth, " +
                        "AttackDamageStart, AttackDamageGrowth, AbilityPowerStart, AbilityPowerGrowth, " +
                        "AttackSpeedStart, AttackSpeedGrowth, RangeStart, RangeGrowth, " +
                        "CriticalStrikeChanceStart, CriticalStrikeChanceGrowth, ArmorStart, ArmorGrowth, " +
                        "MagicResistStart, MagicResistGrowth, MoveSpeedStart, MoveSpeedGrowth) " +
                       "VALUES ('{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', " +
                       "'{12}', '{13}', '{14}', '{15}', '{16}', '{17}', '{18}', '{19}', '{20}', '{21}', '{22}', " +
                       "'{23}', '{24}', '{25}', '{26}', '{27}', '{28}', '{29}', '{30}', '{31}', '{32}')";
                object[] args = {tables[2], source.ID, source.Resource.ID, source.PassiveAbility.ID, source.QAbility.ID,
                    source.WAbility.ID, source.EAbility.ID, source.RAbility.ID, source.Name,
                    source.HealthStart, source.HealthGrowth, source.HealthRegenStart, source.HealthRegenGrowth,
                    source.ResourceStart, source.ResourceGrowth, source.ResourceRegenStart, source.ResourceRegenGrowth,
                    source.AttackDamageStart, source.AttackDamageGrowth, source.AbilityPowerStart, source.AbilityPowerGrowth,
                    source.AttackSpeedStart, source.AttackSpeedGrowth, source.RangeStart, source.RangeGrowth,
                    source.CriticalStrikeChanceStart, source.CriticalStrikeChanceGrowth, source.ArmorStart, source.ArmorGrowth,
                    source.MagicResistStart, source.MagicResistGrowth, source.MoveSpeedStart, source.MoveSpeedGrowth};

                string query = String.Format(format, args);

                SqlCommand cmd = new SqlCommand(query, repo);

                cmd.ExecuteNonQuery();

                return new ReturnMessage() { WasSuccesful = true, Message = "No Problems", Where = "NewChampion", DBState = repo.State };
            }
            catch (Exception e)
            {
                return new ReturnMessage() { WasSuccesful = false, Message = "Something went awry", Exception = e, Where = "NewChampion", DBState = repo.State };
            }
            finally
            {
                repo.Close();
            }
        }
        public ReturnMessage DeleteChampion(Champion source)
        {
            try
            {
                if (repo.State != ConnectionState.Open)
                {
                    repo.Open();
                }
                string query = String.Format(@"DELETE FROM {0} WHERE Id='{1}'", tables[2], source.ID);

                SqlCommand cmd = new SqlCommand(query, repo);
                cmd.ExecuteNonQuery();

                return new ReturnMessage() { WasSuccesful = true, Message = "No Problems", Where = "DeleteChampion", DBState = repo.State };
            }
            catch (Exception e)
            {
                return new ReturnMessage() { WasSuccesful = false, Message = "Something went awry", Exception = e, Where = "DeleteChampion", DBState = repo.State };
            }
            finally
            {
                repo.Close();
            }
        }
        #endregion

        #region Test Methods
        public string GetData(int value)
        {
            return string.Format("you entered: {0}", value);
        }
        #endregion
    }
}
