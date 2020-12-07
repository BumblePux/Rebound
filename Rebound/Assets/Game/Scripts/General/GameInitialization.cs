//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
//
// Name:        GameInitialization.cs
// Created by:  Pux
// Created on:  28/08/19
//
//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;
using BumblePux.Rebound.Audio;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

namespace BumblePux.Rebound.General
{
    public class GameInitialization : MonoBehaviour
    {
        public bool playGamesDebugEnabled = true;

        private void Start()
        {
            // Setup Google Play Games Services
            InitializeGooglePlay();
            SignIn();

            // Setup Unity Ads Service
            InitializeUnityAds();

            // Setup Audio preferences
            SetupMusicPreferences();
            SetupSfxPreferences();

            // Load Main Menu
//#if UNITY_EDITOR
//            SceneManager.LoadScene("MainMenu");
//#endif
        }

        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        // Private Methods
        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

        //----------------------------------------
        private void InitializeGooglePlay()
        {
            PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
                .EnableSavedGames() // enables saving game progress
                .Build();

            PlayGamesPlatform.InitializeInstance(config);
            PlayGamesPlatform.DebugLogEnabled = playGamesDebugEnabled;   // recommended for debugging
            PlayGamesPlatform.Activate();                               // activate the Google Play Games platform
        }

        //----------------------------------------
        private void SignIn()
        {
            Social.localUser.Authenticate((bool success) =>
            {
                if (success)
                    ((PlayGamesPlatform)Social.Active).SetGravityForPopups(Gravity.BOTTOM);

                // Load main menu, regardless of whether we successfully signed in or not.
                SceneManager.LoadScene("MainMenu");
            });
        }

        //----------------------------------------
        private void InitializeUnityAds()
        {
            if (!Advertisement.isInitialized)
                Advertisement.Initialize(AdsData.gameId, AdsData.testMode);
        }

        //----------------------------------------
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