using CCCTLibrary;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace CCCTDBFacade
{
    [ServiceContract]
    public interface IFacade
    {
        #region Test Methods & Initialize
        [OperationContract]
        string GetData(int value);

        [OperationContract]
        ReturnMessage Initialize();

        #endregion

        #region Resource Handeling
        [OperationContract]
        ReturnMessage UpdateResource(Resource source);

        [OperationContract]
        Tuple<List<Resource>, ReturnMessage> GetResources();

        [OperationContract]
        ReturnMessage NewResource(Resource source);

        [OperationContract]
        ReturnMessage DeleteResource(Resource source);

        #endregion

        #region Ability Handeling
        [OperationContract]
        ReturnMessage UpdateAbility(Ability source);

        [OperationContract]
        Tuple<List<Ability>, ReturnMessage> GetAbilities();

        [OperationContract]
        ReturnMessage NewAbility(Ability source);

        [OperationContract]
        ReturnMessage DeleteAbility(Ability source);

        #endregion

        #region Champion Handeling
        [OperationContract]
        ReturnMessage UpdateChampion(Champion source);

        [OperationContract]
        Tuple<List<Champion>, ReturnMessage> GetChampions();

        [OperationContract]
        ReturnMessage NewChampion(Champion source);

        [OperationContract]
        ReturnMessage DeleteChampion(Champion source);  
        
        #endregion
    }




}
