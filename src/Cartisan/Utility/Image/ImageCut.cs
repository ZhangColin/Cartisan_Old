using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace Cartisan.Utility.Image {
    public class ImageCut {
        private int _x;
        private int _y;
        private int _width;
        private int _height;
        public ImageCut(int x, int y, int width, int height) {
            _x = x;
            _y = y;
            _width = width;
            _height = height;
        }

        public Bitmap Cut(System.Drawing.Image sourceImage) {
            // 如果没有源图，则不能剪切
            if (sourceImage == null) {
                return null;
            }

            // 源图的宽与高
            var sourceWidth = sourceImage.Width;
            var sourceHeight = sourceImage.Height;

            // 如果剪切的起点超出了图像范围，则不能剪切
            if (_x >= sourceWidth || _y >= sourceHeight) {
                return null;
            }

            // 如果剪切图像的宽度超出源图的范围，则宽度为从剪切的基点到图像的右边
            if (_x + _width > sourceWidth) {
                _width = sourceWidth - _x;
            }

            // 如果剪切图像的高度超出源图的范围，则高度为从剪切的基点到图像的底部
            if (_y + _height > sourceHeight) {
                _height = sourceHeight - _y;
            }

            try {
                // 创建剪切的图
                var bmpOut = new Bitmap(_width, _height, PixelFormat.Format64bppPArgb);
                var graphic = Graphics.FromImage(bmpOut);
                graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;

                // 在指定范围并且按指定位置绘制源图
                graphic.DrawImage(sourceImage,
                    new Rectangle(0, 0, _width, _height),
                    new Rectangle(_x, _y, _width, _height),
                    GraphicsUnit.Pixel);

                graphic.Dispose();
                return bmpOut;
            }
            catch (Exception) {
                return null;
            }
        }
    }
}