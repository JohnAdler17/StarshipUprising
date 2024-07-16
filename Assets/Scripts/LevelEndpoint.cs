using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEndpoint : MonoBehaviour
{
    LevelManager levelManager;
    [SerializeField] private bool isFinalLevel = false;

    void Awake() {
      levelManager = FindObjectOfType<LevelManager>();
    }

    void OnTriggerEnter2D(Collider2D other) {
      if(other.CompareTag("Player")) {
        if (!isFinalLevel) {
          levelManager.LoadLevelComplete();
        }
        else {
          levelManager.LoadFinalLevelComplete();
        }
      }
    }
}
