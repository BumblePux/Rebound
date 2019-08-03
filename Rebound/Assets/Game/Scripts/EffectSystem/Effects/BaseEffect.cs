//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
//
// Name:        BaseEffect.cs
// Created by:  Pux
// Created on:  02/08/19
//
//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BumblePux.Rebound.EffectSystem
{
    public abstract class BaseEffect : MonoBehaviour
    {
        [SerializeField] protected string popupString = "";
        [SerializeField] protected bool showPopupOnActivation = true;

        private bool canBePlayed = true;

        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        // Properties
        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        public static bool IsRunning { get; private set; }
        public bool CanBePlayed { get { return canBePlayed; } protected set { canBePlayed = value; } }
        public string PopupString { get { return popupString; } }

        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        // Public Methods
        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        public IEnumerator TriggerEffect(float duration)
        {
            IsRunning = true;

            yield return StartCoroutine(Effect(duration));

            IsRunning = false;
        }

        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        // Abstract Methods
        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        public abstract IEnumerator Effect(float duration);
    }
}