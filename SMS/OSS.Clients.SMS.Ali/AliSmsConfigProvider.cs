#region Copyright (C) 2017 Kevin (OSS开源实验室) 公众号：osscore

/***************************************************************************
*　　	文件功能描述：OSSCore插件 —— 阿里云 短信api配置管理
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*    	创建日期：2020-08-14
*       
*****************************************************************************/

#endregion
using OSS.Clients.SMS.Ali.Reqs;

namespace OSS.Clients.SMS.Ali
{
    /// <summary>
    ///  阿里云短信接口相关配置
    /// </summary>
    public  static class AliSmsConfigProvider
    {
        /// <summary>
        ///  默认短信密钥配置
        /// </summary>
        public static AliSmsConfig DefaultConfig { get; set; }
    }
}
