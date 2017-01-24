using System.Linq;
using Logic.Intepreter;
using Logic.Strategy;
using Logic.Command;
using KharGo.MVVM;
using KharGo.SpeechToText;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using System;

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

            if (dataContext.Text.ToLower().Split(' ').ToList().Count == 2 && dataContext.TabControlName.SelectedIndex == 1)
            {
                _interpreter.LearnTwoWordCommand(command, dataContext.Text);
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

            List<string> temp = new List<string>();
            temp.AddRange(dataContext.CommandList);

            dataContext.CommandList = new List<string>();

            temp.Add(dataContext.Text + " ---> " + DateTime.Now);

            dataContext.CommandList = temp;
            dataContext.Text = "";
        }
    }
}
