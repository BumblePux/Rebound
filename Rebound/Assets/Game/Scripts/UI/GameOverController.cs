//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
//
// Name:        GameOver.cs
// Created by:  Pux
// Created on:  13/08/19
//
//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using BumblePux.Rebound.Score;

namespace BumblePux.Rebound.UI
{
    public class GameOverController : MonoBehaviour
    {
        public float animationWaitDuration = 1.5f;
        public Animator gameOverAnimator;
        public TMP_Text scoreText;

        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        // Public Methods
        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        public void OnGameOver()
        {
            SetScore(ScoreManager.Score);
            gameOverAnimator.SetBool("gameOverAppear", true);
        }

        //----------------------------------------
        public void WatchAd()
        {
            Debug.Log("Attempt to show Rewarded Ad");
        }

        //----------------------------------------
        public void LoadMainMenu()
        {
            gameOverAnimator.SetBool("gameOverAppear", false);
            StartCoroutine(LoadSceneAfterAnimations("MainMenu", animationWaitDuration));
        }

        //----------------------------------------
        public void RestartScene()
        {
            gameOverAnimator.SetBool("gameOverAppear", false);
            StartCoroutine(LoadSceneAfterAnimations(SceneManager.GetActiveScene().name, animationWaitDuration));
        }

        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        // Private Methods
        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        private IEnumerator LoadSceneAfterAnimations(string sceneName, float duration)
        {
            yield return new WaitForSeconds(duration);

            SceneManager.LoadScene(sceneName);
        }

        //----------------------------------------
        private void SetScore(int score)
        {
            scoreText.text = score.ToString();
        }
    }
}