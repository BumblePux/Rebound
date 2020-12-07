//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
//
// Name:        Notification.cs
// Created by:  Pux
// Created on:  06/09/19
//
//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

using TMPro;
using UnityEngine;

namespace BumblePux.Rebound
{
    public class Notification : MonoBehaviour
    {
        public static Notification Instance;

        public Animator animator;
        public TMP_Text notifyText;

        public void Show(string message)
        {
            notifyText.text = message;
            animator.Play("notification_appear", -1, 0f);
        }

        private void Awake()
        {
            if (Instance != null && Instance != this)
                Destroy(gameObject);
            else
                Instance = this;
        }
    }
}