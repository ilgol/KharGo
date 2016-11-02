namespace KharGo.Command
{
    abstract class AbstractCommand
    {
        public abstract void Execute();
        public abstract void Undo();
    }
}
