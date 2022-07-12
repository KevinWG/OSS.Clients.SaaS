
#region Copyright (C) 2017 Kevin (OSS开源实验室) 公众号：osscore

/***************************************************************************
*　　	文件功能描述：OSSCore插件 —— 阿里云 短信实现
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*    	创建日期：2017-10-22
*       
*****************************************************************************/

#endregion


using Newtonsoft.Json;
using OSS.Common;
using OSS.Common.Encrypt;
using OSS.Common.Extension;
using OSS.Tools.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using OSS.Common.Extension;

namespace OSS.Clients.SMS.Ali
{

    /// <summary>
    ///  阿里云的短信实现
    /// </summary>
    public class AliSmsClient//:BaseMetaImpl<AliSmsConfig>
    {
        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="sendReq"></param>
        /// <returns></returns>
        public async Task<AliSendSmsResp> SendAsync(AliSendSmsReq sendReq)
        {
            var appConfig = await AliSmsHelper.GetSecret();
            var dirs = new SortedDictionary<string, string>(StringComparer.Ordinal)
            {
                {"Action", "SendSms"},
                {"Version", appConfig.version},
                {"RegionId", appConfig.region_id},
                {"PhoneNumbers", string.Join(",", sendReq.phone_nums)},
                {"SignName", sendReq.sign_name},
                {"TemplateCode", sendReq.template_code}
            };

            if (sendReq.body_paras != null && sendReq.body_paras.Count > 0)
            {
                var temparas = JsonConvert.SerializeObject(sendReq.body_paras);
                dirs.Add("TemplateParam", temparas);
            }

            FillApiPara(appConfig, dirs);

            var req = new OssHttpRequest
            {
                address_url = string.Concat("http://dysmsapi.aliyuncs.com?", GeneratePostData(appConfig, dirs)),
                http_method = HttpMethod.Get
            };

            var content = await req.SendAsync().ReadContentAsStringAsync();
            return JsonConvert.DeserializeObject<AliSendSmsResp>(content);
        }

   

        #region 辅助方法

        private static string GeneratePostData(IAccessSecret config, SortedDictionary<string, string> paras)
        {
            var content = string.Join("&",
                paras.Select(k =>
                    string.Concat(SpecicalUrlEncode(k.Key), "=", SpecicalUrlEncode(k.Value))));

            var preEncryStr = string.Concat("GET&", SpecicalUrlEncode("/"), "&", SpecicalUrlEncode(content));
            var sign = HMACSHA.EncryptBase64(preEncryStr, string.Concat(config.access_secret, "&"));

            return string.Concat("Signature=", SpecicalUrlEncode(sign), "&", content);
        }

        private static void FillApiPara(IAccessKey config, IDictionary<string, string> sortDic)
        {
            var dateTime = DateTime.Now.ToUniversalTime()
                .ToString("yyyy-MM-ddTHH:mm:ssZ", CultureInfo.CreateSpecificCulture("en-US"));

            sortDic.Add("AccessKeyId", config.access_key);
            sortDic.Add("Timestamp", dateTime);
            sortDic.Add("Format", "JSON");
            sortDic.Add("SignatureMethod", "HMAC-SHA1");

            sortDic.Add("SignatureVersion", "1.0");
            sortDic.Add("SignatureNonce", Guid.NewGuid().ToString());
        }

        private static string SpecicalUrlEncode(string data)
        {
            return data.SafeEscapeUriDataString().Replace("+", "%20").Replace("*", "%2A").Replace("%7E", "~");
        }

        #endregion
    }
    
}
