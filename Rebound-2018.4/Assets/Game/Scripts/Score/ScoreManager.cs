//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
//
// Name:        ScoreManager.cs
// Created by:  Pux
// Created on:  13/08/19
//
//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace BumblePux.Rebound.Score
{
    public class ScoreManager : MonoBehaviour
    {
        private static ScoreManager instance;

        public TMP_Text scoreText;
        public Animator scoreAnimator;

        public static int Score { get; private set; }


        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        // Public Methods
        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        public static void IncreaseScore(int amount = 1)
        {
            Score += amount;
            instance.UpdateScoreText();
            instance.PlayUpdateScoreAnimation();
        }

        //----------------------------------------
        public static void ResetScore()
        {
            Score = 0;
            instance.UpdateScoreText();
        }

        //----------------------------------------
        public static void HideScoreDisplay(bool active)
        {
            instance.scoreAnimator.SetBool("hide", active);
        }

        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        // Unity Methods
        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        private void Awake()
        {
            // Setup singleton
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                instance = this;
            }
        }

        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        // Unity Methods
        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        private void UpdateScoreText()
        {
            scoreText.text = Score.ToString();            
        }

        //----------------------------------------
        private void PlayUpdateScoreAnimation()
        {
            scoreAnimator.Play("scoreUI_appear", -1, 0.2f);
        }
    }
}