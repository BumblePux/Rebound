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
using UnityEngine.SceneManagement;
using BumblePux.Rebound.Audio;

namespace BumblePux.Rebound.UI
{
    public class PauseMenuController : MonoBehaviour
    {
        public static bool isPaused;

        public Sound buttonSound;
        public Audio.AudioSettings audioSettings;
        public float animationWaitDuration = 1.5f;

        public GameObject pauseButtonPanel;
        public Animator pauseAnimator;

        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        // Public Methods
        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        public void Pause()
        {
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
        public void ToggleMusic()
        {
            audioSettings.ToggleMusicEnable();
        }

        //----------------------------------------
        public void ToggleSfx()
        {
            audioSettings.ToggleSfxEnable();
        }

        //----------------------------------------
        public void ShowPauseButton(bool active)
        {
            pauseButtonPanel.SetActive(active);
        }
    }
}