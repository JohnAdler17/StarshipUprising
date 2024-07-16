using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    static ScoreKeeper instance;

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

    int playerScore = 0;

    public int GetCurrentScore() {
      return playerScore;
    }

    public void ModifyScore(int amount) {
      playerScore += amount;
      Mathf.Clamp(playerScore, 0, int.MaxValue);
      //Debug.Log(playerScore);
    }

    public void ResetScore() {
      playerScore = 0;
    }
}
