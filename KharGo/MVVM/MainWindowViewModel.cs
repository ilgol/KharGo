using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace KharGo.MVVM
{
    public class MainWindowViewModel : ViewModelBase
    {
        protected string text { get; set; }
        protected List<string> commandList { get; set; }
        protected string accept { get; set; }
        protected string apply { get; set; }
        protected TabControl tabcontrolname { get; set; }

        public TabControl TabControlName
        {
            get { return tabcontrolname; }
            set
            {
                tabcontrolname = value;
                RaisePropertyChanged();
            }
        }

        public string Accept
        {
            get { return accept; }
            set
            {
                accept = value;
                RaisePropertyChanged();
            }
        }

        public string Apply
        {
            get { return apply; }
            set
            {
                apply = value;
                RaisePropertyChanged();
            }
        }

        public string Text
        {
            get { return text; }
            set
            {
                text = value;
                RaisePropertyChanged();
            }
        }

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