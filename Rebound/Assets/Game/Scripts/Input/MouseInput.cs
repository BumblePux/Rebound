//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
//
// Name:        MouseInput.cs
// Created by:  Pux
// Created on:  31/07/19
//
//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BumblePux.Rebound.Utils;

namespace BumblePux.Rebound.UserInput
{
    public class MouseInput : BaseUserInput
    {
        [SerializeField] Enums.MouseButton mouseButton = default;

        public override bool Clicked()
        {
            if (eventSystem != null)
            {
                if (eventSystem.IsPointerOverGameObject())
                    return false;
            }

            return Input.GetMouseButtonDown((int)mouseButton);
        }
    }
}