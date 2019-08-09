//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
//
// Name:        Target.cs
// Created by:  Pux
// Created on:  31/07/19
//
//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using BumblePux.Rebound.Audio;

namespace BumblePux.Rebound.Interactables
{
    public class Target : BaseInteractable
    {
        [Range(0, 120)]
        [SerializeField] private int moveLimit = 90;
        [SerializeField] private AudioClip onInteractedClip = default;

        public UnityEvent OnInteracted;

        private Rigidbody2D rb2d;

        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        // Override Methods
        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        public override void Interact()
        {
            MoveToRandomPosition();

            if (onInteractedClip != null)
                AudioManager.PlaySfx(onInteractedClip);

            OnInteracted.Invoke();
        }

        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        // Unity Methods
        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        private void Start()
        {
            rb2d = GetComponent<Rigidbody2D>();

            MoveToRandomPosition();
        }

        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        // Private Methods
        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        private void MoveToRandomPosition()
        {
            int angle = (int)(rb2d.rotation - 180f) + Random.Range(-moveLimit, moveLimit);
            rb2d.MoveRotation(angle);
        }
    }
}