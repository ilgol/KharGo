using Logic.Intepreter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KharGo.MVVM
{
    public class MainWindowViewModel : ViewModelBase
    {
        protected List<string> commandList { get; set; }

        public List<string> CommandList
        {
            get
            { return commandList; }
            set
            {
                commandList = value;
                RaisePropertyChanged();
            }
        }
    }
}