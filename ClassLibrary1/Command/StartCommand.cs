using System.Diagnostics;
using System.Windows;

namespace Logic.Command
{
    // конкретная команда
    class StartCommand : AbstractCommand
    {
        private string _exename;
        public StartCommand(string exename)
        {
            _exename = exename;
        }
        public override void Execute()
        {
            try
            {
                Process proc = Process.Start(_exename);
            }
            catch { MessageBox.Show("Не удается открыть файл", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }
        }
        public override void Undo()
        {
            
        }
    }
}
