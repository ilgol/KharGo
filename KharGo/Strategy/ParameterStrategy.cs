using System;
using System.Collections.Generic;

namespace KharGo.Strategy
{
    class ParameterStrategy : IStrategy
    {
        List<string> result = new List<string>();
        public List<string> Algorithm(string context)
        {
            switch (context)
            {
                case "Запустить":
                    result.Add("После какого-то времени");
                    result.Add("Сейчас");
                    break;
                case "Загрузить":
                    result.Add("С сайта");
                    break;
                case "Воспроизвести":
                    result.Add("С сайта");
                    result.Add("С компьютера");
                    break;
                default:
                    break;
            }
            return result;
        }
    }
}
