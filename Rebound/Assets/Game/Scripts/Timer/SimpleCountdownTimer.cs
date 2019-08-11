//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
//
// Name:        SimpleCountdownTimer.cs
// Created by:  Pux
// Created on:  30/07/19
//
//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using BumblePux.Rebound.Utils;

namespace BumblePux.Rebound.Timer
{
    public class SimpleCountdownTimer : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private TMP_Text timerText = default;

        [Header("Settings")]
        [SerializeField] private float startTime = 20f;

        [Header("Debug Flags")]
        [SerializeField] private bool countdownActive = false;
        [SerializeField] private bool countdownComplete = false;

        public UnityEvent OnCountdownComplete;

        private float currentTime;

        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        // Properties
        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        public float StartTime
        {
            get { return startTime; }
            set { startTime = value; }
        }

        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        // Public Methods
        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        public void ResetCountdown()
        {
            SetupTimer();
        }
            
        //----------------------------------------
        public void EnableTimer()
        {
            countdownActive = true;
        }

        //----------------------------------------
        public void DisableTimer()
        {
            countdownActive = false;
        }

        //----------------------------------------
        public void UpdateCurrentTime(float amount)
        {
            if (!countdownActive)
                return;

            currentTime += amount;
            UpdateCountdownText();
        }

        //----------------------------------------
        public bool GetCountdownActive()
        {
            return countdownActive;
        }

        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        // Unity Methods
        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        private void Start()
        {
            SetupTimer();
        }

        private void Update()
        {
            if (GameState.IsPaused)
                return;

            if (countdownActive && !countdownComplete)
            {
                Countdown();

                if (countdownComplete)
                {
                    countdownActive = false;
                    OnCountdownComplete.Invoke();
                }

                UpdateCountdownText();
            }
        }

        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        // Private Methods
        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        private void Countdown()
        {
            if (currentTime <= float.Epsilon)
            {
                currentTime = 0f;
                countdownComplete = true;
            }
            else
            {
                currentTime -= Time.deltaTime;
            }
        }

        //----------------------------------------
        private void UpdateCountdownText()
        {
            if (timerText == null)
                return;

            float minutes = Mathf.Floor(currentTime / 60);
            float seconds = currentTime % 60;

            timerText.text = minutes.ToString("00") + ":" + seconds.ToString("00.00");
        }

        //----------------------------------------
        private void SetupTimer()
        {
            currentTime = startTime;
            countdownActive = false;
            countdownComplete = false;
            UpdateCountdownText();
        }
    }
}