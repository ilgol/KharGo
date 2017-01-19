namespace Logic.Command
{
    public abstract class AbstractCommand
    {
        public abstract void Execute();
        public abstract void Undo();
    }
}
