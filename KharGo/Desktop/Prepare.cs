using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Management.Automation;
using Logic.Intepreter;

namespace KharGo.Desktop
{
    public static class Prepare
    {
        public static void InitalizeProgramms()
        {
            using (PowerShell PowerShellInstance = PowerShell.Create())
            {
                PowerShellInstance.AddCommand("Get-ItemProperty");
                PowerShellInstance.AddArgument(@"HKLM:\SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\*");
                Dictionary<string, string> programs = new Dictionary<string, string>();
                PowerShellInstance.AddCommand("Select-Object");
                PowerShellInstance.AddArgument(new List<string>() { "PSChildName", "(Default)" });
                Collection<PSObject> PSOutput = PowerShellInstance.Invoke();

                foreach (var item in PSOutput)
                {
                    string[] result = item.ToString().Split(';');
                    var name = result[0].Substring(14);
                    var path = result[1].Substring(11).Replace("}", string.Empty);
                    if (name.Contains(".exe")/* && path != ""*/)
                    {
                        programs.Add(name, path);

                        Word data = new Word();
                        Mean mean = new Mean();

                        data.word = name.Replace(".exe", string.Empty).ToLower();
                        mean.type = "target";
                        data.list = new List<Mean>();
                        mean.list = new List<string>();
                        mean.list.Add(name.Replace(".exe", string.Empty).ToLower());
                        data.list.Add(mean);

                        Word.Write();
                    }
                }
            }
        }
    }
}
