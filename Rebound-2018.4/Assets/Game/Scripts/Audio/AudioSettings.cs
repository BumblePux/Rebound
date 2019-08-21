//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
//
// Name:        AudioSettings.cs
// Created by:  Pux
// Created on:  15/08/19
//
//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace BumblePux.Rebound.Audio
{
    public class AudioSettings : MonoBehaviour
    {
        public AudioMixer mainMixer;

        public Sprite musicSpriteOn;
        public Sprite musicSpriteOff;
        public Sprite sfxSpriteOn;
        public Sprite sfxSpriteOff;

        public Image musicButton;
        public Image sfxButton;

        private bool isMusicEnabled;
        private bool isSfxEnabled;

        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        // Public Methods
        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        public void ToggleMusicEnable()
        {
            if (isMusicEnabled)
                DisableBackgroundVolume();
            else
                EnableBackgroundVolume();
        }

        //----------------------------------------
        public void ToggleSfxEnable()
        {
            if (isSfxEnabled)
                DisableSfxVolume();
            else
                EnableSfxVolume();
        }

        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        // Unity Methods
        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        private void OnEnable()
        {
            isMusicEnabled = IsMusicEnabled();
            isSfxEnabled = IsSfxEnabled();

            ToggleMusicEnable();
            ToggleSfxEnable();
        }

        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        // Private Methods
        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

        //----------------------------------------
        private void EnableBackgroundVolume()
        {
            mainMixer.SetFloat("Background", -10f);
            musicButton.sprite = musicSpriteOn;
            PlayerPrefs.SetInt("IsMusicEnabled", 1);
            isMusicEnabled = true;
        }

        //----------------------------------------
        private void EnableSfxVolume()
        {
            mainMixer.SetFloat("Sfx", -5f);
            sfxButton.sprite = sfxSpriteOn;
            PlayerPrefs.SetInt("IsSfxEnabled", 1);
            isSfxEnabled = true;
        }

        //----------------------------------------
        private void DisableBackgroundVolume()
        {
            mainMixer.SetFloat("Background", -80f);
            musicButton.sprite = musicSpriteOff;
            PlayerPrefs.SetInt("IsMusicEnabled", 0);
            isMusicEnabled = false;
        }

        //----------------------------------------
        private void DisableSfxVolume()
        {
            mainMixer.SetFloat("Sfx", -80f);
            sfxButton.sprite = sfxSpriteOff;
            PlayerPrefs.SetInt("IsSfxEnabled", 0);
            isSfxEnabled = false;
        }

        //----------------------------------------
        private bool IsMusicEnabled()
        {
            int data = PlayerPrefs.GetInt("IsMusicEnabled");

            if (data == 1)
                return true;

            return false;
        }

        //----------------------------------------
        private bool IsSfxEnabled()
        {
            int data = PlayerPrefs.GetInt("IsSfxEnabled");

            if (data == 1)
                return true;

            return false;
        }
    }
}