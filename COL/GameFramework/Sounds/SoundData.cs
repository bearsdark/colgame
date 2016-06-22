using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COL.GameFramework.Sounds
{
    public class SoundData
    {
        public string AssetName;
        public string Path;
        public string Format;

        public bool IsSoundEffect
        {
            get
            {
                if (String.Compare(Format, "WAV") == 0)
                    return true;
                else
                    return false;
            }
        }

        public bool IsSong
        {
            get
            {
                if (String.Compare(Format, "MP3") == 0)
                    return true;
                else
                    return false;
            }
        }
    }
}
