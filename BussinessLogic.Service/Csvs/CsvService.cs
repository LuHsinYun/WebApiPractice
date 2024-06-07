using System.IO;
using System.Linq;
using BussinessLogic.Entity.ApiResponses;
using BussinessLogic.Entity.Members;
using BussinessLogic.Utilities.Csv;

namespace BussinessLogic.Service.Csvs
{
    /// <summary>
    /// Csv Service
    /// </summary>
    public class CsvService : ICsvService
    {
        /// <summary>
        /// Csv Utility
        /// </summary>
        private CsvUtility _csvUtility;

        public CsvService()
        {
            this._csvUtility = new CsvUtility();
        }

        /// <summary>
        /// 根據會員ID取得會員資料
        /// </summary>
        /// <param name="id">會員ID</param>
        /// <returns>會員資料</returns>
        public ApiResponseBaseEntity<MemberDataEntity> GetMemberById(int id)
        {
            var filePath = "C:\\Users\\Megan Lu\\Downloads";
            var fileName = "TestMemberFile.csv";

            var fullFilePath = Path.Combine(filePath, fileName);

            var dataList = this._csvUtility.ReadAllRecords<MemberDataEntity>(fullFilePath);

            var data = dataList.Where(x => x.MemberId == id).FirstOrDefault();

            if (data != null)
            {
                return new ApiResponseBaseEntity<MemberDataEntity>()
                {
                    Status = ApiStatusEnum.Success,
                    Data = data,
                    ErrorMessage = string.Empty
                };
            }

            return new ApiResponseBaseEntity<MemberDataEntity>()
            {
                Status = ApiStatusEnum.Failure,
                Data = null,
                ErrorMessage = "查無會員資料"
            }; ;
        }
    }
}