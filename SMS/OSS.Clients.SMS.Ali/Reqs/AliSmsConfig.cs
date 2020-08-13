

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

using OSS.Common.BasicMos;

namespace OSS.Clients.SMS.Ali.Reqs
{
    public class AliSmsConfig:AppConfig
    {

        public string Version { get; set; }

        public string RegionId { get; set; }
    }
}
