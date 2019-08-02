//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
//
// Name:        PlayerController.cs
// Created by:  Pux
// Created on:  31/07/19
//
//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BumblePux.Rebound.UserInput;
using BumblePux.Rebound.Interactables;
using BumblePux.Rebound.General;
using UnityEngine.Events;

namespace BumblePux.Rebound.Player
{
    public class PlayerController : MonoBehaviour
    {
        public BaseUserInput input;
        public UnityEvent OnNoInteractableClicked;

        private Rotator2D rotator;
        private SpriteRenderer sr;
        private Transform trail;
        private BaseInteractable interactable;

        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        // Public Methods
        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        public void SetupPlayer(float startSpeed, float maxSpeed, bool useMaxSpeed)
        {
            rotator.Speed = startSpeed;
            rotator.MaxSpeed = maxSpeed;
            rotator.UseMaxSpeed = useMaxSpeed;
        }

        //----------------------------------------
        public void ReactToTargetHit(float speedChange)
        {
            rotator.Speed += speedChange;

            if (AttemptChangeDirection())
            {
                rotator.ChangeDirection();
                sr.flipY = !sr.flipY;

                Vector3 trailPos = trail.localPosition;
                trail.localPosition = new Vector3(0f, trailPos.y * -1, 0f);
            }
        }

        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        // Unity Methods
        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        private void Awake()
        {
            rotator = GetComponent<Rotator2D>();
            sr = GetComponentInChildren<SpriteRenderer>();
            trail = GetComponentInChildren<TrailRenderer>().gameObject.GetComponent<Transform>();
        }

        //----------------------------------------
        private void Update()
        {
            if (input.Clicked())
            {
                if (interactable != null)
                {
                    interactable.Interact();
                }
                else
                {
                    OnNoInteractableClicked.Invoke();
                }
            }
        }

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
            int doChangeDirection = UnityEngine.Random.Range(0, 2);

            if (doChangeDirection == 1)
            {
                return true;
            }
            return false;
        }
    }
}