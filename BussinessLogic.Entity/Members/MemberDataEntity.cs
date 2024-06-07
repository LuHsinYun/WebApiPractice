using System;

namespace BussinessLogic.Entity.Members
{
    public class MemberDataEntity
    {
        /// <summary>
        /// 會員Id
        /// </summary>
        public int MemberId { get; set; }

        /// <summary>
        /// 會員編號
        /// </summary>
        public string MemberCode { get; set; }

        /// <summary>
        /// 手機號碼
        /// </summary>
        public string Cellphone { get; set; }

        /// <summary>
        /// 會員姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        public DateTime Birthday { get; set; }

        /// <summary>
        /// 性別
        /// </summary>
        public GenderEnum Gender { get; set; }

        /// <summary>
        /// 地址(縣市)
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// 地址(鄉鎮市區)
        /// </summary>
        public string District { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 完整地址
        /// </summary>
        public string FullAddress
        {
            get
            {
                var actualAddress = Address.Replace(City, string.Empty).Replace(District, string.Empty);
                return $"{City}{District}{actualAddress}";
            }
        }
    }
}