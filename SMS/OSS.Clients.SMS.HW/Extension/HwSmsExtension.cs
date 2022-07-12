using System.Data;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OSS.Tools.Http;

namespace OSS.Clients.SMS.HW
{
    public static class HwSmsExtension
    {
        public static async Task<HwSendResp> SendAsync(this BaseHwRequest smsReq)
        {
            await FormatSecret(smsReq);

            smsReq.address_url = string.Concat(smsReq.access_secret.api_url, smsReq.ApiPath);
            var resStr = await ((OssHttpRequest)smsReq).SendAsync().ReadContentAsStringAsync();

            return JsonConvert.DeserializeObject<HwSendResp>(resStr);
        }


        private static async Task FormatSecret(BaseHwRequest smsReq)
        {
            if (smsReq.access_secret!=null)
            {
                return;
            }

            var secret = HwSmsHelper.SecretProvider != null
                ? await HwSmsHelper.SecretProvider.Get()
                : HwSmsHelper.default_secret;

            if (secret ==null)
            {
                throw new NoNullAllowedException("未能找到华为云短信访问配置信息");
            }
            smsReq.access_secret = secret;
        }

    }
}