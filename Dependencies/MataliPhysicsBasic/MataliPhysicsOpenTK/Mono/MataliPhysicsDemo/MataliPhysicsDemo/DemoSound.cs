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
    public class DemoListener
    {
        public Vector3 Position;
        public Vector3 TopDirection;
        public Vector3 FrontDirection;
    }

    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class DemoEmitter
    {
        public Vector3 Position;
        public Vector3 FrontDirection;
    }

    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class DemoSound
    {
        DemoSoundQueue soundQueue;
        DemoSoundGroup soundGroup;

        public PhysicsSound SoundData;
        public DemoListener Listener;
        public DemoEmitter Emitter;

        public Vector3 HitPosition;
        public Vector3 RollPosition;
        public Vector3 SlidePosition;
        public Vector3 BackgroundPosition;
        public Vector3 FrontDirection;

        public float HitVolume;
        public float RollVolume;
        public float SlideVolume;
        public float BackgroundVolume;

        bool enableHit;
        bool enableRoll;
        bool enableSlide;
        bool enableBackground;

        float hitRepeatTime;
        float rollMuteTime;
        float slideMuteTime;

        DemoSoundUnit[] hitSoundUnits;
        DemoSoundUnit rollSoundUnit;
        DemoSoundUnit slideSoundUnit;
        DemoSoundUnit backgroundSoundUnit;

        public DemoSoundGroup SoundGroup { get { return soundGroup; } }

        public DemoSound(DemoSoundQueue soundQueue, DemoSoundGroup soundGroup)
        {
            this.soundQueue = soundQueue;
            this.soundGroup = soundGroup;

            enableHit = soundGroup.EnableHit;
            enableRoll = soundGroup.EnableRoll;
            enableSlide = soundGroup.EnableSlide;
            enableBackground = soundGroup.EnableBackground;

            hitRepeatTime = 0.4f;

            if (enableHit)
            {
                hitSoundUnits = new DemoSoundUnit[soundGroup.HitCount];
                for (int i = 0; i < hitSoundUnits.Length; i++)
                    hitSoundUnits[i] = soundGroup.Hit.CreateSoundUnit();
            }

            if (enableRoll)
                rollSoundUnit = soundGroup.Roll.CreateSoundUnit();

            if (enableSlide)
                slideSoundUnit = soundGroup.Slide.CreateSoundUnit();

            if (enableBackground)
                backgroundSoundUnit = soundGroup.Background.CreateSoundUnit();
        }

        public void Update(float time)
        {
            if (enableHit)
                hitRepeatTime += time;

            if (enableRoll)
                rollMuteTime += time;

            if (enableSlide)
                slideMuteTime += time;
        }

        public void Start()
        {
            DemoSoundUnit hitSoundUnit;

            if (enableHit)
            {
                if (HitVolume != 0.0f && hitRepeatTime > soundGroup.MaxHitRepeatTime)
                {
                    hitSoundUnit = null;
                    for (int i = 0; i < hitSoundUnits.Length; i++)
                    {
                        hitSoundUnit = hitSoundUnits[i];
                        if (hitSoundUnit.State != ALSourceState.Playing)
                            break;
                    }

                    if (hitSoundUnit != null)
                    {
                        if ((hitSoundUnit.SourceHandle == 0) && (soundQueue.SourceCount > 0))
                            hitSoundUnit.SetSource(soundQueue.DequeueSource());

                        if (hitSoundUnit.SourceHandle != 0)
                        {
                            Emitter.Position = HitPosition;
                            Emitter.FrontDirection = FrontDirection;

                            hitRepeatTime = 0.0f;
                            hitSoundUnit.Volume = HitVolume;
                            hitSoundUnit.Pitch = SoundData.HitPitch;
                            hitSoundUnit.Start(Listener, Emitter);
                        }
                    }
                }
            }

            if (enableRoll)
            {
                if (RollVolume != 0.0f)
                {
                    if ((rollSoundUnit.SourceHandle == 0) && (soundQueue.SourceCount > 0))
                        rollSoundUnit.SetSource(soundQueue.DequeueSource());

                    if (rollSoundUnit.SourceHandle != 0)
                    {
                        Emitter.Position = RollPosition;
                        Emitter.FrontDirection = FrontDirection;

                        rollMuteTime = 0.0f;
                        rollSoundUnit.Volume = RollVolume;
                        rollSoundUnit.Pitch = SoundData.RollPitch;
                        rollSoundUnit.Start(Listener, Emitter);
                    }
                }
                else
                {
                    RollStop();
                }
            }

            if (enableSlide)
            {
                if (SlideVolume != 0.0f)
                {
                    if ((slideSoundUnit.SourceHandle == 0) && (soundQueue.SourceCount > 0))
                        slideSoundUnit.SetSource(soundQueue.DequeueSource());

                    if (slideSoundUnit.SourceHandle != 0)
                    {
                        Emitter.Position = SlidePosition;
                        Emitter.FrontDirection = FrontDirection;

                        slideMuteTime = 0.0f;
                        slideSoundUnit.Volume = SlideVolume;
                        slideSoundUnit.Pitch = SoundData.SlidePitch;
                        slideSoundUnit.Start(Listener, Emitter);
                    }
                }
                else
                {
                    SlideStop();
                }
            }

            if (enableBackground)
            {
                if (BackgroundVolume != 0.0f)
                {
                    if ((backgroundSoundUnit.SourceHandle == 0) && (soundQueue.SourceCount > 0))
                        backgroundSoundUnit.SetSource(soundQueue.DequeueSource());

                    if (backgroundSoundUnit.SourceHandle != 0)
                    {
                        Emitter.Position = BackgroundPosition;
                        Emitter.FrontDirection = FrontDirection;

                        backgroundSoundUnit.Volume = BackgroundVolume;
                        backgroundSoundUnit.Pitch = SoundData.BackgroundPitch;
                        backgroundSoundUnit.Start(Listener, Emitter);
                    }
                }
                else
                {
                    BackgroundStop();
                }
            }
        }

        public bool HitStop()
        {
            bool stop;
            DemoSoundUnit hitSoundUnit;

            stop = true;

            for (int i = 0; i < hitSoundUnits.Length; i++)
            {
                hitSoundUnit = hitSoundUnits[i];

                if (hitSoundUnit.State == ALSourceState.Playing)
                {
                    stop = false;
                    hitSoundUnit.Start();
                }
            }

            return stop;
        }

        public bool RollStop()
        {
            bool stop = true;

            if (rollSoundUnit.State == ALSourceState.Playing)
            {
                if (rollMuteTime < soundGroup.MinRollMuteTime)
                {
                    stop = false;
                    rollSoundUnit.Volume *= 0.5f;
                    rollSoundUnit.Start();
                }
                else
                {
                    rollSoundUnit.Volume = 0.0f;
                    rollSoundUnit.Stop();
                }
            }

            return stop;
        }

        public bool SlideStop()
        {
            bool stop = true;

            if (slideSoundUnit.State == ALSourceState.Playing)
            {
                if (slideMuteTime < soundGroup.MinSlideMuteTime)
                {
                    stop = false;
                    slideSoundUnit.Volume *= 0.5f;
                    slideSoundUnit.Start();
                }
                else
                {
                    slideSoundUnit.Volume = 0.0f;
                    slideSoundUnit.Stop();
                }
            }

            return stop;
        }

        public bool BackgroundStop()
        {
            if (backgroundSoundUnit.State == ALSourceState.Playing)
            {
                backgroundSoundUnit.Volume = 0.0f;
                backgroundSoundUnit.Stop();
            }

            return true;
        }

        public bool Stop()
        {
            bool stop = true;

            if (enableHit)
                if (!HitStop())
                    stop = false;

            if (enableRoll)
                if (!RollStop())
                    stop = false;

            if (enableSlide)
                if (!SlideStop())
                    stop = false;

            if (enableBackground)
                if (!BackgroundStop())
                    stop = false;

            return stop;
        }

        public void Clear()
        {
            DemoSoundUnit hitSoundUnit;

            SoundData = null;

            if (enableHit)
            {
                for (int i = 0; i < hitSoundUnits.Length; i++)
                {
                    hitSoundUnit = hitSoundUnits[i];

                    if (hitSoundUnit.State == ALSourceState.Playing)
                    {
                        hitSoundUnit.Volume = 0.0f;
                        hitSoundUnit.Stop();
                    }

                    if (hitSoundUnit.SourceHandle != 0)
                    {
                        soundQueue.EnqueueSource(hitSoundUnit.SourceHandle);
                        hitSoundUnit.SourceHandle = 0;
                    }
                }
            }

            if (enableRoll)
            {
                if (rollSoundUnit.State == ALSourceState.Playing)
                {
                    rollSoundUnit.Volume = 0.0f;
                    rollSoundUnit.Stop();
                }

                if (rollSoundUnit.SourceHandle != 0)
                {
                    soundQueue.EnqueueSource(rollSoundUnit.SourceHandle);
                    rollSoundUnit.SourceHandle = 0;
                }
            }

            if (enableSlide)
            {
                if (slideSoundUnit.State == ALSourceState.Playing)
                {
                    slideSoundUnit.Volume = 0.0f;
                    slideSoundUnit.Stop();
                }

                if (slideSoundUnit.SourceHandle != 0)
                {
                    soundQueue.EnqueueSource(slideSoundUnit.SourceHandle);
                    slideSoundUnit.SourceHandle = 0;
                }
            }

            if (enableBackground)
            {
                if (backgroundSoundUnit.State == ALSourceState.Playing)
                {
                    backgroundSoundUnit.Volume = 0.0f;
                    backgroundSoundUnit.Stop();
                }

                if (backgroundSoundUnit.SourceHandle != 0)
                {
                    soundQueue.EnqueueSource(backgroundSoundUnit.SourceHandle);
                    backgroundSoundUnit.SourceHandle = 0;
                }
            }
        }
    }
}
