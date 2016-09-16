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
        List<string> _formatText;
        string _content = "";

        public Text()
        {
            Content = "";
            _formatText = new List<string>();
        }

        public virtual List<string> FormatStr(string strIn, int width)
        {
            int i = 0;
            while (i < strIn.Length)
            {
                int count = 0;
                //string temptStr = "";
                string strOut = "";
                while (count < width && i < strIn.Length)
                {
                    strOut += strIn[i];
                    i++;
                    count++;
                }
                if (strOut[strOut.Length - 1].ToString() == " " || i == strOut.Length -1)
                {
                    _formatText.Add(strOut);
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
                    _formatText.Add(SomeNeedOverWrite.CopyStrToStr(strOut, 0, tempt));
                    //for (int j = 0; j < tempt; j++)
                    //{
                    //    strOut += temptStr[j];
                    //}
                    i -= count;
                }
                i++;
            }
            return _formatText;
        }

        public virtual List<string> Show(int width)
        {
            //string formatStr = "";
            if (Content.Length <= width)
            {
                _formatText.Add(Content);
            }
            else
            {
                return FormatStr(Content, width);
            }
            return _formatText;
        }

        public string Content
        {
            get { return _content; }
            set { _content = value; }
        }
    }
}