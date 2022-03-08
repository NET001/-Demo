using Microsoft.AspNetCore.Mvc;

namespace IoTCenterWebApi.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public string Index()
        {
            return "这是测试";
        }
    }
}
