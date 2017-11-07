using System;

namespace Sapphire.Core.CommonHelper
{
    /// <summary>
    /// 字符过滤选项。
    /// </summary>
    [Flags]
    public enum FilterOptions
    {
        /// <summary>
        /// 只保留数字。
        /// </summary>
        HoldNumber = 1,

        /// <summary>
        /// 只保留字母。
        /// </summary>
        HoldLetter = 2,

        /// <summary>
        /// 只保留汉字。
        /// </summary>
        HoldChinese = 4,

        /// <summary>
        /// 全角转半角。
        /// </summary>
        SbcToDbc = 8
    }
}