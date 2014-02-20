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
    public struct DemoKeyboardState
    {
        long keyState0;
        long keyState1;
        long keyState2;
        long keyState3;
        long keyState4;
        long keyState5;
        long keyState6;
        long keyState7;
        long keyState8;
        long keyState9;
        long keyState10;
        long keyState11;
        long keyState12;
        long keyState13;
        long keyState14;
        long keyState15;

        public bool this[Key key]
        {
            get
            {
                int intKey = (int)key;
                int state = intKey & 15;
                int value = intKey >> 4;
                long bit = 1;
                bit <<= value;

                switch (state)
                {
                    case 0:
                        return ((keyState0 & bit) != 0);
                    case 1:
                        return ((keyState1 & bit) != 0);
                    case 2:
                        return ((keyState2 & bit) != 0);
                    case 3:
                        return ((keyState3 & bit) != 0);
                    case 4:
                        return ((keyState4 & bit) != 0);
                    case 5:
                        return ((keyState5 & bit) != 0);
                    case 6:
                        return ((keyState6 & bit) != 0);
                    case 7:
                        return ((keyState7 & bit) != 0);
                    case 8:
                        return ((keyState8 & bit) != 0);
                    case 9:
                        return ((keyState9 & bit) != 0);
                    case 10:
                        return ((keyState10 & bit) != 0);
                    case 11:
                        return ((keyState11 & bit) != 0);
                    case 12:
                        return ((keyState12 & bit) != 0);
                    case 13:
                        return ((keyState13 & bit) != 0);
                    case 14:
                        return ((keyState14 & bit) != 0);
                    case 15:
                        return ((keyState15 & bit) != 0);
                }

                return false;
            }
        }

        public void Set(Key key, bool keyState)
        {
            int intKey = (int)key;
            int state = intKey & 15;
            int value = intKey >> 4;
            long bit = 1;
            bit <<= value;

            long bitSet = (keyState) ? bit : 0;
            long bitClear = bit ^ (long)-1;

            switch (state)
            {
                case 0:
                    keyState0 = (keyState0 & bitClear) | bitSet;
                    break;
                case 1:
                    keyState1 = (keyState1 & bitClear) | bitSet;
                    break;
                case 2:
                    keyState2 = (keyState2 & bitClear) | bitSet;
                    break;
                case 3:
                    keyState3 = (keyState3 & bitClear) | bitSet;
                    break;
                case 4:
                    keyState4 = (keyState4 & bitClear) | bitSet;
                    break;
                case 5:
                    keyState5 = (keyState5 & bitClear) | bitSet;
                    break;
                case 6:
                    keyState6 = (keyState6 & bitClear) | bitSet;
                    break;
                case 7:
                    keyState7 = (keyState7 & bitClear) | bitSet;
                    break;
                case 8:
                    keyState8 = (keyState8 & bitClear) | bitSet;
                    break;
                case 9:
                    keyState9 = (keyState9 & bitClear) | bitSet;
                    break;
                case 10:
                    keyState10 = (keyState10 & bitClear) | bitSet;
                    break;
                case 11:
                    keyState11 = (keyState11 & bitClear) | bitSet;
                    break;
                case 12:
                    keyState12 = (keyState12 & bitClear) | bitSet;
                    break;
                case 13:
                    keyState13 = (keyState13 & bitClear) | bitSet;
                    break;
                case 14:
                    keyState14 = (keyState14 & bitClear) | bitSet;
                    break;
                case 15:
                    keyState15 = (keyState15 & bitClear) | bitSet;
                    break;
            }
        }
    }
}
