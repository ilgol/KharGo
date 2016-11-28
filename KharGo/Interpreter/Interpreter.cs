using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Forms;
using KharGo;
using System;
using KharGo.Learning;

namespace KharGo.Intepreter
{
    public class Interpreter
    {
        private string _text;
        List<string> unknown = new List<string>();

        public Interpreter(string text)
        {
            _text = text;
        }
        public string Execute()
        {
            List<string> result = TryToRecognize(_text.Split(' ').ToList());
            if (unknown.Count > 0)
            {
                Spelling spelling = new Spelling();
                List<string> temp = new List<string>();

                Dictionary<string, string> learnDict = new Dictionary<string, string>();
                foreach (var item in unknown)
                {
                    string spellingword = Spelling.Correct(item);
                    if (spellingword != item)
                    {
                        temp.Add(spellingword);
                        List<string> templist = new List<string>() { temp.Last() };
                        learnDict.Add(TryToRecognize(templist).Where(x => x!="").First(), item);
                    }
                }
                temp.AddRange(result.Where(x => x != ""));
                unknown.Clear();
                result = TryToRecognize(temp);
                if (temp != result && result.Where(x => x != "").ToList().Count >= 2)
                {

                    string message = $"Did your mean {result[0]} {result[1]}?";
                    string caption = "Error Detected in Input";
                    MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                    DialogResult dialresult;


                    dialresult = MessageBox.Show(message, caption, buttons);

                    if (dialresult ==DialogResult.Yes)
                    {
                        foreach (var item in Meaning.Items.Values)
                        {
                            if (item.Com != null && item.Com.GetWord() == result[0])
                            {
                                try
                                {
                                    item.meaning.Add(learnDict[result[0]]);
                                }
                                //это никто не должен увидеть
                                catch { }
                            }
                            else if (item.Com != null && item.Com.GetWord() == result[1])
                            {
                                try
                                {
                                    item.meaning.Add(learnDict[result[1]]);
                                }
                                //и это тоже... но так надо.
                                catch { }
                            }
                        }
                        Meaning.Write();
                    }
                    else if (dialresult == DialogResult.No)
                    {
                        ((MainWindow)System.Windows.Application.Current.MainWindow).tabControl.SelectedItem = ((MainWindow)System.Windows.Application.Current.MainWindow).TabLearnig;
                        return "let's learn it";
                    }
                }
                else
                {
                    ((MainWindow)System.Windows.Application.Current.MainWindow).tabControl.SelectedItem = ((MainWindow)System.Windows.Application.Current.MainWindow).TabLearnig;
                    return "let's learn it";
                }
            }
            else if (((MainWindow)System.Windows.Application.Current.MainWindow).execute_tb.Text.ToLower().Split(' ').ToList().Count == 2 && ((MainWindow)System.Windows.Application.Current.MainWindow).tabControl.SelectedItem == ((MainWindow)System.Windows.Application.Current.MainWindow).TabLearnig)
            {
                //обучение новой строки при явному указании команды во вкладке обучение
                //работает только для строки из двух слов: action & target или наоборот !!
                List<string> temp = new List<string>() { "",""};
                List<string> wordssimilarity = new List<string>();
                List<string> words = ((MainWindow)System.Windows.Application.Current.MainWindow).execute_tb.Text.ToLower().Split(' ').ToList();
                foreach (var item in words)
                {
                    double counter0 = 0;
                    double counter1 = 0;
                    foreach (var meanitem in Meaning.Items.Values)
                    {
                        if (meanitem.Com.GetWord() == result[0])
                        {
                            foreach (var synonim in meanitem.meaning)
                            {
                                counter0 += item.ToLower().CalculateSimilarity(synonim.ToLower());
                            }
                        }
                        if (meanitem.Com.GetWord() == result[1])
                        {
                            foreach (var synonim in meanitem.meaning)
                            {
                                counter1 += item.ToLower().CalculateSimilarity(synonim.ToLower());
                            }
                        }

                    }
                    wordssimilarity.Add(counter0 > counter1 ? "similarToRes0" : "similarToRes1");
                }

                if (wordssimilarity[0] == "similarToRes0")
                         temp[0] = words[0];
                else temp[1] = words[0];

                if (wordssimilarity[1] == "similarToRes0")
                    temp[0] = words[1];
                else temp[1] = words[1];

                foreach (var item in Meaning.Items.Values)
                {
                    if (item.Com != null && item.Com.GetWord() == result[0])
                            item.meaning.Add(temp[0]);
                    else if (item.Com != null && item.Com.GetWord() == result[1])
                            item.meaning.Add(temp[1]);
                }
            }
            return string.Join(" ", result);
        }
        

        private List<string> TryToRecognize(List<string> list)
        {
            int count = 0;
            int oldcount = 0;
            List<string> result = new List<string>() { "", "", "" };
            list.ForEach(x =>
            {
                foreach (var item in Meaning.Items.Values)
                    foreach (var synonim in item.meaning)
                        if (x == synonim)
                        {
                            count++;
                            switch (item.Com.GetTypeofWord())
                            {
                                case "action":
                                    result[0] = item.Com.GetWord();
                                    break;
                                case "target":
                                    result[1] = item.Com.GetWord();
                                    break;
                                default:
                                    break;
                            }
                        }
                if (count == oldcount)
                    unknown.Add(x);
                else oldcount++;
            });
            return result;
        }
    }
}
