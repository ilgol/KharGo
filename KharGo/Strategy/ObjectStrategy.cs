using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KharGo.Strategy
{
    class ObjectStrategy : IStrategy
    {
        List<string> result = new List<string>();
        public List<string> Algorithm(string context)
        {
            switch (context)
            {
                case "Запустить":
                    result.Add("Skype");
                    result.Add("Outlook");
                    break;
                case "Загрузить":
                    result.Add("Мелодию");
                    result.Add("Skype");
                    result.Add("Outlook");
                    break;
                case "Воспроизвести":
                    result.Add("Мелодию");
                    break;
                default:
                    break;
            }
            return result;
        }
    }
}
