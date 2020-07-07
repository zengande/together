using System;
using System.Collections.Generic;
using System.Text;

namespace Together.Location.Application.BaiduMap
{
    public struct Location
    {
        /// <summary>
        /// 
        /// </summary>
        public double lng { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double lat { get; set; }
    }

    public class AddressComponent
    {
        /// <summary>
        /// 中国
        /// </summary>
        public string country { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int country_code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string country_code_iso { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string country_code_iso2 { get; set; }
        /// <summary>
        /// 上海市
        /// </summary>
        public string province { get; set; }
        /// <summary>
        /// 上海市
        /// </summary>
        public string city { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int city_level { get; set; }
        /// <summary>
        /// 黄浦区
        /// </summary>
        public string district { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string town { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string town_code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string adcode { get; set; }
        /// <summary>
        /// 中山南路
        /// </summary>
        public string street { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string street_number { get; set; }
        /// <summary>
        /// 东北
        /// </summary>
        public string direction { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string distance { get; set; }
    }

    public class Result
    {
        /// <summary>
        /// 
        /// </summary>
        public Location location { get; set; }
        /// <summary>
        /// 上海市黄浦区中山南路187
        /// </summary>
        public string formatted_address { get; set; }
        /// <summary>
        /// 外滩,陆家嘴,董家渡
        /// </summary>
        public string business { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public AddressComponent addressComponent { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> pois { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> roads { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> poiRegions { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string sematic_description { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int cityCode { get; set; }
    }

    public class ReverseGeoCodingResult
    {
        /// <summary>
        /// 
        /// </summary>
        public int status { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Result result { get; set; }
    }
}
