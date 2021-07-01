using System;
using System.Collections.Generic;
using OpenTK.Input;

namespace Input
{
    // Class that specifies the controller of the Gamepads and joysticks that are connected
    public class GamePadController
    {
        // Number of conected GamePads
        public static int playersNumber;

        // Different states of the connected Joysticks. 4 joysticks maximum
        private JoystickState JState1;
        private JoystickState JState2;
        private JoystickState JState3;
        private JoystickState JState4;

        // Indexed gamepad buttons
        private static readonly int A = 1;
        private static readonly int B = 2;
        private static readonly int X = 0;
        private static readonly int Y = 3;
        private static readonly int START = 9;
        private static readonly int SELECT = 8;
        private static readonly int R = 6;
        private static readonly int L = 4;
        private static readonly int axisId = -1;

        // Data structure in charge of relating the names of the buttons of a Gamepad with their representation in memory
        private static Dictionary<ButtonName, GamePadButton> GpButtons;


        /// <summary>
        /// Gampadcontroller builder.Initialize the data structures and collect the states of the joysticks
        /// Registers the number of remote controls that are connected
        /// </summary>
        public GamePadController(){

            JoystickCapabilities capabilities1 = Joystick.GetCapabilities(0);
            JoystickCapabilities capabilities2 = Joystick.GetCapabilities(1);
            JoystickCapabilities capabilities3 = Joystick.GetCapabilities(2);
            JoystickCapabilities capabilities4 = Joystick.GetCapabilities(3);

            if (capabilities1.IsConnected)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("GamePad 1 is connected");
                playersNumber = 1;
                if (capabilities2.IsConnected)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("GamePad 2 is connected");
                    playersNumber++;
                }
                if (capabilities3.IsConnected)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("GamePad 3 is connected");
                    playersNumber++;
                }
                if (capabilities4.IsConnected)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("GamePad 4 is connected");
                    playersNumber++;
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No GamePad detected");
                playersNumber = 0;
            }

            GpButtons = new Dictionary<ButtonName, GamePadButton>();
            GpButtons.Add(ButtonName.A, new GamePadButton(ButtonName.A, A));
            GpButtons.Add(ButtonName.B, new GamePadButton(ButtonName.B, B));
            GpButtons.Add(ButtonName.X, new GamePadButton(ButtonName.X, X));
            GpButtons.Add(ButtonName.Y, new GamePadButton(ButtonName.Y, Y));
            GpButtons.Add(ButtonName.START, new GamePadButton(ButtonName.START, START));
            GpButtons.Add(ButtonName.SELECT, new GamePadButton(ButtonName.SELECT, SELECT));
            GpButtons.Add(ButtonName.R, new GamePadButton(ButtonName.R, R));
            GpButtons.Add(ButtonName.L, new GamePadButton(ButtonName.L, L));
            GpButtons.Add(ButtonName.UP, new GamePadButton(ButtonName.UP, axisId));
            GpButtons.Add(ButtonName.DOWN, new GamePadButton(ButtonName.DOWN, axisId));
            GpButtons.Add(ButtonName.RIGHT, new GamePadButton(ButtonName.RIGHT, axisId));
            GpButtons.Add(ButtonName.LEFT, new GamePadButton(ButtonName.LEFT, axisId));
        }

        /// <summary>
        /// Tracks the Gampad buttons of player number 1
        /// </summary>
        private void OnePlayerHandler(){

            JState1 = Joystick.GetState(0);

            GpButtons.GetValueOrDefault(ButtonName.A).DownHandler(JState1);
            GpButtons.GetValueOrDefault(ButtonName.B).DownHandler(JState1);
            GpButtons.GetValueOrDefault(ButtonName.X).DownHandler(JState1);
            GpButtons.GetValueOrDefault(ButtonName.Y).DownHandler(JState1);
            GpButtons.GetValueOrDefault(ButtonName.START).DownHandler(JState1);
            GpButtons.GetValueOrDefault(ButtonName.SELECT).DownHandler(JState1);
            GpButtons.GetValueOrDefault(ButtonName.R).DownHandler(JState1);
            GpButtons.GetValueOrDefault(ButtonName.L).DownHandler(JState1);
            GpButtons.GetValueOrDefault(ButtonName.UP).AxisHandler(JState1);
            GpButtons.GetValueOrDefault(ButtonName.DOWN).AxisHandler(JState1);
            GpButtons.GetValueOrDefault(ButtonName.RIGHT).AxisHandler(JState1);
            GpButtons.GetValueOrDefault(ButtonName.LEFT).AxisHandler(JState1);        
            
        }

        /// <summary>
        /// Resets all the buttons states
        /// </summary>
        public void Reset(){
            GpButtons.GetValueOrDefault(ButtonName.A).Reset();
            GpButtons.GetValueOrDefault(ButtonName.B).Reset();
            GpButtons.GetValueOrDefault(ButtonName.X).Reset();
            GpButtons.GetValueOrDefault(ButtonName.Y).Reset();
            GpButtons.GetValueOrDefault(ButtonName.START).Reset();
            GpButtons.GetValueOrDefault(ButtonName.SELECT).Reset();
            GpButtons.GetValueOrDefault(ButtonName.R).Reset();
            GpButtons.GetValueOrDefault(ButtonName.L).Reset();
            GpButtons.GetValueOrDefault(ButtonName.UP).Reset();
            GpButtons.GetValueOrDefault(ButtonName.DOWN).Reset();
            GpButtons.GetValueOrDefault(ButtonName.RIGHT).Reset();
            GpButtons.GetValueOrDefault(ButtonName.LEFT).Reset();
        }

        /// <summary>
        /// Detects if the given button is down 
        /// </summary>
        /// <param name="name"> Name of the button </param>
        /// <returns></returns>
        public static bool GpButtonDown(ButtonName name){
            return GpButtons.GetValueOrDefault(name).Down;
        }

        /// <summary>
        /// Detects if the given button was pressed
        /// </summary>
        /// <param name="name"> Name of the button </param>
        /// <returns></returns>
        public static bool GpButtonPress(ButtonName name){
            return GpButtons.GetValueOrDefault(name).Pressed;
        }

        public void Update(){
            switch(playersNumber){
                case 0:
                    break;
                case 1:
                    OnePlayerHandler();                  
                    break;
                case 2:
                    TwoPlayerHandler();
                    break;
                case 3:
                    ThreePlayerHandler();
                    break;
                case 4:
                    FourPlayerHandler();
                    break; 
            }

        }

        /// <summary>
        ///  Tracks the Gamepad buttons of player number 2. NOT SUPPORTED 
        /// </summary>
        private void TwoPlayerHandler(){
            JState1 = Joystick.GetState(0);
            JState2 = Joystick.GetState(1);
        }

        /// <summary>
        ///  Tracks the Gaempad buttons of player number 3. NOT SUPPORTED 
        /// </summary>
        private void ThreePlayerHandler(){
            JState1 = Joystick.GetState(0);
            JState2 = Joystick.GetState(1);
            JState3 = Joystick.GetState(2);
        }

        /// <summary>
        ///  Tracks the Gamepad buttons of player number 4. NOT SUPPORTED 
        /// </summary>
        private void FourPlayerHandler(){
            JState1 = Joystick.GetState(0);
            JState2 = Joystick.GetState(1);
            JState3 = Joystick.GetState(2);
            JState4 = Joystick.GetState(3);
        }

    }

    // Enumeration that specifies the name of the Gamepad
    public enum ButtonName{
        A,B,X,Y,START,SELECT,R,L,UP,DOWN,RIGHT,LEFT
    }

    // Class that specifies the object of a Gamepad Button
    public class GamePadButton{

        public ButtonName Name;
        public int Id;
        public bool Pressed;
        public bool Down;
        public bool DownLast;

        /// <summary>
        /// Gamepad button builder
        /// </summary>
        /// <param name="name"> Name of the button </param>
        /// <param name="id"> index of the button. This must be the index that represents the correct button in the Gamepad </param>
        public GamePadButton(ButtonName name, int id){
            this.Name = name;
            this.Id = id;
            this.Pressed = false;
            this.Down = false;
            this.DownLast = false;
        }

        public void Reset(){
            this.Pressed = false;           
        }

        public void DownHandler(JoystickState js){
            if(js.GetButton(this.Id).Equals(ButtonState.Pressed)){
                if(!this.DownLast){
                    this.Pressed = true;
                }
                this.Down = true;
                this.DownLast = true;
            } else {
                this.DownLast = false;
                this.Down = false;
            }
            
        }

        public void AxisHandler(JoystickState js){
            if(this.Name.Equals(ButtonName.UP) || this.Name.Equals(ButtonName.DOWN)){
                float ax = js.GetAxis(1);
                if((ax > 0.1f && this.Name.Equals(ButtonName.DOWN)) || (ax < -0.1f && this.Name.Equals(ButtonName.UP))){
                    if(!this.DownLast){
                        this.Pressed = true;
                    }
                    this.Down = true;
                    this.DownLast = true;
                } else {
                    this.DownLast = false;
                    this.Down = false;
                }

            } else if(this.Name.Equals(ButtonName.RIGHT) || this.Name.Equals(ButtonName.LEFT)){
                float ax = js.GetAxis(0);
                if((ax > 0.1f && this.Name.Equals(ButtonName.RIGHT)) || (ax < -0.1f && this.Name.Equals(ButtonName.LEFT))){
                    if(!this.DownLast){
                        this.Pressed = true;
                    }
                    this.Down = true;
                    this.DownLast = true;
                } else {
                    this.DownLast = false;
                    this.Down = false;
                }
            } else {
                Console.WriteLine("Axis handler error");
            }
        }

        

    }

}