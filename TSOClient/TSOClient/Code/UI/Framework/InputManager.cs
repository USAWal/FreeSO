﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using TSOClient.Code.UI.Model;
using System.Runtime.InteropServices;

namespace TSOClient.Code.UI.Framework
{
    public class InputManager
    {
        private IFocusableUI LastFocus;

        public void SetFocus(IFocusableUI ui)
        {
            /** No change **/
            if (ui == LastFocus) { return; }

            if (LastFocus != null)
            {
                LastFocus.OnFocusChanged(FocusEvent.FocusOut);
            }

            LastFocus = ui;
            if (ui != null)
            {
                LastFocus.OnFocusChanged(FocusEvent.FocusIn);
            }
        }


        /**D0 = 48,
        D1 = 49,
        D2 = 50,
        D3 = 51,
        D4 = 52,
        D5 = 53,
        D6 = 54,
        D7 = 55,
        D8 = 56,
        D9 = 57,
        A = 65,
        B = 66,
        C = 67,
        D = 68,
        E = 69,
        F = 70,
        G = 71,
        H = 72,
        I = 73,
        J = 74,
        K = 75,
        L = 76,
        M = 77,
        N = 78,
        O = 79,
        P = 80,
        Q = 81,
        R = 82,
        S = 83,
        T = 84,
        U = 85,
        V = 86,
        W = 87,
        X = 88,
        Y = 89,
        Z = 90,**/



        [DllImport("user32.dll")]
        static extern int MapVirtualKey(uint uCode, uint uMapType);

        /// <summary>
        /// Utility to apply the result of pressing keys against a buffer
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="keys"></param>
        public bool ApplyKeyboardInput(StringBuilder m_SBuilder, UpdateState state)
        {
            var PressedKeys = state.KeyboardState.GetPressedKeys();
            if (PressedKeys.Length == 0) { return false; }

            var didChange = false;


            var m_CurrentKeyState = state.KeyboardState;
            var m_OldKeyState = state.PreviousKeyboardState;

            var shift = PressedKeys.Contains(Keys.LeftShift) || PressedKeys.Contains(Keys.RightShift);
            var caps = System.Windows.Forms.Control.IsKeyLocked(System.Windows.Forms.Keys.CapsLock);
            var numLock = System.Windows.Forms.Control.IsKeyLocked(System.Windows.Forms.Keys.NumLock);


            for (int j = 0; j < PressedKeys.Length; j++)
            {
                if (!m_CurrentKeyState.IsKeyUp(PressedKeys[j]) && m_OldKeyState.IsKeyUp(PressedKeys[j]))
                {
                    if (PressedKeys[j] == Keys.Back)
                    {
                        if (m_SBuilder.Length > 0)
                        {
                            m_SBuilder.Remove(m_SBuilder.Length - 1, 1);
                            didChange = true;
                        }
                        //TODO: Figure out how to remove a line if all its characters have been deleted...
                    }
                    else
                    {
                        char value = TranslateChar(PressedKeys[j], shift, caps, numLock);
                        if (value != '\0')
                        {
                            m_SBuilder.Append(value);
                            didChange = true;
                        }
                    }


                }
            }


            return didChange;
        }




        public static char TranslateChar(Keys key, bool shift, bool capsLock, bool numLock)
        {

            switch (key)
            {

                case Keys.A: return TranslateAlphabetic('a', shift, capsLock);

                case Keys.B: return TranslateAlphabetic('b', shift, capsLock);

                case Keys.C: return TranslateAlphabetic('c', shift, capsLock);

                case Keys.D: return TranslateAlphabetic('d', shift, capsLock);

                case Keys.E: return TranslateAlphabetic('e', shift, capsLock);

                case Keys.F: return TranslateAlphabetic('f', shift, capsLock);

                case Keys.G: return TranslateAlphabetic('g', shift, capsLock);

                case Keys.H: return TranslateAlphabetic('h', shift, capsLock);

                case Keys.I: return TranslateAlphabetic('i', shift, capsLock);

                case Keys.J: return TranslateAlphabetic('j', shift, capsLock);

                case Keys.K: return TranslateAlphabetic('k', shift, capsLock);

                case Keys.L: return TranslateAlphabetic('l', shift, capsLock);

                case Keys.M: return TranslateAlphabetic('m', shift, capsLock);

                case Keys.N: return TranslateAlphabetic('n', shift, capsLock);

                case Keys.O: return TranslateAlphabetic('o', shift, capsLock);

                case Keys.P: return TranslateAlphabetic('p', shift, capsLock);

                case Keys.Q: return TranslateAlphabetic('q', shift, capsLock);

                case Keys.R: return TranslateAlphabetic('r', shift, capsLock);

                case Keys.S: return TranslateAlphabetic('s', shift, capsLock);

                case Keys.T: return TranslateAlphabetic('t', shift, capsLock);

                case Keys.U: return TranslateAlphabetic('u', shift, capsLock);

                case Keys.V: return TranslateAlphabetic('v', shift, capsLock);

                case Keys.W: return TranslateAlphabetic('w', shift, capsLock);

                case Keys.X: return TranslateAlphabetic('x', shift, capsLock);

                case Keys.Y: return TranslateAlphabetic('y', shift, capsLock);

                case Keys.Z: return TranslateAlphabetic('z', shift, capsLock);



                case Keys.D0: return (shift) ? ')' : '0';

                case Keys.D1: return (shift) ? '!' : '1';

                case Keys.D2: return (shift) ? '@' : '2';

                case Keys.D3: return (shift) ? '#' : '3';

                case Keys.D4: return (shift) ? '$' : '4';

                case Keys.D5: return (shift) ? '%' : '5';

                case Keys.D6: return (shift) ? '^' : '6';

                case Keys.D7: return (shift) ? '&' : '7';

                case Keys.D8: return (shift) ? '*' : '8';

                case Keys.D9: return (shift) ? '(' : '9';



                case Keys.Add: return '+';

                case Keys.Divide: return '/';

                case Keys.Multiply: return '*';

                case Keys.Subtract: return '-';



                case Keys.Space: return ' ';

                case Keys.Tab: return '\t';



                case Keys.Decimal: if (numLock && !shift) return '.'; break;

                case Keys.NumPad0: if (numLock && !shift) return '0'; break;

                case Keys.NumPad1: if (numLock && !shift) return '1'; break;

                case Keys.NumPad2: if (numLock && !shift) return '2'; break;

                case Keys.NumPad3: if (numLock && !shift) return '3'; break;

                case Keys.NumPad4: if (numLock && !shift) return '4'; break;

                case Keys.NumPad5: if (numLock && !shift) return '5'; break;

                case Keys.NumPad6: if (numLock && !shift) return '6'; break;

                case Keys.NumPad7: if (numLock && !shift) return '7'; break;

                case Keys.NumPad8: if (numLock && !shift) return '8'; break;

                case Keys.NumPad9: if (numLock && !shift) return '9'; break;



                case Keys.OemBackslash: return shift ? '|' : '\\';

                case Keys.OemCloseBrackets: return shift ? '}' : ']';

                case Keys.OemComma: return shift ? '<' : ',';

                case Keys.OemMinus: return shift ? '_' : '-';

                case Keys.OemOpenBrackets: return shift ? '{' : '[';

                case Keys.OemPeriod: return shift ? '>' : '.';

                case Keys.OemPipe: return shift ? '|' : '\\';

                case Keys.OemPlus: return shift ? '+' : '=';

                case Keys.OemQuestion: return shift ? '?' : '/';

                case Keys.OemQuotes: return shift ? '"' : '\'';

                case Keys.OemSemicolon: return shift ? ':' : ';';

                case Keys.OemTilde: return shift ? '~' : '`';

            }



            return (char)0;

        }



        public static char TranslateAlphabetic(char baseChar, bool shift, bool capsLock)
        {
            return (capsLock ^ shift) ? char.ToUpper(baseChar) : baseChar;
        }





        /// <summary>
        /// Mouse event code, ensures depth is considered for mouse events
        /// </summary>
        

        private UIMouseEventRef LastMouseOver;
        private UIMouseEventRef LastMouseDown;
        private bool LastMouseDownState = false;


        public void HandleMouseEvents(TSOClient.Code.UI.Model.UpdateState state)
        {
            var mouseBtnDown = state.MouseState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed;
            var mouseDif = mouseBtnDown != LastMouseDownState;
            LastMouseDownState = mouseBtnDown;

            if (mouseDif)
            {
                if (mouseBtnDown)
                {
                    if (LastMouseDown != null)
                    {
                        /** We already have mouse down on an object **/
                        return;
                    }
                    if (LastMouseOver != null)
                    {
                        LastMouseDown = LastMouseOver;
                        LastMouseDown.Callback(UIMouseEventType.MouseDown, state);
                    }
                }
                else
                {
                    if (LastMouseDown != null)
                    {
                        LastMouseDown.Callback(UIMouseEventType.MouseUp, state);
                        LastMouseDown = null;
                    }
                }
            }

            if (state.MouseEvents.Count > 0)
            {
                var topMost =
                    state.MouseEvents.OrderBy(x => x.Element.Depth).Last();


                /** Same element **/
                if (LastMouseOver == topMost)
                {
                    return;
                }

                if (LastMouseOver != null)
                {
                    LastMouseOver.Callback(UIMouseEventType.MouseOut, state);
                }

                topMost.Callback(UIMouseEventType.MouseOver, state);
                LastMouseOver = topMost;
            }
            else
            {
                if (LastMouseOver != null)
                {
                    LastMouseOver.Callback(UIMouseEventType.MouseOut, state);
                    LastMouseOver = null;
                }
            }

        }

    }
}
