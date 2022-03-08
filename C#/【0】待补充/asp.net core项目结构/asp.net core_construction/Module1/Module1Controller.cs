using Microsoft.AspNetCore.Mvc;

namespace Module1
{
    public class Module1Controller : Controller
    {
        [HttpGet]
        public string Index()
        {
            return "这是Module1Controller";
        }
    }
}

