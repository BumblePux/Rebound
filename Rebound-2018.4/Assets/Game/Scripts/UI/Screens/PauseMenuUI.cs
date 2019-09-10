//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
//
// Name:        PauseMenuUI.cs
// Created by:  Pux
// Created on:  12/08/19
//
//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

using UnityEngine;
using BumblePux.Rebound.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace BumblePux.Rebound.UI
{
    public class PauseMenuUI : MonoBehaviour
    {
        public static bool isPaused;

        public Image musicImage;
        public Image sfxImage;
        public Animator animator;

        //----------------------------------------
        public void Pause()
        {
            Time.timeScale = 0f;
            isPaused = true;

            SetupAudioToggleButtons();

            animator.SetBool("isPaused", isPaused);
        }

        //----------------------------------------
        public void Resume()
        {
            Time.timeScale = 1f;
            isPaused = false;

            animator.SetBool("isPaused", isPaused);
        }

        //----------------------------------------
        public void OpenMainMenu()
        {
            Time.timeScale = 1f;
            isPaused = false;

            SceneManager.LoadScene("MainMenu");
        }

        //----------------------------------------
        public void Restart()
        {
            Time.timeScale = 1f;
            isPaused = false;

            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        //----------------------------------------
        public void ToggleMusic()
        {
            if (AudioManager.GetMusicEnabled())
            {
                AudioManager.SetMusicActive(false);
                musicImage.sprite = AudioManager.GetMusicSprite(false);
            }
            else
            {
                AudioManager.SetMusicActive(true);
                musicImage.sprite = AudioManager.GetMusicSprite(true);
            }
        }

        //----------------------------------------
        public void ToggleSfx()
        {
            if (AudioManager.GetSfxEnabled())
            {
                AudioManager.SetSfxActive(false);
                sfxImage.sprite = AudioManager.GetSfxSprite(false);
            }
            else
            {
                AudioManager.SetSfxActive(true);
                sfxImage.sprite = AudioManager.GetSfxSprite(true);
            }
        }

        //----------------------------------------
        private void SetupAudioToggleButtons()
        {
            // Set music button image
            bool enabled = AudioManager.GetMusicEnabled();
            if (enabled)
                musicImage.sprite = AudioManager.GetMusicSprite(enabled);
            else
                musicImage.sprite = AudioManager.GetMusicSprite(enabled);

            // Set sfx button image
            enabled = AudioManager.GetSfxEnabled();
            if (enabled)
                sfxImage.sprite = AudioManager.GetSfxSprite(enabled);
            else
                sfxImage.sprite = AudioManager.GetSfxSprite(enabled);
        }
    }
}