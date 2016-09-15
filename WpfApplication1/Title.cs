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
        List<string> _formatText;

        public Title()
        {
            TitleTx = "";
            _formatText = new List<string>();
        }

        public override List<string> Show(int width)
        {
            //string formatStr = "";
            string edit = "    ";
            TitleTx = edit + TitleTx.ToUpper();
            if (TitleTx.Length <= width)
            {
                _formatText.Add(TitleTx);
                _formatText.Add("");
            }
            else
            {
                _formatText = FormatStr(TitleTx, width);
                _formatText.Add("");
            }
            return _formatText;
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