using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace KharGo
{
    [DataContract]
    public class MeaningfulWord
    {
        public static Dictionary<List<string>, string> ActionDictionaryList = new Dictionary<List<string>, string>() { { new List<string> { "skype", "скайп" }, "skype" },

                                                                                                                           { new List<string> { "open", "старт", "открыть" }, "запустить" } };
        [DataMember]
        public List<string> Meaning;
        [DataMember]
        public string Command;

        public void Serialize()
        {
            using (StreamReader file = File.OpenText(@"Command.json"))
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Dictionary<List<string>, string>));
            }
        }
    }


    //public static class MeaningfulWord
    //{
    //    public static Dictionary<List<string>, string> ActionDictionaryList = new Dictionary<List<string>, string>() { { new List<string> { "skype", "скайп" }, "skype" },

    //                                                                                                                   { new List<string> { "open", "старт", "открыть" }, "запустить" } };
    //    public static Dictionary<List<string>, string> Command;
    //    public static void Deserialize()
    //    {
    //        using (StreamReader file = File.OpenText(@"Command.json"))
    //        {
    //            JsonSerializer serializer = new JsonSerializer();
    //            Command = (Dictionary<List<string>,string>)serializer.Deserialize(file, typeof(Dictionary<List<string>,string>));
    //        }
    //    }

    //    public static void Serialize(Dictionary<List<string>, string> com)
    //    {
    //        using (StreamWriter file = File.CreateText(@"Command.json"))
    //        {
    //            JsonSerializer serializer = new JsonSerializer();
    //            int i = 0;
    //            foreach (var item in com.Values)
    //            {
    //                serializer.Serialize(file, item);
    //            }
    //        }
    //    }
    //}

}
