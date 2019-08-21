//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
//
// Name:        Rotator2D.cs
// Created by:  Pux
// Created on:  31/07/19
//
//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BumblePux.Rebound.Utils;
using System;

namespace BumblePux.Rebound.General
{
    public class Rotator2D : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float speed = default;
        [SerializeField] private bool useMaxSpeed = false;
        [SerializeField] private float maxSpeed = default;
        [SerializeField] private Enums.Rotation direction = default;

        [Header("Debug Flags")]
        [SerializeField] private bool lockSpeedChange = false;

        private Rigidbody2D rb2d;

        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        // Public Properties
        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        public float Speed
        {
            get { return speed; }
            set
            {
                speed = value;
                CheckSpeedIsValid();
            }
        }

        //----------------------------------------
        public bool UseMaxSpeed
        {
            get { return useMaxSpeed; }
            set
            {
                useMaxSpeed = value;
                CheckSpeedIsValid();
            }
        }

        //----------------------------------------
        public float MaxSpeed
        {
            get { return maxSpeed; }
            set { maxSpeed = value; }
        }

        //----------------------------------------
        public Enums.Rotation Direction
        {
            get { return direction; }
        }

        //----------------------------------------
        public bool LockSpeedChange
        {
            get { return lockSpeedChange; }
            set { lockSpeedChange = value; }
        }

        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        // Public Methods
        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        public void ChangeDirection()
        {
            if (direction == Enums.Rotation.Clockwise)
                direction = Enums.Rotation.AntiClockwise;
            else
                direction = Enums.Rotation.Clockwise;
        }

        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        // Unity Methods
        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        private void Awake()
        {
            rb2d = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            Rotate();
        }

        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        // Private Methods
        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        private void Rotate()
        {
            if (direction == Enums.Rotation.Clockwise)
                rb2d.MoveRotation(rb2d.rotation - speed * Time.fixedDeltaTime);
            else
                rb2d.MoveRotation(rb2d.rotation + speed * Time.fixedDeltaTime);
        }

        //----------------------------------------
        private void CheckSpeedIsValid()
        {
            if (useMaxSpeed && speed > maxSpeed)
                speed = maxSpeed;
        }
    }
}