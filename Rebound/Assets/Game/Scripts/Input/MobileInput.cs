//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
//
// Name:        MobileInput.cs
// Created by:  Pux
// Created on:  11/08/19
//
//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BumblePux.Rebound.UserInput
{
    public class MobileInput : BaseUserInput
    {
        public override bool Clicked()
        {
            if (Input.touchCount > 0)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began && eventSystem.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                    return false;

                return Input.GetTouch(0).phase == TouchPhase.Began;
            }
            else
                return false;
        }
    }
}