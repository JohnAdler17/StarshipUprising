using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvinciblePowerup : MonoBehaviour
{
  Health playerHealth;
  [SerializeField] private AudioSource collectSound;
  [SerializeField] private int invincibleTime = 15;

  private bool isCollected = false;

  void Awake() {
    playerHealth = GameObject.FindWithTag("Player").GetComponent<Health>();
  }

  void OnTriggerEnter2D(Collider2D other) {
    if(other.CompareTag("Player") && !isCollected) {
      playerHealth.Invincibility(invincibleTime);
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
