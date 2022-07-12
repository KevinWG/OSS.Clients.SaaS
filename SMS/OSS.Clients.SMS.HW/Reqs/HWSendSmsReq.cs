using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using OSS.Common.Extension;
using OSS.Tools.Http;

namespace OSS.Clients.SMS.HW
{
    public class HWSendSmsReq : BaseHwRequest
    {
        public HWSendSmsReq():base("/sms/batchSendSms/v1")
        {
            http_method = HttpMethod.Post;
        }

        protected override void PrepareSend()
        {
            var paras = body_paras!=null? JsonConvert.SerializeObject(body_paras):string.Empty;

            this.AddFormPara("from", FormDataEscape(sender));
            this.AddFormPara("to", string.Join(",", phone_nums));
            this.AddFormPara("templateId", FormDataEscape(template_code));
            this.AddFormPara("templateParas", FormDataEscape(paras));
            this.AddFormPara("statusCallback", string.Empty);
            base.PrepareSend();
        }
        
        /// <summary>
        ///  模板编号
        /// </summary>
        public string template_code { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public IList<string> phone_nums { get; set; }


        /// <summary>
        ///  发送人或者发送通道号码等
        /// </summary>
        public string sender { get; set; } = string.Empty;


        /// <summary>
        ///  内容数据
        /// </summary>
        public IList<string> body_paras { get; set; }




        private static string FormDataEscape(string fromData)
        {
            return fromData.SafeEscapeUriDataString().Replace("%20", "+");
        }
    }


    public class HwSendResp
    {
        public string code { get; set; } = string.Empty;

        public string description { get; set; } = string.Empty;
    }

}