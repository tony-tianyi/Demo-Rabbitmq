using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace Test_Ewma
{
    public static class DbDapper
    {

        public static string ConnectionString
        {
            get { return System.Configuration.ConfigurationManager.ConnectionStrings["ConString_JK"].ConnectionString; }
        }

       
           
        
    }
}