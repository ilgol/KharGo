using System;
using System.Xml.Serialization;
using System.IO;
using System.Collections.Generic;

namespace KharGo
{
    [Serializable]
    public class VoiceCommand
    {        
        public string command { get; set; }
        public string answer { get; set; }
        public VoiceCommand()
        { }
        public VoiceCommand(string cmd, string ans)
        {
            command = cmd;
            answer = ans;
        }
        public static void Write(VoiceCommand command)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(VoiceCommand));

            using (FileStream fs = new FileStream("commands.xml", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, command);
            }
        }
        public static List<VoiceCommand> Read()
        {
            XmlSerializer formatter = new XmlSerializer(typeof(VoiceCommand));
            List<VoiceCommand> newCommand = new List<VoiceCommand>();
            using (FileStream fs = new FileStream("commands.xml", FileMode.OpenOrCreate))
            {
                newCommand.Add((VoiceCommand)formatter.Deserialize(fs));
            }
            return newCommand;
        }
    }
}
