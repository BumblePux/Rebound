//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
//
// Name:        Rotator2DTestHelper.cs
// Created by:  Pux
// Created on:  31/07/19
//
//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace BumblePux.Rebound.General
{
    public class Rotator2DTestHelper : MonoBehaviour
    {
        public Rotator2D rotator;

        public TMP_InputField speedInput;
        public Toggle useMaxSpeedToggle;
        public TMP_InputField maxSpeedInput;
        public TMP_Text speedValueText;
        public TMP_Text maxSpeedValueText;
        public Button changeDirectionButton;
        public Button attemptChangeButton;

        private void Start()
        {
            useMaxSpeedToggle.isOn = rotator.UseMaxSpeed;
            speedValueText.text = "Speed: " + rotator.Speed.ToString();
            maxSpeedValueText.text = "Max Speed: " + rotator.MaxSpeed.ToString();

            useMaxSpeedToggle.onValueChanged.AddListener(UpdateMaxSpeedToggle);
            changeDirectionButton.onClick.AddListener(rotator.ChangeDirection);
            //attemptChangeButton.onClick.AddListener(rotator.AttemptChangeDirection);
        }

        public void UpdateSpeed(string valueText)
        {
            float.TryParse(valueText, out float speedValue);
            rotator.Speed = speedValue;

            speedInput.text = "";
            speedValueText.text = "Speed: " + rotator.Speed.ToString();
        }

        public void UpdateMaxSpeed(string valueText)
        {
            float.TryParse(valueText, out float speedValue);
            rotator.MaxSpeed = speedValue;

            maxSpeedInput.text = "";
            maxSpeedValueText.text = "Max Speed: " + rotator.MaxSpeed.ToString();
        }

        public void UpdateMaxSpeedToggle(bool toggle)
        {
            rotator.UseMaxSpeed = toggle;
            speedValueText.text = "Speed: " + rotator.Speed.ToString();
            maxSpeedValueText.text = "Max Speed: " + rotator.MaxSpeed.ToString();
        }
    }
}