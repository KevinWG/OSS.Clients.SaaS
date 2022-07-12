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


using System.Data;
using System.Threading.Tasks;
using OSS.Common;

namespace OSS.Clients.SMS.Ali
{
    /// <summary>
    ///  阿里云短信接口相关配置
    /// </summary>
    public  static class AliSmsHelper
    {


        private static IAliSmsSecretProvider _secretProvider;

        /// <summary>
        ///  配置  阿里云短信访问秘钥提供者
        /// </summary>
        /// <param name="secretProvider"></param>
        public static void SetSmsSecretProvider(IAliSmsSecretProvider secretProvider)
        {
            _secretProvider= secretProvider;
        }

        /// <summary>
        ///  默认短信密钥配置
        /// </summary>
        public static AliSmsSecret default_secret { get; set; }


        internal static async Task<AliSmsSecret> GetSecret()
        {
            AliSmsSecret secret =null ;
            if (_secretProvider !=null)
            {
                secret = await _secretProvider.Get();
            }

            if (secret == null)
            {
                secret = default_secret;
            }

            if (secret == null)
            {
                throw new NoNullAllowedException("未能获取有效的阿里云短信访问秘钥信息");
            }
            return secret;
        }


    }

    /// <summary>
    ///  阿里云短信访问秘钥提供者
    /// </summary>
    public interface IAliSmsSecretProvider : IAccessProvider<AliSmsSecret>
    {
    }
}
