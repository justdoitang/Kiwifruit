using System;
using System.ComponentModel;
using System.Linq;

namespace Kiwifruit.Tools
{
    public static class Util
    {
        /// <summary>
        ///     枚举扩展方法 - 获取枚举值的Description
        /// </summary>
        /// <param name="enumName">需要获取枚举描述的枚举</param>
        /// <returns>描述内容</returns>
        public static string GetDescription(this Enum enumName)
        {
            var fieldInfo = enumName.GetType().GetField(enumName.ToString());
            var attributes = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault() as DescriptionAttribute;
            return attributes?.Description ?? enumName.ToString();
        }
    }
}
