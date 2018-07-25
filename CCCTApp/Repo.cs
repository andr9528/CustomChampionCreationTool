using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using CCCTApp.FacadeService;
using CCCTLibrary;

namespace CCCTApp
{
    public static class Repo
    {
        public static readonly Facade Facade = new Facade();

        public static List<string> ResourceNamesList = new List<string>();
        public static List<string> AbilityNamesList = new List<string>();
        public static List<string> ChampionNamesList = new List<string>();
        public static List<FacadeService.Resource> ResourceList = new List<FacadeService.Resource>();
        public static List<FacadeService.Ability> AbilitiesList = new List<FacadeService.Ability>();
        public static List<FacadeService.Champion> ChampionList = new List<FacadeService.Champion>();

        internal static List<FacadeService.ReturnMessage> Initialize()
        {
            List<FacadeService.ReturnMessage> output = new List<FacadeService.ReturnMessage>
            {
                Facade.Initialize(),
                UpdateAvailableResources(),
                UpdateAvailableAbilities(),
                UpdateAvailableChampions(),
            };

            return output;
        }

        

        #region Local Update Methods
        internal static FacadeService.ReturnMessage UpdateAvailableResources()
        {
            FacadeService.ReturnMessage dummy = new FacadeService.ReturnMessage() { Message = "Null" };
            Tuple<List<FacadeService.Resource>, FacadeService.ReturnMessage> run =
                Tuple.Create(new List<FacadeService.Resource>(), dummy);

            try
            {
                var data = Facade.GetResources();
                run = Tuple.Create(new List<FacadeService.Resource>(data.m_Item1), data.m_Item2);

                if (run.Item1 != null)
                {
                    ResourceList = run.Item1;
                }
                ResourceNamesList.Clear();

                foreach (FacadeService.Resource item in ResourceList)
                {
                    ResourceNamesList.Add(item.Name);
                }

                return new FacadeService.ReturnMessage() { WasSuccesful = true, Message = "No Problems", Where = "UpdateAvailableResources", ChainMessage = run.Item2 };
            }
            catch (System.Exception e)
            {
                return new FacadeService.ReturnMessage() { WasSuccesful = false, Message = e.ToString(), Where = "UpdateAvailableResources", ChainMessage = run.Item2 };
            }
        }
        internal static FacadeService.ReturnMessage UpdateAvailableAbilities()
        {
            FacadeService.ReturnMessage dummy = new FacadeService.ReturnMessage() { Message = "Null" };
            Tuple<List<FacadeService.Ability>, FacadeService.ReturnMessage> run =
                Tuple.Create(new List<FacadeService.Ability>(), dummy);

            try
            {
                var data = Facade.GetAbilities();
                run = Tuple.Create(new List<FacadeService.Ability>(data.m_Item1), data.m_Item2);

                if (run.Item1 != null)
                {
                    AbilitiesList = run.Item1;
                }
                AbilityNamesList.Clear();

                foreach (FacadeService.Ability item in AbilitiesList)
                {
                    AbilityNamesList.Add(item.Name);
                }

                return new FacadeService.ReturnMessage() { WasSuccesful = true, Message = "No Problems", Where = "UpdateAvailableAbilities", ChainMessage = run.Item2 };
            }
            catch (System.Exception e)
            {
                return new FacadeService.ReturnMessage() { WasSuccesful = false, Message = e.ToString(), Where = "UpdateAvailableAbilities", ChainMessage = run.Item2 };
            }
        }

        internal static FacadeService.ReturnMessage UpdateAvailableChampions()
        {
            FacadeService.ReturnMessage dummy = new FacadeService.ReturnMessage() { Message = "Null" };
            Tuple<List<FacadeService.Champion>, FacadeService.ReturnMessage> run =
                Tuple.Create(new List<FacadeService.Champion>(), dummy);

            try
            {
                var data = Facade.GetChampions();
                run = Tuple.Create(new List<FacadeService.Champion>(data.m_Item1), data.m_Item2);

                if (run.Item1 != null)
                {
                    ChampionList = run.Item1;
                }
                ChampionNamesList.Clear();

                foreach (FacadeService.Champion item in ChampionList)
                {
                    ChampionNamesList.Add(item.Name);
                }

                return new FacadeService.ReturnMessage() { WasSuccesful = true, Message = "No Problems", Where = "UpdateAvailableChampions", ChainMessage = run.Item2 };
            }
            catch (System.Exception e)
            {
                return new FacadeService.ReturnMessage() { WasSuccesful = false, Message = e.ToString(), Where = "UpdateAvailableChampions", ChainMessage = run.Item2 };
            }
        }
        #endregion
        #region Get
        internal static Tuple<List<FacadeService.Ability>, FacadeService.ReturnMessage> GetAbilities()
        {
            var data = Facade.GetAbilities();
            return Tuple.Create(new List<FacadeService.Ability>(data.m_Item1), data.m_Item2);
        }
        internal static Tuple<List<FacadeService.Resource>, FacadeService.ReturnMessage> GetResources()
        {
            var data = Facade.GetResources();
            return Tuple.Create(new List<FacadeService.Resource>(data.m_Item1), data.m_Item2);
        }
        internal static Tuple<List<FacadeService.Champion>, FacadeService.ReturnMessage> GetChampions()
        {
            var data = Facade.GetChampions();
            return Tuple.Create(new List<FacadeService.Champion>(data.m_Item1), data.m_Item2);
        }
        #endregion
    }
}