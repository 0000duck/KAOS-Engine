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
    public class DemoSoundUnit
    {
        Demo demo;

        int sampleHandle;
        int sourceHandle;

        float volume;
        float pitch;

        public float Volume { get { return volume; } set { volume = value; AL.Source(sourceHandle, ALSourcef.Gain, volume); } }
        public ALSourceState State { get { return AL.GetSourceState(sourceHandle); } }
        public float Pitch { get { return pitch; } set { pitch = value; AL.Source(sourceHandle, ALSourcef.Pitch, Math.Min(pitch + 1.5f, 2.0f)); } }
        public int SourceHandle { get { return sourceHandle; } set { sourceHandle = value; } }

        public DemoSoundUnit(Demo demo, int sampleHandle)
        {
            this.demo = demo;
            this.sampleHandle = sampleHandle;
        }

        public void SetSource(int sourceHandle)
        {
            this.sourceHandle = sourceHandle;
            AL.Source(sourceHandle, ALSourcei.Buffer, sampleHandle);
        }

        public void Start(DemoListener listener, DemoEmitter emitter)
        {
            AL.Listener(ALListener3f.Position, ref listener.Position);
            AL.Listener(ALListenerfv.Orientation, ref listener.FrontDirection, ref listener.TopDirection);
            AL.Source(sourceHandle, ALSource3f.Position, ref emitter.Position);
            AL.Source(sourceHandle, ALSource3f.Direction, ref emitter.FrontDirection);

            if (AL.GetSourceState(sourceHandle) != ALSourceState.Playing)
                AL.SourcePlay(sourceHandle);
        }

        public void Start()
        {
            if (AL.GetSourceState(sourceHandle) != ALSourceState.Playing)
                AL.SourcePlay(sourceHandle);
        }

        public void Stop()
        {
            AL.SourceStop(sourceHandle);
        }
    }
}
