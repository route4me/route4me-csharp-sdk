using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route4MeSDK.FastProcessing
{
    public static class ConnectionConstants
    {
        //public static string url="http://localhost:8080";
        public static string url = "https://validator.route4me.com:443/";
        public static int PORT = 80;
        //public static int PORT = 8080;
        public static string HOSTNAME = "validator.route4me.com";
        //public static string HOSTNAME = "localhost";
        //public static string QUERY = "transport=polling";
        public static string ROUTE = "socket.io";
        public static int SSL_PORT = 443;
        public static readonly int TIMEOUT = 300000;
    }
}
