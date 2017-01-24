using System.Windows;
using System.Collections.Generic;
using System.IO;
using System.Windows.Controls;
using KharGo.SpeechToText;
using KharGo.MVVM;
using KharGo.Facade;
using KharGo.Desktop;
using Logic.Factory_method;
using Logic.Intepreter;
using System.Threading;

namespace KharGo
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private MainWindowViewModel dataContext;
        STT instance;

        public MainWindow()
        {
            InitializeComponent();

            instance = STT.getInstance();
            dataContext = new MainWindowViewModel();
            dataContext.CommandList = new List<string>();
            dataContext.Accept = "Сказать команду";
            dataContext.Apply = "Применить";
            dataContext.TabControlName = tabcontrol;

            DataContext = dataContext;

            if (!File.Exists("Data.json"))
            {
                ActionWordFactory factory = new StartWordFactory();
                factory.Create();

                factory = new StopWordFactory();
                factory.Create();

                Prepare.InitalizeProgramms(); 

            }

            Word.Read();

            foreach (var values in Word.Items.Values)
            {
                foreach (var item in values.list)
                {
                    if (item.type == "action")
                        listBox_action.Items.Add(values.word);
                    else if (item.type == "target")
                        listBox_target.Items.Add(values.word);
                }
            }
        }

        bool ON = false;

        private void accept_bt_Click(object sender, RoutedEventArgs e)
        {
            if (!ON)
            {
                dataContext.Accept = instance.StartRecord();
                ON = true;
            }
            else
            {
                dataContext.Accept = instance.StopRecord();
                dataContext.Text = instance.Result;
                ON = false;
            }
        }

        private void apply_btn_Click(object sender, RoutedEventArgs e)
        {
            MenuFacade facade = new MenuFacade();             

            if (dataContext.TabControlName.SelectedIndex == 1)
            {
                if (listBox_target.SelectedItem.ToString() == "" && listBox_action.SelectedItem.ToString() == "")
                {
                    MessageBox.Show("Выберите действия!", "Ошибка ввода!", MessageBoxButton.OK, MessageBoxImage.Stop);
                    return;
                }
                facade.Run(listBox_action.SelectedItem.ToString().ToLower() + ' ' + listBox_target.SelectedItem.ToString().ToLower(), dataContext);

                Word.Write();
            }
            else
            {
                if (dataContext.Text != "")
                {
                    facade.Run(dataContext.Text, dataContext);
                }
            }
        }


        private void rules_lb_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (MessageBox.Show("Ви хотите применить эту команду?", "Вопрос", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                dataContext.Text = dataContext.CommandList[((ListBox)sender).SelectedIndex].Split('-')[0].Trim();
                apply_btn_Click(null, null);
            }
        }
    }
}