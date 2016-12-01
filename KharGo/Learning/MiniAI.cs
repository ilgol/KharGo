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
        public static void AddingItems(string word, string action, List<string> list)
        {
            Word Word = new Word();
            Mean Mean = new Mean();

            Word.list = new List<Mean>();
            Word.word = word;
            Mean.type = action;
            Mean.list = list;
            Word.list.Add(Mean);

            Word.Write();
        }
    }
}
