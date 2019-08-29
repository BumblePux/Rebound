//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
//
// Name:        MainMenuController.cs
// Created by:  Pux
// Created on:  12/08/19
//
//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using BumblePux.Rebound.Audio;
using BumblePux.Rebound.Player;
using BumblePux.Rebound.GPGServices;

namespace BumblePux.Rebound.UI
{
    public class MainMenuController : MonoBehaviour
    {
        public Sound mainMenuAudio;
        public float waitDuration = 1f;
        public Animator[] animators;

        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        // Public Methods
        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        public void LoadGame()
        {
            SetAnimationsToDisappear();
            PlayerController.PlayDisappearAnimation();
            StartCoroutine(LoadSceneAfterAnimations("HowToPlay"));
        }

        //----------------------------------------
        public void OpenSettings()
        {
            SetAnimationsToDisappear();
            PlayerController.PlayDisappearAnimation();
            StartCoroutine(LoadSceneAfterAnimations("Settings"));
        }

        //----------------------------------------
        public void OpenLeaderboards()
        {
            GPGS.ShowLeaderboardUI();
        }

        //----------------------------------------
        public void OpenAchievements()
        {
            GPGS.ShowAchievementsUI();
        }

        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        // Unity Methods
        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        private void Start()
        {
            AudioManager.CrossFade(mainMenuAudio);
        }

        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        // Private Methods
        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        private IEnumerator LoadSceneAfterAnimations(string sceneName)
        {
            yield return new WaitForSeconds(waitDuration);

            // Load game scene
            SceneManager.LoadScene(sceneName);
        }

        //----------------------------------------
        private void SetAnimationsToDisappear()
        {
            for (int i = 0; i < animators.Length; i++)
            {
                animators[i].SetTrigger("sceneChange");
            }
        }
    }
}