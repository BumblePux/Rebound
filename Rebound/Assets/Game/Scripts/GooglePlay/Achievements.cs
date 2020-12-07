//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
//
// Name:        Achievements.cs
// Created by:  Pux
// Created on:  06/09/19
//
//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

using UnityEngine;

namespace BumblePux.Rebound
{
    public static class Achievements
    {
        public static void UnlockWelcomeToRebound()
        {
            if (Social.localUser.authenticated)
                Social.ReportProgress(Rebound_GPGSIds.achievement_welcome_to_rebound, 100f, null);
        }

        public static void UnlockTimedModeProgress(int scoreValue)
        {
            if (Social.localUser.authenticated)
            {
                switch (scoreValue)
                {
                    case 10:
                        Social.ReportProgress(Rebound_GPGSIds.achievement_getting_started, 100f, null);
                        break;
                    case 30:
                        Social.ReportProgress(Rebound_GPGSIds.achievement_now_youre_getting_it, 100f, null);
                        break;
                    case 50:
                        Social.ReportProgress(Rebound_GPGSIds.achievement_picking_up_the_pace, 100f, null);
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
}