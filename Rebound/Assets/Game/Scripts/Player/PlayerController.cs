//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
//
// Name:        Player.cs
// Created by:  Pux
// Created on:  12/08/19
//
//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BumblePux.Rebound.General;
using BumblePux.Rebound.Interactables;
using BumblePux.Rebound.UserInput;

namespace BumblePux.Rebound.Player
{
    public class PlayerController : MonoBehaviour
    {
        private static PlayerController instance;

        public static event Action OnTargetMissed;

        public BaseUserInput input;
        public Rotator2D rotator;

        private SpriteRenderer sr;
        private Transform trail;
        private BaseInteractable interactable;

        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        // Public Methods
        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        public static void Setup(float startSpeed, float maxSpeed, bool useMaxSpeed)
        {
            instance.rotator.Speed = startSpeed;
            instance.rotator.MaxSpeed = maxSpeed;
            instance.rotator.UseMaxSpeed = useMaxSpeed;
        }

        //----------------------------------------
        public static void ReactToTargetHit(float speedIncrement)
        {
            instance.rotator.Speed += speedIncrement;

            if (instance.AttemptChangeDirection())
            {
                instance.rotator.ChangeDirection();
                instance.sr.flipY = !instance.sr.flipY;

                Vector3 trailPos = instance.trail.localPosition;
                instance.trail.localPosition = new Vector3(0f, trailPos.y * -1, 0f);
            }
        }

        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        // Unity Methods
        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        private void Awake()
        {
            // Setup singleton
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }

            // Get required child components
            sr = GetComponentInChildren<SpriteRenderer>();
            trail = GetComponentInChildren<TrailRenderer>().gameObject.GetComponent<Transform>();            
        }

        //----------------------------------------
        private void Start()
        {
            // Get input component reference
            if (input == null)
                input = GetComponent<BaseUserInput>();
        }

        //----------------------------------------
        private void Update()
        {
            if (input.Clicked())
            {
                if (interactable != null)
                {
                    interactable.Interact();
                    interactable = null;
                }
                else
                {
                    if (OnTargetMissed != null)
                        OnTargetMissed.Invoke();
                }
            }
        }

        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        // OnTrigger
        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

        //----------------------------------------
        private void OnTriggerEnter2D(Collider2D collision)
        {
            interactable = collision.gameObject.GetComponent<BaseInteractable>();
            if (interactable == null)
                interactable = collision.gameObject.GetComponentInParent<BaseInteractable>();
        }

        //----------------------------------------
        private void OnTriggerExit2D(Collider2D collision)
        {
            interactable = null;
        }

        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        // Private Methods
        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        private bool AttemptChangeDirection()
        {
            int changeDirection = UnityEngine.Random.Range(0, 2);

            if (changeDirection == 1)
                return true;

            return false;
        }
    }
}