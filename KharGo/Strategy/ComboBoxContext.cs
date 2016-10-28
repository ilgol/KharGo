using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KharGo.Strategy
{
    class ComboBoxContext
    {
        protected string _command;
        public IStrategy _object { private get; set; }
        public IStrategy _parameter{ private get; set; }
        public ComboBoxContext(string com)
        {
            _command = com;
        }
        public List<string> Execute(bool a, string context)
        {
            return a ? _object.Algorithm(context) : _parameter.Algorithm(context);
        }
    }
}
