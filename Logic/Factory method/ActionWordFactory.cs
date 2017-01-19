using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KharGo.Intepreter;
namespace KharGo.Factory_method
{
    public abstract class ActionWordFactory
    {
        abstract public Word Create();
    }
}
