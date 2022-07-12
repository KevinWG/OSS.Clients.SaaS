

#region Copyright (C) 2017 Kevin (OSS开源实验室) 公众号：osscore

/***************************************************************************
*　　	文件功能描述：OSSCore插件 —— 阿里云密钥配置
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*    	创建日期：2017-10-22
*       
*****************************************************************************/

#endregion

using OSS.Common;

namespace OSS.Clients.SMS.Ali
{
    /// <summary>
    ///  阿里云短信秘钥配置信息
    /// </summary>
    public class AliSmsSecret : AccessSecret
    {
        /// <summary>
        ///  版本号
        /// </summary>
        public string version { get; set; }

        /// <summary>
        ///  地区id
        /// </summary>
        public string region_id { get; set; }
    }
}
