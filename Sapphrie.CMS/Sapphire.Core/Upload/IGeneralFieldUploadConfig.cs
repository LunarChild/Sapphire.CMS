using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sapphire.Core.Upload
{
    /// <summary>
    /// 上传配置接口
    /// </summary>
    public interface IGeneralFieldUploadConfig
    {
        /// <summary>
        /// 获取上传配置。
        /// </summary>
        /// <returns>上传配置。</returns>
        IUploadConfig GetUploadConfig();
    }
}
