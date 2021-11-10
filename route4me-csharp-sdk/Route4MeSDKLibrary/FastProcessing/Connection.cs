using Quobject.EngineIoClientDotNet.Modules;
using Quobject.SocketIoClientDotNet.Client;

namespace Route4MeSDK.FastProcessing
{
    public class Connection
    {
        public static readonly int TIMEOUT = 300000;

        static Connection()
        {
            LogManager.SetupLogManager();
        }

        protected IO.Options CreateOptions()
        {
            var _ = LogManager.GetLogger(Global.CallerName());
            var config = ConfigBase.Load();
            var options = new IO.Options
            {
                Hostname = config.Server.Hostname,
                ForceNew = true,
                Secure = true,
                Reconnection = true
            };

            options.Port = options.Secure ? 443 : 8080;

            return options;
        }

        protected string CreateUri()
        {
            var options = CreateOptions();
            var uri = ConnectionConstants.url ??
                      string.Format("{0}://{1}:{2}/{3}/", options.Secure ? "https" : "http", options.Hostname,
                          options.Port, ConnectionConstants.ROUTE);
            return uri;
        }

        protected IO.Options CreateOptionsSecure()
        {
            var log = LogManager.GetLogger(Global.CallerName());

            var config = ConfigBase.Load();
            var options = new IO.Options
            {
                Port = config.Server.SslPort,
                Hostname = config.Server.Hostname,
                Secure = true,
                IgnoreServerCertificateValidation = true
            };

            log.Info("Please add to your hosts file: 127.0.0.1 " + options.Hostname);

            return options;
        }
    }

    public class ConfigBase
    {
        public string Version { get; set; }
        public ConfigServer Server { get; set; }

        public static ConfigBase Load()
        {
            var result = new ConfigBase
            {
                Server = new ConfigServer()
            };
            result.Server.Hostname = ConnectionConstants.HOSTNAME;
            result.Server.Port = ConnectionConstants.PORT;
            result.Server.SslPort = ConnectionConstants.SSL_PORT;

            return result;
        }
    }

    public class ConfigServer
    {
        public string Hostname { get; set; }
        public int Port { get; set; }
        public int SslPort { get; set; }
    }
}