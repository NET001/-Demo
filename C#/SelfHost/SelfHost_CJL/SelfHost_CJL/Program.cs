using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.SelfHost;

namespace SelfHost_CJL
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Execute();
            Console.ReadLine();
        }
        static void Execute()
        {
            string baseAddress = $"http://localhost:{20001}/";
            var config = new HttpSelfHostConfiguration(baseAddress);

            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{action}");

            config.MaxBufferSize = 2147483647;  //int maxvalue
            config.MaxReceivedMessageSize = 2147483647;    //int maxvalue
            config.ReceiveTimeout = TimeSpan.FromSeconds(100);
            var server = new HttpSelfHostServer(config);
            server.OpenAsync();
        }
    }
}
