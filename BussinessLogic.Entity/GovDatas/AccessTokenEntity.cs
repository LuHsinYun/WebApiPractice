using System;
using Newtonsoft.Json;

namespace BussinessLogic.Entity.GovDatas
{
    public class AccessTokenEntity
    {
        /// <summary>
        /// 序號
        /// </summary>
        [JsonProperty(PropertyName = "sno")]
        public string Sno { get; set; }

        /// <summary>
        /// 名稱
        /// </summary>
        [JsonProperty(PropertyName = "sna")]
        public string Sna { get; set; }

        /// <summary>
        /// 數量
        /// </summary>
        [JsonProperty(PropertyName = "total")]
        public int Total { get; set; }

        /// <summary>
        /// 可租借數量
        /// </summary>
        [JsonProperty(PropertyName = "available_rent_bikes")]
        public int AvailableRentBikes { get; set; }

        /// <summary>
        /// 行政區
        /// </summary>
        [JsonProperty(PropertyName = "sarea")]
        public string Area { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty(PropertyName = "mday")]
        public DateTime Mday { get; set; }

        /// <summary>
        /// 緯度
        /// </summary>
        [JsonProperty(PropertyName = "latitude")]
        public double Latitude { get; set; }

        /// <summary>
        /// 經度
        /// </summary>
        [JsonProperty(PropertyName = "longitude")]
        public double Longitude { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        [JsonProperty(PropertyName = "ar")]
        public string Address { get; set; }

        /// <summary>
        /// 行政區(英文)
        /// </summary>
        [JsonProperty(PropertyName = "sareaen")]
        public string EnglishArea { get; set; }

        /// <summary>
        /// 名稱(英文)
        /// </summary>
        [JsonProperty(PropertyName = "snaen")]
        public string EnglishName { get; set; }

        /// <summary>
        /// 地址(英文)
        /// </summary>
        [JsonProperty(PropertyName = "aren")]
        public string EnglishAddress { get; set; }

        /// <summary>
        /// 可還車數量
        /// </summary>
        [JsonProperty(PropertyName = "available_return_bikes")]
        public int AvailableReturnBikes { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty(PropertyName = "act")]
        public string Act { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty(PropertyName = "srcUpdateTime")]
        public DateTime SrcUpdateTime { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty(PropertyName = "updateTime")]
        public DateTime UpdateTime { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty(PropertyName = "infoTime")]
        public DateTime InfoTime { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty(PropertyName = "infoDate")]
        public DateTime InfoDate { get; set; }
    }
}