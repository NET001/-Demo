using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Http;
using Newtonsoft.Json;

namespace SelfHost_CJL.Controllers
{
    public class HomeController : ApiController
    {
        [HttpGet]
        [Route("Get1")]
        public string Get1()
        {
            return "get1";
        }

        [HttpGet]
        [Route("Get2")]
        public string Get2(string s1, string s2)
        {
            return $"get2{s1},{s2}";
        }
        [HttpPost]
        [Route("Post1")]
        public string Post1(string s1, string s2)
        {
            return $"Post1{s1},{s2}";
        }
        [HttpPost]
        [Route("Post2")]
        [Route("Post3")]
        public string Post2(object data)
        {
            return $"Post2{JsonConvert.SerializeObject(data)}";
        }
    }
}
