namespace KharGo.Command
{
    // инициатор команды
    class Invoker
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
