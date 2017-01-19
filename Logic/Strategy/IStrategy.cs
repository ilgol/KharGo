using Logic.Command;

namespace Logic.Strategy
{
    public interface IStrategy
    {
        AbstractCommand Algorithm(string context);
    }
}
