using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Forms;
using Logic;
using System;
using Logic.Learning;

namespace Logic.Intepreter
{
    public class Interpreter
    {
        private string _text;
        List<string> unknown = new List<string>();
        List<string> result;

        public Interpreter()
        { }

        public Interpreter(string text)
        {
            _text = text;
        }
        public string Execute()
        {
            result = TryToRecognize(_text.Split(' ').ToList());
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
                        learnDict.Add(TryToRecognize(templist).Where(x => x != "").First(), item);
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

                    if (dialresult == DialogResult.Yes)
                    {
                        foreach (var item in Word.Items.Values)
                            foreach (var synonim in item.list)
                            {
                                if (item.word == result[0])
                                    try
                                    {
                                        synonim.list.Add(learnDict[result[0]]);
                                    }
                                    catch { }
                                else if (item.word == result[1])
                                    try
                                    {
                                        synonim.list.Add(learnDict[result[1]]);
                                    }
                                    catch { }
                            }
                        Word.Write();
                    }
                    else if (dialresult == DialogResult.No)
                    {
                        return "let's learn it";
                    }
                }
                else
                {
                    return "let's learn it";
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
                foreach (var values in Word.Items.Values)
                    foreach (var synonim in values.list)
                        foreach (var item in synonim.list)
                            if (x == item)
                            {
                                count++;
                                switch (synonim.type)
                                {
                                    case "action":
                                        result[0] = values.word;
                                        break;
                                    case "target":
                                        result[1] = values.word;
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

        public string LearnTwoWordCommand(string Text)
        {
            //обучение новой строки при явному указании команды во вкладке обучение
            //работает только для строки из двух слов: action & target или наоборот !!
            List<string> temp = new List<string>() { "", "" };
            List<string> wordssimilarity = new List<string>();
            List<string> words = Text.ToLower().Split(' ').ToList();
            foreach (var item in words)
            {
                double counter0 = 0;
                double counter1 = 0;
                foreach (var meanitem in Word.Items.Values)
                {
                    if (meanitem.word == result[0])
                    {
                        foreach (var synonims in meanitem.list)
                            foreach (var synonim in synonims.list)
                                counter0 += item.ToLower().CalculateSimilarity(synonim.ToLower());
                    }
                    if (meanitem.word == result[1])
                    {
                        foreach (var synonims in meanitem.list)
                            foreach (var synonim in synonims.list)
                                counter1 += item.ToLower().CalculateSimilarity(synonim.ToLower());
                    }
                }
                wordssimilarity.Add(counter0 > counter1 ? "similarToRes0" : "similarToRes1");
            }

            if (wordssimilarity[0] == "similarToRes0")
            {
                temp[0] = words[0];
                temp[1] = words[1];
            }
            else
            {
                temp[1] = words[0];
                temp[0] = words[1];
            }


            foreach (var item in Word.Items.Values)
                foreach (var synonim in item.list)
                {
                    if (item.word == result[0])
                        synonim.list.Add(temp[0]);
                    else if (item.word == result[1])
                        synonim.list.Add(temp[1]);
                }

            return string.Join(" ", result);
        }
    }
}
