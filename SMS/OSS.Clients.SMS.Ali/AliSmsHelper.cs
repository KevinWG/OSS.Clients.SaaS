using System.Data;

namespace OSS.Clients.SMS.Ali
{
    /// <summary>
    ///  阿里云短信接口相关配置
    /// </summary>
    public  static class AliSmsHelper
    {
        private static IAliSmsSecretProvider? _secretProvider;

        /// <summary>
        ///  配置  阿里云短信访问秘钥提供者
        /// </summary>
        /// <param name="secretProvider"></param>
        public static void SetSmsSecretProvider(IAliSmsSecretProvider secretProvider)
        {
            _secretProvider = secretProvider;
        }

        /// <summary>
        ///  默认短信密钥配置
        /// </summary>
        public static AliSmsSecret? default_secret { get; set; }


        internal static async Task<AliSmsSecret> GetSecret()
        {
            AliSmsSecret? secret =null ;

            if (_secretProvider !=null)
            {
                secret = await _secretProvider.Get();
            }

            secret ??= default_secret;

            if (secret == null)
            {
                throw new NoNullAllowedException("未能获取有效的阿里云短信访问秘钥信息");
            }
            return secret;
        }


    }
}