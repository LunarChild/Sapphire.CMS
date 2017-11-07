//using System;
//using System.Drawing;
//using System.Drawing.Imaging;
//using System.IO;
//using System.Linq;
//using System.Web;
//using Sapphire.Core.Config;

//namespace Sapphire.Core.Drawing
//{
//    /// <summary>
//    /// 生成水印类。
//    /// </summary>
//    public class WatermarkBuilder
//    {
//        /// <summary>
//        /// 构造函数。
//        /// </summary>
//        /// <param name="watermarkConfig">水印参数。</param>
//        public WatermarkBuilder(IWatermarkConfig watermarkConfig)
//        {
//            this.WatermarkConfig = watermarkConfig;
//        }

//        /// <summary>
//        /// 水印参数设置。
//        /// </summary>
//        private IWatermarkConfig WatermarkConfig { get; set; }

//        /// <summary>
//        /// 添加水印。
//        /// </summary>
//        /// <param name="originalImagePath">原图片相对地址。</param>
//        public void AddWatermark(string originalImagePath)
//        {
//            this.GenerateWatermarkImage(originalImagePath);
//        }

//        /// <summary>
//        /// 预览水印。
//        /// </summary>
//        /// <param name="originalImagePath">原图片相对地址。</param>
//        public void PreviewWatermark(string originalImagePath)
//        {
//            this.GenerateWatermarkImage(originalImagePath, true);
//        }

//        /// <summary>
//        ///  生成水印图。
//        /// </summary>
//        /// <param name="originalImagePath">原图片相对地址。</param>
//        /// <param name="preview">预览。</param>
//        private void GenerateWatermarkImage(string originalImagePath, bool preview = false)
//        {
//            //var uploadPath = HttpContext.Current.Server.MapPath(originalImagePath);
//            //var newPath = uploadPath;
//            //originalImagePath = uploadPath;

//            //Image image = null;
//            //Bitmap bitmap = null;
//            //Graphics graphics = null;
//            //try
//            //{
//            //    image = Image.FromFile(originalImagePath);
//            //    bitmap = new Bitmap(image.Width, image.Height, PixelFormat.Format32bppArgb);
//            //    graphics = Graphics.FromImage(bitmap);
//            //    graphics.DrawImage(image, 0, 0, image.Width, image.Height);

//            //    switch (this.WatermarkConfig.WatermarkType)
//            //    {
//            //        case WatermarkType.TextWatermark:
//            //            this.AddWatermarkText(graphics, image.Width, image.Height);
//            //            break;

//            //        case WatermarkType.PhotoWatermark:
//            //            this.AddWatermarkImage(graphics, image.Width, image.Height);
//            //            break;
//            //    }

//            //    ImageCodecInfo imageCodecInfo = ImageCodecInfo.GetImageEncoders().First(c => c.MimeType == "image/jpeg");
//            //    EncoderParameters encoderParameters = new EncoderParameters(3);
//            //    encoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, 100L);
//            //    encoderParameters.Param[1] = new EncoderParameter(Encoder.ScanMethod, (int)EncoderValue.ScanMethodInterlaced);
//            //    encoderParameters.Param[2] = new EncoderParameter(Encoder.RenderMethod, (int)EncoderValue.RenderProgressive);

//            //    image.Dispose();
//            //    if (preview)
//            //    {
//            //        newPath = newPath.Replace(Path.GetFileName(newPath), "PreviewWatermarkSample" + Path.GetExtension(newPath));
//            //    }

//            //    bitmap.Save(newPath, imageCodecInfo, encoderParameters);
//            //}
//            //catch (Exception e)
//            //{
//            //    throw e;
//            //}
//            //finally
//            //{
//            //    image.Dispose();
//            //    graphics.Dispose();
//            //    bitmap.Dispose();
//            //}
//        }

//        /// <summary>
//        /// 水印文字。
//        /// </summary>
//        /// <param name="picture">imge 对象。</param>
//        /// <param name="width">原图的宽。</param>
//        /// <param name="height">原图的高。</param>
//        private void AddWatermarkText(Graphics picture, float width, float height)
//        {
//            var watermarkText = this.WatermarkConfig.WatermarkText;
//            var textSize = this.WatermarkConfig.TextSize;
//            var textFont = this.WatermarkConfig.TextFont;
//            var textColor = this.WatermarkConfig.TextColor;
//            var datumMark = this.WatermarkConfig.DatumMark;

//            var margin1 = (float)this.WatermarkConfig.Margin1 / 100;
//            var margin2 = (float)this.WatermarkConfig.Margin2 / 100;

//            Font watermarkFont = null;
//            watermarkFont = new Font(textFont, textSize);

//            SizeF watermarkSizeF = picture.MeasureString(watermarkText, watermarkFont);

//            float xpos = 0;
//            float ypos = 0;

//            this.GetWatermarkPosition(width, height, watermarkSizeF.Width, watermarkSizeF.Height, datumMark, margin1, margin2, out xpos, out ypos);

//            var stringFormat = new StringFormat { Alignment = StringAlignment.Near };

//            var colorConverter = new ColorConverter();
//            var toColor = (Color)colorConverter.ConvertFromString(textColor);
//            var semiTransBrush = new SolidBrush(toColor);
//            try
//            {
//                picture.DrawString(watermarkText, watermarkFont, semiTransBrush, xpos, ypos, stringFormat);
//            }
//            catch
//            {
//                throw new Exception("添加文字水印出现异常。");
//            }
//            finally
//            {
//                semiTransBrush.Dispose();
//                stringFormat.Dispose();
//                watermarkFont.Dispose();
//            }
//        }

//        /// <summary>
//        /// 水印图片。
//        /// </summary>
//        /// <param name="picture">imge 对象。</param>
//        /// <param name="width">原图的宽。</param>
//        /// <param name="height">原图的高。</param>
//        private void AddWatermarkImage(Graphics picture, float width, float height)
//        {
//            var watermarkImageFileName = this.WatermarkConfig.WatermarkImage;
//            var waterMarkPicPath = HttpContext.Current.Server.MapPath("~/Upload" + watermarkImageFileName.Substring(1));
//            var datumMark = this.WatermarkConfig.DatumMark;
//            var margin1 = (float)this.WatermarkConfig.Margin1 / 100;
//            var margin2 = (float)this.WatermarkConfig.Margin2 / 100;

//            // 设置图片的透明度:0.0f为完全透明，1.0f为完全不透明
//            var transparence = Convert.ToSingle(this.WatermarkConfig.WatermarkImageTransparency) / 100f;

//            Image watermark = null;
//            try
//            {
//                watermark = new Bitmap(waterMarkPicPath);
//            }
//            catch
//            {
//                throw new FileNotFoundException("图片水印的路径未找到。");
//            }

//            var imageAttributes = new ImageAttributes();
//            var colorMap = new ColorMap
//            {
//                OldColor = Color.FromArgb(255, 0, 255, 0),
//                NewColor = Color.FromArgb(0, 0, 0, 0)
//            };

//            ColorMap[] remapTable = { colorMap };

//            imageAttributes.SetRemapTable(remapTable, ColorAdjustType.Bitmap);

//            float[][] colorMatrixElements =
//            {
//                new float[] { 1.0f, 0.0f, 0.0f, 0.0f, 0.0f },
//                new float[] { 0.0f, 1.0f, 0.0f, 0.0f, 0.0f },
//                new float[] { 0.0f, 0.0f, 1.0f, 0.0f, 0.0f },
//                new float[] { 0.0f, 0.0f, 0.0f, transparence, 0.0f },
//                new float[] { 0.0f, 0.0f, 0.0f, 0.0f, 1.0f }
//            };

//            var colorMatrix = new ColorMatrix(colorMatrixElements);

//            imageAttributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

//            float xpos = 0;
//            float ypos = 0;
//            var watermarkWidth = 0;
//            var watermarkHeight = 0;
//            var percent = 1d;
//            var waterMarkPercent = 4;  

//            //水印缩小比例为1/4
//            var toWidth = Convert.ToDouble(width / waterMarkPercent); 

//            var toHeight = Convert.ToDouble(height / waterMarkPercent);  
//            var toWatermarkWidth = Convert.ToDouble(watermark.Width);
//            var toWatermarkHeight = Convert.ToDouble(watermark.Height);

//            if ((width > watermark.Width * waterMarkPercent) && (height > watermark.Height * waterMarkPercent))
//            {
//                percent = 1;
//            }
//            else if ((width > watermark.Width * waterMarkPercent) && (height < watermark.Height * waterMarkPercent))
//            {
//                percent = toHeight / toWatermarkHeight;     
//            }
//            else
//            {
//                if ((width < watermark.Width * waterMarkPercent) && (height > watermark.Height * waterMarkPercent))
//                {
//                    percent = toWidth / toWatermarkWidth; 
//                }
//                else
//                {
//                    if ((width * watermark.Height) > (height * watermark.Width))
//                    {
//                        percent = toHeight / toWatermarkHeight; 
//                    }
//                    else
//                    {
//                        percent = toWidth / toWatermarkWidth; 
//                    }
//                }
//            }

//            watermarkWidth = Convert.ToInt32(watermark.Width * percent); 
//            watermarkHeight = Convert.ToInt32(watermark.Height * percent);

//            this.GetWatermarkPosition(width, height, watermarkWidth, watermarkHeight, datumMark, margin1, margin2, out xpos, out ypos);

//            try
//            {
//                picture.DrawImage(watermark, new Rectangle((int)xpos, (int)ypos, watermarkWidth, watermarkHeight), 0, 0, watermark.Width, watermark.Height, GraphicsUnit.Pixel, imageAttributes);
//            }
//            catch
//            {
//                throw new Exception("添加图片水印出现异常。");
//            }
//            finally
//            {
//                watermark.Dispose();
//                imageAttributes.Dispose();
//            }
//        }

//        private void GetWatermarkPosition(float originalImageWidth, float originalImageHeight, float watermarkWidth, float watermarkHeight, DatumMark datumMark, float margin1, float margin2, out float xpos, out float ypos)
//        {
//            xpos = 0;
//            ypos = 0;

//            switch (datumMark)
//            {
//                case DatumMark.UpperLeft:
//                    xpos = originalImageWidth * margin1;
//                    xpos = xpos == originalImageWidth ? xpos - watermarkWidth : xpos;
//                    ypos = originalImageHeight * margin2;
//                    ypos = ypos == originalImageHeight ? ypos - watermarkHeight : ypos;
//                    break;
//                case DatumMark.LowerRight:
//                    xpos = (originalImageWidth - watermarkWidth) - (originalImageWidth * margin1);
//                    xpos = xpos < 0 ? 0 : xpos;
//                    ypos = (originalImageHeight - watermarkHeight) - (originalImageHeight * margin2);
//                    ypos = ypos < 0 ? 0 : ypos;
//                    break;
//                case DatumMark.Middle:
//                    var halfWidth = originalImageWidth / 2;
//                    var halfheight = originalImageHeight / 2;
//                    xpos = halfWidth + (halfWidth * margin1);
//                    xpos = xpos == originalImageWidth ? xpos - watermarkWidth : xpos;
//                    ypos = halfheight + (halfheight * margin2);
//                    ypos = ypos == originalImageHeight ? ypos - watermarkHeight : ypos;
//                    break;
//            }
//        }
//    }
//}
