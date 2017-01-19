using KharGo.Command;

namespace KharGo.Strategy
{
    public interface IStrategy
    {
        AbstractCommand Algorithm(string context);
    }
}
