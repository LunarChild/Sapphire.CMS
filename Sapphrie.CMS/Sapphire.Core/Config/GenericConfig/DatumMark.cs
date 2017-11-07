using System.ComponentModel.DataAnnotations;

namespace Sapphire.Core.Config
{
    /// <summary>
    /// 基准点。
    /// </summary>
    public enum DatumMark
    {
        /// <summary>
        /// 左上。
        /// </summary>
        [Display(Name = "左上")]
        UpperLeft = 0,

        /// <summary>
        /// 右下。
        /// </summary>
        [Display(Name = "右下")]
        LowerRight = 1,

        /// <summary>
        /// 中间。
        /// </summary>
        [Display(Name = "中间")]
        Middle = 2
    }
}
