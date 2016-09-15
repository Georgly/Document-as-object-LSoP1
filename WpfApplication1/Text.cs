using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApplication1
{
    class Text
    {
        string _content = "";

        public Text()
        {
            Content = "";
        }

        public string FormatStr(string strIn, int width)
        {
            string strOut = "";
            int i = 0;
            while (i < strIn.Length)
            {
                string temptStr = "";
                while (i < width)
                {
                    temptStr += strIn[i];
                    i++;
                }
                if (temptStr[strIn.Length - 1].ToString() == " ")
                {
                    strOut += temptStr + "\n";
                }
                else
                {
                    int tempt = temptStr.Length - 1;
                    while (temptStr[tempt] != ' ')
                    {
                        tempt--;
                    }
                    for (int j = 0; j < tempt; j++)
                    {
                        strOut += temptStr[j];
                    }
                    strOut += "\n";
                    i = tempt;
                }
                i++;
            }
            return strOut;
        }

        public virtual string Show(int width)
        {
            string formatStr = "";
            if (Content.Length <= width)
            {
                formatStr = Content;
            }
            else
            {
                FormatStr(Content, width);
            }
            return formatStr;
        }

        public string Content
        {
            get { return _content; }
            set { _content = value; }
        }
    }
}