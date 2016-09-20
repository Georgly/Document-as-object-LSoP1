using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1
{
    class FormattingText
    {
        public static List<string> Show(List<string> firstColumn, List<string> secondColumn)
        {
            List<string> columns = new List<string>();
            if (firstColumn.Count == secondColumn.Count)
            {
                for (int i = 0; i < firstColumn.Count; i++)
                {
                    columns.Add(firstColumn[i] + "   " + secondColumn[i]);
                }
            }
            else if (firstColumn.Count < secondColumn.Count)
            {
                int i = 0;
                int length = firstColumn[0].Length;
                for (i = 0; i < firstColumn.Count - 1; i++)
                {
                    columns.Add(firstColumn[i] + "   " + secondColumn[i]);
                }
                for (int j = i; j < secondColumn.Count; j++)
                {
                    columns.Add("    " + EndSpace(" ", secondColumn[j].Length/2) + "   " + secondColumn[j]);
                }
            }
            else
            {
                int i = 0;
                int length = secondColumn[0].Length;
                for (i = 0; i < secondColumn.Count; i++)
                {
                    columns.Add(firstColumn[i] + "   " + secondColumn[i]);
                }
                for (int j = i; j < firstColumn.Count; j++)
                {
                    columns.Add(firstColumn[j]);
                }
            }
            return columns;
        }

        public static string EndSpace(int spaceNumber)
        {
            string spaces = "";
            for (int i = 0; i < spaceNumber; i++)
            {
                spaces += " ";
            }
            return spaces;
        }

        public static string EndSpace(string strIn, int width)
        {
            int i = strIn.Length - 1;
            string strOut;
            string space = "";
            for (int j = i; j < width; j++)
            {
                space += " ";
            }
            return strOut = strIn + space;
        }

        public static string DeleteSpace(string strIn)
        {
            string strOut = "";
            if (strIn.Length != 0)
            {
                int i = 0;
                while (i < strIn.Length && strIn[i].ToString() == " ")
                {
                    i++;
                }
                int j = strIn.Length - 1;
                while (j > 0 && strIn[j].ToString() == " ")
                {
                    j--;
                }
                for (int k = i; k <= j; k++)
                {
                    strOut += strIn[k];
                }
            }
            return strOut;
        }

        public static string Margin(int delta, int width)//TODO
        {
            // возможно понадобиться делать if(width > 50){ delta *= 2}
            string space = "";
            for (int i = 0; i < width/delta; i++)
            {
                space += " ";
            }
            return space;
        }
    }
}
