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
        private List<string> _formatText;
        private string _content;
        private List<Tegs> _tegs;

        public MarkerList()
        {
            Content = "";
            Tegs = new List<Tegs>();
            FormatText = new List<string>();
        }

        private void Parse()
        {
            if (Tegs.Count != 0)
            {
                if (Tegs[0].Position != 0)
                {
                    FormatText.Add(SomeNeedOverWrite.CopyStrToStr(Content, 0, Tegs[0].Position));
                }
                for (int i = 0, j = 1; i < Tegs.Count; i++, j++)
                {
                    string listItem = j + "." + " " + SomeNeedOverWrite.CopyStrToStr(Content, Tegs[i].Position + 3, Tegs[i + 1].Position);
                    FormatText.Add(listItem);
                    i++;
                }
            }
            else
            {
                FormatText.Add(Content);
            }
        }

        public override List<string> Show(int width)
        {
            Parse();
            List<string> list = new List<string>();
            for (int i = 0; i < FormatText.Count; i++)
            {
                FormatText[i] = FormattingText.DeleteSpace(FormatText[i]);
                FormatStr(FormatText[i], width, ref list);
            }
            return list;
        }

        public void FormatStr(string strIn, int width, ref List<string> list)
        {
            string strOut = "";
            int i = 0;
            string space;
            while (i < strIn.Length)
            {
                int count = 0;
                if (list.Count == 0)
                {
                    space = " ";
                }
                else
                {
                    space = "   ";
                }
                while (count < width - space.Length && i < strIn.Length)
                {
                    strOut += strIn[i];
                    i++;
                    count++;
                }
                i--;
                if (strOut[strOut.Length - 1].ToString() == " " || i == strIn.Length - 1)
                {
                    strOut = FormattingText.DeleteSpace(strOut);
                    list.Add(FormattingText.EndSpace(space + strOut, width));
                }
                else
                {
                    strOut = FormattingText.DeleteSpace(strOut);
                    int tempt = strOut.Length - 1;
                    count = 0;
                    while (strOut[tempt] != ' ' && tempt > 0)
                    {
                        tempt--;
                        count++;
                    }
                    if (tempt > 0)
                    {
                        list.Add(FormattingText.EndSpace(space + SomeNeedOverWrite.CopyStrToStr(strOut, 0, tempt), width));
                        i -= count;
                    }
                    else
                    {
                        strOut = Hyphenation.MakeHyphenation(strOut);
                        list.Add(FormattingText.EndSpace(strOut, width));
                        i -= count - (strOut.Length - 2);
                    }
                }
                i++;
            }
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

        private List<string> FormatText
        {
            get { return _formatText; }
            set { _formatText = value; }
        }
    }
}