using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Logic.Intepreter
{
    //базовый класс для хранения всей информации
    [JsonObject("Base")]
    public class Base<T> where T : Base<T>
    {
        public static Dictionary<Guid, T> Items = new Dictionary<Guid, T>();
        [JsonProperty("ID")]
        public Guid Id { get; private set; }

        public Base()
        {
            Id = Guid.NewGuid();
            Items.Add(Id, (T)this);
        }
    }
}