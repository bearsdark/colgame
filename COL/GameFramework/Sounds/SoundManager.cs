using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COL.GameFramework.Sounds
{
    public class SoundManager
    {
        private static SoundManager _instance = new SoundManager();
        private Dictionary<string, SoundEffectInstance> _sound;
        private Dictionary<string, Song> _songs;

        private bool _enableSoundEffects;
        public bool EnableSoundEffects
        {
            get
            {
                return _instance._enableSoundEffects;
            }
            set
            {
                _instance._enableSoundEffects = value;
            }
        }

        private bool _enableBackgroundMusic;
        public bool EnableBackgroundMusic
        {
            get
            {
                return _instance._enableBackgroundMusic;
            }
            set
            {
                _instance._enableBackgroundMusic = value;
            }
        }
        public SoundManager()
        {
            this._sound = new Dictionary<string, SoundEffectInstance>();
            this._songs = new Dictionary<string, Song>();

            this._enableBackgroundMusic = true;
            this._enableSoundEffects = true;
        }

        public static void AddSoundEffect(string name, SoundEffectInstance soundEffect)
        {
            if (!_instance._sound.ContainsKey(name))
                _instance._sound.Add(name, soundEffect);
        }

        public static void AddSong(string name, Song song)
        {
            if (!_instance._songs.ContainsKey(name))
                _instance._songs.Add(name, song);
        }

        public static void PlaySound(string name, bool isLoop = false)
        {
            if (_instance._enableSoundEffects)
            {
                if (_instance._sound.ContainsKey(name))
                {
                    _instance._sound[name].IsLooped = isLoop;
                    _instance._sound[name].Play();
                }
            }
        }

        public static void PlaySong(string name, bool isLoop = false)
        {
            if (_instance._enableBackgroundMusic)
            {
                if (_instance._songs.ContainsKey(name))
                {
                    MediaPlayer.IsRepeating = isLoop;
                    MediaPlayer.Play(_instance._songs[name]);
                }
            }
        }
    }
}
