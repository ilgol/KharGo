using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Logic.Intepreter
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
            var temp = Word.Items.Values;
            foreach (var item in temp)
            {
                foreach (var synonim in item.list)
                {
                    synonim.list = synonim.list.Distinct().ToList();
                }
            }
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
