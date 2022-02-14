using SouthCore.Default;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.Text;
using SouthCore.Default.Http;
using GWSouthCoreDemo.STD.Model;

namespace GWSouthCoreDemo.STD.Services
{
    //Http请求
    public class HttpStartup
    {
        public void ConfigureServices(IDefaultAppEquipBuilder builder)
        {
            //复用一个HttpClient对象
            HttpClient httpClient = new HttpClient();
            builder.SetHttps(https => https
                .SetHttp(GlobalModel.EquipGroupNames[0], httpClient, (HttpClient httpClient, object[] parms) =>
                {
                    object[] result = new object[] { null };
                    result[0] = httpClient.PostAsync("http://localhost:5000/gecp/electric/queryList", new StringContent("")).Result.Content.ReadAsStringAsync().Result;
                    return result;
                })
                .SetHttp(GlobalModel.EquipGroupNames[1], httpClient, (HttpClient httpClient, object[] parms) =>
                {
                    object[] result = new object[] { null };
                    result[0] = httpClient.PostAsync("http://localhost:5000/gecp/electric/queryList", new StringContent("")).Result.Content.ReadAsStringAsync().Result;
                    return result;
                })
            );
        }
    }
}
