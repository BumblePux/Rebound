//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
//
// Name:        GameOverUI.cs
// Created by:  Pux
// Created on:  13/08/19
//
//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;
using BumblePux.Rebound.Audio;

namespace BumblePux.Rebound.UI
{
    public class GameOverUI : MonoBehaviour, IUnityAdsListener
    {
        public Int scoreData;
        public TMP_Text scoreText;
        public TMP_Text adButtonText;
        public TMP_Text leaderboardText;
        public GameObject adButton;
        public Animator animator;
        public Sound gameOverTheme;

        [Multiline()]
        public string adButtonString;

        //----------------------------------------
        private void OnEnable()
        {
            Advertisement.AddListener(this);
        }

        private void OnDisable()
        {
            Advertisement.RemoveListener(this);
        }

        private void Start()
        {
            if (!Advertisement.isInitialized) 
                Advertisement.Initialize(AdsData.gameId, AdsData.testMode);

            adButtonText.text = adButtonString;
        }

        private void OnDestroy()
        {
            // Reset flag when scene is destroyed.
            // This will happen when loading MainMenu or restarting the game (i.e reload the scene).
            AdsData.hasAdBeenWatched = false;
        }

        //----------------------------------------
        public void ShowUI()
        {
            AudioManager.CrossFade(gameOverTheme);

            if (Social.localUser.authenticated)
            {
                Social.ReportScore(scoreData.Value, Rebound_GPGSIds.leaderboard_timed_mode, (bool success) =>
                {
                    if (success)
                        leaderboardText.text = "Leaderboard Updated";
                    else
                        leaderboardText.text = "Failed to update leaderboard";
                });
            }
            else
            {
                leaderboardText.text = "Not signed in. Cannot update leaderboard";
            }

            scoreText.text = scoreData.Value.ToString();

            StartCoroutine(ShowAdButton());

            animator.SetBool("gameOverAppear", true);
        }

        private IEnumerator ShowAdButton()
        {
            while (true)
            {
                if (!AdsData.hasAdBeenWatched)
                {
                    if (Advertisement.isInitialized && Advertisement.IsReady(AdsData.adPlacementId))
                        adButton.SetActive(true);
                    else
                        adButton.SetActive(false);
                }
                else
                    adButton.SetActive(false);

                yield return new WaitForSeconds(0.1f);
            }
        }

        //----------------------------------------
        public void OpenMainMenu()
        {
            StartCoroutine(LoadAfterAnimations("MainMenu"));
        }

        //----------------------------------------
        public void Restart()
        {
            string currentScene = SceneManager.GetActiveScene().name;
            StartCoroutine(LoadAfterAnimations(currentScene));
        }

        //----------------------------------------
        public void WatchAd()
        {
            Advertisement.Show(AdsData.adPlacementId);
        }

        //----------------------------------------
        private IEnumerator LoadAfterAnimations(string sceneName)
        {
            animator.SetBool("gameOverAppear", false);

            yield return new WaitForSeconds(1.0f);

            SceneManager.LoadScene(sceneName);
        }

        //----------------------------------------
        public void OnUnityAdsReady(string placementId) { }

        //----------------------------------------
        public void OnUnityAdsDidError(string message) { }

        //----------------------------------------
        public void OnUnityAdsDidStart(string placementId) { }

        //----------------------------------------
        public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
        {
            if (placementId == AdsData.adPlacementId && showResult == ShowResult.Finished)
            {
                AdsData.hasAdBeenWatched = true;
                animator.SetBool("gameOverAppear", false);
            }
        }
    }
}