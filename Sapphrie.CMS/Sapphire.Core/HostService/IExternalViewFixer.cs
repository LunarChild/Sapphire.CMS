using System.IO;

namespace Sapphire.Core.HostService
{
    /// <summary>
    /// Used to correct external view files。
    /// </summary>
    public interface IExternalViewFixer
    {
        /// <summary>
        /// Modify the view。
        /// </summary>
        /// <param name="virtualPath">Path to view。</param>
        /// <param name="stream">Stream containing the original view。</param>
        /// <param name="webConfigStream">Stream containing the web config。</param>
        /// <returns>Stream with modified contents。</returns>
        Stream CorrectView(string virtualPath, Stream stream, Stream webConfigStream);
    }
}