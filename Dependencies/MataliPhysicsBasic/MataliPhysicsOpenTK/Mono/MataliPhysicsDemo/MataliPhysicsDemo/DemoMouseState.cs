/*
    Matali Physics Demo
    Copyright (c) 2013 KOMIRES Sp. z o. o.
 */
using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using Komires.MataliPhysics;

namespace MataliPhysicsDemo
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public struct DemoMouseState
    {
        long buttonState0;
        long buttonState1;
        long buttonState2;
        long buttonState3;
        long buttonState4;
        long buttonState5;
        long buttonState6;
        long buttonState7;
        long buttonState8;
        long buttonState9;
        long buttonState10;
        long buttonState11;
        long buttonState12;
        long buttonState13;
        long buttonState14;
        long buttonState15;

        int positionX;
        int positionY;
        int scrollWheel;

        public int X { get { return positionX; } }
        public int Y { get { return positionY; } }
        public int Wheel { get { return scrollWheel; } }

        public bool this[MouseButton button]
        {
            get
            {
                int intButton = (int)button;
                int state = intButton & 15;
                int value = intButton >> 4;
                long bit = 1;
                bit <<= value;

                switch (state)
                {
                    case 0:
                        return ((buttonState0 & bit) != 0);
                    case 1:
                        return ((buttonState1 & bit) != 0);
                    case 2:
                        return ((buttonState2 & bit) != 0);
                    case 3:
                        return ((buttonState3 & bit) != 0);
                    case 4:
                        return ((buttonState4 & bit) != 0);
                    case 5:
                        return ((buttonState5 & bit) != 0);
                    case 6:
                        return ((buttonState6 & bit) != 0);
                    case 7:
                        return ((buttonState7 & bit) != 0);
                    case 8:
                        return ((buttonState8 & bit) != 0);
                    case 9:
                        return ((buttonState9 & bit) != 0);
                    case 10:
                        return ((buttonState10 & bit) != 0);
                    case 11:
                        return ((buttonState11 & bit) != 0);
                    case 12:
                        return ((buttonState12 & bit) != 0);
                    case 13:
                        return ((buttonState13 & bit) != 0);
                    case 14:
                        return ((buttonState14 & bit) != 0);
                    case 15:
                        return ((buttonState15 & bit) != 0);
                }

                return false;
            }
        }

        public void Set(MouseButton button, bool buttonState)
        {
            int intButton = (int)button;
            int state = intButton & 15;
            int value = intButton >> 4;
            long bit = 1;
            bit <<= value;

            long bitSet = (buttonState) ? bit : 0;
            long bitClear = bit ^ (long)-1;

            switch (state)
            {
                case 0:
                    buttonState0 = (buttonState0 & bitClear) | bitSet;
                    break;
                case 1:
                    buttonState1 = (buttonState1 & bitClear) | bitSet;
                    break;
                case 2:
                    buttonState2 = (buttonState2 & bitClear) | bitSet;
                    break;
                case 3:
                    buttonState3 = (buttonState3 & bitClear) | bitSet;
                    break;
                case 4:
                    buttonState4 = (buttonState4 & bitClear) | bitSet;
                    break;
                case 5:
                    buttonState5 = (buttonState5 & bitClear) | bitSet;
                    break;
                case 6:
                    buttonState6 = (buttonState6 & bitClear) | bitSet;
                    break;
                case 7:
                    buttonState7 = (buttonState7 & bitClear) | bitSet;
                    break;
                case 8:
                    buttonState8 = (buttonState8 & bitClear) | bitSet;
                    break;
                case 9:
                    buttonState9 = (buttonState9 & bitClear) | bitSet;
                    break;
                case 10:
                    buttonState10 = (buttonState10 & bitClear) | bitSet;
                    break;
                case 11:
                    buttonState11 = (buttonState11 & bitClear) | bitSet;
                    break;
                case 12:
                    buttonState12 = (buttonState12 & bitClear) | bitSet;
                    break;
                case 13:
                    buttonState13 = (buttonState13 & bitClear) | bitSet;
                    break;
                case 14:
                    buttonState14 = (buttonState14 & bitClear) | bitSet;
                    break;
                case 15:
                    buttonState15 = (buttonState15 & bitClear) | bitSet;
                    break;
            }
        }

        public void Set(int x, int y)
        {
            positionX = x;
            positionY = y;
        }

        public void Set(int wheel)
        {
            scrollWheel = wheel;
        }
    }
}
