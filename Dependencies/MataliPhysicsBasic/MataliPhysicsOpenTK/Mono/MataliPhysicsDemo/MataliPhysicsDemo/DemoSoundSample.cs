/*
    Matali Physics Demo
    Copyright (c) 2013 KOMIRES Sp. z o. o.
 */
using System;
using System.Collections.Generic;
using System.IO;
using OpenTK;
using OpenTK.Audio.OpenAL;
using Komires.MataliPhysics;

namespace MataliPhysicsDemo
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class DemoSoundSample
    {
        Demo demo;

        string dataFileDirectory;
        string dataFileExt;

        ContentWave soundSample;

        int sampleHandle;

        public string FileDirectory { get { return dataFileDirectory; } }
        public string FileExt { get { return dataFileExt; } }

        public DemoSoundSample(Demo demo, string fileDirectory, string fileExt)
        {
            this.demo = demo;

            dataFileDirectory = fileDirectory;
            dataFileExt = fileExt;

            soundSample = new ContentWave();

            sampleHandle = AL.GenBuffer();
        }

        public void Set(Stream stream)
        {
            ALFormat sampleFormat;

            if (!soundSample.LoadFromStream(stream))
                throw new Exception("File is not supported.");

            if (soundSample.Channels == 1)
            {
                if (soundSample.SampleBits == 8)
                    sampleFormat = ALFormat.Mono8;
                else
                    sampleFormat = ALFormat.Mono16;
            }
            else
            {
                if (soundSample.SampleBits == 8)
                    sampleFormat = ALFormat.Stereo8;
                else
                    sampleFormat = ALFormat.Stereo16;
            }

            AL.BufferData(sampleHandle, sampleFormat, soundSample.Data, soundSample.Data.Length, soundSample.SampleRate);
        }

        public DemoSoundUnit CreateSoundUnit()
        {
            return new DemoSoundUnit(demo, sampleHandle);
        }
    }
}
