/**
 * Copyright (c) 2012 Planet Telex Inc. all rights reserved.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *         http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
﻿using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using Goheer.EXIF;
using PlanetTelex.Drawing.Properties;

namespace PlanetTelex.Drawing
{
    /// <summary>
    /// A basic image editing API.
    /// </summary>
    public class ImageEditor
    {
        private readonly string _filename;
        private readonly EXIFextractor _exif;
        
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageEditor"/> class.
        /// </summary>
        /// <param name="filename">The filename.</param>
        public ImageEditor(String filename) : this(Image.FromFile(filename))
        {
            _filename = filename;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageEditor"/> class.
        /// </summary>
        /// <param name="stream">The stream.</param>
        public ImageEditor(Stream stream) : this(Image.FromStream(stream)) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageEditor"/> class.
        /// </summary>
        /// <param name="image">The image.</param>
        public ImageEditor(Image image)
        {
            Image = image;
            InterpolationMode = InterpolationMode.Default;
            _exif = new EXIFextractor(Image);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Returns the <see cref="Image"/> instance being manipulated by this <see cref="ImageEditor"/>.
        /// </summary>
        /// <returns>The <see cref="Image"/> instance.</returns>
        public Image Image
        {
            get { return _image; }
            set
            {
                if (_image != null)
                    _image.Dispose();
                _image = value;
            }
        }
        private Image _image;

        /// <summary>
        /// Gets or sets the Drawing 2D interpolation mode for determining the quality of output.
        /// </summary>
        /// <value>
        /// The interpolation mode.
        /// </value>
        public InterpolationMode InterpolationMode { get; set; }

        #endregion

        #region Resize

        /// <summary>
        /// Resizes the image using the specified size dimensions (keeps width and height proportionate).
        /// </summary>
        /// <param name="size"><see cref="Size"/> object representing the target height and width of the resized image.</param>
        /// <returns>The <see cref="ImageEditor"/> instance (useful for chaining methods).</returns>
        public ImageEditor Resize(Size size)
        {
            Size resizeDimensions = GetResizeDimensions(size);
            ManipulateImage(CreateEmptyBitmap(resizeDimensions), (graphics, bitmap) => graphics.DrawImage(Image, 0, 0, resizeDimensions.Width, resizeDimensions.Height));
            return this;
        }

        /// <summary>
        /// Calculates the new dimensions for resizing an image (maintains the original width to height ratio).
        /// </summary>
        /// <param name="size"><see cref="Size"/> object representing the target height and width.</param>
        /// <returns>Size object representing the resize dimensions based off the target values.</returns>
        public Size GetResizeDimensions(Size size)
        {
            int sourceWidth = Image.Width;
            int sourceHeight = Image.Height;

            float widthRatio = (float)size.Width / sourceWidth;
            float heightRatio = (float)size.Height / sourceHeight;

            float resizeRatio = heightRatio < widthRatio ? heightRatio : widthRatio;

            int destWidth = (int)Math.Round(sourceWidth * resizeRatio);
            int destHeight = (int)Math.Round(sourceHeight * resizeRatio);

            return new Size(destWidth, destHeight);
        }

        #endregion

        #region Color Transformation

        /// <summary>
        /// Converts the image to greyscale colors.
        /// </summary>
        /// <returns>The <see cref="ImageEditor"/> instance (useful for chaining methods).</returns>
        public ImageEditor Greyscale()
        {
            return Grayscale();
        }

        /// <summary>
        /// Converts the image to grayscale colors.
        /// </summary>
        /// <returns>The <see cref="ImageEditor"/> instance (useful for chaining methods).</returns>
        public ImageEditor Grayscale()
        {
            ColorMatrix colorMatrix = new ColorMatrix(
                new[] {
                    new[] {.3f, .3f, .3f, 0, 0},
                    new[] {.59f, .59f, .59f, 0, 0},
                    new[] {.11f, .11f, .11f, 0, 0},
                    new float[] {0, 0, 0, 1, 0},
                    new float[] {0, 0, 0, 0, 1}
                });

            ApplyColorMatrix(colorMatrix);

            return this;
        }

        /// <summary>
        /// Converts the image to sepia colors.
        /// </summary>
        /// <returns>The <see cref="ImageEditor"/> instance (useful for chaining methods).</returns>
        public ImageEditor Sepia()
        {
            ColorMatrix colorMatrix = new ColorMatrix(
                new[] {
                    new[] {.393f, .349f, .272f, 0, 0},
                    new[] {.769f, .686f, .534f, 0, 0},
                    new[] {.189f, .168f, .131f, 0, 0},
                    new float[] {0, 0, 0, 1, 0},
                    new float[] {0, 0, 0, 0, 1}    
                });

            ApplyColorMatrix(colorMatrix);

            return this;
        }


        private void ApplyColorMatrix(ColorMatrix colorMatrix)
        {
            ManipulateImage((graphics, canvas) =>
            {
                ImageAttributes attributes = new ImageAttributes();
                attributes.SetColorMatrix(colorMatrix);
                Bitmap original = ToBitmap();
                graphics.DrawImage(original, new Rectangle(0, 0, original.Width, original.Height), 0, 0, original.Width, original.Height, GraphicsUnit.Pixel, attributes);
            });
        }

        #endregion

        #region Orientation

        /// <summary>
        /// Gets a value indicating whether the orientation is "normal" and not "flipped".
        /// </summary>
        /// <value>
        ///   <c>true</c> if the orientation is normal; otherwise, <c>false</c>.
        /// </value>
        public bool OrientationIsNormal
        {
            get { return Orientation == RotateFlipType.RotateNoneFlipNone; }
        }

        /// <summary>
        /// Fixes the orientation of this instance and resets the EXIF metadata.
        /// </summary>
        /// <returns>
        /// The <see cref="ImageEditor"/> instance (useful for chaining methods).
        /// </returns>
        public ImageEditor FixOrientation()
        {
            return FixOrientation(true);
        }

        /// <summary>
        /// Fixes the orientation of this instance.
        /// </summary>
        /// <param name="resetExifOrientation">if set to <c>true</c> reset orientation in EXIF metadata.</param>
        /// <returns>
        /// The <see cref="ImageEditor"/> instance (useful for chaining methods).
        /// </returns>
        public ImageEditor FixOrientation(bool resetExifOrientation)
        {
            // Don't flip of orientation is correct
            if (!OrientationIsNormal)
            {
                Image.RotateFlip(Orientation);
                Orientation = RotateFlipType.RotateNoneFlipNone;
            }

            return this;
        }

        /// <summary>
        /// Gets the orientation.
        /// </summary>
        public RotateFlipType Orientation
        {
            get
            {
                return OrientationToFlipType(_exif["Orientation"] != null ? int.Parse(_exif["Orientation"].ToString()) : 0);
            }
            set
            {
                _exif.setTag(0x112, FlipTypeToOrientation(value));
            }
        }

        private static string FlipTypeToOrientation(RotateFlipType flipType)
        {
            switch (flipType)
            {
                case RotateFlipType.RotateNoneFlipNone:
                    return "1";
                case RotateFlipType.RotateNoneFlipX:
                    return "2";
                case RotateFlipType.Rotate180FlipNone:
                    return "3";
                case RotateFlipType.Rotate180FlipX:
                    return "4";
                case RotateFlipType.Rotate90FlipX:
                    return "5";
                case RotateFlipType.Rotate90FlipNone:
                    return "6";
                case RotateFlipType.Rotate270FlipX:
                    return "7";
                case RotateFlipType.Rotate270FlipNone:
                    return "8";
                default:
                    return "1";
            }
        }

        private static RotateFlipType OrientationToFlipType(int orientation)
        {
            switch (orientation)
            {
                case 1:
                    return RotateFlipType.RotateNoneFlipNone;
                case 2:
                    return RotateFlipType.RotateNoneFlipX;
                case 3:
                    return RotateFlipType.Rotate180FlipNone;
                case 4:
                    return RotateFlipType.Rotate180FlipX;
                case 5:
                    return RotateFlipType.Rotate90FlipX;
                case 6:
                    return RotateFlipType.Rotate90FlipNone;
                case 7:
                    return RotateFlipType.Rotate270FlipX;
                case 8:
                    return RotateFlipType.Rotate270FlipNone;
                default:
                    return RotateFlipType.RotateNoneFlipNone;
            }
        }

        #endregion

        #region Rotate

        /// <summary>
        /// Rotates the image either clockwise or counter-clockwise.
        /// </summary>
        /// <param name="rotationAngle">The angle (in degrees).
        /// NOTE:
        /// Positive values will rotate clockwise
        /// Negative values will rotate counter-clockwise</param>
        /// <returns>The <see cref="ImageEditor"/> instance (useful for chaining methods).</returns>
        public ImageEditor Rotate(float rotationAngle)
        {
            using (Matrix matrixRotate = new Matrix())
            {
                matrixRotate.Translate((float)Image.Width / -2, (float)Image.Height / -2, MatrixOrder.Append);
                matrixRotate.RotateAt(rotationAngle, new Point(0, 0), MatrixOrder.Append);
                using (GraphicsPath graphicsPath = new GraphicsPath())
                {
                    graphicsPath.AddPolygon(new[] { new Point(0, 0), new Point(Image.Width, 0), new Point(0, Image.Height) });
                    graphicsPath.Transform(matrixRotate);
                    PointF[] pts = graphicsPath.PathPoints;

                    // Create destination bitmap sized to contain rotated source image.
                    Rectangle boundingBox = BoundingBox(Image, matrixRotate);
                    Bitmap bitmap = CreateEmptyBitmap(new Size(boundingBox.Width, boundingBox.Height));

                    ManipulateImage(bitmap, (graphics, canvas) =>
                    {
                        Matrix matrixTarget = new Matrix();
                        matrixTarget.Translate((float)bitmap.Width / 2, (float)bitmap.Height / 2, MatrixOrder.Append);
                        graphics.Transform = matrixTarget;
                        graphics.DrawImage(Image, pts);
                        graphics.DrawRectangle(Pens.Red, boundingBox);
                    });
                }
            }

            return this;
        }

        private static Rectangle BoundingBox(Image img, Matrix matrix)
        {
            GraphicsUnit graphicsUnit = new GraphicsUnit();
            Rectangle rectangle = Rectangle.Round(img.GetBounds(ref graphicsUnit));

            // Transform the four points of the image to get the resized bounding box.
            Point topLeft = new Point(rectangle.Left, rectangle.Top);
            Point topRight = new Point(rectangle.Right, rectangle.Top);
            Point bottomRight = new Point(rectangle.Right, rectangle.Bottom);
            Point bottomLeft = new Point(rectangle.Left, rectangle.Bottom);
            Point[] points = new[] { topLeft, topRight, bottomRight, bottomLeft };
            var types = new[] { (byte)PathPointType.Start, (byte)PathPointType.Line, (byte)PathPointType.Line, (byte)PathPointType.Line };

            using (GraphicsPath graphicsPath = new GraphicsPath(points, types))
            {
                graphicsPath.Transform(matrix);
                return Rectangle.Round(graphicsPath.GetBounds());
            }
        }

        #endregion

        #region Bitmap Methods

        /// <summary>
        /// Converts this <see cref="Image"/> to a <see cref="Bitmap"/>.
        /// </summary>
        /// <returns><see cref="Bitmap"/></returns>
        public Bitmap ToBitmap()
        {
            return new Bitmap(Image);
        }

        /// <summary>
        /// Creates the empty bitmap with the current <see cref="Image"/> dimensions.
        /// </summary>
        /// <returns><see cref="Bitmap"/></returns>
        private Bitmap CreateEmptyBitmap()
        {
            return new Bitmap(Image.Width, Image.Height);
        }

        /// <summary>
        /// Creates the empty bitmap with the specified size.
        /// </summary>
        /// <returns><see cref="Bitmap"/></returns>
        private static Bitmap CreateEmptyBitmap(Size size)
        {
            return new Bitmap(size.Width, size.Height);
        }

        #endregion

        #region Helper methods

        /// <summary>
        /// Gets a graphics drawing surface for this <see cref="Image"/>.
        /// </summary>
        /// <param name="canvas">The canvas.</param>
        /// <returns><see cref="Graphics"/></returns>
        private Graphics GetGraphics(out Bitmap canvas)
        {
            canvas = CreateEmptyBitmap();
            return GetGraphics(canvas);
        }

        /// <summary>
        /// Gets a graphics drawing surface for the supplied canvas.
        /// </summary>
        /// <param name="canvas">The canvas.</param>
        /// <returns><see cref="Graphics"/></returns>
        private Graphics GetGraphics(Bitmap canvas)
        {
            Graphics graphics = Graphics.FromImage(canvas);
            graphics.InterpolationMode = InterpolationMode;
            return graphics;
        }

        /// <summary>
        /// Executes the provided delegate with the <see cref="Graphics"/> for this <see cref="Image"/> available in context.
        /// </summary>
        /// <param name="manipulation">The manipulation.</param>
        /// <returns><see cref="Bitmap"/> that results from the manipulation</returns>
        private Bitmap WithGraphics(Action<Graphics, Bitmap> manipulation)
        {
            Bitmap canvas;
            using (Graphics graphics = GetGraphics(out canvas))
            {
                manipulation(graphics, canvas);
            }
            return canvas;
        }

        /// <summary>
        /// Executes the provided delegate with the <see cref="Graphics"/> for this <see cref="Image"/> available in context.
        /// </summary>
        /// <param name="canvas">The canvas.</param>
        /// <param name="manipulation">The manipulation.</param>
        /// <returns>
        ///   <see cref="Bitmap"/> that results from the manipulation
        /// </returns>
        private Bitmap WithGraphics(Bitmap canvas, Action<Graphics, Bitmap> manipulation)
        {
            using (Graphics graphics = GetGraphics(canvas))
            {
                manipulation(graphics, canvas);
            }
            return canvas;
        }

        /// <summary>
        /// Manipulate this <see cref="Image"/> using the provided delegate.
        /// </summary>
        /// <param name="manipulation">The manipulation.</param>
        private void ManipulateImage(Action<Graphics, Bitmap> manipulation)
        {
            Image = WithGraphics(manipulation);
        }

        /// <summary>
        /// Manipulate this <see cref="Image"/> using the specified delegate.
        /// </summary>
        /// <param name="canvas">The canvas.</param>
        /// <param name="manipulation">The manipulation.</param>
        private void ManipulateImage(Bitmap canvas, Action<Graphics, Bitmap> manipulation)
        {
            Image = WithGraphics(canvas, manipulation);
        }

        #endregion

        #region Save

        /// <summary>
        /// Saves the image to the same file or stream that it was loaded from. Defaults the target format to JPEG.
        /// </summary>
        /// <returns>The <see cref="ImageEditor"/> instance (useful for chaining methods).</returns>
        /// <exception cref="ArgumentException">When the image wasn't loaded from a file.</exception>
        public ImageEditor Save()
        {
            Save(ImageFormat.Jpeg);
            return this;
        }

        /// <summary>
        /// Saves the image to the same file it was loaded from if loaded from a file.
        /// </summary>
        /// <param name="format">The target <see cref="ImageFormat"/> of the saved image.</param>
        /// <returns>The <see cref="ImageEditor"/> instance (useful for chaining methods).</returns>
        /// <exception cref="ArgumentException">When the image wasn't loaded from a file or stream.</exception>
        public ImageEditor Save(ImageFormat format)
        {
            if (_filename != null)
                Save(_filename, format);
            else
                throw new ArgumentException(Resources.ImageNotLoadedFromFile);

            return this;
        }

        /// <summary>
        /// Saves the image to the specified file. Defaults the target format to JPEG.
        /// </summary>
        /// <param name="filename">The target file path of the saved image.</param>
        /// <returns>The <see cref="ImageEditor"/> instance (useful for chaining methods).</returns>
        public ImageEditor Save(string filename)
        {
            Save(filename, ImageFormat.Jpeg);
            return this;
        }

        /// <summary>
        /// Saves the image to the specified file in the specified format.
        /// </summary>
        /// <param name="filename">The target file path of the saved image.</param>
        /// <param name="format">The target <see cref="ImageFormat"/> of the saved image.</param>
        /// <returns>The <see cref="ImageEditor"/> instance (useful for chaining methods).</returns>
        public ImageEditor Save(string filename, ImageFormat format)
        {
            Image.Save(filename, format);
            return this;
        }

        /// <summary>
        /// Saves the image to the specified stream in the specified format.
        /// </summary>
        /// <param name="stream">The stream to use for saving the image.</param>
        /// <param name="format">The target <see cref="ImageFormat"/> of the saved image.</param>
        /// <returns>The <see cref="ImageEditor"/> instance (useful for chaining methods).</returns>
        public ImageEditor Save(Stream stream, ImageFormat format)
        {
            Image.Save(stream, format);
            return this;
        }

        #endregion
    }
}
