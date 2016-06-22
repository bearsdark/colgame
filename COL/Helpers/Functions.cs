using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace COL.Helpers
{
    public class Functions
    {
        public static int CountLineOfString(string str)
        {
            int count = 1;
            for (int i = 0; i < str.Length; i++)
            {
                if(str[i] == '\n')
                {
                    count++;
                }
            }
            return count;
        }
        public static string GetRandomString()
        {
            string path = Path.GetRandomFileName();
            path = path.Replace(".", ""); // Remove period.
            return path;
        }

        private static byte[] encryptData(string data)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider md5Hasher = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] hashedBytes;
            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            hashedBytes = md5Hasher.ComputeHash(encoder.GetBytes(data));
            return hashedBytes;
        }
        public static string md5(string data)
        {
            return BitConverter.ToString(encryptData(data)).Replace("-", "").ToLower();
        }
        public static bool MouseClick()
        {
            bool result = (Game1.lastMouse.LeftButton == ButtonState.Pressed && Game1.currentMouse.LeftButton == ButtonState.Released) ? true : false;
            return result;
        }
        public static bool MouseTouch()
        {
            bool result = (Mouse.GetState().LeftButton == ButtonState.Pressed) ? true : false;
            return result;
        }
        public static bool KeyboardPressed(Keys key)
        {
            bool result = (Keyboard.GetState().IsKeyDown(key) && Game1.lastKey.IsKeyUp(key)) ? true : false;
            return result;
        }
        public static string WrapText(SpriteFont font, string text, float maxLineWidth)
        {
            string[] words = text.Split(' ');
            StringBuilder sb = new StringBuilder();
            float lineWidth = 0f;
            float spaceWidth = font.MeasureString(" ").X;

            foreach (string word in words)
            {
                Vector2 size = font.MeasureString(word);

                if (lineWidth + size.X < maxLineWidth)
                {
                    sb.Append(word + " ");
                    lineWidth += size.X + spaceWidth;
                }
                else
                {
                    sb.Append("\n" + word + " ");
                    lineWidth = size.X + spaceWidth;
                }
            }

            return sb.ToString();
        }
        public static int CountHeightText(SpriteFont font, string text, int width)
        {
            int result = 0;
            string[] textRender = Functions.WrapText(font, text, width).Split('\n');
            foreach(string word in textRender)
            {
                result += (int)font.MeasureString(word).Y;
            }

            return result;
        }
        //public static string GetKeyboardInput(Keys key)
        //{
        //    string result = "";

        //    switch (key)
        //    {
        //        case Keys.Q:
        //            {
        //                result = "q";
        //            }
        //            break;
        //        case Keys.W:
        //            {
        //                result = "w";
        //            }
        //            break;
        //        case Keys.E:
        //            {
        //                result = "e";
        //            }
        //            break;
        //        case Keys.R:
        //            {
        //                result = "r";
        //            }
        //            break;
        //        case Keys.T:
        //            {
        //                result = "t";
        //            }
        //            break;
        //        case Keys.Y:
        //            {
        //                result = "y";
        //            }
        //            break;
        //        case Keys.U:
        //            {
        //                result = "u";
        //            }
        //            break;
        //        case Keys.I:
        //            {
        //                result = "i";
        //            }
        //            break;
        //        case Keys.O:
        //            {
        //                result = "o";
        //            }
        //            break;
        //        case Keys.P:
        //            {
        //                result = "p";
        //            }
        //            break;
        //        case Keys.A:
        //            {
        //                result = "a";
        //            }
        //            break;
        //        case Keys.S:
        //            {
        //                result = "s";
        //            }
        //            break;
        //        case Keys.D:
        //            {
        //                result = "d";
        //            }
        //            break;
        //        case Keys.F:
        //            {
        //                result = "f";
        //            }
        //            break;
        //        case Keys.G:
        //            {
        //                result = "g";
        //            }
        //            break;
        //        case Keys.H:
        //            {
        //                result = "h";
        //            }
        //            break;
        //        case Keys.J:
        //            {
        //                result = "j";
        //            }
        //            break;
        //        case Keys.K:
        //            {
        //                result = "k";
        //            }
        //            break;
        //        case Keys.L:
        //            {
        //                result = "l";
        //            }
        //            break;
        //        case Keys.Z:
        //            {
        //                result = "z";
        //            }
        //            break;
        //        case Keys.X:
        //            {
        //                result = "x";
        //            }
        //            break;
        //        case Keys.C:
        //            {
        //                result = "c";
        //            }
        //            break;
        //        case Keys.V:
        //            {
        //                result = "v";
        //            }
        //            break;
        //        case Keys.B:
        //            {
        //                result = "b";
        //            }
        //            break;
        //        case Keys.N:
        //            {
        //                result = "n";
        //            }
        //            break;
        //        case Keys.M:
        //            {
        //                result = "m";
        //            }
        //            break;
        //        case Keys.D0:
        //            {
        //                result = "0";
        //            }
        //            break;
        //        case Keys.D1:
        //            {
        //                result = "1";
        //            }
        //            break;
        //        case Keys.D2:
        //            {
        //                result = "2";
        //            }
        //            break;
        //        case Keys.D3:
        //            {
        //                result = "3";
        //            }
        //            break;
        //        case Keys.D4:
        //            {
        //                result = "4";
        //            }
        //            break;
        //        case Keys.D5:
        //            {
        //                result = "5";
        //            }
        //            break;
        //        case Keys.D6:
        //            {
        //                result = "6";
        //            }
        //            break;
        //        case Keys.D7:
        //            {
        //                result = "7";
        //            }
        //            break;
        //        case Keys.D8:
        //            {
        //                result = "8";
        //            }
        //            break;
        //        case Keys.D9:
        //            {
        //                result = "9";
        //            }
        //            break;
        //        case Keys.NumPad0:
        //            {
        //                result = "0";
        //            }
        //            break;
        //        case Keys.NumPad1:
        //            {
        //                result = "1";
        //            }
        //            break;
        //        case Keys.NumPad2:
        //            {
        //                result = "2";
        //            }
        //            break;
        //        case Keys.NumPad3:
        //            {
        //                result = "3";
        //            }
        //            break;
        //        case Keys.NumPad4:
        //            {
        //                result = "4";
        //            }
        //            break;
        //        case Keys.NumPad5:
        //            {
        //                result = "5";
        //            }
        //            break;
        //        case Keys.NumPad6:
        //            {
        //                result = "6";
        //            }
        //            break;
        //        case Keys.NumPad7:
        //            {
        //                result = "7";
        //            }
        //            break;
        //        case Keys.NumPad8:
        //            {
        //                result = "8";
        //            }
        //            break;
        //        case Keys.NumPad9:
        //            {
        //                result = "9";
        //            }
        //            break;
        //    }

        //    result = ((Keyboard.GetState().IsKeyDown(Keys.LeftShift) || Keyboard.GetState().IsKeyDown(Keys.RightShift)) && !Console.CapsLock) ? result.ToUpper() : result;
        //    result = (Console.CapsLock && !(Keyboard.GetState().IsKeyDown(Keys.LeftShift) || Keyboard.GetState().IsKeyDown(Keys.RightShift))) ? result.ToUpper() : result;

        //    return result;
        //}
    }
}
