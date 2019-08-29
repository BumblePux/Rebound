//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
//
// Name:        GameInitialization.cs
// Created by:  Pux
// Created on:  28/08/19
//
//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using BumblePux.Rebound.GPGServices;
using BumblePux.Rebound.Audio;

namespace BumblePux.Rebound.General
{
    public class GameInitialization : MonoBehaviour
    {
        private void Start()
        {
            // Setup Google Play Games Services
            GPGS.Initialize();
            GPGS.SetPopupGravity(GooglePlayGames.BasicApi.Gravity.BOTTOM);
            GPGS.SignIn();

            // Setup Audio preferences
            SetupMusicPreferences();
            SetupSfxPreferences();

            // Load Main Menu
            SceneManager.LoadScene("MainMenu");
        }

        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        // Private Methods
        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        private void SetupMusicPreferences()
        {
            if (!PlayerPrefs.HasKey("IsMusicEnabled"))
                AudioManager.SetMusicActive(true);
            else
                AudioManager.SetMusicActive(AudioManager.GetMusicEnabled());
        }

        //----------------------------------------
        private void SetupSfxPreferences()
        {
            if (!PlayerPrefs.HasKey("IsSfxEnabled"))
                AudioManager.SetSfxActive(true);
            else
                AudioManager.SetSfxActive(AudioManager.GetSfxEnabled());
        }
    }
}