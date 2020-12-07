//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
//
// Name:        Rotator2D.cs
// Created by:  Pux
// Created on:  31/07/19
//
//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

using UnityEngine;
using BumblePux.Rebound.Utils;

namespace BumblePux.Rebound.General
{
    public class Rotator2D : MonoBehaviour
    {
        public Rigidbody2D rb2d;
        public float speed = 50f;
        public Enums.Rotation direction;

        public void ChangeDirection()
        {
            if (direction == Enums.Rotation.Clockwise)
                direction = Enums.Rotation.AntiClockwise;
            else
                direction = Enums.Rotation.Clockwise;
        }

        private void FixedUpdate()
        {
            Rotate();
        }

        private void Rotate()
        {
            if (direction == Enums.Rotation.Clockwise)
                rb2d.MoveRotation(rb2d.rotation - speed * Time.fixedDeltaTime);
            else
                rb2d.MoveRotation(rb2d.rotation + speed * Time.fixedDeltaTime);
        }
    }
}