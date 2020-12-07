//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
//
// Name:        ScoreUI.cs
// Created by:  Pux
// Created on:  02/09/19
//
//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

using UnityEngine;
using TMPro;

namespace BumblePux.Rebound.UI
{
    public class ScoreUI : MonoBehaviour
    {
        public Int scoreData;
        public TMP_Text scoreText;
        public Animator animator;

        private void OnEnable()
        {
            scoreData.OnValueChanged += UpdateText;
            scoreData.OnValueChanged += PlayUpdateAnimation;
        }

        private void OnDisable()
        {
            scoreData.OnValueChanged -= UpdateText;
            scoreData.OnValueChanged -= PlayUpdateAnimation;
        }

        private void Start()
        {
            UpdateText();
        }

        private void UpdateText()
        {
            scoreText.text = scoreData.Value.ToString();
        }

        private void PlayUpdateAnimation()
        {
            animator.Play("scoreUI_appear", -1, 0.2f);
        }
    }
}