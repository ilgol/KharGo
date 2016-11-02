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
        ComboBoxContext cbt;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void accept_bt_Click(object sender, RoutedEventArgs e)
        {
            //if (object_cb.Text != "" & param_cb.Text != "")
            //{
            //    rules_lb.Items.Add($"{command_cb.Text} {object_cb.Text.ToLower()} {param_cb.Text.ToLower()}");

            //    Invoker invoker = new Invoker();
            //    StartCommand command = new StartCommand($"{object_cb.Text.ToLower()}.exe");
            //    invoker.SetCommand(command);
            //    invoker.Run();
            //}
            //else
            //{
            //    MessageBox.Show("Вы не установили полностью команду!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            //}

            Invoker invoker = new Invoker();
            StartCommand command = new StartCommand($"{execute_tb.Text}");
            invoker.SetCommand(command);
            invoker.Run();
        }
        private void command_cb_DropDownClosed(object sender, System.EventArgs e)
        {
            cbt = new ComboBoxContext(command_cb.Text);

            cbt._object = new ObjectStrategy();
            var temp = cbt.Execute(true, command_cb.Text);
            object_cb.Items.Clear();
            object_cb.SelectedIndex = 0;
            temp.ForEach(x => object_cb.Items.Add(x));

            cbt._parameter = new ParameterStrategy();
            temp = cbt.Execute(false, command_cb.Text);
            param_cb.Items.Clear();
            param_cb.SelectedIndex = 0;
            temp.ForEach(x => param_cb.Items.Add(x));

            param_cb.IsEnabled = true;
            object_cb.IsEnabled = true;
            if (object_cb.Items.Count != 1)
                object_cb.IsDropDownOpen = true;
            else
                param_cb.IsDropDownOpen = true;
        }
        private void object_cb_DropDownClosed(object sender, EventArgs e)
        {
            if (param_cb.Items.Count != 1)
                param_cb.IsDropDownOpen = true;
        }
        private void cancel_bt_Click(object sender, RoutedEventArgs e)
        {
            if (rules_lb.SelectedItem != null)            
                rules_lb.Items.Remove(rules_lb.SelectedItem);            
            cancel_bt.IsEnabled = false;
        }
        private void rules_lb_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            cancel_bt.IsEnabled = true;
        }
        private void checkBox_Click(object sender, RoutedEventArgs e)
        {
            if (checkBox.IsChecked == true)
            {
                command_cb.ToolTip = null;
                param_cb.ToolTip = null;
                object_cb.ToolTip = null;
                rules_lb.ToolTip = null;
            }
            else
            {
                command_cb.ToolTip = "Выберете команду";
                param_cb.ToolTip = "Задайте параметры для выполнения команды";
                object_cb.ToolTip = "Выберете объект для которого надо выполнить команду";
                rules_lb.ToolTip = "Тут будут отображаться текущее задач";
            }
        }
    }
}