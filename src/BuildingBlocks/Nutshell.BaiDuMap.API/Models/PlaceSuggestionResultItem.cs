using System;
using System.Collections.Generic;
using System.Text;

namespace Nutshell.BaiDuMap.API.Models
{
    public class PlaceSuggestionResultItem
    {
        /// <summary>
        /// 浙大科技园
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Location location { get; set; }
        /// <summary>
        /// 83路;102路;306路;346路;353路
        /// </summary>
        public string city { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string uid { get; set; }

        public string province { get; set; }
        public string business { get; set; }
        public string district { get; set; }
        public string cityid { get; set; }
    }
}
