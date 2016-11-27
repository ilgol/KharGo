using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace KharGo.Intepreter
{
    [DataContract]
    public class Word : Base<Word>
    {
        [DataMember]
        private string _word;
        public Word(string word)
        {
            _word = word;
        }
        public static void Write()
        {
            DataContractSerializer serialization = new DataContractSerializer(typeof(Dictionary<Guid, Word>));
            FileStream file = new FileStream("Word.xml", FileMode.Create);
            serialization.WriteObject(file, Items);
            file.Close();
        }
        public static void Read()
        {
            DataContractSerializer serialization = new DataContractSerializer(typeof(Dictionary<Guid, Word>));
            FileStream file = new FileStream("Word.xml", FileMode.Open);
            XmlDictionaryReader reader = XmlDictionaryReader.CreateTextReader(file, new XmlDictionaryReaderQuotas());
            Items = (Dictionary<Guid, Word>)serialization.ReadObject(reader, true);
            file.Close();
        }

    }
}
