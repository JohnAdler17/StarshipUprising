using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTracker : MonoBehaviour
{
  static LevelTracker instance;

  private int lastLevelPlayed = 0;

  void Awake() {
    ManageSingleton();
  }

  void ManageSingleton() {
    if (instance != null) {
      gameObject.SetActive(false);
      Destroy(gameObject);
    }
    else {
      instance = this;
      DontDestroyOnLoad(gameObject);
    }
  }

  public void UpdateLastLevel(int index) {
    lastLevelPlayed = index;
    Debug.Log("Last level played: " + lastLevelPlayed);
  }

  public int GetLastLevelIndex() {
    return lastLevelPlayed;
  }
}
