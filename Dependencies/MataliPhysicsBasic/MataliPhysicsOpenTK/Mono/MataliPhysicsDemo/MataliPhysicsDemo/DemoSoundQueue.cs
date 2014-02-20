/*
    Matali Physics Demo
    Copyright (c) 2013 KOMIRES Sp. z o. o.
 */
using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Audio.OpenAL;
using Komires.MataliPhysics;

namespace MataliPhysicsDemo
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class DemoSoundQueue
    {
        Queue<DemoSound> sounds;
        Queue<int> sources;

        int maxSoundCount;
        int maxSourceCount;

        public int SoundCount { get { return sounds.Count; } }
        public int SourceCount { get { return sources.Count; } }
        public int MaxSoundCount { get { return maxSoundCount; } }
        public int MaxSourceCount { get { return maxSourceCount; } }

        public DemoSoundQueue(int maxSoundCount)
        {
            this.maxSoundCount = maxSoundCount;

            sounds = new Queue<DemoSound>();
            sources = new Queue<int>();
        }

        public void CreateSources(int maxSourceCount)
        {
            int source;

            for (int i = 0; i < maxSourceCount; i++)
            {
                source = AL.GenSource();

                if (source == 0)
                    break;

                sources.Enqueue(source);
            }

            this.maxSourceCount = sources.Count;
        }

        public void EnqueueSound(DemoSound sound)
        {
            sounds.Enqueue(sound);
        }

        public DemoSound DequeueSound()
        {
            return sounds.Dequeue();
        }

        public void EnqueueSource(int source)
        {
            sources.Enqueue(source);
        }

        public int DequeueSource()
        {
            return sources.Dequeue();
        }

        public void Clear()
        {
            if (sounds.Count > 0)
                sounds.Clear();
        }
    }
}
