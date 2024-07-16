using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int health = 50;
    [SerializeField] int maxHealth = 50;
    [SerializeField] ParticleSystem hitEffect;

    [SerializeField] bool isPlayer;
    [SerializeField] int score = 50;

    CameraShake cameraShake;
    [SerializeField] bool applyCameraShake;

    AudioPlayer audioPlayer;
    ScoreKeeper scoreKeeper;
    LevelManager levelManager;

    private bool isInvincible = false;

    [SerializeField] private GameObject invincibilityAura;

    IEnumerator InvinciblePowerup(int time) {
      isInvincible = true;
      invincibilityAura.SetActive(true);
      yield return new WaitForSeconds(time);
      isInvincible = false;
      invincibilityAura.SetActive(false);
    }

    public void Invincibility(int invincibilityTime) {
      StartCoroutine(InvinciblePowerup(invincibilityTime));
    }

    public int GetHealth() {
      return health;
    }

    void Awake() {
      cameraShake = Camera.main.GetComponent<CameraShake>();
      audioPlayer = FindObjectOfType<AudioPlayer>();
      scoreKeeper = FindObjectOfType<ScoreKeeper>();
      levelManager = FindObjectOfType<LevelManager>();
    }

    void OnTriggerEnter2D(Collider2D other) {
      DamageDealer damageDealer = other.GetComponent<DamageDealer>(); //if this fails, damageDealer is null

      if (damageDealer != null) {
        TakeDamage(damageDealer.GetDamage());
        PlayHitEffect();
        ShakeCamera();
        audioPlayer.PlayShipHitClip();
        damageDealer.Hit();
      }
    }

    void TakeDamage(int damage) {
      if (isInvincible && isPlayer) damage = 0;
      health -= damage;
      if (health <= 0) {
        Die();
      }
    }

    public void AddHealth(int amount) {
      health += amount;
      if (health > maxHealth) {
        health = maxHealth;
      }
    }

    void Die() {
      if (!isPlayer) {
        scoreKeeper.ModifyScore(score);
      }
      else {
        levelManager.LoadGameOver();
      }
      Destroy(gameObject);
    }

    void PlayHitEffect() {
      if(hitEffect != null) {
        ParticleSystem instance = Instantiate(hitEffect, transform.position, Quaternion.identity);
        Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
      }
    }

    void ShakeCamera() {
      if (cameraShake != null && applyCameraShake) {
        cameraShake.Play();
      }
    }
}
