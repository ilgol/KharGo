using System.Linq;
using Logic.Intepreter;
using Logic.Strategy;
using Logic.Command;

namespace KharGo.Facade
{
    public class MenuFacade
    {
        public string Run(string command)
        {
            var res = "";
            foreach (var words in Word.Items.Values)
            {
                foreach (var word in words.list)
                {
                    if (word.list[0] == command)
                    {
                        res = words.word;
                        return res;
                    }
                }
            }
            return res;
        }
    }
}
