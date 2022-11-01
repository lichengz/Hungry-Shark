using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace Shark
{
    public class FishPool : MonoBehaviour
    {
        public ScoreManager scoreManager;
        public Fish fishPrefab;
        public Transform TrackLeft;
        public Transform TrackMid;
        public Transform TrackRight;
        
        private List<Fish> fishs = new List<Fish>();

        private const int INITIAL_POOL_SIZE = 10;

        private const int MAX_POOL_SIZE = 20;
        
        private Fish firstAvailable;


        private void Start()
        {
            if (fishPrefab == null)
            {
                Debug.LogError("Need a reference to the fish prefab");
            }

            Fish.onFishTriggerEnter += OnFishTriggerEnter;
            
            for (int i = 0; i < INITIAL_POOL_SIZE; i++)
            {
                GenerateFish();
            }


            firstAvailable = fishs[0];

            for (int i = 0; i < fishs.Count - 1; i++)
            {
                fishs[i].next = fishs[i + 1];
            }

            fishs[fishs.Count - 1].next = null;
        }


        private void GenerateFish()
        {
            Fish newFish = Instantiate(fishPrefab, transform);

            newFish.gameObject.SetActive(false);

            fishs.Add(newFish);

            newFish.fishPool = this;
        }

        private void OnFishTriggerEnter(Fish fish)
        {
            ConfigureDeactivatedFish(fish);
            fish.gameObject.SetActive(false);
            if (fish.GetFishID().GetColor() == scoreManager.GetTargetColor())
            {
                scoreManager.AddScore();
            }
            else
            {
                scoreManager.MinusScore();
            }
            
        }


        public void ConfigureDeactivatedFish(Fish deactivatedObj)
        {
            deactivatedObj.next = firstAvailable;

            firstAvailable = deactivatedObj;
        }


        public Fish GetFish()
        {
            if (firstAvailable == null)
            {
                if (fishs.Count < MAX_POOL_SIZE)
                {
                    GenerateFish();

                    Fish lastFish = fishs[fishs.Count - 1];

                    ConfigureDeactivatedFish(lastFish);
                }
                else
                {
                    return null;
                }
            }

            Fish newFish = firstAvailable;
            newFish.InitID();

            firstAvailable = newFish.next;

            return newFish;
        }
    }
}
