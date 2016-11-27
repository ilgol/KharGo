using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace KharGo.Intepreter
{
    //базовый класс для хранения всей информации
    [DataContract]
    public class Base<T> where T : Base<T>
    {
        public static Dictionary<Guid, T> Items = new Dictionary<Guid, T>();
        [DataMember]
        public Guid Id { get; private set; }

        public Base()
        {
            Id = Guid.NewGuid();
            Items.Add(Id, (T)this);
        }
    }
}