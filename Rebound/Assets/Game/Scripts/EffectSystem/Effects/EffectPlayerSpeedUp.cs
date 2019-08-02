//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
//
// Name:        EffectPlayerSpeedUp.cs
// Created by:  Pux
// Created on:  02/08/19
//
//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BumblePux.Rebound.General;

namespace BumblePux.Rebound.EffectSystem
{
    public class EffectPlayerSpeedUp : BaseEffect
    {
        [SerializeField] private float speedUpAmount = 50f;

        private Rotator2D rotator;

        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        // Override Methods
        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        public override IEnumerator Effect(float duration)
        {
            if ((rotator.Speed + speedUpAmount) >= rotator.MaxSpeed)
            {
                rotator.LockSpeedChange = true;

                rotator.Speed += speedUpAmount;
                yield return new WaitForSeconds(duration);
                rotator.Speed -= speedUpAmount;

                rotator.LockSpeedChange = false;
            }
            else
            {
                yield return null;
            }
        }

        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        // Unity Methods
        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        private void Awake()
        {
            rotator = GetComponent<Rotator2D>();
        }
    }
}