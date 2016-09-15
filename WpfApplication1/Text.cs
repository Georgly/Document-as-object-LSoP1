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
            string strOut = "";
            int i = 0;
            while (i < strIn.Length)
            {
                //string temptStr = "";
                while (i < width)
                {
                    strOut += strIn[i];
                    i++;
                }
                if (strOut[strIn.Length - 1].ToString() == " ")
                {
                    _formatText.Add(strOut);
                }
                else
                {
                    int tempt = strOut.Length - 1;
                    while (strOut[tempt] != ' ')
                    {
                        tempt--;
                    }
                    _formatText.Add(SomeNeedOverWrite.CopyStrToStr(strOut, 0, tempt));
                    //for (int j = 0; j < tempt; j++)
                    //{
                    //    strOut += temptStr[j];
                    //}
                    i = tempt;
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