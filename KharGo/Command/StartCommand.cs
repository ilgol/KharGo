using System.Diagnostics;

namespace KharGo.Command
{
    // конкретная команда
    class StartCommand : Command
    {
        private string _exename;
        public StartCommand(string exename)
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
            Process proc = Process.Start(_exename);
        }
        public override void Undo()
        {
            
        }
    }
}
