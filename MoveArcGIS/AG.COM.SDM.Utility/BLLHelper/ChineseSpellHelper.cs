using Microsoft.International.Converters.PinYinConverter;
using System.Collections.Generic;

namespace AG.COM.SDM.Utility
{
    /// <summary>
    /// 中文拼音帮助类
    /// </summary>
    public class ChineseSpellHelper
    {
        /// <summary>
        /// 由于ChineseChar.IsValidChar这个判断是否汉字的方法非常慢，因此保存起每个字符的是否汉字属性
        /// </summary>
        public static Dictionary<char, bool> m_CharIsChar = new Dictionary<char, bool>();

        /// <summary>
        /// 获取中文字符的第一个拼音
        /// </summary>
        /// <param name="word"></param>
        /// <param name="CnAddSelf"></param>
        /// <returns></returns>
        public static List<List<string>> GetCnFirstSpell(string word, bool CnAddSelf)
        {
            List<List<string>> result = new List<List<string>>();

            char[] charWords = word.ToCharArray();
            foreach (char charWord in charWords)
            {
                //判断字符是否为汉字后保存起来
                bool isChar = false;
                if (m_CharIsChar.ContainsKey(charWord) == true)
                {
                    isChar = m_CharIsChar[charWord];
                }
                else
                {
                    isChar = ChineseChar.IsValidChar(charWord);
                    m_CharIsChar.Add(charWord, isChar);
                }

                //如果是一个汉字
                if (isChar == true)
                {
                    List<string> tem = new List<string>();
                    ChineseChar tChineseChar = new ChineseChar(charWord);
                    foreach (string pinyin in tChineseChar.Pinyins)
                    {
                        if (!string.IsNullOrEmpty(pinyin))
                        {
                            string first = pinyin.Substring(0, 1);
                            if (tem.Contains(first) == false)
                            {
                                tem.Add(first);
                            }
                        }
                    }
                    if (CnAddSelf == true)
                    {
                        tem.Add(charWord.ToString());
                    }


                    result.Add(tem);
                }
                else
                {
                    List<string> tem = new List<string>();
                    tem.Add(charWord.ToString().ToUpper());
                    result.Add(tem);
                }
            }

            return result;
        }

        /// <summary>
        /// 带拼音首字母查询
        /// </summary>
        /// <param name="queryValue">查询字符</param>
        /// <param name="tAllValues">所有供查询的字符</param>
        /// <param name="tPinYins">存放所有供查询的拼音首字母的数组</param>
        /// <returns></returns>
        public static List<string> PinYinQuery(string queryValue, List<string> tAllValues, ref Dictionary<string, List<List<string>>> tPinYins)
        {
            List<string> result = new List<string>();

            if (tPinYins == null)
            {
                tPinYins = new Dictionary<string, List<List<string>>>();
            }

            foreach (string value in tAllValues)
            {
                string strLetter = string.Empty;
                if (tPinYins.ContainsKey(value) == false)
                {
                    tPinYins.Add(value, ChineseSpellHelper.GetCnFirstSpell(value, true));
                }

                List<List<string>> cnFirstSpell = tPinYins[value];

                bool bApply = false;

                string firstCompareString = queryValue.Substring(0, 1).ToUpper();

                //从首字开始模糊匹配使用下面这段
                if (CompareSubString(cnFirstSpell, 0, queryValue) == true)
                {
                    bApply = true;                
                }

                //从任意字母开始模糊匹配使用下面这段
                //for (int k = 0; k < cnFirstSpell.Count; k++)
                //{
                //    //if (cnFirstSpell[k].Contains(firstCompareString) == true)
                //    //{
                //        if (CompareSubString(cnFirstSpell, k, queryValue) == true)
                //        {
                //            bApply = true;
                //            break;
                //        }
                //    //}
                //}

                if (bApply)
                {
                    result.Add(value);
                }
            }
            if(result.Count <=0)
            {
                result.Add("无");
            }

            return result;
        }

        /// <summary>
        /// 匹配子串
        /// </summary>
        /// <param name="cnFirstSpell"></param>
        /// <param name="compareIndex"></param>
        /// <param name="compareString"></param>
        /// <returns></returns>
        private static bool CompareSubString(List<List<string>> cnFirstSpell, int compareIndex, string compareString)
        {
            if (compareString.Length > cnFirstSpell.Count - compareIndex) return false;

            for (int n = 0; n < compareString.Length; n++)
            {
                if (cnFirstSpell[compareIndex + n].Contains(compareString[n].ToString().ToUpper()) == false)
                {
                    return false;
                }
            }

            return true;
        }
    }

    public class CnFirstSpellTag
    {
        public string Value
        {
            get;
            set;
        }

        public List<List<string>> CnFirstSpell
        {
            get;
            set;
        }
    }
}
