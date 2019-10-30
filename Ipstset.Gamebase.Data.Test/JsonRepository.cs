using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;


namespace Ipstset.Gamebase.Data.Test
{
    public class JsonRepository<T>
    {
        public const string FileDirectory =
               @"C:\Users\ipsts\Documents\Ipstset Software\Projects\Ipstset.Gamebase\Ipstset.Gamebase.Data.Test\Json\";
           //@"C:\Users\dhaug\Downloads\Ipstset.Gamebase_080918\Ipstset.Gamebase\Ipstset.Gamebase.Data.Test\Json\";

        public static string GetFile(string fileName)
        {
            return FileDirectory + fileName;
        }

        public List<T> Create(string file)
        {
            var json = string.Empty;
            using (StreamReader sr = File.OpenText(file))
            {
                json = sr.ReadToEnd();
                return JsonConvert.DeserializeObject<List<T>>(json) ?? new List<T>();
            }
        }


        public void SaveJsonToFile(object obj, string file)
        {
            using (FileStream fs = File.Open(file, FileMode.Truncate))
            using (StreamWriter sw = new StreamWriter(fs))
            using (JsonWriter jw = new JsonTextWriter(sw))
            {
                jw.Formatting = Formatting.Indented;
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(jw, obj);
            }
        }




    }
}
