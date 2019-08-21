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

namespace BumblePux.Rebound.UI
{
    public class MainMenuController : MonoBehaviour
    {
        public Sound mainMenuAudio;
        public string sceneToLoad;
        public float waitDuration = 1f;
        public Animator[] animators;

        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        // Public Methods
        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        public void LoadGame()
        {
            // Start disappear animations
            for (int i = 0; i < animators.Length; i++)
            {
                animators[i].SetTrigger("sceneChange");
            }

            StartCoroutine(LoadSceneAfterAnimations());
        }

        //----------------------------------------
        public void OpenSettings()
        {

        }

        //----------------------------------------
        public void OpenLeaderboards()
        {

        }

        //----------------------------------------
        public void OpenAchievements()
        {

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
        private IEnumerator LoadSceneAfterAnimations()
        {
            yield return new WaitForSeconds(waitDuration);

            // Load game scene
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}