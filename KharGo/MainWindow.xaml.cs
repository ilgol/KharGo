using System;
using System.Windows;
using KharGo.Strategy;
using KharGo.Command;

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
        }
        private void accept_bt_Click(object sender, RoutedEventArgs e)
        {
            Invoker invoker = new Invoker();
            Interpreter interpreter = new Interpreter(execute_tb.Text);

            Context cbt = new Context(interpreter.Execute());
            cbt._object = new ActionStrategy();
            var result = cbt.Execute();
            invoker.SetCommand(result);
            invoker.Run();
        }
        private void cancel_bt_Click(object sender, RoutedEventArgs e)
        {
            if (rules_lb.SelectedItem != null)            
                rules_lb.Items.Remove(rules_lb.SelectedItem);            
            cancel_bt.IsEnabled = false;
        }
    }
}