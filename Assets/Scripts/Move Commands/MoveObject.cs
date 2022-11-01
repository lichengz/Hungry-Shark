using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Shark
{
    public class MoveObject : MonoBehaviour
    {
        private const float MOVE_STEP_DISTANCE = 10f;
        
        public event Action onFinishMoving;


        public void MoveForward()
        {
            Move(Vector3.left);
        }

        public void MoveBack()
        {
            Move(Vector3.right);
        }

        public void TurnLeft()
        {
            Move(Vector3.down);
        }

        public void TurnRight()
        {
            Move(Vector3.up);
        }


        private void Move(Vector3 dir)
        {
            StartCoroutine(SmoothMove(dir));
        }
        
        IEnumerator SmoothMove(Vector3 dir)
        {
            float duration = 0.5f;
            float timePassed = 0f;
            Vector3 curPos = transform.position;
            Vector3 targetPos = curPos + dir * MOVE_STEP_DISTANCE;
            while (timePassed < duration)
            {
                timePassed += Time.deltaTime;
                transform.position = Vector3.Lerp(curPos, targetPos, timePassed / duration);
                yield return null;
            }
            onFinishMoving?.Invoke();
        }
    }
}
