//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
//
// Name:        Tutorial.cs
// Created by:  Pux
// Created on:  06/09/19
//
//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

using BumblePux.Rebound.Audio;
using BumblePux.Rebound.Dialogue;
using BumblePux.Rebound.General;
using BumblePux.Rebound.Interactables;
using BumblePux.Rebound.Player;
using BumblePux.Rebound.UserInput;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BumblePux.Rebound.GameControllers
{
    public class Tutorial : MonoBehaviour
    {
        public BaseUserInput input;
        public PlayerController player;
        public Target target;
        public Sound tutorialAudio;
        public DialogueTrigger tutorialStartDialogue;
        public DialogueTrigger tapDialogue;
        public DialogueTrigger afterTapDialogue;

        private bool triggered;
        private bool tutorialFinished;

        private IEnumerator Start()
        {
            input = GetComponent<BaseUserInput>();
            AudioManager.CrossFade(tutorialAudio);
            yield return StartCoroutine(WaitToStartDialogue());
        }

        private void Update()
        {
            if (input.Clicked())
            {
                DialogueManager.Instance.DisplayNextSentence();

                if (player.target != null && triggered &&!tutorialFinished)
                {
                    tutorialFinished = true;
                    player.SetSpeed(50f);
                    target.ChangePosition();
                    CameraShake.TriggerShake(0.1f, 0.2f);
                    afterTapDialogue.TriggerDialogue();
                }
            }

            if (player.target != null && !triggered)
            {
                triggered = true;
                player.SetSpeed(0f);
                tapDialogue.TriggerDialogue();
            }
        }

        public void StartRocket()
        {
            player.SetSpeed(50f);
        }

        public void FinishTutorial()
        {
            if (tutorialFinished)
            {
                PlayerPrefs.SetInt("HasPlayed", 1);

                if (!SceneInformation.IntendedToLoadTutorial)
                    StartCoroutine(WaitToLoadScene(SceneInformation.NextScene));
                else
                    StartCoroutine(WaitToLoadScene("MainMenu"));
            }
        }

        private IEnumerator WaitToStartDialogue()
        {
            yield return new WaitForSeconds(1f);

            tutorialStartDialogue.TriggerDialogue();
        }

        private IEnumerator WaitToLoadScene(string sceneName)
        {
            yield return new WaitForSeconds(2f);
            SceneManager.LoadScene(sceneName);
        }
    }
}