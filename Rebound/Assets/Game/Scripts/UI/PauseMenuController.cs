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

        public float animationWaitDuration = 1.5f;

        public GameObject pauseButtonPanel;
        public Animator pauseAnimator;

        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        // Public Methods
        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        public void Pause()
        {
            isPaused = true;
            Time.timeScale = 0f;
            pauseAnimator.SetBool("isPaused", isPaused);
        }

        //----------------------------------------
        public void Resume()
        {
            isPaused = false;
            Time.timeScale = 1f;
            pauseAnimator.SetBool("isPaused", isPaused);
        }

        //----------------------------------------
        public void LoadMainMenu()
        {
            isPaused = false;
            Time.timeScale = 1f;
            pauseAnimator.SetBool("isPaused", isPaused);
            //StartCoroutine(LoadSceneAfterAnimations("MainMenu", animationWaitDuration));
            SceneManager.LoadScene("MainMenu");
        }

        //----------------------------------------
        public void RestartScene()
        {
            isPaused = false;
            Time.timeScale = 1f;
            pauseAnimator.SetBool("isPaused", isPaused);
            //StartCoroutine(LoadSceneAfterAnimations(SceneManager.GetActiveScene().name, animationWaitDuration));
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        //----------------------------------------
        public void ShowPauseButton(bool active)
        {
            pauseButtonPanel.SetActive(active);
        }

        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        // Unity Methods
        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (isPaused)
                    Resume();
                else
                    Pause();
            }
        }
    }
}