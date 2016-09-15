using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1
{
    class Title : Text
    {
        string _titleTx;

        public Title()
        {
            TitleTx = "";
        }

        public override string Show(int width)
        {
            string formatStr = "";
            string edit = "   ";
            TitleTx = edit + TitleTx;
            if (TitleTx.Length <= width)
            {
                formatStr = TitleTx + "\n";
            }
            else
            {
                FormatStr(TitleTx, width);
            }
            return formatStr;
        }

        public string TitleTx
        {
            get
            {
                return _titleTx;
            }
            set
            {
                _titleTx = value;
            }
        }
    }
}