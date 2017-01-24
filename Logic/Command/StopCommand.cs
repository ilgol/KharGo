using System.Diagnostics;
using System.Windows;

namespace Logic.Command
{
    // конкретная команда
    class StopCommand : AbstractCommand
    {
        private string _exename;
        public StopCommand(string exename)
        {
            _exename = exename;
        }

        public override void Execute()
        {
            // Prepare the process to run
            ProcessStartInfo start = new ProcessStartInfo();
            // Enter the executable to run, including the complete path
            start.FileName = _exename;
            // Run the external process & wait for it to finish
            try
            {
                foreach (var proc in Process.GetProcessesByName(_exename))
                {
                    proc.Kill();
                }
            }
            catch { MessageBox.Show("Не удается открыть файл", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        public override void Undo()
        {
            
        }
    }
}
