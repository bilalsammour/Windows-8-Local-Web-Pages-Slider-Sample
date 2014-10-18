using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace LocalWebPagesSlider
{
    public static class JsonManager
    {
        public static T Deserialize<T>(string jsonStr)
        {
            MemoryStream memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(jsonStr));
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));

            memoryStream.Position = 0;
            T t = (T)ser.ReadObject(memoryStream);

            return t;
        }

        public static string Serialize<T>(T t)
        {
            MemoryStream memoryStream = new MemoryStream();
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));

            ser.WriteObject(memoryStream, t);

            memoryStream.Position = 0;
            StreamReader sr = new StreamReader(memoryStream);
            string jsonString = sr.ReadToEnd();

            return jsonString;
        }

        public async static Task<T> DeserializeFromFile<T>(string filePath)
        {
            Uri dataUri = new Uri(filePath);

            StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(dataUri);

            return await DeserializeFromFile<T>(file);
        }

        public async static Task<T> DeserializeFromFile<T>(StorageFile file)
        {
            string jsonText = await FileIO.ReadTextAsync(file);

            return Deserialize<T>(jsonText);
        }
    }
}
