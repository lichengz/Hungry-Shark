using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shark
{
    public class FishSpawner : MonoBehaviour
    {
        public FishPool fishPool;

        private float rotationSpeed = 60f;

        private float fireTimer;

        private float fireInterval = 3f;



        void Start()
        {
            fireTimer = Mathf.Infinity;

            if (fishPool == null)
            {
                Debug.LogError("Need a reference to the object pool");
            }
        }



        void Update()
        {
            if (fireTimer > fireInterval)
            {
                fireTimer = 0f;

                Fish newFish = GetAFish();
                if (newFish != null)
                {
                    GameObject newFishGO = newFish.gameObject;
                    
                    newFish.SetMeshAndTrackAccordingToID();
                    
                    newFishGO.SetActive(true);
                }
                else
                {
                    Debug.Log("Couldn't find a new fish");
                }
            }
            
            fireTimer += Time.deltaTime;
        }



        private Fish GetAFish()
        {
            Fish fish = fishPool.GetFish();

            return fish;
        }
    }
}

