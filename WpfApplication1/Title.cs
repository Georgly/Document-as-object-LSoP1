﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1
{
    class Title : Text
    {
        private string _titleTx;
        private List<string> _formatText;

        public Title()
        {
            TitleTx = "";
            _formatText = new List<string>();
        }

        public override List<string> Show(int width)
        {
            TitleTx = FormattingText.DeleteSpace(TitleTx);
            string edit = FormattingText.Margin(7, width);
            TitleTx = edit + TitleTx.ToUpper();
            if (TitleTx.Length <= width)
            {
                _formatText.Add(FormattingText.EndSpace(TitleTx, width));
            }
            else
            {
                _formatText = FormatStr(TitleTx, width);
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