using NAudio.Wave;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace KharGo.SpeechToText
{
    /// <summary>
    /// STT with Singleton
    /// </summary>
    public class STT
    {
        static STT instance;
        DispatcherTimer dispatcherTimer;
        WaveIn waveIn;
        WaveFileWriter writer;
        string outputFilename = "demo.wav";
        bool ON = false;
        List<VoiceCommand> cmd = new List<VoiceCommand>();
        SpeechSynthesizer synth = new SpeechSynthesizer();

        public string Result { get; set; }

        private STT()
        {
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 100);
        }
        public static STT getInstance()
        {
            if (instance == null)
                instance = new STT();
            return instance;
        }
        
        void waveIn_DataAvailable(object sender, WaveInEventArgs e)
        {
            writer.Write(e.Buffer, 0, e.BytesRecorded);
        }

        void waveIn_RecordingStopped(object sender, EventArgs e)
        {
            waveIn.Dispose();
            waveIn = null;
            writer.Close();
            writer = null;
        }



        void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            WebRequest request = WebRequest.Create("https://www.google.com/speech-api/v2/recognize?output=json&lang=ru-RU&key=AIzaSyBOti4mM-6x9WDnZIjIeyEU21OpBXqWBgw");

            request.Method = "POST";
            byte[] byteArray = File.ReadAllBytes(outputFilename);
            request.ContentType = "audio/l16; rate=16000";
            request.ContentLength = byteArray.Length;
            request.GetRequestStream().Write(byteArray, 0, byteArray.Length);

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            StreamReader reader = new StreamReader(response.GetResponseStream());
            Parse(reader.ReadToEnd());

            reader.Close();
            response.Close();

            dispatcherTimer.Stop();
        }

        void Parse(string toParse)
        {
            string toparse = toParse.Replace("{\"result\":[]}\n", "");
            var root = JsonConvert.DeserializeObject<RootObject>(toparse);
            try
            {
                var trans = root.result[0].alternative;
                Result = trans[0].transcript;
            }
            catch { Voice("Извините я вас не поняла"); }
        }
        public void Voice(string ans)
        {
            SpeechSynthesizer synth = new SpeechSynthesizer();
            synth.SetOutputToDefaultAudioDevice();
            synth.Rate = 0;
            synth.Speak(ans);
        }

        public string StartRecord()
        {
            var resultname = "";
            if (ON == false)
            {
                waveIn = new WaveIn();
                waveIn.DeviceNumber = 0;
                waveIn.DataAvailable += waveIn_DataAvailable;
                waveIn.RecordingStopped += new EventHandler<StoppedEventArgs>(waveIn_RecordingStopped);
                waveIn.WaveFormat = new WaveFormat(16000, 1);
                writer = new WaveFileWriter(outputFilename, waveIn.WaveFormat);
                resultname = "Стоп";
                waveIn.StartRecording();
                ON = true;
            }
            else
            {
                waveIn.StopRecording();
                ON = false;
                resultname = "Сказать команду";
                dispatcherTimer.Start();
            }

            return resultname;
        }
    }
}
