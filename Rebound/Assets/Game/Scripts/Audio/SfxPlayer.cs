//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
//
// Name:        SfxPlayer.cs
// Created by:  Pux
// Created on:  08/08/19
//
//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BumblePux.Rebound.Audio
{
    public class SfxPlayer : MonoBehaviour
    {
        [SerializeField] private AudioClip sfxClip = default;

        public void PlaySfx()
        {
            if (sfxClip != null)
                AudioManager.PlaySfx(sfxClip);
        }
    }
}