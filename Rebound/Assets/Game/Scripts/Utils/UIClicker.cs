//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
//
// Name:        UIClicker.cs
// Created by:  Pux
// Created on:  12/08/19
//
//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BumblePux.Rebound.Utils
{
    public class UIClicker : MonoBehaviour
    {
        private EventSystem eventSystem;

        private void Start()
        {
            eventSystem = EventSystem.current;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (eventSystem.IsPointerOverGameObject())
                {
                    Debug.Log("UI element clicked");
                }
                else
                {
                    Debug.Log("UI element missed");
                }
            }
        }
    }
}