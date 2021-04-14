using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeDeliveryDB_API.Data
{
    public class ElephantSqlConnectionString
    {
            public static string Get()
            {
                var uriString = "postgres://qgioncvt:Sabna1o15etkmINxYVBV4CRPMdVr_IWz@satao.db.elephantsql.com:5432/qgioncvt";
                var uri = new Uri(uriString);
                var db = uri.AbsolutePath.Trim('/');
                var user = uri.UserInfo.Split(':')[0];
                var passwd = uri.UserInfo.Split(':')[1];
                var port = uri.Port > 0 ? uri.Port : 5432;
                var connStr = string.Format("Server=satao.db.elephantsql.com;Database=qgioncvt;User Id=qgioncvt;Password=Sabna1o15etkmINxYVBV4CRPMdVr_IWz;Port=5432",
                    uri.Host, db, user, passwd, port);
                return connStr;
            //postgres://gjefcuqh:ToAnyBPY0JfrVDn4W8OcjkeE7RyBCJmE@satao.db.elephantsql.com:5432/gjefcuqh

            //Server=satao.db.elephantsql.com;Database=gjefcuqh;User Id=gjefcuqh;Password=ToAnyBPY0JfrVDn4W8OcjkeE7RyBCJmE ;Port=5432
        }
    }
}
