using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [Header("General")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileLifetime = 5f;
    [SerializeField] float baseFiringRate = 0.2f;

    [SerializeField] private GameObject fireRateAura;
    IEnumerator DoubleFireRate(float time) {
      baseFiringRate = baseFiringRate / 2f;
      fireRateAura.SetActive(true);
      yield return new WaitForSeconds(time);
      baseFiringRate *= 2f;
      fireRateAura.SetActive(false);
    }
    public void FireRatePowerup(float fireRateTime) {
      StartCoroutine(DoubleFireRate(fireRateTime));
    }

    // make a fire rate powerup prefab, then in awake method, findobjectwithtag(player).getcomponent(shooter), then when touched, call fireratepowerupmethod

    [Header("AI")]
    [SerializeField] bool useAI;
    [SerializeField] float firingRateVariance = 0f;
    [SerializeField] float minFiringRate = 1f;

    [HideInInspector] public bool isFiring;

    Coroutine firingCoroutine;
    AudioPlayer audioPlayer;

    void Awake() {
      audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    void Start()
    {
      if (useAI) {
        isFiring = true;
      }
    }

    void Update()
    {
      Fire();
    }

    void Fire() {
      if (isFiring && firingCoroutine == null) {
        firingCoroutine = StartCoroutine(FireContinuously());
      }
      else if (!isFiring && firingCoroutine != null){
        StopCoroutine(firingCoroutine);
        firingCoroutine = null;
      }
    }


    IEnumerator FireContinuously() {
      while (true) {
        GameObject instance = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

        Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();
        if (rb != null) {
          rb.velocity = transform.up * projectileSpeed;
        }

        Destroy(instance, projectileLifetime);

        float timeToNextProjectile = Random.Range(baseFiringRate - firingRateVariance, baseFiringRate + firingRateVariance);
        timeToNextProjectile = Mathf.Clamp(timeToNextProjectile, minFiringRate, float.MaxValue);

        audioPlayer.PlayShootingClip();
        //audioPlayer.GetInstance().PlayShootingClip(); //bypasses the need to find the AudioPlayer in the hierarchy in this script using the singleton pattern in AudioPlayer.cs
        //singleton works well with small games but in a large game, hunting down errors in the singleton pattern can be a nightmare because you lose where the instance is

        yield return new WaitForSeconds(timeToNextProjectile);
      }
    }


}
