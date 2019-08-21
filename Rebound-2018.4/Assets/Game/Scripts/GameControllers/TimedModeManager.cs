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
using BumblePux.Rebound.Planets;
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
        public Planet planet;
        public Target target;

        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        // Private Fields
        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        private float currentTime;
        private bool isGameOver;

        private bool hasTimerStarted;

        private float lastPlayerSpeed;
        

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
            lastPlayerSpeed = PlayerController.GetSpeed();
        }

        //----------------------------------------
        public void TargetMissed()
        {
            if (isGameOver)
                return;

            if (hasTimerStarted)
                UpdateTime(-timePenalty);
        }

        //----------------------------------------
        public void ContinueGame()
        {
            gameOver.OnAdWatched();            
            currentTime = startTime;
            UpdateTimerText();
            PlayerController.Setup(lastPlayerSpeed, maxSpeed, true);
            ShowElements();
            StartCoroutine(WaitForStartingTimer());
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
            lastPlayerSpeed = PlayerController.GetSpeed();
        }

        //----------------------------------------
        private void GameOver()
        {
            isGameOver = true;

            PlayerController.Setup(startSpeed, maxSpeed, true);

            HideElements();

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

        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        // UI
        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

        //----------------------------------------
        private void HideElements()
        {
            pause.ShowPauseButton(false);
            timerAnimator.SetBool("hide", true);
            ScoreManager.HideScoreDisplay(true);
            planet.HidePlanet(true);
            target.HideTarget(true);
        }

        //----------------------------------------
        private void ShowElements()
        {
            pause.ShowPauseButton(true);
            timerAnimator.SetBool("hide", false);
            ScoreManager.HideScoreDisplay(false);
            planet.HidePlanet(false);
            target.HideTarget(false);
        }

        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        // Coroutines
        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

        //----------------------------------------
        private IEnumerator WaitForStartingTimer()
        {
            yield return new WaitForSeconds(2f);
            isGameOver = false;
        }
    }
}