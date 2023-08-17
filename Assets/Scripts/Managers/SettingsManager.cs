using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arkanoid.Managers
{
    public class SettingsManager : MonoBehaviour
    {
        private static readonly string SOUND_VALUE_KEY = "SoundVolume";
        private static SettingsManager _self;

        private void Start()
        {
            _self = this;
        }

        public float SoundVolume { get; private set; } = 30;

        public void SaveSoundVolume(float soundVolume)
        {
            SoundVolume = soundVolume;
            Debug.Log("Громкость: " + SoundVolume);
            PlayerPrefs.SetFloat(SOUND_VALUE_KEY, SoundVolume);
        }

        public float GetSoundVolume()
        {
            if (GetSoundVolumeFromPrefs(out float value))
                return value;

            return SoundVolume;
        }

        private bool GetSoundVolumeFromPrefs(out float value)
        {
            value = PlayerPrefs.GetFloat(SOUND_VALUE_KEY);

            return true;
        }

        public static SettingsManager GetInstance()
        {
            return _self;
        }
    }
}