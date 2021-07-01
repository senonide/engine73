using Text;
using Utils;

namespace GraphicsRenderer
{
    // Class in charge of loading all the assets of the game. The loads are static and are loaded all when starting the application
    public class Assets
    {
        public static bool loaded = false;

        public static Texture2D plane;
        public static Texture2D logo;

        public static FontRenderer font;
        public static FontRenderer font2;

        /// <summary>
        /// This is the main function. It is responsible for loading all the game's assests consecutively. Must be called at game start
        /// </summary>
        public static void Init(){

            font = new FontRenderer("assets/fonts/RobotoMono/RobotoMono-Thin.ttf", 80);
            font2 = new FontRenderer("assets/fonts/RobotoMono/RobotoMono-Bold.ttf", 18);
            
            plane = Loader.LoadTexture("sprites/plane.png");
            
            logo = Loader.LoadTexture("sprites/Logo.png");
            
            loaded = true;
        }

        

    }
}
