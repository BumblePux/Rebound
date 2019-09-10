//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
//
// Name:        MainMenuUI.cs
// Created by:  Pux
// Created on:  04/09/19
//
//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using BumblePux.Rebound.Audio;

namespace BumblePux.Rebound.UI
{
    public class MainMenuUI : MonoBehaviour
    {
        public TMP_Text signInButtonText;
        public TMP_Text authText;
        public Sound mainMenuAudio;
        public Animator[] animators;

        //----------------------------------------
        private void OnEnable()
        {
            authText.text = "Updating...";
        }

        private void Start()
        {
            AudioManager.CrossFade(mainMenuAudio);

            // Set Sign In and Auth text
            StartCoroutine(UpdateSigninText());
        }

        private IEnumerator UpdateSigninText()
        {
            while (true)
            {
                yield return new WaitForSeconds(2.0f);

                if (Social.localUser.authenticated)
                {
                    signInButtonText.text = "Sign Out";
                    authText.text = "Signed in as: " + Social.localUser.userName;
                }
                else
                {
                    signInButtonText.text = "Sign In";
                    authText.text = "Not signed in.";
                }
            }
        }

        //----------------------------------------
        public void PlayGame()
        {
            if (!PlayerPrefs.HasKey("HasPlayed"))
            {
                SceneInformation.IntendedToLoadTutorial = false;
                SceneInformation.NextScene = "TimedMode";
                StartCoroutine(LoadAfterAnimations("HowToPlay"));
            }
            else
            {
                SceneInformation.IntendedToLoadTutorial = false;
                StartCoroutine(LoadAfterAnimations("TimedMode"));
            }
        }

        //----------------------------------------
        public void OpenHowToPlay()
        {
            SceneInformation.IntendedToLoadTutorial = true;
            StartCoroutine(LoadAfterAnimations("HowToPlay"));
        }

        //----------------------------------------
        public void OpenSettings()
        {
            StartCoroutine(LoadAfterAnimations("Settings"));
        }

        //----------------------------------------
        public void ShowAchievements()
        {
            if (Social.localUser.authenticated)
                Social.ShowAchievementsUI();
            //else
                //Debug.Log("Not signed in. Cannot show Achievements");
        }

        //----------------------------------------
        public void ShowLeaderboards()
        {
            if (Social.localUser.authenticated)
                Social.ShowLeaderboardUI();
            //else
                //Debug.Log("Not signed in. Cannot show Leaderboards");
        }

        //----------------------------------------
        public void SignInOut()
        {
            StopAllCoroutines();

            if (!Social.localUser.authenticated)
            {
                Social.localUser.Authenticate((bool success) =>
                {
                    if (success)
                    {
                        ((PlayGamesPlatform)Social.Active).SetGravityForPopups(Gravity.BOTTOM);
                        signInButtonText.text = "Sign Out";
                        authText.text = "Signed in as: " + Social.localUser.userName;
                    }
                    else
                    {
                        signInButtonText.text = "Sign In";
                        authText.text = "Sign-in failed. Please try again.";
                    }
                });
            }
            else
            {
                PlayGamesPlatform.Instance.SignOut();

                signInButtonText.text = "Sign In";
                authText.text = "Signed out";
            }

            StartCoroutine(UpdateSigninText());
        }

        //----------------------------------------
        private IEnumerator LoadAfterAnimations(string sceneName)
        {
            for (int i = 0; i < animators.Length; i++)
            {
                animators[i].SetTrigger("sceneChange");
            }

            yield return new WaitForSeconds(1.0f);

            SceneManager.LoadScene(sceneName);
        }
    }
}