using System.IO;
using System.Drawing.Imaging;
using OpenTK.Graphics.OpenGL;
using System.Drawing;

using GraphicsRenderer;

namespace Utils
{
    // Unique method class for loading images into the game
    public class Loader
    {

        /// <summary>
        /// Load images into the game
        /// </summary>
        /// <param name="path">path of the image files </param>
        /// <returns></returns>
        public static Texture2D LoadTexture(string path){
            if(!File.Exists("assets/" + path)){
                throw new FileNotFoundException("File not found at 'assets/" + path + "'");
            }

            int id = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, id);

            System.Drawing.Bitmap bmp = new System.Drawing.Bitmap("assets/" + path);
            BitmapData Data = bmp.LockBits(new System.Drawing.Rectangle(0, 0, bmp.Width, bmp.Height), 
                ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, Data.Width, Data.Height, 
                0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, Data.Scan0);

            bmp.UnlockBits(Data);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int) TextureWrapMode.Clamp);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int) TextureWrapMode.Clamp);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int) TextureMinFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int) TextureMagFilter.Nearest);

            return new Texture2D(id, bmp.Width, bmp.Height);

        }

    }
}