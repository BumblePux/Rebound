//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
//
// Name:        CameraShake.cs
// Created by:  Pux
// Created on:  02/09/19
//
//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

using System.Collections;
using UnityEngine;

namespace BumblePux.Rebound.General
{
    public class CameraShake : MonoBehaviour
    {
        private static CameraShake instance;

        private new Transform transform;

        public static void TriggerShake(float duration, float magnitude)
        {
            instance.StopAllCoroutines();
            instance.StartCoroutine(instance.Shake(duration, magnitude));
        }

        private IEnumerator Shake(float duration, float magnitude)
        {
            Vector3 originalPos = transform.localPosition;
            float elapsed = 0f;

            while (elapsed < duration)
            {
                float x = Random.Range(-1f, 1f) * magnitude;
                float y = Random.Range(-1f, 1f) * magnitude;

                transform.localPosition = new Vector3(x, y, originalPos.z);
                elapsed += Time.deltaTime;
                yield return null;
            }

            transform.localPosition = originalPos;
        }

        private void Awake()
        {
            SetupSingleton();

            if (transform == null)
                transform = GetComponent<Transform>();
        }

        private void SetupSingleton()
        {
            if (instance != null && instance != this)
                Destroy(gameObject);
            else
                instance = this;
        }
    }
}