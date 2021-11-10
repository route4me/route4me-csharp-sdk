using System.Collections.Immutable;
using Quobject.EngineIoClientDotNet.Client;

namespace Route4MeSDK.FastProcessing
{
    public class Options : Transport.Options
    {
        public bool AutoConnect = true;
        public bool ForceNew = true;

        public string Host;
        public bool Multiplex = true;
        public string QueryString;

        public bool Reconnection = true;
        public int ReconnectionAttempts;
        public long ReconnectionDelay;
        public long ReconnectionDelayMax;
        public bool RememberUpgrade;
        public long Timeout = -1;
        public ImmutableList<string> Transports;
        public bool Upgrade;
    }
}