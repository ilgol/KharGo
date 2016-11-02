using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KharGo
{
    public class Interpreter
    {
        private readonly Dictionary<string, string> vocabulary = new Dictionary<string, string>()
        { { "скайп", "skype" }, { "skype", "skype" },
          { "старт", "запустить" }, { "открыть", "запустить" },
          { "open", "запустить"} }; 

        private string _text;
        public Interpreter(string text)
        {
            _text = text;
        }
        public string Execute()
        {
            List<string> result = new List<string>();
            _text.Split(' ').ToList().ForEach(x =>
                {
                    string tempResult;
                    if (vocabulary.TryGetValue(x, out tempResult))
                        result.Add(tempResult);
                });
            return string.Join(" ", result);
        }
    }
}
