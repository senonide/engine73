using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using Color = OpenTK.Color;

using GraphicsRenderer;
using Utils;


/*
To render text in Engine73, you can do it in two ways.
The first way is to load images with letters already designed in .png format and render them as if they were normal images.
The second way is to render text from .ttf files in an OpenGL context

In this file two groups of classes are specified. One in charge of rendering text in the first way and the second in the other. */
namespace Text
{

    #region RenderTextAsAnImage


    public static class TextHandler
    {

        private static char[] Letters;
        private static Dictionary<char, Texture2D> Alphabet;

        public static void Init(){

            Letters = new char[39] 
                        {
                        'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z','-','+',',','0','1','2','3','4','5','6','7','8','9'
                        };

            Alphabet = new Dictionary<char, Texture2D>();

            for(int i = 0; i < Letters.Length; i++){
                Alphabet.Add(Letters[i], Letter.Texture(Letters[i].ToString()));
            }

        }



        public static void RenderText(string text, Vector2 position, Color color, float scale){
            
            for(int i = 0; i < text.Length; i++){
                GraphicsRenderer.Graphics.Draw(Alphabet.GetValueOrDefault(text[i]), position + new Vector2(scale * 4 * i, 0), new Vector2(scale, scale), color);
            }
        }
        
    }


    internal static class Letter{
        public static Texture2D Texture(string name){

            return Loader.LoadTexture("fonts/ArcadeFont/" + name + ".png");

        }

    }

    #endregion


    #region RenderTextAsTTF

    // This class specifies the settings for rendering the text
    public static class Settings
    {
        // Font for the text
        public static string FontBitmapFilename = "test2.png";

        // Variables for creating the font as a bitmap
        public static int GlyphsPerLine = 16;
        public static int GlyphLineCount = 16;
        public static int GlyphWidth = 20;
        public static int GlyphHeight = 25;

        // Distance for letter spacing
        public static int CharXSpacing = 20;

        // Used to offset rendering glyphs to bitmap
        public static int AtlasOffsetX = -3, AtlassOffsetY = -1;
        public static int FontSize = 14;
        public static bool BitmapFont = false;
        public static string FromFile = "RobotoMono-Bold.ttf";
        public static string FontName = "Consolas";

    }

    // Class in charge of rendering fonts of type.ttf in an openGL context to draw text with this methot use the function DrawText() specified in Grpahics.cs
    public class FontRenderer
    {
        
        public static readonly int GlyphsPerLine = 16;
        public static readonly int GlyphLineCount = 16;
        public static int AtlasOffsetX = -3, AtlassOffsetY = -1;

        public Texture2D alphabet;
        public int GlyphWidth;
        public int GlyphHeight;
        public int CharXSpacing;

        public FontRenderer(string path, int size)
        {
            CharXSpacing = size / 3 + 1;
            GlyphWidth = size * 2;
            GlyphHeight = size * 2;

            int bitmapWidth = GlyphsPerLine * GlyphWidth;
            int bitmapHeight = GlyphLineCount * GlyphHeight;

            System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(bitmapWidth, bitmapHeight, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            var collection = new PrivateFontCollection();
            collection.AddFontFile(path);
            Font font = new Font(collection.Families[0], size);

            var g = System.Drawing.Graphics.FromImage(bitmap);

            g.SmoothingMode = SmoothingMode.HighQuality;
            g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;

            for (int p = 0; p < GlyphLineCount; p++)
            {
                for (int n = 0; n < GlyphsPerLine; n++)
                {
                    char c = (char)(n + p * GlyphsPerLine);
                    g.DrawString(c.ToString(), font, Brushes.White,
                        n * GlyphWidth + AtlasOffsetX, p * GlyphHeight + AtlassOffsetY);
                }
            }

            alphabet = LoadFont(bitmap);

        }

        public static Texture2D LoadFont(System.Drawing.Bitmap bitmap)
        {
           
            int id = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, id);

            BitmapData Data = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height),
                ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, Data.Width, Data.Height,
                0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, Data.Scan0);

            bitmap.UnlockBits(Data);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Clamp);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Clamp);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Filter4Sgis);

            return new Texture2D(id, bitmap.Width, bitmap.Height);
        }

    }

    #endregion

}