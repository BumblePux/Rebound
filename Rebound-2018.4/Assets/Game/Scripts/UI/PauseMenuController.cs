//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
//
// Name:        PauseMenuController.cs
// Created by:  Pux
// Created on:  12/08/19
//
//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using BumblePux.Rebound.Audio;
using BumblePux.Rebound.Player;

namespace BumblePux.Rebound.UI
{
    public class PauseMenuController : MonoBehaviour
    {
        public static bool isPaused;

        public Sound buttonSound;
        public float animationWaitDuration = 1.5f;

        public GameObject pauseButtonPanel;
        public Animator pauseAnimator;

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
        public void Pause()
        {
            SetMusicButton();
            SetSfxButton();
            AudioManager.PlaySfx(buttonSound);
            isPaused = true;
            Time.timeScale = 0f;
            pauseAnimator.SetBool("isPaused", isPaused);
        }

        //----------------------------------------
        public void Resume()
        {
            AudioManager.PlaySfx(buttonSound);
            isPaused = false;
            Time.timeScale = 1f;
            pauseAnimator.SetBool("isPaused", isPaused);
        }

        //----------------------------------------
        public void LoadMainMenu()
        {
            PlayerController.Setup(50f, 600f, true);    // Make sure rocket speed is reset when quitting to main menu
            AudioManager.PlaySfx(buttonSound);
            isPaused = false;
            Time.timeScale = 1f;
            pauseAnimator.SetBool("isPaused", isPaused);            
            SceneManager.LoadScene("MainMenu");
        }

        //----------------------------------------
        public void RestartScene()
        {
            AudioManager.PlaySfx(buttonSound);
            isPaused = false;
            Time.timeScale = 1f;
            pauseAnimator.SetBool("isPaused", isPaused);            
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
        public void ShowPauseButton(bool active)
        {
            pauseButtonPanel.SetActive(active);
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
    }
}