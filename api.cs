using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Arms.FT.WebUI.Controllers.APIS
{
    //URL/DbConnection/DBConnect
    [Authorize]
    [RoutePrefix("DbConnection")]
    public class DbConnectivityController : ApiController
    {
        [HttpGet]
        [Route("DBConnect")]
        public object GetData([FromUri]string fname, string city) //[FromUri] is optional
        {
            using (DataSet ds = ArmsServiceClientInvoke.DisplayRecord(ConnectionType.Default, SP.UspFDDTAPDEVBIPIN, 0, 0, GlobalMethods.GenerateXML(new string[] { "FirstName", fname, "City", city })))
            {
                //Method1
                var JSONString = JsonConvert.SerializeObject(ds.Tables[0]);
                return Request.CreateResponse(HttpStatusCode.OK, JToken.Parse(JSONString));

                //// Method2 (you can use both return)
                //List<PersonModel> myData = ds.Tables[0].ToList<PersonModel>();
                //return myData;
                ////return Request.CreateResponse(HttpStatusCode.OK, myData);
            }
        }
    }

}
