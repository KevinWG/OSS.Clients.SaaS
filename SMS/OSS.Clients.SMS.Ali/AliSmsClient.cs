
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


using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OSS.Clients.SMS.Ali.Reqs;
using OSS.Common.BasicImpls;
using OSS.Common.Resp;
using OSS.Common.Encrypt;
using OSS.Tools.Http.Mos;
using OSS.Common.Extension;

namespace OSS.Clients.SMS.Ali
{

    /// <summary>
    ///  阿里云的短信实现
    /// </summary>
    public class AliSmsClient:BaseMetaImpl<AliSmsConfig>
    {
        /// <inheritdoc />
        public AliSmsClient()
        {
        }

        /// <inheritdoc />
        public AliSmsClient(IMetaProvider<AliSmsConfig> configProvider)
            : base(configProvider)
        {
        }

        public async Task<SendAliSmsResp> Send(SendAliSmsReq sendReq)
        {
            var appConfigRes =await GetMeta();
            if (!appConfigRes.IsSuccess())
                return new SendAliSmsResp().WithResp(appConfigRes);

            var appConfig = appConfigRes.data;
            var dirs = new SortedDictionary<string, string>(StringComparer.Ordinal)
            {
                {"Action", "SendSms"},
                {"Version", appConfig.Version},
                {"RegionId", appConfig.RegionId},
                {"PhoneNumbers", string.Join(",", sendReq.PhoneNums)},
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
                AddressUrl = string.Concat("http://dysmsapi.aliyuncs.com?", GeneratePostData(appConfig, dirs)),
                HttpMethod = HttpMethod.Get
            };

            using (var resp = await req.RestSend())
            {

                var content = await resp.Content.ReadAsStringAsync();
                 return JsonConvert.DeserializeObject<SendAliSmsResp>(content);

            }
        }

        /// <inheritdoc />
        protected override AliSmsConfig GetDefaultMeta()
        {
           return AliSmsConfigProvider.DefaultConfig;
        }

        #region 辅助方法

        private static string GeneratePostData(AliSmsConfig config, SortedDictionary<string, string> paras)
        {
            var content = string.Join("&",
                paras.Select(k =>
                    string.Concat(SpecicalUrlEncode(k.Key), "=", SpecicalUrlEncode(k.Value))));

            var preEncryStr = string.Concat("GET&", SpecicalUrlEncode("/"), "&", SpecicalUrlEncode(content));
            var sign = HMACSHA.EncryptBase64(preEncryStr, string.Concat(config.AppSecret, "&"));

            return string.Concat("Signature=", SpecicalUrlEncode(sign), "&", content);
        }

        private static void FillApiPara(AliSmsConfig config, IDictionary<string, string> sortDic)
        {
            var dateTime = DateTime.Now.ToUniversalTime()
                .ToString("yyyy-MM-ddTHH:mm:ssZ", CultureInfo.CreateSpecificCulture("en-US"));

            sortDic.Add("AccessKeyId", config.AppId);
            sortDic.Add("Timestamp", dateTime);
            sortDic.Add("Format", "JSON");
            sortDic.Add("SignatureMethod", "HMAC-SHA1");

            sortDic.Add("SignatureVersion", "1.0");
            sortDic.Add("SignatureNonce", Guid.NewGuid().ToString());
        }

        private static string SpecicalUrlEncode(string data)
        {
            return data.SafeEscapeUriString().Replace("+", "%20").Replace("*", "%2A").Replace("%7E", "~");
        }

        #endregion
    }
    
}
