using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web.Hosting;
using System.Web.Script.Serialization;

namespace Sapphire.Core.CommonHelper
{
    /// <summary>
    /// 安全助手类。
    /// </summary>
    public class SecurityHelper
    {
        /// <summary>
        /// 公钥信息。
        /// </summary>
        private const string PublicKeyString =
            "<DSAKeyValue><P>mHAEqI6IKfMgDOCpNRIsyEom37D2Y+mjVldF3SAgkeAW2gK5PBgXzj/4z+RgET2sTIsuX2nI0rSy8NDIETzuxKIHG/Y2n1L7rJERO68bv/aLdQnQIRuxMM3sxDdmFWnlzkSJl6aoFDRX12EYadLVt8hNDUom/4yW5vvQ2PfThC8=</P><Q>49y+vLl9Yzzi6DgS54I3CmhG4lk=</Q><G>QmQF+h01/Vs6Sxj9sykGiahKNyHoWgeSl6Gb3x0ZyECpjxuz4N12JM78bdTXAWYXNjddtnF1cwJfXiaGOGaMa/XZTpohFNSPRil4ob06aJPynjoLvRl2nMT9JgUyVZEbZFCGLjO0IR0tjsZYYvXxYFRsrpspoLrVVW7rWnCCkDM=</G><Y>AE3MDzo1EeFMqxzAA81dG0+iIKUg8m6KX1dGk9PpmDJ7zuzJs6DhqULXwWY2OAccC//Cxnc1gP1J1eDyi8lIxFtLxmhahn2iBg4RPaNE5CZ9046uEG0o4vm6dBGi14H3D8NHWkB/OwghRaNWoMib0mI5Ng/BzrVxnukH0qQIbiM=</Y><J>q0LsULCQEyqSH+Q3fOXFbsNnaIVZSTTwoQ4z7FPPL54w/SfTb7scDTZ+NCVKeRKeKZmbVKRJeJYR87DHlfoHajJfQPEQzjT1efqBldnPSogdasae3U4fbs1ZhYAxvOnc43HGC+mmbVnR77Pe</J><Seed>c5Ji+w9X/rpYbEqmJiMM2wSLZ6s=</Seed><PgenCounter>BzA=</PgenCounter></DSAKeyValue>";

        private static readonly byte[] RijndaelKey = { 186, 103, 242, 207, 5, 144, 54, 107, 202, 81, 34, 47, 21, 177, 44, 130, 201, 8, 24, 39, 222, 49, 185, 43, 176, 229, 190, 70, 65, 124, 204, 44 };

        private static readonly byte[] RijndaelIv = { 117, 207, 178, 200, 228, 86, 243, 185, 123, 38, 149, 196, 116, 116, 47, 236 };

        private static readonly string LicensePath = HostingEnvironment.MapPath("~/License/License.lic");

        /// <summary>
        /// 验证许可证是否有效。
        /// </summary>
        /// <returns>有效返回True，否则返回False。</returns>
        public static bool Verify()
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                var dic = (Dictionary<string, object>)serializer.DeserializeObject(DecryptTextFromFile());

                var licenseData = new UnicodeEncoding().GetBytes(dic["LicenseData"].ToString());
                SHA1 sha = new SHA1CryptoServiceProvider();
                var hashValue = sha.ComputeHash(licenseData);
                var signedData = ObjectToByteArray(dic["SignedData"]);

                if (DsaVerifyHash(hashValue, signedData, "SHA1", PublicKeyString))
                {
                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 验证签名数据。
        /// </summary>
        /// <param name="hashValue">哈希数据。</param>
        /// <param name="signedData">已签名的哈希数据。</param>
        /// <param name="hashAlg">哈希算法名称。</param>
        /// <param name="publicKeyXmlString">公钥数据。</param>
        /// <returns>验证通过返回True，否则返回False。</returns>
        private static bool DsaVerifyHash(byte[] hashValue, byte[] signedData, string hashAlg, string publicKeyXmlString)
        {
            try
            {
                var dsa = new DSACryptoServiceProvider();
                dsa.FromXmlString(publicKeyXmlString);

                var signatureDeformatter = new DSASignatureDeformatter(dsa);
                signatureDeformatter.SetHashAlgorithm(hashAlg);

                return signatureDeformatter.VerifySignature(hashValue, signedData);
            }
            catch (CryptographicException)
            {
                return false;
            }
        }

        /// <summary>
        /// 解密许可证。
        /// </summary>
        /// <returns>返回已解密信息。</returns>
        private static string DecryptTextFromFile()
        {
            var fileStream = File.Open(LicensePath, FileMode.Open);
            var rijndaelAlg = Rijndael.Create();
            var cryptoStream = new CryptoStream(fileStream, rijndaelAlg.CreateDecryptor(RijndaelKey, RijndaelIv), CryptoStreamMode.Read);
            var streamReader = new StreamReader(cryptoStream);
            string val;

            try
            {
                val = streamReader.ReadLine();
            }
            catch (Exception)
            {
                val = null;
            }
            finally
            {
                streamReader.Close();
                cryptoStream.Close();
                fileStream.Close();
            }

            return val;
        }

        /// <summary>
        /// Object对象转换为字节数组。
        /// </summary>
        /// <param name="data">object数据。</param>
        /// <returns>返回字节数组。</returns>
        private static byte[] ObjectToByteArray(object data)
        {
            var array = (object[])data;
            var result = new byte[array.Length];
            for (var i = 0; i < array.Length; i++)
            {
                result[i] = byte.Parse(array[i].ToString());
            }

            return result;
        }
    }
}