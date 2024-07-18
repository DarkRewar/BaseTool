using System.Collections.Generic;
using UnityEngine;

namespace BaseTool
{
    public static class SettingsManager
    {
        public static GeneralSettings General;
        public static AudioSettings Audio;

        static SettingsManager()
        {
            General = new GeneralSettings();
            Audio = new AudioSettings();
        }
    }

    public class GeneralSettings
    {
        public static readonly List<Resolution> DefaultResolutions = new List<Resolution>
        {
            new Resolution { width = 1920, height = 1080 },
            new Resolution { width = 1280, height = 720 },
        };

        public List<Resolution> AvailableResolutions = new(DefaultResolutions);

        private bool _fullScreen = true;
        public bool FullScreen
        {
            get => _fullScreen;
            set
            {
                _fullScreen = value;
                SetResolution(_resolution);
            }
        }

        private Resolution _resolution = default;
        public Resolution Resolution => _resolution;

        public void SetResolution(Resolution res)
        {
            _resolution = res;
            SetResolution(res, _fullScreen);
        }

        public void SetResolution(Resolution res, bool fullScreen)
        {
            Screen.SetResolution(res.width, res.height, fullScreen);
        }
    }

    public class AudioSettings
    {
        public string MusicVolumeKey = "MusicVolume";
        public string SFXVolumeKey = "SFXVolume";
        public string VoiceVolumeKey = "VoiceVolume";
        public string UIVolumeKey = "UIVolume";
    }
}
