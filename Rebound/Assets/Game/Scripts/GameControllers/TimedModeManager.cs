//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
//
// Name:        TimedModeManager.cs
// Created by:  Pux
// Created on:  12/08/19
//
//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using BumblePux.Rebound.Player;
using BumblePux.Rebound.Interactables;
using BumblePux.Rebound.Score;
using BumblePux.Rebound.UI;
using BumblePux.Rebound.Audio;

namespace BumblePux.Rebound.GameControllers
{
    public class TimedModeManager : MonoBehaviour
    {
        [Header("Audio Settings")]
        public Sound timedModeAudio;

        [Header("Player Settings")]
        public float startSpeed = 50f;
        public float maxSpeed = 600f;
        public float speedIncrement = 5f;

        [Header("Timer Settings")]
        public float startTime = 20f;
        public float timeBonus = 2f;
        public float timePenalty = 5f;
        public int scoreToStartTimer = 2;

        [Header("References")]
        public TMP_Text timerText;
        public Animator timerAnimator;
        public GameOverController gameOver;
        public PauseMenuController pause;

        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        // Private Fields
        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        private float currentTime;
        private bool isGameOver;

        private bool hasTimerStarted;
        

        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        // Public Methods
        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        public void TargetHit()
        {
            if (isGameOver)
                return;

            ScoreManager.IncreaseScore();

            if (hasTimerStarted)
                UpdateTime(timeBonus);            

            PlayerController.ReactToTargetHit(speedIncrement);
        }

        //----------------------------------------
        public void TargetMissed()
        {
            if (isGameOver)
                return;

            if (hasTimerStarted)
                UpdateTime(-timePenalty);
        }

        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        // Unity Methods
        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        private void Start()
        {
            AudioManager.CrossFade(timedModeAudio);
            SetupGame();
        }

        //----------------------------------------
        private void Update()
        {
            if (isGameOver)
                return;

            if (PauseMenuController.isPaused)
                return;

            if (ScoreManager.Score >= scoreToStartTimer)
            {
                Countdown();
                UpdateTimerText();

                if (!hasTimerStarted)
                    hasTimerStarted = true;
            }            
        }

        //----------------------------------------
        private void OnEnable()
        {
            // Subscribe to required events
            PlayerController.OnTargetMissed += TargetMissed;
            Target.OnTargetHit += TargetHit;
        }

        //----------------------------------------
        private void OnDisable()
        {
            // Unsubscribe from events
            PlayerController.OnTargetMissed -= TargetMissed;
            Target.OnTargetHit -= TargetHit;
        }

        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        // Private Methods
        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        // Game State
        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

            //----------------------------------------
        private void SetupGame()
        {
            currentTime = startTime;
            UpdateTimerText();

            ScoreManager.ResetScore();

            PlayerController.Setup(startSpeed, maxSpeed, true);
        }

        //----------------------------------------
        private void GameOver()
        {
            isGameOver = true;

            PlayerController.Setup(startSpeed, maxSpeed, true);

            pause.ShowPauseButton(false);
            timerAnimator.SetBool("hide", true);
            ScoreManager.HideScoreDisplay(true);

            gameOver.OnGameOver();
        }

        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        // Timer
        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

        //----------------------------------------
        private void Countdown()
        {
            currentTime -= Time.deltaTime;

            if (currentTime <= float.Epsilon)
            {                
                currentTime = 0f;
                GameOver();
            }
        }

        //----------------------------------------
        private void UpdateTime(float amount)
        {
            currentTime += amount;
            UpdateTimerText();
            timerAnimator.Play("timerUI_appear", -1, 0.5f);
        }

        //----------------------------------------
        private void UpdateTimerText()
        {
            float minutes = Mathf.Floor(currentTime / 60);
            float seconds = currentTime % 60;

            timerText.text = minutes.ToString("00") + ":" + seconds.ToString("00.00");
        }
    }
}