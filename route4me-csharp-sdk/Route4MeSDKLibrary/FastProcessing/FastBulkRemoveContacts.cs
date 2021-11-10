using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Route4MeSDK.FastProcessing
{
    public class FastBulkRemoveContacts
    {
        private static List<List<int>> _threadPackage; // Threads package with address IDs to remove.

        public FastBulkRemoveContacts(string ApiKey)
        {
            if (ApiKey != "") apiKey = ApiKey;

            _threadPackage = new List<List<int>>();
        }

        public int TotalRemovedContacts { get; set; }

        public int ChunkPause { get; set; } = 2000;

        public int RemoveContactsChunkSize { get; set; } = 300;

        public string apiKey { get; set; }

        public bool PrintMessagesOnConsole { get; set; } = false;

        public int RemoveAddressBookContacts(int[] contactIDs, out string errorString)
        {
            errorString = "";
            TotalRemovedContacts = 0;

            var chunksNumber = Math.DivRem(contactIDs.Length, RemoveContactsChunkSize, out var remainder);

            chunksNumber = remainder > 0 ? chunksNumber + 1 : chunksNumber;

            var i = 0;

            var chunks = contactIDs.GroupBy(s => i++ / RemoveContactsChunkSize).Select(g => g.ToArray()).ToArray();

            for (i = 0; i < chunks.Length; i++) _threadPackage.Add(chunks[i].ToList());

            Parallel.ForEach(_threadPackage, new ParallelOptions {MaxDegreeOfParallelism = Environment.ProcessorCount},
                RemoveContactsChunks);

            return TotalRemovedContacts;
        }

        private void RemoveContactsChunks(List<int> chunk)
        {
            var route4Me = new Route4MeManagerV5(apiKey);

            var removed = route4Me.RemoveAddressBookContacts(
                chunk.ToArray(),
                out var resultResponse);
            if (PrintMessagesOnConsole)
                Console.WriteLine(removed
                    ? $"The chunk of the {chunk.Count} contacts removed --- {TotalRemovedContacts}"
                    : $"Cannot remove the chunk of the contacts: {Environment.NewLine}" + string.Join(",", chunk.Select(x => x.ToString())));

            if (removed) TotalRemovedContacts += chunk.Count;
        }
    }
}