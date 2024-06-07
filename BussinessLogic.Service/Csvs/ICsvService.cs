using BussinessLogic.Entity.ApiResponses;
using BussinessLogic.Entity.Members;

namespace BussinessLogic.Service.Csvs
{
    public interface ICsvService
    {
        /// <summary>
        /// 根據會員ID取得會員資料
        /// </summary>
        /// <param name="id">會員ID</param>
        /// <returns>會員資料</returns>
        ApiResponseBaseEntity<MemberDataEntity> GetMemberById(int id);
    }
}