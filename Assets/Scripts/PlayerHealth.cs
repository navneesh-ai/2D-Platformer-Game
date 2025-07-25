using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
      public int maxHealth = 3;
    public int currentHealth;
    public Image[] hearts; // Assign in Inspector

    void Start() {
        currentHealth = maxHealth;
        UpdateHearts();
    }

    public void TakeDamage() {
        currentHealth--;
        UpdateHearts();
        if (currentHealth <= 0) {
            FindObjectOfType<PlayerController>().KillPlayer();
        }
    }

    void UpdateHearts() {
        for (int i = 0; i < hearts.Length; i++) {
            hearts[i].enabled = i < currentHealth;
        }
    }
}
