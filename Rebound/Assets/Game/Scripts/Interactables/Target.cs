//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
//
// Name:        Target.cs
// Created by:  Pux
// Created on:  31/07/19
//
//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

using UnityEngine;
using BumblePux.Rebound.Audio;

namespace BumblePux.Rebound.Interactables
{
    public class Target : MonoBehaviour
    {
        public Sound hitSound;
        public GameObject explosionPrefab;
        public Transform graphic;

        [Range(0, 120)]
        public int moveAngle = 90;

        private Rigidbody2D rb2d;
        private Animator anim;
        private GameObject particles;

        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        // Public Methods
        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        public void ChangePosition()
        {
            AudioManager.PlaySfx(hitSound);
            particles.transform.position = graphic.position;
            particles.GetComponent<ParticleSystem>().Play();
            MoveToRandomPosition();
        }

        public void MoveToRandomPosition()
        {
            int angle = (int)(rb2d.rotation - 180f) + Random.Range(-moveAngle, moveAngle);
            rb2d.MoveRotation(angle);
            anim.Play("target_appear", -1, 0f);
        }

        //----------------------------------------
        public void HideTarget(bool active)
        {
            anim.SetBool("hide", active);
        }

        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        // Unity Methods
        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        private void Awake()
        {
            rb2d = GetComponent<Rigidbody2D>();
            anim = GetComponentInChildren<Animator>();

            particles = Instantiate(explosionPrefab, graphic.position, Quaternion.identity);
        }     
    }
}