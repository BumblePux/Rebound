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
        private Animator anim;

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
        private void Awake()
        {
            rb2d = GetComponent<Rigidbody2D>();
            anim = GetComponentInChildren<Animator>();
        }

        //----------------------------------------
        private void OnEnable()
        {
            MoveToRandomPosition();
        }

        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        // Public Methods
        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        public void MoveToRandomPosition()
        {
            int angle = (int)(rb2d.rotation - 180f) + Random.Range(-moveLimit, moveLimit);
            rb2d.MoveRotation(angle);
            anim.SetTrigger("ChangePosition");
        }
    }
}