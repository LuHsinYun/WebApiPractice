using System.Collections.Generic;

namespace BussinessLogic.Entity.Utilities
{
    public class CityEntity
    {
        /// <summary>
        /// 城市
        /// </summary>
        public string CityName { get; set; }

        /// <summary>
        /// 區域清單
        /// </summary>
        public List<DistrictEntity> DistrictList { get; set; }
    }
}