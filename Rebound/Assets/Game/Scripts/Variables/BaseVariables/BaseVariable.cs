//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
//
// Name:        BaseVariable.cs
// Created by:  Pux
// Created on:  02/09/19
//
//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%


using System;
using UnityEngine;

namespace BumblePux.Rebound
{
    public abstract class BaseVariable<T> : ScriptableObject
    {
        public Action OnValueChanged;

        [SerializeField] T variableValue;

        public T Value
        {
            get { return variableValue; }
            set
            {
                variableValue = value;
                OnValueChanged?.Invoke();
            }
        }

        private void OnEnable()
        {
            variableValue = default;
        }
    }
}