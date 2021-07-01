using OpenTK;
using OpenTK.Graphics.OpenGL;

using Text;

namespace GraphicsRenderer
{
    // This is the class that is responsible for rendering images and figures in the game. Uses OpenGL calls and is in charge of creating OpenGL context
    public class Graphics
    {

        /// <summary>
        /// This function is in charge of creating the projection of the game world, it must be called after creating the OpenGL context.
        /// </summary>
        /// <param name="screenWidth"> screen width in number of pixels </param>
        /// <param name="screenHeight"> screen height in number of pixels </param>
        public static void Begin(int screenWidth, int screenHeight)
        {
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(-screenWidth / 2f, screenWidth / 2f, screenHeight / 2f, -screenHeight / 2f, 0f, 1f);
        }

        /*  Graphics.Draw([...]);
            
            1.- Variable that contains the texture <Texture2D>
            2.- Position respect from the center of the image <Vector2>
            3.- Size in pixels of the final texture <Vector2>
            4.- BackLight color that reflex in the image (for real colors use "White") <Color>
            5.- Origin --Unknown description-- (Size of the image plus .5 in each axis) <Vector2>
            */


        /// <summary>
        /// This function is responsible for drawing images on the screen. Overload 0
        /// </summary>
        /// <param name="texture"> Sprite that will be rendered </param>
        /// <param name="position"> Position of the sprite in the screen </param>
        /// <param name="scale"> Sprite scale. To increase or decrease the size of the sprites regardless of their resolution  </param>
        /// <param name="color"> Background color of the sprite. If you want to represent all the colors that this sprite contains, the color must be white </param>
        /// <param name="origin"> Sprite point from which its position on screen is calculated </param>
        /// <param name="angle"> Angle of rotation of the sprite </param>
        public static void Draw(Texture2D texture, Vector2 position, Vector2 scale, Color color, Vector2 origin, float angle){

            Vector2[] vertices = new Vector2[4] {
                new Vector2(0, 0),
                new Vector2(1, 0),
                new Vector2(1, 1),
                new Vector2(0, 1)
            };
            GL.PushMatrix();
            GL.BindTexture(TextureTarget.Texture2D, texture.Id);
            
            GL.Translate(position.X, position.Y, 0);
            GL.Rotate(angle, 0, 0, 1);

            GL.Begin(PrimitiveType.Quads);

            GL.Color4(color);

            for(int i = 0; i < 4; i++){
                
                GL.TexCoord2(vertices[i]);
                vertices[i].X *= texture.Width;
                vertices[i].Y *= texture.Height;
                vertices[i] -= origin;
                vertices[i] *= scale;
                //vertices[i] += position;
                GL.Vertex2(vertices[i]);
            }       
            GL.End();
            GL.PopMatrix();
        }

        /// <summary>
        /// This function is responsible for drawing images on the screen. Overload 1
        /// </summary>
        /// <param name="texture"> Sprite that will be rendered </param>
        /// <param name="position"> Position of the sprite in the screen </param>
        public static void Draw(Texture2D texture, Vector2 position)
        {
            Draw(texture, position, new Vector2(1, 1), Color.White, new Vector2(texture.Width / 2, texture.Height / 2), 0);
        }

        /// <summary>
        /// This function is responsible for drawing images on the screen. Overload 2
        /// </summary>
        /// <param name="texture"> Sprite that will be rendered </param>
        /// <param name="position"> Position of the sprite in the screen </param>
        /// <param name="angle"> Angle of rotation of the sprite </param>
        public static void Draw(Texture2D texture, Vector2 position, float angle)
        {
            Draw(texture, position, new Vector2(1, 1), Color.White, new Vector2(texture.Width / 2, texture.Height / 2), angle);
        }

        /// <summary>
        /// This function is responsible for drawing images on the screen. Overload 3
        /// </summary>
        /// <param name="texture"> Sprite that will be rendered </param>
        /// <param name="position"> Position of the sprite in the screen </param>
        /// <param name="color"> Background color of the sprite. If you want to represent all the colors that this sprite contains, the color must be white </param>
        public static void Draw(Texture2D texture, Vector2 position, Color color)
        {
            Draw(texture, position, new Vector2(1, 1), color, new Vector2(texture.Width / 2, texture.Height / 2), 0);
        }

        /// <summary>
        /// This function is responsible for drawing images on the screen. Overload 4
        /// </summary>
        /// <param name="texture"> Sprite that will be rendered </param>
        /// <param name="position"> Position of the sprite in the screen </param>
        /// <param name="scale"> Sprite scale. To increase or decrease the size of the sprites regardless of their resolution  </param>
        /// <param name="angle"> Angle of rotation of the sprite </param>
        public static void Draw(Texture2D texture, Vector2 position, Vector2 scale, float angle)
        {
            Draw(texture, position, scale, Color.White, new Vector2(texture.Width / 2, texture.Height / 2), angle);
        }

        /// <summary>
        /// This function is responsible for drawing images on the screen. Overload 5
        /// </summary>
        /// <param name="texture"> Sprite that will be rendered </param>
        /// <param name="position"> Position of the sprite in the screen </param>
        /// <param name="scale"> Sprite scale. To increase or decrease the size of the sprites regardless of their resolution  </param>
        /// <param name="origin"> Sprite point from which its position on screen is calculated </param>
        /// <param name="angle"> Angle of rotation of the sprite </param>
        public static void Draw(Texture2D texture, Vector2 position, Vector2 scale, Vector2 origin, float angle)
        {
            Draw(texture, position, scale, Color.White, origin, angle);
        }

        /// <summary>
        /// This function is responsible for drawing images on the screen. Overload 6
        /// </summary>
        /// <param name="texture"> Sprite that will be rendered </param>
        /// <param name="position"> Position of the sprite in the screen </param>
        /// <param name="scale"> Sprite scale. To increase or decrease the size of the sprites regardless of their resolution  </param>
        public static void Draw(Texture2D texture, Vector2 position, Vector2 scale)
        {
            Draw(texture, position, scale, Color.White, new Vector2(texture.Width / 2, texture.Height / 2), 0);
        }

        /// <summary>
        /// This function is responsible for drawing images on the screen. Overload 7
        /// </summary>
        /// <param name="texture"> Sprite that will be rendered </param>
        /// <param name="position"> Position of the sprite in the screen </param>
        /// <param name="scale"> Sprite scale. To increase or decrease the size of the sprites regardless of their resolution  </param>
        /// <param name="color"> Background color of the sprite. If you want to represent all the colors that this sprite contains, the color must be white </param>
        public static void Draw(Texture2D texture, Vector2 position, Vector2 scale, Color color)
        {
            Draw(texture, position, scale, color, new Vector2(texture.Width / 2, texture.Height / 2), 0);
        }

        /// <summary>
        /// Function in charge of drawing a quadrilateral with all its equal angles, 90 degrees. Overload 0
        /// </summary>
        /// <param name="position"> Position of the figure in the screen </param>
        /// <param name="size"> Size of the quadrilateral </param>
        /// <param name="color0"> Color with which the first corner of the quadrilateral begins to be painted </param>
        /// <param name="color1"> Color with which the second corner of the quadrilateral begins to be painted </param>
        /// <param name="color2"> Color with which the third corner of the quadrilateral begins to be painted </param>
        /// <param name="color3"> Color with which the fourth corner of the quadrilateral begins to be painted </param>
        public static void DrawPlane(Vector2 position, Vector2 size, Color color0, Color color1, Color color2, Color color3)
        {

            Vector2[] vertices = new Vector2[4] {
                new Vector2(0, 0),
                new Vector2(1, 0),
                new Vector2(1, 1),
                new Vector2(0, 1)
            };
            GL.PushMatrix();
            GL.BindTexture(TextureTarget.Texture2D, Assets.plane.Id);

            GL.Translate(position.X, position.Y, 0);
            GL.Rotate(0, 0, 0, 1);

            GL.Begin(PrimitiveType.Quads);

            GL.Color4(color0);            

            GL.TexCoord2(vertices[0]);
            vertices[0].X *= Assets.plane.Width;
            vertices[0].Y *= Assets.plane.Height;
            vertices[0] *= size;
            GL.Vertex2(vertices[0]);

            GL.Color4(color1);

            GL.TexCoord2(vertices[1]);
            vertices[1].X *= Assets.plane.Width;
            vertices[1].Y *= Assets.plane.Height;
            vertices[1] *= size;
            GL.Vertex2(vertices[1]);

            GL.Color4(color2);

            GL.TexCoord2(vertices[2]);
            vertices[2].X *= Assets.plane.Width;
            vertices[2].Y *= Assets.plane.Height;
            vertices[2] *= size;
            GL.Vertex2(vertices[2]);

            GL.Color4(color3);

            GL.TexCoord2(vertices[3]);
            vertices[3].X *= Assets.plane.Width;
            vertices[3].Y *= Assets.plane.Height;
            vertices[3] *= size;
            GL.Vertex2(vertices[3]);

            GL.End();
            GL.PopMatrix();
        }

        /// <summary>
        /// Function in charge of drawing a quadrilateral with all its equal angles, 90 degrees. Overload 1
        /// </summary>
        /// <param name="position"> Position of the figure in the screen </param>
        /// <param name="size"> Size of the quadrilateral </param>
        /// <param name="color"> Color with which the entire quadrilateral is painted </param>
        public static void DrawPlane(Vector2 position, Vector2 size, Color color)
        {
            DrawPlane(position, size, color, color, color, color);
        }

        /// <summary>
        /// Function that draws an irregular quadrilateral
        /// </summary>
        /// <param name="vertex0"> Position of the first vertex </param>
        /// <param name="vertex1"> Position of the second vertex </param>
        /// <param name="vertex2"> Position of the third vertex </param>
        /// <param name="vertex3"> Position of the fourth vertex </param>
        /// <param name="color"> Color with which the entire quadrilateral is painted </param>
        public static void DrawQuad(Vector2 vertex0, Vector2 vertex1, Vector2 vertex2, Vector2 vertex3, Color color)
        {

            Vector2[] vertices = new Vector2[4] {
                vertex0,
                vertex1,
                vertex2,
                vertex3
            };
            GL.PushMatrix();
            GL.BindTexture(TextureTarget.Texture2D, Assets.plane.Id);

            GL.Begin(PrimitiveType.Quads);

            GL.Color4(color);

            GL.TexCoord2(vertices[0]);
            vertices[0].X *= Assets.plane.Width;
            vertices[0].Y *= Assets.plane.Height;
            GL.Vertex2(vertices[0]);

            GL.TexCoord2(vertices[1]);
            vertices[1].X *= Assets.plane.Width;
            vertices[1].Y *= Assets.plane.Height;
            GL.Vertex2(vertices[1]);

            GL.TexCoord2(vertices[2]);
            vertices[2].X *= Assets.plane.Width;
            vertices[2].Y *= Assets.plane.Height;
            GL.Vertex2(vertices[2]);

            GL.TexCoord2(vertices[3]);
            vertices[3].X *= Assets.plane.Width;
            vertices[3].Y *= Assets.plane.Height;
            GL.Vertex2(vertices[3]);

            GL.End();
            GL.PopMatrix();
        }

        /// <summary>
        /// Function that renders a text string given a font of type .ttf
        /// </summary>
        /// <param name="text"> Text string to be rendered </param>
        /// <param name="font"> Text font with which it will be drawn </param>
        /// <param name="position">  Position of the first letter of the string  </param>
        /// <param name="color"> Rendered text color </param>
        public static void DrawText(string text, FontRenderer font, Vector2 position, Color color)
        {
            char idx;
            float u_step = (float)font.GlyphWidth / (float)font.alphabet.Width;
            float v_step = (float)font.GlyphHeight / (float)font.alphabet.Height;

            for (int i = 0; i<text.Length; i++)
            {
                idx = text[i];
                float u = (float)(idx % FontRenderer.GlyphsPerLine) * u_step;
                float v = (float)(idx / FontRenderer.GlyphsPerLine) * v_step;

                GL.PushMatrix();
                GL.BindTexture(TextureTarget.Texture2D, font.alphabet.Id);

                GL.Translate(position.X, position.Y, 0);

                GL.Begin(PrimitiveType.Quads);
                
                GL.Color4(color);

                GL.TexCoord2(u, v);
                GL.Vertex2(position.X, position.Y);
                GL.TexCoord2(u + u_step, v);
                GL.Vertex2((position.X + font.GlyphWidth), position.Y);
                GL.TexCoord2(u + u_step, v + v_step);
                GL.Vertex2((position.X + font.GlyphWidth), (position.Y + font.GlyphHeight));
                GL.TexCoord2(u, v + v_step);
                GL.Vertex2(position.X, (position.Y + font.GlyphHeight));

                position.X += font.CharXSpacing;

                GL.End();
                GL.PopMatrix();
            }

        }

    }
}
