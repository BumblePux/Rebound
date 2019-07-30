//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
//
// Name:        SimpleScoreHandler.cs
// Created by:  Pux
// Created on:  30/07/19
//
//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using BumblePux.Rebound.Events;

namespace BumblePux.Rebound.Score
{
    public class SimpleScoreHandler : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private TMP_Text scoreText = default;

        [Header("Debug")]
        [SerializeField] private int currentScore = default;

        public IntEvent OnScoreChanged;

        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        // Public Methods
        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        public void UpdateScore(int amount = 1)
        {
            currentScore += amount;
            UpdateScoreText();
        }

        public void ResetScore()
        {
            SetupScore();
        }

        public int GetCurrentScore()
        {
            return currentScore;
        }

        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        // Unity Methods
        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        private void Start()
        {
            SetupScore();
        }

        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        // Private Methods
        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        private void UpdateScoreText()
        {
            if (scoreText == null)
                return;

            scoreText.text = currentScore.ToString();
        }

        private void SetupScore()
        {
            currentScore = 0;
            UpdateScoreText();
        }
    }
}