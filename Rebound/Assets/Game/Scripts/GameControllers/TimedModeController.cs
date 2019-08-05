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
using BumblePux.Rebound.EffectSystem;

using UnityEngine.SceneManagement;

namespace BumblePux.Rebound.GameControllers
{
    public class TimedModeController : BaseGameController
    {
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

        [Header("References")]
        [SerializeField] private GameObject gameOverCanvas = default;

        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        // References
        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        private PlayerController player;
        private Target target;
        private SimpleScoreHandler score;
        private SimpleCountdownTimer timer;
        private EffectController effects;


        //TEST ONLY - REMOVE THIS!!!
        public void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        // Override Methods
        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        public override void GameStart()
        {
            gameOverCanvas.SetActive(false);

            timer.StartTime = startTime;
            player.SetupPlayer(startSpeed, maxSpeed, true);

            isGameOver = false;
        }

        //----------------------------------------
        public override void GameOver()
        {
            isGameOver = true;

            gameOverCanvas.SetActive(true);
        }

        //----------------------------------------
        public override void TargetHit()
        {
            if (isGameOver)
                return;

            // Update score
            score.UpdateScore();

            // Update timer
            if (score.GetCurrentScore() >= scoreToStartTimer)
            {
                if (!timer.GetCountdownActive())
                    timer.EnableTimer();
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
            if (isGameOver)
                return;

            if (timer.GetCountdownActive())
                timer.UpdateCurrentTime(-timePenalty);
        }

        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        // Unity Methods
        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        private void Awake()
        {
            player = FindObjectOfType<PlayerController>();
            target = FindObjectOfType<Target>();
            score = FindObjectOfType<SimpleScoreHandler>();
            timer = FindObjectOfType<SimpleCountdownTimer>();
            effects = FindObjectOfType<EffectController>();
        }

        //----------------------------------------
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

        //----------------------------------------
        private void Start()
        {
            GameStart();
        }

        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        // Private Methods
        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

    }
}