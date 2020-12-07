//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
//
// Name:        TimedMode.cs
// Created by:  Pux
// Created on:  02/09/19
//
//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

using UnityEngine;
using UnityEngine.Advertisements;
using BumblePux.Rebound.UserInput;
using BumblePux.Rebound.Player;
using BumblePux.Rebound.Interactables;
using BumblePux.Rebound.Timer;
using BumblePux.Rebound.Score;
using BumblePux.Rebound.UI;
using BumblePux.Rebound.General;
using BumblePux.Rebound.Audio;

namespace BumblePux.Rebound.GameControllers
{
    public class TimedMode : MonoBehaviour, IUnityAdsListener
    {
        public static bool isGameOver;

        [Header("Audio")]
        public Sound timedModeAudio;
        public Sound targetMissedAudio;

        [Header("References")]
        public BaseUserInput input;
        public PlayerController player;
        public Target target;
        public CountdownTimer timer;
        public GameOverUI gameOver;

        [Header("General Settings")]
        public float playerSpeedIncrement = 5f;
        public int targetPoints = 1;
        public int scoreToStartTimer = 3;
        public int scoreToDoublePenalty = 30;

        [Header("Player Settings")]
        public float startSpeed = 50f;
        public float maxSpeed = 600f;

        [Header("Timer Settings")]
        public float startTime = 20f;
        public float timeBonus = 2f;
        public float timePenalty = 5f;

        [Header("Camera Shake Settings")]
        public float shakeDuration;
        public float shakeMagnitude;

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
            input = GetComponent<BaseUserInput>();
            AudioManager.CrossFade(timedModeAudio);
            GameStart();
        }

        //----------------------------------------
        private void Update()
        {
            if (isGameOver) return;

            if (PauseMenuUI.isPaused) return;

            if (timer.HasExpired)
            {
                GameOver();
                return;
            }

            if (input.Clicked())
            {
                if (player.target != null)
                    TargetHit();
                else
                    TargetMissed();
            }
        }

        //----------------------------------------
        private void TargetHit()
        {
            ScoreManager.IncreaseScore(targetPoints);

            if (timer.IsActive)
            {
                timer.UpdateCountdown(timeBonus);
                CountdownUI.PlayUpdateAnimation();
                Notification.Instance.Show("+" + timeBonus.ToString() + "s");
            }
            else if (ScoreManager.Score >= scoreToStartTimer)
            {
                timer.IsActive = true;
                Notification.Instance.Show("Timer Started!");
            }

            player.target = null;
            player.IncreaseSpeed(playerSpeedIncrement);
            player.AttemptChangeDirection();

            target.ChangePosition();

            if (ScoreManager.Score % scoreToDoublePenalty == 0)
            {
                timePenalty *= 2f;
                Notification.Instance.Show("Time Penalty Doubled!");
            }

            CameraShake.TriggerShake(0.1f, 0.2f);

            // Check if score-based achievements can be unlocked. If so, unlock the achievement!
            Achievements.UnlockTimedModeProgress(ScoreManager.Score);
        }

        //----------------------------------------
        private void TargetMissed()
        {
            if (timer.IsActive)
            {
                timer.UpdateCountdown(-timePenalty);
                CountdownUI.PlayUpdateAnimation();
                Notification.Instance.Show("-" + timePenalty.ToString() + "s");
            }

            AudioManager.PlaySfx(targetMissedAudio);
            CameraShake.TriggerShake(0.1f, 0.5f);
        }

        //----------------------------------------
        private void GameStart()
        {
            isGameOver = false;

            ScoreManager.ResetScore();

            timer.ResetCountdown(startTime);
            timer.IsActive = false;

            player.maxSpeed = maxSpeed;
            player.SetSpeed(startSpeed);

            target.MoveToRandomPosition();
        }

        //----------------------------------------
        private void GameContinue()
        {
            AudioManager.CrossFade(timedModeAudio);
            isGameOver = false;
            timer.ResetCountdown(startTime);
            timer.IsActive = false;
        }

        //----------------------------------------
        private void GameOver()
        {
            isGameOver = true;
            timer.IsActive = false;

            // Show Game Over UI
            gameOver.ShowUI();

            // Unlock achievement
            Achievements.UnlockWelcomeToRebound();
        }

        //========================================
        // IUnityAdsListener Interface
        //========================================
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
                GameContinue();
            }
        }
    }
}