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
using CCCTLibrary;

namespace CustomChampionCreationTool
{
    public static class Repo
    {
        public static FacadeService.FacadeClient Facade = new FacadeService.FacadeClient();

        internal static ReturnMessage Initialize()
        {
            return Facade.Initialize();
        }
        internal static ReturnMessage UpdateResource(Resource source)
        {
            return Facade.UpdateResource(source);
        }
        internal static Tuple<List<Ability>, ReturnMessage> GetAbilities()
        {
            return Facade.GetAbilities();
        }
        internal static Tuple<List<Resource>, ReturnMessage> GetResources()
        {
            return Facade.GetResources();
        }
        internal static ReturnMessage NewAbility(Ability source)
        {
            return Facade.NewAbility(source);
        }
        internal static ReturnMessage NewResource(Resource source)
        {
            return Facade.NewResource(source);
        }
        internal static ReturnMessage DeleteResource(Resource source)
        {
            return Facade.DeleteResource(source);
        }
        internal static ReturnMessage DeleteAbility(Ability source)
        {
            return Facade.DeleteAbility(source);
        }
    }
}
