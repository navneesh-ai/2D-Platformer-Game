using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelOverController : MonoBehaviour
{
private void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.gameObject.GetComponent<PlayerController>() != null)
    {
      Debug.Log("Player completed the level");
      int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
      int nextSceneIndex = currentSceneIndex + 1;
      if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
      {
        SceneManager.LoadScene(nextSceneIndex);
      }
      else
      {
        Debug.Log("No more levels to load.");
      }
    }
  }
}
