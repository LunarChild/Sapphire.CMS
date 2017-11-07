using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Web;

namespace Sapphire.Core.CommonHelper
{
    /// <summary>
    /// 验证码。
    /// </summary>
    public class CaptchaHelper
    {
        #region 私有属性

        private const float V = 100F;

        // 画图字体类型
        private static readonly IList<FontFamily> Fonts = new List<FontFamily>
        {
            new FontFamily("Times New Roman"),
            new FontFamily("Georgia"),
            new FontFamily("Arial"),
            new FontFamily("Comic Sans MS")
        };

        #endregion

        /// <summary>
        /// 初始化验证码。
        /// </summary>
        public CaptchaHelper()
        {
            this.ValidateCodeBoundString = "a|b|c|d|e|f|g|h|i|j|k|l|m|n|o|p|q|r|s|t|u|v|w|x|y|z|0|1|2|3|4|5|6|7|8|9";
            this.ValidateCodeLengthMode = ValidateCodeLengthType.Static;
            this.ValidateCodeMaxLength = 6;
            this.ValidateCodeMinLength = 4;
            this.ValidateCodeFontSize = 10;
            this.FontColor = Color.Blue;
            this.Background = Color.White;
            this.Width = 100;
            this.Height = 40;
        }

        /// <summary>
        /// 生成验证码的方式。
        /// </summary>
        public enum ValidateCodeLengthType
        {
            /// <summary>
            /// 固定长度的。
            /// </summary>
            Static,

            /// <summary>
            /// 可变长度的。
            /// </summary>
            Random
        }

        #region 公有属性

        /// <summary>
        /// 生成验证码的字符集。
        /// </summary>
        public string ValidateCodeBoundString { get; set; }

        /// <summary>
        /// 只读：获取验证码生成范围的数组。
        /// </summary>
        public string[] ValidateCodeBound
        {
            get
            {
                return this.ValidateCodeBoundString.Split(new[] { '|' });
            }
        }

        /// <summary>
        /// 验证码长度是可变长度的还是固定长度的。
        /// 如果是固定长度的则长度为ValidateCodeMaxLength所设置的值。
        /// </summary>
        public ValidateCodeLengthType ValidateCodeLengthMode { get; set; }

        /// <summary>
        /// 验证码的最大长度。
        /// </summary>
        public byte ValidateCodeMaxLength { get; set; }

        /// <summary>
        /// 验证码的最小长度。
        /// </summary>
        public byte ValidateCodeMinLength { get; set; }

        /// <summary>
        /// 验证码的文字大小，单位为“像素”。
        /// </summary>
        public byte ValidateCodeFontSize { get; set; }

        /// <summary>
        /// 字体颜色。
        /// </summary>
        public Color FontColor { get; set; }

        /// <summary>
        /// 背景颜色。
        /// </summary>
        public Color Background { get; set; }

        /// <summary>
        /// 宽度。
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// 高度。
        /// </summary>
        public int Height { get; set; }

        #endregion

        /// <summary>
        /// 验证用户输入的验证码。
        /// </summary>
        /// <param name="context">HttpContext对象实例。</param>
        /// <param name="captcha">验证码。</param>
        /// <returns>验证结果。</returns>
        public static bool Validate(HttpContext context, string captcha)
        {
            var session = context.Session["session_captcha"];
            return session != null && session.ToString().Equals(captcha, StringComparison.CurrentCultureIgnoreCase);
        }

        #region 生成随机验证码

        /// <summary>
        /// 生成随机验证码图片二进制数据。
        /// </summary>
        /// <param name="valCode">验证码。</param>
        /// <returns>返回随机验证码图片二进制数据。</returns>
        public byte[] CreateValidateGraphic(string valCode)
        {
            var image = this.Generate(valCode);

            var memoryStream = new MemoryStream();
            try
            {
                image.Save(memoryStream, ImageFormat.Gif);

                return memoryStream.ToArray();
            }
            catch
            {
                return memoryStream.ToArray();
            }
            finally
            {
                image.Dispose();
            }
        }

        /// <summary>
        /// 生成验证码图片。
        /// </summary>
        /// <param name="checkCode">验证码字符串。</param>
        /// <returns>返回一个表示验证码的Bitmap。</returns>
        public Bitmap Generate(string checkCode)
        {
            var random = new Random();
            var text = checkCode;

            // 随机选择字体名称
            var familyName = Fonts[random.Next(Fonts.Count - 1)];

            // 创建一个32位的bitmap图片
            var bitmap = new Bitmap(this.Width, this.Height, PixelFormat.Format32bppArgb);

            // 创建一个用于绘画的Graphics对象
            using (var g = Graphics.FromImage(bitmap))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                var rect = new Rectangle(0, 0, this.Width, this.Height);

                // 填充背景
                using (var brush = new HatchBrush(HatchStyle.SmallConfetti, Color.LightGray, this.Background))
                {
                    g.FillRectangle(brush, rect);

                    // 创建文本字体
                    SizeF size;
                    float fontSize = rect.Height + 1;
                    Font font;

                    // 调整文本的大小直到适应图片的大小
                    do
                    {
                        fontSize--;
                        font = new Font(familyName, fontSize, FontStyle.Bold);
                        size = g.MeasureString(text, font);
                    }
                    while (size.Width > rect.Width);

                    // 创建文本格式
                    var format = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };

                    // 为文本创建一个路径并随机的扭曲它
                    var path = new GraphicsPath();
                    path.AddString(text, font.FontFamily, (int)font.Style, font.Size, rect, format);
                    PointF[] points =
                    {
                        new PointF(random.Next(rect.Width) / V, random.Next(rect.Height) / V), new PointF(rect.Width - (random.Next(rect.Width) / V), random.Next(rect.Height) / V),
                        new PointF(random.Next(rect.Width) / V, rect.Height - (random.Next(rect.Height) / V)),
                        new PointF(rect.Width - (random.Next(rect.Width) / V), rect.Height - (random.Next(rect.Height) / V))
                    };
                    var matrix = new Matrix();
                    matrix.Translate(0F, 0F);
                    path.Warp(points, rect, matrix, WarpMode.Perspective, 0F);

                    // 绘制文本
                    using (var hatchBrush = new HatchBrush(HatchStyle.LargeConfetti, Color.LightGray, this.FontColor))
                    {
                        g.FillPath(hatchBrush, path);

                        // 添加随机的噪点
                        var m = Math.Max(rect.Width, rect.Height);
                        for (var i = 0; i < (int)(rect.Width * rect.Height / 30F); i++)
                        {
                            var x = random.Next(rect.Width);
                            var y = random.Next(rect.Height);
                            var w = random.Next(m / 50);
                            var h = random.Next(m / 50);
                            g.FillEllipse(hatchBrush, x, y, w, h);
                        }
                    }

                    // 清理
                    font.Dispose();
                }

                return bitmap;
            }
        }

        /// <summary>
        /// 产生验证码。
        /// </summary>
        /// <returns>返回验证码。</returns>
        public string GetValidateCode()
        {
            var ran = new Random();
            var boundLength = this.ValidateCodeBound.Length;
            int maxLength = this.GetValidateCodeLength();

            var codeStringBuilder = new StringBuilder();
            for (var i = 0; i < maxLength; i++)
            {
                codeStringBuilder.Append(this.ValidateCodeBound[ran.Next(boundLength)]);
            }

            return codeStringBuilder.ToString();
        }

        /// <summary>
        /// 取得要生成的验证码的长度。
        /// </summary>
        /// <returns>返回验证码的长度。</returns>
        private byte GetValidateCodeLength()
        {
            // 如果是定长的
            if (this.ValidateCodeLengthMode == ValidateCodeLengthType.Static)
            {
                return this.ValidateCodeMaxLength;
            }

            var ran = new Random();
            if (this.ValidateCodeMaxLength > this.ValidateCodeMinLength)
            {
                return (byte)ran.Next(this.ValidateCodeMinLength, this.ValidateCodeMaxLength + 1);
            }

            return (byte)ran.Next(this.ValidateCodeMaxLength, this.ValidateCodeMinLength + 1);
        }

        #endregion
    }
}