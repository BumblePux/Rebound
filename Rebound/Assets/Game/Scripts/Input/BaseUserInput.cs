//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
//
// Name:        BaseUserInput.cs
// Created by:  Pux
// Created on:  31/07/19
//
//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BumblePux.Rebound.UserInput
{
    public abstract class BaseUserInput : MonoBehaviour
    {
        protected EventSystem eventSystem;

        public abstract bool Clicked();

        protected virtual void Start()
        {
            eventSystem = EventSystem.current;
        }
    }
}