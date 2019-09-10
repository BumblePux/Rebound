//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
//
// Name:        Dialogie.cs
// Created by:  Pux
// Created on:  07/09/19
//
//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

using UnityEngine;

namespace BumblePux.Rebound.Dialogue
{
    [System.Serializable]
    public class Dialogue
    {
        [TextArea(3, 3)]
        public string[] sentences;
    }
}