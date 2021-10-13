using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Swagger_CJL
{

    public static class SwaggerMildd
    {
        public static void UseSwaggerMildd(this IApplicationBuilder app)
        {
            if (app == null) throw new ArgumentNullException(nameof(app));
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                //根据版本名称倒序 遍历展示
                string ApiName = "CJL";
                string version = "v1";
                c.SwaggerEndpoint($"/swagger/{version}/swagger.json", $"{ApiName} {version}");
                c.SwaggerEndpoint($"https://petstore.swagger.io/v2/swagger.json", $"{ApiName} pet");
                // 路径配置，设置为空，表示直接在根域名（localhost:8001）访问该文件,注意localhost:8001/swagger是访问不到的，去launchSettings.json把launchUrl去掉，如果你想换一个路径，直接写名字即可，比如直接写c.RoutePrefix = "doc";
                c.RoutePrefix = "";
            });
        }
    }
}
