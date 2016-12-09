using System.Windows;
using KharGo.Strategy;
using KharGo.Command;
using KharGo.Intepreter;
using System.Collections.Generic;
using NAudio.Wave;
using System.Speech.Synthesis;
using Newtonsoft.Json;
using System.Net;
using System.IO;
using System;
using System.Diagnostics;
using System.Windows.Threading;
using System.Windows.Controls;

using System.Management.Automation;
using System.Collections.ObjectModel;

namespace KharGo
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            if (!File.Exists("Data.json"))
            {
                Word data1 = new Word();
                Mean mean1 = new Mean();

                data1.list = new List<Mean>();
                data1.word = "запустить";
                mean1.type = "action";
                mean1.list = new List<string> { "open", "старт", "открыть", "запустить", "откріть", "popen", "запустити", "запускай", "запустить", "open", "open", "открытфь", "открой", "открывай", "откры", "запуск" };
                data1.list.Add(mean1);

                using (PowerShell PowerShellInstance = PowerShell.Create())
                {
                    PowerShellInstance.AddCommand("Get-ItemProperty");
                    PowerShellInstance.AddArgument(@"HKLM:\SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\*");
                    Dictionary<string, string> programs = new Dictionary<string, string>();
                    PowerShellInstance.AddCommand("Select-Object");
                    PowerShellInstance.AddArgument(new List<string>() { "PSChildName", "(Default)" });
                    Collection<PSObject> PSOutput = PowerShellInstance.Invoke();
                    foreach (var item in PSOutput)
                    {
                        string[] result = item.ToString().Split(';');
                        var name = result[0].Substring(14);
                        var path = result[1].Substring(11).Replace("}", string.Empty);
                        if (name.Contains(".exe")/* && path != ""*/)
                        {
                            programs.Add(name, path);

                            ///проверяю, все ли работает
                            // Prepare the process to run
                            ProcessStartInfo start = new ProcessStartInfo();
                            // Enter the executable to run, including the complete path
                            start.FileName = name;
                            // Run the external process & wait for it to finish
                            //Process proc = Process.Start(name.Replace(".exe", string.Empty));

                            Word data = new Word();
                            Mean mean = new Mean();

                            data.word = name.Replace(".exe", string.Empty).ToLower();
                            mean.type = "target";
                            data.list = new List<Mean>();
                            mean.list = new List<string>();
                            mean.list.Add(name.Replace(".exe", string.Empty).ToLower());
                            data.list.Add(mean);

                            Word.Write();
                        }
                    }
                }
            }

            Word.Read();

            //word data = new word();
            //word data1 = new word();
            //mean mean = new mean();
            //mean mean1 = new mean();

            //data.list = new List<mean>();
            //data._word = "запустить";
            //mean.type = "action";
            //mean.list = new List<string> { "open", "старт", "открыть", "запустить", "откріть", "popen", "запустити", "запускай", "запустить", "open", "open", "открытфь", "открой", "открывай", "откры", "запуск" };
            //data.list.Add(mean);

            //data1.list = new List<mean>();
            //data1._word = "skype";
            //mean1.type = "target";
            //mean1.list = new List<string> { "skype", "скайп", "скупе", "скаааайп", "скайпе", "skipe", "скайпєц", "скайпулю", "скайпуню", "skyyyyyp", "skyyype", "сйкап", "скйпрлоы", "скайп", "скайп", "скай", "скайп" };
            //data1.list.Add(mean1);

            //word.Write();

            foreach (var values in Word.Items.Values)
            {
                foreach (var item in values.list)
                {
                    if (item.type == "action")
                        cbAction.Items.Add(values.word);
                    else if (item.type == "target")
                        cbTarget.Items.Add(values.word);
                }
            }

            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 100);
        }

        DispatcherTimer dispatcherTimer;
        WaveIn waveIn;
        WaveFileWriter writer;
        string outputFilename = "demo.wav";
        bool ON = false;
        List<VoiceCommand> cmd = new List<VoiceCommand>();
        SpeechSynthesizer synth = new SpeechSynthesizer();

        private void accept_bt_Click(object sender, RoutedEventArgs e)
        {
            //Command cmd = new Command("Как тебя зовут","Меня зовут Джарвис");
            //Command.Write(cmd);

            if (ON == false)
            {
                waveIn = new WaveIn();
                waveIn.DeviceNumber = 0;
                waveIn.DataAvailable += waveIn_DataAvailable;
                waveIn.RecordingStopped += new EventHandler<NAudio.Wave.StoppedEventArgs>(waveIn_RecordingStopped);
                waveIn.WaveFormat = new WaveFormat(16000, 1);
                writer = new WaveFileWriter(outputFilename, waveIn.WaveFormat);
                accept_bt.Content = "Стоп";
                waveIn.StartRecording();
                ON = true;
            }
            else
            {
                waveIn.StopRecording();
                ON = false;
                accept_bt.Content = "Сказать команду";
                dispatcherTimer.Start();
            }
        }
        private void cancel_bt_Click(object sender, RoutedEventArgs e)
        {

            if (tabControl.SelectedItem == TabLearnig)
            {
                if (cbTarget.Text == "" && cbAction.Text == "")
                {
                    MessageBox.Show("Выберите действия!", "Ошибка ввода!", MessageBoxButton.OK, MessageBoxImage.Stop);
                    return;
                }
                RunCommand(cbAction.Text.ToLower() + ' ' + cbTarget.Text.ToLower());
                Word.Write();
            }
            else
            {
                if (execute_tb.Text != "")
                {
                    RunCommand(execute_tb.Text);
                }
            }
        }
        public void Switch()
        {
            tabControl.SelectedItem = TabLearnig;
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
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            WebRequest request = WebRequest.Create("https://www.google.com/speech-api/v2/recognize?output=json&lang=ru-RU&key=AIzaSyBOti4mM-6x9WDnZIjIeyEU21OpBXqWBgw");
            //
            request.Method = "POST";
            byte[] byteArray = File.ReadAllBytes(outputFilename);
            request.ContentType = "audio/l16; rate=16000"; //"16000";
            request.ContentLength = byteArray.Length;
            request.GetRequestStream().Write(byteArray, 0, byteArray.Length);

            // Получить ответ.
            HttpWebResponse response =  (HttpWebResponse) request.GetResponse();
            // Откройте поток, используя StreamReader для легкого доступа.
            StreamReader reader = new StreamReader(response.GetResponseStream());
            //Читайте содержание.
            Parse(reader.ReadToEnd().ToString());
            // Очистите потоки.
            reader.Close();
            response.Close();

            dispatcherTimer.Stop();
        }
        private void RunCommand(string command)
        {
            Invoker invoker = new Invoker();
            Interpreter interpreter = new Interpreter(command.ToLower());


            Context cbt = new Context(interpreter.Execute());

            if (cbt._action == "let's learn it")
            {
                MessageBox.Show("Что вы имели ввиду?", "Неизвестная команда", MessageBoxButton.OK);
                return;
            }

            Voice($"Открываю {cbt._action.Split(' ')[1]}");

            cbt._object = new ActionStrategy();
            var result = cbt.Execute();

            invoker.SetCommand(result);
            invoker.Run();
            rules_lb.Items.Add(execute_tb.Text);
            execute_tb.Clear();
        }
        public void Parse(string toParse)
        {
            //cmd = VoiceCommand.Read();
            string toparse = toParse.Replace("{\"result\":[]}\n", "");
            var root = JsonConvert.DeserializeObject<RootObject>(toparse);
            try
            {
                var trans = root.result[0].alternative;
                //foreach (var item in trans)
                //{
                //foreach (var item1 in cmd)
                //{
                //if (item.transcript == item1.command)
                //{
                execute_tb.Text = trans[0].transcript;
                //Voice(item1.answer);
                //}
                //}
                //}    
            }
            catch { Voice("Извините я вас не поняла"); }        
        }
        public void Voice(string ans)
        {
            SpeechSynthesizer synth = new SpeechSynthesizer();
            // Configure the audio output. 
            synth.SetOutputToDefaultAudioDevice();
            synth.Rate = 0;
            // Speak a string.
            synth.Speak(ans);
        }
        private void rules_lb_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (MessageBox.Show("Ви хотите применить эту команду?", "Вопрос", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                execute_tb.Text = rules_lb.SelectedItem.ToString();
            }
        }
    }
}