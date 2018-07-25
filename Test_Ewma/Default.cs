using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using Dapper;

namespace Test_Ewma
{
    public class Default : Page
    {
        //{
        //    return this.Request.HttpMethod.Equals("POST");
        //}
        public string requestActionString = string.Empty;
        public string RequestActionString
        {
            get
            {
                return this.requestActionString;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Request.HttpMethod.Equals("GET"))
            {
                string str = this.Request["action"];
                if (str == "query")
                {
                    this.Search();
                }
            }
            else
            {
                string requestActionString = this.RequestActionString;
                if (!(requestActionString == "getcard"))
                {
                    if (requestActionString == "updateCard")
                    {
                        Response.Write("1");
                        return;
                    }
                }
                else
                {
                    Response.Write("0");
                    return;
                }
            }
            


        }

        public static void Delete()
        {

            using (IDbConnection conn = new SqlConnection(DbDapper.ConnectionString))
            {
                conn.Open();
                IDbTransaction transaction = conn.BeginTransaction();
                try
                {
                    string query = "select * from JK_Customer where CustomerID=3326730";
                    //string query2 = "DELETE FROM BookReview WHERE BookId = @BookId";
                    //conn.Execute(query2, new { BookId = 2 }, transaction, null, null);
                    conn.Execute(query, new { id = 2 }, transaction, null, null);
                    //提交事务
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    //出现异常，事务Rollback
                    transaction.Rollback();
                    throw new Exception(ex.Message);
                }

            }
        }
        public class Phone
        {
            public string phone { get; set; }
        }
        public void Query()
        {
            using (IDbConnection conn = new SqlConnection(DbDapper.ConnectionString))
            {
                conn.Open();
                string query = "select Phone from JK_Customer where CustomerID=3326730";
                var data = conn.Query<Phone>(query).SingleOrDefault();

                this.Response.Write((object)data.phone);
                this.Response.End();

            }
        }

        public void Search()
        {
            var id = 3326730;
            string query = "select Phone from JK_Customer where CustomerID=@id";
            var data = DBHelper<Phone>.QueryFirst(query, new { id=id});

            this.Response.Write((object)data.phone);
            this.Response.End();
        }
    }
}