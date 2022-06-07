using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using System.Data.SqlClient;
using Arms.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace APIADONET.Controllers.API
{
    //URL/DbConnection/DBConnect
    //[Authorize]
    [RoutePrefix("DbConnection")]
    public class GetInfoController : ApiController
    {
        [HttpGet]
        [Route("DBConnect")]
        public object GetData([FromUri]string fname, string city)
        {
            try
            {
                DataSet ds = new DataSet();
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = "Server=172.16.11.26\\mssql2019; Database=FDDTAPDEV; User ID=arms; Password=roti1234";
                    SqlCommand sqlComm = new SqlCommand("FDDTAPDEVBIPIN", conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;

                    sqlComm.Parameters.AddWithValue("@ConditionalOperator", 0);
                    sqlComm.Parameters.AddWithValue("@RecordID", 0);
                    sqlComm.Parameters.AddWithValue("@XmlData", GlobalMethods.GenerateXML(new string[] { "FirstName", fname, "City", city }));

                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = sqlComm;
                    adapter.Fill(ds);

                    var JSONString = JsonConvert.SerializeObject(ds.Tables[0]);
                    return Request.CreateResponse(HttpStatusCode.OK, JToken.Parse(JSONString));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return new object();
            }
        }
    }
}
