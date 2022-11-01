using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

namespace Shark
{
    public class ScoreManager : MonoBehaviour
    {
        private int score = 0;
        
        private Color targetColor;
        private float interval = 5f;
        private float totalTime = 60f;

        public TextMeshProUGUI targetColorText;
        public TextMeshProUGUI scoreText;
        public TextMeshProUGUI alertText;
        public RectTransform instruction;

        
        // Start is called before the first frame update
        void Start()
        {
            targetColor = GetRandomColorWithOutDuplicate();
            StartCoroutine(DismissInstruction());
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKey(KeyCode.KeypadEnter))
            {
                Time.timeScale = 1;
            }
            interval -= Time.deltaTime;
            if (interval <= 0)
            {
                targetColor = GetRandomColorWithOutDuplicate();
                targetColorText.text = String.Format("Please eat {0} fish", targetColor.ToString());
                interval = 5f;
            }

            totalTime -= Time.deltaTime;
            if (totalTime < 0)
            {
                alertText.text = "Game Over";
                alertText.transform.parent.transform.gameObject.SetActive(true);
                Time.timeScale = 0;
            }
        }

        public Color GetTargetColor()
        {
            return targetColor;
        }
        
        public Color GetRandomColorWithOutDuplicate()
        {
            System.Random random = new System.Random();
            Type type = typeof(Color);
            
            Array values = type.GetEnumValues();
            
            int index = random.Next(values.Length);
            Color color = (Color)values.GetValue(index);

            return color;
        }

        public void AddScore()
        {
            score += 5;
            scoreText.text = score.ToString();
        }
        
        public void MinusScore()
        {
            score -= 5;
            score = Mathf.Max(0, score);
            scoreText.text = score.ToString();
            StartCoroutine(ShowAlert("Ate Wrong Fish !"));
        }
        
        IEnumerator ShowAlert(string text)
        {
            alertText.text = text;
            alertText.transform.parent.transform.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            alertText.transform.parent.transform.gameObject.SetActive(false);
        }
        
        IEnumerator DismissInstruction()
        {
            yield return new WaitForSeconds(5);
            instruction.gameObject.SetActive(false);
        }
    }
    
}

