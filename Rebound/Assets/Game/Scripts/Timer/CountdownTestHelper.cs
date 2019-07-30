//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
//
// Name:        CountdownTestHelper.cs
// Created by:  Pux
// Created on:  30/07/19
//
//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BumblePux.Rebound.Timer
{
    public class CountdownTestHelper : MonoBehaviour
    {
        public SimpleCountdownTimer timer;
        public Button enableButton;
        public Button disableButton;
        public Button resetButton;
        public Button addFiveButton;
        public Button minusFiveButton;

        private void OnEnable()
        {
            enableButton.onClick.AddListener(timer.EnableTimer);
            disableButton.onClick.AddListener(timer.DisableTimer);
            resetButton.onClick.AddListener(timer.ResetCountdown);
            addFiveButton.onClick.AddListener(AddFiveToCountdown);
            minusFiveButton.onClick.AddListener(MinusFiveToCountdown);
        }

        private void AddFiveToCountdown()
        {
            timer.UpdateCurrentTime(5f);
        }

        private void MinusFiveToCountdown()
        {
            timer.UpdateCurrentTime(-5f);
        }
    }
}