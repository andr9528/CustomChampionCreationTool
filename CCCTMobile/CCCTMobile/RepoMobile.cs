using CCCTLibrary;
using FacadeService;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;


namespace CCCTMobile
{
    public static class RepoMobile
    {
        public static FacadeClient Facade;

        public static List<string> ResourceNamesList = new List<string>();
        public static List<string> AbilityNamesList = new List<string>();
        public static List<string> ChampionNamesList = new List<string>();
        public static List<FacadeService.Resource> ResourceList = new List<FacadeService.Resource>();
        public static List<FacadeService.Ability> AbilitiesList = new List<FacadeService.Ability>();
        public static List<FacadeService.Champion> ChampionList = new List<FacadeService.Champion>();

        internal static async Task<List<FacadeService.ReturnMessage>> Initialize()
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback +=
            (se, cert, chain, sslerror) => { return true; };

            TimeSpan timeout = new TimeSpan(0, 5, 0);
            BasicHttpBinding binding = new BasicHttpBinding()
            {
                CloseTimeout = timeout, OpenTimeout = timeout, ReceiveTimeout = timeout, SendTimeout = timeout,
                MaxReceivedMessageSize = 2147483647, MaxBufferSize = 2147483647, MaxBufferPoolSize = 2147483647,
                ReaderQuotas = new System.Xml.XmlDictionaryReaderQuotas()
                {
                    MaxStringContentLength = 2147483647, MaxArrayLength = 2147483647, MaxBytesPerRead = 2147483647, MaxDepth = 32, MaxNameTableCharCount = 2147483647
                },
                Name = "BasicBinding",
            };
            binding.Security.Mode = BasicHttpSecurityMode.Transport;

            Facade = new FacadeClient(binding, new EndpointAddress("http://www12.steenhoff.dk/Facade.svc"));


            /*
             * Returns 'Unhandled Exception: System.InvalidOperationException: Operation 'GetDataAsync' contains a message with parameters. 
                Strongly-typed or untyped message can be paired only with strongly-typed, untyped or void message.' occurred When
                Client Credentials has not been enabled / set up.

             * Returns 'Unhandled Exception: System.NotImplementedException: The method or operation is not implemented. occurred When
             * Client Credentials has been enabled / set up.
             * 
             * Missing something on the service to handle the credentials?
             */


            InitializeResponse response = await Facade.InitializeAsync(new InitializeRequest());

            List<FacadeService.ReturnMessage> output = new List<FacadeService.ReturnMessage>
            {
                response.InitializeResult,
                await UpdateAvailableResourcesAsync(),
                await UpdateAvailableAbilitiesAsync(),
                await UpdateAvailableChampionsAsync()
            };

            return output;
        }
        #region Local Update Methods
        internal static async Task<FacadeService.ReturnMessage> UpdateAvailableResourcesAsync()
        {
            FacadeService.ReturnMessage dummy = new FacadeService.ReturnMessage() { Message = "Null" };
            Tuple<List<FacadeService.Resource>, FacadeService.ReturnMessage> run =
                Tuple.Create(new List<FacadeService.Resource>(), dummy);

            try
            {
                GetResourcesResponse response = await Facade.GetResourcesAsync(new GetResourcesRequest());
                run = Tuple.Create(new List<FacadeService.Resource>(response.GetResourcesResult.m_Item1), response.GetResourcesResult.m_Item2);

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
        internal static async Task<FacadeService.ReturnMessage> UpdateAvailableAbilitiesAsync()
        {
            FacadeService.ReturnMessage dummy = new FacadeService.ReturnMessage() { Message = "Null" };
            Tuple<List<FacadeService.Ability>, FacadeService.ReturnMessage> run =
                Tuple.Create(new List<FacadeService.Ability>(), dummy);

            try
            {
                GetAbilitiesResponse response = await Facade.GetAbilitiesAsync(new GetAbilitiesRequest());
                run = Tuple.Create(new List<FacadeService.Ability>(response.GetAbilitiesResult.m_Item1), response.GetAbilitiesResult.m_Item2);

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

        internal static async Task<FacadeService.ReturnMessage> UpdateAvailableChampionsAsync()
        {
            FacadeService.ReturnMessage dummy = new FacadeService.ReturnMessage() { Message = "Null" };
            Tuple<List<FacadeService.Champion>, FacadeService.ReturnMessage> run =
                Tuple.Create(new List<FacadeService.Champion>(), dummy);

            try
            {
                GetChampionsResponse response = await Facade.GetChampionsAsync(new GetChampionsRequest());
                run = Tuple.Create(new List<FacadeService.Champion>(response.GetChampionsResult.m_Item1), response.GetChampionsResult.m_Item2);

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
        internal static async Task<Tuple<List<FacadeService.Resource>, FacadeService.ReturnMessage>> GetResourcesAsync()
        {
            FacadeService.ReturnMessage dummy = new FacadeService.ReturnMessage() { Message = "Null" };
            Tuple<List<FacadeService.Resource>, FacadeService.ReturnMessage> run =
                Tuple.Create(new List<FacadeService.Resource>(), dummy);

            GetResourcesResponse response = await Facade.GetResourcesAsync(new GetResourcesRequest());
            run = Tuple.Create(new List<FacadeService.Resource>(response.GetResourcesResult.m_Item1), response.GetResourcesResult.m_Item2);

            return run;
        }

        internal static async Task<Tuple<List<FacadeService.Ability>, FacadeService.ReturnMessage>> GetAbilitiesAsync()
        {
            FacadeService.ReturnMessage dummy = new FacadeService.ReturnMessage() { Message = "Null" };
            Tuple<List<FacadeService.Ability>, FacadeService.ReturnMessage> run =
                Tuple.Create(new List<FacadeService.Ability>(), dummy);

            GetAbilitiesResponse response = await Facade.GetAbilitiesAsync(new GetAbilitiesRequest());
            run = Tuple.Create(new List<FacadeService.Ability>(response.GetAbilitiesResult.m_Item1), response.GetAbilitiesResult.m_Item2);

            return run;
        }

        internal static async Task<Tuple<List<FacadeService.Champion>, FacadeService.ReturnMessage>> GetChampionsAsync()
        {
            FacadeService.ReturnMessage dummy = new FacadeService.ReturnMessage() { Message = "Null" };
            Tuple<List<FacadeService.Champion>, FacadeService.ReturnMessage> run =
                Tuple.Create(new List<FacadeService.Champion>(), dummy);

            GetChampionsResponse response = await Facade.GetChampionsAsync(new GetChampionsRequest());
            run = Tuple.Create(new List<FacadeService.Champion>(response.GetChampionsResult.m_Item1), response.GetChampionsResult.m_Item2);

            return run;
        }
        #endregion

        #region Regenerate
        //private static Tuple<List<CCCTLibrary.Resource>, CCCTLibrary.ReturnMessage> RegenerateResources(GetResourcesResponse response)
        //{
        //    CCCTLibrary.ReturnMessage message = RegenerateMessage(response.GetResourcesResult.m_Item2);
        //    List<CCCTLibrary.Resource> list = new List<CCCTLibrary.Resource>();

        //    foreach (FacadeService.Resource item in response.GetResourcesResult.m_Item1)
        //    {

        //    }

        //    return Tuple.Create(list, message);
        //}

        //private static Tuple<List<CCCTLibrary.Ability>, CCCTLibrary.ReturnMessage> RegenerateAbilities(GetAbilitiesResponse response)
        //{
        //    CCCTLibrary.ReturnMessage message = RegenerateMessage(response.GetAbilitiesResult.m_Item2);
        //    List<CCCTLibrary.Ability> list = new List<CCCTLibrary.Ability>();

        //    foreach (FacadeService.Ability item in response.GetAbilitiesResult.m_Item1)
        //    {

        //    }

        //    return Tuple.Create(list, message);
        //}

        //private static Tuple<List<CCCTLibrary.Champion>, CCCTLibrary.ReturnMessage> RegenerateChampions(GetChampionsResponse response)
        //{
        //    CCCTLibrary.ReturnMessage message = RegenerateMessage(response.GetChampionsResult.m_Item2);
        //    List<CCCTLibrary.Champion> list = new List<CCCTLibrary.Champion>();

        //    foreach (FacadeService.Champion item in response.GetChampionsResult.m_Item1)
        //    {

        //    }

        //    return Tuple.Create(list, message);
        //}

        //private static CCCTLibrary.ReturnMessage RegenerateMessage(FacadeService.ReturnMessage item)
        //{
        //    /* Unable to extract 2 values from the 'item'
        //     *  Exception & DBScheme
        //     *  Reason: Cannot implicitly convert type 'FacadeService.x' to 'System.x'
        //     */ 

        //    CCCTLibrary.ReturnMessage output = new CCCTLibrary.ReturnMessage()
        //    {
        //        Message = item.Message,
        //        WasSuccesful = item.WasSuccesful,
        //        ChainMessage = RegenerateMessage(item.ChainMessage),
        //        Where = item.Where,
        //        DBState = (System.Data.ConnectionState)item.DBState
        //    };

        //    return output;
        //}
        #endregion
    }
}
