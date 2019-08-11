//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
//
// Name:        GameOver.cs
// Created by:  Pux
// Created on:  11/08/19
//
//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using BumblePux.Rebound.Score;
using BumblePux.Rebound.Utils;
using BumblePux.Rebound.GameControllers;

namespace BumblePux.Rebound.UI
{
    public class GameOverController : MonoBehaviour
    {
        private static GameOverController instance;

        [SerializeField] private GameObject gameOverPanel = default;
        [SerializeField] private Animator gameOverAnimator = default;
        [SerializeField] private TMP_Text scoreText = default;

        [Header("External References")]
        [SerializeField] private SimpleScoreHandler score = default;
        [SerializeField] private MainMenu mainMenu = default;
        [SerializeField] private BaseGameController controller = default;

        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        // Public Methods
        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        public static void SetActive(bool active)
        {
            instance.gameOverPanel.SetActive(active);
        }

        //----------------------------------------
        public static void ShowGameOverPanel()
        {
            instance.SetScoreText();
            instance.gameOverAnimator.SetBool("isGameOver", true);
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
        private void SetScoreText()
        {
            scoreText.text = score.GetCurrentScore().ToString();
        }

        //----------------------------------------
        private IEnumerator MainMenu()
        {
            instance.gameOverAnimator.SetBool("isGameOver", false);

            yield return StartCoroutine(WaitForAnimations());

            mainMenu.OnReturnToTitleScreen();
        }

        //----------------------------------------
        private IEnumerator Restart()
        {
            instance.gameOverAnimator.SetBool("isGameOver", false);

            yield return StartCoroutine(WaitForAnimations());

            controller.GameStart();
        }

        //----------------------------------------
        private IEnumerator WaitForAnimations()
        {
            yield return new WaitForSeconds(gameOverAnimator.GetCurrentAnimatorStateInfo(0).length);
        }
    }
}