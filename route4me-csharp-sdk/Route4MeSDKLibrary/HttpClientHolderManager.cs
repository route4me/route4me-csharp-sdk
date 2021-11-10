using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Route4MeSDKLibrary
{
    /// <summary>
    ///     Manages <see cref="System.Net.Http.HttpClient" /> in order to enable re-usage of
    ///     <see cref="System.Net.Http.HttpClient" />. Also support GC logic for those instances which are not in use for some
    ///     period of time.
    /// </summary>
    internal class HttpClientHolderManager
    {
        private static readonly Dictionary<string, HttpClientWrapper> HttpClientWrappers =
            new Dictionary<string, HttpClientWrapper>();

        private static readonly object SyncRoot = new object();

        static HttpClientHolderManager()
        {
            // ReSharper disable once ObjectCreationAsStatement
            new SequentialTimer(OnTimerCallback, TimeSpan.FromHours(2));
        }

        public static HttpClientHolder AcquireHttpClientHolder(string baseAddress)
        {
            lock (SyncRoot)
            {
                HttpClientWrapper wrapper;
                if (HttpClientWrappers.TryGetValue(baseAddress, out var prev))
                {
                    prev.RefCount++;
                    prev.LastAccess = DateTime.Now;
                    wrapper = prev;
                }
                else
                {
                    wrapper = new HttpClientWrapper(CreateHttpClient(baseAddress));
                    HttpClientWrappers.Add(baseAddress, wrapper);
                }

                return new HttpClientHolder(wrapper.HttpClient, baseAddress);
            }
        }

        public static void ReleaseHttpClientHolder(string baseAddress)
        {
            lock (SyncRoot)
            {
                if (HttpClientWrappers.TryGetValue(baseAddress, out var wrapper))
                {
                    if (wrapper.RefCount > 0) wrapper.RefCount--;
                    wrapper.LastAccess = DateTime.Now;
                }
            }
        }

        private static HttpClient CreateHttpClient(string baseAddress)
        {
            var result = new HttpClient {BaseAddress = new Uri(baseAddress), Timeout = TimeSpan.FromMinutes(30)};

            result.DefaultRequestHeaders.Accept.Clear();
            result.DefaultRequestHeaders.ConnectionClose = false;
            result.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            ServicePointManager.FindServicePoint(new Uri(baseAddress)).ConnectionLeaseTimeout = 60 * 1000;

            return result;
        }

        private static void OnTimerCallback()
        {
            lock (SyncRoot)
            {
                var now = DateTime.Now;
                var keysToRemove = new List<string>();
                foreach (var kvp in HttpClientWrappers)
                    if (kvp.Value.RefCount == 0 && now - kvp.Value.LastAccess > TimeSpan.FromHours(1))
                        keysToRemove.Add(kvp.Key);

                foreach (var key in keysToRemove)
                {
                    if (HttpClientWrappers.TryGetValue(key, out var removed))
                    {
                        HttpClientWrappers.Remove(key);
                        removed.HttpClient.Dispose();
                    }
                }
            }
        }

        private class HttpClientWrapper
        {
            public HttpClientWrapper(HttpClient httpClient)
            {
                HttpClient = httpClient;
                LastAccess = DateTime.Now;
                RefCount = 1;
            }

            public DateTime LastAccess { get; set; }
            public HttpClient HttpClient { get; }
            public int RefCount { get; set; }
        }
    }
}