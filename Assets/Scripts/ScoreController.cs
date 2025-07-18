using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreController : MonoBehaviour
{
   public int score = 0;
   public TextMeshProUGUI scoreText;

   public void Awake()
   {
      scoreText = GetComponent<TextMeshProUGUI>();
   }

   public void IncrementScore(int increment)
   {
      score += increment;
      RefreshUI();
   }

   private void RefreshUI()
   {
      scoreText.text = "Score: " + score;
   }
   
}
