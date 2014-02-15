﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KAOS.Utilities
{
    public class PreciseTimer
    {
        [System.Security.SuppressUnmanagedCodeSecurity]
        [System.Runtime.InteropServices.DllImport("kernel32")]
        private static extern bool QueryPerformanceFrequency(ref long PerformanceFrequency);

        [System.Security.SuppressUnmanagedCodeSecurity]
        [System.Runtime.InteropServices.DllImport("kernel32")]
        private static extern bool QueryPerformanceCounter(ref long PerformanceCount);

        long m_ticksPerSecond = 0;
        long m_previouslyElapsedTime = 0;

        public PreciseTimer()
        {
            QueryPerformanceFrequency(ref m_ticksPerSecond);
            GetElapsedTime();
        }

        public float GetElapsedTime()
        {
            long time = 0;
            QueryPerformanceCounter(ref time);

            float elapsedTime = (float)(time - m_previouslyElapsedTime) / (float)m_ticksPerSecond;

            return elapsedTime;
        }

    }
}