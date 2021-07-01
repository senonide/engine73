using System;
using System.Collections.Generic;
using OpenTK.Input;
using System.IO;

using Input;

namespace Audio
{
    // This class defines the properties of an audio clip
    internal class Clip
    {
        // If true, when the audio clip ends, it will restart
        private bool loop;

        // This variable stores the information of the audio wave that is reproduced
        private NAudio.Wave.WaveFileReader wave;
        // This is the audio output device
        private NAudio.Wave.DirectSoundOut output;

        public bool Looping
        {
            get { return loop;  }
        }
        public NAudio.Wave.WaveFileReader Wave
        {
            get { return wave; }
        }
        public NAudio.Wave.DirectSoundOut Output
        {
            get { return output; }
        }

        /// <summary>
        /// Build an audio clip and play it
        /// </summary>
        /// <param name="wav"> path of the .wav file </param>
        /// <param name="loop"> true for looping the clip </param>
        public Clip(string wav, bool loop)
        {
            wave = new NAudio.Wave.WaveFileReader(File.OpenRead(wav));
            this.output = new NAudio.Wave.DirectSoundOut();
            this.loop = loop;
            if (loop)
            {
                LoopStream loopWave = new LoopStream(wave);
                this.output.Init(new NAudio.Wave.WaveChannel32(loopWave));
            } else
            {
                this.output.Init(new NAudio.Wave.WaveChannel32(wave));
            }
            this.Play();
        }

        public void Update()
        {
                        
        }

        /// <summary>
        /// Plays the clip if it's stop
        /// </summary>
        public void Play()
        {
            this.output.Play();
        }

        /// <summary>
        /// Stops the clip if it's playing
        /// </summary>
        public void Stop()
        {
            this.output.Pause();
        }

    }

    // This class defines the global audio handler for the game. Save all audio clips and manage them
    class AudioHandler
    {
        // Data structure in charge of storing all the clips that are playing in the game
        public static List<Clip> data;
        
        /// <summary>
        /// Initialize the AudioHandler variables
        /// </summary>
        public static void Init()
        {
            data = new List<Clip>();
        }

        /// <summary>
        /// Checks if the audio clips have finished and destroys them. This function is called in every frame of the game
        /// </summary>
        public static void Update()
        {
            for(int i = 0; i<data.Count; i++)
            {
                data[i].Update();
                if(data[i].Wave.CurrentTime.Equals(data[i].Wave.TotalTime) && !data[i].Looping)
                {
                    Console.WriteLine("Remove clip");
                    data[i].Wave.Close();
                    data.Remove(data[i]);
                }
            }
        }

        /// <summary>
        /// Loads an audio clip into memory
        /// </summary>
        /// <param name="path"> path of the .wav file </param>
        /// <param name="loop"> true for looping the clip </param>
        public static void Load(string path, bool loop)
        {
            data.Add(new Clip(path, loop));
        }

    }


}
