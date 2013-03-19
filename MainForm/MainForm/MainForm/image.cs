using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace MainForm
{
    class image
    {
        public static Bitmap ResizeBitmap( Bitmap originalBitmap, int requiredHeight, int requiredWidth )
        {
                int[] heightWidthRequiredDimensions;

                // Pass dimensions to worker method depending on image type required
                heightWidthRequiredDimensions = WorkDimensions(originalBitmap.Height, originalBitmap.Width, requiredHeight, requiredWidth);


                Bitmap resizedBitmap = new Bitmap(heightWidthRequiredDimensions[1],
                                                    heightWidthRequiredDimensions[0]);

                const float resolution = 72;

                resizedBitmap.SetResolution(resolution, resolution);

                Graphics graphic = Graphics.FromImage((Image)resizedBitmap);

                graphic.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                graphic.DrawImage(originalBitmap, 0, 0, resizedBitmap.Width, resizedBitmap.Height);

                graphic.Dispose();
                originalBitmap.Dispose();
                //resizedBitmap.Dispose(); // Still in use


                return resizedBitmap;
        }


        private static int[] WorkDimensions(int originalHeight, int originalWidth, int requiredHeight, int requiredWidth )
        {
            int imgHeight = 0;
            int imgWidth = 0;

            imgWidth = requiredHeight;
            imgHeight = requiredWidth;


            int requiredHeightLocal = originalHeight;
            int requiredWidthLocal = originalWidth;

            double ratio = 0;

            // Check height first
            // If original height exceeds maximum, get new height and work ratio.
            if ( originalHeight > imgHeight )
            {
                ratio = double.Parse( ( (double) imgHeight / (double) originalHeight ).ToString() );
                requiredHeightLocal = imgHeight;
                requiredWidthLocal = (int) ( (decimal) originalWidth * (decimal) ratio );
            }

            // Check width second. It will most likely have been sized down enough
            // in the previous if statement. If not, change both dimensions here by width.
            // If new width exceeds maximum, get new width and height ratio.
            if ( requiredWidthLocal >= imgWidth )
            {
                ratio = double.Parse( ( (double) imgWidth / (double) originalWidth ).ToString() );
                requiredWidthLocal = imgWidth;
                requiredHeightLocal = (int) ( (double) originalHeight * (double) ratio );
            }

            int[] heightWidthDimensionArr = { requiredHeightLocal, requiredWidthLocal };

            return heightWidthDimensionArr;
        }
        public static Bitmap RotateImg(Bitmap bmp, float angle, Color bkColor)
        {
            int w = bmp.Width;
            int h = bmp.Height;
            PixelFormat pf = default(PixelFormat);
            if (bkColor == Color.Transparent)
            {
                pf = PixelFormat.Format32bppArgb;
            }
            else
            {
                pf = bmp.PixelFormat;
            }

            Bitmap tempImg = new Bitmap(w, h, pf);
            Graphics g = Graphics.FromImage(tempImg);
            g.Clear(bkColor);
            g.DrawImageUnscaled(bmp, 1, 1);
            g.Dispose();

            GraphicsPath path = new GraphicsPath();
            path.AddRectangle(new RectangleF(0f, 0f, w, h));
            Matrix mtrx = new Matrix();
            //Using System.Drawing.Drawing2D.Matrix class 
            mtrx.Rotate(angle);
            RectangleF rct = path.GetBounds(mtrx);
            Bitmap newImg = new Bitmap(Convert.ToInt32(rct.Width), Convert.ToInt32(rct.Height), pf);
            g = Graphics.FromImage(newImg);
            g.Clear(bkColor);
            g.TranslateTransform(-rct.X, -rct.Y);
            g.RotateTransform(angle);
            g.InterpolationMode = InterpolationMode.HighQualityBilinear;
            g.DrawImageUnscaled(tempImg, 0, 0);
            g.Dispose();
            tempImg.Dispose();
            return newImg;
        }
    }
}

