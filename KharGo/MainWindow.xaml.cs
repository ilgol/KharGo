﻿using System.Windows;
using KharGo.Strategy;
using KharGo.Command;
using KharGo.Intepreter;
using KharGo.Learning;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System;

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
        }
        private void accept_bt_Click(object sender, RoutedEventArgs e)
        {
            if (tabControl.SelectedItem == TabLearnig )
            {
                if (cbTarget.Text == "" && cbAction.Text == "")
                {
                    MessageBox.Show("Выберите действия!", "Ошибка ввода!", MessageBoxButton.OK, MessageBoxImage.Stop);
                    return;
                }
                Invoker invoker = new Invoker();
                Interpreter interpreter = new Interpreter(cbAction.Text.ToLower() + ' ' + cbTarget.Text.ToLower());

                Context cbt = new Context(interpreter.Execute());
                cbt._object = new ActionStrategy();
                var result = cbt.Execute();
                invoker.SetCommand(result);
                invoker.Run();
                Word.Write();
            }
            else
            {
                if (execute_tb.Text != "")
                {
                    Invoker invoker = new Invoker();
                    Interpreter interpreter = new Interpreter(execute_tb.Text.ToLower());

                    Context cbt = new Context(interpreter.Execute());
                    if (cbt._action == "let's learn it")
                    {
                        MessageBox.Show("Что вы имели ввиду?", "Неизвестная команда", MessageBoxButton.OK);
                        return;
                    }
                    cbt._object = new ActionStrategy();
                    var result = cbt.Execute();
                    invoker.SetCommand(result);
                    invoker.Run();
                }
                else
                    MessageBox.Show("Введите команду!", "Ошибка ввода!", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
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