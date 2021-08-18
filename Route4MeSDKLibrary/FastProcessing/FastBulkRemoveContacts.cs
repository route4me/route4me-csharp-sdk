using Route4MeSDK.DataTypes.V5;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Route4MeSDK.FastProcessing
{
    public class FastBulkRemoveContacts
    {
        public int TotalRemovedContacts { get; set; }

        public int ChunkPause { get; set; } = 2000;

        static List<List<int>> threadPackage; // Threads package with address IDs to remove.

        public int RemoveContactsChunkSize { get; set; } = 300;

        public string apiKey { get; set; }

        public bool PrintMessagesOnConsole { get; set; } = false;

        public FastBulkRemoveContacts(string ApiKey)
        {
            if (ApiKey != "") apiKey = ApiKey;

            threadPackage = new List<List<int>>();
        }

        public int RemoveAddressBookContacts(int[] contactIDs, out string errorString)
        {
            errorString = "";
            TotalRemovedContacts = 0;

            int chunksNumber = Math.DivRem(contactIDs.Length, RemoveContactsChunkSize, out int remainder);

            chunksNumber = remainder > 0 ? chunksNumber + 1 : chunksNumber;

            int i = 0;

            int[][] chunks = contactIDs.GroupBy(s => i++ / RemoveContactsChunkSize).Select(g => g.ToArray()).ToArray();

            for (i = 0; i < chunks.Length; i++) threadPackage.Add(chunks[i].ToList());

            Parallel.ForEach(threadPackage, chunk =>
            {
                RemoveContactsChunks(chunk);
            });

            return TotalRemovedContacts;
        }

        private async void RemoveContactsChunks(List<int> chunk)
        {
            var route4Me = new Route4MeManagerV5(apiKey);

            var removed = route4Me.RemoveAddressBookContacts(
                                        chunk.ToArray(),
                                        out ResultResponse resultResponse);
            if (PrintMessagesOnConsole)
            {
                Console.WriteLine(removed
                ? $"The chunk of the {chunk.Count} contacts removed --- {TotalRemovedContacts}"
                : $"Cannot remove the chunk of the contacts: {Environment.NewLine}" + string.Join(",", chunk));
            }

            if (removed) TotalRemovedContacts += chunk.Count;

        }

    }
}
