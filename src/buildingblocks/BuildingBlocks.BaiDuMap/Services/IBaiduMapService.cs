using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Together.Location.Application.BaiduMap;

namespace Together.BuildingBlogs.BaiDuMap.Services
{
    public interface IBaiduMapService
    {
        Task<ReverseGeoCodingResult> ReverseGeoCodingAsync(string location);
    }
}
