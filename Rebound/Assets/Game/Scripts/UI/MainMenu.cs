//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
//
// Name:        MainMenu.cs
// Created by:  Pux
// Created on:  09/08/19
//
//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BumblePux.Rebound.UI
{
    public class MainMenu : MonoBehaviour
    {
        public Animator buttonsAnimator;
        public Animator playButtonAnimator;
        public Animator titleTextAnimator;

        private bool isMenuOpen = false;
        private bool isGameStarted = false;

        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        // Public Methods
        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        public void OnPlaySelected()
        {
            StartCoroutine(Play());
        }

        //----------------------------------------
        public void OnMenuSelected()
        {
            if (isMenuOpen)
            {
                isMenuOpen = false;
                buttonsAnimator.SetBool("isOpened", false);
            }
            else
            {
                isMenuOpen = true;
                buttonsAnimator.SetBool("isOpened", true);
            }
        }

        //----------------------------------------
        public void OnHelpSelected()
        {
            Debug.Log("Would open \"How to Play\" page.");
        }

        //----------------------------------------
        public void OnSettingsSelected()
        {
            Debug.Log("Would open Settings menu");
        }

        //----------------------------------------
        public void OnLeaderboardsSelected()
        {
            Debug.Log("Would open Google Leaderboards");
        }

        //----------------------------------------
        public void OnAchievementsSelected()
        {
            Debug.Log("Would open Google Achievements");
        }

        //----------------------------------------
        public void OnReturnedToTitleScreen()
        {
            if (isGameStarted)
            {
                isGameStarted = false;
                titleTextAnimator.SetBool("startGame", false);
                playButtonAnimator.SetBool("startGame", false);
                buttonsAnimator.SetBool("startGame", false);
            }            
        }

        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        // Unity Methods
        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        // DEBUG ONLY - REMOVE ONCE TESTING IS FINISHED!!!
        //private void Update()
        //{
        //    if (Input.GetKeyDown(KeyCode.Space))
        //    {
        //        OnReturnedToTitleScreen();
        //    }
        //}

        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        // Private Methods
        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        private IEnumerator Play()
        {
            // Close the menu if it's open
            if (isMenuOpen)
                OnMenuSelected();

            yield return StartCoroutine(WaitForButtonAnimationFinished());

            isGameStarted = true;
            titleTextAnimator.SetBool("startGame", true);
            playButtonAnimator.SetBool("startGame", true);
            buttonsAnimator.SetBool("startGame", true);
        }

        //----------------------------------------
        private IEnumerator WaitForButtonAnimationFinished()
        {
            yield return new WaitForSeconds(0.5f);
        }
    }
}