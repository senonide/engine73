using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

using Audio;
using GraphicsRenderer;
using Input;
using Text;
using Utils;
using States;

namespace Engine73
{
    /// <summary>
    /// Main class of the game that creates the basic parameters of operation
    /// </summary>
    class Engine
    {
        private static GameWindow Window;
        public static DisplayDevice device = DisplayDevice.Default;
        private static GraphicsMode graphicsMode;
        private static GameWindowFlags flags;
        public static int Width;
        public static int Height;

        public static int MaxWidth = device.Width;
        public static int MaxHeight = device.Height;
        private readonly string Title = "Engine 73";
        private readonly int UPS = 60;
        private readonly int FPS = 60;

        public static bool debug;
        public static bool mute;
        public static bool antiAlias;

        public static View view;

        /// <summary>
        /// Load initial game settings
        /// </summary>
        private static void LoadConfig()
        {
            Width = 1280;
            Height = 720;
            flags = GameWindowFlags.FixedWindow;
            if (flags.Equals(GameWindowFlags.Fullscreen))
            {
                Width = MaxWidth;
                Height = MaxHeight;
            }
            antiAlias = true;
            if (antiAlias)
            {
                graphicsMode = new GraphicsMode(32, 24, 0, 8);
            }
            else
            {
                graphicsMode = GraphicsMode.Default;
            }
            mute = false;
            debug = true;
        }

        /// <summary>
        /// Builder of the game environment. Create window and initialize it also create an Opengl context
        /// </summary>
        public Engine()
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("#########################################");
            Console.WriteLine("##                                     ##");
            Console.WriteLine("##            Engine73 0.0.1           ##");
            Console.WriteLine("##                                     ##");
            Console.WriteLine("#########################################\n\n");

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("Loading configuration                        ");


            LoadConfig();
            
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Success\n");

            //Window setup
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("Generating an OpenGL context                 ");
            Window = new GameWindow(Width, Height, graphicsMode, Title, flags);
            Window.UpdateFrame += Update;
            Window.RenderFrame += Render;
            Window.Unload += OnClose;


            GL.ClearColor(Color4.Black);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
            GL.Enable(EnableCap.Blend);
            GL.Enable(EnableCap.Texture2D);
            GL.Enable(EnableCap.LineSmooth);

            GL.Hint(HintTarget.LineSmoothHint, HintMode.Nicest);
            GL.Hint(HintTarget.PolygonSmoothHint, HintMode.Nicest);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Success\n\n");

            Init(Window);

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("\n\nRunning...");
            Window.Run(UPS, FPS);
        }

        /// <summary>
        /// Initialize the game services and creates the first State
        /// </summary>
        /// <param name="Window"></param>
        private static void Init(GameWindow Window)
        {
            if (!mute)
            {
                AudioHandler.Init();
            }
            Assets.Init();
            TextHandler.Init();
            InputHandler.Init(Window);
            Timer.Init();
            State.ChangeState(new MainState());
            view = new View(Vector2.Zero, 1.0, 0.0);
        }


        /// <summary>
        /// Function that is executed in each frame (UPS)
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="args"></param>
        private void Update(object obj, FrameEventArgs args)
        {
            InputHandler.Reset();
            InputHandler.GpUpdate();

            if (InputHandler.KeyPress(Key.Escape))
                Window.Close();

            //------------------------  

            State.CurrentState.Update();

            //------------------------

            //view.position.X -= 0.1f;
            view.Update();
            AudioHandler.Update();
            InputHandler.Update();
        }

        /// <summary>
        /// Function that is executed in each frame (FPS)
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="args"></param>
        private void Render(object obj, FrameEventArgs args)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);

            Graphics.Begin(Width, Height);
            view.ApplyTransform();

            //------------------------  

            State.CurrentState.Render();

            //------------------------

            Window.SwapBuffers();
        }

        /// <summary>
        /// Function that is executed when closing the application
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="args"></param>
        private void OnClose(object obj, EventArgs args)
        {
            Console.WriteLine(((double)Timer.CurrentTimeMillis()) / 1000.0 + " seconds");
            Timer.Stop();
        }
    }
}
