//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
//
// Name:        PlatformInputAdder.cs
// Created by:  Pux
// Created on:  11/08/19
//
//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BumblePux.Rebound.UserInput
{
    public class PlatformInputAdder : MonoBehaviour
    {
        private void Awake()
        {
#if UNITY_EDITOR || UNITY_STANDALONE            
            gameObject.AddComponent<MouseInput>();
#elif UNITY_ANDROID
            gameObject.AddComponent<MobileInput>();            
#endif
        }
    }
}