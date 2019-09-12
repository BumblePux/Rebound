//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
//
// Name:        CountdownUI.cs
// Created by:  Pux
// Created on:  02/09/19
//
//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

using UnityEngine;
using TMPro;

namespace BumblePux.Rebound.UI
{
    public class CountdownUI : MonoBehaviour
    {
        public Float countdownData;
        public TMP_Text minutesText;
        public TMP_Text secondsText;
        public static Animator animator;

        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            UpdateText();
        }

        private void UpdateText()
        {
            float minutes = Mathf.Floor(countdownData.Value / 60f);
            float seconds = countdownData.Value % 60f;

            minutesText.text = minutes.ToString("00");
            secondsText.text = seconds.ToString("00.00");
        }

        public static void PlayUpdateAnimation()
        {
            animator.Play("timerUI_appear", -1, 0.5f);
        }
    }
}