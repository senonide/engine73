using System.Collections.Generic;
using OpenTK;
using OpenTK.Input;

namespace Input
{

    // Class that manages all inputs
    public class InputHandler
    {
        private static GamePadController gamePadController;
        
        // Keyboard keys
        private static List<Key> keysDown;
        private static List<Key> keysDownLast;

        // Mouse buttons
        private static List<MouseButton> mouseButtonsDown;
        private static List<MouseButton> mouseButtonsDownLast;

        // Mouse cursor position
        public static int MouseX;
        public static int MouseY;

        // Mouse wheel variable
        public static int MouseWheel;

        public static void Init(GameWindow window){

            MouseWheel = 0;

            keysDown = new List<Key>();
            keysDownLast = new List<Key>();
            mouseButtonsDown = new List<MouseButton>();
            mouseButtonsDownLast = new List<MouseButton>();

            gamePadController = new GamePadController();            

            window.KeyDown += KeyDownHandler;
            window.KeyUp += KeyUpHandler;
            window.MouseDown += MouseDownHandler;
            window.MouseUp += MouseUpHandler;
            window.MouseWheel += MouseWheelHandler;

        }

        static void KeyDownHandler(object sender, KeyboardKeyEventArgs e){
            keysDown.Add(e.Key);
        }

        static void KeyUpHandler(object sender, KeyboardKeyEventArgs e){

            while(keysDown.Contains(e.Key)){
                keysDown.Remove(e.Key);
            }

        }

        static void MouseDownHandler(object sender, MouseButtonEventArgs e){

            mouseButtonsDown.Add(e.Button);
            MouseX = e.X;
            MouseY = e.Y;

        }

        static void MouseUpHandler(object sender, MouseButtonEventArgs e){

            while(mouseButtonsDown.Contains(e.Button)){
                mouseButtonsDown.Remove(e.Button);
            }

        }

        static void MouseWheelHandler(object sender, MouseWheelEventArgs e){
            MouseWheel = e.Delta;
        }


        public static void Update(){
            keysDownLast = new List<Key>(keysDown);
            mouseButtonsDownLast = new List<MouseButton>(mouseButtonsDown);
            MouseWheel = 0;
        }

        public static void GpUpdate(){
            gamePadController.Update();
        }

        public static void Reset(){
            gamePadController.Reset();
        }

        public static bool KeyPress(Key key){
            return (keysDown.Contains(key) && !keysDownLast.Contains(key));
        }

        public static bool KeyRelease(Key key){
            return (!keysDown.Contains(key) && keysDownLast.Contains(key));
        }

        public static bool KeyDown(Key key){
            return keysDown.Contains(key);
        }

        public static bool MouseButtonPress(MouseButton button){
            return (mouseButtonsDown.Contains(button) && !mouseButtonsDownLast.Contains(button));
        }

        public static bool MouseButtonRelease(MouseButton button){
            return (!mouseButtonsDown.Contains(button) && mouseButtonsDownLast.Contains(button));
        }

        public static bool MouseButtonDown(MouseButton button){
            return mouseButtonsDown.Contains(button);
        }



    }
}