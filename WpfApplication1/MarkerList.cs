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
        List<string> _formatText;
        string _content;
        List<Tegs> _tegs;

        public MarkerList()
        {
            Content = "";
            Tegs = new List<Tegs>();
            FormatText = new List<string>();
        }

        void Parse()
        {
            if (Tegs.Count != 0)
            {
                if (Tegs[0].Position != 0)
                {
                    FormatText.Add(SomeNeedOverWrite.CopyStrToStr(Content, 0, Tegs[0].Position));
                }
                for (int i = 0; i < Tegs.Count; i++)
                {
                    string listItem = i + "." + " " + SomeNeedOverWrite.CopyStrToStr(Content, Tegs[i].Position + 3, Tegs[i + 1].Position);
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
            List<string> list = FormatText;
            for (int i = 0; i < FormatText.Count; i++)
            {
                if (list[i].Length < width) // TODO сделать отступы и добавление пробелв до длины
                {
                    list[i] = FormatText[i];
                }
                else
                {
                    /*list[i] = */FormatStr(FormatText[i], width, ref list);
                }
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
                //string temptStr = "";
                if (i == 0)
                {
                    space = " ";
                }
                else
                {
                    space = "    ";
                }
                while (count < width - space.Length && i < strIn.Length)
                {
                    strOut += strIn[i];
                    i++;
                    count++;
                }
                strOut = space + strOut;
                if (strOut[strOut.Length - 1].ToString() == " " || i == strOut.Length - 1)
                {
                    list.Add(space + strOut);
                }
                else
                {
                    int tempt = strOut.Length - 1;
                    count = 0;
                    while (strOut[tempt] != ' ')
                    {
                        tempt--;
                        count++;
                    }
                    list.Add(SomeNeedOverWrite.CopyStrToStr(strOut, 0, tempt));
                    //for (int j = 0; j < tempt; j++)
                    //{
                    //    strOut += temptStr[j];
                    //}
                    i -= count;
                }
                i++;
            }
            //return _formatText;
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

        List<string> FormatText
        {
            get { return _formatText; }
            set { _formatText = value; }
        }
    }
}