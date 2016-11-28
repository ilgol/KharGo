using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KharGo.Command;

namespace KharGo.Strategy
{
    class Context
    {
        public string _action { get; private set; }
        public IStrategy _object { private get; set; }
        public Context(string action)
        {
            _action = action;
        }
        public AbstractCommand Execute()
        {
            return _object.Algorithm(_action);
        }
    }
}
