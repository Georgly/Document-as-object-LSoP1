using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApplication1
{
    class Section : Text
    {
        List<Text> _sectionContent;
        string _content;
        List<Tegs> _tegs;

        public Section()
        {
            Content = "";
            Tegs = new List<Tegs>();
            SectionContent = new List<Text>();
        }

        void Parse()
        {
            if (Tegs.Count != 0)
            {
                if (Tegs[0].Position != 0)
                {
                    Text text = new Text();
                    text.Content = SomeNeedOverWrite.CopyStrToStr(Content, 0, Tegs[0].Position);
                    text.Content = FormattingText.DeleteSpace(text.Content);
                    if (text.Content.Length != 0)
                    {
                        SectionContent.Add(text);
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
                                section.Content = SomeNeedOverWrite.CopyStrToStr(Content, Tegs[i].Position + 3, Tegs[j].Position);
                                section.Tegs = SomeNeedOverWrite.CopyListToList(Tegs, i + 1, j);
                                SectionContent.Add(section);
                                i = j;
                                break;
                            }
                        case "/к/":
                            {
                                Columns column = new Columns();
                                int j = EndTeg(i, "к/");
                                column.Content = SomeNeedOverWrite.CopyStrToStr(Content, Tegs[i].Position + 3, Tegs[j].Position);
                                column.Tegs = SomeNeedOverWrite.CopyListToList(Tegs, i + 1, j);
                                SectionContent.Add(column);
                                i = j;
                                break;
                            }
                        case "/з/":
                            {
                                Title title = new Title();
                                int j = EndTeg(i, "з/");
                                title.TitleTx = SomeNeedOverWrite.CopyStrToStr(Content, Tegs[i].Position + 3, Tegs[j].Position);
                                SectionContent.Add(title);
                                i = j;
                                break;
                            }
                        case "/л/":
                            {
                                MarkerList mrList = new MarkerList();
                                int j = EndTeg(i, "л/");
                                mrList.Content = SomeNeedOverWrite.CopyStrToStr(Content, Tegs[i].Position + 3, Tegs[j].Position);
                                mrList.Tegs = SomeNeedOverWrite.CopyListToList(Tegs, i + 1, j);
                                SectionContent.Add(mrList);
                                i = j;
                                break;
                            }
                    }
                    if (i < Tegs.Count - 2)
                    {
                        if (i < Tegs.Count - 1)
                        {
                            if ((Tegs[i + 1].Position - Tegs[i].Position + 4 > 3) && FormattingText.DeleteSpace(SomeNeedOverWrite.CopyStrToStr(Content, Tegs[i].Position + 4, Tegs[i + 1].Position)) != "")
                            {
                                Text text = new Text();
                                text.Content = SomeNeedOverWrite.CopyStrToStr(Content, Tegs[i].Position + 4, Tegs[i + 1].Position);
                                text.Content = FormattingText.DeleteSpace(text.Content);
                                SectionContent.Add(text);
                            }
                        }
                    }
                    if ((i == Tegs.Count - 1) && (Tegs[i].Position + 3 < Content.Length - 1))
                    {
                        Text text = new Text();
                        text.Content = SomeNeedOverWrite.CopyStrToStr(Content, Tegs[i].Position + 4, Content.Length);
                        text.Content = FormattingText.DeleteSpace(text.Content);
                        if (text.Content.Length != 0)
                        {
                            SectionContent.Add(text);
                        }
                    }
                }
            }
            else
            {
                Text text = new Text();
                text.Content = Content;
                SectionContent.Add(text);
            }
            
        }

        public override List<string> Show(int width)
        {
            Parse();
            List<string> formatText = new List<string>();
            List<string> textFragment;
            for (int i = 0; i < SectionContent.Count; i++)
            {
                bool check = (SectionContent[i].GetType().ToString() == "WpfApplication1.Columns");
                if (check)
                {
                    if ((i == SectionContent.Count - 1) || (SectionContent[i + 1].GetType().ToString() != "WpfApplication1.Columns"))
                    {
                        textFragment = SectionContent[i].Show(width - 7);
                        for (int j = 0; j < textFragment.Count; j++)
                        {
                            formatText.Add("    " + textFragment[j]);
                        }
                    }
                    else
                    {
                        textFragment = FormattingText.Show(SectionContent[i].Show(width/2 - 5), SectionContent[i + 1].Show(width/2 - 5));
                        for (int j = 0; j < textFragment.Count; j++)
                        {
                            formatText.Add("    " + textFragment[j]);
                        }
                        i++;
                    }
                }
                else
                {
                    textFragment = SectionContent[i].Show(width - 1);
                    for (int j = 0; j < textFragment.Count; j++)
                    {
                        formatText.Add(" " + textFragment[j]);
                    }
                }
            }
            formatText.Add("");
            return formatText;
        }

        public int EndTeg(int beginPos, string teg)
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

        public string Content
        {
            get
            {
                return _content;
            }
            set
            {
                _content = value;
            }
        }

        public List<Tegs> Tegs
        {
            get { return _tegs; }
            set { _tegs = value; }
        }

        List<Text> SectionContent
        {
            get { return _sectionContent; }
            set { _sectionContent = value; }
        }
    }
}