using KharGo.Intepreter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KharGo.Learning
{
    public class MiniAI
    {
        public static Dictionary<List<string>, Dictionary<string,string>> ActionDictionaryList;
        public static void AddingItems(Dictionary<List<string>, Dictionary<string, string>> adl)
        {
            ActionDictionaryList = adl;
            foreach (var item in adl)
            {
                Word word = new Word(item.Value.Keys.First(), item.Value.Values.First());
                Meaning meaning = new Meaning();
                meaning.Com = word;
                meaning.meaning = new List<string>(item.Key);
            }
            Meaning.Write();
            Word.Write();
        }
    }
}
