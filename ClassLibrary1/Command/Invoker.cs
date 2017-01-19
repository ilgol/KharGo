namespace Logic.Command
{
    // инициатор команды
    public class Invoker
    {
        AbstractCommand command;
        public void SetCommand(AbstractCommand c)
        {
            command = c;
        }
        public void Run()
        {
            command.Execute();
        }
        public void Cancel()
        {
            command.Undo();
        }
    }
}
