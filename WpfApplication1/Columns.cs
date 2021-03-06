﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1
{
    class Columns : Text
    {
        List<Text> _columnContent;
        string _content;
        List<Tegs> _tegs;

        public Columns()
        {
            ColumnContent = new List<Text>();
            Content = "";
            Tegs = new List<Tegs>();
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
                        ColumnContent.Add(text);
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
                                ColumnContent.Add(section);
                                i = j;
                                break;
                            }
                        case "/к/":
                            {
                                Columns column = new Columns();
                                int j = EndTeg(i, "к/");
                                column.Content = SomeNeedOverWrite.CopyStrToStr(Content, Tegs[i].Position + 3, Tegs[j].Position);
                                column.Tegs = SomeNeedOverWrite.CopyListToList(Tegs, i + 1, j);
                                ColumnContent.Add(column);
                                i = j;
                                break;
                            }
                        case "/з/":
                            {
                                Title title = new Title();
                                int j = EndTeg(i, "з/");
                                title.TitleTx = SomeNeedOverWrite.CopyStrToStr(Content, Tegs[i].Position + 3, Tegs[j].Position);
                                ColumnContent.Add(title);
                                i = j;
                                break;
                            }
                        case "/л/":
                            {
                                MarkerList mrList = new MarkerList();
                                int j = EndTeg(i, "л/");
                                mrList.Content = SomeNeedOverWrite.CopyStrToStr(Content, Tegs[i].Position + 3, Tegs[j].Position);
                                mrList.Tegs = SomeNeedOverWrite.CopyListToList(Tegs, i + 1, j);
                                ColumnContent.Add(mrList);
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
                                ColumnContent.Add(text);
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
                            ColumnContent.Add(text);
                        }
                    }
                }
            }
            else
            {
                Text text = new Text();
                text.Content = Content;
                ColumnContent.Add(text);
            }
        }

        public override List<string> Show(int width)
        {
            Parse();
            List<string> formatText = new List<string>();
            List<string> textFragment;
            for (int i = 0; i < ColumnContent.Count; i++)
            {
                bool check = ColumnContent[i].GetType().ToString() == "WpfApplication1.Columns";
                if (check)
                {
                    if (i == ColumnContent.Count - 1 || ColumnContent[i + 1].GetType().ToString() != "WpfApplication1.Columns")
                    {
                        textFragment = ColumnContent[i].Show(width - 6);
                        for (int j = 0; j < textFragment.Count; j++)
                        {
                            formatText.Add("    " + textFragment[j]);
                        }
                    }
                    else
                    {
                        textFragment = FormattingText.Show(ColumnContent[i].Show(width - 5), ColumnContent[i + 1].Show(width - 5));
                        for (int j = 0; j < textFragment.Count; j++)
                        {
                            formatText.Add("    " + textFragment[j]);
                        }
                    }
                }
                else
                {
                    textFragment = ColumnContent[i].Show(width - 1);
                    for (int j = 0; j < textFragment.Count; j++)
                    {
                        formatText.Add(textFragment[j]);
                    }
                }
            }
            formatText.Add("");
            return formatText;
        }

        public int EndTeg(int beginPos, string teg)
        {
            int countRepeat = 1;
            int i = beginPos;
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
            return i;
        }

        List<Text> ColumnContent
        {
            get { return _columnContent; }
            set { _columnContent = value; }
        }

        public string Content
        {
            get { return _content; }
            set { _content = value; }
        }

        public List<Tegs> Tegs
        {
            get { return _tegs; }
            set { _tegs = value; }
        }
    }
}