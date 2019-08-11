//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
//
// Name:        PauseController.cs
// Created by:  Pux
// Created on:  05/08/19
//
//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

using System.Collections;
using UnityEngine;
using BumblePux.Rebound.Utils;
using BumblePux.Rebound.GameControllers;

namespace BumblePux.Rebound.UI
{
    public class PauseController : MonoBehaviour
    {
        private static PauseController instance;

        [SerializeField] private GameObject buttonPanel = default;
        [SerializeField] private GameObject pausePanel = default;

        [Header("Animators")]
        public Animator pauseButtonAnimator;
        public Animator pausePanelAnimator;

        [Header("External Referenes")]
        public MainMenu mainMenu;
        public BaseGameController controller;

        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        // Public Methods
        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        public static void SetActive(bool active)
        {
            if (instance == null)
                return;

            instance.buttonPanel.SetActive(active);
            instance.pausePanel.SetActive(active);
        }

        //----------------------------------------
        public void OnPauseSelected()
        {
            StopAllCoroutines();
            StartCoroutine(Pause());
        }

        //----------------------------------------
        public void OnResumeSelected()
        {
            StopAllCoroutines();
            StartCoroutine(Resume());
        }

        //----------------------------------------
        public void OnHomeSelected()
        {
            StopAllCoroutines();
            StartCoroutine(MainMenu());
        }

        //----------------------------------------
        public void OnRestartSelected()
        {
            StopAllCoroutines();
            StartCoroutine(Restart());
        }

        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        // Unity Methods
        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        private void Awake()
        {
            // Initialize Singleton
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }

        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        // Private Methods
        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        private IEnumerator Pause()
        {
            GameState.IsPaused = true;
            SetAnimators(true);

            yield return StartCoroutine(WaitForAnimations());

            Time.timeScale = 0f;
        }

        //----------------------------------------
        private IEnumerator Resume()
        {
            Time.timeScale = 1f;
            
            SetAnimators(false);

            yield return StartCoroutine(WaitForAnimations());

            GameState.IsPaused = false;
        }

        //----------------------------------------
        private IEnumerator MainMenu()
        {
            Time.timeScale = 1f;

            GameState.IsPaused = false;
            SetAnimators(false);

            yield return StartCoroutine(WaitForAnimations());

            controller.GameQuit();
            mainMenu.OnReturnToTitleScreen();
        }

        //----------------------------------------
        private IEnumerator Restart()
        {
            Time.timeScale = 1f;

            GameState.IsPaused = false;
            SetAnimators(false);            

            yield return StartCoroutine(WaitForAnimations());

            controller.GameStart();
        }

        //----------------------------------------
        private IEnumerator WaitForAnimations()
        {
            yield return new WaitForSeconds(pauseButtonAnimator.GetCurrentAnimatorStateInfo(0).length);
        }

        //----------------------------------------
        private void SetAnimators(bool toggle)
        {
            pauseButtonAnimator.SetBool("isPaused", toggle);
            pausePanelAnimator.SetBool("isPaused", toggle);
        }
    }
}