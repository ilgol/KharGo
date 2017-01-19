using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic.Intepreter;
namespace Logic.Factory_method
{
    public  class StopWordFactory : ActionWordFactory
    {
        public override Word Create()
        {
            return new StopWord();
        }
    }
}
