//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
//
// Name:        CountdownTimer.cs
// Created by:  Pux
// Created on:  02/09/19
//
//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

using UnityEngine;

namespace BumblePux.Rebound.Timer
{
    public class CountdownTimer : MonoBehaviour
    {
        public Float countdownData;

        public bool IsActive { get; set; }
        public bool HasExpired { get; private set; }

        public void ResetCountdown(float startTime)
        {
            countdownData.Value = startTime;
            HasExpired = false;
            IsActive = false;
        }

        public void UpdateCountdown(float amount)
        {
            countdownData.Value += amount;
        }

        private void Update()
        {
            if (IsActive && !HasExpired)
            {
                Countdown();
            }
        }

        private void Countdown()
        {
            countdownData.Value -= Time.deltaTime;

            if (countdownData.Value <= float.Epsilon)
                Expired();
        }

        private void Expired()
        {
            countdownData.Value = 0;
            HasExpired = true;
        }
    }
}