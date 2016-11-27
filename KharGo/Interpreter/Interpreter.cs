using KharGo.Learning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KharGo.Intepreter
{
    public class Interpreter
    {
        private string _text;
        public Interpreter(string text)
        {
            _text = text;
        }
        public string Execute()
        {
            List<string> result = new List<string>();
            bool sort = false;
            _text.Split(' ').ToList().ForEach(x =>
                {
                    foreach (var item in MiniAI.ActionDictionaryList)
                    {
                        foreach (var key in item.Key)
                        {
                            if (x == key)
                            {
                                result.Add(item.Value.Keys.First());
                                sort = item.Value.Values.First();
                                break;
                            }
                        }
                    }
                });
            return sort != false ? string.Join(" ", result) : Swap(result);
        }
        private string Swap(List<string> result)
        {
            var left = result[0];
            var right = result[1];
            var temp = left;
            left = right;
            right = temp;
            return left + " " +right;            
        }
    }
}
