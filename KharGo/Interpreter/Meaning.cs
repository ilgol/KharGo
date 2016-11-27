using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace KharGo.Intepreter
{
    [DataContract]
    public class Meaning : Base<Meaning>
    {
        [DataMember]
        public List<string> meaning { get; set; }
        [DataMember]
        private Guid _wordId;
        public Word Com
        {
            get
            {
                if (_wordId == Guid.Empty)
                    return null;
                return Word.Items[_wordId];
            }
            set
            {
                if (value == null)
                    _wordId = Guid.Empty;
                else
                    _wordId = value.Id;
            }
        }
        public static void Write()
        {
            DataContractSerializer serialization = new DataContractSerializer(typeof(Dictionary<Guid, Meaning>));
            FileStream file = new FileStream("Meaning.xml", FileMode.Create);
            serialization.WriteObject(file, Items);
            file.Close();
        }
        public static void Read()
        {
            DataContractSerializer serialization = new DataContractSerializer(typeof(Dictionary<Guid, Meaning>));
            FileStream file = new FileStream("Meaning.xml", FileMode.Open);
            XmlDictionaryReader reader = XmlDictionaryReader.CreateTextReader(file, new XmlDictionaryReaderQuotas());
            Items = (Dictionary<Guid, Meaning>)serialization.ReadObject(reader, true);
            file.Close();
        }
    }
}
