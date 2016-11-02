using System.Collections.Generic;
using KharGo.Command;

namespace KharGo.Strategy
{
    class ActionStrategy : IStrategy
    {
        AbstractCommand result;
        public AbstractCommand Algorithm(string context)
        {
            switch (context.Split(' ')[0])
            {
                case "запустить":
                    result = new StartCommand(context.Split(' ')[1]);
                    break;                   
                default:
                    break;
            }
            return result;
        }
    }
}
