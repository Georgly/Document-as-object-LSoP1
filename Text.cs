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
        List<Text> _formatDocument = new List<Text>();
        public static string _text;
        static List<Tegs> tegs = new List<Tegs>();

        void Parser()
        {
            
        }

        public static void CheckEnter()
        {
            for (int i = 1; i <= 4; i++)
            {
                bool check = true;
                switch (i)
                {
                    case 1:
                        {
                            check = FindTeg("с/");
                            break;
                        }
                    case 2:
                        {
                            check = FindTeg("з/");
                            break;
                        }
                    case 3:
                        {
                            check = FindTeg("к/");
                            break;
                        }
                    case 4:
                        {
                            check = FindTeg("л/");
                            break;
                        }
                    case 5:
                        {
                            check = FindTeg("п/");
                            break;
                        }
                }
                if (!check)
                {
                    return;
                }
            }
        }

        static bool FindTeg(string type)
        {
            int left = 0;
            int right = _text.Length;
            while ((left < _text.Length) && (right > 1))
            {
                int begin = _text.IndexOf("/" + type, left);
                int end = _text.LastIndexOf("/!" + type, right);
                if (begin == -1 && end == -1)
                {
                    left = _text.Length;
                    right = 0;
                }
                else
                {
                    if (begin != -1)
                    {
                        tegs.Add(new Tegs(begin, "/" + type));
                        left = begin + 3;
                    }
                    else
                    {
                        left++;
                    }
                    if (end != -1)
                    {
                        tegs.Add(new Tegs(end, "/!" + type));
                        right = end - 1;
                    }
                    else
                    {
                        end--;
                    }
                }
            }
            if (tegs.Count % 2 != 0)
            {
                MessageBox.Show("Пропущен тэг! Проверьте правильность ввода", "Проверка синтаксиса", MessageBoxButton.OK);
                return false;
            }
            return true;
        }
    }
}
