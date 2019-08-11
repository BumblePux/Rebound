//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
//
// Name:        Planet.cs
// Created by:  Pux
// Created on:  02/08/19
//
//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BumblePux.Rebound.Planets
{
    public class Planet : MonoBehaviour
    {
        [SerializeField] private Sprite[] planetSprites = default;

        private SpriteRenderer sr;
        private Animator anim;

        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        // Public Methods
        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        public void ShowRandomPlanet()
        {
            int planetIndex = Random.Range(0, planetSprites.Length);

            sr.sprite = planetSprites[planetIndex];
            anim.SetTrigger("ShowPlanet");
        }

        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        // Unity Methods
        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        private void Awake()
        {
            sr = GetComponent<SpriteRenderer>();
            anim = GetComponent<Animator>();
        }

        //----------------------------------------
        private void OnEnable()
        {            
            ShowRandomPlanet();
        }
    }
}