using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Forms;
using KharGo;

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

                result = TryToRecognize(temp);
                if (temp != result)
                {

                    string message = $"Did your mean {result[0]} {result[1]}?";
                    string caption = "Error Detected in Input";
                    MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                    DialogResult dialresult;

                    // Displays the MessageBox.

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

                    }
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
                {
                    foreach (var synonim in item.meaning)
                    {

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
