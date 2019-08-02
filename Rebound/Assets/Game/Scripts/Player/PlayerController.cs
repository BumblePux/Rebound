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
using UnityEngine.Events;

namespace BumblePux.Rebound.Player
{
    public class PlayerController : MonoBehaviour
    {
        public BaseUserInput input;
        public UnityEvent OnNoInteractableClicked;

        private BaseInteractable interactable;

        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        // Unity Methods
        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
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
    }
}