namespace OSS.Clients.SMS.HW
{
    /// <summary>
    ///  华为短信辅助类
    /// </summary>
    public static class HwSmsHelper
    {
        internal static HwSecretProvider? SecretProvider { get; private set; }

        /// <summary>
        ///  设置秘钥提供者
        /// </summary>
        /// <param name="secretProvider"></param>
        public static void SetSecretProvider(HwSecretProvider secretProvider)
        {
            SecretProvider = secretProvider;
        }

        /// <summary>
        ///  全局默认秘钥
        /// </summary>
        public static HwSecret? default_secret { get; set; }
    }
}