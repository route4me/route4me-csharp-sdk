using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Runtime.Serialization.Formatters.Binary;

namespace Route4MeSDK.FastProcessing
{
    public class FastFileReading
    {
        const long offset = 0x10000000; // 256 megabytes
        const long length = 0x20000000; // 512 megabytes

        public void fastReadFromFile(String sFileName)
        {
            if (sFileName.Substring(1, 1) != ":")
            {
                String startupPath = AppDomain.CurrentDomain.BaseDirectory;
                sFileName = startupPath + "/" + sFileName;

            }

            using (MemoryMappedFile memoryMappedFile = MemoryMappedFile.CreateFromFile(sFileName))
            {

                using (MemoryMappedViewStream memoryMappedViewStream = memoryMappedFile.CreateViewStream(0, 1204, MemoryMappedFileAccess.Read))
                {

                    var contentArray = new byte[1024];

                    memoryMappedViewStream.Read(contentArray, 0, contentArray.Length);

                    string content = Encoding.UTF8.GetString(contentArray);

                }

            }
        }

        public string readJsonTextFromFile(String sFileName)
        {
            if (!File.Exists(sFileName))
            {
                Console.WriteLine("The file " + sFileName + " doesn't exist..."); return "";
            }

            string jsonContent = File.ReadAllText(sFileName);

            return jsonContent;
        }

        public List<T> getObjectsListfromJsonText<T>(string jsonText)
        {
            List<T> lsObjects = fastJSON.JSON.ToObject<List<T>>(jsonText);

            return lsObjects;
        }

        public List<T> getObjectsListFromJsonFile<T>(String sFileName)
        {
            String jsonText = readJsonTextFromFile(sFileName);

            List<T> lsObjects = getObjectsListfromJsonText<T>(jsonText);

            return lsObjects;
        }

    }
}
