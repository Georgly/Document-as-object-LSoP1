using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApplication1
{
    class MarkerList : Text
    {
        List<string> _listContent;
        string _content;
        List<Tegs> _tegs;

        public MarkerList()
        {
            Content = "";
            Tegs = new List<Tegs>();
            ListContent = new List<string>();
        }

        void Parse()
        {
            if (Tegs.Count != 0)
            {
                if (Tegs[0].Position != 0)
                {
                    ListContent.Add(SomeNeedOverWrite.CopyStrToStr(Content, 0, Tegs[0].Position));
                }
                for (int i = 0; i < Tegs.Count; i++)
                {
                    string listItem = Convert.ToChar(149) + " " + SomeNeedOverWrite.CopyStrToStr(Content, Tegs[i].Position + 3, Tegs[i + 1].Position);
                    ListContent.Add(listItem);
                    i++;
                }
            }
            else
            {
                ListContent.Add(Content);
            }
        }

        public override string Show(int width)
        {
            Parse();
            string formatStr = "";
            for (int i = 0; i < ListContent.Count; i++)
            {
                if (ListContent[i].Length < width)
                {
                    formatStr += ListContent[i] + "\n";
                }
                else
                {
                    formatStr += FormatStr(ListContent[i], width);
                }
            }
            formatStr += "\n";
            return formatStr;
        }

        //public int EndTeg(string teg)
        //{
        //    int countRepeat = 1;
        //    int i = 1;
        //    while (countRepeat != 0)
        //    {
        //        if ("/" + teg == Tegs[i].TegType)
        //        {
        //            countRepeat++;
        //        }
        //        if ("/!" + teg == Tegs[i].TegType)
        //        {
        //            countRepeat--;
        //        }
        //        i++;
        //    }
        //    return i - 1;
        //}

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

        List<string> ListContent
        {
            get { return _listContent; }
            set { _listContent = value; }
        }
    }
}