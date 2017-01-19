using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic.Intepreter;
namespace Logic.Factory_method
{
    public  class StartWord : Word
    {
        public StartWord() 
        {
            this.list = new List<Mean>();
            this.word = "запустить";
            Mean mean = new Mean();
            mean.type = "action";
            mean.list = new List<string> { "open", "старт", "открыть",  "откріть", "popen", "запустити", "запускай", "открытфь", "открой", "открывай", "откры", "запуск" };
            this.list.Add(mean);

        }
    }
}
