//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
//
// Name:        ScoreManager.cs
// Created by:  Pux
// Created on:  13/08/19
//
//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

using UnityEngine;

namespace BumblePux.Rebound.Score
{
    public class ScoreManager : MonoBehaviour
    {
        private static ScoreManager instance;

        public Int scoreData;

        public static int Score
        {
            get { return instance.scoreData.Value; }
        }

        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        // Public Methods
        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        public static void IncreaseScore(int amount = 1)
        {
            if (instance == null) return;

            instance.scoreData.Value += amount;            
        }

        //----------------------------------------
        public static void ResetScore()
        {
            if (instance == null) return;

            instance.scoreData.Value = 0;
        }

        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        // Unity Methods
        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        private void Awake()
        {
            // Setup singleton
            if (instance != null && instance != this)
                Destroy(gameObject);
            else
                instance = this;
        }
    }
}