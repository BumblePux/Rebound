//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
//
// Name:        Target.cs
// Created by:  Pux
// Created on:  31/07/19
//
//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BumblePux.Rebound.Audio;

namespace BumblePux.Rebound.Interactables
{
    public class Target : BaseInteractable
    {
        [Range(0, 120)]
        public int moveLimit = 90;
        public Sound onInteractedSound;

        public static Action OnTargetHit;

        private Rigidbody2D rb2d;
        private Animator anim;

        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        // Override Methods
        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        public override void Interact()
        {
            MoveToRandomPosition();

            // Play sound effect
            if (onInteractedSound != null)
                AudioManager.PlaySfx(onInteractedSound);

            if (OnTargetHit != null)
                OnTargetHit.Invoke();
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
            int angle = (int)(rb2d.rotation - 180f) + UnityEngine.Random.Range(-moveLimit, moveLimit);
            rb2d.MoveRotation(angle);
            anim.Play("target_appear", -1, 0f);
        }

        //----------------------------------------
        public void HideTarget(bool active)
        {
            anim.SetBool("hide", active);
        }
    }
}