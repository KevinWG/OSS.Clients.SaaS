namespace OSS.Clients.SMS.HW
{
    public static class HwSmsHelper
    {
        internal static HwSecretProvider SecretProvider { get; private set; }
        public static void SetSecretProvider(HwSecretProvider secretProvider)
        {
            SecretProvider = secretProvider;
        }

        public static HwSecret default_secret { get; set; }
    }
}