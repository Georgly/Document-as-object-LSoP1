using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1
{
    class Hyphenation
    {
        private static char[] wovel = { 'а', 'е', 'ё', 'и', 'о', 'у', 'ы', 'э', 'ю', 'я'};

        //bool SameLetter(char firstLett, char secondLett)
        //{
        //    if (firstLett == secondLett && !CheckVowel(firstLett))
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        private static bool ConsonantVowel(char letterOne, char letterTwo)
        {
            if (!CheckVowel(letterOne) && CheckVowel(letterTwo))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static bool VowelConsonant(char letterOne, char letterTwo)
        {
            if (CheckVowel(letterOne) && !CheckVowel(letterTwo))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static bool CheckVowel(char letter)
        {
            for (int i = 0; i < wovel.Length; i++)
            {
                if (wovel[i] == letter)
                {
                    return true;
                }
            }
            return false;
        }

        private static bool DoubleConsonant(char letterOne, char letterTwo)
        {
            if (!CheckVowel(letterOne) && !CheckVowel(letterTwo))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static bool DoubleVowel(char letterOne, char letterTwo)
        {
            if (CheckVowel(letterOne) && CheckVowel(letterTwo))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static bool UnLike(char letterOne, char letterTwo)
        {
            if (CheckVowel(letterOne) && letterTwo == 'й')
            {
                return true;
            }
            else if (!CheckVowel(letterOne) && (letterTwo == 'ь' || letterTwo == 'ъ'))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static bool NotLetter(char letter)
        {
            if (CheckVowel(letter) || !CheckVowel(letter))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static string MakeHyphenation(string strIn)//TODO 
        {
            string strOut;
            int strLength = strIn.Length;
            while (strLength > 2)
            {
                if (NotLetter(strIn[strLength - 1]))
                {
                    strOut = SomeNeedOverWrite.CopyStrToStr(strIn, 0, strLength - 1);
                    return strOut;
                }
                if (UnLike(strIn[strLength - 2], strIn[strLength - 1]))
                {
                    strOut = SomeNeedOverWrite.CopyStrToStr(strIn, 0, strLength) + "-";
                    return strOut;
                }
                else if (DoubleVowel(strIn[strLength - 2], strIn[strLength - 1]))
                {
                    strOut = SomeNeedOverWrite.CopyStrToStr(strIn, 0, strLength - 1) + "-";
                    return strOut;
                }
                else if (ConsonantVowel(strIn[strLength - 2], strIn[strLength - 1]))
                {
                    strOut = SomeNeedOverWrite.CopyStrToStr(strIn, 0, strLength /*- 2*/) + "-";
                    return strOut;
                }
                else if(DoubleConsonant(strIn[strLength - 2], strIn[strLength - 1]))
                {
                    strOut = SomeNeedOverWrite.CopyStrToStr(strIn, 0, strLength - 1) + "-";
                    return strOut;
                }
                else if (VowelConsonant(strIn[strLength - 2], strIn[strLength - 1]))
                {
                    strOut = SomeNeedOverWrite.CopyStrToStr(strIn, 0, strLength - 1) + "-";
                    return strOut;
                }
                strLength--;
            }
            return strIn + "-";

        }
    }
}
