using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Controllers;
using Magicodes.Admin.Web.Host.Startup;
using Microsoft.AspNetCore.Mvc;

namespace Magicodes.Admin.Web.Host.Controllers
{
    public class ConfigurationController : AbpController
    {
        private readonly UserConfigurationBuilder _abpUserConfigurationBuilder;

        public ConfigurationController(UserConfigurationBuilder abpUserConfigurationBuilder)
        {
            _abpUserConfigurationBuilder = abpUserConfigurationBuilder;
        }

        public async Task<JsonResult> GetAll()
        {
            var userConfig = await _abpUserConfigurationBuilder.GetAll();
            return Json(userConfig);
        }
    }
}
