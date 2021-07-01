using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace GraphicsRenderer
{
    // Enumeration that stores the types of movement transitions of the game camera
    public enum TweenType{
        Instant,
        Linear,
        QuadraticInOut,
        CubicInOut,
        QuarticOut,
        BounceOut
    }

    // Class in which the game camera is specified
    public class View
    {
        // Camera position in the game world
        private Vector2 position;

        
        // Rotation of the game camera in radians , positive values are in clockwise
        public double rotation;

        /* Actual zoom of the game camera
        No zoom = 1
        x2 zoom = 2 */
        public double zoom;

        // Transition related variables
        private Vector2 positionGoto, positionFrom;
        private TweenType tweenType;
        private int currentStep, tweenSteps;


        public Vector2 Position{
            get{
                return this.position;
            }
        }
        public Vector2 PositionGoto{
            get {
                return positionGoto;
            }
        }

        // Translator from screen position to game world position
        public Vector2 ToWorld(Vector2 input){
            input /= (float)zoom;
            Vector2 dX = new Vector2((float)Math.Cos(rotation), (float)Math.Sin(rotation));
            Vector2 dY = new Vector2((float)Math.Cos(rotation + MathHelper.PiOver2), (float)Math.Sin(rotation + MathHelper.PiOver2));

            return (this.position + dX * input.X + dY * input.Y);
        }


        public View(Vector2 startPosition, double startZoom = 1.0, double startRotation = 0.0){

            this.position = startPosition;
            this.zoom = startZoom;
            this.rotation = startRotation;

        }

        /// <summary>
        /// Function in charge of updating the position of the game camera and its zoom. This function must be called in every frame
        /// </summary>
        public void Update(){

            if(currentStep < tweenSteps){
                currentStep++;
                switch(tweenType){
                    case TweenType.Linear:
                        this.position = positionFrom + (positionGoto - positionFrom) * GetLinear((float)currentStep/tweenSteps);
                        break;
                    case TweenType.QuadraticInOut:
                        this.position = positionFrom + (positionGoto - positionFrom) * GetQuadraticInOut((float)currentStep/tweenSteps);
                        break;
                    case TweenType.CubicInOut:
                        this.position = positionFrom + (positionGoto - positionFrom) * GetCubicInOut((float)currentStep/tweenSteps);
                        break;
                    case TweenType.QuarticOut:
                        this.position = positionFrom + (positionGoto - positionFrom) * GetQuarticOut((float)currentStep/tweenSteps);
                        break;
                    case TweenType.BounceOut:
                        this.position = positionFrom + (positionGoto - positionFrom) * GetBounceOut((float)currentStep/tweenSteps);
                        break;
                    
                }
            } else {
                position = positionGoto;
            }

            if(zoom<0.1){
                zoom=0.1;
            }
            if(zoom > 1){
                zoom = 1;
            }

        }


        /// <summary>
        /// Sets a new position for the camera instantly. Overload 0
        /// </summary>
        /// <param name="newPosition"> New position to go </param>
        public void SetPosition(Vector2 newPosition){
            this.positionFrom = this.position;
            this.position = newPosition;
            this.positionGoto = newPosition;
            tweenType = TweenType.Instant;
            currentStep = 0;
            tweenSteps = 0;
        }


        /// <summary>
        /// Add sweet transitions to the movement of the camera. Overload 1
        /// </summary>
        /// <param name="newPosition"> New position to go </param>
        /// <param name="type"> Type of transition to display </param>
        /// <param name="numSteps"> Number of steps that the transition will take </param>
        public void SetPosition(Vector2 newPosition, TweenType type, int numSteps){
            this.positionFrom = this.position;
            this.position = newPosition;
            this.positionGoto = newPosition;
            tweenType = type;
            currentStep = 0;
            tweenSteps = numSteps;
        }

        public float GetLinear(float t){
            return t;
        }

        public float GetQuadraticInOut(float t){
            return (t*t)/((2 * t * t) - (2 * t) + 1);
        }

        public float GetCubicInOut(float t){
            return (t*t*t)/((3 * t * t) - (3 * t) + 1);
        }

        public float GetQuarticOut(float t){
            return -((t-1) * (t-1) * (t-1) * (t-1)) + 1;
        }

        public float GetBounceOut(float t){
            float p = 0.3f;
            return (float)Math.Pow(2, -10 * t) * (float)Math.Sin((t - p / 4) * (2 * Math.PI) / p) + 1;
        }

        public void ApplyTransform(){
            Matrix4 transform = Matrix4.Identity;

            transform = Matrix4.Mult(transform, Matrix4.CreateTranslation(-position.X, -position.Y, 0));
            transform = Matrix4.Mult(transform, Matrix4.CreateRotationZ(-(float)rotation));
            transform = Matrix4.Mult(transform, Matrix4.CreateScale((float)zoom, (float)zoom, 1.0f));

            GL.MultMatrix(ref transform);

        }



    }
}