//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
//
// Name:        BackgroundMusicPlayer.cs
// Created by:  Pux
// Created on:  07/08/19
//
//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BumblePux.Rebound.Audio
{
    public class BackgroundMusicPlayer : MonoBehaviour
    {
        [SerializeField] private AudioClip backgroundClip = default;
        [Range(0f, 1f)]
        [SerializeField] private float volume = 0.8f;
        [SerializeField] private bool playOnSceneLoad = default;

        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        // Public Methods
        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        public void PlayBackgroundMusic()
        {
            if (backgroundClip != null)
                AudioManager.PlayMusic(backgroundClip);
        }

        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        // Unity Methods
        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        private void Start()
        {
            AudioManager.SetMusicVolume(volume);

            if (playOnSceneLoad)
                PlayBackgroundMusic();
        }
    }
}