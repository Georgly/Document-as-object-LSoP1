using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1
{
    class FormatText
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
                for (i = 0; i < firstColumn.Count; i++)
                {
                    columns.Add(firstColumn[i] + "   " + secondColumn[i]);
                }
                for (int j = i; j < secondColumn.Count; j++)
                {
                    columns.Add(secondColumn[j]);
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

        public static void Margin() //TODO
        { }

        public static string DeleteSpace(string strIn)
        {
            string strOut = "";
            int i = 0;
            while (strIn[i].ToString() == " " && i < strIn.Length)
            {
                i++;
            }
            int j = strIn.Length - 1;
            while (strIn[j].ToString() == " " && j > 0)
            {
                j--;
            }
            for (int k = i; k < j; k++)
            {
                strOut += strIn[k];
            }
            return strOut;
        }
    }
}
