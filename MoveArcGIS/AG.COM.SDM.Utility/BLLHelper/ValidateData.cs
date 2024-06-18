using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace AG.COM.SDM.Utility
{
    /// <summary>
    /// 数据格式验证类
    /// </summary>
    public class ValidateData
    {
        #region 一般验证

        /// <summary>
        /// 验证不能为空
        /// </summary>
        /// <param name="value"></param>
        /// <param name="fieldname"></param>
        /// <returns></returns>
        public static bool NotNull(string value, string fieldname)
        {
            if (string.IsNullOrEmpty(value))
            {
                System.Windows.Forms.MessageBox.Show("【" + fieldname + "】不能为空", "输入格式错误");

                return false;
            }

            return true;
        }

        /// <summary>
        /// 验证string的最大长度
        /// </summary>
        /// <param name="value">验证的值</param>
        /// <param name="MaxLength">最大长度</param>
        /// <param name="fieldname">字段名称</param>
        /// <returns></returns>
        public static bool StringLength(string value, int maxLength, string fieldname)
        {
            if (value != null && value.Length > maxLength)
            {
                System.Windows.Forms.MessageBox.Show("【" + fieldname + "】长度不能大于" + maxLength.ToString(), "输入格式错误");

                return false;
            }

            return true;
        }

        /// <summary>
        /// 验证string的最大长度（varchar2类型字段）
        /// </summary>
        /// <param name="value">验证的值</param>
        /// <param name="maxLength">最大长度</param>
        /// <param name="fieldname">字段名称</param>
        /// <returns></returns>
        public static bool StringLengthVarchar2(string value, int maxLength, string fieldname)
        {
            if (value != null && Encoding.Default.GetByteCount(value) > maxLength)
            {
                System.Windows.Forms.MessageBox.Show("【" + fieldname + "】长度不能大于" + maxLength.ToString() + "个字节", "输入格式错误");

                return false;
            }

            return true;
        }

        /// <summary>
        /// 验证是否数值类型且验证的整数部分和小数部分长度
        /// </summary>
        /// <param name="value"></param>
        /// <param name="intLength"></param>
        /// <param name="floatLength"></param>
        /// <param name="fieldname"></param>
        /// <returns></returns>
        public static bool NumberLength(string value, int intLength, int floatLength, string fieldname)
        {
            string strRegex = "^-?[0-9]{0," + intLength + "}(\\.[0-9]{0," + floatLength + "})?$";

            if (!Regex.IsMatch(value, strRegex))
            {
                System.Windows.Forms.MessageBox.Show("【" + fieldname + "】整数部分长度不能大于" + intLength.ToString() +
                    "，小数部分长度不能大于" + floatLength.ToString(), "输入格式错误");

                return false;
            }

            return true;
        }

        /// <summary>
        /// 验证是否数字（数字长度，正负，是否小数不管，为数字即可）
        /// </summary>
        /// <param name="value"></param>
        /// <param name="fieldname"></param>
        /// <returns></returns>
        public static bool IsNumber(string value, string fieldname)
        {
            decimal valueDec = 0;
            if (decimal.TryParse(value, out valueDec) == true)
            {
                return true;
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("【" + fieldname + "】只能输入数字", "输入格式错误");

                return false;
            }
        }

        /// <summary>
        /// 验证不能为空（ComboBox控件）
        /// </summary>
        /// <param name="value">ComboBox</param>
        /// <param name="fieldname">字段名称</param>
        /// <returns></returns>
        public static bool NotNullComboBox(ComboBox value, string fieldname)
        {
            if (value.SelectedIndex < 0)
            {
                System.Windows.Forms.MessageBox.Show("【" + fieldname + "】不能为空", "输入格式错误");

                return false;
            }

            return true;
        }

        #endregion

        #region  PropertyGrid控件中验证

        /// <summary>
        /// 验证string的最大长度
        /// </summary>
        /// <param name="value">验证的值</param>
        /// <param name="MaxLength">最大长度</param>
        /// <returns></returns>
        public static bool PGValidateStringLength(string value, int maxLength)
        {
            if (value != null && value.Length > maxLength)
            {
                System.Windows.Forms.MessageBox.Show("长度不能大于" + maxLength.ToString(), "输入格式错误");

                return false;
            }

            return true;
        }

        /// <summary>
        /// 验证decimal?的整数部分和小数部分长度
        /// </summary>
        /// <param name="value"></param>
        /// <param name="intLength"></param>
        /// <param name="floatLength"></param>
        /// <returns></returns>
        public static bool PGValidateDecimalNullLength(decimal? value, int intLength, int floatLength)
        {
            string strRegex = "^[0-9]{0," + intLength + "}(\\.[0-9]{0," + floatLength + "})?$";

            ///因为正则表达式只能验证字符串，因此这里要先转为字符串
            string strValue = DataConvert.DecNullToString(value);

            if (!Regex.IsMatch(strValue, strRegex))
            {
                System.Windows.Forms.MessageBox.Show("整数部分长度不能大于" + intLength.ToString() +
                    "，小数部分长度不能大于" + floatLength.ToString(), "输入格式错误");

                return false;
            }

            return true;
        }

        #endregion
    }
}
