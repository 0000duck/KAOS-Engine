
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KAOS.Utilities
{
    /// <summary>
    /// Provides accurate timing information which may later be used for any animation.
    /// </summary>
    public class AnimationTimer
    {
        [System.Security.SuppressUnmanagedCodeSecurity]
        [System.Runtime.InteropServices.DllImport("kernel32")]
        private static extern bool QueryPerformanceFrequency(ref long performanceFrequency);

        [System.Security.SuppressUnmanagedCodeSecurity]
        [System.Runtime.InteropServices.DllImport("kernel32")]
        private static extern bool QueryPerformanceCounter(ref long performanceCount);

        private readonly long _ticksPerSecond = 0;
        private const long PreviouslyElapsedTime = 0;

        public AnimationTimer()
        {
            QueryPerformanceFrequency(ref _ticksPerSecond);
            GetElapsedTime();
        }

        public float GetElapsedTime()
        {
            long time = 0;
            QueryPerformanceCounter(ref time);

            float elapsedTime = (float)(time - PreviouslyElapsedTime) / (float)_ticksPerSecond;

            return elapsedTime;
        }

    }
}
