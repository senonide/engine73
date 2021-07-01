using System.Diagnostics;

namespace Utils
{
    // Class that allows us to measure time and consult it
    public class Timer
    {

        private static Stopwatch timer;

        public static void Init(){
            timer = new Stopwatch();
            timer.Start();
        }

        public static void Stop(){
            timer.Reset();
        }

        public static void Reset(){
            timer.Restart();
        }

        public static long CurrentTimeMillis(){
            return timer.ElapsedMilliseconds;
        }

        
    }
}