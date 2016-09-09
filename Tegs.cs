using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1
{
    class Tegs
    {
        private int _position;
        private string _tegType;
            //section = 1,
            //title = 2,
            //column = 3,
            //markList = 4

        public Tegs(int position, string type)
        {
            _position = position;
            _tegType = type;
        }

        public int Position
        {
            get
            {
                return _position;
            }
            set
            {
                if (value >= 0)
                {
                    _position = value;
                }
            }
        }

        public string TegType
        {
            get
            {
                return _tegType;
            }
            set
            {
                _tegType = value;
            }
        }
    }
}
