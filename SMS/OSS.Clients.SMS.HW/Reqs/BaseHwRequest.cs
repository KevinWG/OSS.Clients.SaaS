using System;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using OSS.Tools.Http;

namespace OSS.Clients.SMS.HW
{
    public class BaseHwRequest : OssHttpRequest
    {
        internal string ApiPath { get; }

        public BaseHwRequest(string apiPath)
        {
            ApiPath = apiPath;
        }

        /// <summary>
        ///  华为云应用配置信息
        /// </summary>
        public HwSecret access_secret { get; set; }

        protected override void OnSending(HttpRequestMessage r)
        {
            r.Headers.Add("Authorization", "WSSE realm=\"SDP\",profile=\"UsernameToken\",type=\"Appkey\"");

            r.Content?.Headers.Add("X-WSSE", BuildWSSEHeader(access_secret.access_key, access_secret.access_secret));
            base.OnSending(r);
        }

        private static string BuildWSSEHeader(string appKey, string appSecret)
        {
            var now   = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ssZ"); //Created
            var nonce = Guid.NewGuid().ToString().Replace("-", "");    //Nonce

            var    material = Encoding.UTF8.GetBytes(nonce + now + appSecret);
            byte[] hashed;

            using (var sha256 = SHA256.Create())
            {
                hashed = sha256.ComputeHash(material);
            }

            var hexDigest = BitConverter.ToString(hashed).Replace("-", "");
            var base64    = Convert.ToBase64String(Encoding.UTF8.GetBytes(hexDigest));

            return
                $"UsernameToken Username=\"{appKey}\",PasswordDigest=\"{base64}\",Nonce=\"{nonce}\",Created=\"{now}\"";
        }
    }
}