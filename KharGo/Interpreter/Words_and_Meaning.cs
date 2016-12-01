using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace KharGo.Intepreter
{
    [JsonObject("Words&Meanings")]
    public class Word : Base<Word>
    {
        [JsonProperty("Word")]
        public string word { get; set; }
        [JsonProperty("Means")]
        public List<Mean> list{ get; set; }
        public static void Read()
        {
            Items = JsonConvert.DeserializeObject<Dictionary<Guid,Word>>(File.ReadAllText(@"Data.json"));
        }
        public static void Write()
        {
            File.WriteAllText(@"Data.json", JsonConvert.SerializeObject(Items));
        }
    }
    public class Mean
    {
        [JsonProperty("Type")]
        public string type { get; set; }
        [JsonProperty("Meanings")]
        public List<string> list { get; set; }
    }
}
