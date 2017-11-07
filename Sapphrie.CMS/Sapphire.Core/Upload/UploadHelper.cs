using System;
using System.Collections.Specialized;
using System.IO;
using System.Web;
using Sapphire.Core.Config;
using Sapphire.Core.CommonHelper;

namespace Sapphire.Core.Upload
{
    /// <summary>
    /// 上传助手类。
    /// </summary>
    public class UploadHelper
    {
        private const string Blackslash = @"\";

        ///// <summary>
        ///// 文件上传。
        ///// </summary>
        ///// <param name="file">客户端上载的文件。</param>
        ///// <param name="uploadProviderKey">上传提供者Key。</param>
        ///// <param name="uploadRuleKeys">上传目录规则、文件名规则。</param>
        ///// <returns>上传文件结果。</returns>
        //public static UploadFileResult UploadFile(PowerHttpFile file, string uploadProviderKey, NameValueCollection uploadRuleKeys)
        //{
        //    return UploadFile(file, UploadProviders.Providers[uploadProviderKey], uploadRuleKeys);
        //}

        /// <summary>
        /// 文件上传。
        /// </summary>
        /// <param name="file">客户端上载的文件。</param>
        /// <param name="uploadConfig">上传配置。</param>
        /// <param name="savePathFileName">上传路径。</param>
        /// <returns>上传文件结果。</returns>
        public static UploadFileResult UploadFile(PowerHttpFile file, IUploadConfig uploadConfig, string savePathFileName)
        {
            var uploadFileResult = new UploadFileResult();
            try
            {
                file.SaveAs(savePathFileName);               
                //if (uploadConfig.EnableWatermark)
                //{
                //    new WatermarkBuilder(WatermarkConfig.Instance).AddWatermark(relativePathFileName);
                //}
                
                //uploadFileResult.FileName = fileName;

                return uploadFileResult;
            }
            catch (Exception e)
            {
                uploadFileResult.IsError = true;
                uploadFileResult.ErrorMsg = e.Message;

                return uploadFileResult;
            }
        }
    }
}