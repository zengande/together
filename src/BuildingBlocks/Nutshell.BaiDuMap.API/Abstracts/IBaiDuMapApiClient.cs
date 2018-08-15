using Nutshell.BaiDuMap.API.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Nutshell.BaiDuMap.API.Abstracts
{
    public interface IBaiDuMapApiClient
    {
        /// <summary>
        /// 行政区划区域检索
        /// </summary>
        /// <param name="query">检索关键字</param>
        /// <param name="region">检索行政区划区域</param>
        /// <returns></returns>
        Task<BaiduMapApiResult<PlaceSearchResultItem>> PlaceSearch(string query, string region = "中国");
        /// <summary>
        /// 地点输入提示
        /// </summary>
        /// <param name="query"></param>
        /// <param name="region"></param>
        /// <returns></returns>
        Task<BaiduMapApiResult<PlaceSuggestionResultItem>> PlaceSuggestion(string query, string region = "中国");
    }
}
