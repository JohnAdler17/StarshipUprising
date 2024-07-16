using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRatePowerup : MonoBehaviour
{
  Shooter playerShooter;
  [SerializeField] private AudioSource collectSound;
  [SerializeField] private float doubleFireRateTime = 10f;

  private bool isCollected = false;

  void Awake() {
    playerShooter = GameObject.FindWithTag("Player").GetComponent<Shooter>();
  }

  void OnTriggerEnter2D(Collider2D other) {
    if(other.CompareTag("Player") && !isCollected) {
      playerShooter.FireRatePowerup(doubleFireRateTime);
      isCollected = true;
      StartCoroutine(PlayCollectSound());
    }
  }

  IEnumerator PlayCollectSound() {
    collectSound.Play();
    yield return new WaitForSeconds(0.1f);
    Destroy(gameObject);
  }
}
