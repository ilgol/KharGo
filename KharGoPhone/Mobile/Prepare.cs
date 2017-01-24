using System.Collections.Generic;
using Logic.Intepreter;
using Windows.Storage;
using System.IO;
using System.Runtime.Serialization;
using System;
using Newtonsoft.Json;

namespace KharGo.Mobile
{
    public static class Prepare
    {
        public static Dictionary<string, string> commands = new Dictionary<string, string>
        {
            { "Display", "ms-settings-screenrotation:" },
            { "Notifications", "ms-settings-notifications:" },
            { "Battery Saver", "ms-settings-power://" },
            { "Bluetooth", "ms-settings-bluetooth:" },
            { "Wi-Fi", "ms-settings-wifi://" },
            { "Airplane mode", "ms-settings-airplanemode://" },
            { "Cellular", "ms-settings-cellular://" },
            { "Lock screen", "ms-settings-lock://" },
            { "Your account", "ms-settings-emailandaccounts://" },
            { "Your workplace", "ms-settings-workplace:" },
            { "Location", "ms-settings-location:" }
        };

        public static Dictionary<Guid, Word> InitalizeProgramms()
        {
            foreach (var item in commands)
            {

                Word data = new Word();
                Mean mean = new Mean();

                data.word = item.Value.ToLower();
                mean.type = "target";
                data.list = new List<Mean>();
                mean.list = new List<string>();
                mean.list.Add(item.Key.ToLower());
                data.list.Add(mean);
            }

            return Word.Items;
        }
    }
}
