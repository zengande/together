using System;
using System.Collections.Generic;
using System.Text;

namespace Nutshell.BaiDuMap.API.Models
{
    public class BaiduMapApiResult<T>
    {
        /// <summary>
        /// 
        /// </summary>
        public int status { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string message { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<T> result { get; set; }
    }
}
