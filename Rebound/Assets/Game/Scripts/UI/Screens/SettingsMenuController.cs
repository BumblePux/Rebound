//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
//
// Name:        SettingsMenuController.cs
// Created by:  Pux
// Created on:  24/08/19
//
//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using BumblePux.Rebound.Audio;

namespace BumblePux.Rebound.UI
{
    public class SettingsMenuController : MonoBehaviour
    {
        [Header("Animators")]
        public float waitDuration = 1f;
        public Animator[] animators;

        [Header("Audio Buttons")]
        public Image sfxImage;
        public Sprite sfxOn;
        public Sprite sfxOff;
        public Image musicImage;
        public Sprite musicOn;
        public Sprite musicOff;

        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        // Public Methods
        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        public void OnSfxSelected()
        {
            bool enabled = AudioManager.GetSfxEnabled();

            if (enabled)
            {
                AudioManager.SetSfxActive(false);
                sfxImage.sprite = sfxOff;
            }
            else
            {
                AudioManager.SetSfxActive(true);
                sfxImage.sprite = sfxOn;
            }
        }

        //----------------------------------------
        public void OnMusicSelected()
        {
            bool enabled = AudioManager.GetMusicEnabled();

            if (enabled)
            {
                AudioManager.SetMusicActive(false);
                musicImage.sprite = musicOff;
            }
            else
            {
                AudioManager.SetMusicActive(true);
                musicImage.sprite = musicOn;
            }
        }

        //----------------------------------------
        public void OpenPrivacyPolicy()
        {
            Application.OpenURL("https://unity3d.com/legal/privacy-policy");
        }

        //----------------------------------------
        public void OnBackButtonSelected()
        {
            SetAnimationsToDisappear();
            StartCoroutine(LoadSceneAfterAnimations("MainMenu"));
        }

        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        // Unity Methods
        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        private void Start()
        {
            SetSfxButton();
            SetMusicButton();
        }

        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        // Private Methods
        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        private void SetSfxButton()
        {
            bool enabled = AudioManager.GetSfxEnabled();

            if (enabled)
                sfxImage.sprite = sfxOn;
            else
                sfxImage.sprite = sfxOff;
        }

        //----------------------------------------
        private void SetMusicButton()
        {
            bool enabled = AudioManager.GetMusicEnabled();

            if (enabled)
                musicImage.sprite = musicOn;
            else
                musicImage.sprite = musicOff;
        }

        //----------------------------------------
        private IEnumerator LoadSceneAfterAnimations(string sceneName)
        {
            yield return new WaitForSeconds(waitDuration);

            // Load game scene
            SceneManager.LoadScene(sceneName);
        }

        //----------------------------------------
        private void SetAnimationsToDisappear()
        {
            for (int i = 0; i < animators.Length; i++)
            {
                animators[i].SetTrigger("sceneChange");
            }
        }
    }
}