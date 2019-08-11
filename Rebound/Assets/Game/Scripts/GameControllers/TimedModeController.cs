//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
//
// Name:        TimedModeController.cs
// Created by:  Pux
// Created on:  02/08/19
//
//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BumblePux.Rebound.Player;
using BumblePux.Rebound.Interactables;
using BumblePux.Rebound.Score;
using BumblePux.Rebound.Timer;
using BumblePux.Rebound.Planets;
using BumblePux.Rebound.EffectSystem;
using BumblePux.Rebound.Utils;
using BumblePux.Rebound.UI;

using UnityEngine.SceneManagement;

namespace BumblePux.Rebound.GameControllers
{
    public class TimedModeController : BaseGameController
    {
        [Header("Game References")]
        [SerializeField] private PlayerController player = default;
        [SerializeField] private Target target = default;
        [SerializeField] private SimpleScoreHandler score = default;
        [SerializeField] private SimpleCountdownTimer timer = default;
        [SerializeField] private EffectController effects = default;
        [SerializeField] private Planet planet = default;

        [Header("Canvas References")]
        [SerializeField] private GameObject scoreUICanvas = default;
        [SerializeField] private GameObject timerUICanvas = default;

        [Header("Timer Settings")]
        [SerializeField] private float startTime = 20f;
        [SerializeField] private float timeBonus = 2f;
        [SerializeField] private float timePenalty = 5f;
        [SerializeField] private int scoreToStartTimer = 2;

        [Header("Player Settings")]
        [SerializeField] private float startSpeed = 50f;
        [SerializeField] private float maxSpeed = 600f;
        [SerializeField] private float speedStep = 10f;

        [Header("Effect Settings")]
        [SerializeField] private int minScoreToStartEffects = 10;
        [SerializeField] private int triggerEffectAfterXPoints = 8;

        private bool timerActive = false;

        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        // Override Methods
        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        public override void GameStart()
        {
            PauseController.SetActive(true);

            GameState.IsGameOver = false;

            SetPrefabsActive(true);
            SetupGame();

            scoreUICanvas.SetActive(true);
            timerUICanvas.SetActive(true);            
        }

        //----------------------------------------
        public override void GameOver()
        {
            PauseController.SetActive(false);
            GameOverController.ShowGameOverPanel();

            GameState.IsGameOver = true;
            SetPrefabsActive(false);
            SetupGame();

            scoreUICanvas.SetActive(false);
            timerUICanvas.SetActive(false);
        }

        //----------------------------------------
        public override void GameQuit()
        {
            PauseController.SetActive(false);

            GameState.IsGameOver = true;
            SetPrefabsActive(false);
            SetupGame();

            scoreUICanvas.SetActive(false);
            timerUICanvas.SetActive(false);
        }

        //----------------------------------------
        public override void TargetHit()
        {
            if (GameState.IsGameOver)
                return;

            // Update score
            score.UpdateScore();

            // Update timer
            if (score.GetCurrentScore() >= scoreToStartTimer)
            {
                if (!timer.GetCountdownActive() && !timerActive)
                {
                    timer.EnableTimer();
                    timerActive = true;
                }
                else
                    timer.UpdateCurrentTime(timeBonus);
            }            

            // Update player
            player.ReactToTargetHit(speedStep);

            // Attempt to trigger effect
            if (score.GetCurrentScore() >= minScoreToStartEffects && score.GetCurrentScore() % triggerEffectAfterXPoints == 0)
            {
                effects.TriggerRandomEffect();
            }
        }

        //----------------------------------------
        public override void TargetMissed()
        {
            if (GameState.IsGameOver)
                return;

            if (timer.GetCountdownActive())
                timer.UpdateCurrentTime(-timePenalty);
        }

        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        // Unity Methods
        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        private void OnEnable()
        {
            player.OnNoInteractableClicked.AddListener(TargetMissed);
            target.OnInteracted.AddListener(TargetHit);
            timer.OnCountdownComplete.AddListener(GameOver);
        }

        //----------------------------------------
        private void OnDisable()
        {
            player.OnNoInteractableClicked.RemoveListener(TargetMissed);
            target.OnInteracted.RemoveListener(TargetHit);
            timer.OnCountdownComplete.RemoveListener(GameOver);
        }

        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        // Private Methods
        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        private void SetPrefabsActive(bool active)
        {
            target.gameObject.SetActive(active);
            score.gameObject.SetActive(active);
            timer.gameObject.SetActive(active);
            effects.gameObject.SetActive(active);
            planet.gameObject.SetActive(active);
        }

        //----------------------------------------
        private void SetupGame()
        {
            timerActive = false;
            timer.ResetCountdown();
            timer.StartTime = startTime;
            player.SetupPlayer(startSpeed, maxSpeed, true);
            score.ResetScore();
            planet.ShowRandomPlanet();
            target.MoveToRandomPosition();
        }
    }
}