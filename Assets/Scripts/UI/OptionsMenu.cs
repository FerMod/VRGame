using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Garitto.UI
{
    public static class PlayerPrefsKeys
    {
        public const string MasterVolume = "MasterVolume";
        public const string MusicVolume = "MusicVolume";
        public const string EffectsVolume = "EffectsVolume";
    }

    public static class AudioMixerParameters
    {
        public const string MasterVolume = "Master";
        public const string MusicVolume = "Music";
        public const string EffectsVolume = "Effects";
    }

    public class OptionsMenu : MonoBehaviour
    {
        [Header("Sound")]
        public AudioMixer audioMixer;

        [Space]
        public Slider masterSlider;
        public Slider musicSlider;
        public Slider effectsSlider;

        private void Start()
        {
            //#if UNITY_EDITOR
            //            PlayerPrefs.DeleteAll();
            //#endif

            LoadData();

            // Sound
            masterSlider.onValueChanged.AddListener(SetMasterVolume);
            musicSlider.onValueChanged.AddListener(SetMusicVolume);
            effectsSlider.onValueChanged.AddListener(SetEffectsVolume);
        }

        private void OnDestroy()
        {
            // Sound
            if (masterSlider != null)
            {
                masterSlider.onValueChanged.RemoveListener(SetMasterVolume);
            }
            if (musicSlider != null)
            {
                musicSlider.onValueChanged.RemoveListener(SetMusicVolume);
            }
            if (effectsSlider != null)
            {
                effectsSlider.onValueChanged.RemoveListener(SetEffectsVolume);
            }
        }

        #region Sound
        public void SetMasterVolume(float value)
        {
            audioMixer.SetFloat("Master", ValueToDecibels(value));
        }

        public void SetMusicVolume(float value)
        {
            audioMixer.SetFloat("Music", ValueToDecibels(value));
        }

        public void SetEffectsVolume(float value)
        {
            audioMixer.SetFloat("Effects", ValueToDecibels(value));
        }
        #endregion

        #region Load/Save
        private void LoadData()
        {
            LoadSoundConfig();
        }

        private void SaveData()
        {
            SaveSoundConfig();
        }

        public void SaveSoundConfig()
        {
            PlayerPrefs.SetFloat(PlayerPrefsKeys.MasterVolume, masterSlider.value);
            PlayerPrefs.SetFloat(PlayerPrefsKeys.MusicVolume, musicSlider.value);
            PlayerPrefs.SetFloat(PlayerPrefsKeys.EffectsVolume, effectsSlider.value);
        }

        public void LoadSoundConfig()
        {
            if (PlayerPrefs.HasKey(PlayerPrefsKeys.MasterVolume))
            {
                masterSlider.value = PlayerPrefs.GetFloat(PlayerPrefsKeys.MasterVolume);
                SetMasterVolume(masterSlider.value);
            }
            else
            {
                audioMixer.GetFloat("Master", out var dbVolume);
                masterSlider.value = DecibelsToValue(dbVolume);
            }

            if (PlayerPrefs.HasKey(PlayerPrefsKeys.MusicVolume))
            {
                musicSlider.value = PlayerPrefs.GetFloat(PlayerPrefsKeys.MusicVolume);
                SetMusicVolume(musicSlider.value);
            }
            else
            {
                audioMixer.GetFloat("Music", out var dbVolume);
                musicSlider.value = DecibelsToValue(dbVolume);
            }

            if (PlayerPrefs.HasKey(PlayerPrefsKeys.EffectsVolume))
            {
                effectsSlider.value = PlayerPrefs.GetFloat(PlayerPrefsKeys.EffectsVolume);
                SetEffectsVolume(effectsSlider.value);
            }
            else
            {
                audioMixer.GetFloat("Effects", out var dbVolume);
                effectsSlider.value = DecibelsToValue(dbVolume);
            }
        }
        #endregion

        private float ValueToDecibels(float value)
        {
            return value > 0 ? Mathf.Log10(value) * 20 : -80f;
        }
        private float DecibelsToValue(float decibels)
        {
            var value = Mathf.Pow(10, decibels / 20);
            return Mathf.Clamp(value, 0f, 1f);
        }
    }

}
