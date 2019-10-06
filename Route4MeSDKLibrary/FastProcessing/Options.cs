using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quobject.SocketIoClientDotNet.EngineIoClientDotNet.Client;
using Quobject.SocketIoClientDotNet.Client;
using System.Collections.Immutable;

namespace Route4MeSDK.FastProcessing
{
    public class Options : Transport.Options
    {
        public bool ForceNew = true;
        public bool Multiplex = true;

        public string Host;
        public string QueryString;
        public bool RememberUpgrade;
        public ImmutableList<string> Transports;
        public bool Upgrade;

        public bool Reconnection = true;
        public int ReconnectionAttempts;
        public long ReconnectionDelay;
        public long ReconnectionDelayMax;
        public long Timeout = -1;
        public bool AutoConnect = true;

        public Options() { }

        /*
        public static Options FromURI(Uri uri, Options opts)
        {

        }
        */
    }
}
