namespace Sapphire.Core.Config
{
    /// <summary>
    ///图片缩略图信息接口
    /// </summary>
    public interface IThumbnailConfig
    {
        /// <summary>
        /// 
        /// </summary>
        string ThumMode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        int ThumbnailPositionX { get; set; }

        /// <summary>
        /// 。
        /// </summary>
        int ThumbnailPositionY { get; set; }

        /// <summary>
        /// 。
        /// </summary>
        int ThumbnailWidth { get; set; }

        /// <summary>
        /// 。
        /// </summary>
        int ThumbnailHeight { get; set; }

        /// <summary>
        /// 。
        /// </summary>
        string FileNameVariable { get; set; }


    }
}
