using System.ComponentModel.DataAnnotations;

namespace Sapphire.Core.Config
{
    /// <summary>
    /// 水印类型。
    /// </summary>
    public enum WatermarkType
    {
        /// <summary>
        /// 文字水印。
        /// </summary>
        [Display(Name = "文字水印")]
        TextWatermark = 0,

        /// <summary>
        /// 图片水印。
        /// </summary>
        [Display(Name = "图片水印")]
        PhotoWatermark = 1
    }
}
