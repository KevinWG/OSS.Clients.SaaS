using OSS.Common;

namespace OSS.Clients.SMS.HW
{
 
    public class HwSecret : AccessSecret
    {
        /// <summary>
        ///  接口请求地址
        /// </summary>
        public string api_url { get; set; } = string.Empty;
    }

}
