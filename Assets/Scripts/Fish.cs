using System;
using System.Collections;
using System.Collections.Generic;
using Shark;
using UnityEngine;

namespace Shark
{
    public class Fish : MonoBehaviour
    {
        private FishID ID;
        [SerializeField] private MeshRenderer meshRenderer;
        
        private float fishSpeed = 10f;

        private float deactivationDistance = 30f;

        [System.NonSerialized] public Fish next;
        [System.NonSerialized] public FishPool fishPool;

        public static Action<Fish> onFishTriggerEnter;

        void Update()
        {        
            transform.Translate(Vector3.down* fishSpeed * Time.deltaTime);

            if (Vector3.SqrMagnitude(transform.position) > deactivationDistance * deactivationDistance)
            {
                fishPool.ConfigureDeactivatedFish(this);
                gameObject.SetActive(false);
            }
        }

        public void InitID()
        {
            ID = new FishID(GetRandomColor(), GetRandomTrack());
        }

        public FishID GetFishID()
        {
            return ID;
        }
        
        public Color GetRandomColor()
        { 
            System.Random random = new System.Random();
            Type type = typeof(Color);
            
            Array values = type.GetEnumValues();
            
            int index = random.Next(values.Length);
            Color color = (Color)values.GetValue(index);

             return color;
        }
        
        public Track GetRandomTrack()
        { 
            System.Random random = new System.Random();
            Type type = typeof(Track);
            
            Array values = type.GetEnumValues();
            
            int index = random.Next(values.Length);
            Track track = (Track)values.GetValue(index);

            return track;
        }

        private void OnTriggerEnter(Collider other)
        {
            onFishTriggerEnter?.Invoke(this);
        }

        public void SetMeshAndTrackAccordingToID()
        {
            if (ID == null) return;
            switch (ID.GetColor())
            {
                case Color.Blue:
                    meshRenderer.material.color = UnityEngine.Color.blue;
                    break;
                case Color.Red:
                    meshRenderer.material.color = UnityEngine.Color.red;
                    break;
                case Color.Green:
                    meshRenderer.material.color = UnityEngine.Color.green;
                    break;
            }

            switch (ID.GetTrack())
            {
                case Track.Left:
                    transform.position = fishPool.TrackLeft.position;
                    break;
                case Track.Mid:
                    transform.position = fishPool.TrackMid.position;
                    break;
                case Track.Right:
                    transform.position = fishPool.TrackRight.position;
                    break;
            }
        }
    }
}
