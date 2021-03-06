﻿using System.Windows;
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
using KharGo.Factory_method;
using System.Management.Automation;
using System.Collections.ObjectModel;
using KharGo.SpeechToText;
using KharGo.MVVM;
using System.Linq;
using KharGo.Facade;
using KharGo.Desktop;

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
                        cbAction.Items.Add(values.word);
                    else if (item.type == "target")
                        cbTarget.Items.Add(values.word);
                }
            }
        }


        private void accept_bt_Click(object sender, RoutedEventArgs e)
        {
            dataContext.Accept = instance.StartRecord();

            dataContext.Text = instance.Result;

        }

        private void apply_btn_Click(object sender, RoutedEventArgs e)
        {
            MenuFacade facade = new MenuFacade();             

            if (dataContext.TabControlName.SelectedItem == TabLearnig)
            {
                if (cbTarget.Text == "" && cbAction.Text == "")
                {
                    MessageBox.Show("Выберите действия!", "Ошибка ввода!", MessageBoxButton.OK, MessageBoxImage.Stop);
                    return;
                }
                facade.Run(cbAction.Text.ToLower() + ' ' + cbTarget.Text.ToLower(), dataContext);

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
                dataContext.Text = dataContext.CommandList[((ListBox)sender).SelectedIndex];
            }
        }
    }
}