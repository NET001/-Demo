﻿using AspNetCoreRateLimit;
using Blog.Core_CJL.Common;
using Blog.Core_CJL.Common.Helper;
using log4net;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core_CJL.Extensions.Middlewares
{
    public static class IpLimitMildd
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(IpLimitMildd));
        public static void UseIpLimitMildd(this IApplicationBuilder app)
        {
            if (app == null) throw new ArgumentNullException(nameof(app));
            try
            {
                if (Appsettings.app("Middleware", "IpRateLimit", "Enabled").ObjToBool())
                {
                    app.UseIpRateLimiting();
                }
            }
            catch (Exception e)
            {
                log.Error($"Error occured limiting ip rate.\n{e.Message}");
                throw;
            }
        }
    }
}
