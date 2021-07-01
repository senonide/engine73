using OpenTK;

using Utils;

namespace GraphicsRenderer
{
    // This class deals with managing the animations of the game. To create an animated render, your sprites are loaded into an object of this class directly
    public class Animator
    {
        // Index of the current rendered sprite
        public int index;
        // True if the animation is running
        private bool running;
        // True if the animation should be looping
        private bool loop;

        // Array of  sprites that make up the animation
        private Texture2D[] frames;
        // Speed ​​of transition of sprites in the animation
        private int velocity;
        // Position of the animated set in the game
        public Vector2 position;

        // Time control variables in animations
        private long time;
        private long lastTime;

        /// <summary>
        /// Builder of an animated game object
        /// </summary>
        /// <param name="frames">  Array of  sprites that make up the animation </param>
        /// <param name="velocity"> Speed ​​of transition of sprites in the animation </param>
        /// <param name="position"> Position of the animated set in the game </param>
        /// <param name="loop"> True if the animation should be looping </param>
        public Animator(Texture2D[] frames, int velocity, Vector2 position, bool loop){
            this.frames = frames;
            this.velocity = velocity;
            this.position = position;
            this.loop = loop;
            index = 0;
            running = true;
            time = 0;	
            lastTime = Timer.CurrentTimeMillis();

        }

        /// <summary>
        /// Copy builder
        /// </summary>
        /// <param name="a"> Animator object to replicate </param>
        public Animator(Animator a) {
            this.frames = a.frames;
            this.velocity = a.velocity;
            this.position = a.position;
            index = 0;
            running = true;
            time = 0;
            lastTime = Timer.CurrentTimeMillis();
        }

        public Vector2 Position {
            get {
                return position;
            }
        }

        public int Size {
            get {
                return frames.Length;
            }
        }

        public bool isRunning {
            get {
                return running;
            }
        }

        public Texture2D CurrentFrame {
            get {
                return frames[index];
            }
        }


        /// <summary>
        /// Updates the current frame that the game is rendering 
        /// </summary>
        public void Update() {
            time = time + Timer.CurrentTimeMillis() - lastTime;
            lastTime = Timer.CurrentTimeMillis();
            
            if(time>velocity) {
                time = 0;
                index = index + 1;
                if(index>=frames.Length) {
                    if(!loop){
                        running = false;
                    } else {
                        index = 0;
                    }
                }
            }
        }
        
    }
}