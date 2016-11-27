using System.Windows;
using KharGo.Strategy;
using KharGo.Command;
using KharGo.Intepreter;
using KharGo.Learning;
using System.Collections.Generic;

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

            //Dictionary<List<string>, Dictionary<string, string>> ActionDictionaryList = new Dictionary<List<string>, Dictionary<string, string>>()
            //{ { new List<string> { "skype", "скайп", "скупе", "скаааайп", "скайпе" }, new Dictionary<string,string> {{ "skype",  "target" }} },
            //{ new List<string> { "open", "старт", "открыть" }, new Dictionary<string, string> { { "запустить", "action" } } } };

            //MiniAI.AddingItems(ActionDictionaryList);
            Meaning.Read();
            Word.Read();
            foreach (var item in Word.Items.Values)
            {
                if (item.GetTypeofWord() == "action")
                    cbAction.Items.Add(item);
                else if (item.GetTypeofWord() == "target")
                    cbTarget.Items.Add(item);
            }

        }
        private void accept_bt_Click(object sender, RoutedEventArgs e)
        {
            if (execute_tb.Text != "")
            {
                Invoker invoker = new Invoker();
                Interpreter interpreter = new Interpreter(execute_tb.Text.ToLower());

                Context cbt = new Context(interpreter.Execute());
                cbt._object = new ActionStrategy();
                var result = cbt.Execute();
                invoker.SetCommand(result);
                invoker.Run();
            }
            else
               MessageBox.Show("Введите команду!", "Ошибка ввода!", MessageBoxButton.OK, MessageBoxImage.Stop);
        }
        private void cancel_bt_Click(object sender, RoutedEventArgs e)
        {
            if (rules_lb.SelectedItem != null)            
                rules_lb.Items.Remove(rules_lb.SelectedItem);            
            cancel_bt.IsEnabled = false;
        }

        public void Switch()
        {
            tabControl.SelectedItem = TabLearnig;
        }
    }
}