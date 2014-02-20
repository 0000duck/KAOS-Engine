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
    public class DemoSoundGroup
    {
        Demo demo;
        List<DemoSound> totalSounds;
        List<DemoSound> currentSounds;

        public float MaxHitRepeatTime;
        public float MinRollMuteTime;
        public float MinSlideMuteTime;

        public int HitCount;

        public bool EnableHit;
        public bool EnableRoll;
        public bool EnableSlide;
        public bool EnableBackground;

        public DemoSoundSample Hit;
        public DemoSoundSample Roll;
        public DemoSoundSample Slide;
        public DemoSoundSample Background;

        public DemoSoundGroup(Demo demo, int hitCount, DemoSoundSample hit, DemoSoundSample roll, DemoSoundSample slide, DemoSoundSample background)
        {
            this.demo = demo;

            totalSounds = new List<DemoSound>();
            currentSounds = new List<DemoSound>();

            MaxHitRepeatTime = 0.4f;
            MinRollMuteTime = 0.2f;
            MinSlideMuteTime = 0.2f;

            HitCount = hitCount;

            EnableHit = hit != null;
            EnableRoll = roll != null;
            EnableSlide = slide != null;
            EnableBackground = background != null;

            Hit = hit;
            Roll = roll;
            Slide = slide;
            Background = background;
        }

        public DemoSound GetSound(PhysicsSound soundData, DemoListener listener, DemoEmitter emitter)
        {
            int index;
            DemoSound sound;

            if (currentSounds.Count > 0)
            {
                index = currentSounds.Count - 1;
                sound = currentSounds[index];
                currentSounds.RemoveAt(index);
            }
            else
            {
                sound = new DemoSound(demo.SoundQueue, this);
                totalSounds.Add(sound);
            }

            sound.SoundData = soundData;
            sound.Listener = listener;
            sound.Emitter = emitter;

            return sound;
        }

        public void SetSound(DemoSound demoSound)
        {
            demoSound.Clear();
            currentSounds.Add(demoSound);
        }

        public void ClearAllSounds()
        {
            DemoSound sound;

            if (totalSounds.Count > 0)
            {
                currentSounds.Clear();
                for (int i = 0; i < totalSounds.Count; i++)
                {
                    sound = totalSounds[i];

                    if (sound.SoundData != null)
                        sound.SoundData.UserDataObj = null;

                    SetSound(sound);
                }
            }
        }
    }
}
