using KharGo.Command;

namespace KharGo.Strategy
{
    interface IStrategy
    {
        AbstractCommand Algorithm(string context);
    }
}
