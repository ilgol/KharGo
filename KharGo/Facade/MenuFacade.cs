using System.Linq;
using KharGo.Intepreter;
using KharGo.Strategy;
using KharGo.Command;
using KharGo.MVVM;
using KharGo.SpeechToText;
using System.Windows;
using System.Windows.Controls;

namespace KharGo.Facade
{
    public class MenuFacade
    {
        Interpreter _interpreter;
        Invoker _invoker;
        Context _context;

        public void Run(string command, MainWindowViewModel dataContext)
        {
            _invoker = new Invoker();
            _interpreter = new Interpreter(command.ToLower());

            if (dataContext.Text.ToLower().Split(' ').ToList().Count == 2 && dataContext.TabControlName.SelectedItem.ToString() == "TabLearning")
            {
                _interpreter.LearnTwoWordCommand(dataContext.Text);
            }

            _context = new Context(_interpreter.Execute());


            if (_context._action == "let's learn it")
            {
                dataContext.TabControlName.SelectedItem = dataContext.TabControlName.Items[1];
                MessageBox.Show("Что вы имели ввиду?", "Неизвестная команда", MessageBoxButton.OK);
                return;
            }

            STT.getInstance().Voice($"Выполняю {dataContext.Text}");

            _context._object = new ActionStrategy();
            var result = _context.Execute();

            _invoker.SetCommand(result);
            _invoker.Run();

            dataContext.CommandList.Add(dataContext.Text);
            dataContext.Text = "";
        }
    }
}
