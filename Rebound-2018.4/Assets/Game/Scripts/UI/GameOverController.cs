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
using UnityEngine.UI;
using TMPro;
using BumblePux.Rebound.Score;
using BumblePux.Rebound.Audio;
using BumblePux.Rebound.Player;
using BumblePux.Rebound.Ads;
using BumblePux.Rebound.GPGServices;

namespace BumblePux.Rebound.UI
{
    public class GameOverController : MonoBehaviour
    {
        public Sound buttonSound;
        public float animationWaitDuration = 1.5f;
        public Animator gameOverAnimator;
        public TMP_Text scoreText;
        public Button adButton;

        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        // Public Methods
        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        public void OnGameOver()
        {
            SetScore(ScoreManager.Score);
            gameOverAnimator.SetBool("gameOverAppear", true);

            if (!AdManager.IsAdReady())
                adButton.gameObject.SetActive(false);

            GPGS.UnlockWelcomeAchievement();
            GPGS.PostToLeaderboard(ScoreManager.Score, Rebound_GPGSIds.leaderboard_timed_mode);
        }

        //----------------------------------------
        public void OnAdWatched()
        {
            // AdManager.ShowTimedModeRewardedAd() added as listener in code,
            // so this method could be removed. Keeping for now in case it is needed
            // in the future.
            gameOverAnimator.SetBool("gameOverAppear", false);
            adButton.gameObject.SetActive(false);
        }

        //----------------------------------------
        public void LoadMainMenu()
        {
            AudioManager.PlaySfx(buttonSound);
            PlayerController.PlayDisappearAnimation();
            gameOverAnimator.SetBool("gameOverAppear", false);
            StartCoroutine(LoadSceneAfterAnimations("MainMenu", animationWaitDuration));
        }

        //----------------------------------------
        public void RestartScene()
        {
            AudioManager.PlaySfx(buttonSound);
            gameOverAnimator.SetBool("gameOverAppear", false);
            StartCoroutine(LoadSceneAfterAnimations(SceneManager.GetActiveScene().name, animationWaitDuration));
        }

        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        // Unity Methods
        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        private void OnEnable()
        {
            adButton.onClick.AddListener(AdManager.ShowTimedModeRewardedAd);
        }

        //----------------------------------------
        private void OnDisable()
        {
            adButton.onClick.RemoveListener(AdManager.ShowTimedModeRewardedAd);
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