using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quobject.SocketIoClientDotNet.EngineIoClientDotNet.Modules;
//using IO = Quobject.EngineIoClientDotNet.Client.Transports;
using IO = Quobject.SocketIoClientDotNet.Client.IO;

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
            var log = LogManager.GetLogger(Global.CallerName());


            var config = ConfigBase.Load();
            var options = new IO.Options();

            options.Hostname = config.server.hostname;
            options.ForceNew = true;
            options.Secure = true;
            options.Port = options.Secure ? 443 : 8080;
            //log.Info("Please add to your hosts file: 127.0.0.1 " + options.Hostname);
            options.Reconnection = true;
            return options;
        }

        protected string CreateUri()
        {
            var options = CreateOptions();
            //var uri = string.Format("{0}://{1}:{2}/{3}/{4}", options.Secure ? "https" : "http", options.Hostname, options.Port, ConnectionConstants.ROUTE, ConnectionConstants.QUERY);
            var uri = ConnectionConstants.url != null ?
                ConnectionConstants.url :
                string.Format("{0}://{1}:{2}/{3}/", options.Secure ? "https" : "http", options.Hostname, options.Port, ConnectionConstants.ROUTE);
            return uri;
        }

        protected IO.Options CreateOptionsSecure()
        {
            var log = LogManager.GetLogger(Global.CallerName());

            var config = ConfigBase.Load();
            var options = new IO.Options();
            options.Port = config.server.ssl_port;
            options.Hostname = config.server.hostname;
            log.Info("Please add to your hosts file: 127.0.0.1 " + options.Hostname);
            options.Secure = true;
            options.IgnoreServerCertificateValidation = true;
            return options;
        }
    }

    public class ConfigBase
    {
        public string version { get; set; }
        public ConfigServer server { get; set; }

        public static ConfigBase Load()
        {
            var result = new ConfigBase()
            {
                server = new ConfigServer()
            };
            result.server.hostname = ConnectionConstants.HOSTNAME;
            result.server.port = ConnectionConstants.PORT;
            result.server.ssl_port = ConnectionConstants.SSL_PORT;

            return result;
        }
    }

    public class ConfigServer
    {
        public string hostname { get; set; }
        public int port { get; set; }
        public int ssl_port { get; set; }
    }
}
