//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
//
// Name:        EffectController.cs
// Created by:  Pux
// Created on:  02/08/19
//
//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace BumblePux.Rebound.EffectSystem
{
    public class EffectController : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private TMP_Text popupText = default;

        [Header("Settings")]
        [SerializeField] private float effectDuration = 5f;

        [Header("Effects List")]
        [SerializeField] private List<BaseEffect> effects = new List<BaseEffect>();

        // Private References
        private Animator anim;

        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        // Public Methods
        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        public void TriggerRandomEffect()
        {
            int effectIndex = Random.Range(0, effects.Count);

            if (effects[effectIndex].CanBePlayed && !BaseEffect.IsRunning)
            {
                popupText.text = effects[effectIndex].PopupString;
                anim.SetTrigger("PlayPopup");

                StartCoroutine(effects[effectIndex].TriggerEffect(effectDuration));
            }            
        }

        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        // Unity Methods
        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        private void Start()
        {
            anim = GetComponent<Animator>();

            FindEffects();
        }

        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        // Private Methods
        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        private void FindEffects()
        {
            var foundEffects = FindObjectsOfType<BaseEffect>();

            foreach (var effect in foundEffects)
            {
                effects.Add(effect);
            }
        }
    }
}