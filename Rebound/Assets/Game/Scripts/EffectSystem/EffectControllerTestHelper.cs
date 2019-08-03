//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
//
// Name:        EffectControllerTestHelper.cs
// Created by:  Bee
// Created on:  03/08/19
//
//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BumblePux.Rebound.EffectSystem
{
    public class EffectControllerTestHelper : MonoBehaviour
    {
        public Toggle toggle;

        private void Update()
        {
            toggle.isOn = BaseEffect.IsRunning;
        }
    }
}