using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
// -- /с//з/есампри/!з//л/самрпир/п/ртол/!п//п/мпио/!п//!л//!с/
namespace WpfApplication1
{
    class Document
    {
        List<Text> _formatDocument;
        string _text;
        List<Tegs> _tegs;

        public Document(string text)
        {
            _formatDocument = new List<Text>();
            _text = text;
            Tegs = new List<Tegs>();
        }

        public void Format(int width)
        {
            StreamWriter file = null;
            try
            {
                if (CheckEnter())
                {
                    file = new StreamWriter("Text.txt", false);
                    List<string> result = Show(width);
                    for (int i = 0; i < result.Count; i++)
                    {
                        file.WriteLine(result[i]);
                    }
                    file.Close();
                }
                else
	            {
                    MessageBox.Show("Пропущен тэг! Проверьте правильность ввода", "Проверка синтаксиса", MessageBoxButton.OK);
                }
            }
            catch (IOException)
            {
                Error.EnterError();
            }
            finally
            {
                if (file != null)
                {
                    file.Close();
                }
            }
        }

        void Parse()
        {
            SomeNeedOverWrite.SortList(ref _tegs);
            if (Tegs.Count == 0)
            {
                Text text = new Text();
                text.Content = _text;
                _formatDocument.Add(text);
            }
            else
            {
                if (Tegs[0].Position != 0)
                {
                    Text text = new Text();
                    text.Content = SomeNeedOverWrite.CopyStrToStr(_text, 0, Tegs[0].Position);
                    text.Content = FormattingText.DeleteSpace(text.Content);
                    if (text.Content.Length != 0)
                    {
                        _formatDocument.Add(text);
                    }
                }
                for (int i = 0; i < Tegs.Count; i++)
                {
                    switch (Tegs[i].TegType)
                    {
                        case "/с/":
                            {
                                Section section = new Section();
                                int j = EndTeg(i, "с/");
                                section.Content = SomeNeedOverWrite.CopyStrToStr(_text, Tegs[i].Position + 3, Tegs[j].Position);
                                section.Tegs = SomeNeedOverWrite.CopyListToList(Tegs, i + 1, j);
                                _formatDocument.Add(section);
                                i = j;
                                break;
                            }
                        case "/к/":
                            {
                                Columns column = new Columns();
                                int j = EndTeg(i, "к/");
                                column.Content = SomeNeedOverWrite.CopyStrToStr(_text, Tegs[i].Position + 3, Tegs[j].Position);
                                column.Tegs = SomeNeedOverWrite.CopyListToList(Tegs, i + 1, j);
                                _formatDocument.Add(column);
                                i = j;
                                break;
                            }
                        case "/з/":
                            {
                                Title title = new Title();
                                int j = EndTeg(i, "з/");
                                title.TitleTx = SomeNeedOverWrite.CopyStrToStr(_text, Tegs[i].Position + 3, Tegs[j].Position);
                                _formatDocument.Add(title);
                                i = j;
                                break;
                            }
                        case "/л/":
                            {
                                MarkerList mrList = new MarkerList();
                                int j = EndTeg(i, "л/");
                                mrList.Content = SomeNeedOverWrite.CopyStrToStr(_text, Tegs[i].Position + 3, Tegs[j].Position);
                                mrList.Tegs = SomeNeedOverWrite.CopyListToList(Tegs, i + 1, j);
                                _formatDocument.Add(mrList);
                                i = j;
                                break;
                            }
                    }
                    if (i < Tegs.Count - 2)
                    {
                        if (i < Tegs.Count - 1)
                        {
                            if ((Tegs[i + 1].Position - Tegs[i].Position + 4 > 3) && FormattingText.DeleteSpace(SomeNeedOverWrite.CopyStrToStr(_text, Tegs[i].Position + 4, Tegs[i + 1].Position)) != "")
                            {
                                Text text = new Text();
                                text.Content = SomeNeedOverWrite.CopyStrToStr(_text, Tegs[i].Position + 4, Tegs[i + 1].Position);
                                text.Content = FormattingText.DeleteSpace(text.Content);
                                _formatDocument.Add(text);
                            }
                        }
                    }
                    if ((i == Tegs.Count - 1) && (Tegs[i].Position + 3 < _text.Length - 1))
                    {
                        Text text = new Text();
                        text.Content = SomeNeedOverWrite.CopyStrToStr(_text, Tegs[i].Position + 4, _text.Length);
                        text.Content = FormattingText.DeleteSpace(text.Content);
                        if (text.Content.Length != 0)
                        {
                            _formatDocument.Add(text);
                        }
                    }
                }
            }
        }

        List<string> Show(int width)
        {
            Parse();
            List<string> formatText = new List<string>();
            List<string> textFragment = new List<string>();
            for (int i = 0; i < _formatDocument.Count; i++)
            {
                bool check = (_formatDocument[i].GetType().ToString() == "WpfApplication1.Columns");
                if (check)
                {
                    if ((i == _formatDocument.Count - 1) || (_formatDocument[i + 1].GetType().ToString() != "WpfApplication1.Columns"))
                    {
                        textFragment = _formatDocument[i].Show(width - 7);
                        for (int j = 0; j < textFragment.Count; j++)
                        {
                            formatText.Add("    " + textFragment[j]);
                        }
                    }
                    else
                    {
                        textFragment = FormattingText.Show(_formatDocument[i].Show(width/2 - 5), _formatDocument[i + 1].Show(width/2 - 5));
                        for (int j = 0; j < textFragment.Count; j++)
                        {
                            formatText.Add("    " + textFragment[j]);
                        }
                    }
                }
                else
                {
                    textFragment = _formatDocument[i].Show(width);
                    for (int j = 0; j < textFragment.Count; j++)
                    {
                        formatText.Add(textFragment[j]);
                    }
                }
            }
            return formatText;
        }

        int EndTeg(int beginPos ,string teg)
        {
            int countRepeat = 1;
            int i = beginPos + 1;
            while (countRepeat != 0)
            {
                if ("/" + teg == Tegs[i].TegType)
                {
                    countRepeat++;
                }
                if ("/!" + teg == Tegs[i].TegType)
                {
                    countRepeat--;
                }
                i++;
            }
            return i - 1;
        }

        bool CheckEnter()
        {
            bool check = true;
            for (int i = 1; i <= 5; i++)
            {
                switch (i)
                {
                    case 1:
                        {
                            check = FindTeg("с/");
                            break;
                        }
                    case 2:
                        {
                            check = FindTeg("з/");
                            break;
                        }
                    case 3:
                        {
                            check = FindTeg("к/");
                            break;
                        }
                    case 4:
                        {
                            check = FindTeg("л/");
                            break;
                        }
                    case 5:
                        {
                            check = FindTeg("п/");
                            break;
                        }
                }
                if (!check)
                {
                    return false;
                }
            }
            return check;
        }

        bool FindTeg(string type)
        {
            int left = 0;
            int right = _text.Length;
            int checkTegs = 0;
            while ((left < _text.Length) && (right > 1))
            {
                int begin = _text.IndexOf("/" + type, left);
                int end = _text.LastIndexOf("/!" + type, right);
                if (begin == -1 && end == -1)
                {
                    left = _text.Length;
                    right = 0;
                }
                else
                {
                    if (begin != -1)
                    {
                        Tegs.Add(new Tegs(begin, "/" + type));
                        left = begin + 3;
                        checkTegs++;
                    }
                    else
                    {
                        left++;
                    }
                    if (end != -1)
                    {
                        Tegs.Add(new Tegs(end, "/!" + type));
                        right = end - 1;
                        checkTegs--;
                    }
                    else
                    {
                        end--;
                    }
                }
            }
            if (checkTegs != 0)
            {
                return false;
            }
            return true;
        }

        List<Tegs> Tegs
        {
            get { return _tegs; }
            set { _tegs = value; }
        }
    }
}