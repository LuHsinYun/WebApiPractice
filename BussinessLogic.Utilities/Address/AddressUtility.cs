using System.Collections.Generic;
using System.IO;
using BussinessLogic.Entity.Utilities;
using Newtonsoft.Json;
using System.Linq;

namespace BussinessLogic.Utilities.Address
{
    /// <summary>
    /// 地址套件
    /// </summary>
    public class AddressUtility
    {
        /// <summary>
        /// 取得行政區對照表
        /// </summary>
        /// <returns>行政區對照表</returns>
        public List<CityEntity> GetCityEntities()
        {
            var path = $"{Directory.GetCurrentDirectory()}\\TaiwanAddressCityMapping.json";

            using (StreamReader sr = new StreamReader(path))
            {
                var areaContent = sr.ReadToEnd();
                var result = JsonConvert.DeserializeObject<List<CityEntity>>(areaContent);

                return result;
            }
        }

        /// <summary>
        /// 檢查縣市和鄉鎮市區是否正確
        /// </summary>
        /// <param name="city">縣市</param>
        /// <param name="district">鄉鎮市區</param>
        /// <returns></returns>
        public bool CheckDistrictInCity(string city, string district)
        {
            var isValid = false;

            var cityEntity = this.GetCityEntities();

            var mappingCityResult = cityEntity.Where(c => c.CityName == city).FirstOrDefault();
            if (mappingCityResult != null)
            {
                var mappingDistrictResult = mappingCityResult.DistrictList.Where(d => d.DistrictName == district).FirstOrDefault();
                if (mappingDistrictResult != null)
                {
                    isValid = true;
                }
            }

            return isValid;
        }
    }
}