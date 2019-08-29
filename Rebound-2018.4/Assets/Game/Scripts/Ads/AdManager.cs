//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
//
// Name:        AdManager.cs
// Created by:  Pux
// Created on:  16/08/19
//
//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.Events;
using UnityEngine.UI;

namespace BumblePux.Rebound.Ads
{
    public class AdManager : MonoBehaviour, IUnityAdsListener
    {
        private static AdManager instance;

        public UnityEvent OnAdCompleted;

        private string gameId = "3258294";
        private string unityPlacementId = "rewardedVideo";
        private bool testMode = true;

        private bool isAdReady = false;

        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        // Public Methods
        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        
        //----------------------------------------
        public static void ShowTimedModeRewardedAd()
        {
            if (instance == null)
                return;

            Advertisement.Show(instance.unityPlacementId);
        }

        //----------------------------------------
        public static bool IsAdReady()
        {
            return instance.isAdReady;
        }

        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        // IUnityAdsListener Interface Methods
        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        public void OnUnityAdsDidError(string message)
        {
            //Debug.Log("Ad did error: " + message);
        }

        //----------------------------------------
        public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
        {
            if (showResult == ShowResult.Finished)
            {
                //Debug.Log("Ad finished. Dishing out rewards!");
                instance.OnAdCompleted.Invoke();
            }
            else if (showResult == ShowResult.Skipped)
            {
                //Debug.Log("Ad skipped. No rewards for you!");
            }
            else if (showResult == ShowResult.Failed)
            {
                //Debug.Log("Ad did not finish due to an error");
            }
        }

        //----------------------------------------
        public void OnUnityAdsDidStart(string placementId)
        {
            //Debug.Log("Ad did start: " + placementId);
        }

        //----------------------------------------
        public void OnUnityAdsReady(string placementId)
        {
            if (placementId == unityPlacementId)
            {
                instance.isAdReady = true;
            }
        }

        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        // Unity Methods
        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        private void OnEnable()
        {
            SetupNonPersistentSingleton();
        }

        //----------------------------------------
        private void Start()
        {
            Advertisement.AddListener(this);
            Advertisement.Initialize(gameId, testMode);
        }

        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        // Private Methods
        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        private void SetupNonPersistentSingleton()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
                return;
            }
            else
            {
                instance = this;
            }
        }
    }
}