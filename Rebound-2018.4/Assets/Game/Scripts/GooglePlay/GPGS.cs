//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
//
// Name:        GPGS.cs
// Created by:  Pux
// Created on:  21/08/19
//
//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

namespace BumblePux.Rebound.GPGServices
{
    public static class GPGS
    {
        public static void Initialize()
        {
            PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
                // enable saving game progress
                .EnableSavedGames()
                .Build();

            PlayGamesPlatform.InitializeInstance(config);
            // recommended for debugging
            PlayGamesPlatform.DebugLogEnabled = true;
            // Activate the Google Play Games platform
            PlayGamesPlatform.Activate();
        }

        public static void ShowLeaderboardUI()
        {
            if (Social.localUser.authenticated)
                Social.ShowLeaderboardUI();
        }

        public static void ShowAchievementsUI()
        {
            if (Social.localUser.authenticated)
                Social.ShowAchievementsUI();
        }

        public static void PostToLeaderboard(int score, string leaderboard)
        {
            if (Social.localUser.authenticated)
            {
                Social.ReportScore(score, leaderboard, (bool success) =>
                {
                    // Handle success or failure
                });
            }
        }

        public static void SetPopupGravity(Gravity gravity)
        {
            if (Social.localUser.authenticated)
            {
                ((PlayGamesPlatform)Social.Active).SetGravityForPopups(gravity);
            }
        }

        public static void SignIn()
        {
            Social.localUser.Authenticate((bool success) =>
            {
                if (success)
                {
                    //Debug.Log("Login Successful");
                }
                else
                {
                    //Debug.Log("Login failed");
                }
            });
        }

        public static void UnlockWelcomeAchievement()
        {
            if (!Social.localUser.authenticated)
                return;

            Social.ReportProgress(Rebound_GPGSIds.achievement_welcome_to_rebound, 100f, null);
        }

        public static void AttemptTimedModeAchievementsUnlock(float currentScore)
        {
            if (!Social.localUser.authenticated)
                return;

            switch (currentScore)
            {
                case 10:
                    Social.ReportProgress(Rebound_GPGSIds.achievement_getting_started, 100f, null);
                    break;
                case 30:
                    Social.ReportProgress(Rebound_GPGSIds.achievement_now_youre_getting_it, 100f, null);
                    break;
                case 50:
                    Social.ReportProgress(Rebound_GPGSIds.achievement_now_you_lose_more_time, 100f, null);
                    break;
                case 80:
                    Social.ReportProgress(Rebound_GPGSIds.achievement_youre_still_going, 100f, null);
                    break;
                case 100:
                    Social.ReportProgress(Rebound_GPGSIds.achievement_you_cant_miss_now, 100f, null);
                    break;
                case 150:
                    Social.ReportProgress(Rebound_GPGSIds.achievement_what_a_pro, 100f, null);
                    break;
            }
        }
    }
}