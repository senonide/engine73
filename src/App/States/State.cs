namespace States
{
    public abstract class State
    {
        private static State currentState = null;

        public static State CurrentState{
            get {
                return currentState;
            }
        }

        public static void ChangeState(State newState){
            currentState = newState;
        }

        /// <summary>
        /// This function is called on every frame to update game related data
        /// </summary>
        public abstract void Update();

        /// <summary>
        /// This function is called on every frame to draw objects in the OpenGL context
        /// </summary>
        public abstract void Render();

    }
}