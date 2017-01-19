using Logic.Intepreter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Factory_method
{
    public class StopWord : Word
    {
        public StopWord()
        {

            this.list = new List<Mean>();
            this.word = "остановить";
            Mean mean = new Mean();
            mean.type = "action";
            mean.list = new List<string> { "close", "kill", "остановить", "прекратить", "закрыть", "stop"};
            this.list.Add(mean);

        }
    }
}
