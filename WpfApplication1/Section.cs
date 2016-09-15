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
                if (Tegs[0].Position != 0)// TODO TODO исправить добавление пробела
                {
                    Text text = new Text();
                    text.Content = SomeNeedOverWrite.CopyStrToStr(Content, 0, Tegs[0].Position);
                    SectionContent.Add(text);
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
                                title.TitleTx = SomeNeedOverWrite.CopyStrToStr(Content, Tegs[i].Position + 3, Tegs[j].Position);// TODO не +3 а +2 везде
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
                }
            }
            else
            {
                Text text = new Text();
                text.Content = Content;
                SectionContent.Add(text);
            }
            
        }

        public override string Show(int width)
        {
            Parse();
            string formatStr = "";
            for (int i = 0; i < SectionContent.Count; i++)
            {
                formatStr += SectionContent[i].Show(width);
            }
            formatStr += "\n\n";
            return formatStr;
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