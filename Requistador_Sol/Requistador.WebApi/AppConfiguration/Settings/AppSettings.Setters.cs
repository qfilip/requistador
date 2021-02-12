namespace Requistador.WebApi.AppConfiguration.Settings
{
    public partial class AppSettings
    {
        public static void SetRequestProcessingInterval(int minutes)
        {
            Instance._parameters.RequestProcessingInterval = minutes;
        }
    }
}
