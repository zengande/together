namespace WebMVC.Infrastructure.Dtos
{
    public class ActivityAddressDto
    {
        public string Province { get; set; }
        public string City { get; set; }
        public string Detail { get; set; }
        /// <summary>
        /// 经度
        /// </summary>
        public double Longitude { get; set; }
        /// <summary>
        /// 纬度
        /// </summary>
        public double Latitude { get; set; }
    }
}
