using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationTest.Controllers
{
    public class HomeController : ControllerBase
    {
        [HttpGet]
        [Route("/index")]
        public string index()
        {
            return "Hello Worde";
        }
        //查询水表
        [HttpPost]
        [Route("/gecp/water_meter/queryList")]
        public string findlist()
        {
            string result = "{\"status\":\"success\",\"msg\":\"成功\",\"content\":[{\"waterCode\":1,\"waterName\":\"智能水表1号\",\"flow\":12.34,\"currentTime\":1638852100000,\"totalValue\":1234.34,\"area\":\"室外环境及设备\"},{\"waterCode\":2,\"waterName\":\"智能水表2号\",\"flow\":12.34,\"currentTime\":1638852100000,\"totalValue\":1234.34,\"area\":\"室外环境及设备\"},{\"waterCode\":3,\"waterName\":\"流量计1号\",\"flow\":12.34,\"currentTime\":1638852100000,\"totalValue\":1234.34,\"area\":\"室外环境及设备\"}]}";
            return result;
        }
        //查询电表
        [HttpPost]
        [Route("/gecp/electric/queryList")]
        public string findEleDeviceinfoList()
        {
            string result = "{\"status\":\"success\",\"msg\":\"成功\",\"content\":[{\"electricCode\":1,\"electricName\":\"1# 七彩廊道 - 473 \",\"quantity\":12.34,\"area\":\"1号站\",\"type\":48},{\"electricCode\":2,\"electricName\":\"1# 解挂锁站电控箱JGS3 - 459 \",\"quantity\":12.34,\"area\":\"2号站\",\"type\":49},{\"electricCode\":3,\"electricName\":\"1# 七彩廊道 - 473 \",\"online\":1,\"quantity\":12.34,\"area\":\"3号站\",\"type\":50}]}";
            return result;
        }
    }
}
