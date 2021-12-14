#region Copyright (C) 2017 Kevin (OSS开源实验室) 公众号：osscore

/***************************************************************************
*　　	文件功能描述：OSSCore插件 —— 阿里云 短信请求实体
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*    	创建日期：2017-10-22
*       
*****************************************************************************/

#endregion

using System.Collections.Generic;
using OSS.Common.Resp;

namespace OSS.Clients.SMS.Ali.Reqs
{
    public class SendAliSmsReq
    {
        /// <summary>
        ///  模板编号
        /// </summary>
        public string template_code { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public IList<string> PhoneNums { get; set; }

        /// <summary>
        ///  签名
        /// </summary>
        public string sign_name { get; set; }

        /// <summary>
        ///  内容数据
        /// </summary>
        public Dictionary<string, string> body_paras { get; set; }

    }

    public class SendAliSmsResp : Resp
    {
        /// <summary>
        ///  状态码-返回OK代表请求成功,其他错误码详见错误码列表
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        ///	状态码的描述
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        ///  发送回执ID,可根据该ID查询具体的发送状态
        /// </summary>
        public string BizId { get; set; }

        /// <summary>
        ///  请求ID
        /// </summary>
        public string RequestId { get; set; }
    }
}
