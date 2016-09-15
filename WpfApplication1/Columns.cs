using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1
{
    class Columns : Text //have to think some houres
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
            if (Tegs[0].Position != 0)
            {
                Text text = new Text();
                text.Content = SomeNeedOverWrite.CopyStrToStr(base.Content, 0, Tegs[0].Position);
                ColumnContent.Add(text);
            }
            int i = 0;
            int j = SomeNeedOverWrite.FindIndex(Tegs, "/0/");
            if (j != -1)
            {
                if (Tegs[0].Position != 0)
                {
                    Text text = new Text();
                    text.Content = SomeNeedOverWrite.CopyStrToStr(base.Content, 0, Tegs[0].Position);
                    ColumnContent.Add(text);
                }
                for (i = 0; i <= j; i++)
                {

                }
            }
        }

        //public override string Show(int width)
        //{
        //    return "0";
        //}

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