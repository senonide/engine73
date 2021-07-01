using System;
using OpenTK;
using OpenTK.Input;

using Engine73;
using Audio;
using GraphicsRenderer;
using Input;
using Text;

using App;

namespace States
{

    // This is the main state of the actual game. When everything has been loaded and initilized, the game will be looping here.
    public class MainState : State
    {

        private Player player;

        public MainState(){
            player = new Player(0, 200);
            AudioHandler.Load("assets/wave.wav", true);
        }

        public override void Update(){
                 
            // You can move the camera by clicking on the window with the right button
            if(InputHandler.MouseButtonPress(MouseButton.Left)){
                Vector2 pos = new Vector2(InputHandler.MouseX, InputHandler.MouseY);
                pos -= new Vector2(Engine.Width/2, Engine.Height/2);
                pos = Engine.view.ToWorld(pos);
                if (Engine.debug)
                {
                    Console.WriteLine("ScreenX = " + InputHandler.MouseX + "; ScreenY = " + InputHandler.MouseY);
                    Console.WriteLine("WorldX = " + pos.X + "; WorldY = " + pos.Y + ";");
                }
            } else if(InputHandler.MouseButtonPress(MouseButton.Right)){
                Vector2 pos = new Vector2(InputHandler.MouseX, InputHandler.MouseY);
                pos -= new Vector2(Engine.Width/2, Engine.Height/2);
                pos = Engine.view.ToWorld(pos);

                // You can play with different types of transitions 
                Engine.view.SetPosition(pos, TweenType.QuadraticInOut, 60);  
            }
            
            // You can modify the zoom of the camera
            if(InputHandler.MouseWheel!=0){
                Engine.view.zoom += InputHandler.MouseWheel*0.1;
            }

            player.Update();

        }

        public override void Render(){

            player.Render();
            

            TextHandler.RenderText("PRESS-ESC-TO-EXIT+++", new Vector2(-Engine.Width/2 + 10,-Engine.Height/2 + 15), Color.White, 2);
            
            Graphics.Draw(Assets.logo, new Vector2(Engine.Width / 2 - 70, Engine.Height / 2 - 70), new Vector2(3, 3), Color.White);
            
            Graphics.DrawText("Game Engine", Assets.font, new Vector2(-160, -180), Color.LightGray);
            Graphics.DrawText("Scroll to zoom the camera", Assets.font2, new Vector2(-230, -80), Color.LightBlue);
            Graphics.DrawText("Right click to move the camera to the clicked spot", Assets.font2, new Vector2(-230, -65), Color.LightBlue);
            Graphics.DrawText("Move the red square with W,A,S,D. You can also try using a GamePad", Assets.font2, new Vector2(-230, -50), Color.LightBlue);
            
            Graphics.DrawText("You're this -->", Assets.font2, new Vector2(-120, 97), Color.LightCoral);

            Graphics.DrawPlane(new Vector2(-300, -210), new Vector2(300, 2), new Color(255, 255, 255, 0), Color.White, Color.White, new Color(255, 255, 255, 0));
            Graphics.DrawPlane(new Vector2(0, -210), new Vector2(300, 2), Color.White, new Color(255, 255, 255, 0), new Color(255, 255, 255, 0), Color.White);

        }
    }
}