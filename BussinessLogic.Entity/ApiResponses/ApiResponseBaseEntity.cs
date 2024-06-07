namespace BussinessLogic.Entity.ApiResponses
{
    public class ApiResponseBaseEntity<T>
    {
        /// <summary>
        /// 狀態
        /// </summary>
        public ApiStatusEnum Status { get; set; }

        /// <summary>
        /// 錯誤訊息
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// 資料
        /// </summary>
        public T Data { get; set; }
    }
}