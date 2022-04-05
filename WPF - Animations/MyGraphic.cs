using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Controls; // Canvas
using System.Drawing;
using System.Windows.Media; // Context
using System.IO; // Memory Stream
using System.Windows.Media.Imaging;

namespace WPF___Animations
{
    class MyGraphic : Canvas 
    {
        public MyObject myObject = new MyObject();

        protected override void OnRender(DrawingContext drawingContext)
        {
            myObject.DrawObjects(drawingContext);
        }

        public void ReDraw()
        {
            InvalidateVisual();
        }
    }

    class MyObject 
    {
        System.Drawing.Image image; // pictures

        int SpriteIndex;
        System.Drawing.Rectangle[] SpriteOffset;
        System.Windows.Point Pos;
        int GoXStep;
        System.Windows.Size size;

        public void LoadImage()
        {
            string pathImage = "dog_run.png";
            image = System.Drawing.Image.FromFile(pathImage);

            SpriteIndex = 0;
            SpriteOffset = new System.Drawing.Rectangle[]
            {
                new System.Drawing.Rectangle(244, 3, 57, 42),
                new System.Drawing.Rectangle(304, 4, 54, 39),
                new System.Drawing.Rectangle(363, 4, 57, 38),
                new System.Drawing.Rectangle(420, 1, 55, 40),
                new System.Drawing.Rectangle(483, 4, 57, 36),
                new System.Drawing.Rectangle(542, 4, 57, 38)
            };

            Pos = new System.Windows.Point(10, 15);
            size = new System.Windows.Size(50, 50);
            GoXStep = 5;
        }

        public void DrawPartImage(DrawingContext drawingContext, System.Windows.Point pos, System.Drawing.Image image, System.Drawing.Rectangle part)
        {
            // take part
            Bitmap bmpPart = new Bitmap(part.Width, part.Height);
            using(Graphics g = Graphics.FromImage(bmpPart))
            {
                g.DrawImage(image, new System.Drawing.Rectangle
                    (0, 0, part.Width, part.Height), 
                    part.X, part.Y, part.Width, part.Height, 
                    GraphicsUnit.Pixel);
            }

            MemoryStream ms = new MemoryStream();
            bmpPart.Save(ms, System.Drawing.Imaging.ImageFormat.Png);

            BitmapImage bmpImage = new BitmapImage();
            bmpImage.BeginInit();
            ms.Seek(0, SeekOrigin.Begin);
            bmpImage.StreamSource = ms;
            bmpImage.EndInit();

            // drawing
            System.Windows.Size size = new System.Windows.Size(part.Width, part.Height);
            drawingContext.DrawImage(bmpImage, new System.Windows.Rect(pos, size));
        }

        public void DrawObjects(DrawingContext drawingContext)
        {
            if (SpriteIndex < 0 || SpriteIndex >= SpriteOffset.Length)
                SpriteIndex = 0;
            System.Drawing.Rectangle rect = SpriteOffset[SpriteIndex];
            DrawPartImage(drawingContext, Pos, image, rect);
            SpriteIndex++;
        }

        public void MoveObjects()
        {
            Pos.X += GoXStep;
        }
    }
}
