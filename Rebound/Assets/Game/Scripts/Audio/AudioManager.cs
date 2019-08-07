//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
//
// Name:        AudioManager.cs
// Created by:  Pux
// Created on:  07/08/19
//
//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BumblePux.Rebound.Audio
{
    public class AudioManager : MonoBehaviour
    {
        private static AudioManager instance;

        [Header("Singleton Settings")]
        [SerializeField] private bool isPersistent = true;

        private AudioSource primarySource;
        private AudioSource secondarySource;
        private AudioSource sfxSource;

        private bool isPrimarySourcePlaying;

        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        // Public Methods
        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        public static void PlayMusic(AudioClip audioClip)
        {
            if (instance == null)
                return;

            AudioSource activeSource = instance.isPrimarySourcePlaying ? instance.primarySource : instance.secondarySource;

            activeSource.clip = audioClip;
            activeSource.Play();
        }

        //----------------------------------------
        public static void PlayMusicWithFade(AudioClip audioClip, float transitionTime = 1f)
        {
            if (instance == null)
                return;

            AudioSource activeSource = instance.isPrimarySourcePlaying ? instance.primarySource : instance.secondarySource;

            instance.StartCoroutine(UpdateMusicWithFade(activeSource, audioClip, transitionTime));
        }

        //----------------------------------------
        public static void PlayMusicWithCrossFade(AudioClip audioClip, float transitionTime = 1f)
        {
            if (instance == null)
                return;

            // Determine current and new audio source
            AudioSource activeSource = instance.isPrimarySourcePlaying ? instance.primarySource : instance.secondarySource;
            AudioSource newSource = instance.isPrimarySourcePlaying ? instance.secondarySource : instance.primarySource;

            // Swap the active source
            instance.isPrimarySourcePlaying = !instance.isPrimarySourcePlaying;

            newSource.clip = audioClip;
            newSource.Play();

            instance.StartCoroutine(UpdateMusicWithCrossFade(activeSource, newSource, transitionTime));
        }

        //----------------------------------------
        public static void PlaySfx(AudioClip audioClip)
        {
            if (instance == null)
                return;

            instance.sfxSource.PlayOneShot(audioClip);
        }

        //----------------------------------------
        public static void PlaySfx(AudioClip audioClip, float volume)
        {
            if (instance == null)
                return;

            instance.sfxSource.PlayOneShot(audioClip, volume);
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
            InitializeSingleton();

            CreateAudioSources();
            InitializeAudioSources();
        }

        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        // Private Methods
        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        private static IEnumerator UpdateMusicWithFade(AudioSource activeSource, AudioClip audioClip, float transitionTime)
        {
            if (!activeSource.isPlaying)
                activeSource.Play();

            // Do music fade out
            for (float transition = 0f; transition < transitionTime; transition += Time.deltaTime)
            {
                activeSource.volume = 1 - (transition / transitionTime);
                yield return null;
            }

            activeSource.Stop();
            activeSource.clip = audioClip;
            activeSource.Play();

            // Do music fade out
            for (float transition = 0f; transition < transitionTime; transition += Time.deltaTime)
            {
                activeSource.volume = transition / transitionTime;
                yield return null;
            }
        }

        //----------------------------------------
        private static IEnumerator UpdateMusicWithCrossFade(AudioSource originalSource, AudioSource newSource, float transitionTime)
        {
            for (float transition = 0f; transition < transitionTime; transition += Time.deltaTime)
            {
                originalSource.volume = 1 - (transition / transitionTime);
                newSource.volume = transition / transitionTime;
                yield return null;
            }

            originalSource.Stop();
        }

        //----------------------------------------
        private void InitializeSingleton()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                instance = this;

                if (isPersistent)
                    DontDestroyOnLoad(gameObject);
            }
        }

        //----------------------------------------
        private void CreateAudioSources()
        {
            primarySource = gameObject.AddComponent<AudioSource>();
            secondarySource = gameObject.AddComponent<AudioSource>();
            sfxSource = gameObject.AddComponent<AudioSource>();
        }

        //----------------------------------------
        private void InitializeAudioSources()
        {
            primarySource.loop = true;
            secondarySource.loop = true;
        }
    }
}