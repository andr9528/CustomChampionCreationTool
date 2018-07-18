using System;
using System.Collections.Generic;
using CCCTLibrary;

namespace CustomChampionCreationTool
{
    public static class RepoPC
    {
        public static FacadeService.FacadeClient Facade = new FacadeService.FacadeClient();

        public static List<string> ResourceNamesList = new List<string>();
        public static List<string> AbilityNamesList = new List<string>();
        public static List<string> ChampionNamesList = new List<string>();
        public static List<Resource> ResourceList = new List<Resource>();
        public static List<Ability> AbilitiesList = new List<Ability>();
        public static List<Champion> ChampionList = new List<Champion>();

        internal static List<ReturnMessage> Initialize()
        {
            List<ReturnMessage> output = new List<ReturnMessage>
            {
                Facade.Initialize(),
                UpdateAvailableResources(),
                UpdateAvailableAbilities(),
                UpdateAvailableChampions()
            };

            return output;
        }
        #region Local Update Methods
        internal static ReturnMessage UpdateAvailableResources()
        {
            ReturnMessage dummy = new ReturnMessage() { Message = "Null" };
            Tuple<List<Resource>, ReturnMessage> run =
                Tuple.Create(new List<Resource>(), dummy);

            try
            {
                run = Facade.GetResources();

                if (run.Item1 != null)
                {
                    ResourceList = run.Item1; 
                }
                ResourceNamesList.Clear();

                foreach (Resource item in ResourceList)
                {
                    ResourceNamesList.Add(item.ToStringR());
                }

                return new ReturnMessage() { WasSuccesful = true, Message = "No Problems", Where = "UpdateAvailableResources", ChainMessage = run.Item2}; 
            }
            catch (Exception e)
            {
                return new ReturnMessage() { WasSuccesful = false, Message = "Something went awry", Exception = e, Where = "UpdateAvailableResources", ChainMessage = run.Item2};
            }
        }
        internal static ReturnMessage UpdateAvailableAbilities()
        {
            ReturnMessage dummy = new ReturnMessage() { Message = "Null" };
            Tuple<List<Ability>, ReturnMessage> run =
                Tuple.Create(new List<Ability>(), dummy);

            try
            {
                run = Facade.GetAbilities();

                if (run.Item1 != null)
                {
                    AbilitiesList = run.Item1; 
                }
                AbilityNamesList.Clear();

                foreach (Ability item in AbilitiesList)
                {
                    AbilityNamesList.Add(item.ToStringA());
                }

                return new ReturnMessage() { WasSuccesful = true, Message = "No Problems", Where = "UpdateAvailableAbilities", ChainMessage = run.Item2 };
            }
            catch (Exception e)
            {
                return new ReturnMessage() { WasSuccesful = false, Message = "Something went awry", Exception = e, Where = "UpdateAvailableAbilities", ChainMessage = run.Item2 };
            }
        }
        
        internal static ReturnMessage UpdateAvailableChampions()
        {
            ReturnMessage dummy = new ReturnMessage() { Message = "Null" };
            Tuple<List<Champion>, ReturnMessage> run = 
                Tuple.Create(new List<Champion>(), dummy);

            try
            {
                run = Facade.GetChampions();

                if (run.Item1 != null)
                {
                    ChampionList = run.Item1;
                }
                ChampionNamesList.Clear();

                foreach (Champion item in ChampionList)
                {
                    ChampionNamesList.Add(item.ToStringC());
                }

                return new ReturnMessage() { WasSuccesful = true, Message = "No Problems", Where = "UpdateAvailableChampions", ChainMessage = run.Item2 };
            }
            catch (Exception e)
            {
                return new ReturnMessage() { WasSuccesful = false, Message = "Something went awry", Exception = e, Where = "UpdateAvailableChampions", ChainMessage = run.Item2 };
            }
        }
        #endregion

        #region Database Handeling
        #region Update
        internal static ReturnMessage UpdateResource(Resource source)
        {
            return Facade.UpdateResource(source);
        }
        internal static ReturnMessage UpdateAbility(Ability source)
        {
            return Facade.UpdateAbility(source);
        }
        internal static ReturnMessage UpdateChampion(Champion source)
        {
            return Facade.UpdateChampion(source);
        }
        #endregion

        #region Get
        internal static Tuple<List<Ability>, ReturnMessage> GetAbilities()
        {
            return Facade.GetAbilities();
        }
        internal static Tuple<List<Resource>, ReturnMessage> GetResources()
        {
            return Facade.GetResources();
        }
        internal static Tuple<List<Champion>, ReturnMessage> GetChampions()
        {
            return Facade.GetChampions();
        }
        #endregion

        #region New
        internal static ReturnMessage NewAbility(Ability source)
        {
            return Facade.NewAbility(source);
        }
        internal static ReturnMessage NewResource(Resource source)
        {
            return Facade.NewResource(source);
        }
        internal static ReturnMessage NewChampion(Champion source)
        {
            return Facade.NewChampion(source);
        }
        #endregion

        #region Delete
        internal static ReturnMessage DeleteResource(Resource source)
        {
            return Facade.DeleteResource(source);
        }
        internal static ReturnMessage DeleteAbility(Ability source)
        {
            return Facade.DeleteAbility(source);
        }
        internal static ReturnMessage DeleteChampion(Champion source)
        {
            return Facade.DeleteChampion(source);
        }
        #endregion
        #endregion
    }
}
