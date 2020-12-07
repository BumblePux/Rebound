//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
//
// Name:        Player.cs
// Created by:  Pux
// Created on:  12/08/19
//
//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

using UnityEngine;
using BumblePux.Rebound.General;
using BumblePux.Rebound.Interactables;

namespace BumblePux.Rebound.Player
{
    public class PlayerController : MonoBehaviour
    {        
        public Rotator2D rotator;
        public float maxSpeed = 600f;

        private Animator animator;
        private SpriteRenderer sr;
        private Transform trail;

        [HideInInspector]
        public Target target;

        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        // Public Methods
        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

        //----------------------------------------
        public void SetSpeed(float speed)
        {
            rotator.speed = speed;
        }

        //----------------------------------------
        public void IncreaseSpeed(float amount)
        {
            rotator.speed += amount;
            if (rotator.speed > maxSpeed)
                rotator.speed = maxSpeed;
        }

        //----------------------------------------
        public void AttemptChangeDirection()
        {
            int toggle = Random.Range(0, 2);
            if (toggle == 1)
            {
                rotator.ChangeDirection();
                FlipImage();
            }
        }

        //----------------------------------------
        public void Disappear()
        {
            animator.SetTrigger("sceneChange");
        }

        //----------------------------------------
        public void GetReferences()
        {
            animator = GetComponentInChildren<Animator>();
            sr = GetComponentInChildren<SpriteRenderer>();
            trail = GetComponentInChildren<TrailRenderer>().gameObject.GetComponent<Transform>();
        }

        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        // Unity Methods
        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

        //----------------------------------------
        private void OnTriggerEnter2D(Collider2D collision)
        {
            target = collision.gameObject.GetComponentInParent<Target>();
        }

        //----------------------------------------
        private void OnTriggerExit2D(Collider2D collision)
        {
            target = null;
        }

        //----------------------------------------
        private void Start()
        {
            GetReferences();
        }

        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        // Private Methods
        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

        //----------------------------------------
        private void FlipImage()
        {
            sr.flipY = !sr.flipY;

            Vector3 newPos = trail.localPosition;
            newPos = new Vector3(newPos.x, newPos.y * -1f, newPos.z);
            trail.localPosition = newPos;
        }
    }
}