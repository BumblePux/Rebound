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

        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        // References
        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        private PlayerController player;
        private Target target;
        private SimpleScoreHandler score;
        private SimpleCountdownTimer timer;

        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        // Override Methods
        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        public override void GameStart()
        {
            timer.StartTime = startTime;
            player.SetupPlayer(startSpeed, maxSpeed, true);

            isGameOver = false;
        }

        //----------------------------------------
        public override void GameOver()
        {
            isGameOver = true;
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