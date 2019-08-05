//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
//
// Name:        PauseController.cs
// Created by:  Pux
// Created on:  05/08/19
//
//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BumblePux.Rebound.UI
{
    public class PauseController : MonoBehaviour
    {
        [SerializeField] private Button pauseButton = default;
        [SerializeField] private GameObject pausePanel = default;

        private Animator anim;

        private bool isPaused;

        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        // Public Methods
        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        public void OnPauseButtonClicked()
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }

        //----------------------------------------
        public void Pause()
        {
            isPaused = true;

            Time.timeScale = 0f;

            pauseButton.gameObject.SetActive(false);
            pausePanel.SetActive(true);
        }

        //----------------------------------------
        public void Resume()
        {
            isPaused = false;

            Time.timeScale = 1f;

            pauseButton.gameObject.SetActive(true);
            pausePanel.SetActive(false);
        }

        //----------------------------------------
        public void RestartScene()
        {

        }

        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        // Unity Methods
        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        private void Awake()
        {
            anim = GetComponent<Animator>();
        }

        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        // Private Methods
        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        
    }
}