//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
//
// Name:        GameState.cs
// Created by:  Pux
// Created on:  09/08/19
//
//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BumblePux.Rebound.GameControllers;

namespace BumblePux.Rebound.Utils
{
    public static class GameState
    {
        public static bool GameModeActive
        {
            get { return gameModeActive; }
            set
            {
                gameModeActive = value;
            }
        }

        public static bool IsGameOver
        {
            get { return isGameOver; }
            set
            {
                isGameOver = value;
            }
        }

        public static bool IsPaused
        {
            get { return isPaused; }
            set
            {
                isPaused = value;
            }
        }

        private static bool gameModeActive;
        private static bool isGameOver;
        private static bool isPaused;
    }
}