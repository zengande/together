using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Together.BuildingBlogs.BaiDuMap.Services;

namespace Together.BuildingBlocks.BaiDuMap
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBaiDuMapServices(this IServiceCollection services)
        {
            services.AddHttpClient<IBaiduMapService, BaiduMapService>(http=> {
                http.BaseAddress = new Uri("https://api.map.baidu.com");
            });

            return services;
        }
    }
}
