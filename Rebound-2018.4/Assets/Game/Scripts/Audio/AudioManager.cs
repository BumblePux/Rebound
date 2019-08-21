//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
//
// Name:        AudioManager.cs
// Created by:  Pux
// Created on:  07/08/19
//
//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace BumblePux.Rebound.Audio
{
    public class AudioManager : MonoBehaviour
    {
        private static AudioManager instance;

        public AudioMixerGroup backgroundMixer;
        public AudioMixerGroup sfxMixer;

        private AudioSource primarySource;
        private AudioSource secondarySource;
        private AudioSource sfxSource;

        private bool isFirstSourcePlaying;

        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        // Public Methods
        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        public static void Play(Sound sound)
        {
            if (instance == null)
                return;

            AudioSource activeSource = instance.isFirstSourcePlaying ? instance.primarySource : instance.secondarySource;

            activeSource.clip = sound.clip;
            activeSource.volume = sound.volume;
            activeSource.pitch = sound.pitch;
            activeSource.Play();            
        }

        //----------------------------------------
        public static void PlaySfx(Sound sound)
        {
            if (instance == null)
                return;

            instance.sfxSource.volume = sound.volume;
            instance.sfxSource.pitch = sound.pitch;
            instance.sfxSource.clip = sound.clip;
            instance.sfxSource.Play();
        }

        //----------------------------------------
        public static void Stop()
        {
            if (instance == null)
                return;

            AudioSource activeSource = instance.isFirstSourcePlaying ? instance.primarySource : instance.secondarySource;

            activeSource.Stop();
        }

        //----------------------------------------
        public static void FadeIn(Sound sound, float duration = 1f)
        {
            if (instance == null)
                return;

            AudioSource activeSource = instance.isFirstSourcePlaying ? instance.primarySource : instance.secondarySource;
            activeSource.clip = sound.clip;
            activeSource.volume = sound.volume;
            activeSource.clip = sound.clip;

            instance.StartCoroutine(instance.FadeAudioIn(activeSource, duration));
        }

        //----------------------------------------
        public static void FadeOut(float duration = 1f)
        {
            if (instance == null)
                return;

            AudioSource activeSource = instance.isFirstSourcePlaying ? instance.primarySource : instance.secondarySource;
            
            if (activeSource.isPlaying)
                instance.StartCoroutine(instance.FadeAudioOut(activeSource, duration));
        }

        //----------------------------------------
        public static void CrossFade(Sound sound, float duration = 1f)
        {
            if (instance == null)
                return;

            AudioSource activeSource = instance.isFirstSourcePlaying ? instance.primarySource : instance.secondarySource;
            AudioSource newSource = instance.isFirstSourcePlaying ? instance.secondarySource : instance.primarySource;

            instance.isFirstSourcePlaying = !instance.isFirstSourcePlaying;

            newSource.clip = sound.clip;
            newSource.volume = sound.volume;
            newSource.clip = sound.clip;
            newSource.Play();

            instance.StartCoroutine(instance.CrossFadeAudio(activeSource, newSource, sound, duration));
        }

        //----------------------------------------
        public static void SetMusicVolume(float volume)
        {
            if (instance == null)
                return;

            instance.primarySource.volume = volume;
            instance.secondarySource.volume = volume;
        }

        //----------------------------------------
        public static void SetSfxVolume(float volume)
        {
            if (instance == null)
                return;

            instance.sfxSource.volume = volume;
        }

        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        // Unity Methods
        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        private void Awake()
        {
            SetupSingleton();
            SetupAudioSources();            
        }

        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        // Private Methods
        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        private void SetupSingleton()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
                return;
            }
            else
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }

        //----------------------------------------
        private void SetupAudioSources()
        {
            // Create sources
            primarySource = gameObject.AddComponent<AudioSource>();
            secondarySource = gameObject.AddComponent<AudioSource>();
            sfxSource = gameObject.AddComponent<AudioSource>();

            // Set parameters
            primarySource.loop = true;
            primarySource.outputAudioMixerGroup = backgroundMixer;

            secondarySource.loop = true;
            secondarySource.outputAudioMixerGroup = backgroundMixer;

            sfxSource.loop = false;
            sfxSource.outputAudioMixerGroup = sfxMixer;
        }

        //----------------------------------------
        private IEnumerator FadeAudioIn(AudioSource activeSource, float duration)
        {
            if (!activeSource.isPlaying)
                activeSource.Play();

            for (float transition = 0f; transition < duration; transition += Time.deltaTime)
            {
                activeSource.volume = transition / duration;
                yield return null;
            }            
        }

        //----------------------------------------
        private IEnumerator FadeAudioOut(AudioSource activeSource, float duration)
        {
            for (float transition = 0f; transition < duration; transition += Time.deltaTime)
            {
                activeSource.volume = 1 - (transition / duration);
                yield return null;
            }

            activeSource.Stop();
        }

        //----------------------------------------
        private IEnumerator CrossFadeAudio(AudioSource originalSource, AudioSource newSource, Sound sound, float duration)
        {
            for (float transition = 0f; transition < duration; transition += Time.deltaTime)
            {
                originalSource.volume = sound.volume - (transition / duration);
                newSource.volume = (transition / duration) * sound.volume;
                yield return null;
            }

            originalSource.Stop();
        }
    }
}