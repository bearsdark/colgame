using COL.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COL.GameObjects
{
    public class TextInput
    {
        private KeyboardState ActualKeyState,
                             LastKeyState;
        public string text = "";
        public string textReplace = "";
        private string replaceText = "";
        private string _char;
        private int length;
        private float spaceTime;

        public TextInput(int length)
        {
            this.length = length;
        }
        public TextInput(int length, string replaceTxt)
        {
            this.length = length;
            this.replaceText = replaceTxt;
        }
        private Keys[] keysToCheck = new Keys[]
        {
               Keys.D0, Keys.D1, Keys.D2, Keys.D3,
               Keys.D4, Keys.D5, Keys.D6, Keys.D7,
               Keys.D8, Keys.D9,

               Keys.NumPad0, Keys.NumPad1, Keys.NumPad2,
               Keys.NumPad3, Keys.NumPad4, Keys.NumPad5,
               Keys.NumPad6, Keys.NumPad7, Keys.NumPad8,
               Keys.NumPad9,

               Keys.A, Keys.B, Keys.C, Keys.D, Keys.E,
               Keys.F, Keys.G, Keys.H, Keys.I, Keys.J,
               Keys.K, Keys.L, Keys.M, Keys.N, Keys.O,
               Keys.P, Keys.Q, Keys.R, Keys.S, Keys.T,
               Keys.U, Keys.V, Keys.W, Keys.X, Keys.Y,
               Keys.Z, Keys.OemPeriod, (Keys)45
        };

        public void Update(GameTime gameTime)
        {
            this.LastKeyState = this.ActualKeyState;
            this.ActualKeyState = Keyboard.GetState();

            foreach (Keys key in this.keysToCheck)
            {
                if (this.ActualKeyState.IsKeyDown(key) && this.LastKeyState.IsKeyUp(key))
                {
                    _char = null;

                    switch (key)
                    {
                        case Keys.D0:
                            _char += "0";
                            break;
                        case Keys.D1:
                            _char += "1";
                            break;
                        case Keys.D2:
                            _char += "2";
                            break;
                        case Keys.D3:
                            _char += "3";
                            break;
                        case Keys.D4:
                            _char += "4";
                            break;
                        case Keys.D5:
                            _char += "5";
                            break;
                        case Keys.D6:
                            _char += "6";
                            break;
                        case Keys.D7:
                            _char += "7";
                            break;
                        case Keys.D8:
                            _char += "8";
                            break;
                        case Keys.D9:
                            _char += "9";
                            break;
                        case Keys.NumPad0:
                            _char += "0";
                            break;
                        case Keys.NumPad1:
                            _char += "1";
                            break;
                        case Keys.NumPad2:
                            _char += "2";
                            break;
                        case Keys.NumPad3:
                            _char += "3";
                            break;
                        case Keys.NumPad4:
                            _char += "4";
                            break;
                        case Keys.NumPad5:
                            _char += "5";
                            break;
                        case Keys.NumPad6:
                            _char += "6";
                            break;
                        case Keys.NumPad7:
                            _char += "7";
                            break;
                        case Keys.NumPad8:
                            _char += "8";
                            break;
                        case Keys.NumPad9:
                            _char += "9";
                            break;
                        case Keys.A:
                            _char += "a";
                            break;
                        case Keys.B:
                            _char += "b";
                            break;
                        case Keys.C:
                            _char += "c";
                            break;
                        case Keys.D:
                            _char += "d";
                            break;
                        case Keys.E:
                            _char += "e";
                            break;
                        case Keys.F:
                            _char += "f";
                            break;
                        case Keys.G:
                            _char += "g";
                            break;
                        case Keys.H:
                            _char += "h";
                            break;
                        case Keys.I:
                            _char += "i";
                            break;
                        case Keys.J:
                            _char += "j";
                            break;
                        case Keys.K:
                            _char += "k";
                            break;
                        case Keys.L:
                            _char += "l";
                            break;
                        case Keys.M:
                            _char += "m";
                            break;
                        case Keys.N:
                            _char += "n";
                            break;
                        case Keys.O:
                            _char += "o";
                            break;
                        case Keys.P:
                            _char += "p";
                            break;
                        case Keys.Q:
                            _char += "q";
                            break;
                        case Keys.R:
                            _char += "r";
                            break;
                        case Keys.S:
                            _char += "s";
                            break;
                        case Keys.T:
                            _char += "t";
                            break;
                        case Keys.U:
                            _char += "u";
                            break;
                        case Keys.V:
                            _char += "v";
                            break;
                        case Keys.W:
                            _char += "w";
                            break;
                        case Keys.X:
                            _char += "x";
                            break;
                        case Keys.Y:
                            _char += "y";
                            break;
                        case Keys.Z:
                            _char += "z";
                            break;
                        case Keys.OemPeriod:
                            _char += ".";
                            break;
                        case (Keys)45:
                            _char += "-";
                            break;
                    }
                    if (key != Keys.Back)
                    {
                        if (((ActualKeyState.IsKeyDown(Keys.LeftShift) ||
                            ActualKeyState.IsKeyDown(Keys.RightShift)) && !Console.CapsLock) ||
                            (!(ActualKeyState.IsKeyDown(Keys.LeftShift) ||
                            ActualKeyState.IsKeyDown(Keys.RightShift)) && Console.CapsLock))
                        {
                            _char = _char.ToUpper();
                        }
                    }
                    if (text.Length < this.length)
                    {
                        text += _char;
                    }
                    if (this.textReplace.Length < this.length && this.replaceText != "" && _char != null)
                    {
                        this.textReplace += this.replaceText;
                    }
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Back))
            {
                this.spaceTime += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

                if(this.spaceTime >= 75)
                {
                    this.spaceTime = 0;

                    if (text.Length > 0)
                        this.text = this.text.Remove(text.Length - 1);

                    if (this.textReplace.Length > 0)
                        this.textReplace = this.textReplace.Remove(this.textReplace.Length - 1);
                }
            }
        }
    }
}
