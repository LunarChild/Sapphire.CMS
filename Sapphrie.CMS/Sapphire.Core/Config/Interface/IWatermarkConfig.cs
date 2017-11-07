namespace Sapphire.Core.Config
{
    /// <summary>
    /// 水印配置信息接口
    /// </summary>
    public interface IWatermarkConfig
    {
        /// <summary>
        /// 水印类型。
        /// </summary>
        WatermarkType WatermarkType { get; set; }

        /// <summary>
        /// 水印文字。
        /// </summary>
        string WatermarkText { get; set; }

        /// <summary>
        /// 文字字体。
        /// </summary>
        string TextFont { get; set; }

        /// <summary>
        /// 文字大小。
        /// </summary>
        int TextSize { get; set; }

        /// <summary>
        /// 文字颜色。
        /// </summary>
        string TextColor { get; set; }

        /// <summary>
        /// 水印图片。
        /// </summary>
        string WatermarkImage { get; set; }

        /// <summary>
        /// 水印图片透明度。
        /// </summary>
        int WatermarkImageTransparency { get; set; }

        /// <summary>
        /// 基准点。
        /// </summary>
        DatumMark DatumMark { get; set; }

        /// <summary>
        /// 边距。
        /// </summary>
        int Margin1 { get; set; }

        /// <summary>
        /// 边距。
        /// </summary>
        int Margin2 { get; set; }
    }
}
