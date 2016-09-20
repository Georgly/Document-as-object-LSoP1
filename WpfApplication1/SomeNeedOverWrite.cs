using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1
{
    class SomeNeedOverWrite
    {
        public static void SortList(ref List<Tegs> list)
        {
            int n = list.Count;
            int d = n / 2;
            while (d > 0)
            {
                bool Ok = true;
                while (Ok)
                {
                    Ok = false;
                    for (int i = 0; i < n - d; i++)
                        if (list[i].Position > list[i + d].Position)
                        {
                            Tegs t = list[i]; list[i] = list[i + d];
                            list[i + d] = t;
                            Ok = true;
                        }
                }
                d = d / 2;
            }
        }

        public static string CopyStrToStr(string strIn, int firstIndex, int lastIndex)
        {
            string strOut = "";

            for (int i = firstIndex; i < lastIndex; i++)
            {
                strOut += strIn[i];
            }
            return strOut;
        }

        public static List<Tegs> CopyListToList(List<Tegs> listIn, int firstIndex, int lastIndex)
        {
            List<Tegs> listOut = new List<Tegs>();
            if (listIn.Count != 0)
            {
                int delta;
                if (firstIndex == 0)
                {
                    delta = 0;
                }
                else if (listIn[firstIndex].Position - listIn[firstIndex - 1].Position == 3)
                {
                    delta = listIn[firstIndex].Position;
                }
                else
                {
                    delta = listIn[firstIndex - 1].Position + 3;
                }
                for (int i = firstIndex; i < lastIndex; i++)
                {
                    listOut.Add(new Tegs(listIn[i].Position - delta, listIn[i].TegType));
                }
            }

            return listOut;
        }

        static public int FindIndex(List<Tegs> list, string teg)
        {
            int index = 0;

            while (list[index].TegType != teg || index != list.Count)
            {
                index++;
            }

            if (index < list.Count)
            {
                return index;
            }
            else
            {
                return -1;
            }
        }
    }
}