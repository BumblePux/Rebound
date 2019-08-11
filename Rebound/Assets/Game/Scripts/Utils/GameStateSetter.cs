//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
//
// Name:        GameStateSetter.cs
// Created by:  Pux
// Created on:  09/08/19
//
//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BumblePux.Rebound.Utils
{
    public class GameStateSetter : MonoBehaviour
    {
        public void ToggleGameOver()
        {
            GameState.IsGameOver = !GameState.IsGameOver;
        }

        public void ToggleGameModeActive()
        {
            GameState.GameModeActive = !GameState.GameModeActive;
        }

        public void ToggleIsPaused()
        {
            GameState.IsPaused = !GameState.IsPaused;
        }
    }
}