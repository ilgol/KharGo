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
        public static Dictionary<List<string>, Dictionary<string,bool>> ActionDictionaryList;
        public static void AddingItems(Dictionary<List<string>, Dictionary<string,bool>> adl)
        {
            ActionDictionaryList = adl;
            foreach (var item in adl)
            {
                Word word = new Word(item.Value.Keys.FirstOrDefault());
                Meaning meaning = new Meaning();
                meaning.Com = word;
                meaning.meaning = new List<string>();
                meaning.meaning.AddRange(item.Key);
            }
            Meaning.Write();
            Word.Write();
        }
    }
}
